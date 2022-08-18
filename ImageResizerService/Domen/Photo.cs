using ImageResizerService.Domen.Enum;
using Microsoft.AspNetCore.Http;

namespace ImageResizerService.Domen
{
    public class Photo : PersistentObject
    {
        public string Name { get; set; }
        public PhotoStatus PhotoStatus { get; set; } 
        public string Path { get; set; }

    }
}
