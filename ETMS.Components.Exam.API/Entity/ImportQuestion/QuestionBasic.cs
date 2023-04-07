using System.Collections.Generic;

namespace ETMS.Components.Exam.API.Entity.ImportQuestion
{
    public class QuestionBasic
    {
        private string questionID;

        public string QuestionID
        {
            get { return questionID; }
            set { questionID = value; }
        }

        private string questionTitle;

        public string QuestionTitle
        {
            get { return questionTitle; }
            set { questionTitle = value; }
        }

        private short difficult;

        public short Difficult
        {
            get { return difficult; }
            set { difficult = value; }
        }


        public List<QuestionExpansion> Qreplenish
        {
            get;
            set;
        }

        public string Msg
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }
    }
}
