using AutoMapper;
using Services.Dto;
using Model;

namespace EntityMap
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile()
        {
            CreateMap<CategoryDto, Category>()
                .ForMember(c => c.Children, opt => opt.Ignore())
                .ForMember(c => c.ParentCategory, opt => opt.Ignore())
                .ForMember(c => c.Products, opt => opt.Ignore());
            CreateMap<ReviewDto, Review>()
                .ForMember(r => r.Product, opt => opt.Ignore())
                .ForMember(r => r.User, opt => opt.Ignore());
            CreateMap<ProductDto, Product>()
                .ForMember(p => p.Category, opt => opt.Ignore())
                .ForMember(p => p.Reviews, opt => opt.Ignore());
        }
    }
}
