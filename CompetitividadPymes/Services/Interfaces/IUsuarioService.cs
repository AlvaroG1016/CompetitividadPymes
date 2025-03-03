using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;

namespace CompetitividadPymes.Services.Interfaces
{
    public interface IUsuarioService 
    {
        Task<List<UsuarioResponseDTO>> GetAllUsuarios(Usuario usuario);
        Task<UsuarioResponseDTO?> GetUsuarioById(int id, Usuario usuario);
        Task<UsuarioResponseDTO> CreateUsuario(UsuarioRequestDTO usuarioDto, int idUsuarioCreador);
        Task<bool> UpdateUsuario(int id, UsuarioRequestDTO usuarioDto, Usuario usuarioEditor);
        Task<bool> DeleteUsuario(int id, Usuario usuarioSolicitante);
    }
}
