namespace FirstWebApp.Services;


public class AuthorizationService : IAuthorizationService
{
    public Task<bool> IsUserValid(string login, string password)
    {
        return Task.FromResult(login == "david" && password == "12345678");
    }
}
