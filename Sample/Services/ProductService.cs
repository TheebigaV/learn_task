using Sample.Interfaces;
using Sample.Models;

namespace Sample.Services
{
    public class ProductService : IProductService
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 100000 },
            new Product { Id = 2, Name = "Mobile", Price = 50000 }
        };

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Product Create(Product product)
        {
            _products.Add(product);
            return product;
        }

        public Product? Update(int id, Product updatedProduct)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null) return null;

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            return existingProduct;
        }

        public bool Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return false;

            _products.Remove(product);
            return true;
        }
    }
}