namespace METROWIND.Services
{
    public class BlobService(BlobServiceClient blobServiceClient): IBlobService
    {
        public async Task<ImmutableArray<string>> GetImagessFromBlob(string containerName)
        {
            var blobUrls = ImmutableArray.CreateBuilder<string>();
            var containerClient = blobServiceClient.GetBlobContainerClient(
                containerName.ToLower());

            await foreach (var blob in containerClient.GetBlobsAsync())
            {
                var blobClient = containerClient.GetBlobClient(blob.Name);
                blobUrls.Add(blobClient.Uri.ToString());
            }
            
            return blobUrls.ToImmutableArray();
        }
    }
}
