using Jack.DataScience.Data.CSV;
using Jack.DataScience.Storage.AWSS3;
using Jack.DataScience.Storage.AWSS3.Extensions;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsExamples.Athena
{
    public class SftpEtl
    {

        private readonly SftpClient sftpClient;
        private readonly AWSS3API awsS3API;
        public SftpEtl(SftpClient sftpClient, AWSS3API awsS3API)
        {
            this.sftpClient = sftpClient;
            this.awsS3API = awsS3API;
        }

        public async Task TransferProductsData()
        {
            sftpClient.Connect();

            using(var sftpStream = sftpClient.OpenRead("/root/data/products.csv"))
            {
                var products = sftpStream.ReadCsv<ProductItem>();
                await awsS3API.WriteParquet(products, "products.parquet");
            }

            sftpClient.Disconnect();
        }

        public async Task TransferDailySalesData()
        {
            DateTime date = DateTime.UtcNow.Date;

            sftpClient.Connect();

            var directory = "/root/data";

            var dates = sftpClient.ListDirectory(directory)
                .Where(sftpFile => !sftpFile.IsDirectory && sftpFile.Name.StartsWith("sales"))
                .Select(sftpFile => sftpFile.Name.Substring(6, 10))
                .OrderByDescending(name => name)
                .ToList() ;

            string dateFound = null;
            foreach(var sftpDate in dates)
            {
                if (!await awsS3API.FileExists($"sales/{sftpDate}/data.parquet"))
                {
                    dateFound = sftpDate;
                    break;
                }
            }

            if(dateFound != null)
            {
                using (var sftpStream = sftpClient.OpenRead($"/root/data/sales-{dateFound}.csv"))
                {
                    var sales = sftpStream.ReadCsv<SaleEntry>();
                    await awsS3API.WriteParquet(sales, $"sales/{dateFound}/data.parquet");
                }
            }

            sftpClient.Disconnect();

        }
    }
}
