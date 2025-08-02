using AutoMapper;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;

namespace CompetitividadPymes.Mappers
{
    public class EncuestaMappingProfile : Profile
    {
        public EncuestaMappingProfile()
        {
            CreateMap<EncuestaRequestDTO, Encuestum>().ReverseMap();
        }
    }
}
