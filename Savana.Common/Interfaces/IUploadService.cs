using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Savana.Common.Entities;

namespace Savana.Common.Interfaces
{
    public interface IUploadService
    {
        Task<string> UploadFile(IFormFile file, int? width, int? height);
    }
}