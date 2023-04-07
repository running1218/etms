// File:    QuestionBaseLogic.cs
// Author:  Administrator
// Created: 2011��12��16�� 10:27:19
// Purpose: Definition of Class QuestionBaseLogic

using System;
using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ItemBank;
using Autumn.Objects.Factory;
using Autumn.Context;
using Autumn.Transaction.Interceptor;
using System.Linq;
using ETMS.Components.Exam.API.Interface.ItemBank;
namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{

    ///<summary>
    /// ʵ�ֶ����⹦���߼��Ļ���ʵ��
    ///</summary>
    public class QuestionBaseLogic : IMessageSourceAware, IInitializingObject, ICommonQuestionLogic
    {

        private static string Err_QuestionBaseLogic_Data_Invalid = "ItemBank.QuestionBaseLogic.Data.Invalid";
        private static string Err_QuestionBaseLogic_Not_Found = "ItemBank.QuestionBaseLogic.Not.Found";
        private static string Err_QuestionBaseLogic_Invalid_Parameter = "ItemBank.QuestionBaseLogic.Invalid.Parameter";
        #region --����--
        /// <summary>
        /// ������ⷴ�����߼��ӿ�
        /// </summary>
        public IQuestionFeedbackLogic QuestionFeedbackLogic { get; set; }
        /// <summary>
        /// ����ѡ������߼��ӿ�
        /// </summary>
        public IOptionFeedbackLogic OptionFeedbackLogic { get; set; }
        /// <summary>
        /// �������˼·���߼��ӿ�
        /// </summary>
        public IQuestionExtendLogic QuestionExtendLogic { get; set; }
        /// <summary>
        /// ������ɲ��ֵ��߼��ӿ�
        /// </summary>
        public IQuestionLogic QuestionTitleLogic { get; set; }
        #endregion

        #region IMessageSourceAware ��Ա

        public IMessageSource MessageSource
        {
            get;
            set;
        }

        #endregion

        #region IInitializingObject ��Ա

        public void AfterPropertiesSet()
        {
            if (this.QuestionFeedbackLogic == null)
                throw new Exception("please set QuestionFeedbackLogic Property First!");
            if (this.OptionFeedbackLogic == null)
                throw new Exception("please set OptionFeedbackLogic Property First!");
            if (this.QuestionExtendLogic == null)
                throw new Exception("please set QuestionExtendLogic Property First!");
            if (this.QuestionTitleLogic == null)
                throw new Exception("please set QuestionTitleLogic Property First!");
        }

        #endregion

        ///<summary>
        /// ���һ������
        ///</summary>
        /// <param name="question">Ҫ��ӵ�����Ļ��ࡣ������ϵͳ֧�ֵļ�������ľ�����ʵ����</param>
        /// <returns>������ID��Ϣ��������</returns>
        public CommonQuestion AddQuestion(CommonQuestion questionItem)
        {
            //��Ҫ��ӵ����������֤
            int nValid = 0;
            string sErrMsg = "";
            nValid = ValidCommonQuestion(questionItem, out sErrMsg);
            if (nValid != 0)
            {
                throw new ETMS.AppContext.BusinessException(Err_QuestionBaseLogic_Data_Invalid, new Exception(sErrMsg));
            }

            //�������
            System.Guid questionID = this.QuestionTitleLogic.AddQuestion(questionItem.Question);
            ////�������Ľ���˼·
            //if (questionItem.QuestionExtend != null)
            //{
            //    questionItem.QuestionExtend.QuestionID = questionID;
            //    this.QuestionExtendLogic.Add(questionItem.QuestionExtend);
            //}
            ////������ⷴ��
            //if (questionItem.QuestionFeedback != null)
            //{
            //    questionItem.QuestionFeedback.QuestionID = questionID;
            //    questionItem.QuestionFeedback.FeedbackID =
            //        this.QuestionFeedbackLogic.Add(questionItem.QuestionFeedback);
            //}
            ////���ѡ����
            //if (questionItem.LstOptionFeedbacks != null && questionItem.LstOptionFeedbacks.Count > 0)
            //{
            //    //��Ӷ��ѡ��
            //    foreach (OptionFeedback feedback in questionItem.LstOptionFeedbacks)
            //    {
            //        feedback.QuestionID = questionID;
            //        feedback.OptionFeedbackID = this.OptionFeedbackLogic.Add(feedback);
            //    }
            //}

            return questionItem;
        }
        /// <summary>
        /// ��֤ͨ����������Ƿ�Ϸ�
        /// </summary>
        /// <param name="questionItem">Ҫ��֤���������</param>
        /// <param name="sErrMsg">������Ϸ������صĴ�����Ϣ</param>
        /// <returns></returns>
        private int ValidCommonQuestion(CommonQuestion questionItem, out string sErrMsg)
        {
            sErrMsg = "";
            if (questionItem == null)
            {
                sErrMsg = "������ϢΪNULL";
                return 1;
            }

            #region --//��֤ѡ���--
            if (questionItem.QuestionType == QuestionType.SingleChoice || questionItem.QuestionType == QuestionType.MultipleChoice)
            { }
            else
            {
                if (questionItem.LstOptionFeedbacks != null && questionItem.LstOptionFeedbacks.Count > 0)
                {
                    sErrMsg = "ֻ�е�ѡ����ѡ��ɴ���ѡ������������Ͳ�֧��ѡ���";
                    return 2;
                }
            }
            #endregion

            #region --//��֤�����ֵ�ID�Ƿ�һ��--
            if (questionItem.QuestionExtend != null)
            {
                if (questionItem.QuestionExtend.QuestionID != questionItem.Question.QuestionID)
                {
                    sErrMsg = "����˼·�е�����ID�������е�����ID��һ��";
                    return 3;
                }
            }
            if (questionItem.QuestionFeedback != null)
            {
                if (questionItem.QuestionFeedback.QuestionID != questionItem.Question.QuestionID)
                {
                    sErrMsg = "���ⷴ���е�����ID�������е�����ID��һ��";
                    return 3;
                }
            }
            if (questionItem.LstOptionFeedbacks != null && questionItem.LstOptionFeedbacks.Count > 0)
            {
                var LstTmp = from item in questionItem.LstOptionFeedbacks
                             where item.QuestionID != questionItem.Question.QuestionID
                             select item;
                if (LstTmp.Count() > 0)
                {
                    sErrMsg = "ѡ����е�����ID�������е�����ID��һ��";
                    return 3;
                }
            }
            #endregion

            #region --//��֤ѡ������Ƿ������ͬ�Ĳ���--
            if (questionItem.LstOptionFeedbacks != null && questionItem.LstOptionFeedbacks.Count > 0)
            {
                var LstResult = questionItem.LstOptionFeedbacks.GroupBy(
                    x =>
                    {
                        return x.Options;
                    }).Where(
                    y =>
                    {
                        return y.Count<OptionFeedback>() > 1;
                    });
                //���Բ�����һ��д��
                //var LstResult = from tmp in
                //                    (
                //                        from optionFeedback in questionItem.LstOptionFeedbacks
                //                        group optionFeedback by optionFeedback.Options)
                //                where tmp.Count() > 1
                //                select tmp;

                if (LstResult.Count() > 0)
                {
                    sErrMsg = "ѡ����д����ظ�������";
                    return 4;
                }
            }
            #endregion

            return 0;
        }
        ///<summary>
        /// ����һ�����������Ϣ
        ///</summary>
        ///<remarks>
        ///1,���µ���������Ѵ��ڣ���������ڣ��޷����и��£�<br></br>
        ///2,����ʱ������Ϊ�������Ӵ𰸷�����ѡ����ͽ���˼·��<br></br>
        ///3,�������˼·���𰸷�����ѡ����Ѵ��ڵĲ��ֽ����£�<br></br>
        ///</remarks>
        /// <param name="questionID">Ҫ���µ�����ID</param>
        /// <param name="newQuestion">���µ�����ʵ�壨����Ϊ�������͵�ʵ�壩</param>
        public void Update(System.Guid questionID, CommonQuestion newQuestion)
        {
            #region --��֤Ҫ���µ��������--
            if (newQuestion == null || newQuestion.Question == null || newQuestion.Question.QuestionID != questionID)
            {
                throw new ETMS.AppContext.BusinessException(Err_QuestionBaseLogic_Invalid_Parameter, new Exception("����ID����������е�����ID��һ��"));
            }
            //���Ҫ���µ���������Ƿ����ֵһ��
            int nValid = 0;
            string sErrMsg = "";
            nValid = this.ValidCommonQuestion(newQuestion, out sErrMsg);
            if (nValid != 0)
            {
                throw new ETMS.AppContext.BusinessException(Err_QuestionBaseLogic_Data_Invalid, new Exception(sErrMsg));
            }
            #endregion

            //�õ������ԭ����Ϣ
            CommonQuestion OldQuestion = this.GetByID(questionID);
            if (OldQuestion == null || OldQuestion.Question == null)
            {
                throw new ETMS.AppContext.BusinessException(Err_QuestionBaseLogic_Not_Found, new Exception("�����Ѳ����ڣ��޷�����"));
            }

            //��ɵĸ���
            this.QuestionTitleLogic.Update(questionID, newQuestion.Question);
            ////����˼·�ĸ���
            //this.QuestionExtendLogic.Update(newQuestion.QuestionExtend);
            //#region --//�𰸷����ĸ���--
            //if (OldQuestion.QuestionFeedback != null && newQuestion.QuestionFeedback != null)
            //{
            //    //���ԭ���Ѵ��ڴ𰸷�����
            //    newQuestion.QuestionFeedback.FeedbackID = OldQuestion.QuestionFeedback.FeedbackID;
            //    //����
            //    this.QuestionFeedbackLogic.Update(newQuestion.QuestionFeedback);
            //}
            //if (OldQuestion.QuestionFeedback != null && newQuestion.QuestionFeedback == null)
            //{
            //    //ԭ��Ҫɾ����
            //    this.QuestionFeedbackLogic.Delete(OldQuestion.QuestionFeedback.FeedbackID);
            //}
            //if (OldQuestion.QuestionFeedback == null && newQuestion.QuestionFeedback != null)
            //{
            //    //����µĴ��ⷴ��
            //    this.QuestionFeedbackLogic.Add(newQuestion.QuestionFeedback);
            //}
            //#endregion

            //#region -- //ѡ����ĸ���--
            //IList<OptionFeedback> LstOptionFeedbacks = null;
            ////�õ�Ҫɾ����ѡ���
            //LstOptionFeedbacks = this.GetListDeleted(OldQuestion.LstOptionFeedbacks, newQuestion.LstOptionFeedbacks);
            //if (LstOptionFeedbacks != null && LstOptionFeedbacks.Count > 0)
            //{
            //    //����ĸ�ɾ����
            //    LstOptionFeedbacks.Select(
            //        x =>
            //        {
            //            this.OptionFeedbackLogic.Delete(x.OptionFeedbackID);
            //            return x;
            //        }).ToList<OptionFeedback>();
            //}
            ////�õ�Ҫ���µ�ѡ���
            //LstOptionFeedbacks = this.GetListUpdated(OldQuestion.LstOptionFeedbacks, newQuestion.LstOptionFeedbacks);
            //if (LstOptionFeedbacks != null && LstOptionFeedbacks.Count > 0)
            //{
            //    LstOptionFeedbacks.Select(
            //        x =>
            //        {
            //            this.OptionFeedbackLogic.Update(x.OptionFeedbackID, x.Options, x.Content);
            //            return x;
            //        }).ToList<OptionFeedback>();
            //}
            ////�õ�Ҫ������ѡ���
            //LstOptionFeedbacks = this.GetListNew(OldQuestion.LstOptionFeedbacks, newQuestion.LstOptionFeedbacks);
            //if (LstOptionFeedbacks != null && LstOptionFeedbacks.Count > 0)
            //{
            //    LstOptionFeedbacks.Select(
            //        x =>
            //        {
            //            x.QuestionID = questionID;
            //            System.Guid OptionFeedbackID = this.OptionFeedbackLogic.Add(x);
            //            x.OptionFeedbackID = OptionFeedbackID;
            //            return x;
            //        }).ToList<OptionFeedback>();
            //}
            //#endregion

        }
        /// <summary>
        /// �õ���Ҫ����ӵ�
        /// </summary>
        /// <param name="LstOld"></param>
        /// <param name="LstNew"></param>
        /// <returns></returns>
        private IList<OptionFeedback> GetListNew(IList<OptionFeedback> LstOld,
            IList<OptionFeedback> LstNew)
        {
            if (LstOld == null || LstOld.Count <= 0 || LstNew == null || LstNew.Count <= 0)
                return LstNew;
            //�õ��Ѵ���
            var LstTmp = from item in LstNew
                         join old in LstOld
                         on item.OptionFeedbackID equals old.OptionFeedbackID
                         select item;
            //�õ������ڵ�
            IList<OptionFeedback> LstResult = LstNew.Except(LstTmp).ToList<OptionFeedback>();
            return LstResult;
        }
        /// <summary>
        /// �õ���Ҫɾ����
        /// </summary>
        /// <param name="LstOld"></param>
        /// <param name="LstNew"></param>
        /// <returns></returns>
        private IList<OptionFeedback> GetListDeleted(IList<OptionFeedback> LstOld,
          IList<OptionFeedback> LstNew)
        {
            if (LstOld == null || LstOld.Count <= 0)
                return null;
            if (LstNew == null || LstNew.Count <= 0)
                return LstOld;

            //�õ��Ѵ���
            var LstTmp = from item in LstOld
                         join old in LstNew
                         on item.OptionFeedbackID equals old.OptionFeedbackID
                         select item;
            //�õ������ڵ�
            IList<OptionFeedback> LstResult = LstOld.Except(LstTmp).ToList<OptionFeedback>();
            return LstResult;
        }
        /// <summary>
        /// �õ���Ҫ���µ�
        /// </summary>
        /// <param name="LstOld"></param>
        /// <param name="LstNew"></param>
        /// <returns></returns>
        private IList<OptionFeedback> GetListUpdated(IList<OptionFeedback> LstOld,
        IList<OptionFeedback> LstNew)
        {
            if (LstOld == null || LstOld.Count <= 0 || LstNew == null || LstNew.Count <= 0)
                return null;
            //�õ��Ѵ���
            var LstTmp = (from item in LstNew
                          join old in LstOld
                          on item.OptionFeedbackID equals old.OptionFeedbackID
                          select item).ToList<OptionFeedback>();
            return LstTmp;
        }
        ///<summary>
        /// ɾ��ָ��������
        ///</summary>
        /// <param name="questionID">Ҫɾ���������ID</param>
        [Transaction]
        public void Delete(System.Guid questionID)
        {
            CommonQuestion questionItem = this.GetByID(questionID);
            if (questionItem == null || questionItem.Question == null)
                return;
            //�����߼��㣬��ÿһ����ɾ��
            this.QuestionTitleLogic.Delete(questionID);
            if (questionItem.QuestionExtend != null)
                this.QuestionExtendLogic.Delete(questionID);
            if (questionItem.QuestionFeedback != null)
                this.QuestionFeedbackLogic.DeleteInQuestion(questionID);
            if (questionItem.LstOptionFeedbacks != null && questionItem.LstOptionFeedbacks.Count > 0)
            {
                this.OptionFeedbackLogic.DeleteInQuestion(questionID);
            }
        }

        ///<summary>
        /// �õ�ĳһ���⡣�������������͵õ������������͵�ʵ����
        ///</summary>
        /// <param name="questionID">Ҫ��ȡ�������ID</param>
        public CommonQuestion GetByID(System.Guid questionID)
        {
            CommonQuestion questionItem = new CommonQuestion();
            //�õ���������������Ϣ
            Question question = this.QuestionTitleLogic.GetByID(questionID);
            if (question == null)
                return null;
            questionItem.Question = question;
            questionItem.QuestionType = question.QuestionType;
            //�õ����ⷴ��
            questionItem.QuestionFeedback = this.QuestionFeedbackLogic.GetFeedback(questionID);
            questionItem.LstOptionFeedbacks = this.OptionFeedbackLogic.GetFeedback(questionID);
            questionItem.QuestionExtend = this.QuestionExtendLogic.GetQuestionExtend(questionID);

            return questionItem;
        }
        /// <summary>
        /// �޸Ĵ��ַ���
        /// </summary>
        /// <param name="questionID">����ID</param>
        /// <param name="answer">���ַ���</param>
        public void UpdateAnswers(Guid questionID, string answer)
        {
            this.QuestionTitleLogic.UpdateAnswers(questionID, answer);
        }
    }
}
