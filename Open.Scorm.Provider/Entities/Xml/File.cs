using System.Xml.Serialization;

namespace Open.Scorm.Entities
{
    public class File  
    {
        public File(){ }

        private string href;
        [XmlAttribute("href")]
        public string Href
        {
            get { return href; }
            set { href = value; }
        }
    }
}
