using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace ImageResizerService.Service
{
    public class ResizeService : IResizeService
    {
        public void ConvertImage(IFormFile image)
        {
            var file = Image.FromStream(image.OpenReadStream());
            var convertedImage = resizeImage(file, new Size(192, 184));

            StreamWriter sw = new StreamWriter("C:/Users/Alexander/Desktop/ConvertedImages/Images.txt");
            sw.WriteLine("Photo is downloaded");
            sw.WriteLine(convertedImage.Size);
            convertedImage.Save(@"C:/Users/Alexander/Desktop/ConvertedImages/photo.jpg");
            sw.Close();
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size); 
        }
    }
}
