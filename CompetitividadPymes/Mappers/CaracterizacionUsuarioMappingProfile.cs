using AutoMapper;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;

namespace CompetitividadPymes.Mappers
{
    public class CaracterizacionUsuarioMappingProfile : Profile
    {
        public CaracterizacionUsuarioMappingProfile()
        {
            CreateMap<CaracterizacionUsuario, CaracterizacionUsuarioRequestDTO>().ReverseMap();
            CreateMap<CaracterizacionUsuario, CaracterizacionUsuarioResponseDTO>().ReverseMap();

        }
    }
}
