using Jack.DataScience.Data.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace VsExample.Data.Mongo
{
    public class Person: DocumentBase
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsMale { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<string> TeamMembers { get; set; }
        public string ReportsTo { get; set; }
    }
}
