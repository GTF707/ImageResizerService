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
                    var convertedImage = resizeImage(file, new Size(Convert.ToInt32(type.Width), Convert.ToInt32(type.Height)));
                    await convertedImage.SaveAsync($@"{LINK}/X{type.Width.ToString() + type.Height.ToString()}/" + photo.Name);
                }
            };
        }
           
        //private async Task DoWork()
        //{
        //    var photos = await PhotoProvider
        //        .GetAll()
        //        .Where(x => x.PhotoStatus == Domen.Enum.PhotoStatus.Unreaded)
        //        .Take(1)
        //        .ToListAsync();

        //    photos
        //        .ForEach(x => x.PhotoStatus = Domen.Enum.PhotoStatus.InProgress);

        //    foreach (var item in photos)
        //    {
        //        using (var stream = File.OpenRead(item.Path + item.Name))
        //        {
        //            var image = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
        //            var file = Image.Load(image.OpenReadStream());


        //            foreach (var format in type.formatNames)
        //            {
        //                string[] splitedFormat = format.Split('X');
        //                var width = splitedFormat[1];
        //                var height = splitedFormat[2];
        //                var convertedImage = resizeImage(file, new Size(Convert.ToInt32(width), Convert.ToInt32(height)));
        //                convertedImage.Save($@"{Link}/X{width + height}/" + image.FileName);
        //            }
        //        };
        //    }
        //    photos
        //        .ForEach(x => x.PhotoStatus = Domen.Enum.PhotoStatus.Readed);
                
        //    await PhotoProvider.UpdateRange(photos);
        //    await PhotoProvider.SaveChangesAsync();
        //}

        public static Image resizeImage(Image imgToResize, Size size)
        {
            var clone = imgToResize.Clone(
                    i => i.Resize(size.Width, size.Height));
            return clone;
        }
    }
}
