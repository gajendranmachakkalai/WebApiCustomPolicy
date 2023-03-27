using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MTApp.Utilities.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MTApp.Utilities.JWTAuthentication
{
    /// <summary>
    /// JWTToken helper
    /// </summary>
    public class JWTTokenHelper : IJWTTokenHelper
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secret;
        private readonly TimeSpan _tokenLifeTime;

        internal JWTTokenHelper(string issuer, string audience, string Secret, TimeSpan tokenLifeTime)
        {
            _issuer = issuer;
            _audience = audience;
            _secret = Secret;
            _tokenLifeTime = tokenLifeTime;
        }

        public JWTTokenHelper(IOptions<ServiceConfiguration> settings)
        {
            _issuer = settings.Value.JwtSettings.Issuer;
            _audience = settings.Value.JwtSettings.Audience;
            _secret = settings.Value.JwtSettings.Secret;
            _tokenLifeTime = settings.Value.JwtSettings.TokenLifeTime;
        }
        /// <summary>
        /// Generate JWT Token using claims
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public JwtToken GenerateJWTToken(Dictionary<string, string> claims)
        {
            JwtToken jwtToken = new JwtToken();
            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsIdentity Subject = new ClaimsIdentity();
            var CreateDateTimeUTC = DateTime.UtcNow;
            foreach (var item in claims)
            {
                Subject.AddClaim(new Claim(item.Key, item.Value));
            }
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = Subject,
                Expires = CreateDateTimeUTC.Add(_tokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _issuer,
                Audience = _audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtToken()
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = Guid.NewGuid().ToString(),
                CreateDateTimeUTC = CreateDateTimeUTC
            };
        }
        /// <summary>
        /// Validate JWT Token
        /// </summary>
        /// <param name="jwtToken">jwttoken</param>
        /// <returns></returns>
        public IEnumerable<Claim> ValidateJWTToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidIssuer = _issuer,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var token = (JwtSecurityToken)validatedToken;
            return token.Claims;
        }
    }
}
