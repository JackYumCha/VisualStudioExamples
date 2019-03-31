using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace VsExample.Data.MySQL
{
    public class MySQLDataModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var options = context.Resolve<MySQLOptions>();
                var optionsBuilder = new DbContextOptionsBuilder<MySQLDataContext>();
                optionsBuilder.UseMySql(options.ConnectionString);
                return optionsBuilder.Options;
            });
            builder.RegisterType<MySQLDataContext>();
            base.Load(builder);
        }
    }
}
