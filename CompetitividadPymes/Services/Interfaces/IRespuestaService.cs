using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;

namespace CompetitividadPymes.Services.Interfaces
{
    public interface IRespuestaService
    {
        Task<ResultadoEncuestaDTO> ActualizarRespuestas(List<RespuestaRequestDTO> respuestas);
        Task<ResultadoEncuestaDTO> ObtenerResultadosEncuesta(int idEncuesta);
        Task<List<RespuestaPorFactorDTO>> ObtenerRespuestasPorFactor(int encuestaId, int factorId);

    }
}
