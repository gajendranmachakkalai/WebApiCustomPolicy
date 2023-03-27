using Microsoft.EntityFrameworkCore;
using MTApp.Utilities.Model;
using MTAPP.DAL.Model;
using MTAPP.DAL.Repository;
using MTAPP.Model;
using MTAppWebApi.Utilities;

namespace MTAppWebApi.Service
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<AppUser> _userRepository;
        public AuthService(IGenericRepository<AppUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel ValidateUser(string username, string password)
        {
            UserModel userdetails = null;
            var isvalid = false;
            var user = _userRepository.Get().AsNoTracking().Where(x => username == username).AsEnumerable();
            foreach (var item in user)
            {
                if (string.Equals(username, item.username) && string.Equals(password, item.passwordhash))
                {
                    isvalid = true;
                    userdetails = UserServiceUtility.ConvertToModel(item);
                    break;
                }
            }
            return userdetails;
        }
    }
}
