using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FotoConvector.Domen;
using ImageResizerService.Domen;
using ImageResizerService.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
namespace ImageResizerService.Service
{
    public class ResizeService : IResizeService
    {
        public string fileName { get; private set; }
        private IPhotoProvider PhotoProvider { get; set; }

        public string link = "C:/Users/Alexander/Desktop/ConvertedImages";
        
       

        public ResizeService(IPhotoProvider photoProvider)
        {
            PhotoProvider = photoProvider;
        }

        public async Task<string> ConvertImage(IFormFile image)
        {

            

            string fileName = string.Empty;

            if (image != null)
            {
                fileName = $"{CryptHelper.CreateMD5(DateTime.Now.ToString())}{Path.GetExtension(image.FileName)}";
                var path = $"{Directory.GetCurrentDirectory()}/Files/";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (var fileStream = new FileStream(path + fileName, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }


            var file = Image.Load(image.OpenReadStream());

            
            PhotoType type = new PhotoType(new List<string>());
            
            foreach (var item in type.formatNames)
            {
                string[] splitedFormat = item.Split('X');
                var width = splitedFormat[1];
                var height = splitedFormat[2];
                var convertedImage = resizeImage(file, new Size(Convert.ToInt32(width), Convert.ToInt32(height)));
                convertedImage.Save($@"{link}/X{width + height}/" + image.FileName);
            }
         


            var photo = new Photo
            {
                Name = fileName,

            };
            PhotoProvider.Create(photo);
            PhotoProvider.SaveChanges();
            return fileName;

        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            var clone = imgToResize.Clone(
                    i => i.Resize(size.Width, size.Height));
            return clone;
        }

       
    

    }
}
