// File:    IPaperQuestionLogic.cs
// Author:  Administrator
// Created: 2012��1��12�� 17:24:36
// Purpose: Definition of Interface IPaperQuestionLogic

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.ServiceModel;

///<summary>
/// ����
///</summary>
namespace ETMS.Components.Exam.API.Interface.Test
{
    ///<summary>
    /// �Ծ�������ع���
    ///</summary>
    [ServiceContract(Namespace = "http://Autumn.Business.LMS.Interface.Test/IPaperQuestionLogic")]
    public interface IPaperQuestionLogic
    {
        ///<summary>
        /// �õ�ָ���Ծ��и������͵������������ڹ̶��Ծ���������Ծ�û���á�
        ///</summary>
        /// <param name="testPaperID">�Ծ�ID</param>
        [OperationContract]
        IList<QuestionTypeCnt> GetQuestionTypeCntInPaper(Guid testPaperID);

        ///<summary>
        /// �����Ծ���ָ�����͵ĸ�����ķ�����ֻ���ڹ̶��Ծ�����Ծ�������������ĸ��²�����һ���ӿڡ�
        ///</summary>
        /// <param name="testPaperID">�Ծ�ID</param>
        /// <param name="questionType">Ҫ���õ���������</param>
        /// <param name="score">ÿһ��ķ���</param>
        [OperationContract]
        void UpdateQuestionTypeScoreInPaper(Guid testPaperID, QuestionType questionType, decimal score);

        ///<summary>
        /// ����ָ������ķ���
        ///</summary>
        /// <param name="testPaperID">�Ծ�ID</param>
        /// <param name="paperQuestionID">�������Ծ��е�ID</param>
        /// <param name="score">����</param>
        [OperationContract]
        void UpdateQuestionScore(Guid testPaperID, Guid questionID, decimal score);
        /// <summary>
        /// ����ָ����������
        /// </summary>
        /// <param name="testPaperID">�Ծ�ID</param>
        /// <param name="questionID">�������Ծ��е�ID</param>
        /// <param name="order">���</param>
        [OperationContract]
        void UpdateQuestionSequence(Guid testPaperID, Guid questionID, int order);
        ///<summary>
        /// ���Ծ������һ�µ�����
        ///</summary>
        /// <param name="paperQuestion">Ҫ��ӵ�������Ϣ</param>
        [OperationContract]
        Guid AddQuestion(PaperQuestion paperQuestion);

        ///<summary>
        /// ���Ծ���ɾ��ĳһ����
        ///</summary>
        /// <param name="testPaperID">�Ծ�ID</param>
        /// <param name="paperQuestionID">�Ծ�����ID</param>
        [OperationContract]
        void Delete(Guid testPaperID, Guid paperQuestionID);

        ///<summary>
        /// �����Ծ���ĳһ����
        ///</summary>
        /// <param name="testPaperID">�Ծ�ID</param>
        /// <param name="paperQuestionID">Ҫ�滻������ID</param>
        /// <param name="newPaperQuestion">�滻��������</param>
        [OperationContract]
        void Update(Guid testPaperID, Guid paperQuestionID, PaperQuestion newPaperQuestion);

        ///<summary>
        /// ���Ծ��в�ѯָ�����͵�������Ϣ
        ///</summary>
        /// <param name="testPaperID">�Ծ�ID</param>
        /// <param name="questionType">��������</param>
        [OperationContract]
        IList<PaperQuestionView> FindQuestionView(Guid testPaperID, QuestionType questionType, int pageSize, int pageIndex, out int totalSize);

        ///<summary>
        /// �Զ����⡣�߼�-�̶��Ծ�Ҳ�������ڸ߼�-�����ʽ����ĳһ������Զ����⡣
        /// ���õ������������Ϣ�����͡���Ŀ���Ѷȡ���������ŵȣ� ҵ�����
        /// ͬһ������ࡢͬһ���͡�ͬһ�Ѷ������ѡ���������⣬
        /// ����ѡ�е����ⲻ����ԭ�����ظ���Ҳ������ͬһ�������Ѵ��ڵ����������ظ���
        ///</summary>
        /// <param name="testPaperID">�Ծ�ID</param>
        /// <param name="srcPaperQuestionID">Ҫ���滻���Ծ�����ID</param>
        [OperationContract]
        PaperQuestionView ReplaceQuestion(Guid testPaperID, Guid srcPaperQuestionID);

        ///<summary>
        /// �����������Ծ��Ƿ���������Ҫ���ж෽��ļ�飬
        /// ���磺����Ծ��ܷ��뼰��֣��Ծ��ܷ����Ծ�����ָ���ķ�������������Ƿ�Ϸ��ȡ�
        ///</summary>
        /// <param name="testPaperID">Ҫ�����Ծ�ID</param>
        [OperationContract]
        bool ValidateTestPaper(Guid testPaperID);
        
        /// <summary>
        /// ������в�ѯ���⣬ָ�����ࡢָ�����͡�ָ���Ѷȵ��������������ѯ��
        /// </summary>
        /// <param name="testPaperID">�Ծ�ID(�����Ծ������е�����)</param>
        /// <param name="questionCategory">�������ID(Guid.EmptyΪȫ��)</param>
        /// <param name="questionType">��������(NullΪȫ��)</param>
        /// <param name="difficulty">�����Ѷ�(0Ϊȫ��)</param>
        /// <param name="questionTitle">��Ŀ</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns>����ID��QuestionID�����ͣ�QuestionType����Ŀ��QuestionTitle������������ƣ�QuestionBankName���Ѷȣ�Difficulty�����ö���ObjectID��ѧ�ƣ�Subject</returns>
        [OperationContract]
        IList<Question> FindTKQuestion(Guid ownerID,Guid testPaperID, Guid questionCategory, QuestionType questionType,
           int difficulty, string questionTitle, int pageSize, int pageIndex, out int totalCount);

        //[OperationContract]
        //IList<Question> FindTKQuestion(Guid testPaperID, Guid questionCategory, QuestionType questionType,
        //   int difficulty, string questionTitle, int pageSize, int pageIndex, out int totalCount);
        /// <summary>
        /// ���ѡ�������
        /// </summary>
        /// <param name="testPaperID">�Ծ�ID</param>
        /// <param name="questionList">����ID�����������б�</param>
        [OperationContract]
        void AddQuestionToTestPaper(Guid testPaperID, IList<KeyValuePair<Guid, QuestionType>> questionList);
        
        /// <summary>
        /// �г��Ծ�������ӵ������б�
        /// </summary>
        /// <param name="testPaperID">�Ծ�ID</param>
        /// <param name="totalCount">������Ŀ</param>
        /// <returns></returns>
        [OperationContract]
        IList<PaperQuestionView> GetQuestionViewList(Guid testPaperID, out int totalCount);

        /// <summary>
        /// �����Ծ�ID��ȡ�Ծ��µ�����ID�б�
        /// </summary>
        /// <param name="testPaperID"></param>
        /// <returns></returns>
        [OperationContract]
        IList<Guid> GetQuestionIDsByTestPaperID(Guid testPaperID);

        /// <summary>
        /// �����Ծ����������
        /// </summary>
        /// <param name="testpaperID"></param>
        /// <param name="total"></param>
        [OperationContract]
        void UpdateTestpaperTotalQuantity(Guid testpaperID, int total, decimal score=100M);

        /// <summary>
        /// ɾ���Ծ����������
        /// </summary>
        /// <param name="testpaperID"></param>
        [OperationContract]
        void TestPaperDeleteQuestion(Guid testpaperID);
    }
}