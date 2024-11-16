using AutoMapper;
using Models.DTOs;
using Models.Entities;

namespace Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Chair, ChairDto>()
                .ForMember(dest => dest.Ocuped, opt => opt.MapFrom(src => src.Ocuped ? 1 : 0)); // Si es true, será 1
            CreateMap<Service, ServiceDto>()
                .ForMember(c => c.Name, m => m.MapFrom(m => m.Name));
            CreateMap<Orden, OrdenDto>()
                .ForMember(c => c.Numero, m => m.MapFrom(m => m.Numero));
            CreateMap<Marca, MarcaDto>()
                .ForMember(c => c.Name, m => m.MapFrom(m => m.Name));
            CreateMap<Category, CategoryDto>()
                .ForMember(c => c.Name, m => m.MapFrom(m => m.Name));
            CreateMap<Product, ProductDto>()
                .ForMember(c => c.Name, m => m.MapFrom(m => m.Name));

        }
    }
}
