namespace FirstWebApp.MappingProfiles;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dto=> dto.ProductName, o=> o.MapFrom(p=> p.Name))
            .ReverseMap();
    }
}
