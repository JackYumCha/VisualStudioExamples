using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcAngular;

namespace VsExample.AspAPI.Dtos
{
    [AngularType]
    public class ListAnimalRequest
    {
        public int NumberPerPage { get; set; }
        public int PageIndex { get; set; }
        public string Filter { get; set; }
    }
}
