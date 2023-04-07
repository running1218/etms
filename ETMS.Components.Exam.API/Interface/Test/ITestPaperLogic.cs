// File:    ITestPaperLogic.cs
// Author:  Administrator
// Created: 2012年1月12日 15:21:22
// Purpose: Definition of Interface ITestPaperLogic

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;
using System.ServiceModel;
using ETMS.Components.Exam.API.Entity;
///<summary>
/// 测评
///</summary>
namespace ETMS.Components.Exam.API.Interface.Test
{
    ///<summary>
    /// 试卷基本信息的功能接口
    ///</summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.Test/ITestPaperLogic")]
    public interface ITestPaperLogic
    {
        ///<summary>
        /// 添加一个新的试卷基本信息
        ///</summary>
        /// <param name="testPaper">要添加的新的试卷</param>
        [OperationContract]
        System.Guid AddTestPaper(TestPaper testPaper);

        //[OperationContract]
        //Guid AddCoursewareTestPaper(string coursewareName, Guid category)
        ///<summary>
        /// 更新一个已存在的试卷基本信息
        ///</summary>
        /// <param name="testPaperID">要更新的试卷的ID</param>
        /// <param name="newTestPaper">更新试卷基本信息</param>
        [OperationContract]
        void Update(System.Guid testPaperID, TestPaper newTestPaper);

        ///<summary>
        /// 删除指定的试卷信息。删除后，会将试卷基本信息、试卷与试题关系、试卷的反馈等删除掉。对于已根据此定义生成的考生答卷不进行任何处理。
        ///</summary>
        /// <param name="testPaperID">要删除的试卷ID</param>
        [OperationContract]
        void Delete(System.Guid testPaperID);

        ///<summary>
        /// 得到某一试卷基本信息
        ///</summary>
        /// <param name="testPaperID">要获取的试卷ID</param>
        [OperationContract]
        TestPaper GetByID(System.Guid testPaperID);

        ///<summary>
        /// 更新试题的总题数、试卷总分数
        ///</summary>
        /// <param name="questionsCnt">试卷中的总题数</param>
        /// <param name="totalScore">试卷中的总分数</param>
        [OperationContract]
        void UpdateQuestionsCount(Guid testPaperID, int questionsCnt, int totalScore);

        ///<summary>
        /// 更新及格分数、答卷时长、答卷次数等信息
        ///</summary>
        /// <param name="testPaperID">试卷ID</param>
        /// <param name="jige">及格分数</param>
        /// <param name="examTime">考试的时间(单位：秒）0表示不限制。</param>
        /// <param name="examTimes">允许考试次数</param>
        [OperationContract]
        void UpdateExamTimes(Guid testPaperID, int jige, int examTime, int examTimes);
        /// <summary>
        /// 复制题库的试题数据到考试库的试题备份表
        /// </summary>
        /// <param name="testPaperID"></param>
        [OperationContract]
        void CopyTKQuestionData(Guid testPaperID);

        [OperationContract]
        void DeleteBatchTestPaper(IList<Guid> ids);
        
        /// <summary>
        /// 运营商管理试卷资源
        /// </summary>
        /// <param name="search">查询条件（CategoryID为Guid.Empty,其他的值根据查询条件赋值）</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageNo">页数</param>
        /// <param name="total">总数</param>
        /// <returns>TestPaperID：试卷ID,TestPaperName：试卷名称,TestPaperType：试卷类型,UpdatedDate：更新时间，CreatedDate：上传时间 </returns>
        [OperationContract]
        IList<TestSearchResult> GetTestPaperListByOperator(TestSearch search, int pageSize, int pageNo, out int total);
        
        /// <summary>
        /// 查询我自己的试卷资源列表
        /// </summary>
        /// <param name="ownerID">用户ID</param>
        /// <param name="search">查询条件（
        /// 一种：分类ID+关键字+试卷类型+分享状态+审核状态
        /// 二种：分类ID+试题内容+试卷类型+分享状态+审核状态）</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageNo">页数</param>
        /// <param name="total">总数</param>
        /// <returns>TestPaperID：试卷ID,TestPaperName：试卷名称,TestPaperType：试卷类型,CreatedDate：上传时间，UpdatedDate：更新时间</returns>
        [OperationContract]
        IList<TestSearchResult> GetMyTestPaperList(Guid ownerID, TestSearch search, int pageSize, int pageNo, out int total);
        
        /// <summary>
        /// 预览试卷
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        IList<TestPaperUnit> GetTestPaperSchema(Guid testPaperID, TestPaperType type);
        
        /// <summary>
        /// 设置试卷的分享状态
        /// </summary>
        /// <param name="testpaperID"></param>
        /// <param name="state"></param>
        [OperationContract]
        void SetShareState(Guid testpaperID, EnumShareStatus state);
        
        /// <summary>
        /// 设置试卷的审核状态
        /// </summary>
        /// <param name="testpaperID"></param>
        /// <param name="state"></param>
        [OperationContract]
        void SetAuditState(Guid testpaperID, TestStatus state);

        /// <summary>
        /// 设置试卷总分和试卷的题目总数(只用于简单试卷和高级固定试卷)
        /// </summary>
        /// <param name="testpaperID"></param>
        [OperationContract]
        void SetFixTestPaperScoreAndCount(Guid testpaperID);
        
        /// <summary>
        /// 将试卷移动至别的分类
        /// </summary>
        /// <param name="testpaperIDs">试卷ID列表</param>
        /// <param name="categoryID">分类ID</param>
        [OperationContract]
        void SetTestPaperCategoryID(IList<Guid> testpaperIDs, Guid categoryID);
    }
}