using Sample.Interfaces;
using Sample.Models;
using Sample.Data;
using Microsoft.EntityFrameworkCore;

namespace Sample.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext context;

        public ProductService(AppDbContext context)
        {
            this.context = context;
        }

        public List<Product> GetAll(string? name, int pageNumber, int pageSize)
        {
            var query = context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Product? GetById(int id)
        {
            return context.Products.Find(id);
        }

        public Product Create(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product? Update(int id, Product updatedProduct)
        {
            var existingProduct = context.Products.Find(id);
            if (existingProduct == null) return null;

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;

            context.SaveChanges();
            return existingProduct;
        }

        public bool Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product == null) return false;

            context.Products.Remove(product);
            context.SaveChanges();
            return true;
        }
    }
}