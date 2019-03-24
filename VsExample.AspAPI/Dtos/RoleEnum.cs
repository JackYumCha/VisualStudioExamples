using MvcAngular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VsExample.AspAPI.Dtos
{
    [AngularType]
    public enum RoleEnum
    {
        Administrator,
        User,
        Visitor
    }
}
