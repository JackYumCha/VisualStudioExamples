﻿using System;
using System.Collections.Generic;

namespace VsExample.Data.MySQLDbFirst
{
    public partial class FriendShip
    {
        public int Id { get; set; }
        public int FromPerson { get; set; }
        public int ToPerson { get; set; }
    }
}
