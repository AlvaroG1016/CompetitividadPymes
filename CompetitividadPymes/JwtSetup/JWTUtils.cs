using CompetitividadPymes.JwtSetup;
using CompetitividadPymes.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CompetitividadPymes.JwtSetup
{
    public class JWTUtils
    {

        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;

        public JWTUtils(IConfiguration configuration, IOptions<JwtSettings> jwtSettings)
        {
            _configuration = configuration;
            _jwtSettings = jwtSettings.Value;
        }




        public string GenerarJWT(Usuario modelo, List<string> permisos)
        {
            var userClaims = new List<Claim>
        {
        new Claim(ClaimTypes.NameIdentifier, modelo.IdUsuario.ToString()),
        new Claim(ClaimTypes.Email, modelo.Correo!),
        new Claim("Rol", modelo.IdRolNavigation.Nombre) // Agregar el rol al token
        };

            //Agregar cada permiso como un claim
            foreach (var permiso in permisos)
            {
                userClaims.Add(new Claim("Permiso", permiso));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); 

            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }


    }
}
