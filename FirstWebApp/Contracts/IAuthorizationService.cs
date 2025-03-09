namespace FirstWebApp.Contracts;

public interface IAuthorizationService
{
    Task<UserInfo?> GetUserInfo(string login, string password);
}
