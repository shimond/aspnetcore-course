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
        // Retrieve bearer information from options
        var bearerInformation = bearerOptions.Value;

        // Validate user credentials
        var userInfo = await authorizationService.GetUserInfo(request.UserName, request.Password);
        if (userInfo is null)
        {
            // Return unauthorized result if credentials are invalid
            return TypedResults.Unauthorized();
        }

        // Create claims for the JWT token
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, request.UserName),
            new Claim(JwtRegisteredClaimNames.Sub, request.UserName)
        };

        // Add audience claims to the token
        var audiences = bearerInformation.ValidAudiences;
        foreach (var aud in audiences)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, aud));
        }

        // Add the relevant role
        claims.Add(new Claim(ClaimTypes.Role, userInfo.Role));

        // Retrieve signing key information
        var signKeyInfo = bearerInformation.SigningKeys.First(x => x.Issuer == bearerInformation.ValidIssuer);
        var signKey = signKeyInfo.Value;

        // Convert signing key from base64 string to byte array
        var keyMaterial = new byte[32];
        Convert.TryFromBase64String(signKey, keyMaterial, out var bytesWritten);

        // Create signing credentials using the signing key
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(keyMaterial), SecurityAlgorithms.HmacSha256Signature);

        // Create security token descriptor
        var token = new SecurityTokenDescriptor
        {
            SigningCredentials = signingCredentials,
            Expires = DateTime.UtcNow.AddHours(1), // Set token expiration time
            Issuer = bearerInformation.ValidIssuer,
            Subject = new ClaimsIdentity(claims)
        };

        // Generate JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(tokenHandler.CreateToken(token));

        // Return the generated token in the response
        return TypedResults.Ok(new LoginResponse(tokenString));
    }
}
