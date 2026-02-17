using Microsoft.AspNetCore.Mvc;
using Sample.Interfaces;
using Sample.Models; 
using System.Collections.Generic; 

namespace Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet("{id}")] 
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
        {
            return NotFound(new { message = "Product not found!" });
        }
        return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product newProduct){
            var created = _productService.Create(newProduct);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product updateProduct){
            var updated = _productService.Update(id, updateProduct);
            if (updated == null)
                return NotFound(new { message = "Cannot Update, Product not Found" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var deleted = _productService.Delete(id);
            if (!deleted)
                return NotFound(new { message = "Cannot Delete, Product not Found" });

            return NoContent();
        }

    }
}
