using Microsoft.AspNetCore.Mvc;
using Sample.Models; 
using System.Collections.Generic; 

namespace Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id =1 , Name = "Laptop" , Price = 100000},
            new Product { Id =2 , Name = "Mobile" , Price = 50000}
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_products);
        }

        [HttpGet("{id}")] 
        public IActionResult GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
        {
            return NotFound(new { message = "Product not found!" });
        }
        return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product newProduct){
            _products.Add(newProduct);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product updateProduct){
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if(existingProduct == null)
            {
                return NotFound(new {message = "Cannot Update,Product not Found"});
            }
            existingProduct.Name = updateProduct.Name;
            existingProduct.Price = updateProduct.Price;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var product = _products.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound(new {message = "Cannot Delete,Product not Found"});
            }
            _products.Remove(product);
            return NoContent();
        }

    }
}
