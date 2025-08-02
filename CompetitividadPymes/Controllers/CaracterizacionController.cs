using CompetitividadPymes.Models.CustomResponses;
using CompetitividadPymes.Services.Interfaces;
using CompetitividadPymes.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompetitividadPymes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class CaracterizacionController : ControllerBase
    {
        private readonly ICaracterizacionVerificationService _caracterizacionVerificationService;
        private readonly GeneralUtilities _utils;

        public CaracterizacionController(
            ICaracterizacionVerificationService caracterizacionVerificationService,
            GeneralUtilities utils)
        {
            _caracterizacionVerificationService = caracterizacionVerificationService;
            _utils = utils;
        }

        /// <summary>
        /// Verifica si la caracterización de empresa está completa para la empresa del usuario autenticado
        /// </summary>
        /// <returns>Estado de completitud de la caracterización de empresa</returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse>> VerificarCaracterizacionEmpresa()
        {
            try
            {
                // Obtener ID de empresa desde el token JWT
                int idEmpresa = _utils.ObtenerIdEmpresaToken();

                var resultado = await _caracterizacionVerificationService.VerificarCaracterizacionEmpresa(idEmpresa);

                return Ok(ResponseBuilder.BuildSuccessResponse(
                    resultado,
                    resultado.Mensaje
                ));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ResponseBuilder.BuildErrorResponse("Token inválido o expirado"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseBuilder.BuildErrorResponse(
                    $"Error interno del servidor: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Verifica si la caracterización de usuario está completa para la empresa del usuario autenticado
        /// </summary>
        /// <returns>Estado de completitud de la caracterización de usuario</returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse>> VerificarCaracterizacionUsuario()
        {
            try
            {
                // Obtener ID de empresa desde el token JWT
                int idEmpresa = _utils.ObtenerIdEmpresaToken();

                var resultado = await _caracterizacionVerificationService.VerificarCaracterizacionUsuario(idEmpresa);

                return Ok(ResponseBuilder.BuildSuccessResponse(
                    resultado,
                    resultado.Mensaje
                ));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ResponseBuilder.BuildErrorResponse("Token inválido o expirado"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseBuilder.BuildErrorResponse(
                    $"Error interno del servidor: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Verifica el estado completo de todas las caracterizaciones y determina si se pueden iniciar los factores
        /// </summary>
        /// <returns>Estado completo de caracterizaciones y disponibilidad de factores</returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse>> VerificarEstadoCompleto()
        {
            try
            {
                // Obtener ID de empresa desde el token JWT
                int idEmpresa = _utils.ObtenerIdEmpresaToken();

                var resultado = await _caracterizacionVerificationService.VerificarTodasLasCaracterizaciones(idEmpresa);

                return Ok(ResponseBuilder.BuildSuccessResponse(
                    resultado,
                    resultado.Mensaje
                ));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ResponseBuilder.BuildErrorResponse("Token inválido o expirado"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseBuilder.BuildErrorResponse(
                    $"Error interno del servidor: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Método alternativo que permite verificar por ID de empresa específico (para administradores)
        /// </summary>
        /// <param name="idEmpresa">ID de la empresa a verificar</param>
        /// <returns>Estado completo de caracterizaciones para la empresa especificada</returns>
        [HttpGet("{idEmpresa}")]
        [Authorize(Roles = "Administrador")] // Solo administradores pueden consultar otras empresas
        public async Task<ActionResult<GeneralResponse>> VerificarEstadoCompletoPorEmpresa(int idEmpresa)
        {
            try
            {
                if (idEmpresa <= 0)
                {
                    return BadRequest(ResponseBuilder.BuildErrorResponse("ID de empresa debe ser mayor a 0"));
                }

                var resultado = await _caracterizacionVerificationService.VerificarTodasLasCaracterizaciones(idEmpresa);

                return Ok(ResponseBuilder.BuildSuccessResponse(
                    resultado,
                    $"Estado de caracterizaciones para empresa {idEmpresa}: {resultado.Mensaje}"
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseBuilder.BuildErrorResponse(
                    $"Error interno del servidor: {ex.Message}"
                ));
            }
        }

        /// <summary>
        /// Endpoint simplificado que retorna solo true/false si las caracterizaciones están completas
        /// Útil para validaciones rápidas desde el frontend
        /// </summary>
        /// <returns>Boolean indicando si ambas caracterizaciones están completas</returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse>> SonCaracterizacionesCompletas()
        {
            try
            {
                int idEmpresa = _utils.ObtenerIdEmpresaToken();

                var resultado = await _caracterizacionVerificationService.VerificarTodasLasCaracterizaciones(idEmpresa);

                // Respuesta simplificada solo con boolean
                var respuestaSimplificada = new
                {
                    completed = resultado.TodasCompletas,
                    canStartFactors = resultado.PuedenEmpezarFactores,
                    message = resultado.Mensaje
                };

                return Ok(ResponseBuilder.BuildSuccessResponse(
                    respuestaSimplificada,
                    resultado.TodasCompletas ? "Caracterizaciones completas" : "Caracterizaciones pendientes"
                ));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ResponseBuilder.BuildErrorResponse("Token inválido o expirado"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseBuilder.BuildErrorResponse(
                    $"Error interno del servidor: {ex.Message}"
                ));
            }
        }
    }
}