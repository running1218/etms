using System.Xml.Serialization;

namespace Open.Scorm.Entities
{
    public class Organizations
    {
        public Organizations() { }

        private string defaultValue;
        [XmlAttribute("default")]
        public string Default
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        private OrganizationCollection organization;
        [XmlElement(Type = (typeof(Organization)), ElementName = "organization")]
        public OrganizationCollection Organization
        {
            get { return organization; }
            set { organization = value; }
        }
    }
}
