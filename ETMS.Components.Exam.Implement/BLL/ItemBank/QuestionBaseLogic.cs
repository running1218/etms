// File:    QuestionBaseLogic.cs
// Author:  Administrator
// Created: 2011年12月16日 10:27:19
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
    /// 实现对试题功能逻辑的基本实现
    ///</summary>
    public class QuestionBaseLogic : IMessageSourceAware, IInitializingObject, ICommonQuestionLogic
    {

        private static string Err_QuestionBaseLogic_Data_Invalid = "ItemBank.QuestionBaseLogic.Data.Invalid";
        private static string Err_QuestionBaseLogic_Not_Found = "ItemBank.QuestionBaseLogic.Not.Found";
        private static string Err_QuestionBaseLogic_Invalid_Parameter = "ItemBank.QuestionBaseLogic.Invalid.Parameter";
        #region --属性--
        /// <summary>
        /// 试题答题反馈的逻辑接口
        /// </summary>
        public IQuestionFeedbackLogic QuestionFeedbackLogic { get; set; }
        /// <summary>
        /// 试题选项反馈的逻辑接口
        /// </summary>
        public IOptionFeedbackLogic OptionFeedbackLogic { get; set; }
        /// <summary>
        /// 试题解题思路的逻辑接口
        /// </summary>
        public IQuestionExtendLogic QuestionExtendLogic { get; set; }
        /// <summary>
        /// 试题题干部分的逻辑接口
        /// </summary>
        public IQuestionLogic QuestionTitleLogic { get; set; }
        #endregion

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
        /// 添加一个试题
        ///</summary>
        /// <param name="question">要添加的试题的基类。必须是系统支持的几种试题的具体类实例。</param>
        /// <returns>包含有ID信息的试题类</returns>
        public CommonQuestion AddQuestion(CommonQuestion questionItem)
        {
            //对要添加的试题进行验证
            int nValid = 0;
            string sErrMsg = "";
            nValid = ValidCommonQuestion(questionItem, out sErrMsg);
            if (nValid != 0)
            {
                throw new ETMS.AppContext.BusinessException(Err_QuestionBaseLogic_Data_Invalid, new Exception(sErrMsg));
            }

            //添加试题
            System.Guid questionID = this.QuestionTitleLogic.AddQuestion(questionItem.Question);
            ////添加试题的解题思路
            //if (questionItem.QuestionExtend != null)
            //{
            //    questionItem.QuestionExtend.QuestionID = questionID;
            //    this.QuestionExtendLogic.Add(questionItem.QuestionExtend);
            //}
            ////添加试题反馈
            //if (questionItem.QuestionFeedback != null)
            //{
            //    questionItem.QuestionFeedback.QuestionID = questionID;
            //    questionItem.QuestionFeedback.FeedbackID =
            //        this.QuestionFeedbackLogic.Add(questionItem.QuestionFeedback);
            //}
            ////添加选择反馈
            //if (questionItem.LstOptionFeedbacks != null && questionItem.LstOptionFeedbacks.Count > 0)
            //{
            //    //添加多个选项
            //    foreach (OptionFeedback feedback in questionItem.LstOptionFeedbacks)
            //    {
            //        feedback.QuestionID = questionID;
            //        feedback.OptionFeedbackID = this.OptionFeedbackLogic.Add(feedback);
            //    }
            //}

            return questionItem;
        }
        /// <summary>
        /// 验证通用试题对象是否合法
        /// </summary>
        /// <param name="questionItem">要验证的试题对象</param>
        /// <param name="sErrMsg">如果不合法，返回的错误信息</param>
        /// <returns></returns>
        private int ValidCommonQuestion(CommonQuestion questionItem, out string sErrMsg)
        {
            sErrMsg = "";
            if (questionItem == null)
            {
                sErrMsg = "试题信息为NULL";
                return 1;
            }

            #region --//验证选项反馈--
            if (questionItem.QuestionType == QuestionType.SingleChoice || questionItem.QuestionType == QuestionType.MultipleChoice)
            { }
            else
            {
                if (questionItem.LstOptionFeedbacks != null && questionItem.LstOptionFeedbacks.Count > 0)
                {
                    sErrMsg = "只有单选、多选题可存在选项反馈，其它题型不支持选项反馈";
                    return 2;
                }
            }
            #endregion

            #region --//验证各部分的ID是否一致--
            if (questionItem.QuestionExtend != null)
            {
                if (questionItem.QuestionExtend.QuestionID != questionItem.Question.QuestionID)
                {
                    sErrMsg = "解题思路中的试题ID与题面中的试题ID不一致";
                    return 3;
                }
            }
            if (questionItem.QuestionFeedback != null)
            {
                if (questionItem.QuestionFeedback.QuestionID != questionItem.Question.QuestionID)
                {
                    sErrMsg = "答题反馈中的试题ID与题面中的试题ID不一致";
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
                    sErrMsg = "选项反馈中的试题ID与题面中的试题ID不一致";
                    return 3;
                }
            }
            #endregion

            #region --//验证选项反馈中是否存在相同的部分--
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
                //可以采用另一种写法
                //var LstResult = from tmp in
                //                    (
                //                        from optionFeedback in questionItem.LstOptionFeedbacks
                //                        group optionFeedback by optionFeedback.Options)
                //                where tmp.Count() > 1
                //                select tmp;

                if (LstResult.Count() > 0)
                {
                    sErrMsg = "选项反馈中存在重复的设置";
                    return 4;
                }
            }
            #endregion

            return 0;
        }
        ///<summary>
        /// 更新一个试题基本信息
        ///</summary>
        ///<remarks>
        ///1,更新的试题必须已存在，如果不存在，无法进行更新；<br></br>
        ///2,更新时，可以为试题增加答案反馈，选项反馈和解题思路；<br></br>
        ///3,如果解题思路，答案反馈，选项反馈已存在的部分将更新；<br></br>
        ///</remarks>
        /// <param name="questionID">要更新的试题ID</param>
        /// <param name="newQuestion">更新的试题实体（必须为试题类型的实体）</param>
        public void Update(System.Guid questionID, CommonQuestion newQuestion)
        {
            #region --验证要更新的试题对象--
            if (newQuestion == null || newQuestion.Question == null || newQuestion.Question.QuestionID != questionID)
            {
                throw new ETMS.AppContext.BusinessException(Err_QuestionBaseLogic_Invalid_Parameter, new Exception("试题ID与试题对象中的试题ID不一致"));
            }
            //检查要更新的试题对象是否各项值一致
            int nValid = 0;
            string sErrMsg = "";
            nValid = this.ValidCommonQuestion(newQuestion, out sErrMsg);
            if (nValid != 0)
            {
                throw new ETMS.AppContext.BusinessException(Err_QuestionBaseLogic_Data_Invalid, new Exception(sErrMsg));
            }
            #endregion

            //得到试题的原来信息
            CommonQuestion OldQuestion = this.GetByID(questionID);
            if (OldQuestion == null || OldQuestion.Question == null)
            {
                throw new ETMS.AppContext.BusinessException(Err_QuestionBaseLogic_Not_Found, new Exception("试题已不存在，无法更新"));
            }

            //题干的更新
            this.QuestionTitleLogic.Update(questionID, newQuestion.Question);
            ////解题思路的更新
            //this.QuestionExtendLogic.Update(newQuestion.QuestionExtend);
            //#region --//答案反馈的更新--
            //if (OldQuestion.QuestionFeedback != null && newQuestion.QuestionFeedback != null)
            //{
            //    //如果原来已存在答案反馈，
            //    newQuestion.QuestionFeedback.FeedbackID = OldQuestion.QuestionFeedback.FeedbackID;
            //    //更新
            //    this.QuestionFeedbackLogic.Update(newQuestion.QuestionFeedback);
            //}
            //if (OldQuestion.QuestionFeedback != null && newQuestion.QuestionFeedback == null)
            //{
            //    //原来要删除掉
            //    this.QuestionFeedbackLogic.Delete(OldQuestion.QuestionFeedback.FeedbackID);
            //}
            //if (OldQuestion.QuestionFeedback == null && newQuestion.QuestionFeedback != null)
            //{
            //    //添加新的答题反馈
            //    this.QuestionFeedbackLogic.Add(newQuestion.QuestionFeedback);
            //}
            //#endregion

            //#region -- //选项反馈的更新--
            //IList<OptionFeedback> LstOptionFeedbacks = null;
            ////得到要删除的选项反馈
            //LstOptionFeedbacks = this.GetListDeleted(OldQuestion.LstOptionFeedbacks, newQuestion.LstOptionFeedbacks);
            //if (LstOptionFeedbacks != null && LstOptionFeedbacks.Count > 0)
            //{
            //    //将多的给删除掉
            //    LstOptionFeedbacks.Select(
            //        x =>
            //        {
            //            this.OptionFeedbackLogic.Delete(x.OptionFeedbackID);
            //            return x;
            //        }).ToList<OptionFeedback>();
            //}
            ////得到要更新的选项反馈
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
            ////得到要新增的选项反馈
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
        /// 得到需要新添加的
        /// </summary>
        /// <param name="LstOld"></param>
        /// <param name="LstNew"></param>
        /// <returns></returns>
        private IList<OptionFeedback> GetListNew(IList<OptionFeedback> LstOld,
            IList<OptionFeedback> LstNew)
        {
            if (LstOld == null || LstOld.Count <= 0 || LstNew == null || LstNew.Count <= 0)
                return LstNew;
            //得到已存在
            var LstTmp = from item in LstNew
                         join old in LstOld
                         on item.OptionFeedbackID equals old.OptionFeedbackID
                         select item;
            //得到不存在的
            IList<OptionFeedback> LstResult = LstNew.Except(LstTmp).ToList<OptionFeedback>();
            return LstResult;
        }
        /// <summary>
        /// 得到需要删除的
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

            //得到已存在
            var LstTmp = from item in LstOld
                         join old in LstNew
                         on item.OptionFeedbackID equals old.OptionFeedbackID
                         select item;
            //得到不存在的
            IList<OptionFeedback> LstResult = LstOld.Except(LstTmp).ToList<OptionFeedback>();
            return LstResult;
        }
        /// <summary>
        /// 得到需要更新的
        /// </summary>
        /// <param name="LstOld"></param>
        /// <param name="LstNew"></param>
        /// <returns></returns>
        private IList<OptionFeedback> GetListUpdated(IList<OptionFeedback> LstOld,
        IList<OptionFeedback> LstNew)
        {
            if (LstOld == null || LstOld.Count <= 0 || LstNew == null || LstNew.Count <= 0)
                return null;
            //得到已存在
            var LstTmp = (from item in LstNew
                          join old in LstOld
                          on item.OptionFeedbackID equals old.OptionFeedbackID
                          select item).ToList<OptionFeedback>();
            return LstTmp;
        }
        ///<summary>
        /// 删除指定的试题
        ///</summary>
        /// <param name="questionID">要删除的试题的ID</param>
        [Transaction]
        public void Delete(System.Guid questionID)
        {
            CommonQuestion questionItem = this.GetByID(questionID);
            if (questionItem == null || questionItem.Question == null)
                return;
            //调用逻辑层，将每一部分删除
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
        /// 得到某一试题。会根据试题的类型得到具体试题类型的实例。
        ///</summary>
        /// <param name="questionID">要获取的试题的ID</param>
        public CommonQuestion GetByID(System.Guid questionID)
        {
            CommonQuestion questionItem = new CommonQuestion();
            //得到试题各个方面的信息
            Question question = this.QuestionTitleLogic.GetByID(questionID);
            if (question == null)
                return null;
            questionItem.Question = question;
            questionItem.QuestionType = question.QuestionType;
            //得到答题反馈
            questionItem.QuestionFeedback = this.QuestionFeedbackLogic.GetFeedback(questionID);
            questionItem.LstOptionFeedbacks = this.OptionFeedbackLogic.GetFeedback(questionID);
            questionItem.QuestionExtend = this.QuestionExtendLogic.GetQuestionExtend(questionID);

            return questionItem;
        }
        /// <summary>
        /// 修改答案字符串
        /// </summary>
        /// <param name="questionID">试题ID</param>
        /// <param name="answer">答案字符串</param>
        public void UpdateAnswers(Guid questionID, string answer)
        {
            this.QuestionTitleLogic.UpdateAnswers(questionID, answer);
        }
    }
}
