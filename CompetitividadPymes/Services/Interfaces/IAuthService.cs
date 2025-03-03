using CompetitividadPymes.Models.CustomResponses;
using CompetitividadPymes.Models.DTO;

namespace CompetitividadPymes.Services.Interfaces
{
    public interface IAuthService
    {

        Task<AuthResponse> Login(LoginDTO loginDTO);

    }
}
