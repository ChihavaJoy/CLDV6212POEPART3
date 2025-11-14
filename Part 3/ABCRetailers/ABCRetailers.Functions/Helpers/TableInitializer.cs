using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;


namespace ABCRetailers.Functions.Helpers
{
    public static class TableInitializer
    {
        public static async Task EnsureTablesExistAsync(IConfiguration config)
        {
            // Pick whichever connection string exists
            var conn = config["STORAGE_CONNECTION"] ?? config["AzureWebJobsStorage"];
            if (string.IsNullOrWhiteSpace(conn))
                return; // nothing to do

            var serviceClient = new TableServiceClient(conn);

            var tables = new[]
            {
                config["TABLE_CUSTOMER"] ?? "Customer",
                config["TABLE_PRODUCT"]  ?? "Product",
                config["TABLE_ORDER"]    ?? "Order"
            };

            foreach (var name in tables)
                await serviceClient.CreateTableIfNotExistsAsync(name);
        }
    }
}
