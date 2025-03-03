using CompetitividadPymes.Models.CustomResponses;
using CompetitividadPymes.Models.DTO;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Services.Implementations;
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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> CrearUsuario(UsuarioRequestDTO usuario, int idUsuarioCreador)
        {
            try
            {
                var response = await _usuarioService.CreateUsuario(usuario, idUsuarioCreador);
                return Ok(ResponseBuilder.BuildSuccessResponse(response, ""));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.BuildErrorResponse(ex.Message));

            }
        }
    }
}
