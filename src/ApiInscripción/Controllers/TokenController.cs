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
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // This is naive endpoint for demo, it should use Basic authentication
        // to provide token or POST request
        [IdentityBasicAuthentication] // Enable Basic authentication for this action.
        //[BasicAuthenticationFilter]
        public string Post(string username, string password)
        {
            _log.Debug("Entra");
            if (new UsuarioRepository().ExisteUsuario(username, password))
            {
                return JwtManager.GenerateToken(username);
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}
