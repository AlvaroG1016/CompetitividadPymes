using CompetitividadPymes.Models.DTO.Response;

namespace CompetitividadPymes.Services.Interfaces
{
    public interface ICaracterizacionVerificationService
    {
        Task<CaracterizacionStatusResponseDTO> VerificarCaracterizacionEmpresa(int idEmpresa);
        Task<CaracterizacionStatusResponseDTO> VerificarCaracterizacionUsuario(int idEmpresa);
        Task<CaracterizacionesCompleteStatusResponseDTO> VerificarTodasLasCaracterizaciones(int idEmpresa);
    }
}
