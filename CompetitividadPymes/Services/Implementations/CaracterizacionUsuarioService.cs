using AutoMapper;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;
using CompetitividadPymes.Services.Interfaces;
using CompetitividadPymes.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CompetitividadPymes.Services.Implementations
{
    public class CaracterizacionUsuarioService : ICaracterizacionUsuarioService
    {
        private readonly PymesCompetitividadContext _context;
        private readonly IMapper _mapper;
        private readonly IEncuestaService _encuestaService;
        private readonly GeneralUtilities _utils;
        
        public CaracterizacionUsuarioService(PymesCompetitividadContext context, IMapper mapper, IEncuestaService encuestaService, GeneralUtilities utils)
        {
            _context = context;
            _mapper = mapper;
            _encuestaService = encuestaService;
            _utils = utils;
        }

        public async Task CreateCaracterizacionUsuario(CaracterizacionUsuarioRequestDTO req)
        {
            if (req == null)
            {
                throw new ArgumentNullException(nameof(req), "Los datos son requeridos");
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                int idEmpresa = _utils.ObtenerIdEmpresaToken();
                var caracterizacion = _mapper.Map<CaracterizacionUsuario>(req);
                caracterizacion.IdEmpresa = idEmpresa;
                _context.CaracterizacionUsuarios.Add(caracterizacion);

                var caracterizacionEmpresa = await _context.CaracterizacionEmpresas
                    .FirstOrDefaultAsync(x => x.IdEmpresa == idEmpresa);

                if (caracterizacionEmpresa == null)
                {
                    throw new InvalidOperationException("No se encontró la caracterización de la empresa asociada a este usuario.");
                }

                var encuesta = new Encuestum
                {
                    IdEmpresa = idEmpresa,
                    IdCarEmpresa= caracterizacionEmpresa.Id,
                    // Establecer la relación directamente con la entidad, no con el ID
                    IdCarUsuarioNavigation = caracterizacion,
                    Estado = "En Proceso"
                };

                _context.Encuesta.Add(encuesta);

                // Un solo SaveChanges al final
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task CreateEncuesta(int idEmpresa, int idCarUsuario, int idCarEmpresa)
        {
            var encuestaRequest = new EncuestaRequestDTO
            {
                id_empresa = idEmpresa,
                id_carEmpresa = idCarEmpresa,
                id_carUsuario = idCarUsuario
            };

            // IMPORTANTE: Usar await aquí también
            await _encuestaService.CreateEncuesta(encuestaRequest);
        }
    }
}
