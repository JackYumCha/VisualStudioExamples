using MvcAngular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VsExample.AspAPI.Dtos
{
    [AngularType]
    public class GenericJwtToken
    {
        public string Id { get; set; }
        public List<RoleEnum> Roles { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public bool Valid { get; set; }
        public DateTime ExpiringDate { get; set; }
    }
}
