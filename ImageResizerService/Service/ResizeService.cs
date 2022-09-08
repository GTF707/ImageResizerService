using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FotoConvector.Domen;
using ImageResizerService.Domen;
using ImageResizerService.Repository.Interfaces;
using ImageResizerService.Utils;
using ImageResizerService.DTO;
using ImageResizerService.Domen.Enum;
//using System.Drawing;
using SixLabors.ImageSharp;
using System.Threading;

namespace ImageResizerService.Service
{
    public class ResizeService : IResizeService
    {
        //private const string SOURCE_FOLDER = "/var/www/html/source/";
        //private const string SOURCE_FOLDER = "C:/Users/Alexander/Desktop/ConvertedImages/";
        //private const string SOURCE_FOLDER = "C:/Drive/C#Programm/Masters/Masters/Files/";
        private IPhotoProvider PhotoProvider { get; set; }
        private Task<PhotoType> Type { get; set; }

        public ResizeService(IPhotoProvider photoProvider)
        {
            PhotoProvider = photoProvider;
            Type = PhotoType.getFirst();
        }
        public async Task<ResponceFormatDto> SaveImage(ResizeTaskRequest request)
        {

            if (!Directory.Exists(request.Path))
                Directory.CreateDirectory(request.Path);

            //Image image = Image.FromFile(request.Path + request.Name);

            Image image = null;
            using (var stream = File.OpenRead(request.Path + request.Name))
            {
                try
                {
                    image = Image.Load(stream);
                }
                catch (Exception)
                {
                    return null;
                }

            }

            if (image == null)
            {
                return null;
            }
            //Console.WriteLine("Файл получен и начинает обработку");

            if (Type.Result.Width > image.Width || Type.Result.Height > image.Height)
            {
                return null;
            }

            var photo = new Photo
            {
                Name = request.Name,
                PhotoStatus = PhotoStatus.Unreaded,
                Path = request.Path
            };

            await PhotoProvider.CreateAsync(photo);
            await PhotoProvider.SaveChangesAsync();
            //Console.WriteLine("Файл добавлен в базу");

            var formatOptimizer = await FormatOptimizer.GetStringFormats(image);
            List<FormatType> formatEnums = new List<FormatType>();

            foreach (var item in formatOptimizer)
            {
                Enum.TryParse(item, out FormatType type);
                formatEnums.Add(type);
            }
            //Console.WriteLine("Получены типы конвертирования для загруженного файла");

            ResponceFormatDto responce = new ResponceFormatDto(formatEnums, request.Name);
            //Console.WriteLine("Отправляется ответ");
            return responce;
        }

        public async Task<ResizeTaskRequest> SaveAllImages(ResizeTaskRequest request)
        {
            List<string> images = new List<string>();
            foreach (var item in request.NamesList)
            {
                var response = await SaveImage(new ResizeTaskRequest(request.Path, item));
                if (response != null)
                    images.Add(response.FileName);
            }
            ResizeTaskRequest resizeTaskRequest = new ResizeTaskRequest(request.Path, images);
            return resizeTaskRequest;
        }
    }
}
