using Jack.DataScience.Common;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace VsExample.Data.MySQL
{
    public class MySQLDataContextFactory : IDesignTimeDbContextFactory<MySQLDataContext>
    {
        public MySQLDataContext CreateDbContext(string[] args)
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer();
            autoFacContainer.RegisterOptions<MySQLOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<MySQLDataModule>();
            var services = autoFacContainer.ContainerBuilder.Build();
            return services.Resolve<MySQLDataContext>();
        }
    }
}
