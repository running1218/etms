using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Utility.Logging;

using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.StudentCourse;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;

namespace ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse
{
    public partial class Sty_StudentCourseLogic
    {

        /// <summary>
        /// 批量设置成绩
        /// </summary>
        /// <param name="hashStudentGrade">存放成绩的哈希表【StudentCourseID】【成绩】</param>
        /// <param name="modifyUser">成绩录入人，既操作员</param>
        public void BatchSetGrade(Hashtable hashStudentGrade, string modifyUser)
        {
            System.Collections.IDictionaryEnumerator myEnumerator = hashStudentGrade.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                Guid studentCourseID = new Guid(myEnumerator.Key.ToString());
                try
                {
                    decimal grade = decimal.Parse(myEnumerator.Value.ToString());
                    Sty_StudentCourse entity = DAL.GetById(studentCourseID);
                    entity.ModifyUser = modifyUser;
                    entity.ModifyTime = System.DateTime.Now;
                    entity.SumGrade = grade;

                    //取培训项目课程信息
                    Tr_ItemCourseLogic logicItemCourse = new Tr_ItemCourseLogic();
                    Tr_ItemCourse entityItemCourse = logicItemCourse.GetById(entity.TrainingItemCourseID);
                    //设置项目课程为“已经录入成绩”
                    if (entityItemCourse.IsInputGrade == false)
                    {
                        entityItemCourse.IsInputGrade = true;
                        logicItemCourse.Update(entityItemCourse);
                    }
                    if (grade >= entityItemCourse.PassLine)
                        entity.IsPass = true;
                    else
                        entity.IsPass = false;
                    entity.Remark = ETMS.AppContext.UserContext.Current.UserID.ToString() + DateTime.Now.ToString();
                    //保存
                    Save(entity, OperationAction.Edit);

                }
                catch { }
            }
        }



