using System.Collections.Generic;
using System.ComponentModel;
namespace FotoConvector.Domen
{
    public class PhotoType
    {

        [DefaultValue("")]
        public List<string> formatNames { get; set; }
        

        public PhotoType(List<string> formatNames)
        {
            this.formatNames = formatNames;
            
            formatNames.Add("X192X184");
            formatNames.Add("X168X168");

            formatNames.Add("X128X168");
            formatNames.Add("X112X112");

            formatNames.Add("X80X80");
            formatNames.Add("X72X96");

            formatNames.Add("X72X88");
            formatNames.Add("X56X56");

            formatNames.Add("X48X48");
            formatNames.Add("X40X40");
        }
    }

}
