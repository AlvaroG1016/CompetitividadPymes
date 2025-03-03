using System.ComponentModel.DataAnnotations;

namespace CompetitividadPymes.Models.DTO.Request
{
    public class EmpresaRequestDTO
    {
        [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El sector de la empresa es obligatorio.")]
        public string Sector { get; set; } = null!;
        [Required(ErrorMessage = "El correo del usuario administrador es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string CorreoUsuario { get; set; } = null!;
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string PasswordUsuario { get; set; } = null!;
        [Required(ErrorMessage = "La clasificacion de la empresa es obligatoria.")]
        public string Clasificacion { get; set; } = null!;
        [Required(ErrorMessage = "El estado de la empresa es obligatorio.")]
        public string Estado { get; set; } = null!;
    }
}
