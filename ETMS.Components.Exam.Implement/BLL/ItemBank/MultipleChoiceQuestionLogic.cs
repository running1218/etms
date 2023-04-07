// File:    MultipleChoiceQuestionLogic.cs
// Author:  Administrator
// Created: 2011��12��16�� 10:27:24
// Purpose: Definition of Class MultipleChoiceQuestionLogic

using System;
using System.Collections.Generic;
using Autumn.Context;
using Autumn.Objects.Factory;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.Linq;
using Autumn.Transaction.Interceptor;
using ETMS.Components.Exam.API.Interface.ItemBank;

namespace ETMS.Components.Exam.Implement.BLL.ItemBank
{
    internal class MultipleChoiceQuestionLogic : IMessageSourceAware, IInitializingObject, IQuestionServiceLogic
    {
        private static string Err_MultipleChoiceQuestion_Invalid_Type = "ItemBank.MultipleChoiceQuestion.Invalid.Type";
        private static string Err_MultipleChoiceQuestion_Invalid_Data = "ItemBank.MultipleChoiceQuestion.Invalid.Data";
        private static string Err_MultipleChoiceQuestion_Not_Found = "ItemBank.MultipleChoiceQuestion.Not.Found";
        #region --����--
        /// <summary>
        /// �����������Ϣ�Ľӿ�
        /// </summary>
        public ICommonQuestionLogic CommonQuestionLogic { get; set; }
        /// <summary>
        /// ѡ��ķ���ӿ�
        /// </summary>
        public IOptionServiceLogic OptionService { get; set; }
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
        }

        #endregion

        #region IQuestionService ��Ա

