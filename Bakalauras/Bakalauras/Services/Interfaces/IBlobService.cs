using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;

namespace Bakalauras.Services.Interfaces
{
    public interface IBlobService
    {
        Task<Azure.Response> GetBlobAsync(string name);
        Task UploadBlob(string content, string filename);
    }
}
