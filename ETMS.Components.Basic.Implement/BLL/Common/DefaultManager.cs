
using ETMS.Components.Basic.Implement.DAL.Common;
namespace ETMS.Components.Basic.Implement.BLL.Common
{
    /// <summary>
    /// 关联关系数据管理的默认实现类
    /// </summary>
    public abstract class DefaultManager : IManager
    {
        private IDataAccess m_DAL;
        protected IDataAccess DAL
        {
            get
            {
                return m_DAL;
            }
        }

        public DefaultManager(ETMS.AppContext.IObject manager, IDataAccess dal)
        {
            this.m_Manager = manager;
            this.m_DAL = dal;
        }

        #region IManager 成员

        private ETMS.AppContext.IObject m_Manager;
        public ETMS.AppContext.IObject Manager
        {
            get
            {
                return this.m_Manager;
            }
            set
            {
                this.m_Manager = value;
            }
        }

        public void Associate(ETMS.AppContext.IObject member)
        {
            doAssociate(member);
            DAL.Add(member);
        }
        protected abstract void doAssociate(ETMS.AppContext.IObject member);

        public void ReleaseAssociate(ETMS.AppContext.IObject member)
        {
            doReleaseAssociate(member);
            DAL.Delete(member);
        }

        protected virtual void doReleaseAssociate(ETMS.AppContext.IObject member) { }

        public virtual ETMS.AppContext.IObject[] GetAllMembers(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                filter = "";
            filter = string.Format(" AND ({0}='{1}' {2})", this.m_Manager.PK.Key, this.m_Manager.PK.Value, filter);
            return (ETMS.AppContext.IObject[])DAL.Query(filter);
        }

        public virtual ETMS.AppContext.IObject[] GetAllMembers(int pageIndex, int pageSize, string filter, string orderBy, out int recordCount)
        {
            if (string.IsNullOrEmpty(filter))
                filter = "";
            filter = string.Format(" AND ({0}='{1}' {2})", this.m_Manager.PK.Key, this.m_Manager.PK.Value, filter);

            return (ETMS.AppContext.IObject[])DAL.Query(pageIndex, pageSize, filter, orderBy, out recordCount);
        }

        public virtual ETMS.AppContext.IObject GetMemberByPkValue(ETMS.AppContext.IObject pk)
        {
            return (ETMS.AppContext.IObject)DAL.Query(pk.PK.Value);
        }
        #endregion
    }
}
