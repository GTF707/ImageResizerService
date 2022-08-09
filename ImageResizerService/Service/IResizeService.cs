using ImageResizerService.Controllers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ImageResizerService.Service
{
    public interface IResizeService
    {
        void ConvertImage(IFormFile image);

    }
}
