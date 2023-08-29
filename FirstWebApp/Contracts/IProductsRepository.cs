namespace FirstWebApp.Contracts;

public interface IProductsRepository
{
    Task<List<Product>> GetAllProducts();
}
