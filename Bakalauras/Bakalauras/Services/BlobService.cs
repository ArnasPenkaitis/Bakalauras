using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Bakalauras.Services.Interfaces;

namespace Bakalauras.Services
{
    public class BlobService: IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<Azure.Response> GetBlobAsync(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("gltfs");

            var blobClient =  containerClient.GetBlobClient(name);

            var blobDownlaodInfo =  await blobClient.DownloadToAsync("C:\\Users\\Arnas\\Desktop\\"+ name);


            return blobDownlaodInfo;
        }

        public async Task UploadBlob(string content, string filename)
        {
            var converted = System.Convert.FromBase64String(content);
            var containerClient = _blobServiceClient.GetBlobContainerClient("gltfs");
            var blobClient = containerClient.GetBlobClient(filename);

            using var memoryStream =  new MemoryStream(converted);

            var result =await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders {ContentType = MimeTypes.GetMimeType(filename)});
        }
    }
}
