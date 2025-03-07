namespace FirstWebApp.Models.Dtos;

public record ProductDto
{
    public required int Id { get; init; }
    public string? ProductName { get; init; }
    public string? Description { get; init; }
    public double Price { get; init; }
}


public record ProductDtoOneLine(int id, string name, string desc, double price);