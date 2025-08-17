using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompetitividadPymes.Services.Interfaces;
using CompetitividadPymes.Models.CustomResponses;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Services.Implementations;
using CompetitividadPymes.Utilities;

namespace CompetitividadPymes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class RespuestaController : ControllerBase
    {
        private readonly IRespuestaService _respuestaService;

        public RespuestaController(IRespuestaService respuestaService)
        {
            _respuestaService = respuestaService;
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> ActualizarRespuestas([FromBody] List<RespuestaRequestDTO> respuestas)
        {
            try
            {
                var response = await _respuestaService.ActualizarRespuestas(respuestas);
                return Ok(ResponseBuilder.BuildSuccessResponse(response, ""));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.BuildErrorResponse(ex.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse>> ObtenerResultadosEncuesta(int Id)
        {
            try
            {
                var response = await _respuestaService.ObtenerResultadosEncuesta(Id);
                return Ok(ResponseBuilder.BuildSuccessResponse(response, ""));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.BuildErrorResponse(ex.Message));
            }
        }

        // NUEVO MÉTODO para obtener respuestas por factor
        [HttpGet]
        public async Task<ActionResult<GeneralResponse>> ObtenerRespuestasPorFactor(int encuestaId, int factorId)
        {
            try
            {
                var response = await _respuestaService.ObtenerRespuestasPorFactor(encuestaId, factorId);
                return Ok(ResponseBuilder.BuildSuccessResponse(response, ""));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.BuildErrorResponse(ex.Message));
            }
        }
        [HttpGet]
        public async Task<ActionResult<GeneralResponse>> ObtenerComparativoCompetitividad(int idEncuesta)
        {
            try
            {
                var response = await _respuestaService.ObtenerComparativoCompetitividad(idEncuesta);
                return Ok(ResponseBuilder.BuildSuccessResponse(response, ""));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.BuildErrorResponse(ex.Message));
            }
        }
    }
}