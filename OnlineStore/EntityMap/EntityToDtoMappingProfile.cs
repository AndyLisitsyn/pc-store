using AutoMapper;
using Services.Dto;
using Model;

namespace EntityMap
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<Review, ReviewDto>();
        }
    }
}
