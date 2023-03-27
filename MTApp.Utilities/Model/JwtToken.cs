using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTApp.Utilities.Model
{
    public class JwtToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreateDateTimeUTC { get; set; }
    }
}
