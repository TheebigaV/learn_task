using System.ComponentModel.DataAnnotations;

namespace Sample.DTOs
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; } = string.Empty;

        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000")]
        public decimal Price { get; set; }
    }
}