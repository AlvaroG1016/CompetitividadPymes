using AutoMapper;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;

namespace CompetitividadPymes.Mappers
{
    public class SectorMappingProfile : Profile
    {
        public SectorMappingProfile()
        {
            CreateMap<Sector, SectoresResponseDTO>().ReverseMap();
            CreateMap<SectorSubsector, SubSectorResponseDTO>().ReverseMap();
            CreateMap<SubSector, SubSectorResponseDTO>().ReverseMap();
        }
    }
}
