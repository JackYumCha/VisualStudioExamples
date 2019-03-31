using System;
using System.Collections.Generic;

namespace VsExample.Data.MySQLDbFirst
{
    public partial class EfmigrationsHistory
    {
        public string MigrationId { get; set; }
        public string ProductVersion { get; set; }
    }
}
