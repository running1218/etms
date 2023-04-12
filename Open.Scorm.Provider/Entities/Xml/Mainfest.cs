using System;
using System.Xml.Serialization;

namespace Open.Scorm.Entities
{
    [XmlRoot("manifest", Namespace = Declarations.Namespace), Serializable]
    public class Manifest
    {
        private string _identifier;
        [XmlAttribute("identifier")]
        public string Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        private OrganizationsCollection organizations;
        [XmlElement(Type = (typeof(Organizations)), ElementName = "organizations", IsNullable = false)]
        public OrganizationsCollection Organizations
        {
            get { return organizations; }
            set { organizations = value; }
        }

        private ResourcesCollection resources;
        [XmlElement(Type = typeof(Resources), ElementName = "resources")]
        public ResourcesCollection Resources
        {
            get { return resources; }
            set { resources = value; }
        }

        private string schemaLocation;
        [XmlAttribute("schemaLocation", Namespace = Declarations.Xsi)]
        public string SchemaLocation
        {
            get { return schemaLocation; }
            set { schemaLocation = value; }
        }
    }
}
