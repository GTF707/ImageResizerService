using System.Collections.Generic;
using System.ComponentModel;
namespace FotoConvector.Domen
{
    /// <summary>
    /// 
    /// </summary>
    public class PhotoType
    {
        /// <summary>
        /// Photo types
        /// </summary>
        public static List<PhotoType> Types
        {
            get {
                if (Types == null || Types.Count == 0)
                    fillTypes(Types);
                return Types;
            } 
        }
        public double Width { get; }
        public double Height { get; }

        private PhotoType(double width, double height)
        {
            Width = width;
            Height = height;
        }

        private static void fillTypes(List<PhotoType> types)
        {
            types = new List<PhotoType>();
            for(int i = 2; i < 16; i+=2)
                types.Add(new PhotoType(i* 32, i * 32));

        }
    }

}
