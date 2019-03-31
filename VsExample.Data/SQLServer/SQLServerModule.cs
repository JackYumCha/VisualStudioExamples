using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.EntityFrameworkCore;

namespace VsExample.Data.SQLServer
{
    public class SQLServerModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var options = context.Resolve<SQLServerOptions>();
                var optionsBuilder = new DbContextOptionsBuilder<SQLServerDataContext>();
                optionsBuilder.UseSqlServer(options.ConnectionString);
                return optionsBuilder.Options;
            });
            builder.RegisterType<SQLServerDataContext>();
            base.Load(builder);
        }
    }
}
