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
            //Write a line of text
            sw.Write("Photo is downloaded");
            //Write a second line of text
            sw.WriteLine("From the StreamWriter");
            //Close the file
            sw.Close();
        }
    }
}
