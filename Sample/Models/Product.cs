using System.ComponentModel.DataAnnotations;

namespace Sample.Models

{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public required string Name { get; set; }

        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000")]
        public decimal Price { get; set; }
    }
}
