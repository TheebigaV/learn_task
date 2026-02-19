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
            try {
                return Ok(_productService.GetAll());
            }
            catch (Exception ex){
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")] 
        public IActionResult GetById(int id)
        {
            try {
                var product = _productService.GetById(id);
                if (product == null)
            {
                return NotFound(new { message = "Product not found!" });
            }
            return Ok(product);
            }
            catch (Exception ex){
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult Create(Product newProduct){

            try {
                var created = _productService.Create(newProduct);
                return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
            }
            catch (Exception ex){
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product updateProduct){
            try {
                var updated = _productService.Update(id, updateProduct);
                if (updated == null)
                    return NotFound(new { message = "Cannot Update, Product not Found" });

                return NoContent();
            }
            catch (Exception ex){
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            try {
                var deleted = _productService.Delete(id);
                if (!deleted)
                    return NotFound(new { message = "Cannot Delete, Product not Found" });

                return NoContent();
            }
            catch (Exception ex){
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
