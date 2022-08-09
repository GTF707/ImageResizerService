using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace ImageResizerService.Service
{
    public class ResizeService : IResizeService
    {
        public void ConvertImage(IFormFile image)
        {
            StreamWriter sw = new StreamWriter("C:/Users/Alexander/Desktop/ConvertedImages/Images.txt");
            sw.WriteLine("Photo is downloaded");
            sw.WriteLine("From the StreamWriter");
            sw.Close();
        }
    }
}
