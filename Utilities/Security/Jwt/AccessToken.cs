using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Security.Jwt
{
    public class AccessToken
    {
        public string Token { get; set; }
        public string ReToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
