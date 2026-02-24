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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var products = _productService.GetAll();
                var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product = _productService.GetById(id);
                if (product == null)
                {
                    return NotFound(new { message = "Product not found!" });
                }
                var productDto = _mapper.Map<ProductDto>(product);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto productDto)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(productDto);

                var createdProduct = _productService.Create(productEntity);

                var createdProductDto = _mapper.Map<ProductDto>(createdProduct);

                return CreatedAtAction(nameof(GetById), new { id = createdProductDto.Id }, createdProductDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateProductDto updateDto)
        {
            try
            {
                var productEntity = _mapper.Map<Product>(updateDto);

                var updated = _productService.Update(id, productEntity);
                if (updated == null)
                    return NotFound(new { message = "Cannot Update, Product not Found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleted = _productService.Delete(id);
                if (!deleted)
                    return NotFound(new { message = "Cannot Delete, Product not Found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
