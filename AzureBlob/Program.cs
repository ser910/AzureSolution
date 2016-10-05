using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureBlob
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.
                Parse(CloudConfigurationManager.
                GetSetting("StorageConnectionString"));
            //create the table client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("newContaner");
            container.CreateIfNotExists();
            Console.WriteLine("create blob container");
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            CloudBlockBlob blob = container.GetBlockBlobReference("pic1");
            using (var fileStream = System.IO.File.OpenRead(@"path\myfile"))
            {
                blob.UploadFromStream(fileStream);
            }
            //https://azure.microsoft.com/ru-ru/documentation/articles/storage-dotnet-how-to-use-blobs/
        }
    }
}
