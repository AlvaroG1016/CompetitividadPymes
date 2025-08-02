using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO;
using System.Security.Claims;

namespace CompetitividadPymes.Utilities
{
    public class GeneralUtilities
    {
        private readonly PymesCompetitividadContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GeneralUtilities(PymesCompetitividadContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor; 
        }

        public async Task<bool> esAdmin(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);

            if (usuario == null)
            {
                
                return false;
            }

            
            return usuario.IdRol == 1;
        }


        public async Task<bool> esEmpleado(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);

            if (usuario == null)
            {

                return false;
            }


            return usuario.IdRol == 3;
        }

        public int ObtenerIdUsuarioToken()
        {
            var usuarioId =  int.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (usuarioId == null)
            {
                throw new InvalidOperationException("No se pudo obtener el ID del usuario desde el token.");
            }

            return usuarioId;
        }
        public int ObtenerIdEmpresaToken()
        {
            var claimValue = _httpContextAccessor.HttpContext?.User.FindFirst("IdEmpresa")?.Value;

            if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out var idEmpresa))
            {
                throw new InvalidOperationException("No se pudo obtener un ID de empresa válido desde el token.");
            }

            return idEmpresa;
        }

    }
}
