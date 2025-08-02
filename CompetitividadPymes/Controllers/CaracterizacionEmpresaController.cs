using Azure;
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
    public class CaracterizacionEmpresaController : ControllerBase
    {
        private readonly ICaracterizacionEmpresaService _caracterizacionEmpresaService;
        public CaracterizacionEmpresaController(ICaracterizacionEmpresaService caracterizacionEmpresaService)
        {
            _caracterizacionEmpresaService = caracterizacionEmpresaService;
        }

        [HttpPost]
        public async Task<ActionResult> CrearCaracterizacionEmpresa([FromBody] CaracterizacionEmpresaRequestDTO caracterizacionEmpresa)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos de entrada no válidos " + ModelState);

            try
            {
                await _caracterizacionEmpresaService.CreateCaracterizacionEmpresa(caracterizacionEmpresa);
                return Ok(ResponseBuilder.BuildSuccessResponse(""));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ResponseBuilder.BuildErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseBuilder.BuildErrorResponse("Ocurrió un error interno"));
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetCaracterizacionEmpresaByIdEmpresa()
        {
            
            try
            {
                var response = await _caracterizacionEmpresaService.GetCaracterizacionEmpresaByIdEmpresa();
                return Ok(ResponseBuilder.BuildSuccessResponse(response,""));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ResponseBuilder.BuildErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseBuilder.BuildErrorResponse("Ocurrió un error interno"));
            }
        }
    }
}
