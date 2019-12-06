using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ApiInscripción.Filters.BasicAuth
{
    public class IdentityBasicAuthenticationAttribute : BasicAuthenticationAttribute
    {
        protected override async Task<IPrincipal> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            //System.Guid.NewGuid()
            var usr = "363dcec1-bf5f-40c2-af98-ea158cb07800.ismp.edu.ar";

            //var cryptoRandomDataGenerator = new System.Security.Cryptography.RNGCryptoServiceProvider();
            //byte[] buffer = new byte[32];
            //cryptoRandomDataGenerator.GetBytes(buffer);
            //string uniq = Convert.ToBase64String(buffer);
            //return uniq;
            var pwd = "U2BGBPPbSgDVYUN+qSnG0/uyUkI+CzqhULDGlvCa4Iw=";

            if (userName != usr || password != pwd)
            {
                // No user with userName/password exists.
                return null;
            }

            // Create a ClaimsIdentity with all the claims for this user.
            Claim nameClaim = new Claim(ClaimTypes.Name, userName);
            List<Claim> claims = new List<Claim> { nameClaim };

            // important to set the identity this way, otherwise IsAuthenticated will be false
            // see: http://leastprivilege.com/2012/09/24/claimsidentity-isauthenticated-and-authenticationtype-in-net-4-5/
            ClaimsIdentity identity = new ClaimsIdentity(claims, AuthenticationTypes.Basic);

            var principal = await Task.FromResult(new ClaimsPrincipal(identity));
            return principal;
        }
    }
}