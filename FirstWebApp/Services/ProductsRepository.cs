namespace FirstWebApp.Services;

public class ProductsRepository : IProductsRepository
{
    public ProductsRepository()
    {
            
    }
    public List<Product> GetAllProducts()
    {
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


public class ProductsRepository2 : IProductsRepository
{
    public List<Product> GetAllProducts()
    {
        return new List<Product>()
        {
            new Product
            {
                    Id=2,
                    Name="Bisli",
                    Producer="Osem",
                    Amount = 99,
                    Description = "The best product ever",
                    Price = 12.6m
            }
        };
    }

}
