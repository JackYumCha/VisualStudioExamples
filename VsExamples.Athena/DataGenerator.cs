using Jack.DataScience.Data.CSV;
using Jack.DataScience.Storage.AWSS3;
using Jack.DataScience.Storage.AzureBlobStorage;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace VsExamples.Athena
{
    public class DataGenerator
    {
        private readonly SftpClient sftpClient;
        private readonly AWSS3API awsS3API;
        private readonly AzureBlobStorageAPI azureBlobStorageAPI;

        public DataGenerator(SftpClient sftpClient, AWSS3API awsS3API, AzureBlobStorageAPI azureBlobStorageAPI)
        {
            this.sftpClient = sftpClient;
            this.awsS3API = awsS3API;
            this.azureBlobStorageAPI = azureBlobStorageAPI;
        }


        public List<ProductItem> GenerateInventoryData()
        {
            return new List<ProductItem>()
            {
                new ProductItem()
                {
                    ItemKey = "OnePlus7",
                    ItemName = "1+7",
                    Description = "1+7",
                    InventoryQuantity =  100d,
                    UnitPrice = 4400,
                },
                new ProductItem()
                {
                    ItemKey = "iPhone XS Plus",
                    ItemName = "iPhone XS Plus",
                    Description = "iPhone XS Plus",
                    InventoryQuantity =  200d,
                    UnitPrice = 12000,
                },
                new ProductItem()
                {
                    ItemKey = "Samsung Galaxy Note 7",
                    ItemName = "Samsung Galaxy Note 7",
                    Description = "Samsung Galaxy Note 7",
                    InventoryQuantity =  400d,
                    UnitPrice = 8000,
                },
                new ProductItem()
                {
                    ItemKey = "Xiaomi 8",
                    ItemName = "Xiaomi 8",
                    Description = "Xiaomi 8",
                    InventoryQuantity =  800d,
                    UnitPrice = 3000,
                }
            };
        }

        public List<SaleEntry> GenerateSaleData()
        {
            var products = GenerateInventoryData();

            var list = new List<SaleEntry>();
            var random = new Random();
            
            for(int i = 0;i < 100; i++)
            {
                var item = products[random.Next(products.Count)];
                var quantity = (double)random.Next(5);
                list.Add(new SaleEntry()
                {
                    ItemKey = item.ItemKey,
                    ItemName = item.ItemName,
                    UnitPrice = item.UnitPrice,
                    Quantity = quantity,
                    Price = item.UnitPrice * quantity,
                });
            }
            return list;
        }


        public void WriteDataToSFTP()
        {
            sftpClient.Connect();

            using (MemoryStream memoryStream = new MemoryStream())
            {

                var products = GenerateInventoryData();
                memoryStream.WriteCsv(products);

                using (var streamToUpload = new MemoryStream(memoryStream.ToArray()))
                {
                    sftpClient.UploadFile(streamToUpload, $"/root/data/products.csv");
                }
            }

            for(DateTime date = DateTime.UtcNow.AddDays(-10); date < DateTime.UtcNow; date = date.AddDays(1))
            {

                using (MemoryStream memoryStream = new MemoryStream())
                {

                    var sales = GenerateSaleData();
                    memoryStream.WriteCsv(sales);
                    using (var streamToUpload = new MemoryStream(memoryStream.ToArray()))
                    {
                        sftpClient.UploadFile(streamToUpload, $"/root/data/sales-{date.ToString("yyyy-MM-dd")}.csv");
                    }
                }
            }

            sftpClient.Disconnect();
        }

        public async Task WriteToBlobStorageContainer()
        {
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {

                    var products = GenerateInventoryData();
                    memoryStream.WriteCsv(products);

                    using (var streamToUpload = new MemoryStream(memoryStream.ToArray()))
                    {
                        await azureBlobStorageAPI.Upload("products.csv", streamToUpload);
                    }
                }
            }

            {
                for (DateTime date = DateTime.UtcNow.AddDays(-10); date < DateTime.UtcNow; date = date.AddDays(1))
                {

                    using (MemoryStream memoryStream = new MemoryStream())
                    {

                        var sales = GenerateSaleData();
                        memoryStream.WriteCsv(sales);
                        using (var streamToUpload = new MemoryStream(memoryStream.ToArray()))
                        {
                            await azureBlobStorageAPI.Upload($"sale-{date.ToString("yyyy-MM-dd")}.csv", streamToUpload);
                        }
                    }
                }
            }

        }
    }
}
