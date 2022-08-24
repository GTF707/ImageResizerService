using ImageResizerService.Domen.Enum;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ImageResizerService.DTO
{
    public class ResponceFormatDto
    {
        public List<FormatType> Sizes { get; set; }
        public string FileName { get; set; }

        public ResponceFormatDto(List<FormatType> sizes, string fileName)
        {
            Sizes = sizes;
            FileName = fileName;
        }
    }
}
