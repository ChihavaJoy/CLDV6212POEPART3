using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;

namespace ABCRetailers.Functions.Functions
{
    public class BlobFunctions
    {
        [Function("OnProductImageUploaded")]
        public void OnProductImageUploaded(
            [BlobTrigger("%BLOB_PRODUCT_IMAGES%/{name}", Connection = "STORAGE_CONNECTION")] Stream blob,
            string name,
            FunctionContext ctx)
        {
            var log = ctx.GetLogger("OnProductImageUploaded");
            log.LogInformation($"Product image uploaded: {name}, size={blob.Length} bytes");
        }
    }
}
