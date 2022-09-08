using System.Collections.Generic;

namespace ImageResizerService.DTO
{
    public class ResizeTaskRequest
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public List<string> NamesList { get; set; }

        public ResizeTaskRequest()
        {

        }

        public ResizeTaskRequest(string path, string name)
        {
            Path = path;
            Name = name;
        }

        public ResizeTaskRequest(string name)
        {
            Name = name;
        }

        public ResizeTaskRequest(string path, List<string> namesList)
        {
            Path = path;
            NamesList = namesList;
        }
    }
}
