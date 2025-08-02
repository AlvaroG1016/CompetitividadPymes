using CompetitividadPymes.Models.CustomResponses;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Services.Interfaces;
using CompetitividadPymes.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompetitividadPymes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CaracterizacionUsuarioController : ControllerBase
    {
        private readonly ICaracterizacionUsuarioService _caracterizacionUsuarioService;

        public CaracterizacionUsuarioController(ICaracterizacionUsuarioService caracterizacionUsuarioService)
        {
            _caracterizacionUsuarioService = caracterizacionUsuarioService;
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> CrearCaracterizacionUsuario([FromBody] CaracterizacionUsuarioRequestDTO caracterizacionUsuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseBuilder.BuildErrorResponse("Datos de entrada no válidos " + ModelState));

            try
            {
                await _caracterizacionUsuarioService.CreateCaracterizacionUsuario(caracterizacionUsuario);
                return Ok(ResponseBuilder.BuildSuccessResponse( "Creado Exitosamente"));
            }
            catch (InvalidOperationException ex)
            {
                // _logger.LogWarning(ex, "Error de validación en CrearCaracterizacionUsuario");
                return Conflict(ResponseBuilder.BuildErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error inesperado en CrearCaracterizacionUsuario");
                return StatusCode(500, ResponseBuilder.BuildErrorResponse("Ocurrió un error interno"));
            }
        }

    }
}
