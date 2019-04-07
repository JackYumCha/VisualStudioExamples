using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jack.DataScience.Data.MongoDB;
using MvcAngular;

namespace VsExample.AspAPI.Dtos
{
    [AngularType]
    public class Animal: DocumentBase
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
