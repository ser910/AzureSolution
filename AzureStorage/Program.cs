using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using LogLevel = Microsoft.Framework.Logging;

namespace AzureStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.
                Parse(CloudConfigurationManager.
                GetSetting("StorageConnectionString"));
            //create the table client
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("peopleTable");
            table.CreateIfNotExists();

            ////create the batch operation
            //TableBatchOperation batchOperation = new TableBatchOperation();

            ////create a new customer entity
            //CustomerEntity customer1 = new CustomerEntity("Khanu", "Nina");
            //customer1.Email = "nina.khanu@gmail.com";
            //customer1.PhoneNumber = "+380990000000";

            ////create the tableoperation that inserts the customer entity
            ////TableOperation insertOperation = TableOperation.Insert(customer1);

            ////execute the insert operation
            ////table.Execute(insertOperation);

            //CustomerEntity customer2 = new CustomerEntity("Khanu", "Ruslan");
            //customer2.Email = "ruslan.khanu@gmail.com";
            //customer2.PhoneNumber = "+380990000000";

            //CustomerEntity customer3 = new CustomerEntity("Khanu", "Lyudmila");
            //customer3.Email = "lyudmila.brynza@gmail.com";
            //customer3.PhoneNumber = "+380990000000";

            ////add all customer entities to the batch insert operation
            //batchOperation.Insert(customer1);
            //batchOperation.Insert(customer2);
            //batchOperation.Insert(customer3);

            ////execute the batch operation
            //table.ExecuteBatch(batchOperation);

            //create a retrieve operation that expects a customer entity
            TableOperation retrieveOperation = TableOperation.Retrieve<CustomerEntity>("Khanu", "Lyudmila");

            //execute the operation
            TableResult retrievedResult = table.Execute(retrieveOperation);

            //assign the result to a CustomerEntity
            CustomerEntity deleteEntity = (CustomerEntity) retrievedResult.Result;

            //create the Delete TableOperation
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                //execute the operation
                table.Execute(deleteOperation);

                Console.WriteLine("Entity deleted.");
            }
            else
            {
                Console.WriteLine("Could not retrieve the entity.");
            }
        }
    }
}