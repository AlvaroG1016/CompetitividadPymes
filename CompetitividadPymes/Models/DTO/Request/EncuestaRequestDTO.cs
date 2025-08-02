namespace CompetitividadPymes.Models.DTO.Request
{
    public class EncuestaRequestDTO
    {
        public int id_empresa { get; set; }
        public DateTime fecha_aplicacion { get; set; }
        public string estado { get; set; } = "En Proceso";
        public int id_carUsuario { get; set; }
        public int id_carEmpresa { get; set; }

    }
}
