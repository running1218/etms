//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-3-30 14:26:51.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================


namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// ѧԱ��Ϣ(�û���չ��)ҵ��ʵ��
    /// </summary>
    public partial class Site_Student : User
    {
        /// <summary>
        /// copy�û�������Ϣ
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