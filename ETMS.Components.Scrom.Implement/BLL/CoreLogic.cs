using System;
using System.Data;
using ETMS.Components.Scrom.Implement.DAL;
namespace ETMS.Components.Scrom.Implement.BLL
{
    public partial class CoreLogic
    {
        private static readonly CoreDataAccess CoreDal = new CoreDataAccess();

        /// <summary>
        /// ��ȡ�û�ѧϰ����¼
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataTable GetCmiCore(Guid ResourceID,Guid ItemCourseResID, int UserID)
        {
            return CoreDal.GetCmiCoreByCRU(ResourceID, ItemCourseResID,UserID);
        }

        /// <summary>
        /// ��ȡ�û����ѧϰ״̬
        /// </summary>
        /// <param name="ResourceID">��ԴID</param>
        /// <param name="UserID">�û�ID</param>
        /// <returns></returns>
        public string GetCoreLessonStatus(Guid ResourceID,Guid ItemCourseResID, int UserID)
        {
            string StatusCode = "";
            DataTable dt = CoreDal.GetCmiCoreByCRU(ResourceID,ItemCourseResID, UserID);
            if (dt.Rows.Count > 0) {
                StatusCode = Convert.ToString(dt.Rows[0]["StatusCode"]);
            }
            return StatusCode;
        }

        //����ѧϰ״̬����ʼֵ��
        public void InsertCoreLessonStatus(Guid ResourceID,Guid ItemCourseResID, int UserID, string Status)
        {
            CmiCoreInsert(ResourceID, ItemCourseResID,UserID, Status, "", "", "", 0);
        }
        
        /// <summary>
        /// ���� �ϵ�����
        /// </summary>
        /// <param name="ResourceID">��Դ���</param>
        /// <param name="UserID">�û����</param>
        /// <param name="StatusCode">״̬����</param>
        /// <param name="ExitCode">ԭ�����</param>
        /// <param name="ScoreRaw">�÷�</param>
        /// <param name="lessonlocation">������λ��</param>
        /// <param name="SessionTime">ѧϰʱ��</param>
        public void CmiCoreInsert(Guid ResourceID,Guid ItemCourseResID, int UserID, string StatusCode, string ExitCode, string ScoreRaw, string lessonlocation, int SessionTime)
        {
            CoreDal.CmiCoreInsert(ResourceID, ItemCourseResID, UserID, StatusCode, ExitCode, ScoreRaw, lessonlocation, SessionTime);
        }


        /// <summary>
        /// �����û����ѧϰ״̬
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="Status"></param>
        public void SetCoreLessonStatus(Guid ResourceID,Guid ItemCourseResID, int UserID, string Status)
        {
            CoreDal.SetCoreLessonStatus(ResourceID,ItemCourseResID, UserID, Status);
        }

        /// <summary>
        /// ѧϰģʽ����ͨģʽ
        /// </summary>
        /// <returns></returns>
        public string GetCoreLessonMode()
        {   
            return "normal";
        }


        /// <summary>
        /// �����˳����
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="Exit"></param>
        public void SetCoreExit(Guid ResourceID,Guid ItemCourseResID, int UserID, string ExitCode)
        {
            CoreDal.CmiCoreUpdateExitCodeByCRU(ResourceID,ItemCourseResID, UserID, ExitCode);
        }

        /// <summary>
        /// ��ȡѧ��SCO�ɼ� 0-100 ���ֵ
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string GetCoreScoreRaw(Guid ResourceID,Guid ItemCourseResID, int UserID)
        {
            string ScoreRaw = "";
            DataTable dt = CoreDal.GetCmiCoreByCRU(ResourceID,ItemCourseResID, UserID);
            if (dt.Rows.Count > 0)
            {
                ScoreRaw = Convert.ToString(dt.Rows[0]["ScoreRaw"]);
            }
            return ScoreRaw;
        }

        /// <summary>
        /// ����ѧ��SCO�ɼ� 0-100 ���ֵ
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="ScoreRaw"></param>
        public void SetCoreScoreRaw(Guid ResourceID,Guid ItemCourseResID, int UserID, string ScoreRaw)
        {
            CoreDal.CmiCoreUpdateScoreRawByRUS(ResourceID,ItemCourseResID, UserID, ScoreRaw);
        }

        /// <summary>
        /// ��ȡ�ϵ���ѧ��ַ
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string GetCoreLessonLocation(Guid ResourceID,Guid ItemCourseResID, int UserID)
        {
            string lessonlocation = "";
            DataTable dt = CoreDal.GetCmiCoreByCRU(ResourceID,ItemCourseResID, UserID);
            if (dt.Rows.Count > 0)
            {
                lessonlocation = Convert.ToString(dt.Rows[0]["lessonlocation"]);
            }
            return lessonlocation;
        }

        /// <summary>
        /// ���öϵ���ѧ��ַ
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="LessonLocation"></param>
        public void SetCoreLessonLocation(Guid ResourceID, Guid ItemCourseResID, int UserID, string LessonLocation)
        {
            CoreDal.CmiCoreUpdateLessonlocationByRUL(ResourceID, ItemCourseResID, UserID, LessonLocation);
        }

        /// <summary>
        /// �����û�ѧϰʱ��
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="SessionTime"></param>
        public void SetCoreSessionTime(Guid ResourceID,Guid ItemCourseResID,int UserID, int SessionTime)
        {
            CoreDal.SetCoreSessionTime(ResourceID,ItemCourseResID, UserID, SessionTime);
        }
    }
}
