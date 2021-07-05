using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Savana.Common.Interfaces
{
    public interface IUploadService
    {
        Task<string> UploadFile(IFormFile file, int? width, int? height);
        Task<string> RemoveFile(string fileName);
    }
}