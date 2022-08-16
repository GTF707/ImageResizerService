using System;
using ImageResizerService.Domen.Enum;

namespace ImageResizerService.Domen
{
    public class Tasks : PersistentObject
    {
        public string Name { get; set; }

       public TaskType TaskType { get; set; }

        public Tasks()
        {
        }
    }
}

