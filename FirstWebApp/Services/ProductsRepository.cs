namespace FirstWebApp.Services;

public class ProductsRepository(AspnetCourseFromDbContext courseDbContext) : IProductsRepository
{
    public async Task<ProductEntity> AddNewProduct(ProductEntity product)
    {
        courseDbContext.Products.Add(product);
        await courseDbContext.SaveChangesAsync();
        return product;
    }

    public async Task DeleteItem(int id)
    {
        var item = await courseDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (item != null)
        {
            courseDbContext.Products.Remove(item);
        }
        await courseDbContext.SaveChangesAsync();
    }

    public async Task<List<ProductEntity>> GetAllProducts()
    {
        var products = await courseDbContext.Products.ToListAsync();
        return products;
    }

    public async Task<ProductEntity?> GetProductById(int id)
    {
        var item = await courseDbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (item == null)
        {
            return null;
        }
        return item;
    }

    public async Task<ProductEntity> UpdateProduct(int id, ProductEntity product)
    {
        courseDbContext.Products.Update(product);
        await courseDbContext.SaveChangesAsync();
        return product;
    }

}