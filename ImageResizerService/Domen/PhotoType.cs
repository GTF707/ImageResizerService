using System.ComponentModel;
namespace FotoConvector.Domen
{
    public class PhotoType
    {

        [DefaultValue("")]
        public string X192184 { get; set; }

        [DefaultValue("")]
        public string X168168 { get; set; }

        [DefaultValue("")]
        public string X128168 { get; set; }

        [DefaultValue("")]
        public string X112112 { get; set; }

        [DefaultValue("")]
        public string X8080 { get; set; }

        public string X7296 { get; set; }

        [DefaultValue("")]
        public string X7288 { get; set; }

        [DefaultValue("")]
        public string X5656 { get; set; }

        [DefaultValue("")]
        public string X4848 { get; set; }

        [DefaultValue("")]
        public string X4040 { get; set; }

        public PhotoType(
            string x192184,
            string x168168,
            string x128168,
            string x112112, 
            string x8080, 
            string x7296,
            string x7288,
            string x5656,
            string x4848,
            string x4040)
        {
            X192184 = x192184;
            X168168 = x168168;
            X128168 = x128168;
            X112112 = x112112;
            X8080 = x8080;
            X7296 = x7296;
            X7288 = x7288;
            X5656 = x5656;
            X4848 = x4848;
            X4040 = x4040;
        }

    }

}
