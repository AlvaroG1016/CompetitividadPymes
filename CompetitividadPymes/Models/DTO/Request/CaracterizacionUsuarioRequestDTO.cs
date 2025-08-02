using CompetitividadPymes.Models.Domain;

namespace CompetitividadPymes.Models.DTO.Request
{
    public class CaracterizacionUsuarioRequestDTO
    {
        public string Nombre { get; set; } = null!;

        public int Edad { get; set; }

        public string Genero { get; set; } = null!;

        public string Cargo { get; set; } = null!;

        public int Antiguedad { get; set; }

        public string EmailInstitucional { get; set; } = null!;

        public string EmailPersonal { get; set; } = null!;

    }
}
