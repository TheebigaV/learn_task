using Sample.Interfaces;
using Sample.Models;

namespace Sample.Services
{
    public class ProductService : IProductService
    {
        private static readonly List<Product> products =
        [
            new Product { Id = 1, Name = "Laptop", Price = 100000 },
            new Product { Id = 2, Name = "Mobile", Price = 50000 }
        ];

        public List<Product> GetAll()
        {
            return products;
        }

        public Product? GetById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public Product Create(Product product)
        {
            product.Id = products.Count == 0 ? 1 : products.Max(p => p.Id) + 1;
            products.Add(product);
            return product;
        }

        public Product? Update(int id, Product updatedProduct)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null) return null;

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;

            return existingProduct;
        }

        public bool Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return false;

            products.Remove(product);
            return true;
        }
    }
}