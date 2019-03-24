using Autofac;
using Jack.DataScience.Common;
using Jack.DataScience.Http.Jwt;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsExample.AspAPI.Dtos;


namespace VsExample.AspAPI.Utils
{
    public static class LoginTokenExtensions
    {
        public static GenericJwtToken WriteJWTCookie(this HttpResponse response, GenericJwtToken token)
        {
            token.Valid = true;
            var jwtEncoder = Startup.ApplicationContainer.Resolve<JwtObjectEncoder>();
            token.Token = jwtEncoder.Encode(token);
            AuthOptions authOptions = Startup.ApplicationContainer.Resolve<AuthOptions>();
            response.Cookies.Append(authOptions.JWTCookieKey, token.Token, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(authOptions.TokenExpiringDays)
            });
            return token;
        }

        public static void DeleteJWTCookie(this HttpResponse response)
        {
            AuthOptions authOptions = Startup.ApplicationContainer.Resolve<AuthOptions>();
            response.Cookies.Delete(authOptions.JWTCookieKey);
        }

        public static GenericJwtToken ReadJWTCookie(this HttpRequest request)
        {
            string jwt = null;
            AuthOptions authOptions = Startup.ApplicationContainer.Resolve<AuthOptions>();
            try
            {
                if (request.Cookies.TryGetValue(authOptions.JWTCookieKey, out jwt))
                {
                    var jwtEncoder = Startup.ApplicationContainer.Resolve<JwtObjectEncoder>();
                    var login = jwtEncoder.Decode<GenericJwtToken>(jwt);
                    return login;
                }
            }
            catch (Exception ex)
            {
            }
            return new GenericJwtToken() { Valid = false };
        }
    }
}
