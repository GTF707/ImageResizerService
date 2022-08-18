using FotoConvector.Domen;
using ImageResizerService.Repository.Interfaces;
using ImageResizerService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImageResizerService.Worker
{
    public class Worker : BackgroundService
    {
        public string link = "C:/Users/Alexander/Desktop/ConvertedImages";
        private IPhotoProvider PhotoProvider;
        private Timer Timer;

        public Worker(IPhotoProvider photoProvider)
        {
            PhotoProvider = photoProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Timer = new Timer(async (t) => await DoWork(), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }
           
        private async Task/*<List<string>>*/ DoWork()
        {
            var photos = await PhotoProvider
                .GetAll()
                .Where(x => x.PhotoStatus == Domen.Enum.PhotoStatus.Unreaded)
                .ToListAsync();

            //List<string> pathsToFiles = new List<string>();

            foreach (var item in photos)
            {
                using (var stream = File.OpenRead(item.Path + item.Name))
                {
                    var image = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    var file = Image.Load(image.OpenReadStream());

                    PhotoType type = new PhotoType(new List<string>());

                    foreach (var format in type.formatNames)
                    {
                        string[] splitedFormat = format.Split('X');
                        var width = splitedFormat[1];
                        var height = splitedFormat[2];
                        var convertedImage = resizeImage(file, new Size(Convert.ToInt32(width), Convert.ToInt32(height)));
                        convertedImage.Save($@"{link}/X{width + height}/" + image.FileName);
                        //pathsToFiles.Add($@"{link}/X{width + height}/" + image.FileName);
                    }
                };
                item.PhotoStatus = Domen.Enum.PhotoStatus.Readed;
                PhotoProvider.Update(item);
                await PhotoProvider.SaveChangesAsync();
            }
            //return pathsToFiles;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            var clone = imgToResize.Clone(
                    i => i.Resize(size.Width, size.Height));
            return clone;
        }
    }
}
