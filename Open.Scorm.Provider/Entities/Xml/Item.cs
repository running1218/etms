using System.Xml.Serialization;

namespace Open.Scorm.Entities
{
    public class Item
    {
        private string identifier;
        [XmlAttribute("identifier")]
        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        private bool isVisible;
        [XmlAttribute("isvisible")]
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        private string identifierref;
        [XmlAttribute("identifierref")]
        public string IdentifierRef
        {
            get { return identifierref; }
            set { identifierref = value; }
        }

        private Title title;
        [XmlElement("title")]
        public Title Title
        {
            get { return title; }
            set { title = value; }
        }

        private ItemCollection _item;
        [XmlElement(Type = (typeof(Item)), ElementName = "item")]
        public ItemCollection Items
        {
            get { return _item; }
            set { _item = value; }
        }
    }
}
