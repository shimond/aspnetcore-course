using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FirstWebApp.EndPoints;

public static class AuthEndPoints
{
    public static void MapAuth(this WebApplication app)
    {
        var paymentsGroup = app.MapGroup("auth").WithTags("Auth");
        paymentsGroup.MapPost("Login", Login).WithName(nameof(Login));
    }

    static async Task<Results<UnauthorizedHttpResult, Ok<LoginResponse>>> Login(LoginRequest request, IOptions<BearerInfo> bearerOptions, IAuthorizationService authorizationService)
    {
        var bearerInformation = bearerOptions.Value;
        var isValid = await authorizationService.IsUserValid(request.UserName, request.Password);
        if (!isValid)
        {
            return TypedResults.Unauthorized();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, request.UserName),
            new Claim(JwtRegisteredClaimNames.Sub, request.UserName)
        };

        var audiences = bearerInformation.ValidAudiences;
        foreach (var aud in audiences)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, aud));
        }


        var signKeyInfo = bearerInformation.SigningKeys.First(x=>x.Issuer == bearerInformation.ValidIssuer);
        var signKey = signKeyInfo.Value;

        var keyMaterial = new byte[32];
        Convert.TryFromBase64String(signKey, keyMaterial, out var bytesWritten);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(keyMaterial), SecurityAlgorithms.HmacSha256Signature);

        var token = new SecurityTokenDescriptor
        {
            SigningCredentials = signingCredentials,
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = bearerInformation.ValidIssuer,
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(tokenHandler.CreateToken(token));
        return TypedResults.Ok(new LoginResponse(tokenString)); 
    }
}
