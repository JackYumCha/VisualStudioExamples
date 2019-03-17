using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcAngular;

namespace VsExample.AspAPI.Dtos
{
    [AngularType]
    public class ListAnimalResponse
    {
        public int PageIndex { get; set; }
        public int NumberOfPages { get; set; }
        public List<Animal> Items { get; set; }
    }
}
