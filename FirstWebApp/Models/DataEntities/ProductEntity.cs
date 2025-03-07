namespace FirstWebApp.Models.DataEntities;

public record ProductEntity
{
    public required int Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public double Price { get; init; }
    public int? Amount { get; init;}
    public string?  Producer  { get; init; }

}
