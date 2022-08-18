using ImageResizerService.Controllers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ImageResizerService.Service
{
    public interface IResizeService
    {
        public Task<string> SaveImage(IFormFile image);

    }
}

