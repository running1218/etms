using System;

namespace ETMS.Utility.Service.Notify
{

    /// <summary>
    /// 提醒消息类型
    /// </summary>
    [Serializable]
    public abstract class NotifyMessageType
    {
        /// <summary>
        /// 用户注册后-激活通知
        /// </summary>
        public const string User_Register_Activate = "User_Register_Activate";

        /// <summary>
        /// 用户忘记密码-重置通知
        /// </summary>
        public const string User_ForgetPassword_Reset = "User_ForgetPassword_Reset";

        /// <summary>
        /// 学付宝资金账户激活-激活码发送
        /// </summary>
        public const string Epay_ActiveAccount = "Epay_ActiveAccount";

        /// <summary>
        /// 学付宝资金账户-找回支付密码
        /// </summary>
        public const string Epay_ReBackPassword = "Epay_ReBackPassword";

        /// <summary>
        /// 统一用户认证-手机绑定
        /// </summary>
        public const string SSO_Account_BindMobile = "SSO_Account_BindMobile";

        /// <summary>
        /// 统一用户认证-邮箱激活
        /// </summary>
        public const string SSO_Account_EmailActivate = "SSO_Account_EmailActivate";

        /// <summary>
        /// 统一用户认证-通过手机重置登陆密码
        /// </summary>
        public const string SSO_Account_ResetPasswordByMobile = "SSO_Account_ResetPasswordByMobile";
        /// <summary>
        /// 统一用户认证-通过邮箱重置登陆密码
        /// </summary>
        public const string SSO_Account_ResetPasswordByEmail = "SSO_Account_ResetPasswordByEmail";
        /// <summary>
        /// 已购课程赠送激活码-激活码发送
        /// </summary>
        public const string Order_SendOrderProductAsGiftActiveCode = "Order_SendOrderProductAsGiftActiveCode";
    }
}