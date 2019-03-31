using Autofac;
using Jack.DataScience.Common;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace VsExample.Data.SQLServer
{
    public class SQLServerDataContextFactory : IDesignTimeDbContextFactory<SQLServerDataContext>
    {
        public SQLServerDataContext CreateDbContext(string[] args)
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer();
            autoFacContainer.RegisterOptions<SQLServerOptions>();
            autoFacContainer.ContainerBuilder.RegisterModule<SQLServerModule>();
            var services = autoFacContainer.ContainerBuilder.Build();
            return services.Resolve<SQLServerDataContext>();
        }
    }
}
