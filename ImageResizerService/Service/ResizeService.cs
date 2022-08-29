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
using System.Drawing;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageResizerService.Service
{
    public class ResizeService : IResizeService
    {
        private IPhotoProvider PhotoProvider { get; set; }
        private PhotoType Type { get; set; }
        

        private const string LINK = "C:/Drive/C#Programm/Masters/Masters/Files/";
        
        public ResizeService(IPhotoProvider photoProvider)
        {
            PhotoProvider = photoProvider;
            Type = PhotoType.getFirst();
        }

        public async Task<ResponceFormatDto> SaveImage(ResizeTaskRequest request)
        {
            //if (request == null)
            //{
            //    var path = $@"{request.Path}/CanseledFiles{request.Name}";
            //    if (!Directory.Exists(path))
            //        Directory.CreateDirectory(path);


            //    return null;
            //}

            Image image = Image.FromFile(request.Path + request.Name);

            if (Type.Width > image.Width || Type.Height > image.Height)
                return null;

            var photo = new Photo
            {
                Name = request.Name,
                PhotoStatus = PhotoStatus.Unreaded,
                Path = request.Path
            };

            PhotoProvider.Create(photo);
            PhotoProvider.SaveChanges();

            var formatOptimizer = FormatOptimizer.GetStringFormats(image);
            List<FormatType> formatEnums = new List<FormatType>();
            
            foreach (var item in formatOptimizer)
            {
                Enum.TryParse(item, out FormatType type);
                formatEnums.Add(type);
            }

            ResponceFormatDto responce = new ResponceFormatDto(formatEnums, request.Name);
            return responce;
        }
    }
}
