using System;
using System.Collections.Generic;
using System.Linq;
using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    public class OptionGroupServiceLogic : IMessageSourceAware, IInitializingObject,IOptionGroupServiceLogic
    {
        private static string Err_OptionGroupService_Not_Found = "ItemBank.OptionGroupService.Not.Found";

        public IOptionGroupLogic OptionGroupLogic { get; set; }

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
            if (this.OptionGroupLogic == null)
            {
                throw new Exception("please set OptionGroupLogic Property First!");
            }
        }

        #endregion

        #region IOptionGroupService 成员

        public OptionGroupItem AddOptionGroup(OptionGroup optionGroup, 
            IList<QuestionOption> options)
        {
            IOptionGroupLogic logic= this.OptionGroupLogic.AddOptionGroup(optionGroup, options);
            OptionGroupItem optionGroupItem = new OptionGroupItem();
            optionGroupItem.OptionGroup = logic.OptionGroup;
            optionGroupItem.Options = logic.Options;

            return optionGroupItem;
        }

        public bool Delete(Guid questionID, Guid optionGroupID)
        {
            IOptionGroupLogic logic = this.OptionGroupLogic.Load(questionID, optionGroupID);
            ThrowNotFoundException(logic);

            return logic.Delete();
        }

        public OptionGroupItem Load(Guid questionID, Guid optionGroupID)
        {
            IOptionGroupLogic logic = this.OptionGroupLogic.Load(questionID, optionGroupID);
            OptionGroupItem optionGroupItem = new OptionGroupItem();
            optionGroupItem.OptionGroup = logic.OptionGroup;
            optionGroupItem.Options = logic.Options;

            return optionGroupItem;
        }

        public IList<OptionGroupItem> LoadAllInQuestion(Guid questionID)
        {
            IList<IOptionGroupLogic> LstLogic = this.OptionGroupLogic.LoadAllInQuestion(questionID);
            if (LstLogic == null || LstLogic.Count <= 0)
                return null;

            IList<OptionGroupItem> LstGroupItems = LstLogic.Select(
                x =>
                {
                    OptionGroupItem item = new OptionGroupItem();
                    item.Options = x.Options;
                    item.OptionGroup = x.OptionGroup;

                    return item;
                }).ToList<OptionGroupItem>();

            return LstGroupItems;
        }

        public bool Update(OptionGroupItem NewOptionGroupItem)
        {
            IOptionGroupLogic logic = this.OptionGroupLogic.Load(NewOptionGroupItem.OptionGroup.QuestionID,
                NewOptionGroupItem.OptionGroup.OptionGroupTitleID);
            ThrowNotFoundException(logic);

            logic.OptionGroup = NewOptionGroupItem.OptionGroup;
            logic.Options = NewOptionGroupItem.Options;

            return logic.Update();
        }

        private void ThrowNotFoundException(IOptionGroupLogic optionGroupLogic)
        {
            if (optionGroupLogic == null || optionGroupLogic.OptionGroup == null ||
                optionGroupLogic.OptionGroup.OptionGroupTitleID == null ||
                optionGroupLogic.OptionGroup.OptionGroupTitleID == Guid.Empty)
            {
                throw new ETMS.AppContext.BusinessException(Err_OptionGroupService_Not_Found);
            }
        }
        #endregion
    }
}
