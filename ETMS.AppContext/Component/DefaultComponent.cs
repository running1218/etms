namespace ETMS.AppContext.Component
{
    /// <summary>
    /// 默认组件实现
    /// </summary>
    public class DefaultComponent : IComponent
    {

        public virtual string GroupID { get; set; }

        public virtual string ID { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual int OrderNo { get; set; }

        public virtual bool IsInited { get; set; }

        public virtual string ManageAppHome { get; set; }

        public virtual string ListAppHome { get; set; }
        //public virtual IList<ComponentSetting> Settings { get; set; }

        public void Init(ApplicationContext context)
        {
            doInit(context);
            //设置已经初始化标志
            this.IsInited = true;
        }
        public virtual void doInit(ApplicationContext context)
        {
        }
    }
}
