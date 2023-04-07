using System;
using System.Web.UI.WebControls;
namespace ETMS.Controls
{
    /// <summary>
    /// ��ͼģʽö�ٶ���
    /// </summary>
    public enum ViewMode
    {
        /// <summary>
        /// ������ͼ
        /// </summary>
        Manage = 0,
        /// <summary>
        /// �༭��ͼ
        /// </summary>
        Edit = 1,
        /// <summary>
        /// �����ͼ
        /// </summary>
        Browse = 2,
        /// <summary>
        /// �б���ͼ
        /// </summary>
        List = 3
    }

    /// <summary>
    /// ����ͼ�ؼ�����ӿڶ���
    /// </summary>
    public interface IMuliViewControl
    {
        /// <summary>
        /// ��ģ��
        /// ע�⣺�˽ӿ�Ϊϵͳ���ã��������û����ã�
        /// </summary>
        Object DomainModel { get;}

        /// <summary>
        /// �ؼ�ģʽ{0������ģʽ��1���༭ģʽ��2�����ģʽ��Ĭ�ϣ�}
        /// </summary>
        ViewMode ControlMode { get;}

        /// <summary>
        /// ��DomainMode�󶨵�������ͼ�µĿؼ�
        /// </summary>
        /// <param name="domainModel"></param>
        void BindFromData_Manage(object domainModel);

        /// <summary>
        /// ��DomainMode�󶨵��б���ͼ�µĿؼ�
        /// </summary>
        /// <param name="domainModel"></param>
        void BindFromData_List(object domainModel);

        /// <summary>
        ///  ��DomainMode�󶨵��༭��ͼ�µĿؼ�
        /// </summary>
        /// <param name="domainModel"></param>
        void BindFromData_Edit(object domainModel);

        /// <summary>
        ///  ��DomainMode�󶨵������ͼ�µĿؼ�
        /// </summary>
        /// <param name="domainModel"></param>
        void BindFromData_Browse(object domainModel);

        /// <summary>
        /// ���༭��ͼ�µĿؼ���Ϣ�󶨵�DomainMode
        /// </summary>
        /// <returns></returns>
        object UnBindFromData(object domainModel);

        /// <summary>
        /// �����ݰ󶨵�ָ��ģʽ�µĿؼ�
        /// </summary>
        /// <param name="domainModel">����ģ��</param>
        /// <param name="controlMode">ָ��ģʽ</param>
        void BindFromData(object domainModel, ViewMode controlMode);
    }

    /// <summary>
    /// ����ͼ�ؼ�ί�ɹ���
    /// �ڲ���װ�˶���ͼ�ؼ�������߼�
    /// </summary>
    public class CustomMuliView : MultiView, IMuliViewControl
    {

        #region IMuliViewControl ��Ա

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
        /// �ؼ�ģʽ{0������ģʽ��1���༭ģʽ��2�����ģʽ}
        /// </summary>
        public ViewMode ControlMode
        {
            get
            {
                if (ViewState["ControlMode"] == null)
                {
                    throw new Exception("δ�趨�ؼ�ģʽ��");
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
            if (this.ControlMode == ViewMode.Browse)//���ģʽ��Ϊ�����޸����ݣ���ˣ���������
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
                throw new Exception("��ȷ��ҳ����û��ؼ�ʵ��IMuliViewControl�ӿڣ�");
            }
        }
        private void BindFromData()
        {
            //������ͼ����Ĭ�Ϲ��򣺰�����ͼID����ƥ��
            for (int i = 0; i < this.Views.Count; i++)
            {
                if (this.Views[i].ID.IndexOf(this.ControlMode.ToString(), StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    this.ActiveViewIndex = i;
                    break;
                }
            }
            //�����ͼ���������չ���������ͼģʽö��ֵ��ȡ��
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
                throw new Exception("��ȷ��ҳ����û��ؼ�ʵ��IMuliViewControl�ӿڣ�");
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
                throw new Exception("��ȷ��ҳ����û��ؼ�ʵ��IMuliViewControl�ӿڣ�");
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
                throw new Exception("��ȷ��ҳ����û��ؼ�ʵ��IMuliViewControl�ӿڣ�");
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
                throw new Exception("��ȷ��ҳ����û��ؼ�ʵ��IMuliViewControl�ӿڣ�");
            }      
        }
        #endregion
    }
}
