using Application.DTOs.FileService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Abstract.Common;

public interface IAzureFileService
{
    public Task<FileUploadResult> UploudFileAsync(string container, IFormFile file);
 
}
