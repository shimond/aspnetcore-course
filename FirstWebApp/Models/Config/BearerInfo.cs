
namespace FirstWebApp.Models.Config;

public class BearerInfo
{
    public required string[] ValidAudiences { get; set; }
    public required string ValidIssuer { get; set; }
    public required List<JwtInfo> SigningKeys { get; set; }
}