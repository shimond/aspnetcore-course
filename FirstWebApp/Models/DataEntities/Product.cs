namespace FirstWebApp.Models.DataEntities
{
    public record Product
    {
        public required int Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public decimal? Amount { get; init;}
        public string?  Producer  { get; init; }

    }
}
