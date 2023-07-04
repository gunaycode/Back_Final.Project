using Application.Abstract.Common;
using Application.DTOs.FileService;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Infrastructure.Services;

public class AzureFileService : IAzureFileService
{

    private readonly BlobServiceClient _blobServiceClient;
    private BlobContainerClient _blobContainerClient;

    public AzureFileService(IConfiguration configuration)
    {
        _blobServiceClient =  new (configuration["Storage:Azure"]);
    }

    public async Task<FileUploadResult> UploudFileAsync(string containerName, IFormFile file)
    {
       string newFileName=Guid.NewGuid().ToString()+file.FileName;
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(newFileName);
        await blobClient.UploadAsync(file.OpenReadStream());

        return new FileUploadResult(file.FileName,$"{containerName}/{newFileName}");

    }
}
