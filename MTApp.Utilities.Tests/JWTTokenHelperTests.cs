using MTApp.Utilities.JWTAuthentication;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MTApp.Utilities.Tests
{
    [TestClass]
    public class JWTTokenHelperTests
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secret;
        private readonly TimeSpan _tokenLifeTime;
        public JWTTokenHelperTests()
        {
            _issuer = "UnitTest";
            _audience = "UnitTestApp";
            _secret = "AD(G+KbPeShVmYq3t6w9y$B3#)G@VcQf";
            _tokenLifeTime = TimeSpan.FromMinutes(5);
        }

        [TestMethod]
        public void GenerateToken_Return_Token()
        {
            //Setup
            IJWTTokenHelper jWTTokenHelper = new JWTTokenHelper(_issuer, _audience, _secret, _tokenLifeTime);
            Dictionary<string, string> claims = new Dictionary<string, string>();
            claims.Add("UserName", "UnitTest");
            claims.Add("Role", "Tester");
            //Action
            var token = jWTTokenHelper.GenerateJWTToken(claims);
            //Assert
            Assert.IsNotNull(token.Token, "Token should not be null");
            Assert.IsNotNull(token.RefreshToken, "Refresh Token should not be null");
        }

        [TestMethod]
        public void ValidateToken_Return_Token()
        {
            //Setup
            IJWTTokenHelper jWTTokenHelper = new JWTTokenHelper(_issuer, _audience, _secret, _tokenLifeTime);
            Dictionary<string, string> claims = new Dictionary<string, string>();
            claims.Add("UserName", "UnitTest");
            claims.Add("Role", "Tester");
            //Action
            var token = jWTTokenHelper.GenerateJWTToken(claims);
            var actualClaims = jWTTokenHelper.ValidateJWTToken(token.Token);
            //Assert
            Assert.IsNotNull(actualClaims, "Claims should not be null");
            Assert.AreEqual("UnitTest", actualClaims.First(x => x.Type == "UserName").Value, "Claim UserName should be equal with expected claim");
            Assert.AreEqual("Tester", actualClaims.First(x => x.Type == "Role").Value, "Claim UserName should be equal with expected claim");
        }
    }
}