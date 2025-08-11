using CakePapi.Services.ProductAPI.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakePapi.Services.ProductAPI.Repository
{
    public interface iProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(int productId);
    }
}
