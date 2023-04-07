using System;
using System.Collections.Generic;
using System.Linq;

using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Interface.Test;
namespace ETMS.Components.Exam.Implement.BLL.Test
{
    public class TestFeedbackServiceLogic : ITestFeedbackServiceLogic, IMessageSourceAware, IInitializingObject
    {
        public ITestFeedbackLogic TestFeedbackLogic { get; set; }

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (this.TestFeedbackLogic == null)
            {
                throw new Exception("please set OptionGroupLogic Property First!");
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

        #region ITestFeedbackService 成员

        public Guid AddFeedbackItem(Guid testPaperID, TestFeedback testFeedbackItem)
        {
             this.TestFeedbackLogic.Load(testPaperID);
             Guid NewItemID = this.TestFeedbackLogic.AddItem(testFeedbackItem);

            return NewItemID;
        }

        public void UpdateFeedbackItem(Guid testPaperID, TestFeedback newTestFeedback)
        {
            this.TestFeedbackLogic.Load(testPaperID);
            this.TestFeedbackLogic.UpdateItem(newTestFeedback);

        }

        public void DeleteFeedbackItem(Guid testPaperID, Guid testFeedbackID)
        {
             this.TestFeedbackLogic.Load(testPaperID);
             this.TestFeedbackLogic.DeleteItem(testFeedbackID);
        }

        public TestFeedback GetFeedbackByID(Guid testPaperID, Guid testFeedbackItemID)
        {
             this.TestFeedbackLogic.Load(testPaperID);
             if (this.TestFeedbackLogic.Feedbacks == null || this.TestFeedbackLogic.Feedbacks.Count <= 0)
            {
                return null;
            }

            //得到
            var LstTemps = from FeedbackItem in this.TestFeedbackLogic.Feedbacks
                           where FeedbackItem.FeedbackID == testFeedbackItemID
                           select FeedbackItem;
            if (LstTemps != null && LstTemps.ToList().Count > 0)
            {
                return LstTemps.ToList<TestFeedback>()[0];
            }

            return null;
        }

        public IList<TestFeedback> GetFeedbacksInTestPaper(Guid testPaperID)
        {
             this.TestFeedbackLogic.Load(testPaperID);
             return this.TestFeedbackLogic.Feedbacks;
        }

        public void DeleteAllInTestPaper(Guid testPaperID)
        {
            this.TestFeedbackLogic.Load(testPaperID);
            this.TestFeedbackLogic.Delete();
        }

        public void UpdateFeedbacks(Guid testPaperID, IList<TestFeedback> lstFeedbacks)
        {
            this.TestFeedbackLogic.Load(testPaperID);
            this.TestFeedbackLogic.Feedbacks = lstFeedbacks;
            this.TestFeedbackLogic.Update();
        }

        #endregion
    }
}
