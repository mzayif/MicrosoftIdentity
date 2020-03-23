using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace AspNetCoreIdentityExample.Models.Authentication
{
    public class UserClaimProvider : IClaimsTransformation
    {
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
            Claim claim = null;
            if (principal.HasClaim(x => x.Type == "username"))
            {
                claim = new Claim("username", identity.Name);
                identity.AddClaim(claim);
            }
            if (principal.HasClaim(x => x.Type == "logintime"))
            {
                claim = new Claim("logintime", DateTime.Now.ToString());
                identity.AddClaim(claim);
            }

            return principal;
        }
    }
}
