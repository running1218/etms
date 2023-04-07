//*******************************************************************
//  
// 文件名(File Name):                        MuiltpleQuestion.cs
//
// 数据表(Tables):                           Nothing
//
// 作者(Author):                             JunYi Hu
//
// 日期(Create Date):                        2013.3.18
//
// 修改记录(Revision History):
//          R1:
//             修改作者：
//             修改日期：
//             修改理由：
//
//
//
//*******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Components.Exam.API.Entity.ImportQuestion;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;
using ETMS.Components.Exam.API;

namespace ETMS.Components.Basic.Implement.BLL.Import
{
    public class MuiltpleQuestion : IQuestion
    {
        private List<QuestionBasic> QBList;
        private Guid questionBankID;

        private string msgError;
        private bool orpateValuate;



        public List<QuestionBasic> GetQbList
        {
            get { return QBList; }
            set { QBList = value; }
        }

        protected string[] letterlist = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        public MuiltpleQuestion(Import_AQuestion IA, Guid QuestionBankID)
        {
            questionBankID = QuestionBankID;

            orpateValuate = IA.GetImportData(out QBList, out msgError);
        }

        public bool GetOrpateValuate(out string msg)
        {
            msg = msgError;
            return orpateValuate;
        }

        public List<QuestionBasic> GetQuestionList(List<QuestionBasic> qbList, out int errCount)
        {
            QuestionBase eQuestion = new MultipleChoiceQuestion();
            IQuestionServiceLogic questionLogic = ServiceRepository.MultipleChoiceQuestionService;
            errCount = 0;
            var p = from s in qbList where s.Msg == "" select s;
            foreach (QuestionBasic qb in p)
            {
                try
                {
                   
                    eQuestion = setQuestionEntity((MultipleChoiceQuestion)eQuestion, qb);
                    questionLogic.AddQuestion(eQuestion);
                }
                catch (Exception ex)
                {
                    errCount++;
                    qb.State = "失败";
                    string[] msg = ex.Message.Split('~');
                    qb.Msg = msg[msg.Length - 1];
                }
            }
            errCount += qbList.Count() - p.Count();
            return qbList;
        }


        private MultipleChoiceQuestion setQuestionEntity(MultipleChoiceQuestion eQuestion, QuestionBasic qb)
        {
            eQuestion.CommonQuestion = new CommonQuestion();
            eQuestion.CommonQuestion.Question = new Question();
            eQuestion.CommonQuestion.Question.QuestionBankID = questionBankID;
            eQuestion.CommonQuestion.Question.ParentQuestionID = Guid.Empty;
            eQuestion.CommonQuestion.Question.ObjectID = (short)ETMS.AppContext.UserContext.Current.OrganizationID;
            eQuestion.CommonQuestion.Question.QuestionType = QuestionType.MultipleChoice;
            eQuestion.CommonQuestion.QuestionType = QuestionType.MultipleChoice;
            eQuestion.CommonQuestion.Question.QuestionTitle = qb.QuestionTitle;
            eQuestion.CommonQuestion.Question.Difficulty = qb.Difficult;
            eQuestion.Options = new List<QuestionOption>();


            List<QuestionOption> AnswerOptions = new List<QuestionOption>();
            eQuestion.Answer = new MultipleChoiceAnswer(AnswerOptions);
            int i = 0;
            foreach (QuestionExpansion item in qb.Qreplenish)
            {
                if (string.IsNullOrWhiteSpace(item.OptionContent))
                {
                    throw new Exception("选项内容不能为空！");
                }
                QuestionOption option = new QuestionOption();
                option.QuestionID = eQuestion.CommonQuestion.Question.QuestionID;
                option.OptionID = Guid.NewGuid();
                option.OptionCode = letterlist[i];
                option.OptionContent = item.OptionContent;
                eQuestion.Options.Add(option);
                if (item.Answer)
                {
                    eQuestion.Answer.AnswerOptions.Add(option);
                }
                i++;
            }
            return eQuestion;
        }
    }
}
