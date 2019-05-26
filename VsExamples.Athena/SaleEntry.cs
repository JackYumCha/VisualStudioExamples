using System;
using System.Collections.Generic;
using System.Text;

namespace VsExamples.Athena
{
    public class SaleEntry
    {
        public string ItemKey { get; set; }
        public string ItemName { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
    }

    public class ProductItem
    {
        public string ItemKey { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public double InventoryQuantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
