using System;
using System.Web.UI.WebControls;
namespace ETMS.Controls
{
    /// <summary>
    /// 视图模式枚举定义
    /// </summary>
    public enum ViewMode
    {
        /// <summary>
        /// 管理视图
        /// </summary>
        Manage = 0,
        /// <summary>
        /// 编辑视图
        /// </summary>
        Edit = 1,
        /// <summary>
        /// 浏览视图
        /// </summary>
        Browse = 2,
        /// <summary>
        /// 列表视图
        /// </summary>
        List = 3
    }

    /// <summary>
    /// 多视图控件抽象接口定义
    /// </summary>
    public interface IMuliViewControl
    {
        /// <summary>
        /// 域模型
        /// 注意：此接口为系统调用，不允许用户调用！
        /// </summary>
        Object DomainModel { get;}

        /// <summary>
        /// 控件模式{0：管理模式；1：编辑模式；2：浏览模式（默认）}
        /// </summary>
        ViewMode ControlMode { get;}

        /// <summary>
        /// 将DomainMode绑定到管理视图下的控件
        /// </summary>
        /// <param name="domainModel"></param>
        void BindFromData_Manage(object domainModel);

        /// <summary>
        /// 将DomainMode绑定到列表视图下的控件
        /// </summary>
        /// <param name="domainModel"></param>
        void BindFromData_List(object domainModel);

        /// <summary>
        ///  将DomainMode绑定到编辑视图下的控件
        /// </summary>
        /// <param name="domainModel"></param>
        void BindFromData_Edit(object domainModel);

        /// <summary>
        ///  将DomainMode绑定到浏览视图下的控件
        /// </summary>
        /// <param name="domainModel"></param>
        void BindFromData_Browse(object domainModel);

        /// <summary>
        /// 将编辑视图下的控件信息绑定到DomainMode
        /// </summary>
        /// <returns></returns>
        object UnBindFromData(object domainModel);

        /// <summary>
        /// 将数据绑定到指定模式下的控件
        /// </summary>
        /// <param name="domainModel">数据模型</param>
        /// <param name="controlMode">指定模式</param>
        void BindFromData(object domainModel, ViewMode controlMode);
    }

    /// <summary>
    /// 多视图控件委派工具
    /// 内部封装了多视图控件必须的逻辑
    /// </summary>
    public class CustomMuliView : MultiView, IMuliViewControl
    {

        #region IMuliViewControl 成员

        private object InnerDomainModel
        {
            get
            {
                return (object)ViewState["InnerDomainModel"];
            }
        }
        public Object DomainModel
        {
            get
            {
                return this.UnBindFromData(this.InnerDomainModel);
            }
            //set
            //{
            //    this.m_DomainModel = value;
            //}
        }
        /// <summary>
        /// 控件模式{0：管理模式；1：编辑模式；2：浏览模式}
        /// </summary>
        public ViewMode ControlMode
        {
            get
            {
                if (ViewState["ControlMode"] == null)
                {
                    throw new Exception("未设定控件模式！");
                }
                return (ViewMode)ViewState["ControlMode"];
            }
            //set
            //{
            //    if (ViewState["ControlMode"] == null || (ViewMode)ViewState["ControlMode"] != value)
            //    {
            //        ViewState["ControlMode"] = value;
            //        this.BindFromData();
            //    }
            //}
        }
        public object UnBindFromData(object domainModel)
        {
            if (this.ControlMode == ViewMode.Browse)//浏览模式因为不会修改数据，因此，返回自身。
                return domainModel;

            if (this.Page is IMuliViewControl)
            {
                return ((IMuliViewControl)this.Page).UnBindFromData(domainModel);
            }
            else if (this.Parent is IMuliViewControl)
            {
                return ((IMuliViewControl)this.Parent).UnBindFromData(domainModel);
            }
            else
            {
                throw new Exception("请确保页面或用户控件实现IMuliViewControl接口！");
            }
        }
        private void BindFromData()
        {
            //激活视图规则，默认规则：按照视图ID进行匹配
            for (int i = 0; i < this.Views.Count; i++)
            {
                if (this.Views[i].ID.IndexOf(this.ControlMode.ToString(), StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    this.ActiveViewIndex = i;
                    break;
                }
            }
            //如果视图命名不按照规则，则按照视图模式枚举值来取！
            if (this.ActiveViewIndex == -1)
            {
                this.ActiveViewIndex = (int)this.ControlMode;
            }
            switch (this.ControlMode)
            {
                case ViewMode.Manage:
                    this.BindFromData_Manage(this.InnerDomainModel);
                    break;
                case ViewMode.Edit:
                    this.BindFromData_Edit(this.InnerDomainModel);
                    break;
                case ViewMode.Browse:
                    this.BindFromData_Browse(this.InnerDomainModel);
                    break;
                case ViewMode.List:
                    this.BindFromData_List(this.InnerDomainModel);
                    break;
            }
        }

        public void BindFromData(object domainModel, ViewMode controlMode)
        {
            ViewState["InnerDomainModel"] = domainModel;
            ViewState["ControlMode"] = controlMode;
            //if (ViewState["ControlMode"] == null || (ViewMode)ViewState["ControlMode"] != controlMode)
            //{

            //}
            this.BindFromData();
        }
        public void BindFromData_Manage(object domainModel)
        {            
            if (this.Page is IMuliViewControl)
            {
                ((IMuliViewControl)this.Page).BindFromData_Manage(domainModel);    
            }
            else if (this.Parent is IMuliViewControl)
            {
                ((IMuliViewControl)this.Parent).BindFromData_Manage(domainModel);
            }
            else
            {
                throw new Exception("请确保页面或用户控件实现IMuliViewControl接口！");
            }
        }

        public void BindFromData_List(object domainModel)
        {
            if (this.Page is IMuliViewControl)
            {
                ((IMuliViewControl)this.Page).BindFromData_List(domainModel);
            }
            else if (this.Parent is IMuliViewControl)
            {
                ((IMuliViewControl)this.Parent).BindFromData_List(domainModel);
            }
            else
            {
                throw new Exception("请确保页面或用户控件实现IMuliViewControl接口！");
            }          
        }
        public void BindFromData_Edit(object domainModel)
        {
            if (this.Page is IMuliViewControl)
            {
                ((IMuliViewControl)this.Page).BindFromData_Edit(domainModel);
            }
            else if (this.Parent is IMuliViewControl)
            {
                ((IMuliViewControl)this.Parent).BindFromData_Edit(domainModel);
            }
            else
            {
                throw new Exception("请确保页面或用户控件实现IMuliViewControl接口！");
            }              
        }
        public void BindFromData_Browse(object domainModel)
        {
            if (this.Page is IMuliViewControl)
            {
                ((IMuliViewControl)this.Page).BindFromData_Browse(domainModel);
            }
            else if (this.Parent is IMuliViewControl)
            {
                ((IMuliViewControl)this.Parent).BindFromData_Browse(domainModel);
            }
            else
            {
                throw new Exception("请确保页面或用户控件实现IMuliViewControl接口！");
            }      
        }
        #endregion
    }
}
