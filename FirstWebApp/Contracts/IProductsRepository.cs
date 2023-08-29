namespace FirstWebApp.Contracts;

public interface IProductsRepository
{
    Task<Product> AddNewProduct(Product product);
    Task DeleteItem(int id);
    Task<List<Product>> GetAllProducts();
    Task<Product?> GetProductById(int id);
    Task<Product> UpdateProduct(int id, Product product);
}