        public Guid AddQuestion(QuestionBase question)
        {
            ThrowNotValid(question, System.Guid.Empty);
            MultipleChoiceQuestion MyQuestion = (MultipleChoiceQuestion)question;
            MyQuestion.CommonQuestion = this.CommonQuestionLogic.AddQuestion(question.CommonQuestion);
            if (MyQuestion.CommonQuestion == null || MyQuestion.CommonQuestion.Question == null)
                return Guid.Empty;

            //�������ѡ��
            if (MyQuestion.Options != null && MyQuestion.Options.Count > 0)
            {
                Array.ForEach<QuestionOption>(MyQuestion.Options.ToArray<QuestionOption>(),
                    x =>
                    {
                        x.QuestionID = MyQuestion.CommonQuestion.Question.QuestionID;
                        x.OptionID = this.OptionService.AddOption(x);
                    });
                //��һ�ַ���
                //MyQuestion.Options.Select(
                //    x => 
                //    { 
                //        x.OptionID =this.OptionService.AddOption(x);
                //        return x;
                //    }).ToList<QuestionOption>();
            }
            //���ô�JSON�ִ����������
            MyQuestion.CommonQuestion.Question.Answers = MyQuestion.Answer.Answer;
            //�����,���а�����ѡ���ID��������Ҫ��󱣴�
            this.CommonQuestionLogic.UpdateAnswers(MyQuestion.CommonQuestion.Question.QuestionID,
                MyQuestion.Answer.Answer);

            return MyQuestion.CommonQuestion.Question.QuestionID;
        }
        private int ValidAnswer(MultipleChoiceQuestion MyQuestion, out string sErrMsg)
        {
            sErrMsg = "";
            if (MyQuestion.Answer == null ||
                MyQuestion.Answer.AnswerOptions == null || MyQuestion.Answer.AnswerOptions.Count <= 0)
            {
                sErrMsg = "δ����ָ����";
                return 1;
            }

            //������ѡ���Ƿ���������ѡ��
            var LstTmps = MyQuestion.Answer.AnswerOptions.Except(MyQuestion.Options);
            if (LstTmps != null && LstTmps.Count() > 0)
            {
                sErrMsg = "����ѡ��δȫ����������ѡ��";
                return 2;
            }
            return 0;
        }
        /// <summary>
        /// ��֤ѡ������ѡ���Ƿ����Ҫ��
        /// </summary>
        /// <param name="LstOptions"></param>
        /// <param name="QuestionID"></param>
        /// <param name="sErrMsg"></param>
        /// <returns></returns>
        private int ValidQuestionOptions(IList<QuestionOption> LstOptions,
            System.Guid QuestionID, out string sErrMsg)
        {
            sErrMsg = "";
            if (LstOptions == null || LstOptions.Count <= 0)
                return 0;

            //���ѡ������Ƿ�Ψһ
            var LstTmp = from x in
                             (from item in LstOptions
                              group item by item.OptionCode)
                         where x.Count() > 1
                         select x;
            if (LstTmp.Count() > 0)
            {
                sErrMsg = "ѡ����ⲻΨһ";
                return 1;
            }

            var lstTmp1 = from x in
                              (from item in LstOptions
                               group item by item.OptionContent)
                          where x.Count() > 1
                          select x;
            if (lstTmp1.Count() > 0)
            {
                sErrMsg = "ѡ�����ݲ�Ψһ";
                return 1;
            }

            //���ѡ��������ID�Ƿ�һ��
            var LstTmp2 = (from item in LstOptions
                           where item.OptionID != Guid.Empty && item.QuestionID != QuestionID
                           select item).ToList<QuestionOption>();
            if (LstTmp2.Count() > 0)
            {
                sErrMsg = "ѡ��������ID�����Ȿ��ID��һ��";
                return 2;
            }
            return 0;
        }
        public void Update(Guid questionID, QuestionBase newQuestion)
        {
            ThrowNotValid(newQuestion, questionID);
            //�õ�ԭ����
            MultipleChoiceQuestion MyOldQuestion = (MultipleChoiceQuestion)this.GetByID(questionID);
            if (MyOldQuestion == null || MyOldQuestion.CommonQuestion == null
                || MyOldQuestion.CommonQuestion.Question == null)
            {
                throw new ETMS.AppContext.BusinessException(Err_MultipleChoiceQuestion_Not_Found, new Exception("�����Ѳ����ڣ��޷�����"));
            }

            MultipleChoiceQuestion MyQuestion = (MultipleChoiceQuestion)newQuestion;

            //���и���
            this.CommonQuestionLogic.Update(questionID, newQuestion.CommonQuestion);
            #region --//��������ѡ���
            //�õ�ɾ����
            IList<QuestionOption> LstTemps = QuestionUtils.GetListDeleted(MyOldQuestion.Options, MyQuestion.Options);
            if (LstTemps != null && LstTemps.Count > 0)
            {
                Array.ForEach<QuestionOption>(LstTemps.ToArray<QuestionOption>(),
                    x =>
                    {
                        this.OptionService.Delete(x.OptionID);
                    });
            }
            //�õ�������
            LstTemps = QuestionUtils.GetListNew(MyOldQuestion.Options, MyQuestion.Options);
            if (LstTemps != null && LstTemps.Count > 0)
            {
                Array.ForEach<QuestionOption>(LstTemps.ToArray<QuestionOption>(),
                    x =>
                    {
                        x.QuestionID = questionID;
                        System.Guid OptionID = this.OptionService.AddOption(x);
                        x.OptionID = OptionID;
                    });
            }

            //�õ����µ�
            LstTemps = QuestionUtils.GetListUpdated(MyOldQuestion.Options, MyQuestion.Options);
            if (LstTemps != null && LstTemps.Count > 0)
            {
                Array.ForEach<QuestionOption>(LstTemps.ToArray<QuestionOption>(),
                    x =>
                    {
                        this.OptionService.Update(x);
                    });
            }
            #endregion

            #region --//���±����--
            //���ô�JSON�ִ����������
            MyQuestion.CommonQuestion.Question.Answers = MyQuestion.Answer.Answer;
            //�����,���а�����ѡ���ID��������Ҫ��󱣴�
            this.CommonQuestionLogic.UpdateAnswers(MyQuestion.CommonQuestion.Question.QuestionID,
                MyQuestion.Answer.Answer);
            #endregion
        }

        private void ThrowNotValid(QuestionBase question, Guid questionID)
        {
            if (question == null || question.CommonQuestion == null || question.CommonQuestion.Question == null)
            {
                throw new ETMS.AppContext.BusinessException(Err_MultipleChoiceQuestion_Invalid_Data, new Exception("�������ݲ�����"));
            }
            //�ж����ύ�������Ƿ���ȷ
            if (!(question is MultipleChoiceQuestion))
            {
                throw new ETMS.AppContext.BusinessException(Err_MultipleChoiceQuestion_Invalid_Type, new Exception("��������ͷǶ�ѡ��"));
            }
            MultipleChoiceQuestion MyQuestion = (MultipleChoiceQuestion)question;
            //�������ѡ��
            string sErrMsg = "";
            int nValid = this.ValidQuestionOptions(MyQuestion.Options, questionID, out sErrMsg);
            if (nValid != 0)
            {
                throw new ETMS.AppContext.BusinessException(Err_MultipleChoiceQuestion_Invalid_Data, new Exception(sErrMsg));
            }
            //��֤���Ƿ�Ϸ�
            nValid = this.ValidAnswer(MyQuestion, out sErrMsg);
            if (nValid != 0)
            {
                throw new ETMS.AppContext.BusinessException(Err_MultipleChoiceQuestion_Invalid_Data, new Exception(sErrMsg));
            }
        }

