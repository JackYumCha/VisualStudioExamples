using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsExample.AspAPI.Dtos;
using VsExample.AspAPI.Utils;

namespace VsExample.AspAPI.Authorization
{
    public class RoleFilter : IAuthorizationFilter
    {
        private readonly RoleEnum[] roles;

        public RoleFilter(RoleEnum[] roles)
        {
            this.roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //string jwt = null;
            var login = context.HttpContext.Request.ReadJWTCookie();

            if ((login.ExpiringDate > DateTime.Now && login?.Roles != null) &&
                (roles.Length == 0 ?
                    login.Roles.Any() : roles.Intersect(login.Roles).Any())
                 )
                return;

            context.Result = new UnauthorizedResult();
        }
    }
}
