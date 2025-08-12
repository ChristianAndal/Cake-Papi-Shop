using CakePapi.Services.ProductAPI.Models.Dto;
using CakePapi.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CakePapi.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository _productRepository;
        public ProductAPIController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                // Fetch product data
                IEnumerable<ProductDto> productDtos = await _productRepository.GetProducts();

                // Ensure the result is assigned to _response
                _response.Result = productDtos ?? new List<ProductDto>(); // Avoid null assignments
                _response.IsSuccess = true;  // Assuming successful operation
            }
            catch (Exception ex)  // Capture the exception instance
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };  // Use the exception instance
            }

            return _response;
        }
        [HttpGet]
        [Route("{id}")]  // Correct route to capture 'id' parameter
        public async Task<object> Get(int id)
        {
            try
            {
                // Fetch product data
                ProductDto productDto = await _productRepository.GetProductById(id);  // Single ProductDto, not IEnumerable

                // Ensure the result is assigned to _response
                _response.Result = productDto ?? new ProductDto();  // Avoid null assignment, if null, return an empty ProductDto
                _response.IsSuccess = true;  // Assuming successful operation
            }
            catch (Exception ex)  // Capture the exception instance
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };  // Use the exception instance
            }

            return _response;
        }
    }
}
