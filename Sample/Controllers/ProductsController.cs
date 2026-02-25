using Microsoft.AspNetCore.Mvc;
using Sample.Interfaces;
using Sample.Models;
using System.Collections.Generic;
using AutoMapper;
using Sample.DTOs;

namespace Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProductService productService, IMapper mapper) : ControllerBase
    {
        private readonly IProductService productService = productService;
        private readonly IMapper mapper = mapper;

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? name)
        {
            try
            {
                var products = productService.GetAll(name);
                var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);
                return Ok(productsDto);
            }
            catch (Exception )
            {
                return StatusCode(500, "Internal server error");
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
            catch (Exception )
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto productDto)
        {
            try
            {
                var productEntity = mapper.Map<Product>(productDto);
                var createdProduct = productService.Create(productEntity);
                var createdProductDto = mapper.Map<ProductDto>(createdProduct);
                return CreatedAtAction(nameof(GetById), new { id = createdProductDto.Id }, createdProductDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateProductDto updateDto)
        {
            try
            {
                var productEntity = mapper.Map<Product>(updateDto);
                var updated = productService.Update(id, productEntity);
                if (updated == null)
                    return NotFound(new { message = "Cannot Update, Product not Found" });

                return NoContent();
            }
            catch (Exception )
            {
                return StatusCode(500, "Internal server error");
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
            catch (Exception )
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}