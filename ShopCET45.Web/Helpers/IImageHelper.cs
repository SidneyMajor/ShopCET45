using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ShopCET45.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }
}
