using ImageResizerService.Controllers;
using ImageResizerService.DTO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ImageResizerService.Service
{
    public interface IResizeService
    {
        public Task<ResponceFormatDto> SaveImage(ResizeTaskRequest request);

    }
}

