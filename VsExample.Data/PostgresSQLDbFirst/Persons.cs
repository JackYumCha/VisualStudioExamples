using System;
using System.Collections.Generic;

namespace VsExample.Data.PostgresSQLDbFirst
{
    public partial class Persons
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsMale { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
