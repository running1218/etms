namespace ETMS.Components.Exam.API.Entity.ImportQuestion
{
    public class QuestionExpansion
    {
        private bool answer;

        public bool Answer
        {
            get { return answer; }
            set { answer = value; }
        }

        private string option;

        public string Option
        {
            get { return option; }
            set { option = value; }
        }

        private string optionContent;

        public string OptionContent
        {
            get { return optionContent; }
            set { optionContent = value; }
        }

        public string AnswerContent
        {
            get;
            set;
        }
    }
}
