using ImageResizerService.DTO;
using ImageResizerService.Service;
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

        /// <summary>
        /// Конвертация изображения
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResponceFormatDto), 200)]
        [Produces("application/json")]
        [HttpPost("save-file")]
        public async Task<ResponceFormatDto> ConvertImage([FromBody] ResizeTaskRequest request)
        {
            return await ResizeService.SaveImage(request);
        }

        /// <summary>
        /// Конвертация всех имеющихся изображений
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(void), 200)]
        [Produces("application/json")]
        [HttpPost("save-all-files")]
        public async Task<ResizeTaskRequest> ConvertAllImages([FromBody] ResizeTaskRequest request)
        {
            return await ResizeService.SaveAllImages(request);
        }
    }
}
