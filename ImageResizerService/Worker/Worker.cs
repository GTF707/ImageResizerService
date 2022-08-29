using ImageResizerService.Domen;
using ImageResizerService.Domen.Enum;
using ImageResizerService.Repository.Interfaces;
using ImageResizerService.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImageResizerService.Worker
{
    public class Worker : BackgroundService
    {

        //public const string LINK = "C:/Users/Alexander/Desktop/ConvertedImages";
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

            photos.ForEach(p => p.PhotoStatus = PhotoStatus.Readed);
            await PhotoProvider.SaveChangesAsync();

        }

        private async Task resizePhoto(Photo photo)
        {
            using (var stream = File.OpenRead(photo.Path + photo.Name))
            {
                var file = Image.Load(stream);

                foreach (var type in FormatOptimizer.GetFormats(file))
                {
                        var convertedImage = resizeImage(file, new Size(Convert.ToInt32(type.Width), Convert.ToInt32(type.Height)));
                        var path = $@"{photo.Path}/ResizedImages/X{type.Width.ToString() + type.Height.ToString()}/";

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        await convertedImage.SaveAsync(path + photo.Name);
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
