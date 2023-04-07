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
    public class SingleQuestion : IQuestion
    {
        private Guid questionBankID;

        private List<QuestionBasic> QBList;

        private string msgError;
        private bool orpateValuate;

        protected string[] letterlist = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        public SingleQuestion(Import_AQuestion IA, Guid QuestionBankID)
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
            QuestionBase eQuestion = new SingleChoiceQuestion();
            IQuestionServiceLogic questionLogic = ServiceRepository.SingleChoiceQuestionService;
            errCount = 0;
            var p = from s in qbList where s.Msg == "" select s;
            foreach (QuestionBasic qb in p)
            {
                try
                {
                    eQuestion = setQuestionEntity((SingleChoiceQuestion)eQuestion, qb);
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

        private SingleChoiceQuestion setQuestionEntity(SingleChoiceQuestion eQuestion, QuestionBasic qb)
        {
            eQuestion.CommonQuestion = new CommonQuestion();
            eQuestion.CommonQuestion.Question = new Question();
            //eQuestion.CommonQuestion.Question.QuestionID = Guid.NewGuid();
            eQuestion.CommonQuestion.Question.QuestionBankID = questionBankID;
            eQuestion.CommonQuestion.Question.ParentQuestionID = Guid.Empty;
            eQuestion.CommonQuestion.Question.ObjectID = (short)ETMS.AppContext.UserContext.Current.OrganizationID;
            eQuestion.CommonQuestion.Question.QuestionType = QuestionType.SingleChoice;
            eQuestion.CommonQuestion.QuestionType = QuestionType.SingleChoice;
            eQuestion.CommonQuestion.Question.QuestionTitle = qb.QuestionTitle;
            eQuestion.CommonQuestion.Question.Difficulty = qb.Difficult;
            eQuestion.Options = new List<QuestionOption>();
            int i = 0;

            foreach (QuestionExpansion item in qb.Qreplenish)
            {
                QuestionOption option = new QuestionOption();
                option.QuestionID = eQuestion.CommonQuestion.Question.QuestionID;
                option.OptionID = Guid.NewGuid();
                //option.QuestionID = eQuestion.CommonQuestion.Question.QuestionID;
                option.OptionCode = letterlist[i];
                option.OptionContent = item.OptionContent;
                eQuestion.Options.Add(option);
                if (item.Answer)
                {
                    eQuestion.Answer = new SingleChoiceAnswer(eQuestion.Options[i]);
                }
                i++;
            }
            return eQuestion;
        }

        public List<QuestionBasic> GetQbList
        {
            get { return QBList; }
            set { QBList = value; }
        }
    }
}
