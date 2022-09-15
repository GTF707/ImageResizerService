using System.Collections.Generic;

namespace ImageResizerService.DTO
{
    public class ResizeAllTasksRequest
    {
        public string Path { get; set; }
        public List<string> NamesList { get; set; }

        public ResizeAllTasksRequest()
        {

        }

        public ResizeAllTasksRequest(string path, List<string> namesList)
        {
            Path = path;
            NamesList = namesList;

        }
    }
}
