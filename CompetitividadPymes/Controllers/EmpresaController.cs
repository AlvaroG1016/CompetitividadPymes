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
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> CrearEmpresa(EmpresaRequestDTO empresa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseBuilder.BuildErrorResponse("Datos de entrada no válidos "+ ModelState));
            try
            {
                var response = await _empresaService.CreateEmpresa(empresa);
                return Ok(ResponseBuilder.BuildSuccessResponse(response, ""));
            }
            catch (InvalidOperationException ex)
            {
               // _logger.LogWarning(ex, "Error de validación en CrearEmpresa");
                return Conflict(ResponseBuilder.BuildErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, "Error inesperado en CrearEmpresa");
                return StatusCode(500, ResponseBuilder.BuildErrorResponse("Ocurrió un error interno"));
            }
        }
    }
}
