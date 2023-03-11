using Market.Entities.Dto;
using Market.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Route("Products")]
    public class ProductsController : Controller
    {
        private LoggerLib.Interfaces.ILogger _logger;
        private IDataBaseManager _manager;
        private IProductsRepository _repository;

        public ProductsController(LoggerLib.Interfaces.ILogger logger, IDataBaseManager manager)
        {
            _logger = logger;
            _manager = manager;
            _repository = manager.ProductsRepository;
        }

        [HttpGet("GetProductsByCategory/{category}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            try
            {
                var result = await _repository.GetProductsByCategory(category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"Ошибка при получении товаров по категории {category}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto product)
        {
            try
            {
                await _repository.AddAsync(product);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"Ошибка при добавлении нового товара {ex.Message}");
                return StatusCode(500, ex.Message);

            }
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            try
            {
                var result = await _repository.GetProductById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }
}
