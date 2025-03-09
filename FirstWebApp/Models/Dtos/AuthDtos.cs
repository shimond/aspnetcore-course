using Microsoft.AspNetCore.Http.HttpResults;

namespace FirstWebApp.Models.Dtos;

public record LoginRequest(string UserName, string Password);
public record LoginResponse(string Token);

