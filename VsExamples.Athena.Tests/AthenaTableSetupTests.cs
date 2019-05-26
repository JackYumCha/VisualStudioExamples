using Autofac;
using Jack.DataScience.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace VsExamples.Athena.Tests
{
    public class AthenaTableSetupTests
    {
        [Fact(DisplayName = "Setup Products Table")]
        public async void SetupProductsTable()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var athenaTableSetup = services.Resolve<AthenaTableSetup>();

            await athenaTableSetup.CreateProductsTable();

            Debugger.Break();
        }

        [Fact(DisplayName = "Setup Sales Table")]
        public async void SetupSalesTable()
        {
            AutoFacContainer autoFacContainer = new AutoFacContainer("dev");
            autoFacContainer.Setup();
            var services = autoFacContainer.ContainerBuilder.Build();

            var athenaTableSetup = services.Resolve<AthenaTableSetup>();

            await athenaTableSetup.CreateDailySalesTable();

            Debugger.Break();
        }
    }

    /*
SELECT 
s.itemkey,
s.itemname,
p.description,
p.unitprice,
s.quantity,
s.price

FROM "productsdb"."sales" s
join "productsdb"."products" p
on s.itemkey = p.itemkey
where date = 26
limit 10
     */

    /*
with p as
(
  select itemkey as key, description, unitprice as uprice
  from "productsdb"."products"
 )
SELECT 
s.itemkey,
s.itemname,
description,
uprice,
s.quantity,
s.price

FROM "productsdb"."sales" s
join p
on itemkey = key
where date = 26
limit 10
     */
}
