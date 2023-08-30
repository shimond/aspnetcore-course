using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Product>))]

        public async Task<ActionResult<List<Product>>> Get()
        {
            var result = await _productsRepository.GetAllProducts();
            return Ok("Wow");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var result = await _productsRepository.GetProductById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Product>> DeleteItem(int id)
        {
            var result = await _productsRepository.GetProductById(id);
            if (result == null)
            {
                return NotFound();
            }

            await _productsRepository.DeleteItem(id);

            return Ok(result);
        }


        //api/products/3
        [HttpPut("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Product>> UpdateItem(int id, Product product)
        {
            var result = await _productsRepository.GetProductById(id);
            if (result == null)
            {
                return NotFound();
            }

            await _productsRepository.UpdateProduct(id, product);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Product))]
        public async Task<ActionResult<Product>> AddNewProduct(Product product)
        {
            var addedProduct = await _productsRepository.AddNewProduct(product);
            return Created($"api/products/{addedProduct.Id}", addedProduct);
        }

    }
}
