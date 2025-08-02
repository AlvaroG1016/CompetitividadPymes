using CompetitividadPymes.Models.DTO.Request;

namespace CompetitividadPymes.Services.Interfaces
{
    public interface IEncuestaService
    {
        Task CreateEncuesta(EncuestaRequestDTO req);
    }
}
