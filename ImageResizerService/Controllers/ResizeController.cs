using ImageResizerService.DTO;
using ImageResizerService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ImageResizerService.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class ResizeController
    {
        private readonly IResizeService ResizeService;

        public ResizeController(IResizeService resizeService)
        {
            ResizeService = resizeService;
        }

        [ProducesResponseType(typeof(ResponceFormatDto), 200)]
        [Produces("application/json")]
        [HttpPost("save-file")]
        public async Task<ResponceFormatDto> ConvertImage([FromBody] ResizeTaskRequest request)
        {
            return await ResizeService.SaveImage(request);
        }
    }
}
