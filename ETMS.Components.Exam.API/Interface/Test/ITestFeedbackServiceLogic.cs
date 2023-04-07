using System;
using ETMS.Components.Exam.API.Entity.Test;
using System.Collections.Generic;
using System.ServiceModel;
namespace ETMS.Components.Exam.API.Interface.Test
{
    /// <summary>
    /// 试卷反馈信息服务接口
    /// </summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.Test/ITestFeedbackServiceLogic")]
    public interface ITestFeedbackServiceLogic
    {
        /// <summary>
        /// 为试卷添加一新的答题反馈项
        /// </summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="testFeedbackItem">添加的答案反馈</param>
        [OperationContract]
        Guid AddFeedbackItem(Guid testPaperID, TestFeedback testFeedbackItem);
        ///<summary>
        /// 更新一个已存在的试卷反馈项信息
        ///</summary>
        /// <param name="testPaperID">要更新的试卷ID</param>
        /// <param name="newTestFeedback">更新试卷反馈项</param>
        [OperationContract]
        void UpdateFeedbackItem(Guid testPaperID, TestFeedback newTestFeedback);
        ///<summary>
        /// 删除指定试卷中，某一指定的答卷反馈项
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="testFeedbackID">要删除的试卷反馈ID</param>
        [OperationContract]
        void DeleteFeedbackItem(Guid testPaperID, System.Guid testFeedbackID);

        
        ///<summary>
        /// 得到一指定ID的试卷反馈信息
        ///</summary>
        /// <param name="testPaperID">要获取的试卷ID</param>
        /// <param name="testFeedbackItemID">试卷反馈项ID</param>
        [OperationContract]
        TestFeedback GetFeedbackByID(Guid testPaperID,System.Guid testFeedbackItemID);

        ///<summary>
        /// 得到某一指定试卷的答卷反馈信息
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        [OperationContract]
        IList<TestFeedback> GetFeedbacksInTestPaper(Guid testPaperID);
        ///<summary>
        /// 删除某一指定试卷中的所有试卷反馈信息
        ///</summary>
        [OperationContract]
        void DeleteAllInTestPaper(Guid testPaperID);
        ///<summary>
        /// 更新某一试卷中所有的试卷反馈项。可新增、删除或修改。此接口为大粒度接口，前端可优先考虑使用该接口。
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="lstFeedbacks">变化后的试卷反馈项列表</param>
        [OperationContract]
        void UpdateFeedbacks(Guid testPaperID, IList<TestFeedback> lstFeedbacks);
    }
}
