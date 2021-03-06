﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureStorage
{
    class CustomerEntity : TableEntity
    {
        public CustomerEntity(string lastName, string firstName)
        {
            this.PartitionKey = lastName;
            this.RowKey = firstName;
        }
        //this constructor must exist always!!!!!!
        public CustomerEntity() { }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
