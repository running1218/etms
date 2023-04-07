using System.Runtime.Serialization;

namespace ETMS.Components.StudyClass.API.Entity.StudyClass
{
    [DataContract]
    public class DocumentImage
    {
        /// <summary>
        /// 原图地址
        /// </summary>
        [DataMember(Name = "img", Order = 0)]
        public string ImageUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 缩略图地址
        /// </summary>
        [DataMember(Name = "thumb", Order = 1)]
        public string ThumbnailUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 说明
        /// </summary>
        [DataMember(Name = "caption", Order = 2)]
        public string Description
        {
            get;
            set;
        }
    }
}
