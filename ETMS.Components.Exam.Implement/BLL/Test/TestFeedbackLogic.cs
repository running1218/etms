using System;
using System.Collections.Generic;
using System.Linq;
using Autumn.Objects.Factory;
using Autumn.Context;

using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.Implement.DAL.Test;
using ETMS.Components.Exam.API.Interface.Test;
namespace ETMS.Components.Exam.Implement.BLL.Test
{
    /// <summary>
    /// 试卷答题反馈逻辑实现
    /// </summary>
    public class TestFeedbackLogic : ITestFeedbackLogic, IMessageSourceAware, IInitializingObject
    {
        #region --错误代码--
        private static string Err_TestFeedback_Not_Found = "ItemBank.OptionGroup.Not.Found";
        private static string Err_TestFeedback_Data_Invalid = "ItemBank.OptionGroup.Data.Invalid";
        private static string Err_TestFeedback_Instance_Invalid = "ItemBank.OptionGroup.Instance.Invalid";
        private static string Err_TestFeedback_New_Invalid = "ItemBank.OptionGroup.New.Invalid";
        #endregion

        public ITestFeedbackDao TestFeedbackDao { get; set; }

        #region IInitializingObject 成员

        public void AfterPropertiesSet()
        {
            if (TestFeedbackDao == null)
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

        #region --属性--
        private IList<TestFeedback> _Feedbacks = null;
        public IList<TestFeedback> Feedbacks
        {
            get
            {
                if (this._Feedbacks == null)
                {
                    this._Feedbacks = TestFeedbackDao.FindTestFeedbacksInPaper(this.TestPaperID);
                }
                return this._Feedbacks;
            }
            set
            {
                this._Feedbacks = value;
            }
        }
        public Guid TestPaperID
        {
            get;set;
        }

        #endregion

        #region ITestFeedbackLogic 成员

        public void Load(Guid testPaperID)
        {
            //TestFeedbackLogic oTestFeedbackLogic = new TestFeedbackLogic();
            //采用延心加载得到试卷对应的全部答题反馈项
            //oTestFeedbackLogic.Feedbacks = TestFeedbackDao.FindTestFeedbacksInPaper(testPaperID);
            this._Feedbacks = null;
            this.TestPaperID = testPaperID;
        }
        /// <summary>
        /// 为当前试卷添加一新的答题反馈信息
        /// </summary>
        /// <param name="feedbackItem"></param>
        public Guid AddItem(TestFeedback feedbackItem)
        {
            ThrowNotInitializedExeception();

            //验证答题反馈是否是否存在重叠的情况，
            string sErrMsg = "";
            int nRet = this.ValidateFeedbackItem(feedbackItem, out sErrMsg);
            if (nRet != 0)
            {
                throw new ETMS.AppContext.BusinessException(Err_TestFeedback_Data_Invalid, new Exception(sErrMsg));
            }

            //添加
            feedbackItem.TestPaperID = this.TestPaperID;
            feedbackItem.FeedbackID = Guid.NewGuid();

            TestFeedbackDao.AddTestFeedback(feedbackItem);
            //添加成功
            this.Feedbacks.Add(feedbackItem);

            return feedbackItem.FeedbackID;
        }
        /// <summary>
        /// 更新当前试卷的答题反馈项
        /// </summary>
        /// <param name="newFeedbackItem"></param>
        public void UpdateItem(TestFeedback newFeedbackItem)
        {
            ThrowNotInitializedExeception();

            //检查是否存在，
            TestFeedback UpdatedFeedback = this.GetFeedbackItemInList(newFeedbackItem.FeedbackID);
            if (UpdatedFeedback == null)
            {
                throw new ETMS.AppContext.BusinessException(Err_TestFeedback_Data_Invalid, new Exception("要更新的答题反馈项的不属于当前试卷"));
            }

            //检查更新后的分数是否重叠
            string sErrMsg="";
            int nRet=this.ValidateFeedbackItem(newFeedbackItem, out sErrMsg);
            if (nRet != 0)
            {
                throw new ETMS.AppContext.BusinessException(Err_TestFeedback_Data_Invalid, new Exception(sErrMsg));
            }

            //更新
            TestFeedbackDao.Update(newFeedbackItem);
            //删除原来的
            this.Feedbacks.Remove(UpdatedFeedback);

            //对本地进行更新
            this.Feedbacks.Add(newFeedbackItem);
        }
        /// <summary>
        /// 删除当前试卷中指定的答题反馈项
        /// </summary>
        /// <param name="testFeedbackID"></param>
        public void DeleteItem(Guid testFeedbackID)
        {
            ThrowNotInitializedExeception();
            TestFeedback DeletedItem = GetFeedbackItemInList(testFeedbackID);
            if (DeletedItem == null)
            {
                throw new ETMS.AppContext.BusinessException(Err_TestFeedback_Data_Invalid, new Exception("要更新的答题反馈项的不属于当前试卷"));
            }
            //删除
            TestFeedbackDao.Delete(testFeedbackID);
            //从本地删除
            this.Feedbacks.Remove(DeletedItem);
        }
        /// <summary>
        /// 删除试卷的全部答题反馈信息
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            ThrowNotInitializedExeception();
            TestFeedbackDao.DeleteAllInPaper(this.TestPaperID);
            this.Feedbacks.Clear();

            return true;
        }
        /// <summary>
        /// 更新试卷的全部答题反馈信息
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            ThrowNotInitializedExeception();
            //得到原内容
            IList<TestFeedback> LstOldFeedbacks= TestFeedbackDao.FindTestFeedbacksInPaper(this.TestPaperID);

