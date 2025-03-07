namespace FirstWebApp.Services;

public class ProductsRepository : IProductsRepository
{
    private readonly AspnetCourseFromDbContext _courseDbContext;

    public ProductsRepository(AspnetCourseFromDbContext courseDbContext)
    {
        this._courseDbContext = courseDbContext;
    }

    public async Task<ProductEntity> AddNewProduct(ProductEntity product)
    {
        _courseDbContext.Products.Add(product);
        await _courseDbContext.SaveChangesAsync();
        return product;
    }

    public async Task DeleteItem(int id)
    {
        var item = await _courseDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (item != null)
        {
            _courseDbContext.Products.Remove(item);
        }
        await _courseDbContext.SaveChangesAsync();
    }

    public async Task<List<ProductEntity>> GetAllProducts()
    {
        var products = await _courseDbContext.Products.ToListAsync();
        return products;
    }

    public async Task<ProductEntity?> GetProductById(int id)
    {
        var item = await _courseDbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (item == null)
        {
            return null;
        }
        return item;
    }

    public async Task<ProductEntity> UpdateProduct(int id, ProductEntity product)
    {
        _courseDbContext.Products.Update(product);
        await _courseDbContext.SaveChangesAsync();
        return product;
    }

}