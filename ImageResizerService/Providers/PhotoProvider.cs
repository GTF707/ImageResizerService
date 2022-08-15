using ImageResizerService.Domen;
using ImageResizerService.Repository.Interfaces;
using ImageResizerService.Storage;
using Microsoft.Extensions.Caching.Distributed;

namespace ImageResizerService.Repository.Providers
{
    public class PhotoProvider : IPhotoProvider
    {
        private readonly AppDbContext context;

        public PhotoProvider(AppDbContext context)
        {
            this.context = context;
        }

        public void Create(Photo item)
        {
            context.Set<Photo>().Add(item);
        }
    }
}
