using ETMS.AppContext.Component;

namespace ETMS.Components.Basic.API
{

    public interface IPublicFacade : IComponent
    {
        /// <summary>
        /// 获取机构编码
        /// </summary>
        string GetOrgCodeByID(int OrganizationID);
        
    }
}
