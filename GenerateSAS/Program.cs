using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateSAS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What type of SAS do you want to generate? \n1. Blob \n2. Table \n3. Queue \n4. File");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 4)
            {
                GenerateFileSAS();
            }
            else
            {
                Console.WriteLine("Option not available yet.");
            }
        }

        private static void GenerateFileSAS()
        {
            Console.WriteLine("Default Endpoints Protocol: 1 = http, 2 = https");
            int choice = int.Parse(Console.ReadLine());
            string defaultEndpointsProtocol = "https";
            if (choice == 1)
            {
                defaultEndpointsProtocol = "http";
            }
            else if (choice == 2)
            {
                defaultEndpointsProtocol = "https";
            }
            Console.WriteLine("Account Name:");
            string accountName = Console.ReadLine();
            Console.WriteLine("Account Key:");
            string accountKey = Console.ReadLine();
            string storageConnectionString = "DefaultEndpointsProtocol=" + defaultEndpointsProtocol + ";AccountName=" + accountName + ";AccountKey=" + accountKey;

            //Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //Create the blob client object.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Get a reference to a container to use for the sample code, and create it if it does not exist.
            CloudBlobContainer container = blobClient.GetContainerReference("sascontainer");
            container.CreateIfNotExists();
        }
    }
}
