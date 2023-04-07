namespace ETMS.Components.Basic.API.Entity.Bulletin
{
    /// <summary>
    /// 公告相关枚举
    /// </summary>
    public enum BulletinEnum
    { 
      
    }

    /// <summary>
    /// 公告类别枚举
    /// </summary>
    public enum BulletinTypeEnum
    {
        /// <summary>
        /// 公告
        /// </summary>
        Builletin = 1,

        /// <summary>
        /// 项目课程公告
        /// </summary>
        MentorData = 2,
        /// <summary>
        /// 项目课程导学资料
        /// </summary>
        CourseNotice =4,
    }

    /// <summary>
    /// 公告状态枚举
    /// </summary>
    public enum BulletinStatusEnum
    {
        /// <summary>
        /// 公告
        /// </summary>
        CanUse = 1,

        /// <summary>
        /// 导学
        /// </summary>
        NotCanUse= 2,
    }

}
