using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Threading;

namespace AzureStorageQUIZ
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.
                Parse(CloudConfigurationManager.
                GetSetting("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("newqueue");
            queue.CreateIfNotExists();
            CloudQueueMessage message = new CloudQueueMessage("Hello Azure!!!");
            queue.AddMessage(message);
            Console.WriteLine("add message");
            queue.AddMessage(new CloudQueueMessage("Hello two"));
            Console.WriteLine("add anothe message");
            //CloudQueueMessage peekedMessage = queue.PeekMessage(); - создает локальную копию
            //Console.WriteLine(peekedMessage.AsString);
            CloudQueueMessage g_message = queue.GetMessage();
            g_message.SetMessageContent("Updated content");
            queue.UpdateMessage(g_message, TimeSpan.FromSeconds(5.0), MessageUpdateFields.Content | MessageUpdateFields.Visibility);
            //CloudQueueMessage g_message = queue.PeekMessage();
            //g_message.SetMessageContent("Updated content");
            //queue.UpdateMessage(g_message, TimeSpan.FromSeconds(5.0), MessageUpdateFields.Content | MessageUpdateFields.Visibility);
            //peekedMessage = queue.PeekMessage();
            //Console.WriteLine(peekedMessage.AsString);
            Thread.Sleep(6000);
            while ((message = queue.GetMessage())!= null)
            {
                Console.WriteLine(message.AsString);
                //queue.DeleteMessage(message);
            }
            queue.FetchAttributes();
            int? cachedMessageCount = queue.ApproximateMessageCount;
            Console.WriteLine("n="+cachedMessageCount);
            queue.Delete();
            Console.ReadKey(true);
        }
    }
}
