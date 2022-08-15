using ImageResizerService.Domen;
using ImageResizerService.Providers.Repository;
using ImageResizerService.Repository.Interfaces;
using ImageResizerService.Storage;
using Microsoft.Extensions.Caching.Distributed;

namespace ImageResizerService.Repository.Providers
{
    public class PhotoProvider : Repository<Photo>, IPhotoProvider
    {
        public PhotoProvider(AppDbContext context)
            : base(context)
        { 
        }
    }
}
