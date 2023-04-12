using System.Xml.Serialization;

namespace Open.Scorm.Entities
{
    public class Title 
    {
        public Title() { }

        private string originalValue;
        [XmlText]
        public string OriginalValue
        {
            get { return this.originalValue; }
            set { this.originalValue = value; }
        }
    }
}
