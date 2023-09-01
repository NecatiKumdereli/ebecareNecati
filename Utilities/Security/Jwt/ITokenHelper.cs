using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(dynamic claims_options);
    }
}
