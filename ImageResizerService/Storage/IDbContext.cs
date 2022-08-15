﻿using ImageResizerService.Domen;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ImageResizerService.Storage
{
    public interface IDbContext
    {
        DbSet<Domen.Task> Tasks { get; set; }

        DbSet<Photos> Photos { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
