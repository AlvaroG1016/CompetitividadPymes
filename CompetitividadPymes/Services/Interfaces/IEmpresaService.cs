using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;

namespace CompetitividadPymes.Services.Interfaces
{
    public interface IEmpresaService
    {
        Task<List<EmpresaResponseDTO>> GetAllEmpresas(Usuario usuario);
        Task<EmpresaResponseDTO?> GetEmpresaById(int id);
        Task<EmpresaResponseDTO> CreateEmpresa(EmpresaRequestDTO empresaDto);
        Task<bool> UpdateEmpresa(int id, EmpresaRequestDTO empresaDto);
        Task<bool> DeleteEmpresa(int id);
    }
}
