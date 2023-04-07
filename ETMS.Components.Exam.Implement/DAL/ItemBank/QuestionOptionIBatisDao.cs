using System;
using System.Collections.Generic;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// 实现了对试题选项的数据访问
    /// </summary>
    public class QuestionOptionIBatisDao : ReadWriteDataMapperDaoSupport,IQuestionOptionDao
    {
        #region 添加和更新选项操作
        public void AddOption(QuestionOption questionOption)
        {
            var oValue = new { QuestionOption = questionOption, UserID = AppContext.UserContext.Current.UserID };

            DataMapperClient_Write.Insert("ItemBank.QuestionOption.Add", oValue);
        }

        public void Update(QuestionOption newQuestionOption)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("ItemBank.QuestionOption.Update",
                new
                {
                    QuestionOption = newQuestionOption,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }
        #endregion

        #region --删除选项操作--
        public void Delete(Guid questionOptionID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("ItemBank.QuestionOption.Delete",
                new
                {
                    QuestionOptionID =questionOptionID ,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }

        public void DeleteByQuestionID(Guid questionID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("ItemBank.QuestionOption.DeleteByQuestionID",
                new
                {
                    QuestionID = questionID,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }

        public void DeleteByGroupID(Guid questionID, Guid optionGroupID)
        {
            int nRowCnt = 0;
            nRowCnt =DataMapperClient_Write.Delete("ItemBank.QuestionOption.DeleteByGroupID",
                new
                {
                    QuestionID = questionID,
                    OptionGroupID = optionGroupID,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }

        /// <summary>
        /// 删除某一试题中多个选项
        /// </summary>
        /// <param name="questionID">选项所在试题ID</param>
        /// <param name="LstOptionsID">要删除的选项ID列表</param>
        public void DeleteByOptionsID(System.Guid questionID, IList<System.Guid> LstOptionsID)
        {
            if (LstOptionsID == null || LstOptionsID.Count <= 0)
                return;

            string sOptions = "";
            foreach (System.Guid guid in LstOptionsID)
            {
                sOptions += string.IsNullOrEmpty(sOptions) ? string.Format("'{0}'", guid.ToString()) : string.Format(",'{0}'", guid.ToString());
            }
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("ItemBank.QuestionOption.DeleteByOptionsID",
                new
                {
                    QuestionID = questionID,
                    OptionsID = sOptions,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }
        #endregion

        #region --获取选项--
        public QuestionOption GetQuestionOption(Guid questionOptionID)
        {
            QuestionOption QuestionOption=DataMapperClient_Read.QueryForObject<QuestionOption>(
                "ItemBank.QuestionOption.GetByID", questionOptionID);

            return QuestionOption;
        }

        public IList<QuestionOption> FindQuestionOptionsInQuestion(Guid questionID)
        {
            IList<QuestionOption> LstQuestionOptions = DataMapperClient_Read.QueryForList<QuestionOption>(
                "ItemBank.QuestionOption.FindQuestionOptionsInQuestion", questionID);

            return LstQuestionOptions;
        }

        public IList<QuestionOption> FindQuestionOptionsInGroup(Guid questionID, Guid optionGroupID)
        {
            IList<QuestionOption> LstQuestionOptions = DataMapperClient_Read.QueryForList<QuestionOption>(
                "ItemBank.QuestionOption.FindQuestionOptionsInGroup",
                new { QuestionID =questionID,OptionGroupID=optionGroupID });

            return LstQuestionOptions;
        }
        #endregion
    }
}
