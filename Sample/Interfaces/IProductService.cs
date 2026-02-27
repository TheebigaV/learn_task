using Sample.Models;

namespace Sample.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAll(string? name, int pageNumber, int pageSize);
        Product? GetById(int id);
        Product Create(Product product);
        Product? Update(int id, Product updatedProduct);
        bool Delete(int id);
    }
}
