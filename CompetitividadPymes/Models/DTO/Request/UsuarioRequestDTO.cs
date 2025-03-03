namespace CompetitividadPymes.Models.DTO.Request
{
    public class UsuarioRequestDTO
    {


        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Password { get; set; } = null!;
        public int IdRol { get; set; }

    }
}
