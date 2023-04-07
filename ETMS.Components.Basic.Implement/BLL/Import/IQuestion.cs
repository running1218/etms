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

using System.Collections.Generic;
using ETMS.Components.Exam.API.Entity.ImportQuestion;

namespace ETMS.Components.Basic.Implement.BLL.Import
{
    public interface IQuestion
    {
        bool GetOrpateValuate(out string msg);
        List<QuestionBasic> GetQbList { get; set; }
        List<QuestionBasic> GetQuestionList(List<QuestionBasic> qbList,out int errCount);
    }
}
