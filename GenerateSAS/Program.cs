using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
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
            CloudStorageAccount storageAccount = GetStorageAccount();

            //Create the file client object
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();

            //Get a reference to file share
            Console.WriteLine("Share:");
            string shareName = Console.ReadLine();
            CloudFileShare share  = fileClient.GetShareReference(shareName);

            //Get reference to file
            Console.WriteLine("File:");
            string fileName = Console.ReadLine();
            CloudFileDirectory rootDir = share.GetRootDirectoryReference();
            CloudFile file = rootDir.GetFileReference("abc.txt");
       
            //Create SAS
            SharedAccessFilePolicy sasConstraints = new SharedAccessFilePolicy();
            sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-10);
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(10);
            sasConstraints.Permissions = SharedAccessFilePermissions.Read;

            //Generate the shared access signature on the blob, setting the constraints directly on the signature.
            string sasFileToken = file.GetSharedAccessSignature(sasConstraints);

            //Return the URI string for the container, including the SAS token.
            Console.WriteLine(file.Uri + sasFileToken);

        }

        private static CloudStorageAccount GetStorageAccount()
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
            Console.WriteLine(storageConnectionString);

            //Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            return storageAccount;
        }
    }
}
