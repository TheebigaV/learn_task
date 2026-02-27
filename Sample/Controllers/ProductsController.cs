using Microsoft.AspNetCore.Mvc;
using Sample.Interfaces;
using Sample.Models;
using AutoMapper;
using Sample.DTOs;

namespace Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProductService productService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var products = productService.GetAll();
                var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);
                return Ok(productsDto);
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
                var productDto = mapper.Map<ProductDto>(product);
                return Ok(productDto);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto newProductDto)
        {
            try
            {
                if (newProductDto == null)
                    return BadRequest(new { message = "Request Body is Missing" });

                var productEntity = mapper.Map<Product>(newProductDto);
                var createdProduct = productService.Create(productEntity);
                var productDto = mapper.Map<ProductDto>(createdProduct);

                return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateProductDto updateProductDto)
        {
            try
            {
                if (updateProductDto == null)
                    return BadRequest(new { message = "Request body is missing" });

                if (string.IsNullOrWhiteSpace(updateProductDto.Name))
                    return BadRequest(new { message = "Name is required" });

                if (updateProductDto.Price <= 0)
                    return BadRequest(new { message = "Price must be greater than 0" });

                var productEntity = mapper.Map<Product>(updateProductDto);
                var updated = productService.Update(id, productEntity);

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