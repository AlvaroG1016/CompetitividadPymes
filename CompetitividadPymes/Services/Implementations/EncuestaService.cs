using AutoMapper;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Services.Interfaces;

namespace CompetitividadPymes.Services.Implementations
{
    public class EncuestaService :IEncuestaService
    {
        private readonly PymesCompetitividadContext _context;
        private readonly IMapper _mapper;

        public EncuestaService(PymesCompetitividadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateEncuesta(EncuestaRequestDTO req)
        {
            if (req == null)
            {
                throw new ArgumentNullException(nameof(req), "Los datos son requeridos");
            }
            try
            {

            var encuesta = _mapper.Map<Encuestum>(req);
            _context.Encuesta.Add(encuesta);
            await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
