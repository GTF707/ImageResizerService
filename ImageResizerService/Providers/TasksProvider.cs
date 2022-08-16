using System;
using ImageResizerService.Domen;
using ImageResizerService.Providers.Repository;
using ImageResizerService.Storage;

namespace ImageResizerService.Providers
{
    public class TasksProvider : Repository<Tasks>, ITasksProvider
    {
        public TasksProvider(AppDbContext context) : base(context)
        {
        }
    }
}

