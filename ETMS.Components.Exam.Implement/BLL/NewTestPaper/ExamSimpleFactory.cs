namespace ETMS.Components.Exam.Implement.BLL.NewTestPaper
{
    public class ExamHomeworkFactory
    {
        public static AExamHomework Create(string type)
        {
            if (type == "ExamLogic")
            {
                return new ExamLogic();
            }
            else if (type == "HomeworkLogic")
            {
                return new HomeworkLogic();
            }
            else
            {
                return null;
            }
        }
    }
}
