using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace ImageResizerService.Service
{
    public class ResizeService : IResizeService
    {
        public void ConvertImage(IFormFile image)
        {
            var file = Image.FromStream(image.OpenReadStream());
            StreamWriter sw = new StreamWriter("C:/Users/Alexander/Desktop/ConvertedImages/Images.txt");
            sw.WriteLine("Photo is downloaded");
            sw.WriteLine($"Width = {file.Width} Height = {file.Height}");
            sw.Close();
        }
    }
}
