﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FotoConvector.Domen
{
    public class PhotoType
    {
        private static List<PhotoType> Types;

        public static async Task<List<PhotoType>> getTypes()
        {
            if (Types == null || Types.Count == 0)
                Types = await fillTypes();
            return Types;
        }

        public static async Task<PhotoType> getFirst()
        {
            if (Types == null || Types.Count == 0)
                Types = await fillTypes();
            return Types.First();
        }

        public double Width { get; }
        public double Height { get; }

        public PhotoType(double width, double height)
        {
            Width = width;
            Height = height;
        }

        private static async Task<List<PhotoType>> fillTypes()
        {
            var types = new List<PhotoType>();
            for(int i = 2; i < 16; i+=2)
                types.Add(new PhotoType(i* 32, i * 32));

            return types;
        }
    }
}
