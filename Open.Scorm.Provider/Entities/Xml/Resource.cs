using System.Xml.Serialization;

namespace Open.Scorm.Entities
{
    public class Resource
    {
        public Resource() { }

        private string identifier;
        [XmlAttribute("identifier")]
        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        private ResourceType type;
        [XmlAttribute("type")]
        public ResourceType Type
        {
            get { return type; }
            set { type = value; }
        }

        private string href;
        [XmlAttribute("href")]
        public string Href
        {
            get { return href; }
            set { href = value; }
        }

        private ScormType scormtype;
        [XmlAttribute("scormtype", Namespace = Declarations.Adlcp)]
        public ScormType Scormtype
        {
            get { return scormtype; }
            set { scormtype = value; }
        }

        private FileCollection file;
        [XmlElement(Type = (typeof(File)), ElementName = "file")]
        public FileCollection File
        {
            get { return file; }
            set { file = value; }
        }
    }
}
