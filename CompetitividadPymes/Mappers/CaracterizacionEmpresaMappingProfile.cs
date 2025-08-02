using AutoMapper;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;

namespace CompetitividadPymes.Mappers
{
    public class CaracterizacionEmpresaMappingProfile : Profile
    {
        public CaracterizacionEmpresaMappingProfile()
        {
            CreateMap<CaracterizacionEmpresaRequestDTO, CaracterizacionEmpresa>().ReverseMap();
            CreateMap<CaracterizacionEmpresaResponseDTO, CaracterizacionEmpresa>().ReverseMap();

        }
    }
}
