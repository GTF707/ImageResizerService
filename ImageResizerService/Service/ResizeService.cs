﻿using System;
using System.IO;
using System.Threading.Tasks;
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
            var convertedImage = resizeImage(file, new Size(192, 184));
            var convertedImage1 = resizeImage(file, new Size(168, 168));
            var convertedImage2 = resizeImage(file, new Size(128, 168));
            var convertedImage3 = resizeImage(file, new Size(112, 112));
            var convertedImage4 = resizeImage(file, new Size(80, 80));
            var convertedImage5 = resizeImage(file, new Size(72, 96));
            var convertedImage6 = resizeImage(file, new Size(72, 88));
            var convertedImage7 = resizeImage(file, new Size(56, 56));
            var convertedImage8 = resizeImage(file, new Size(48, 48));
            var convertedImage9 = resizeImage(file, new Size(40, 40));
            //StreamWriter sw = new StreamWriter(@"/Users/kristina/Desktop/photo/.jpeg");
            //sw.WriteLine("Photo is downloaded");
            //sw.WriteLine(convertedImage.Size);



            convertedImage.Save(@"C:/Users/Alexander/Desktop/ConvertedImages/X192184/" + image.FileName);
            convertedImage1.Save(@"C:/Users/Alexander/Desktop/ConvertedImages/X168168/" + image.FileName);
            convertedImage2.Save(@"C:/Users/Alexander/Desktop/ConvertedImages/X128168/" + image.FileName);
            convertedImage3.Save(@"C:/Users/Alexander/Desktop/ConvertedImages/X112112/" + image.FileName);
            convertedImage4.Save(@"C:/Users/Alexander/Desktop/ConvertedImages/X8080/" + image.FileName);
            convertedImage5.Save(@"C:/Users/Alexander/Desktop/ConvertedImages/X7296/" + image.FileName);
            convertedImage6.Save(@"C:/Users/Alexander/Desktop/ConvertedImages/X7288/" + image.FileName);
            convertedImage7.Save(@"C:/Users/Alexander/Desktop/ConvertedImages/X5656/" + image.FileName);
            convertedImage8.Save(@"C:/Users/Alexander/Desktop/ConvertedImages/X4848/" + image.FileName);
            convertedImage9.Save(@"C:/Users/Alexander/Desktop/ConvertedImages/X4040/" + image.FileName);
            //sw.Close();

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
