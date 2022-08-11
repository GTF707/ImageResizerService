using ImageResizerService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;



namespace ImageResizerService.Controllers
{
    public class ResizeController
    {
        private readonly IResizeService ResizeService;

        public ResizeController(IResizeService resizeService)
        {
            ResizeService = resizeService;
        }

        /// <summary>
        /// Конвертация файла / Convertation file
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(void), 200)]
        [Produces("application/json")]
        [HttpPost("convert-image")]
        public async Task<string> ConvertImage(IFormFile image)
        {
            return await ResizeService.ConvertImage(image);
        }
    }
}
