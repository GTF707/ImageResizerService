using System.Collections.Generic;

namespace ImageResizerService.DTO
{
    public class ResizeTaskRequest
    {
        public string Path { get; set; }
        public string Name { get; set; }

        public ResizeTaskRequest()
        {

        }

        public ResizeTaskRequest(string path, string name)
        {
            Path = path;
            Name = name;
        }

    }
}
