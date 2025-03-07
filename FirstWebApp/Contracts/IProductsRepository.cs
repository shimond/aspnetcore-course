
namespace FirstWebApp.Contracts;

public interface IProductsRepository
{
    Task<ProductEntity> AddNewProduct(ProductEntity product);
    Task DeleteItem(int id);
    Task<List<ProductEntity>> GetAllProducts();
    Task<ProductEntity?> GetProductById(int id);
    Task<ProductEntity> UpdateProduct(int id, ProductEntity product);
}
