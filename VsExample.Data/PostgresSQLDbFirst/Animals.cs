using System;
using System.Collections.Generic;

namespace VsExample.Data.PostgresSQLDbFirst
{
    public partial class Animals
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Price { get; set; }
        public bool IsMale { get; set; }
    }
}
