
namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 学员信息(用户扩展表)业务实体
    /// </summary>
    public partial class Site_Student : User
    {
        /// <summary>
        /// copy用户基本信息
        /// </summary>
        /// <param name="userBaseInfo"></param>
        public void CopyUserBaseInfo(User userBaseInfo)
        {
            this.UserID = userBaseInfo.UserID;

            this.LoginName = userBaseInfo.LoginName;

            this.RealName = userBaseInfo.RealName;

            this.PassWord = userBaseInfo.PassWord;

            this.Email = userBaseInfo.Email;

            this.Telphone = userBaseInfo.Telphone;

            this.Description = userBaseInfo.Description;

            this.Status = userBaseInfo.Status;

            this.Creator = userBaseInfo.Creator;

            this.CreateTime = userBaseInfo.CreateTime;

            this.Modifier = userBaseInfo.Modifier;

            this.ModifyTime = userBaseInfo.ModifyTime;

            this.OrganizationID = userBaseInfo.OrganizationID;
            this.DepartmentID = userBaseInfo.DepartmentID;
            this.OfficeTelphone = userBaseInfo.OfficeTelphone;
            this.MobilePhone = userBaseInfo.MobilePhone;
            this.IsSysAccount = userBaseInfo.IsSysAccount;

            this.PhotoUrl = userBaseInfo.PhotoUrl;
            this.SexTypeID = userBaseInfo.SexTypeID;
            this.Birthday = userBaseInfo.Birthday;
            this.Identity = userBaseInfo.Identity;
            this.PoliticsTypeID = userBaseInfo.PoliticsTypeID;
            this.TitleName = userBaseInfo.TitleName;
        }
    }
}
