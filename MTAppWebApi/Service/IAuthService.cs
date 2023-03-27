using MTAPP.Model;

namespace MTAppWebApi.Service
{
    public interface IAuthService
    {
        UserModel ValidateUser(string username, string password);
    }
}