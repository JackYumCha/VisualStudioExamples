using Autofac;
using Jack.DataScience.Common;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace VsExample.Data.PostgresSQL
{
    public class PostgresSQLDataContextFactory : IDesignTimeDbContextFactory<PostgresSQLDataContext>
    {
        public PostgresSQLDataContext CreateDbContext(string[] args)
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer();
            autoFacContainer.RegisterOptions<PostgresSQLOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<PostgresSQLModule>();
            var services = autoFacContainer.ContainerBuilder.Build();
            return services.Resolve<PostgresSQLDataContext>();
        }
    }
}
