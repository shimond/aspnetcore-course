namespace FirstWebApp.Services;

public class ProductsRepository : IProductsRepository
{
    private List<Product> _products = new List<Product>();

    public ProductsRepository()
    {
        _products.Add(new Product
        {
            Id = 1,
            Name = "Bamba",
            Producer = "Osem",
            Amount = 99,
            Description = "The best product ever",
            Price = 12.6m
        });
    }

    public Task<Product> AddNewProduct(Product product)
    {
        var newId = this._products.Max(x => x.Id) + 1;
        var productToInsert = product with { Id = newId };
        _products.Add(productToInsert);
        return Task.FromResult(productToInsert);
    }

    public async Task DeleteItem(int id)
    {
        await Task.Delay(100);
        var item = this._products.FirstOrDefault(x=> x.Id == id);
        if(item != null)
        {
            this._products.Remove(item);
        }
    }

    public async Task<List<Product>> GetAllProducts()
    {
        await Task.Delay(1000);
        return _products;
    }

    public async Task<Product?> GetProductById(int id)
    {
        await Task.Delay(500);
        var item = this._products.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return null;
        }
        return item;
    }

    public async Task<Product> UpdateProduct(int id, Product product)
    {
        await Task.Delay(500);
        var item = this._products.First(x => x.Id == id);

        var itemToUpdate  = item with { Producer = product.Producer, Amount = product.Amount, Description = product.Description, Price = product.Price, Name = product.Name };
        _products =  _products.Select(item => { 
            if(item.Id  == id)
            {
                return itemToUpdate;
            }
            else
            {
                return item;
            }
        }).ToList();

        return itemToUpdate;
    }
}

