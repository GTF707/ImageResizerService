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
//using System.Drawing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

using Windows.UI.Xaml.Media.Imaging;

namespace ImageResizerService.Service
{
    public class ResizeService : IResizeService
    {
        //private const string SOURCE_FOLDER = "/var/www/html/source/";
        private const string SOURCE_FOLDER = "C:/Users/Alexander/Desktop/ConvertedImages/";
        private IPhotoProvider PhotoProvider { get; set; }
        private PhotoType Type { get; set; }
        
        public ResizeService(IPhotoProvider photoProvider)

        {
            PhotoProvider = photoProvider;
            Type = PhotoType.getFirst();
        }

        public async Task<ResponceFormatDto> SaveImage(ResizeTaskRequest request)
        {

            if (!Directory.Exists(SOURCE_FOLDER))
                Directory.CreateDirectory(SOURCE_FOLDER);

            //Image image = Image.FromFile(request.Path + request.Name);

            Image image = null;
            using (var stream = File.OpenRead(SOURCE_FOLDER + request.Name))
            {
                image = Image.Load(stream);
            }
            Console.WriteLine("Файл получен и начинает обработку");

            if (Type.Width > image.Width || Type.Height > image.Height || image.Width != image.Height)
                return null;

            var photo = new Photo
            {
                Name = request.Name,
                PhotoStatus = PhotoStatus.Unreaded,
                Path = SOURCE_FOLDER
            };

            PhotoProvider.Create(photo);
            PhotoProvider.SaveChanges();
            Console.WriteLine("Файл добавлен в базу");

            var formatOptimizer = FormatOptimizer.GetStringFormats(image);
            List<FormatType> formatEnums = new List<FormatType>();

            foreach (var item in formatOptimizer)
            {
                Enum.TryParse(item, out FormatType type);
                formatEnums.Add(type);
            }
            Console.WriteLine("Получены типы конвертирования для загруженного файла");

            ResponceFormatDto responce = new ResponceFormatDto(formatEnums, request.Name);
            Console.WriteLine("Отправляется ответ");
            return responce;
        }
    }
}
