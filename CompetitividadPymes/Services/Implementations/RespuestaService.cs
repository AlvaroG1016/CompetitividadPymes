using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;
using CompetitividadPymes.Services.Interfaces;

namespace CompetitividadPymes.Services.Implementations
{
    public class RespuestaService : IRespuestaService
    {
        private readonly PymesCompetitividadContext _context;

        public RespuestaService(PymesCompetitividadContext context)
        {
            _context = context;
        }

        public async Task<string> ActualizarRespuestas(List<RespuestaRequestDTO> respuestas)
        {
            foreach (var r in respuestas)
            {
                var nuevaRespuesta = new Respuestum
                {
                    IdEncuesta = r.IdEncuesta,
                    IdPregunta = r.IdPregunta,
                    ValorRespuesta = r.ValorRespuesta
                };

                _context.Respuesta.Add(nuevaRespuesta);
            }

            await _context.SaveChangesAsync();

            return "ok";
        }

    }
}
