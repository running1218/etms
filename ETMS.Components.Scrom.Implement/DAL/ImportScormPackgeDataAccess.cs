using System;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;
using ETMS.Components.Scrom.API.Entity;


namespace ETMS.Components.Scrom.Implement.DAL
{
    public partial class ImportScormPackgeDataAccess
    {
        /// <summary>
        /// 删除临时表数据 z_Item，z_Resource
        /// </summary>
        public void del_Z_ItemANDz_Resource()
        {
            string commandName = "dbo.Pr_sco_Del_z_ItemANDz_Resource";
            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, null);
        }

        /// <summary>
        /// 导入课件时插入临时表dbo.z_Item
        /// </summary>
        /// <param name="courseNumber"></param>
        /// <param name="organizedID"></param>
        /// <param name="organizedName"></param>
        /// <param name="identifier"></param>
        /// <param name="identifierName"></param>
        /// <param name="identifierParent"></param>
        /// <param name="identifierref"></param>
        /// <param name="isvisible"></param>
        /// <param name="number"></param>
        public void Add_Z_Item(string coursewareID, string organizedID, string organizedName, string identifier, string identifierName, string identifierParent, string identifierref, int isvisible, int number)
        {
            string commandName = "dbo.Pr_sco_z_Item_insert";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@coursewareID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@organizedID",SqlDbType.NVarChar),
                new SqlParameter("@organizedName",SqlDbType.NVarChar),
                new SqlParameter("@identifier",SqlDbType.NVarChar),
                new SqlParameter("@identifierName",SqlDbType.NVarChar),
                new SqlParameter("@identifierParent",SqlDbType.NVarChar),
                new SqlParameter("@identifierref",SqlDbType.NVarChar),
                new SqlParameter("@isvisible",SqlDbType.Bit),
                new SqlParameter("@number",SqlDbType.Float),
            };

            param[0].Value = new Guid(coursewareID);
            param[1].Value = organizedID;
            param[2].Value = organizedName;
            param[3].Value = identifier;
            param[4].Value = identifierName;
            param[5].Value = identifierParent;
            param[6].Value = identifierref;
            param[7].Value = isvisible;
            param[8].Value = number;

            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, param);
        }

        /// <summary>
        /// 导入课件时插入临时表dbo.z_Resource
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="scormtype"></param>
        /// <param name="href"></param>
        /// <param name="fileUrl"></param>
        public void Add_Z_Resource(string identifier, string scormtype, string href, string fileUrl)
        {
            string commandName = "dbo.Pr_sco_z_Resource_insert";

            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@identifier",SqlDbType.NVarChar,255),
                new SqlParameter("@scormtype",SqlDbType.NVarChar,255),
                new SqlParameter("@href",SqlDbType.NVarChar,255),
                new SqlParameter("@fileUrl",SqlDbType.NVarChar,255)
            };
            param[0].Value = identifier;
            param[1].Value = scormtype;
            param[2].Value = href;
            param[3].Value = fileUrl;

            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, param);
        }


        /// <summary>
        /// 从临时表中导入到课件表中
        /// </summary>
        public void inputScorm()
        {
            string commandName = "dbo.Pr_sco_InputScorm";
            SqlHelper.ExecuteNonQuery(ConnectionString.ScormWrite, CommandType.StoredProcedure, commandName, null);
        }

    }

    public partial class ImportScormPackgeDataAccess
    {
        public int DeleteScormData(Guid courseWareID, SqlTransaction sqlTrans)
        {
            string commandName = "Pr_sco_e_All_Delete";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@CourseWareID",SqlDbType.UniqueIdentifier)
            };

            param[0].Value = courseWareID;

            return SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, commandName, param);
        }

        public int SaveOrganization(ScormOrganization entity, SqlTransaction sqlTrans)
        {
            string commandName = "Pr_sco_e_Organization_Insert";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@OrgID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@CourseWareID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@OrgTitle",SqlDbType.NVarChar),
                new SqlParameter("@StructureCode",SqlDbType.NVarChar),
                new SqlParameter("@Identifier",SqlDbType.NVarChar),
                new SqlParameter("@Creator",SqlDbType.NVarChar),
                new SqlParameter("@CreateTime",SqlDbType.DateTime)
            };

            param[0].Value = entity.OrgID;
            param[1].Value = entity.CourseWareID;
            param[2].Value = entity.OrgTitle;
            param[3].Value = entity.StructureCode;
            param[4].Value = entity.Identifier;
            param[5].Value = entity.Creator;
            param[6].Value = entity.CreateTime;

            return SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, commandName, param);
        }

        public int SaveItem(ScormItem entity, SqlTransaction sqlTrans)
        {
            string commandName = "Pr_sco_e_Item_Insert";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@ItemID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@CoursewareID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@OrgID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@ItemTitle",SqlDbType.NVarChar),
                new SqlParameter("@ParentItemID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@SequenceNo",SqlDbType.Int),
                new SqlParameter("@IsVisible",SqlDbType.Int),
                new SqlParameter("@ItemIdentifier",SqlDbType.NVarChar),
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@Creator",SqlDbType.NVarChar),
                new SqlParameter("@CreateTime",SqlDbType.DateTime),
                new SqlParameter("@Delete",SqlDbType.Int)
            };

            param[0].Value = entity.ItemID;
            param[1].Value = entity.CoursewareID;
            param[2].Value = entity.OrgID;
            param[3].Value = entity.ItemTitle;
            if (entity.ParentItemID == Guid.Empty)
                param[4].Value = DBNull.Value;
            else
                param[4].Value = entity.ParentItemID;
            param[5].Value = entity.SequenceNo;
            param[6].Value = entity.IsVisible;
            param[7].Value = entity.ItemIdentifier;
            if (entity.ResourceID == Guid.Empty)
                param[8].Value = DBNull.Value;
            else
                param[8].Value = entity.ResourceID;
            param[9].Value = entity.Creator;
            param[10].Value = entity.CreateTime;
            param[11].Value = entity.Delete;

            return SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, commandName, param);
        }

        public int SaveResource(ScormResource entity, SqlTransaction sqlTrans)
        {
            string commandName = "Pr_sco_e_Resource_Insert";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@ScormTypeCode",SqlDbType.NVarChar),
                new SqlParameter("@ResourceName",SqlDbType.NVarChar),
                new SqlParameter("@ResourceHref",SqlDbType.NVarChar),
                new SqlParameter("@Type",SqlDbType.NVarChar),
                new SqlParameter("@Resourceidentifier",SqlDbType.NVarChar),
                new SqlParameter("@CoursewareID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@Creator",SqlDbType.NVarChar),
                new SqlParameter("@CreateTime",SqlDbType.DateTime)
            };

            param[0].Value = entity.ResourceID;
            param[1].Value = entity.ScormTypeCode;
            param[2].Value = entity.ResourceName;
            param[3].Value = entity.ResourceHref;
            param[4].Value = entity.Type;
            param[5].Value = entity.Resourceidentifier;
            param[6].Value = entity.CoursewareID;
            param[7].Value = entity.Creator;
            param[8].Value = entity.CreateTime;

            return SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, commandName, param);
        }

        public int SaveResourceFile(ScormFile entity, SqlTransaction sqlTrans)
        {
            string commandName = "Pr_sco_e_SourceFile_Insert";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@FileHref",SqlDbType.NVarChar)
            };
            param[0].Value = entity.ResourceID;
            param[1].Value = entity.FileHref;

            return SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, commandName, param);
        }
    }
}
