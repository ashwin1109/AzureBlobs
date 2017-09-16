using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
namespace AMListBlobEx
{
    class Program
    {
        static void Main(string[] args)
        {
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            //CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            //// Create the container if it doesn't already exist.
            //container.CreateIfNotExists();
            //container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            //// Retrieve reference to a blob named "AMblob".
            //CloudBlockBlob blockBlob = container.GetBlockBlobReference("AMblob");

            //// Create or overwrite the "AMblob" blob with contents from a local file.
            //using (var fileStream = System.IO.File.OpenRead(@"c:\check\kwame.txt"))
            //{
            //    blockBlob.UploadFromStream(fileStream);
            //}
            //Console.WriteLine("File upload...");
            AMListBlobEx();
        }

        private static void AMListBlobEx()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                        CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            // Retrieve reference to a previously created container.
            

            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);

                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);

                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                    Console.WriteLine("Directory: {0}", directory.Uri);
                }
            }
            Console.WriteLine("Reading done...");
        }

        //async public static Task ListBlobsSegmentedInFlatListing(CloudBlobContainer container)
        //{
        //    //List blobs to the console window, with paging.
        //    Console.WriteLine("List blobs in pages:");

        //    int i = 0;
        //    BlobContinuationToken continuationToken = null;
        //    BlobResultSegment resultSegment = null;

        //    //Call ListBlobsSegmentedAsync and enumerate the result segment returned, while the continuation token is non-null.
        //    //When the continuation token is null, the last page has been returned and execution can exit the loop.
        //    do
        //    {
        //        //This overload allows control of the page size. You can return all remaining results by passing null for the maxResults parameter,
        //        //or by calling a different overload.
        //        resultSegment = await container.ListBlobsSegmentedAsync("", true, BlobListingDetails.All, 10, continuationToken, null, null);
        //        if (resultSegment.Results.Count<IListBlobItem>() > 0) { Console.WriteLine("Page {0}:", ++i); }
        //        foreach (var blobItem in resultSegment.Results)
        //        {
        //            Console.WriteLine("\t{0}", blobItem.StorageUri.PrimaryUri);
        //        }
        //        Console.WriteLine();

        //        //Get the continuation token.
        //        continuationToken = resultSegment.ContinuationToken;
        //    }
        //    while (continuationToken != null);
        //}
       
    }
}
