namespace FirstWebApp.Services;

public class ProductsRepository : IProductsRepository
{
    public ProductsRepository()
    {
            
    }
    public async Task<List<Product>> GetAllProducts()
    {
        await Task.Delay(1000);

        return new List<Product>()
        {
            new Product
            {
                    Id=1,
                    Name="Bamba",
                    Producer="Osem",
                    Amount = 99,
                    Description = "The best product ever",
                    Price = 12.6m
            }
        };
    }

}

