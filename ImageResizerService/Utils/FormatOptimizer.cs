using FotoConvector.Domen;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

using System.Linq;

namespace ImageResizerService.Utils
{
    public class FormatOptimizer
    {

        public static List<PhotoType> GetFormats(SixLabors.ImageSharp.Image image)
        {
            List<PhotoType> photoTypes = new List<PhotoType>();
            foreach (var type in PhotoType.getTypes())
            {
                if (type.Width > image.Width || type.Height > image.Height)
                {
                    continue;
                }
                else
                {
                    photoTypes.Add(new PhotoType(type.Width, type.Height));
                }
            }
            return photoTypes;
        }

        public static List<string> GetStringFormats(Image image)
        {
            List<string> list = new List<string>();
            foreach (var type in PhotoType.getTypes())
            {
                if (type.Width > image.Width || type.Height > image.Height)
                {
                    continue;
                }
                else
                {
                    list.Add("W" + type.Width.ToString() + "_H" + type.Height.ToString());
                }
            }
            return list;
        }
    }
}
