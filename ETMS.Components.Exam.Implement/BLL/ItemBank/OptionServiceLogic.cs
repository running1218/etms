using System;
using System.Collections.Generic;
using System.Linq;
using Autumn.Objects.Factory;
using Autumn.Context;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;

namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    public class OptionServiceLogic : IMessageSourceAware, IInitializingObject, IOptionServiceLogic
    {
        private static string Err_OptionService_Not_Found="ItemBank.OptionService.Not.Found";
        public IQuestionOptionLogic QuestionOptionLogic { get; set; }

        #region IMessageSourceAware 成员

        public IMessageSource MessageSource
        {
            get;
            set;
        }

        #endregion

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            
        }

        #endregion

        #region IOptionService 成员

        public Guid AddOption(QuestionOption option)
        {
            IQuestionOptionLogic optionLogic= this.QuestionOptionLogic.AddOption(option);
            if (optionLogic != null)
                return optionLogic.QuestionOption.OptionID;

            return Guid.Empty;
        }

        public bool Delete(Guid questionOptionID)
        {
            //得到后再修改
            IQuestionOptionLogic optionLogic = this.QuestionOptionLogic.Load(questionOptionID);
            ThrowNotFoundException(optionLogic);

            return optionLogic.Delete();
        }

        public bool DeleteByGroupID(Guid questionID, Guid optionGroupID)
        {
            return this.QuestionOptionLogic.DeleteByGroupID(questionID, optionGroupID);
        }

        public bool DeleteByQuestionID(Guid questionID)
        {
            return this.QuestionOptionLogic.DeleteByQuestionID(questionID);
        }

        public QuestionOption Load(Guid questionOptionID)
        {
            IQuestionOptionLogic optionLogic = this.QuestionOptionLogic.Load(questionOptionID);
            if (optionLogic == null)
                return null;

            return optionLogic.QuestionOption;
        }

        public IList<QuestionOption> LoadAllInGroup(Guid questionID, Guid optionGroupTitleID)
        {
            IList<IQuestionOptionLogic> LstQuestionLogic = this.QuestionOptionLogic.LoadAllInGroup(
                questionID, optionGroupTitleID);
            if (LstQuestionLogic == null || LstQuestionLogic.Count <= 0)
                return null;
            //转换
            var LstOptions = from OptionLogic in LstQuestionLogic
                             orderby OptionLogic.QuestionOption.OptionCode 
                             select OptionLogic.QuestionOption;
            if (LstOptions == null)
                return null;

            return LstOptions.ToList<QuestionOption>();
        }

        public IList<QuestionOption> LoadAllInQuestion(Guid questionID)
        {
            IList<IQuestionOptionLogic> LstQuestionLogic = this.QuestionOptionLogic.LoadAllInQuestion(
                questionID);
            if (LstQuestionLogic == null || LstQuestionLogic.Count <= 0)
                return null;
            //转换
            var LstOptions = from OptionLogic in LstQuestionLogic
                             orderby OptionLogic.QuestionOption.OptionCode 
                             select OptionLogic.QuestionOption;
            if (LstOptions == null)
                return null;

            return LstOptions.ToList<QuestionOption>();
        }

        public bool Update(QuestionOption option)
        {
            IQuestionOptionLogic optionLogic = this.QuestionOptionLogic.Load(option.OptionID);
            ThrowNotFoundException(optionLogic);

            optionLogic.QuestionOption = option;
            return optionLogic.Update();
        }
        private void ThrowNotFoundException(IQuestionOptionLogic optionLogic )
        { 
            if (optionLogic == null || optionLogic.QuestionOption==null ||
                optionLogic.QuestionOption.OptionID == null || optionLogic.QuestionOption.OptionID == Guid.Empty)
            {
                throw new ETMS.AppContext.BusinessException(Err_OptionService_Not_Found);
            }
        }
        #endregion
    }
}
