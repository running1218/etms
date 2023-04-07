using System;
using System.Xml.Serialization;

namespace ETMS.Components.Reporting.API.Entity
{
    [XmlRoot("titleItem"), Serializable]
    public class OnlineStudyTitle
    {
        private ItemCollection item;
        [XmlElement(Type = (typeof(OnLineStudyTitleItem)), ElementName = "item", IsNullable = false)]
        public ItemCollection Item
        {
            get { return item; }
            set { item = value; }
        }
    }
}
