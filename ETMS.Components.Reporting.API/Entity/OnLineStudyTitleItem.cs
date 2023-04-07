using System.Xml.Serialization;

namespace ETMS.Components.Reporting.API.Entity
{
    public class OnLineStudyTitleItem
    {
        private int indexNumber;
        [XmlAttribute("indexNumber")]
        public int IndexNumber
        {
            get { return indexNumber; }
            set { indexNumber = value; }
        }

        private string titleKey;
        [XmlAttribute("titleKey")]
        public string TitleKey
        {
            get { return titleKey; }
            set { titleKey = value; }
        }

        private string titleValue;
        [XmlAttribute("titleValue")]
        public string TitleValue
        {
            get { return titleValue; }
            set { titleValue = value; }
        }
    }
}
