namespace FirstWebApp.Contracts;

public interface IAuthorizationService
{
    Task<bool> IsUserValid(string login, string password);
}
