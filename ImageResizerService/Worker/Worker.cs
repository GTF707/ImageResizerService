using FotoConvector.Domen;
using ImageResizerService.Domen;
using ImageResizerService.Domen.Enum;
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

        public const string LINK = "C:/Users/Alexander/Desktop/ConvertedImages";
        private IPhotoProvider PhotoProvider;

        public Worker(IPhotoProvider photoProvider)
        {
            PhotoProvider = photoProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(true)
                await doWork();
        }

        private async Task doWork()
        {
            //const int POOL_SIZE = 10;

            //Task[] tasks = new Task[POOL_SIZE];
            //for (int i = 0; i < POOL_SIZE; i++)
            //{
            var photos = await PhotoProvider
                .GetAll()
                .Where(x => x.PhotoStatus.Equals(PhotoStatus.Unreaded))
                .Take(10)
                .ToListAsync();

            if (photos.Count == 0)
            {
                Thread.Sleep(1000);
                return;
            }

            photos.ForEach(p => p.PhotoStatus = PhotoStatus.InProgress);
            await PhotoProvider.SaveChangesAsync();

            foreach (var photo in photos)
            {
                await resizePhoto(photo);
            }
                //tasks[i] = resizePhoto(photos[i]);
                //Task.Run(async () => tasks[i]);

                //Task.WaitAll(tasks);

            photos.ForEach(p => p.PhotoStatus = PhotoStatus.Readed);
            await PhotoProvider.SaveChangesAsync();
            //}

                

        }

        private async Task resizePhoto(Photo photo)
        {
            using (var stream = File.OpenRead(photo.Path + photo.Name))
            {
                var file = Image.Load(stream);

                foreach (var type in PhotoType.getTypes())
                {
                    if(type.Width > file.Width || type.Height > file.Height)
                    {
                        continue;
                    }
                    else
                    {
                        var convertedImage = resizeImage(file, new Size(Convert.ToInt32(type.Width), Convert.ToInt32(type.Height)));
                        var path = $@"{LINK}/X{type.Width.ToString() + type.Height.ToString()}/";

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        await convertedImage.SaveAsync(path + photo.Name);
                    }
                }
            };
        }
           
        public static Image resizeImage(Image imgToResize, Size size)
        {
            var clone = imgToResize.Clone(
                    i => i.Resize(size.Width, size.Height));
            return clone;
        }
    }
}
