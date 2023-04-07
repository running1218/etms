using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Scrom.Implement.DAL
{
    public class CoreDataAccess
    {

        /// <summary>
        /// ������Դ��š��û���� ��öϵ�����
        /// </summary>
        /// <param name="ResourceID">��Դ���</param>
        /// <param name="UserID">�û����</param>
        /// <returns></returns>
        public DataTable GetCmiCoreByCRU(Guid ResourceID,Guid itemCourseResID, int UserID)
        {   
            string commandName = "dbo.Pr_sco_Cmi_Core_GetByRU";
            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier)
            };

            parms[0].Value = ResourceID;
            parms[1].Value = UserID;
            parms[2].Value = itemCourseResID;

            return SqlHelper.ExecuteDataset(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CmiCoreID"></param>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="StatusCode"></param>
        /// <param name="ExitCode"></param>
        /// <param name="ScoreRaw"></param>
        /// <param name="LessonLocation"></param>
        /// <param name="SessionTime"></param>
        public void CmiCoreUpdate(int CmiCoreID, Guid ResourceID,Guid itemCourseResID, int UserID, string StatusCode, string ExitCode,
            string ScoreRaw, string LessonLocation, int SessionTime)
        {

            string commandName = "dbo.Pr_Sco_Cmi_Core_Update";

            SqlParameter[] parms = 
				{				 
					new SqlParameter( "@CmiCoreID" , SqlDbType.Int),
                    new SqlParameter( "@ResourceID" , SqlDbType.UniqueIdentifier),
                    new SqlParameter( "@UserID" , SqlDbType.Int),
                    new SqlParameter( "@StatusCode" , SqlDbType.NVarChar, 50),
                    new SqlParameter( "@ExitCode" , SqlDbType.NVarChar, 50),
                    new SqlParameter( "@ScoreRaw" , SqlDbType.NVarChar, 10),
                    new SqlParameter( "@LessonLocation" , SqlDbType.NVarChar, 200),
                    new SqlParameter( "@SessionTime" , SqlDbType.Int),
                    new SqlParameter( "@ItemCourseResID",SqlDbType.UniqueIdentifier)
				};

            parms[0].Value = CmiCoreID;
            parms[1].Value = ResourceID;
            parms[2].Value = UserID;
            parms[3].Value = StatusCode;
            parms[4].Value = ExitCode;
            parms[5].Value = ScoreRaw;
            parms[6].Value = LessonLocation;
            parms[7].Value = SessionTime;
            parms[8].Value = itemCourseResID;

            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms);
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
        public void CmiCoreInsert(Guid ResourceID,Guid itemCourseResID, int UserID, string StatusCode, string ExitCode, string ScoreRaw, string lessonlocation, int SessionTime)
        {
            string commandName = "dbo.Pr_sco_Cmi_Core_Insert";

            SqlParameter[] parms ={ 
              new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
              new SqlParameter("@UserID",SqlDbType.Int),
              new SqlParameter("@StatusCode",SqlDbType.NVarChar,50),
              new SqlParameter("@ExitCode",SqlDbType.NVarChar,50),
              new SqlParameter("@ScoreRaw",SqlDbType.NVarChar,10),
              new SqlParameter("@lessonlocation",SqlDbType.NVarChar,200),
              new SqlParameter("@SessionTime",SqlDbType.Int),
              new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier),
            };

            parms[0].Value = ResourceID;
            parms[1].Value = UserID;
            parms[2].Value = StatusCode;
            parms[3].Value = ExitCode;
            parms[4].Value = ScoreRaw;
            parms[5].Value = lessonlocation;
            parms[6].Value = SessionTime;
            parms[7].Value = itemCourseResID;

            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// ������Դ��š��û���� �޸Ķϵ������˳�״̬
        /// </summary>
        /// <param name="ResourceID">��Դ���</param>
        /// <param name="UserID">�û����</param>
        /// <param name="ExitCode">�˳�״̬</param>
        public void CmiCoreUpdateExitCodeByCRU(Guid ResourceID,Guid itemCourseResID, int UserID, string ExitCode)
        {
            string commandName = "dbo.Pr_sco_Cmi_Core_UpdateExitCodeByRU";
            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@ExitCode",SqlDbType.NVarChar,50),
                new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier)
            };
            parms[0].Value = ResourceID;
            parms[1].Value = UserID;
            parms[2].Value = ExitCode;
            parms[3].Value = itemCourseResID;

            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// ������Դ��š��û���� �޸ĳɼ�
        /// </summary>
        /// <param name="ResourceID">��Դ���</param>
        /// <param name="UserID">�û����</param>
        /// <param name="ScoreRaw">�ɼ�</param>
        public void CmiCoreUpdateScoreRawByRUS(Guid ResourceID,Guid itemCourseResID, int UserID, string ScoreRaw)
        {
            string commandName = "dbo.Pr_sco_Cmi_CoreUpdateScoreRawByRUS";
            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@ScoreRaw",SqlDbType.NVarChar,10),
                new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier)
            };

            parms[0].Value = ResourceID;
            parms[1].Value = UserID;
            parms[2].Value = ScoreRaw;
            parms[3].Value = itemCourseResID;
            

            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// ������Դ��š��û���� �޸�������λ��
        /// </summary>
        /// <param name="ResourceID">��Դ���</param>
        /// <param name="UserID">�û����</param>
        /// <param name="Lessonlocation">������λ��</param>
        public void CmiCoreUpdateLessonlocationByRUL(Guid ResourceID,Guid itemCourseResID, int UserID, string Lessonlocation)
        {
            string commandName = "dbo.Pr_sco_Cmi_CoreUpdateLessonlocationByRUL";
            SqlParameter[] parms ={ 
               new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
               new SqlParameter("@UserID",SqlDbType.Int),
               new SqlParameter("@lessonlocation",SqlDbType.NVarChar,10),
               new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier)
            };

            parms[0].Value = ResourceID;
            parms[1].Value = UserID;
            parms[2].Value = Lessonlocation;
            parms[3].Value = itemCourseResID;

            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms);
        }
        
        /// <summary>
        /// �����û����ѧϰ״̬
        /// </summary>
        /// <param name="ResourceID">��Դ���</param>
        /// <param name="UserID">�û����</param>
        /// <param name="Status">���ѧϰ״̬</param>
        public void SetCoreLessonStatus(Guid ResourceID, Guid itemCourseResID,int UserID, string Status)
        {
            string commandName = "dbo.Pr_Sco_Cmi_Core_UpdateByRUS";
            SqlParameter[] parms ={ 
              new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
              new SqlParameter("@UserID",SqlDbType.Int),
              new SqlParameter("@StatusCode",SqlDbType.NVarChar,50),
              new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier)
            };

            parms[0].Value = ResourceID;
            parms[1].Value = UserID;
            parms[2].Value = Status;
            parms[3].Value = itemCourseResID;
            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// �����û�ѧϰʱ��
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="SessionTime"></param>
        public void SetCoreSessionTime(Guid ResourceID,Guid itemCourseResID, int UserID, int SessionTime)
        {
            string commandName = "dbo.Pr_Sco_Cmi_Core_UpdateSessionTime";
            SqlParameter[] parms ={ 
              new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
              new SqlParameter("@UserID",SqlDbType.Int),
              new SqlParameter("@SessionTime",SqlDbType.Int),
              new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier)
            };

            parms[0].Value = ResourceID;
            parms[1].Value = UserID;
            parms[2].Value = SessionTime;
            parms[3].Value = itemCourseResID;
            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, parms);
        }
    }
}
