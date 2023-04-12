using System;
using System.Xml.Serialization;

namespace Open.Scorm.Entities
{
    public class InitScorm
    {

    }

    internal struct Declarations
    {
        public const string Namespace = "http://www.imsproject.org/xsd/imscp_rootv1p1p2";
        public const string Xsi = "http://www.w3.org/2001/XMLSchema-instance";
        public const string Adlcp = "http://www.adlnet.org/xsd/adlcp_rootv1p2";
    }

    [Serializable]
    public enum ScormType
    {
        [XmlEnum(Name = "asset")]
        asset,
        [XmlEnum(Name = "sco")]
        sco
    }

    [Serializable]
    public enum StructureType
    {
        [XmlEnum(Name = "hierarchical")]
        hierarchical,
    }

    [Serializable]
    public enum ResourceType
    {
        [XmlEnum(Name="webcontent")]
        webcontent,
    }
}
