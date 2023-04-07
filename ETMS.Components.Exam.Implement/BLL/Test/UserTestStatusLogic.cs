using System;
using Autumn.Objects.Factory;
using Autumn.Context;

using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.Implement.DAL.Test;

namespace ETMS.Components.Exam.Implement.BLL.Test
{
    /// <summary>
    /// 考生答卷中答题的逻辑实现
    /// </summary>
    public class UserTestStatusLogic : IMessageSourceAware, IInitializingObject
    {
        #region --错误代码--
        private static string Err_TestFeedback_Not_Found = "ItemBank.OptionGroup.Not.Found";
        private static string Err_TestFeedback_Data_Invalid = "ItemBank.OptionGroup.Data.Invalid";
        private static string Err_UserExamResult_Instance_Invalid = "ItemBank.UserExamResult.Instance.Invalid";
        private static string Err_TestFeedback_New_Invalid = "ItemBank.OptionGroup.New.Invalid";
        #endregion

        public IUserExamDao UserExamDao { get; set; }

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (UserExamDao == null)
            {
                throw new Exception("please set TestFeedbackDao Property First!");
            }
        }

        #endregion

        #region IMessageSourceAware 成员

        public IMessageSource MessageSource
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 得到考生某一答卷状态
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <returns></returns>
        public UserTestStatusType GetTestStatusType(Guid UserExamID)
        {
            return UserExamDao.GetTestStatusType(UserExamID);
        }
        /// <summary>
        /// 更新考生某一答卷状态
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="NewTestStatusType"></param>
        public void UpdateTestStatusType(Guid UserExamID, UserTestStatusType NewTestStatusType)
        {
            UserExamDao.UpdateTestStatusType(UserExamID, NewTestStatusType);
        }

        /// <summary>
        /// 更新考生某一答卷状态 add 2013-8-30 hjy
        /// </summary>
        /// <param name="UserExamID"></param>
        /// <param name="NewTestStatusType"></param>
        public void UpdateUserScoreOver(Guid UserExamID, UserTestStatusType NewTestStatusType)
        {
            UserExamDao.UpdateUserScoreOver(UserExamID, NewTestStatusType);
        }

        private bool ValidIsInitialized()
        {
            //if (this.UserExamID == null || this.UserExamID == Guid.Empty)
            //{
            //    return false;
            //}

            return true;
        }
        /// <summary>
        /// 检查实例中数据是否完整，不完整直接抛出异常。
        /// </summary>
        private void ThrowNotInitializedExeception()
        {
            bool bIsInit = this.ValidIsInitialized();
            if (!bIsInit)
            {
                throw new ETMS.AppContext.BusinessException(Err_UserExamResult_Instance_Invalid,
                    new Exception("未正确加载数据，请正确加载试题选项数据加载"));
            }
        }
    }
}
