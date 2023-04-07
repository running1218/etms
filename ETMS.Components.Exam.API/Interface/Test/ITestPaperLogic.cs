// File:    ITestPaperLogic.cs
// Author:  Administrator
// Created: 2012��1��12�� 15:21:22
// Purpose: Definition of Interface ITestPaperLogic

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;
using System.ServiceModel;
using ETMS.Components.Exam.API.Entity;
///<summary>
/// ����
///</summary>
namespace ETMS.Components.Exam.API.Interface.Test
{
    ///<summary>
    /// �Ծ������Ϣ�Ĺ��ܽӿ�
    ///</summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.Test/ITestPaperLogic")]
    public interface ITestPaperLogic
    {
        ///<summary>
        /// ���һ���µ��Ծ������Ϣ
        ///</summary>
        /// <param name="testPaper">Ҫ��ӵ��µ��Ծ�</param>
        [OperationContract]
        System.Guid AddTestPaper(TestPaper testPaper);

        //[OperationContract]
        //Guid AddCoursewareTestPaper(string coursewareName, Guid category)
        ///<summary>
        /// ����һ���Ѵ��ڵ��Ծ������Ϣ
        ///</summary>
        /// <param name="testPaperID">Ҫ���µ��Ծ��ID</param>
        /// <param name="newTestPaper">�����Ծ������Ϣ</param>
        [OperationContract]
        void Update(System.Guid testPaperID, TestPaper newTestPaper);

        ///<summary>
        /// ɾ��ָ�����Ծ���Ϣ��ɾ���󣬻Ὣ�Ծ������Ϣ���Ծ��������ϵ���Ծ�ķ�����ɾ�����������Ѹ��ݴ˶������ɵĿ�����������κδ���
        ///</summary>
        /// <param name="testPaperID">Ҫɾ�����Ծ�ID</param>
        [OperationContract]
        void Delete(System.Guid testPaperID);

        ///<summary>
        /// �õ�ĳһ�Ծ������Ϣ
        ///</summary>
        /// <param name="testPaperID">Ҫ��ȡ���Ծ�ID</param>
        [OperationContract]
        TestPaper GetByID(System.Guid testPaperID);

        ///<summary>
        /// ������������������Ծ��ܷ���
        ///</summary>
        /// <param name="questionsCnt">�Ծ��е�������</param>
        /// <param name="totalScore">�Ծ��е��ܷ���</param>
        [OperationContract]
        void UpdateQuestionsCount(Guid testPaperID, int questionsCnt, int totalScore);

        ///<summary>
        /// ���¼�����������ʱ��������������Ϣ
        ///</summary>
        /// <param name="testPaperID">�Ծ�ID</param>
        /// <param name="jige">�������</param>
        /// <param name="examTime">���Ե�ʱ��(��λ���룩0��ʾ�����ơ�</param>
        /// <param name="examTimes">�����Դ���</param>
        [OperationContract]
        void UpdateExamTimes(Guid testPaperID, int jige, int examTime, int examTimes);
        /// <summary>
        /// ���������������ݵ����Կ�����ⱸ�ݱ�
        /// </summary>
        /// <param name="testPaperID"></param>
        [OperationContract]
        void CopyTKQuestionData(Guid testPaperID);

        [OperationContract]
        void DeleteBatchTestPaper(IList<Guid> ids);
        
        /// <summary>
        /// ��Ӫ�̹����Ծ���Դ
        /// </summary>
        /// <param name="search">��ѯ������CategoryIDΪGuid.Empty,������ֵ���ݲ�ѯ������ֵ��</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <param name="pageNo">ҳ��</param>
        /// <param name="total">����</param>
        /// <returns>TestPaperID���Ծ�ID,TestPaperName���Ծ�����,TestPaperType���Ծ�����,UpdatedDate������ʱ�䣬CreatedDate���ϴ�ʱ�� </returns>
        [OperationContract]
        IList<TestSearchResult> GetTestPaperListByOperator(TestSearch search, int pageSize, int pageNo, out int total);
        
        /// <summary>
        /// ��ѯ���Լ����Ծ���Դ�б�
        /// </summary>
        /// <param name="ownerID">�û�ID</param>
        /// <param name="search">��ѯ������
        /// һ�֣�����ID+�ؼ���+�Ծ�����+����״̬+���״̬
        /// ���֣�����ID+��������+�Ծ�����+����״̬+���״̬��</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <param name="pageNo">ҳ��</param>
        /// <param name="total">����</param>
        /// <returns>TestPaperID���Ծ�ID,TestPaperName���Ծ�����,TestPaperType���Ծ�����,CreatedDate���ϴ�ʱ�䣬UpdatedDate������ʱ��</returns>
        [OperationContract]
        IList<TestSearchResult> GetMyTestPaperList(Guid ownerID, TestSearch search, int pageSize, int pageNo, out int total);
        
        /// <summary>
        /// Ԥ���Ծ�
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        IList<TestPaperUnit> GetTestPaperSchema(Guid testPaperID, TestPaperType type);
        
        /// <summary>
        /// �����Ծ�ķ���״̬
        /// </summary>
        /// <param name="testpaperID"></param>
        /// <param name="state"></param>
        [OperationContract]
        void SetShareState(Guid testpaperID, EnumShareStatus state);
        
        /// <summary>
        /// �����Ծ�����״̬
        /// </summary>
        /// <param name="testpaperID"></param>
        /// <param name="state"></param>
        [OperationContract]
        void SetAuditState(Guid testpaperID, TestStatus state);

        /// <summary>
        /// �����Ծ��ֺܷ��Ծ����Ŀ����(ֻ���ڼ��Ծ�͸߼��̶��Ծ�)
        /// </summary>
        /// <param name="testpaperID"></param>
        [OperationContract]
        void SetFixTestPaperScoreAndCount(Guid testpaperID);
        
        /// <summary>
        /// ���Ծ��ƶ�����ķ���
        /// </summary>
        /// <param name="testpaperIDs">�Ծ�ID�б�</param>
        /// <param name="categoryID">����ID</param>
        [OperationContract]
        void SetTestPaperCategoryID(IList<Guid> testpaperIDs, Guid categoryID);
    }
}