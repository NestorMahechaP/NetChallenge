using AutoMapper;
using NetChallenge.Database.Model;
using NetChallenge.WebApi.Dtos;

namespace NetChallenge.WebApi.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, Product>()
                .ForPath(s => s.Id, opt => opt.Ignore());
        }
    }
}
