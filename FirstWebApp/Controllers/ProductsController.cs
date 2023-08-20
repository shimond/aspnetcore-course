using FirstWebApp.Models.Dtos;
using FirstWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FirstWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly HeaderRemoveConfig _headerConfig;
        private readonly IProductsRepository _productsRepository;

        public ProductsController(
            IConfiguration configuration,
            IProductsRepository productsRepository,
            IOptionsSnapshot<HeaderRemoveConfig> options)
        {
            _productsRepository = productsRepository;
            var logLevel = configuration["Logging:LogLevel:Default"];
            this._headerConfig = options.Value;
        }

        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            var result = _productsRepository.GetAllProducts();
            return Ok(result);
        }

        [HttpGet("ConfigValue")]
        public ActionResult<HeaderRemoveConfig> GetConfig() {
            return Ok(this._headerConfig);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public ActionResult<string> GetString(int id, string? name, int? number)
        {
            if(id > 100)
            {
                return NotFound();
            }
            return Ok(DateTime.Now.ToLongTimeString());
        }

        [HttpPost("SendProduct/{name}")]
        public ActionResult<ProductDto> SendAndGetProduct(string name, ProductDto p) {

            //p.Name = "asdasd";
            return Ok(p);
        }
    }
}
