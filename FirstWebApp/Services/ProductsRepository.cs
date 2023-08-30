using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Services;

public class ProductsRepository : IProductsRepository
{
    private readonly CourseDbContext _courseDbContext;

    public ProductsRepository(CourseDbContext courseDbContext)
    {
        this._courseDbContext = courseDbContext;
    }

    public async Task<Product> AddNewProduct(Product product)
    {
        _courseDbContext.Products.Add(product);
        await _courseDbContext.SaveChangesAsync();
        return product;
    }

    public async Task DeleteItem(int id)
    {
        var item = await _courseDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if(item != null)
        {
            _courseDbContext.Products.Remove(item);
        }
        await _courseDbContext.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllProducts()
    {
        var products = await _courseDbContext.Products.ToListAsync();
        return products;
    }

    public async Task<Product?> GetProductById(int id)
    {
        var item = await _courseDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (item == null)
        {
            return null;
        }
        return item;
    }

    public async Task<Product> UpdateProduct(int id, Product product)
    {
        _courseDbContext.Products.Update(product);
        await _courseDbContext.SaveChangesAsync();
        return product;
    }
}

