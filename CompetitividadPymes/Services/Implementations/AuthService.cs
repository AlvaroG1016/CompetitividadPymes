using CompetitividadPymes.JwtSetup;
using CompetitividadPymes.Models.CustomResponses;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO;
using CompetitividadPymes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompetitividadPymes.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly JWTUtils _jwtUtils;
        private readonly PymesCompetitividadContext _context;
        private readonly EncryptUtilities _encryptUtilities;


        public AuthService(JWTUtils jwtUtils, PymesCompetitividadContext context, EncryptUtilities encryptUtilities)
        {
            _jwtUtils = jwtUtils;
            _context = context;
            _encryptUtilities = encryptUtilities;
        }


        public async Task<AuthResponse> Login(LoginDTO loginDto)
        {
            var usuarioEncontrado = await _context.Usuarios
                .Include(u => u.IdEmpresaNavigation)
                .Include(u => u.IdRolNavigation)
                .ThenInclude(r => r.IdPermisos) // Cargar los permisos del rol
                .FirstOrDefaultAsync(u => u.Correo == loginDto.Correo);

            if (usuarioEncontrado == null || !_encryptUtilities.VerifyPassword(loginDto.Password, usuarioEncontrado.Password))
            {
                throw new InvalidOperationException("Correo o contraseña incorrectos");
            }

            loginDto.Password = string.Empty;

            var permisos = usuarioEncontrado.IdRolNavigation.IdPermisos.Select(p => p.Nombre).ToList();

            string token = _jwtUtils.GenerarJWT(usuarioEncontrado, permisos);

            return new AuthResponse
            {
                Correo = usuarioEncontrado.Correo,
                Token = token,
                OpcionesMenu = "GenerarOpcionesMenu(permisos) TODO",
                NombreUsuario = usuarioEncontrado.Nombre
            };
        }

    }
}
