using Jack.DataScience.Data.AWSAthena;
using Jack.DataScience.Storage.AWSS3;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VsExamples.Athena
{
    public class AthenaTableSetup
    {
        private readonly AWSS3API awsS3API;
        private readonly AWSAthenaAPI awsAthenaAPI;

        public AthenaTableSetup(AWSS3API awsS3API, AWSAthenaAPI awsAthenaAPI)
        {
            this.awsS3API = awsS3API;
            this.awsAthenaAPI = awsAthenaAPI;
        }

        public async Task CreateProductsTable()
        {
            var productsTableName = "`productsdb`.`products`";
            var queryDeleteTable = $@"DROP TABLE IF EXISTS {productsTableName}";
            await awsAthenaAPI.ExecuteQuery(queryDeleteTable);

            var query = $@"CREATE EXTERNAL TABLE IF NOT EXISTS {productsTableName}(
`{nameof(ProductItem.ItemKey)}` STRING,
`{nameof(ProductItem.ItemName)}` STRING,
`{nameof(ProductItem.Description)}` STRING,
`{nameof(ProductItem.UnitPrice)}` DOUBLE,
`{nameof(ProductItem.InventoryQuantity)}` DOUBLE
)
ROW FORMAT SERDE 'org.apache.hadoop.hive.ql.io.parquet.serde.ParquetHiveSerDe'
WITH SERDEPROPERTIES (
  'serialization.format' = '1'
)
LOCATION 's3://{awsS3API.Options.Bucket}/products/'";
            await awsAthenaAPI.ExecuteQuery(query);
        }

        public async Task CreateDailySalesTable()
        {
            var salesTableName = "`productsdb`.`sales`";
            var queryDeleteTable = $@"DROP TABLE IF EXISTS {salesTableName}";
            await awsAthenaAPI.ExecuteQuery(queryDeleteTable);

            var query = $@"CREATE EXTERNAL TABLE IF NOT EXISTS {salesTableName}(
`{nameof(SaleEntry.ItemKey)}` STRING,
`{nameof(SaleEntry.ItemName)}` STRING,
`{nameof(SaleEntry.UnitPrice)}` DOUBLE,
`{nameof(SaleEntry.Quantity)}` DOUBLE,
`{nameof(SaleEntry.Price)}` DOUBLE
)
PARTITIONED BY (
    `year` integer,
    `month` integer,
    `date` integer
)
ROW FORMAT SERDE 'org.apache.hadoop.hive.ql.io.parquet.serde.ParquetHiveSerDe'
WITH SERDEPROPERTIES (
  'serialization.format' = '1'
)
LOCATION 's3://{awsS3API.Options.Bucket}/sales/'";
            await awsAthenaAPI.ExecuteQuery(query);

            await awsAthenaAPI.LoadPartition(salesTableName, $"`year`=2019, `month`=5, `date`=26", $"s3://{awsS3API.Options.Bucket}/sales/2019-05-26/");

            await awsAthenaAPI.LoadPartition(salesTableName, $"`year`=2019, `month`=5, `date`=25", $"s3://{awsS3API.Options.Bucket}/sales/2019-05-25/");

        }
    }
}
