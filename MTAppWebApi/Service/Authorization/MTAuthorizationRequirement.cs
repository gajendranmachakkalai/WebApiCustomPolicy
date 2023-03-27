using Microsoft.AspNetCore.Authorization;

namespace MTAppWebApi.Service.Authorization
{
    public class MTAuthorizationRequirement : IAuthorizationRequirement
    {
        public MTAuthorizationRequirement()
        {

        }
        public MTAuthorizationRequirement(string sqlConnectionstring)
        {
            //SqlConnectionstring = sqlConnectionstring;
        }

        //public string SqlConnectionstring { get; private set; }
    }
}
