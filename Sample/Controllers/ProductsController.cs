using Microsoft.AspNetCore.Mvc;
using Sample.Interfaces;
using Sample.Models;

namespace Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(productService.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product = productService.GetById(id);
                if (product == null)
                {
                    return NotFound(new { message = "Product not found!" });
                }
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            try
            {
                if (newProduct == null)
                    return BadRequest(new { message = "Request Body is Missing" });

                var createdProduct = productService.Create(newProduct);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product updateProduct)
        {
            try
            {
                if (updateProduct == null)
                    return BadRequest(new { message = "Request body is missing" });

                if (string.IsNullOrWhiteSpace(updateProduct.Name))
                    return BadRequest(new { message = "Name is required" });

                if (updateProduct.Price <= 0)
                    return BadRequest(new { message = "Price must be greater than 0" });

                var updated = productService.Update(id, updateProduct);

                if (updated == null)
                    return NotFound(new { message = "Cannot Update, Product not Found" });

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleted = productService.Delete(id);
                if (!deleted)
                    return NotFound(new { message = "Cannot Delete, Product not Found" });

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}