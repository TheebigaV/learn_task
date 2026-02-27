using Microsoft.AspNetCore.Mvc;
using Sample.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> products =
        [
            new Product { Id = 1, Name = "Laptop", Price = 100000 },
            new Product { Id = 2, Name = "Mobile", Price = 50000 }
        ];

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Product not found!" });
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest(new { message = "Request Body is Missing" });
            }

            if (string.IsNullOrWhiteSpace(newProduct.Name))
            {
                return BadRequest(new { message = "Name is required" });
            }

            if (newProduct.Price <= 0)
            {
                return BadRequest(new { message = "Price must be greater than 0" });
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
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Cannot Delete, Product not Found" });
            }

            products.Remove(product);

            return NoContent();
        }
    }
}