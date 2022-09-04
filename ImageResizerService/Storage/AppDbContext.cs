﻿using ImageResizerService.Domen;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageResizerService.Storage
{
    public class AppDbContext : DbContext, IDbContext
    {
        public DbSet<Photo> Photos { get; set; }
        

        public AppDbContext() : base()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=89187786606Alex;Database=ImageStorage");
            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=Ivan230691;Database=ImageStorage");
            //optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=Ivan230691!;Database=image_storage");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }

        public new IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }
    }
}
