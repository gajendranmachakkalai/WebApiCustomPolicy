using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTApp.Utilities.Model
{
    public class ServiceConfiguration
    {
        public JwtSettings JwtSettings { get; set; }

    }

    public class JwtSettings
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }

    }
}