            //得到删除的
            IList<TestFeedback> LstTmps = TestFeedbackUtils.GetListDeleted(LstOldFeedbacks, this.Feedbacks);
            if (LstTmps != null && LstTmps.Count > 0)
            {
                Array.ForEach<TestFeedback>(LstTmps.ToArray<TestFeedback>(),
                    x =>
                    {
                        TestFeedbackDao.Delete(x.FeedbackID);
                    });
            }
            //得到更新
            LstTmps = TestFeedbackUtils.GetListUpdated(LstOldFeedbacks, this.Feedbacks);
            if (LstTmps != null && LstTmps.Count > 0)
            {
                Array.ForEach<TestFeedback>(LstTmps.ToArray<TestFeedback>(),
                    x =>
                    {
                        TestFeedbackDao.Update(x);
                    });
            }
            //得到新增
            LstTmps = TestFeedbackUtils.GetListNew(LstOldFeedbacks, this.Feedbacks);
            if (LstTmps != null && LstTmps.Count > 0)
            {
                Array.ForEach<TestFeedback>(LstTmps.ToArray<TestFeedback>(),
                    x =>
                    {
                        x.FeedbackID = Guid.NewGuid();
                        x.TestPaperID = this.TestPaperID;

                        TestFeedbackDao.AddTestFeedback(x);
                    });
            }

            return true;
        }
        #endregion
        /// <summary>
        /// 从当前试卷的答题反馈中得到指定的ID的反馈项
        /// </summary>
        /// <param name="FeedbackID"></param>
        /// <returns></returns>
        private TestFeedback GetFeedbackItemInList(Guid FeedbackID)
        {
            //检查是否存在，
            var LstFeedbacks = from FeedbackItem in this.Feedbacks
                               where FeedbackItem.FeedbackID == FeedbackID
                               select FeedbackItem;
            if (LstFeedbacks == null || LstFeedbacks.ToList().Count <= 0)
            {
                return null;
            }

            return  LstFeedbacks.ToList()[0];
        }
        /// <summary>
        /// 验证指定的答题反馈项是否与已存在不冲突
        /// </summary>
        /// <param name="feedbackItem"></param>
        /// <returns></returns>
        private int ValidateFeedbackItem(TestFeedback feedbackItem,out string sErrMsg)
        {
            sErrMsg = "";
            if(feedbackItem.BeginScore>=feedbackItem.EndScore)
            {
                sErrMsg ="开始分数一定要大于结束分数";
                return 1;
            }

            //检查是否存在分数交差的情况
            var LstInterset = from FeedbackItem in this.Feedbacks
                              where ((FeedbackItem.BeginScore <= feedbackItem.BeginScore && FeedbackItem.EndScore >= feedbackItem.BeginScore) ||
                              (FeedbackItem.BeginScore <= feedbackItem.EndScore && FeedbackItem.EndScore >= feedbackItem.EndScore)) &&
                              feedbackItem.FeedbackID != FeedbackItem.FeedbackID
                              select FeedbackItem;
            if (LstInterset.ToList().Count > 0)
            {
                sErrMsg = "同一试卷中存在分值范围冲突";
                return 2;
            }
            //ToDo:
            return 0;
        }

        private bool ValidIsInitialized()
        {
            if (this.TestPaperID == null || this.TestPaperID == Guid.Empty)
            {
                return false;
            }
            if (this.Feedbacks == null)
            {
                return false;
            }

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
                throw new ETMS.AppContext.BusinessException(Err_TestFeedback_Instance_Invalid, 
                    new Exception("未正确加载数据，请正确加载试题选项数据加载"));
            }
        }
    }

    /// <summary>
    /// 试卷答题反馈项工具类
    /// </summary>
    public class TestFeedbackUtils
    {
        /// <summary>
        /// 得到需要新添加的
        /// </summary>
        /// <param name="LstOld"></param>
        /// <param name="LstNew"></param>
        /// <returns></returns>
        public static IList<TestFeedback> GetListNew(IList<TestFeedback> LstOld,
            IList<TestFeedback> LstNew)
        {
            if (LstOld == null || LstOld.Count <= 0 || LstNew == null || LstNew.Count <= 0)
                return LstNew;
            //得到已存在
            var LstTmp = from item in LstNew
                         join old in LstOld
                         on item.FeedbackID equals old.FeedbackID
                         select item;
            //得到不存在的
            IList<TestFeedback> LstResult = LstNew.Except(LstTmp).ToList<TestFeedback>();
            return LstResult;
        }
        /// <summary>
        /// 得到需要删除的
        /// </summary>
        /// <param name="LstOld"></param>
        /// <param name="LstNew"></param>
        /// <returns></returns>
        public static IList<TestFeedback> GetListDeleted(IList<TestFeedback> LstOld,
          IList<TestFeedback> LstNew)
        {
            if (LstOld == null || LstOld.Count <= 0)
                return null;
            if (LstNew == null || LstNew.Count <= 0)
                return LstOld;

            //得到已存在
            var LstTmp = from item in LstOld
                         join old in LstNew
                         on item.FeedbackID equals old.FeedbackID
                         select item;
            //得到不存在的
            IList<TestFeedback> LstResult = LstOld.Except(LstTmp).ToList<TestFeedback>();
            return LstResult;
        }
        /// <summary>
        /// 得到需要更新的
        /// </summary>
        /// <param name="LstOld"></param>
        /// <param name="LstNew"></param>
        /// <returns></returns>
        public static IList<TestFeedback> GetListUpdated(IList<TestFeedback> LstOld,
        IList<TestFeedback> LstNew)
        {
            if (LstOld == null || LstOld.Count <= 0 || LstNew == null || LstNew.Count <= 0)
                return null;
            //得到已存在
            var LstTmp = (from item in LstNew
                          join old in LstOld
                          on item.FeedbackID equals old.FeedbackID
                          select item).ToList<TestFeedback>();
            return LstTmp;
        }
    }
}
