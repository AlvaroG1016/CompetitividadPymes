using CompetitividadPymes.Models.Domain;

namespace CompetitividadPymes.Services.Implementations
{
    public class FactorService
    {
        private readonly PymesCompetitividadContext _context;

        public FactorService(PymesCompetitividadContext context)
        {
            _context = context;
        }

    }
}
