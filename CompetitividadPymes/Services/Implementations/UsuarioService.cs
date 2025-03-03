using AutoMapper;
using CompetitividadPymes.JwtSetup;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;
using CompetitividadPymes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CompetitividadPymes.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly PymesCompetitividadContext _context;
        private readonly IMapper _mapper;
        private readonly EncryptUtilities _encryptUtilities;


        public UsuarioService(PymesCompetitividadContext context, IMapper mapper, EncryptUtilities encryptUtilities)
        {
            _context = context;
            _mapper = mapper;
            _encryptUtilities = encryptUtilities;
        }


        /// <summary>
        /// Ete metodo obtiene todos los usuarios (Solo SuperAdmin puede ver todos)
        /// </summary>
        public async Task<List<UsuarioResponseDTO>> GetAllUsuarios(Usuario usuario)
        {
            IQueryable<Usuario> query = _context.Usuarios.Include(u => u.IdRolNavigation);

            if (usuario.IdRolNavigation.Nombre != "SuperAdmin")
            {
                query = query.Where(u => u.IdEmpresa == usuario.IdEmpresa);
            }

            var listUsuarios = await query.ToListAsync();
            return listUsuarios.Select(x => _mapper.Map<UsuarioResponseDTO>(x)).ToList();
        }

        /// <summary>
        /// Ete metodo obtiene un usuario por ID (Debe ser de la misma empresa o ser SuperAdmin)
        /// </summary>

        public async Task<UsuarioResponseDTO?> GetUsuarioById(int id, Usuario usuario)
        {
            var user = await _context.Usuarios.Include(u => u.IdRolNavigation).FirstOrDefaultAsync(u => u.IdUsuario == id);
            if (user == null || (usuario.IdRolNavigation.Nombre != "SuperAdmin" && user.IdEmpresa != usuario.IdEmpresa))
                return null;

            return _mapper.Map<UsuarioResponseDTO>(user);
        }

        /// <summary>
        /// Ete metodo crea un nuevo usuario (Solo Admin de Empresa puede hacerlo)
        /// </summary>

        public async Task<UsuarioResponseDTO> CreateUsuario(UsuarioRequestDTO usuarioDto, int idUsuarioCreador)
        {
            var usuarioCreador = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuarioCreador);

            if (usuarioCreador == null)
                throw new UnauthorizedAccessException("El usuario creador no existe.");

            if (usuarioCreador.IdRol != 2 && usuarioCreador.IdRol != 2)
                throw new UnauthorizedAccessException("No tienes permisos para crear usuarios.");

            if (await _context.Usuarios.AnyAsync(u => u.Correo == usuarioDto.Correo))
                throw new InvalidOperationException("El correo ya está registrado.");

            var nuevoUsuario = _mapper.Map<Usuario>(usuarioDto);
            nuevoUsuario.IdEmpresa = usuarioCreador.IdEmpresa;
            nuevoUsuario.Password = _encryptUtilities.HashPassword(usuarioDto.Password);
            nuevoUsuario.Estado = "Activo";
            nuevoUsuario.FechaRegistro = DateTime.UtcNow;

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return _mapper.Map<UsuarioResponseDTO>(nuevoUsuario);
        }


        /// <summary>
        /// Ete metodo actualiza un usuario (Solo Admin de Empresa puede actualizar usuarios de su empresa)
        /// </summary>

        public async Task<bool> UpdateUsuario(int id, UsuarioRequestDTO usuarioDto, Usuario usuarioEditor)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null || (usuarioEditor.IdRolNavigation.Nombre != "SuperAdmin" && usuario.IdEmpresa != usuarioEditor.IdEmpresa))
                return false;

            if (usuarioEditor.IdRolNavigation.Nombre != "Administrador de Empresa" && usuarioEditor.IdRolNavigation.Nombre != "SuperAdmin")
                throw new UnauthorizedAccessException("No tienes permisos para modificar este usuario.");

            _mapper.Map(usuarioDto, usuario);
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Ete metodo elimina un usuario (No puede eliminar el último admin de una empresa)
        /// </summary>

        public async Task<bool> DeleteUsuario(int id, Usuario usuarioSolicitante)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.IdUsuario == id)
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync();

            if (usuario == null || (usuarioSolicitante.IdRolNavigation.Nombre != "SuperAdmin" && usuario.IdEmpresa != usuarioSolicitante.IdEmpresa))
                return false;

            // Verificar si es el último administrador de la empresa
            bool esUltimoAdmin = _context.Usuarios.Count(u => u.IdEmpresa == usuario.IdEmpresa && u.IdRol == 1) <= 1;
            if (esUltimoAdmin)
                throw new InvalidOperationException("No se puede eliminar el último administrador de la empresa.");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

       
       
    }
}
