using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureStorageFile
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.
                Parse(CloudConfigurationManager.
                GetSetting("StorageConnectionString"));
            //create the table client
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference("qwerty");
            if (share.Exists())
            {
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();
                CloudFile file = rootDir.GetFileReference("images.jpg");
                if (file.Exists())
                {
                    CloudFile destFile = rootDir.GetFileReference("logs.txt");
                    Console.WriteLine(file.DownloadTextAsync().Result);
                    destFile.StartCopy(file);
                    Console.WriteLine(destFile.DownloadText());
                }
                CloudFile newFile = rootDir.GetFileReference("new.txt");
                //newFile.UploadFromStream еще не допилено
                newFile.UploadText("пример файла в root директории");
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("filecontainer");
                container.CreateIfNotExists();
                CloudBlockBlob destBlob = container.GetBlockBlobReference("fileToBlob.txt");
                string fileSas = newFile.GetSharedAccessSignature(new SharedAccessFilePolicy()
                {
                    Permissions = SharedAccessFilePermissions.Read,
                    SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24) //доступ к файлу на 24 часа
                });
                Uri fileSasUri = new Uri(newFile.StorageUri.PrimaryUri.ToString() + fileSas);
                destBlob.StartCopy(fileSasUri); 
            }
            Console.WriteLine("Program end");
            Console.ReadKey(true);
        }
    }
}
