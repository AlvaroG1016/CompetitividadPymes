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

    }
}
