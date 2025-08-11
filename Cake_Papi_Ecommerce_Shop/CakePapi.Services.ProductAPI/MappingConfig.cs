using AutoMapper;
using CakePapi.Services.ProductAPI.Models.Dto;
using CakePapi.Services.ProductAPI.Models; 
namespace CakePapi.Services.ProductAPI
{
    public class MappingConfig
    {
        public MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
            return mappingConfig;
        }
    }
}
