using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ApiInscripción.Lib.Security.Authentication
{
    public static class JwtManager
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>
        //private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        private const string Secret = "+1o53XxbqzPVlsm21Pl0SNwSGWRfdk5OB5FpxIYSASZe3bHC43z2Qy4vH9oW3YG+uD1ZPS1+b7xW/8jibRhebA==";

        public static string GenerateToken(string username, int expireMinutes = 60)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "ismp.edu.ar",
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                IssuedAt = now,
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                _log.Debug("JWT token: Válido desde: " + JsonConvert.SerializeObject(jwtToken.ValidFrom) + 
                    " hasta " + JsonConvert.SerializeObject(jwtToken.ValidTo) +
                    " - Now: " + DateTime.Now.ToUniversalTime());

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (SecurityTokenExpiredException ex)
            {
                _log.Error("Token expirado: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                return null;
            }
        }
    }
}