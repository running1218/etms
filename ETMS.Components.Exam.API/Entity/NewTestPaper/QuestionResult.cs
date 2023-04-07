using System;

namespace ETMS.Components.Exam.API.Entity.NewTestPaper
{
    public class QuestionResult
    {
        public Guid UserExamID { get; set; }

        public Guid QuestionID { get; set; }

        public string UserAnswer { get; set; }

        public decimal UserScore { get; set; }
    }
}
