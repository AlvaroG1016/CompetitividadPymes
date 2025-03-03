using CompetitividadPymes.Models.Domain;

namespace CompetitividadPymes.Models.DTO
{
    public class LoginDTO
    {
        public string Correo { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
