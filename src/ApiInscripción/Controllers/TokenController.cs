using ApiInscripción.Lib.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiInscripción.Filters.BasicAuth;
using ApiInscripción.Repositories;

namespace ApiInscripción.Controllers
{
    [Authorize]
    public class TokenController : ApiController
    {
        // This is naive endpoint for demo, it should use Basic authentication
        // to provide token or POST request
        [IdentityBasicAuthentication] // Enable Basic authentication for this action.
        //[BasicAuthenticationFilter]
        public string Post(string username, string password)
        {
            if (new UsuarioRepository().ExisteUsuario(username, password))
            {
                return JwtManager.GenerateToken(username);
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}
