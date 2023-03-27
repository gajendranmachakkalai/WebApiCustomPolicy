using MTApp.Utilities.Model;
using System.Security.Claims;

namespace MTApp.Utilities.JWTAuthentication
{
    public interface IJWTTokenHelper
    {
        JwtToken GenerateJWTToken(Dictionary<string, string> claims);
        IEnumerable<Claim> ValidateJWTToken(string jwtToken);
    }
}