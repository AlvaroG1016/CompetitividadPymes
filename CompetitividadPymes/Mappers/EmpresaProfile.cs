using AutoMapper;
using CompetitividadPymes.Models.Domain;
using CompetitividadPymes.Models.DTO.Request;
using CompetitividadPymes.Models.DTO.Response;

namespace CompetitividadPymes.Mappers
{
    public class EmpresaProfile : Profile
    {
        public EmpresaProfile()
        {
            CreateMap<Empresa, EmpresaResponseDTO>();
            CreateMap<EmpresaRequestDTO, Empresa>();

        }
    }
}
