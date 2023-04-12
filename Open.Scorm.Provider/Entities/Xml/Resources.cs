using System.Xml.Serialization;

namespace Open.Scorm.Entities
{
    public class Resources
    {
        public Resources() { }

        private ResourceCollection resource;
        [XmlElement(Type = (typeof(Resource)), ElementName = "resource")]
        public ResourceCollection Resource
        {
            get { return resource; }
            set { resource = value; }
        }
    }
}
