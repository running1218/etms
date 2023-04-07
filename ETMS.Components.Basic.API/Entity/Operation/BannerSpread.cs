using ETMS.AppContext;
using System;

namespace ETMS.Components.Basic.API.Entity.Operation
{
    /// <summary>
    /// banner图
    /// </summary>
    [Serializable]
    public class BannerSpread: AbstractObject
    {
        public BannerSpread() { }

        #region Properties

        /// <summary>
        /// Banner推广ID
        /// <summary>
        public Guid BannerSpreadID { get; set; }

        /// <summary>
        /// 推广名称
        /// <summary>
        public String SpreadName { get; set; }

        /// <summary>
        /// Web端推广链接
        /// <summary>
        public String SpreadPCLink { get; set; }

        /// <summary>
        /// 移动端推广链接
        /// <summary>
        public String SpreadMobileLink { get; set; }
        /// <summary>
		/// SEO关键字
		/// <summary>
		public String KeyWords { get; set; }

        /// <summary>
        /// PC图片名 
        /// <summary>
        public String PCImageName { get; set; }

        /// <summary>
        /// PC图片路径
        /// <summary>
        public String PCImagePath { get; set; }
        /// <summary>
        /// 移动端图片名
        /// <summary>
        public String MobileImageName { get; set; }

        /// <summary>
        /// 移动端图片路径 
        /// <summary>
        public String MobileImagePath { get; set; }
      

        /// <summary>
        /// 状态：0-停用；1-启用；
        /// <summary>
        public Int16? ReleaseStatus { get; set; }

        /// <summary>
        /// 顺序
        /// <summary>
        public Int32? Order { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public Int32 OrgID { get; set; }

        /// <summary>
        /// 创建人ID
        /// <summary>
        public int? Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// <summary>
        public int? Modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// <summary>
        public DateTime? ModifyTime { get; set; }

        #endregion Properties

        #region Override

        public override string DefaultKeyName
        {
            get { return "BannerSpreadID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.BannerSpreadID;
            }
            set
            {
                this.BannerSpreadID = (Guid)value;
            }
        }

        #endregion override
    }
    /// <summary>
    /// Banner实体类
    /// </summary>
    public class Banner
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { set; get; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link { set; get; }

    }
}
