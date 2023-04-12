using System.Xml.Serialization;

namespace Open.Scorm.Entities
{
    public class Organization
    {
        public Organization() { }

        private string identifier;
        [XmlAttribute("identifier")]
        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        private StructureType structure;
        [XmlAttribute("structure")]
        public StructureType Structure
        {
            get { return structure; }
            set { structure = value; }
        }

        private Title title;
        [XmlElement("title")]
        public Title Title
        {
            get { return title; }
            set { title = value; }
        }

        private ItemCollection item;
        [XmlElement(Type = (typeof(Item)), ElementName = "item")]
        public ItemCollection Item
        {
            get { return item; }
            set { item = value; }
        }
    }
}
