using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FotoConvector.Domen;
using ImageResizerService.Domen;
using ImageResizerService.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using ImageResizerService.Utils;
using ImageResizerService.DTO;
using ImageResizerService.Domen.Enum;

namespace ImageResizerService.Service
{
    public class ResizeService : IResizeService
    {
        private IPhotoProvider PhotoProvider { get; set; }
        private PhotoType type;
        
        public ResizeService(IPhotoProvider photoProvider)
        {
            PhotoProvider = photoProvider;
            type = PhotoType.getFirst();
        }

        public async Task<ResponceFormatDto> SaveImage(IFormFile image)
        {
            if (image == null)
                return null;

            var file = System.Drawing.Image.FromStream(image.OpenReadStream());
            
                if (type.Width > file.Width || type.Height > file.Height)
                    return null;
            

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

            var formatOptimizer = FormatOptimizer.GetStringFormats(file);
            List<FormatType> formatEnums = new List<FormatType>();
            
            foreach (var item in formatOptimizer)
            {
                Enum.TryParse(item, out FormatType type);
                formatEnums.Add(type);
            }

            ResponceFormatDto responce = new ResponceFormatDto(formatEnums, fileName);
            return responce;
        }
    }
}
