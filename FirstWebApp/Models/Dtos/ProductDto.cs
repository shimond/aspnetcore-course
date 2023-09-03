namespace FirstWebApp.Models.Dtos;

public record ProductDto
{
    public required int Id { get; init; }
    public string? ProductName { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
}


public record ProductDtoSingleLine(int id, string name, string desc, decimal price);