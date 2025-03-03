using AutoMapper;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;

namespace CompetitividadPymes.Mappers
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioRequestDTO>()
                .ReverseMap();
            CreateMap<Usuario, UsuarioResponseDTO>()
                .ReverseMap();
        }
    }
}
