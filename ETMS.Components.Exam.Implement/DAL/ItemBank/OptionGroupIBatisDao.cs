using System;
using System.Collections.Generic;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    /// <summary>
    /// 实现了对试题选项组的数据访问
    /// </summary>
    public class OptionGroupIBatisDao : ReadWriteDataMapperDaoSupport,IOptionGroupDao
    {
        #region IOptionGroupDao 成员

        public void AddOptionGroup(OptionGroup optionGroup)
        {
            var oValue = new { OptionGroup = optionGroup, UserID = AppContext.UserContext.Current.UserID };

            DataMapperClient_Write.Insert("ItemBank.OptionGroup.Add", oValue);
        }

        public void Update(OptionGroup newOptionGroup)
        {
            int nRowCnt = 0;

            nRowCnt = DataMapperClient_Write.Update("ItemBank.OptionGroup.Update",
                new
                {
                    OptionGroup = newOptionGroup,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }
        #endregion

        #region --删除选项组操作--
        public void Delete(Guid optionGroupID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("ItemBank.OptionGroup.Delete",
                new
                {
                    OptionGroupTitleID = optionGroupID,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }

        public void DeleteByQuestionID(Guid questionID)
        {
            int nRowCnt = 0;
            nRowCnt = DataMapperClient_Write.Delete("ItemBank.OptionGroup.DeleteByQuestionID",
                new
                {
                    QuestionID = questionID,
                    UserID = AppContext.UserContext.Current.UserID
                });
        }

        #endregion

        #region --获取选项组操作--
        public IList<OptionGroup> FindOptionGroupsInQuestion(Guid questionID)
        {
            IList<OptionGroup> LstOptionGroups = DataMapperClient_Read.QueryForList<OptionGroup>(
                "ItemBank.OptionGroup.FindOptionGroupsInQuestion", questionID);

            return LstOptionGroups;
        }

        public OptionGroup GetOptionGroup(Guid optionGroupID)
        {
            OptionGroup OptionGroup = DataMapperClient_Read.QueryForObject<OptionGroup>(
                "ItemBank.OptionGroup.GetByID", optionGroupID);

            return OptionGroup;
        }

        #endregion
    }
}
