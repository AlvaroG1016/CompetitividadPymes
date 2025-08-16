using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;

namespace CompetitividadPymes.Services.Interfaces
{
    public interface ICaracterizacionUsuarioService
    {
        Task<CaracterizacionUsuarioResponseDTO> CreateCaracterizacionUsuario(CaracterizacionUsuarioRequestDTO req);
        Task<CaracterizacionUsuarioResponseDTO> GetIdEncuenstaByEmpresa();
    }
}
