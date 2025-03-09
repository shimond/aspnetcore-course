namespace FirstWebApp.Models.Config;

public class JwtInfo
{
    public  required string Id { get; set; }
    public required string Issuer { get; set; }
    public required string Value { get; set; }
    public required int Length { get; set; }
}
