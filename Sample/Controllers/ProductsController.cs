using Microsoft.AspNetCore.Mvc;
using Sample.Interfaces;
using Sample.Models;

namespace Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService productService = productService;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(productService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = productService.GetById(id);

            if (product == null)
                return NotFound(new { message = "Product not found!" });

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest(new {message = "Request Body is Missing"});
            }

            products.Add(newProduct);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newProduct.Id },
                newProduct
            );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product updateProduct)
        {
            if (updateProduct == null)
                return BadRequest(new { message = "Request body is missing" });

            if (string.IsNullOrWhiteSpace(updateProduct.Name))
                return BadRequest(new { message = "Name is required" });

            if (updateProduct.Price <= 0)
                return BadRequest(new { message = "Price must be greater than 0" });

            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
                return NotFound(new { message = "Cannot Update, Product not Found" });

            existingProduct.Name = updateProduct.Name;
            existingProduct.Price = updateProduct.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = productService.Delete(id);

            if (!deleted)
                return NotFound(new { message = "Cannot Delete, Product not Found" });
            }

            products.Remove(product);

            return NoContent();
        }
    }
}