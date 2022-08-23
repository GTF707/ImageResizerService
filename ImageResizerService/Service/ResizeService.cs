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
        private IPhotoProvider PhotoProvider { get; set; }

        public string link = "C:/Users/Alexander/Desktop/ConvertedImages";

        public ResizeService(IPhotoProvider photoProvider)
        {
            PhotoProvider = photoProvider;
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            if (image != null)
            {
                string fileName = $"{CryptHelper.CreateMD5(Guid.NewGuid().ToString())}{Path.GetExtension(image.FileName)}";
                var path = $"{Directory.GetCurrentDirectory()}/Files/";
                path = path.Replace(@"\", "/");


                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);


                using (var fileStream = new FileStream(path + fileName, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                var photo = new Photo
                {
                    Name = fileName,
                    PhotoStatus = Domen.Enum.PhotoStatus.Unreaded,
                    Path = path
                };

                PhotoProvider.Create(photo);
                PhotoProvider.SaveChanges();
            }
            return link;
        }
    }
}
