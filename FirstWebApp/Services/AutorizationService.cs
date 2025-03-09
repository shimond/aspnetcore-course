namespace FirstWebApp.Services;


public class AuthorizationService : IAuthorizationService
{
    public async Task<UserInfo?> GetUserInfo(string login, string password)
    {
        await Task.Delay(1000);
        if(login == "david" && password == "12345678")
        {
            return new UserInfo(login, "david@gmail.com", "user");
        }
        else if(login == "admin" && password == "Aa123456")
        {
            return new UserInfo(login, "admin@gmail.com", "admin");
        }
        return null;
    }
}
