using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.EntityFrameworkCore;

namespace VsExample.Data.PostgresSQL
{
    public class PostgresSQLModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var options = context.Resolve<PostgresSQLOptions>();
                var optionsBuilder = new DbContextOptionsBuilder<PostgresSQLDataContext>();
                optionsBuilder.UseNpgsql(options.ConnectionString);
                return optionsBuilder.Options;
            });
            builder.RegisterType<PostgresSQLDataContext>();
            base.Load(builder);
        }
    }
}
