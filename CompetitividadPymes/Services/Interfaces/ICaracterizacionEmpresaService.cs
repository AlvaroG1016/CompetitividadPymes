using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;

namespace CompetitividadPymes.Services.Interfaces
{
    public interface ICaracterizacionEmpresaService
    {
        Task CreateCaracterizacionEmpresa(CaracterizacionEmpresaRequestDTO req);
        Task<CaracterizacionEmpresaResponseDTO> GetCaracterizacionEmpresaByIdEmpresa();
        Task<List<SectoresResponseDTO>> GetAllSectores();
        Task<List<SubSectorResponseDTO>> GetSubsectoresBySectorId(int idSector);
    }
}
