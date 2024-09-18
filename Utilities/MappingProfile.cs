using AutoMapper;
using Models.DTOs;
using Models.Entities;

namespace Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Chair,ChairDto>()
                .ForMember(c => c.Name,m => m.MapFrom(m => m.Name));
            CreateMap<Service, ServiceDto>()
                .ForMember(c => c.Name, m => m.MapFrom(m => m.Name));
            CreateMap<Orden, OrdenDto>()
                .ForMember(c => c.Numero, m => m.MapFrom(m => m.Numero));

        }
    }
}
