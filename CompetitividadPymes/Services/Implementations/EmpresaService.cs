using AutoMapper;
using CompetitividadPymes.JwtSetup;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;
using CompetitividadPymes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompetitividadPymes.Services.Implementations
{


    //TODO : Implementar la validacion de los permisos en base al middleware de autorizacion para cada metodo o los pertinentes

    public class EmpresaService : IEmpresaService
    {
        private readonly PymesCompetitividadContext _context;
        private readonly IMapper _mapper;
        private readonly EncryptUtilities _encrypt;

        public EmpresaService(PymesCompetitividadContext context, IMapper mapper, EncryptUtilities encrypt)
        {
            _context = context;
            _mapper = mapper;
            _encrypt = encrypt;
        }

        /// <summary>
        /// Este metodo trae todas las empresas SOLO si es usuario logueado es SuperAdmin, de lo contrario, devuelve la empresa a la que pertenece.
        /// </summary>
        public async Task<List<EmpresaResponseDTO>> GetAllEmpresas(Usuario usuario)
        {
            IQueryable<Empresa> query = _context.Empresas;

            if (usuario.IdRolNavigation.Nombre != "SuperAdmin")
            {
                query = query.Where(e => e.IdEmpresa == usuario.IdEmpresa);
            }

            var listEmpresas = await query.ToListAsync();
            return listEmpresas.Select(x => _mapper.Map<EmpresaResponseDTO>(x)).ToList();
        }


        public async Task<EmpresaResponseDTO?> GetEmpresaById(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            return empresa == null ? null : _mapper.Map<EmpresaResponseDTO>(empresa);
        }

        /// <summary>
        /// Ete metodo crea empresa, validando existencia, asignadno estado de pago pendiente y creando usuario administrador de empresa asignado a la misma.
        /// </summary>
        public async Task<EmpresaResponseDTO> CreateEmpresa(EmpresaRequestDTO empresaDto)
        {
            if (empresaDto == null)
                throw new ArgumentNullException(nameof(empresaDto), "Los datos de la empresa son requeridos");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Validar que el nombre de la empresa no exista
                var empresaExistente = await _context.Empresas
                    .FirstOrDefaultAsync(e => e.Nombre == empresaDto.Nombre);

                if (empresaExistente != null)
                    throw new InvalidOperationException("Ya existe una empresa con ese nombre");

                // 2. Crear la empresa en estado "Pendiente de pago"
                var empresa = _mapper.Map<Empresa>(empresaDto);
                empresa.Estado = "Pendiente de pago";
                empresa.FechaRegistro = DateTime.UtcNow;

                _context.Empresas.Add(empresa);
                await _context.SaveChangesAsync();

                // 3. Validar que la contraseña no sea nula o vacia
                if (string.IsNullOrWhiteSpace(empresaDto.PasswordUsuario))
                    throw new InvalidOperationException("La contraseña del usuario administrador es requerida");

                // 4. Crear usuario administrador automaticamente
                var adminUsuario = new Usuario
                {
                    IdEmpresa = empresa.IdEmpresa,
                    IdRol = 2, // Rol Administrador de Empresa
                    Nombre = $"{empresaDto.Nombre} Admin",
                    Correo = empresaDto.CorreoUsuario,
                    Password = _encrypt.HashPassword(empresaDto.PasswordUsuario),
                    Estado = "Activo",
                    FechaRegistro = DateTime.UtcNow
                };

                _context.Usuarios.Add(adminUsuario);
                await _context.SaveChangesAsync();

                // 5. Confirmar la transaccion
                await transaction.CommitAsync();

                return _mapper.Map<EmpresaResponseDTO>(empresa);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error al crear la empresa: {ex.Message}", ex);
            }
        }



        public async Task<bool> UpdateEmpresa(int id, EmpresaRequestDTO empresaDto)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null) return false;

            _mapper.Map(empresaDto, empresa);
            _context.Empresas.Update(empresa);
            await _context.SaveChangesAsync();
            return true;
        }


        /// <summary>
        /// Este metodo elimina una empresa SOLO si no tiene usuarios o suscripciones activas.
        /// </summary>
        public async Task<bool> DeleteEmpresa(int id)
        {
            var empresa = await _context.Empresas
                .Include(e => e.Usuarios)
                .Include(e => e.Suscripcions)
                .FirstOrDefaultAsync(e => e.IdEmpresa == id);

            if (empresa == null) return false;

            // Validar si la empresa tiene usuarios o suscripciones activas
            var tieneUsuarios = await _context.Usuarios.AnyAsync(u => u.IdEmpresa == id);
            var tieneSuscripcionesActivas = await _context.Suscripcions.AnyAsync(s => s.IdEmpresa == id && s.Estado == "Activa");

            if (tieneUsuarios || tieneSuscripcionesActivas)
                throw new InvalidOperationException("No se puede eliminar una empresa con usuarios o suscripciones activas");


            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