        /// <summary>
        /// 保存操作
        /// </summary>
        public void Save(Sty_StudentCourse entity, OperationAction action)
        {
            try
            {
                if (action == OperationAction.Add)
                    Add(entity);
                else if (action == OperationAction.Edit)
                    Update(entity);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Sty_StudentCourseCode", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("StudentStudy.Sty_StudentCourse.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Sty_StudentCourseName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("StudentStudy.Sty_StudentCourse.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }

        /// <summary>
        /// 设置学生选课的课程状态
        /// </summary>
        /// <param name="studentCourseID">学生选课ID</param>
        /// <param name="isUse">是否启用</param>
        public void SetStudentCourseIsUse(Guid studentCourseID, int isUse)
        {
            Sty_StudentCourse entity = DAL.GetById(studentCourseID);
            entity.IsUse = isUse;
            Save(entity, OperationAction.Edit);
        }




        /// <summary>
        /// 删除
        /// </summary>
        protected void doRemove(Guid studentCourse)
        {
            try
            {
                DAL.Remove(studentCourse);
                //记录删除日志（根据ID删除）
                BizLogHelper.Operate(studentCourse, "删除");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("StudentStudy.Sty_StudentCourse.DataUsed");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }



        /// <summary>
        /// 获取项目报名学员中没有选择相关培训项目课程的学员
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <returns></returns>
        public DataTable GetStudentForNoSelectCourse(Guid trainingItemCourseID)
        {
            return DAL.GetStudentForNoSelectCourse(trainingItemCourseID);
        }

        /// <summary>
        /// 获取培训项目课程下学员总数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <returns></returns>
        public int GetItemCourseStudentNum(Guid trainingItemCourseID)
        {
            return DAL.GetItemCourseStudentNum(trainingItemCourseID);
        }
        /// <summary>
        /// 获取项目报名学员中已经选择相关培训项目课程的学员
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <returns></returns>
        public DataTable GetStudentForSelectedCourse(Guid trainingItemCourseID)
        {
            return DAL.GetStudentForSelectedCourse(trainingItemCourseID);
        }

        /// <summary>
        /// 增加学员选课
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <param name="studentSignupIDs">项目学员报名编号列表</param>
        /// <param name="createUserID">创建用户编号</param>
        /// <param name="createUser">创建用户姓名</param>
        public void AddStudentSelectCourse(Guid trainingItemCourseID, Guid[] studentSignupIDs, int createUserID, string createUser)
        {
            string studentSignupIDList = string.Empty;
            for (int i = 0; i < studentSignupIDs.Length; i++)
            {
                if (string.IsNullOrEmpty(studentSignupIDList))
                {
                    studentSignupIDList = studentSignupIDs[i].ToString();
                }
                else
                {
                    studentSignupIDList = string.Format("{0},{1}", studentSignupIDList, studentSignupIDs[i].ToString());
                }
            }
            AddStudentSelectCourse(trainingItemCourseID, studentSignupIDList, createUserID, createUser);

        }

        /// <summary>
        /// 学生前台选课报名
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="trainingItemCourseID"></param>
        public void SignUpTraniningItemCourse(Guid trainingItemID, Guid trainingItemCourseID)
        {
            Guid studentSignupID = new Sty_StudentSignupLogic().TrainingItemSelfSignUp(trainingItemID);
            AddStudentSelectCourse(trainingItemCourseID, new Guid[] { studentSignupID }, UserContext.Current.UserID, UserContext.Current.RealName);
        }

        /// <summary>
        /// 增加学员选课程
        /// </summary>
        /// <param name="trainingItemCourseIDArray">培训项目课程ID数组</param>
        /// <param name="studentSignupID">项目学员报名ID</param>
        /// <param name="createUserID">创建用户编号</param>
        /// <param name="createUser">创建用户姓名</param>
        public void AddCourseSelectStudent(Guid[] trainingItemCourseIDArray, Guid studentSignupID, int createUserID, string createUser)
        {
            for (int i = 0; i < trainingItemCourseIDArray.Length; i++)
            {
                try
                {
                    AddStudentSelectCourse(trainingItemCourseIDArray[i], studentSignupID.ToString(), createUserID, createUser);
                }
                catch
                {

                }
            }
        }







        /// <summary>
        /// 增加学员选课:按培训项目课程全部选课
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <param name="createUserID">创建用户编号</param>
        /// <param name="createUser">创建用户姓名</param>
        public void AddStudentSelectCourse(Guid trainingItemCourseID, int createUserID, string createUser)
        {
            AddStudentSelectCourse(trainingItemCourseID, "", createUserID, createUser);
        }

        /// <summary>
        /// 增加学员选课:批量选课
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <param name="studentSignupIDList">项目学员报名编号列表</param>
        /// <param name="createUserID">创建用户编号</param>
        /// <param name="createUser">创建用户姓名</param>
        public void AddStudentSelectCourse(Guid trainingItemCourseID, string studentSignupIDList, int createUserID, string createUser)
        {
            DAL.AddStudentSelectCourse(trainingItemCourseID, studentSignupIDList, createUserID, createUser);

        }


        /// <summary>
        /// 查询某个培训项目课程可选课的已经报名学员ID，为学员选课用
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="type">--范围:
        /// --0：整个项目已经报名的学员
        /// --1：未在本项目选课且在其他项目也没有选课的学员
        /// --2：未在本项目选课的整个项目已经报名学员</param>
        /// <returns></returns>
        public DataTable GetNoSelectCourseStudentFromStudentSignup(Guid trainingItemCourseID, int type)
        {
            return DAL.GetNoSelectCourseStudentFromStudentSignup(trainingItemCourseID, type);
        }


        /// <summary>
        /// 增加学员选课:将整个培训项目的所有报名学员，选择指定的培训项目课程（学员都选择该门课程）
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程编号</param>
        /// <param name="addType">添加方式，及添加学员的范围:
        /// 0：整个项目已经报名的学员
        /// 1：未在本项目选课且在其他项目也没有选课的学员
        /// 2：未在本项目选课的整个项目已经报名学员</param>
        /// <param name="createUserID">创建用户编号</param>
        /// <param name="createUser">创建用户姓名</param>
        public void AddAllTrainingItemStudentSignupToStudentCourse(Guid trainingItemCourseID, int addType, int createUserID, string createUser)
        {
            switch (addType)
            {
                case 0://整个项目已经报名的学员
                    AddStudentSelectCourse(trainingItemCourseID, "", createUserID, createUser);
                    break;
                case 1://未在本项目选课且在其他项目也没有选课的学员
                    DataTable dt = GetNoSelectCourseStudentFromStudentSignup(trainingItemCourseID, addType);
                    foreach(DataRow row in dt.Rows)
                    {
                        try
                        {
                            string studentSignupID = row[0].ToString();
                            AddStudentSelectCourse(trainingItemCourseID, studentSignupID, createUserID, createUser);
                        }
                        catch
                        {
                        }
                    }
                    break;
            }

        }



        /// <summary>
        /// 删除学员选课
        /// </summary>
        /// <param name="studentCourses">学员选课编号列表</param>
        public void DeleteStudentSelectCourse(Guid[] studentCourses)
        {
            string studentCourseList = string.Empty;
            for (int i = 0; i < studentCourses.Length; i++)
            {
                if (string.IsNullOrEmpty(studentCourseList))
                {
                    studentCourseList = studentCourses[i].ToString();
                }
                else
                {
                    studentCourseList = string.Format("{0},{1}", studentCourseList, studentCourses[i].ToString());
                }
            }
            DeleteStudentSelectCourse(studentCourseList);
        }

        /// <summary>
        /// 删除学员选课
        /// </summary>
        /// <param name="studentCourseList">学员选课编号列表</param>
        public void DeleteStudentSelectCourse(string studentCourseList)
        {
            DAL.DeleteStudentSelectCourse(studentCourseList);
        }

        /// <summary>
        /// 跟据项目ID删除项目课程下所有的学员
        /// </summary>
        /// <param name="TrainingItemID">项目ID</param>
        public void DeleteStudentCourseByTrainingItemID(Guid TrainingItemID)
        {
            DAL.DeleteStudentCourseByTrainingItemID(TrainingItemID);
        }

        /// <summary>
        /// 取消学员报名（前台）
        /// </summary>
        /// <param name="studentSignupID"></param>
        /// <param name="trainingItemCourseID"></param>
        public void CancelStudentSignCourse(Guid studentSignupID, Guid trainingItemCourseID)
        {
            DAL.CancelStudentSignCourse(studentSignupID, trainingItemCourseID);
        }

        /// <summary>
        /// 获取学员全部课程列表
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <returns></returns>
        public DataTable GetStudentAllCourseByUserID(string CourseName, int userID)
        {
            return GetStudentCourseByUserID(CourseName,userID, 0);
        }

        public List<Tr_ItemCourse> GetStudentAllCourseByUserID(string CourseName,int pageIndex, int pageSize, out int totalRecordCount)
        {
            var source = GetStudentAllCourseByUserID(CourseName,UserContext.Current.UserID).ToList<Tr_ItemCourse>();
            totalRecordCount = source.Count;
            return source.ToList<Tr_ItemCourse>().PageList<Tr_ItemCourse>(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取学员首页课程列表
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <returns></returns>
        public DataTable GetStudentCourseByUserID(int userID)
        {
            return GetStudentCourseByUserID(userID, 5);
        }

        /// <summary>
        /// 学员学习课程列表
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <param name="topNum">返回记录数量</param>
        /// <returns></returns>
        public DataTable GetStudentCourseByUserID(int userID, int topNum)
        {
            return DAL.GetStudentCourseByUserID(userID, topNum);
        }

        /// <summary>
        /// 学员学习课程列表
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <param name="topNum">返回记录数量</param>
        /// <returns></returns>
        public DataTable GetStudentCourseByUserID(string CourseName, int userID, int topNum)
        {
            return DAL.GetStudentCourseByUserID(CourseName,userID, topNum);
        }


        /// <summary>
        /// 获取学员培训项目列表
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <returns></returns>
        public DataTable GetTrainingItemListByUserID(int userID)
        {
            return DAL.GetTrainingItemListByUserID(userID);
        }

        /// <summary>
        /// 培训项目获取学员课程成绩
        /// </summary>
        /// <param name="trainingItemID">培训项目编号</param>
        /// <returns></returns>
        public DataTable GetCourseScoreByTrainingItemID(Guid trainingItemID)
        {
            return DAL.GetCourseScoreByTrainingItemID(trainingItemID);
        }

        /// <summary>
        /// 获取学员首页培训项目列表
        /// </summary>
        /// <returns></returns>
        public List<Tr_Item> GetStudyTrainingItemsTop()
        {
            return GetTrainingItemListByUserID(UserContext.Current.UserID).ToList<Tr_Item>().PageList<Tr_Item>(1, 5);
        }

        /// <summary>
        /// 获取所有组织机构项目列表
        /// </summary>
        /// <returns></returns>
        public List<Tr_Item> GetAllOrganizationTrainingItemList()
        {
            return DAL.GetAllOrganizationTrainingItemList().ToList<Tr_Item>();
        }

        /// <summary>
        /// 获取培训学员项目列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Tr_Item> GetStudyTrainingItems(int pageIndex, int pageSize, out int totalRecordCount)
        {
            var source = GetTrainingItemListByUserID(UserContext.Current.UserID).ToList<Tr_Item>();
            totalRecordCount = source.Count;
            return source.PageList<Tr_Item>(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取项目下课程信息
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <returns></returns>
        public List<Tr_ItemCourse> GetStudyTrainingItemCourse(string CourseName,Guid trainingItemID, int pageIndex, int pageSize, out int totalRecordCount)
        {
            var source = GetStudentAllCourseByUserID(CourseName,UserContext.Current.UserID).ToList<Tr_ItemCourse>().Where(t => t.TrainingItemID.Equals(trainingItemID)).ToList<Tr_ItemCourse>();
            totalRecordCount = source.Count;
            return source.PageList<Tr_ItemCourse>(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取学生的考试成绩列表
        /// from dbo.vw_StudentCourse a 
        /// inner join dbo.vw_ValidItemCourse b on a.TrainingItemCourseID=b.TrainingItemCourseID
        /// inner join dbo.vw_ValidStudent c on c.UserID = a.UserID
        /// inner join dbo.Res_Course d on d.CourseID = b.CourseID
        /// inner join dbo.Tr_Item e on e.TrainingItemID = b.TrainingItemID
        /// </summary>
        /// <param name="studentID">学生ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        /// <returns>DataTable</returns>
        public DataTable GetStudentCourseScoreList(int studentID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetStudentCourseScoreList(studentID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取学生的有“考试成绩”的列表,不包括已经归档的项目
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCourseScoreListToExamGrade(int studentID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += " AND e.ItemStatus!='90'";//不包括已经归档的项目
            return GetStudentCourseScoreList(studentID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }






        /// <summary>
        /// 获取某个学生的某个培训项目课程在线考试的次数
        /// </summary>
        /// <param name="studentID">学生ID</param>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetStudentCourseOnLineTestNum(int studentID, Guid trainingItemCourseID)
        {
            return DAL.GetStudentCourseOnLineTestNum(studentID, trainingItemCourseID);
        }


        /// <summary>
        /// 获取某个学生的所有课程学习资源
        /// </summary>
        /// <param name="studentID">学生ID</param>
        public DataTable GetStudentALLCourseResList(int studentID)
        {
            return DAL.GetStudentALLCourseResList(studentID);
        }


        /// <summary>
        /// 获取学生的某个培训项目课程的所有学习资源列表
        /// </summary>
        /// <param name="studentID">学生ID</param>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        public DataTable GetStudentCourseResListByTrainingItemCourseID(int studentID, Guid trainingItemCourseID)
        {
            return DAL.GetStudentCourseResListByTrainingItemCourseID(studentID, trainingItemCourseID);
        }

        
        /// <summary>
        /// 获取当前项目下学员选课信息
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetStudentCourseByTrainingItemIDAndUserID(Guid trainingItemID, int userID)
        {
            return DAL.GetStudentCourseByTrainingItemIDAndUserID(trainingItemID, userID);
        }
        /// <summary>
        /// 学员选课人数
        /// </summary>
        /// <param name="CourseID"></param>
        /// <returns></returns>
        public Int32 GetStudentCourseUserTotal(Guid CourseID)
        {
            return DAL.GetStudentCourseUserTotal(CourseID);
        }
    }
}
