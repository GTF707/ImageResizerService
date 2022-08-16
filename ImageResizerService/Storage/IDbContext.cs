using ImageResizerService.Domen;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ImageResizerService.Storage
{
    public interface IDbContext
    {
        DbSet<Domen.Tasks> Tasks { get; set; }
        DbSet<Photo> Photos { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
       
       
    }
}