        [Transaction]
        public void Delete(Guid questionID)
        {
            //ɾ������
            this.CommonQuestionLogic.Delete(questionID);
            //ɾ��ѡ��
            this.OptionService.DeleteByQuestionID(questionID);
        }

        public void DeleteBatch(IList<Guid> questionIDs)
        {
            throw new NotImplementedException();
        }

        public QuestionBase GetByID(Guid questionID)
        {
            //�õ�һ����ѡ��
            MultipleChoiceQuestion question = new MultipleChoiceQuestion();
            //�õ����������Ϣ
            CommonQuestion CommonQuestion = this.CommonQuestionLogic.GetByID(questionID);
            if (CommonQuestion == null || CommonQuestion.Question == null)
                return null;
            //������������Ƿ���ȷ
            if (CommonQuestion.Question.QuestionType != QuestionType.MultipleChoice)
            {
                throw new ETMS.AppContext.BusinessException(Err_MultipleChoiceQuestion_Invalid_Type, new Exception("�������ͷǶ�ѡ��"));
            }

            question.CommonQuestion = CommonQuestion;
            //�õ������ѡ��
            IList<QuestionOption> LstOptions = this.OptionService.LoadAllInQuestion(questionID);
            question.Options = LstOptions;

            //����𰸵Ľ���
            string sAnswer = CommonQuestion.Question.Answers;
            IList<OptionAnswer> LstOptionAnswer = AnswerBase.Deserialize<IList<OptionAnswer>>(sAnswer);
            if (LstOptionAnswer != null && LstOptionAnswer.Count > 0)
            {
                //�õ����д𰸵Ķ�Ӧ��ѡ��
                var LstAnswerOptions = (from itemOption in question.Options
                                        join itemAnswerOption in LstOptionAnswer
                                        on itemOption.OptionID equals itemAnswerOption.OptionID
                                        select itemOption).ToList<QuestionOption>();

                if (LstAnswerOptions != null && LstAnswerOptions.Count > 0)
                {
                    MultipleChoiceAnswer oAnswer = new MultipleChoiceAnswer(sAnswer, LstAnswerOptions);
                    question.Answer = oAnswer;
                }
            }
            return question;
        }

        #endregion
    }

    /// <summary>
    /// ���⹤���࣬�ṩһЩ�����ڸ�������֮�乲�õķ���
    /// </summary>
    public class QuestionUtils
    {
        /// <summary>
        /// �õ���Ҫ����ӵ�
        /// </summary>
        /// <param name="LstOld"></param>
        /// <param name="LstNew"></param>
        /// <returns></returns>
        public static IList<QuestionOption> GetListNew(IList<QuestionOption> LstOld,
            IList<QuestionOption> LstNew)
        {
            if (LstOld == null || LstOld.Count <= 0 || LstNew == null || LstNew.Count <= 0)
                return LstNew;
            //�õ��Ѵ���
            var LstTmp = from item in LstNew
                         join old in LstOld
                         on item.OptionID equals old.OptionID
                         select item;
            //�õ������ڵ�
            IList<QuestionOption> LstResult = LstNew.Except(LstTmp).ToList<QuestionOption>();
            return LstResult;
        }
        /// <summary>
        /// �õ���Ҫɾ����
        /// </summary>
        /// <param name="LstOld"></param>
        /// <param name="LstNew"></param>
        /// <returns></returns>
        public static IList<QuestionOption> GetListDeleted(IList<QuestionOption> LstOld,
          IList<QuestionOption> LstNew)
        {
            if (LstOld == null || LstOld.Count <= 0)
                return null;
            if (LstNew == null || LstNew.Count <= 0)
                return LstOld;

            //�õ��Ѵ���
            var LstTmp = from item in LstOld
                         join old in LstNew
                         on item.OptionID equals old.OptionID
                         select item;
            //�õ������ڵ�
            IList<QuestionOption> LstResult = LstOld.Except(LstTmp).ToList<QuestionOption>();
            return LstResult;
        }
        /// <summary>
        /// �õ���Ҫ���µ�
        /// </summary>
        /// <param name="LstOld"></param>
        /// <param name="LstNew"></param>
        /// <returns></returns>
        public static IList<QuestionOption> GetListUpdated(IList<QuestionOption> LstOld,
        IList<QuestionOption> LstNew)
        {
            if (LstOld == null || LstOld.Count <= 0 || LstNew == null || LstNew.Count <= 0)
                return null;
            //�õ��Ѵ���
            var LstTmp = (from item in LstNew
                          join old in LstOld
                          on item.OptionID equals old.OptionID
                          select item).ToList<QuestionOption>();
            return LstTmp;
        }
    }
}