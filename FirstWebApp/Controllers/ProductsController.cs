using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

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
