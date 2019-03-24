using Microsoft.AspNetCore.Mvc;
using MvcAngular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsExample.AspAPI.Dtos;
using Jack.DataScience.Http.Password;
using Jack.DataScience.Http.Jwt;
using VsExample.AspAPI.Utils;
using Jack.DataScience.Common;
using VsExample.AspAPI.Authorization;

namespace VsExample.AspAPI.Controllers
{
    [Angular, Route("[controller]/[action]")]
    public class UserController: Controller
    {

        private readonly JwtObjectEncoder jwtObjectEncoder;
        private readonly GenericJwtToken genericJwtToken;
        private readonly AuthOptions authOptions;

        public UserController(
            JwtObjectEncoder jwtObjectEncoder,
            GenericJwtToken genericJwtToken,
            AuthOptions authOptions
            )
        {
            this.jwtObjectEncoder = jwtObjectEncoder;
            this.genericJwtToken = genericJwtToken;
            this.authOptions = authOptions;
        }

        [HttpPost]
        public GenericJwtToken Login([FromBody] LoginRequest loginRequest)
        {
            if(loginRequest.Username == "abc" && loginRequest.PasswordHash == "123".ToMD5Hash())
            {
                var token = new GenericJwtToken()
                {
                    Id = "abc",
                    Roles = new List<RoleEnum>() { RoleEnum.Administrator },
                    Valid = true,
                    Name = "ABC",
                    ExpiringDate = DateTime.Now.AddDays(authOptions.TokenExpiringDays)
                };

                // token.Token = jwtObjectEncoder.Encode(token);
                token = Response.WriteJWTCookie(token);
                return token;
            }

            return new GenericJwtToken();
        }

        [HttpPost]
        [Role()]
        public GenericJwtToken Renew()
        {
            if (genericJwtToken.Valid)
            {
                var token = genericJwtToken;
                token.Token = null;
                token.ExpiringDate = DateTime.Now.AddDays(authOptions.TokenExpiringDays);
                token = Response.WriteJWTCookie(token);

                return token;
            }
            return new GenericJwtToken();
        }

        [HttpPost]
        public GenericJwtToken Logout()
        {
            Response.DeleteJWTCookie();
            return null;
        }
    }
}
