namespace University.Mooc.AppContext.Component
{
    /// <summary>
    /// 组件定义
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// 分组ID（业务归类相似的一组组件）
        /// </summary>
        string GroupID { get; }

        /// <summary>
        /// 组件ID（组件库内要求唯一）
        /// </summary>
        string ID { get; }

        /// <summary>
        /// 组件名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 组件描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 同组内顺序号(由小到大顺序排列)
        /// </summary>
        int OrderNo { get; }

        /// <summary>
        /// 管理应用首页
        /// </summary>
        string ManageAppHome { get; set; }

        /// <summary>
        /// 列表查看-应用首页
        /// </summary>
        string ListAppHome { get; set; }

        /// <summary>
        /// 是否初始化
        /// </summary>
        bool IsInited { get; }
        
        ///// <summary>
        ///// 组件设置信息
        ///// </summary>
        //IList<ComponentSetting> Settings { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        void Init(AppContext.ApplicationContext context);
    }
}
