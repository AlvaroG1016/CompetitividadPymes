using CompetitividadPymes.Models.DTO.Request;

namespace CompetitividadPymes.Services.Interfaces
{
    public interface IRespuestaService
    {
        Task<string> ActualizarRespuestas(List<RespuestaRequestDTO> respuestas);
    }
}
