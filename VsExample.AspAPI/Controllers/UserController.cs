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
using Jack.DataScience.Data.MongoDB;
using MongoDB.Driver;

namespace VsExample.AspAPI.Controllers
{
    [Angular, Route("[controller]/[action]")]
    public class UserController: Controller
    {

        private readonly JwtObjectEncoder jwtObjectEncoder;
        private readonly GenericJwtToken genericJwtToken;
        private readonly AuthOptions authOptions;
        private readonly MongoContext mongoContext;

        public UserController(
            JwtObjectEncoder jwtObjectEncoder,
            GenericJwtToken genericJwtToken,
            AuthOptions authOptions,
            MongoContext mongoContext
            )
        {
            this.jwtObjectEncoder = jwtObjectEncoder;
            this.genericJwtToken = genericJwtToken;
            this.authOptions = authOptions;
            this.mongoContext = mongoContext;
        }

        [HttpPost]
        public GenericJwtToken Login([FromBody] LoginRequest loginRequest)
        {

            var found = mongoContext.Collection<User>()
                .AsQueryable()
                .Where(u => u._id == loginRequest.Username && u.PasswordHash == loginRequest.PasswordHash)
                .FirstOrDefault();

            if(found == null)
            {
                return new GenericJwtToken()
                {
                    Valid = false,
                    Roles = new List<RoleEnum>()
                };
            }

            var token = new GenericJwtToken()
            {
                Id = found._id,
                Roles = found.Roles,
                Valid = true,
                Name = found.Name,
                ExpiringDate = DateTime.Now.AddDays(authOptions.TokenExpiringDays)
            };

            // token.Token = jwtObjectEncoder.Encode(token);
            token = Response.WriteJWTCookie(token);
            return token;
        }

        [HttpPost]
        [Role()]
        public GenericJwtToken Renew()
        {
            if (genericJwtToken.Valid)
            {


                var found = mongoContext.Collection<User>()
                    .AsQueryable()
                    .Where(u => u._id == genericJwtToken.Id)
                    .FirstOrDefault();

                if(found == null)
                {
                    return new GenericJwtToken()
                    {
                        Valid = false,
                        Roles = new List<RoleEnum>()
                    };
                }

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
