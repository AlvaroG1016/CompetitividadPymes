using AutoMapper;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Response;
using CompetitividadPymes.Services.Interfaces;
using CompetitividadPymes.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CompetitividadPymes.Services.Implementations
{
    public class CaracterizacionVerificationService : ICaracterizacionVerificationService
    {
        private readonly PymesCompetitividadContext _context;
        private readonly IMapper _mapper;
        private readonly GeneralUtilities _utils;

        public CaracterizacionVerificationService(
            PymesCompetitividadContext context,
            IMapper mapper,
            GeneralUtilities utils)
        {
            _context = context;
            _mapper = mapper;
            _utils = utils;
        }

        public async Task<CaracterizacionStatusResponseDTO> VerificarCaracterizacionEmpresa(int idEmpresa)
        {
            try
            {
                var caracterizacionEmpresa = await _context.CaracterizacionEmpresas
                    .FirstOrDefaultAsync(x => x.IdEmpresa == idEmpresa);

                if (caracterizacionEmpresa == null)
                {
                    return new CaracterizacionStatusResponseDTO
                    {
                        Completed = false,
                        Percentage = 0,
                        Estado = "No Completada",
                        Mensaje = "No se ha completado la caracterización de la empresa"
                    };
                }

                // Si existe el registro, está completa (se llena todo de una vez)
                return new CaracterizacionStatusResponseDTO
                {
                    Completed = true,
                    Percentage = 100,

                    Estado = "Completada",
                    Mensaje = "Caracterización de empresa completada exitosamente"
                };
            }
            catch (Exception ex)
            {
                return new CaracterizacionStatusResponseDTO
                {
                    Completed = false,
                    Percentage = 0,
                    Estado = "Error",
                    Mensaje = $"Error al verificar caracterización de empresa: {ex.Message}"
                };
            }
        }

        public async Task<CaracterizacionStatusResponseDTO> VerificarCaracterizacionUsuario(int idEmpresa)
        {
            try
            {
                var caracterizacionUsuario = await _context.CaracterizacionUsuarios
                    .FirstOrDefaultAsync(x => x.IdEmpresa == idEmpresa);

                if (caracterizacionUsuario == null)
                {
                    return new CaracterizacionStatusResponseDTO
                    {
                        Completed = false,
                        Percentage = 0,
                        Estado = "No Completada",
                        Mensaje = "No se ha completado la caracterización de usuario"
                    };
                }

                // Si existe el registro, está completa (se llena todo de una vez)
                return new CaracterizacionStatusResponseDTO
                {
                    Completed = true,
                    Percentage = 100,
       
                    Estado = "Completada",
                    Mensaje = "Caracterización de usuario completada exitosamente"
                };
            }
            catch (Exception ex)
            {
                return new CaracterizacionStatusResponseDTO
                {
                    Completed = false,
                    Percentage = 0,
                    Estado = "Error",
                    Mensaje = $"Error al verificar caracterización de usuario: {ex.Message}"
                };
            }
        }

        public async Task<CaracterizacionesCompleteStatusResponseDTO> VerificarTodasLasCaracterizaciones(int idEmpresa)
        {
            try
            {
                var empresaStatus = await VerificarCaracterizacionEmpresa(idEmpresa);
                var usuarioStatus = await VerificarCaracterizacionUsuario(idEmpresa);

                bool todasCompletas = empresaStatus.Completed && usuarioStatus.Completed;
                bool puedenEmpezarFactores = todasCompletas;

                string mensaje;
                if (todasCompletas)
                {
                    mensaje = "Todas las caracterizaciones están completas. Los factores de competitividad están disponibles.";
                }
                else if (empresaStatus.Completed && !usuarioStatus.Completed)
                {
                    mensaje = "Caracterización de empresa completa. Complete la caracterización de usuario para acceder a los factores.";
                }
                else if (!empresaStatus.Completed)
                {
                    mensaje = "Complete primero la caracterización de empresa para continuar.";
                }
                else
                {
                    mensaje = "Faltan caracterizaciones por completar.";
                }

                return new CaracterizacionesCompleteStatusResponseDTO
                {
                    TodasCompletas = todasCompletas,
                    CaracterizacionEmpresa = empresaStatus,
                    CaracterizacionUsuario = usuarioStatus,
                    PuedenEmpezarFactores = puedenEmpezarFactores,
                    Mensaje = mensaje
                };
            }
            catch (Exception ex)
            {
                return new CaracterizacionesCompleteStatusResponseDTO
                {
                    TodasCompletas = false,
                    CaracterizacionEmpresa = new CaracterizacionStatusResponseDTO { Completed = false, Percentage = 0 },
                    CaracterizacionUsuario = new CaracterizacionStatusResponseDTO { Completed = false, Percentage = 0 },
                    PuedenEmpezarFactores = false,
                    Mensaje = $"Error al verificar caracterizaciones: {ex.Message}"
                };
            }
        }

        // ================================================
        // LÓGICA SIMPLIFICADA - NO HAY GUARDADO TEMPORAL
        // ================================================
        // Si el registro existe en la BD = 100% completo
        // Si no existe = 0% (no completado)
        // No se requieren métodos de validación de campos individuales
    }
}