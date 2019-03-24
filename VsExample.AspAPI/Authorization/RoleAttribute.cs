using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsExample.AspAPI.Dtos;

namespace VsExample.AspAPI.Authorization
{
    public class RoleAttribute : TypeFilterAttribute
    {
        public RoleAttribute(params RoleEnum[] roles) : base(typeof(RoleFilter))
        {
            Arguments = new object[] { roles };
        }
    }
}
