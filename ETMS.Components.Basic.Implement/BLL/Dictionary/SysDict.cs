using System.Data;

using ETMS.Components.Basic.API.Entity.Dictionary;
using ETMS.Components.Basic.Implement.DAL.Dictionary;
using ETMS.Utility.BizCache;
namespace ETMS.Components.Basic.Implement.BLL.Dictionary
{
    public class SysDict
    {
        #region 缓存方式存放字典，字典获取规则：字典表名
        /// <summary>
        /// 查询一个表的两个字段的数据，主要是针对字典表的查询，特别是系统字典表
        /// </summary>
        /// <param name="tableName">字典表的名称</param> 
        /// <returns>table:ColumnCodeValue,ColumnCodeName</returns>
        public static DataTable GetCommonSysDictionary(string tableName)
        {
            //config/BizCache.config中定义缓存过期策略
            string key = "SysDic";
            return BizCacheHelper.GetOrInsertItem<DataTable>(key, tableName, () =>
              {
                  SysDictionaryDataAccess dal = new SysDictionaryDataAccess();
                  DataTable dt = dal.ExecuteSelectSQL(string.Format(" exec [Pr_Common_SYS_GetDictionaryList] '{0}'", tableName));
                  return dt;
              });
        }


        /// <summary>
        /// 获取业务字典数据
        /// </summary>
        /// <param name="tableName">字典表的名称</param> 
        /// <param name="dicIDValue">字典项值</param>
        /// <returns></returns>
        public static string GetDictionaryItemInfoByID(string tableName, string dicIDValue)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(dicIDValue))
            {
                return "";
            }
            //config/BizCache.config中定义缓存过期策略
            string key = "SysDic";
            string cacheItemKey = string.Format("{0}/{1}", tableName, dicIDValue);
            return BizCacheHelper.GetOrInsertItem<string>(key, cacheItemKey, () =>
            {
                DataTable dt = GetCommonSysDictionary(tableName);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return "";
                }

                System.Data.DataRow[] DicRow = dt.Select(string.Format(" ColumnCodeValue={0}", dicIDValue));
                if (DicRow.Length > 0)
                {
                    return DicRow[0]["ColumnNameValue"].ToString();
                }
                else
                {
                    return "";
                }
            });
        }
        #endregion


        /// <summary>
        /// 查询“课件类型Dic_Sys_CoursewareType”系统字典表的可用数据，返回：CoursewareTypeID,CoursewareTypeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_CoursewareType()
        {
            return Dic_Sys_CoursewareType(false);
        }
        /// <summary>
        /// 查询“课件类型Dic_Sys_CoursewareType”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_CoursewareType(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_CoursewareType", "CoursewareTypeID", "CoursewareTypeName", isRename);
        }
        /// <summary>
        /// 查询“课程类型Dic_Sys_CourseType”系统字典表的可用数据，返回：CourseTypeID,CourseTypeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_CourseType()
        {
            return Dic_Sys_CourseType(false);
        }
        /// <summary>
        /// 查询“课程类型Dic_Sys_CourseType”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_CourseType(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_CourseType", "CourseTypeID", "CourseTypeName", isRename);
        }
        /// <summary>
        /// 查询“课程等级Dic_Sys_CourseLevel”系统字典表的可用数据，返回：CourseLevelID,CourseLevelName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_CourseLevel()
        {
            return Dic_Sys_CourseLevel(false);
        }
        /// <summary>
        /// 查询“课程等级Dic_Sys_CourseLevel”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_CourseLevel(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_CourseLevel", "CourseLevelID", "CourseLevelName", isRename);
        }
        /// <summary>
        /// 查询“课程资源类型Dic_Sys_CourseResType”系统字典表的可用数据，返回：CourseResTypeID,CourseResTypeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_CourseResType()
        {
            return Dic_Sys_CourseResType(false);
        }
        /// <summary>
        /// 查询“课程资源类型Dic_Sys_CourseResType”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_CourseResType(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_CourseResType", "CourseResTypeID", "CourseResTypeName", isRename);
        }
        /// <summary>
        /// 查询“课程属性Dic_Sys_CourseAttr”系统字典表的可用数据，返回：CourseAttrID,CourseAttrName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_CourseAttr()
        {
            return Dic_Sys_CourseAttr(false);
        }
        /// <summary>
        /// 查询“课程属性Dic_Sys_CourseAttr”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_CourseAttr(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_CourseAttr", "CourseAttrID", "CourseAttrName", isRename);
        }
        /// <summary>
        /// 查询“培训级别Dic_Sys_TrainingLevel”系统字典表的可用数据，返回：TrainingLevelID,TrainingLevelName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_TrainingLevel()
        {
            return Dic_Sys_TrainingLevel(false);
        }
        /// <summary>
        /// 查询“培训级别Dic_Sys_TrainingLevel”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_TrainingLevel(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_TrainingLevel", "TrainingLevelID", "TrainingLevelName", isRename);
        }
        /// <summary>
        /// 查询“培训方式Dic_Sys_TrainingModel”系统字典表的可用数据，返回：TrainingModelID,TrainingModelName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_TrainingModel()
        {
            return Dic_Sys_TrainingModel(false);
        }
        /// <summary>
        /// 查询“培训方式Dic_Sys_TrainingModel”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_TrainingModel(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_TrainingModel", "TrainingModelID", "TrainingModelName", isRename);
        }
        /// <summary>
        /// 查询“授课方式Dic_Sys_TeachModel”系统字典表的可用数据，返回：TeachModelID,TeachModelName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_TeachModel()
        {
            return Dic_Sys_TeachModel(false);
        }
        /// <summary>
        /// 查询“授课方式Dic_Sys_TrainingModel”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_TeachModel(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_TeachModel", "TeachModelID", "TeachModelName", isRename);
        }
        /// <summary>
        /// 查询“项目结束方式Dic_Sys_ItemEndMode”系统字典表的可用数据，返回：ItemEndModeID,ItemEndModeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_ItemEndMode()
        {
            return Dic_Sys_ItemEndMode(false);
        }
        /// <summary>
        /// 查询“项目结束方式Dic_Sys_ItemEndMode”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_ItemEndMode(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_ItemEndMode", "ItemEndModeID", "ItemEndModeName", isRename);
        }
        /// <summary>
        /// 查询“计划类型Dic_Sys_PlanType”系统字典表的可用数据，返回：PlanTypeID,PlanTypeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_PlanType()
        {
            return Dic_Sys_PlanType(false);
        }
        /// <summary>
        /// 查询“计划类型Dic_Sys_ItemEndMode”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_PlanType(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_PlanType", "PlanTypeID", "PlanTypeName", isRename);
        }
        /// <summary>
        /// 查询“专业类别Dic_Sys_SpecialtyType”系统字典表的可用数据，返回：SpecialtyTypeID,SpecialtyTypeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_SpecialtyType()
        {
            return Dic_Sys_SpecialtyType(false);
        }
        /// <summary>
        /// 查询“专业类别Dic_Sys_ItemEndMode”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_SpecialtyType(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_SpecialtyType", "SpecialtyTypeID", "SpecialtyTypeName", isRename);
        }
        /// <summary>
        /// 查询“报名方式Dic_Sys_SignupMode”系统字典表的可用数据，返回：SignupModeID,SignupModeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_SignupMode()
        {
            return Dic_Sys_SignupMode(false);
        }
        /// <summary>
        /// 查询“报名方式Dic_Sys_SignupMode”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_SignupMode(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_SignupMode", "SignupModeID", "SignupModeName", isRename);
        }
        /// <summary>
        /// 查询“学员类型Dic_Sys_StudentType”系统字典表的可用数据，返回：StudentTypeID,StudentTypeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_StudentType()
        {
            return Dic_Sys_StudentType(false);
        }
        /// <summary>
        /// 查询“学员类型Dic_Sys_StudentType”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_StudentType(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_StudentType", "StudentTypeID", "StudentTypeName", isRename);
        }
        /// <summary>
        /// 查询“签到信息Dic_Sys_SigninType”系统字典表的可用数据，返回：SigninTypeID,SigninTypeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_SigninType()
        {
            return Dic_Sys_SigninType(false);
        }
        /// <summary>
        /// 查询“签到信息Dic_Sys_SigninType”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_SigninType(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_SigninType", "SigninTypeID", "SigninTypeName", isRename);
        }
        /// <summary>
        /// 查询“教室用途Dic_Sys_ClassRoomPurpose”系统字典表的可用数据，返回：ClassRoomPurposeID,ClassRoomPurposeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_ClassRoomPurpose()
        {
            return Dic_Sys_ClassRoomPurpose(false);
        }
        /// <summary>
        /// 查询“教室用途Dic_Sys_ClassRoomPurpose”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_ClassRoomPurpose(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_ClassRoomPurpose", "ClassRoomPurposeID", "ClassRoomPurposeName", isRename);
        }
        /// <summary>
        /// 查询“课时安排状态Dic_Sys_CourseHoursStatus”系统字典表的可用数据，返回：CourseHoursStatusID,CourseHoursStatusName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_CourseHoursStatus()
        {
            return Dic_Sys_CourseHoursStatus(false);
        }
        /// <summary>
        /// 查询“课时安排状态Dic_Sys_CourseHoursStatus”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_CourseHoursStatus(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_CourseHoursStatus", "CourseHoursStatusID", "CourseHoursStatusName", isRename);
        }
        /// <summary>
        /// 查询“培训时间说明Dic_Sys_TrainingTimeDesc”系统字典表的可用数据，返回：TrainingTimeDescID,TrainingTimeDescName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_TrainingTimeDesc()
        {
            return Dic_Sys_TrainingTimeDesc(false);
        }
        /// <summary>
        /// 查询“培训时间说明Dic_Sys_TrainingTimeDesc”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_TrainingTimeDesc(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_TrainingTimeDesc", "TrainingTimeDescID", "TrainingTimeDescName", isRename);
        }
        /// <summary>
        /// 查询“讲师分类Dic_Sys_TeacherType”系统字典表的可用数据，返回：TeacherTypeID,TeacherTypeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_TeacherType()
        {
            return Dic_Sys_TeacherType(false);
        }
        /// <summary>
        /// 查询“讲师分类Dic_Sys_TeacherType”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_TeacherType(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_TeacherType", "TeacherTypeID", "TeacherTypeName", isRename);
        }
        /// <summary>
        /// 查询“学习地图类型Dic_Sys_ELearningMapType”系统字典表的可用数据，返回：ELearningMapTypeID,ELearningMapTypeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_ELearningMapType()
        {
            return Dic_Sys_ELearningMapType(false);
        }
        /// <summary>
        /// 查询“学习地图类型Dic_Sys_ELearningMapType”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_ELearningMapType(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_ELearningMapType", "ELearningMapTypeID", "ELearningMapTypeName", isRename);
        }
        /// <summary>
        /// 查询“讲师来源Dic_Sys_TeacherSource”系统字典表的可用数据，返回：TeacherSourceID,TeacherSourceName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_TeacherSource()
        {
            return Dic_Sys_TeacherSource(false);
        }
        /// <summary>
        /// 查询“讲师来源Dic_Sys_TeacherSource”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_TeacherSource(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_TeacherSource", "TeacherSourceID", "TeacherSourceName", isRename);
        }
        /// <summary>
        /// 查询“讲师等级Dic_Sys_TeacherLevel”系统字典表的可用数据，返回：TeacherLevelID,TeacherLevelName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_TeacherLevel()
        {
            return Dic_Sys_TeacherLevel(false);
        }
        /// <summary>
        /// 查询“讲师等级Dic_Sys_TeacherLevel”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_TeacherLevel(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_TeacherLevel", "TeacherLevelID", "TeacherLevelName", isRename);
        }
        /// <summary>
        /// 查询“调查题型Dic_Sys_Poll_TitleType”系统字典表的可用数据，返回：TitleTypeID,TitleTypeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_Poll_TitleType()
        {
            return Dic_Sys_Poll_TitleType(false);
        }
        /// <summary>
        /// 查询“调查题型Dic_Sys_Poll_TitleType”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_Poll_TitleType(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_Poll_TitleType", "TitleTypeID", "TitleTypeName", isRename);
        }
        /// <summary>
        /// 查询“调查类型Dic_Sys_Poll_QueryType”系统字典表的可用数据，返回：QueryTypeID,QueryTypeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_Poll_QueryType()
        {
            return Dic_Sys_Poll_QueryType(false);
        }
        /// <summary>
        /// 查询“调查类型Dic_Sys_Poll_QueryType”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_Poll_QueryType(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_Poll_QueryType", "QueryTypeID", "QueryTypeName", isRename);
        }
        /// <summary>
        /// 查询“调查对象类型Dic_Sys_Poll_QueryObjectType”系统字典表的可用数据，返回：QueryObjectTypeID,QueryObjectTypeName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_Poll_QueryObjectType()
        {
            return Dic_Sys_Poll_QueryObjectType(false);
        }
        /// <summary>
        /// 查询“调查对象类型Dic_Sys_Poll_QueryObjectType”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_Poll_QueryObjectType(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_Poll_QueryObjectType", "QueryObjectTypeID", "QueryObjectTypeName", isRename);
        }
        /// <summary>
        /// 查询“公告级别Inf_dic_InfoLevel”系统字典表的可用数据，返回：InfoLevelID,InfoLevelName
        /// </summary>
        /// <returns></returns>
        public static DataTable Inf_dic_InfoLevel()
        {
            return Inf_dic_InfoLevel(false);
        }
        /// <summary>
        /// 查询“公告级别Inf_dic_InfoLevel”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Inf_dic_InfoLevel(bool isRename)
        {
            return GetSysDictionary("Inf_dic_InfoLevel", "InfoLevelID", "InfoLevelName", isRename);
        }




        /// <summary>
        /// 查询“公告类别Inf_dic_BulletinType”系统字典表的可用数据，返回：ArticleTypeID,ArticleTypeName
        /// Inf_dic_BulletinType表中ArticleTypeID值小于10的为公告类别
        /// </summary>
        /// <returns></returns>
        public static DataTable Inf_dic_BulletinType()
        {
            return Inf_dic_BulletinType(false);
        }
        /// <summary>
        /// 查询“公告类别Inf_dic_BulletinType”系统字典表的可用数据
        /// Inf_dic_BulletinType表中ArticleTypeID值小于10的为公告类别
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Inf_dic_BulletinType(bool isRename)
        {
            string conditionSQL = " AND (ArticleTypeID < 10)";
            return GetSysDictionary("Inf_dic_BulletinType", "ArticleTypeID", "ArticleTypeName", conditionSQL, isRename);
        }
        /// <summary>
        /// 查询“导学资料类型Inf_dic_BulletinType”系统字典表的可用数据，返回：ArticleTypeID,ArticleTypeName
        /// Inf_dic_BulletinType表中ArticleTypeID值大于10的为导学资料类型
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_MentorDataType()
        {
            return Dic_MentorDataType(false);
        }
        /// <summary>
        /// 查询“导学资料类型Inf_dic_BulletinType”系统字典表的可用数据
        /// Inf_dic_BulletinType表中ArticleTypeID值大于10的为导学资料类型
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_MentorDataType(bool isRename)
        {
            string conditionSQL = " AND (ArticleTypeID > 10)";
            return GetSysDictionary("Inf_dic_BulletinType", "ArticleTypeID", "ArticleTypeName", conditionSQL, isRename);
        }


        /// <summary>
        /// 查询“作业状态Dic_Sys_JobStatus”系统字典表的可用数据，返回：JobStatusID,JobStatusName
        /// </summary>
        /// <returns></returns>
        public static DataTable Dic_Sys_JobStatus()
        {
            return Dic_Sys_JobStatus(false);
        }
        /// <summary>
        /// 查询“作业状态Dic_Sys_JobStatus”系统字典表的可用数据
        /// </summary>
        /// <param name="isRename">是否重命名，如果为true，则将字典表的主键字段重新命名为ColumnCodeValue，字典表的数据项重新命名为ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable Dic_Sys_JobStatus(bool isRename)
        {
            return GetSysDictionary("Dic_Sys_JobStatus", "JobStatusID", "JobStatusName", isRename);
        }



        /// <summary>
        /// 根据系统字典表的英文名称取其内容
        /// 如果字典表没有在此写相应的方法，则返回null
        /// </summary>
        /// <param name="tableName">系统字典表的英文名称，如：Dic_Sys_TrainingLevel</param>
        /// <returns></returns>
        public static DataTable GetSysDictionaryByTableName(string tableName)
        {
            return GetSysDictionaryByTableName(tableName, false);
        }
        /// <summary>
        /// 根据系统字典表的英文名称取其内容
        /// 如果字典表没有在此写相应的方法，则返回null
        /// </summary>
        /// <param name="tableName">系统字典表的英文名称，如：Dic_Sys_TrainingLevel</param>
        /// <returns></returns>
        public static DataTable GetSysDictionaryByTableName(string tableName, bool isRename)
        {
            DataTable dt = new DataTable();
            switch (tableName.ToLower())
            {
                case "dic_sys_ranktype"://职级类型
                    dt = GetSysDictionary("Dic_Sys_RankType", "RankTypeID", "RankTypeName", isRename);
                    ;
                    break;
                case "dic_sys_coursewaretype"://课件类型
                    dt = Dic_Sys_CoursewareType(isRename);
                    break;
                case "dic_sys_coursetype"://课程类型
                    dt = Dic_Sys_CourseType(isRename);
                    break;
                case "dic_sys_courselevel"://课程等级
                    dt = Dic_Sys_CourseLevel(isRename);
                    break;
                case "dic_sys_courserestype"://课程资源类型
                    dt = Dic_Sys_CourseResType(isRename);
                    break;
                case "dic_sys_courseattr"://课程属性
                    dt = Dic_Sys_CourseAttr(isRename);
                    break;
                case "dic_sys_traininglevel"://培训级别
                    dt = Dic_Sys_TrainingLevel(isRename);
                    break;
                case "dic_sys_trainingmodel"://培训方式
                    dt = Dic_Sys_TrainingModel(isRename);
                    break;
                case "dic_sys_teachmodel"://授课方式
                    dt = Dic_Sys_TeachModel(isRename);
                    break;
                case "dic_sys_itemendmode"://项目结束方式
                    dt = Dic_Sys_ItemEndMode(isRename);
                    break;
                case "dic_sys_plantype"://计划类型
                    dt = Dic_Sys_PlanType(isRename);
                    break;
                case "dic_sys_specialtytype"://专业类别
                    dt = Dic_Sys_SpecialtyType(isRename);
                    break;
                case "dic_sys_signupmode"://报名方式
                    dt = Dic_Sys_SignupMode(isRename);
                    break;
                case "dic_sys_studenttype"://学员类型
                    dt = Dic_Sys_StudentType(isRename);
                    break;
                case "dic_sys_classroompurpose"://教室用途
                    dt = Dic_Sys_ClassRoomPurpose(isRename);
                    break;
                case "dic_sys_coursehoursstatus"://课时安排状态
                    dt = Dic_Sys_CourseHoursStatus(isRename);
                    break;
                case "dic_sys_trainingtimedesc"://培训时间说明
                    dt = Dic_Sys_TrainingTimeDesc(isRename);
                    break;
                case "dic_sys_teachertype"://讲师分类
                    dt = Dic_Sys_TeacherType(isRename);
                    break;
                case "dic_sys_elearningmaptype"://学习地图类型
                    dt = Dic_Sys_ELearningMapType(isRename);
                    break;
                case "dic_sys_teachersource"://讲师来源
                    dt = Dic_Sys_TeacherSource(isRename);
                    break;
                case "dic_sys_teacherlevel"://讲师等级
                    dt = Dic_Sys_TeacherLevel(isRename);
                    break;
                case "dic_sys_poll_titletype"://调查题型
                    dt = Dic_Sys_Poll_TitleType(isRename);
                    break;
                case "dic_sys_poll_querytype"://调查类型
                    dt = Dic_Sys_Poll_QueryType(isRename);
                    break;
                case "dic_sys_poll_queryobjecttype"://课程类型
                    dt = Dic_Sys_Poll_QueryObjectType(isRename);
                    break;
                case "inf_dic_infolevel"://公告级别
                    dt = Inf_dic_InfoLevel(isRename);
                    break;
                case "Inf_dic_BulletinType"://公告类别
                    dt = Inf_dic_BulletinType(isRename);
                    break;
                case "dic_mentordatatype"://导学资料类型
                    dt = Dic_MentorDataType(isRename);
                    break;
                case "dic_sys_jobstatus"://作业状态
                    dt = Dic_Sys_JobStatus(isRename);
                    break;
            }
            return dt;
        }


        /// <summary>
        /// 根据系统字典表的英文名称取其内容
        /// 如果字典表没有在此写相应的方法，则返回null
        /// </summary>
        /// <param name="sysDicionaryType">系统字典表的枚举类型SysDicionaryTypeEnum</param>
        /// <returns></returns>
        public static DataTable GetSysDictionaryBySysDicionaryEnum(SysDicionaryTypeEnum sysDicionaryType)
        {
            return GetSysDictionaryBySysDicionaryEnum(sysDicionaryType, false);
        }


        /// <summary>
        /// 根据系统字典表的英文名称取其内容
        /// 如果字典表没有在此写相应的方法，则返回null
        /// </summary>
        /// <param name="sysDicionaryType">系统字典表的枚举类型SysDicionaryTypeEnum</param>
        /// <param name="isRename">是否对字段重新命名，如果重新命名，则返回ColumnCodeValue,ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable GetSysDictionaryBySysDicionaryEnum(SysDicionaryTypeEnum sysDicionaryType, bool isRename)
        {
            string tableName = sysDicionaryType.ToString();
            return GetSysDictionaryByTableName(tableName, isRename);

        }

        /// <summary>
        /// 根据系统字典表的中文名称取其内容
        /// 如果字典表没有在此写相应的方法，则返回null
        /// </summary>
        /// <param name="tableDesc">系统字典表的中文名称，如：培训级别</param>
        /// <returns></returns>
        public static DataTable GetSysDictionaryByTableDesc(string tableDesc)
        {
            return GetSysDictionaryByTableDesc(tableDesc, false);
        }
        /// <summary>
        /// 根据系统字典表的中文名称取其内容
        /// 如果字典表没有在此写相应的方法，则返回null
        /// </summary>
        /// <param name="tableDesc">系统字典表的中文名称，如：培训级别</param>
        /// <param name="isRename">是否对字段重新命名，如果重新命名，则返回ColumnCodeValue,ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable GetSysDictionaryByTableDesc(string tableDesc, bool isRename)
        {
            string tableName = "";
            switch (tableDesc)
            {
                case "课件类型":
                    tableName = "Dic_Sys_CoursewareType";
                    break;
                case "课程类型":
                    tableName = "Dic_Sys_CourseType";
                    break;
                case "课程等级":
                    tableName = "Dic_Sys_CourseLevel";
                    break;
                case "课程资源类型":
                    tableName = "Dic_Sys_CourseResType";
                    break;
                case "课程属性":
                    tableName = "Dic_Sys_CourseAttr";
                    break;
                case "培训级别":
                    tableName = "Dic_Sys_TrainingLevel";
                    break;
                case "培训方式":
                    tableName = "Dic_Sys_TrainingModel";
                    break;
                case "授课方式":
                    tableName = "Dic_Sys_TeachModel";
                    break;
                case "项目结束方式":
                    tableName = "Dic_Sys_ItemEndMode";
                    break;
                case "计划类型":
                    tableName = "Dic_Sys_PlanType";
                    break;
                case "专业类别":
                    tableName = "Dic_Sys_SpecialtyType";
                    break;
                case "报名方式":
                    tableName = "Dic_Sys_SignupMode";
                    break;
                case "学员类型":
                    tableName = "Dic_Sys_StudentType";
                    break;
                case "教室用途":
                    tableName = "Dic_Sys_ClassRoomPurpose";
                    break;
                case "课时安排状态":
                    tableName = "Dic_Sys_CourseHoursStatus";
                    break;
                case "培训时间说明":
                    tableName = "Dic_Sys_TrainingTimeDesc";
                    break;
                case "讲师分类":
                    tableName = "Dic_Sys_TeacherType";
                    break;
                case "学习地图类型":
                    tableName = "Dic_Sys_ELearningMapType";
                    break;
                case "讲师来源":
                    tableName = "Dic_Sys_TeacherSource";
                    break;
                case "讲师等级":
                    tableName = "Dic_Sys_TeacherLevel";
                    break;
                case "调查题型":
                    tableName = "Dic_Sys_CourseType";
                    break;
                case "调查类型":
                    tableName = "Dic_Sys_Poll_QueryType";
                    break;
                case "调查对象类型":
                    tableName = "Dic_Sys_Poll_QueryObjectType";
                    break;
                case "公告级别":
                    tableName = "Inf_dic_InfoLevel";
                    break;
                case "公告类别":
                    tableName = "Inf_dic_BulletinType";
                    break;
                case "导学资料类型":
                    tableName = "Dic_MentorDataType";
                    break;
                case "作业状态":
                    tableName = "Dic_Sys_JobStatus";
                    break;
                default:
                    tableName = "";
                    break;
            }
            return GetSysDictionaryByTableName(tableName, isRename);
        }


        /// <summary>
        /// 查询一个表的两个字段的数据，主要是针对字典表的查询，特别是系统字典表
        /// </summary>
        /// <param name="tableName">字典表的名称</param>
        /// <param name="codeFieldName">字典表的主键字段名称</param>
        /// <param name="dataFieldName">字典表的数据项名称</param>
        /// <returns></returns>
        public static DataTable GetSysDictionary(string tableName, string codeFieldName, string dataFieldName)
        {
            return GetSysDictionary(tableName, codeFieldName, dataFieldName, "", false);
        }


        /// <summary>
        /// 查询一个表的两个字段的数据，主要是针对字典表的查询，特别是系统字典表
        /// 如果字典表没有在此写相应的方法，则返回null
        /// </summary>
        /// <param name="tableName">字典表的名称</param>
        /// <param name="codeFieldName">字典表的主键字段名称</param>
        /// <param name="dataFieldName">字典表的数据项名称</param>
        /// <param name="isRename">是否对字段重新命名，如果重新命名，则返回ColumnCodeValue,ColumnNameValue</param>
        /// <returns></returns>
        public static DataTable GetSysDictionary(string tableName, string codeFieldName, string dataFieldName, bool isRename)
        {
            return GetSysDictionary(tableName, codeFieldName, dataFieldName, "", isRename);
        }



        /// <summary>
        /// 查询一个表的两个字段的数据，主要是针对字典表的查询，特别是系统字典表
        /// </summary>
        /// <param name="tableName">字典表的名称</param>
        /// <param name="codeFieldName">字典表的主键字段名称</param>
        /// <param name="dataFieldName">字典表的数据项名称</param>
        /// <returns></returns>
        public static DataTable GetSysDictionary(string tableName, string codeFieldName, string dataFieldName, string conditionSQL)
        {
            return GetSysDictionary(tableName, codeFieldName, dataFieldName, conditionSQL, false);
        }


        /// <summary>
        /// 查询一个表的两个字段的数据，主要是针对字典表的查询，特别是系统字典表
        /// 如果字典表没有在此写相应的方法，则返回null
        /// </summary>
        /// <param name="tableName">字典表的名称</param>
        /// <param name="codeFieldName">字典表的主键字段名称</param>
        /// <param name="dataFieldName">字典表的数据项名称</param>
        /// <param name="isRename">是否对字段重新命名，如果重新命名，则返回ColumnCodeValue,ColumnNameValue</param>
        /// <param name="conditionSQL">以AND开头的条件表达式</param>
        /// <returns></returns>
        public static DataTable GetSysDictionary(string tableName, string codeFieldName, string dataFieldName, string conditionSQL, bool isRename)
        {
            if (tableName == "")
                return null;
            string sqlModal = @"SELECT {0} , {1} FROM {2} WHERE IsUse > 0 {3} ORDER BY OrderNum";
            if (isRename)
                sqlModal = @"SELECT {0} AS ColumnCodeValue, {1} AS ColumnNameValue FROM {2} WHERE IsUse > 0 {3} ORDER BY OrderNum";
            string sql = string.Format(sqlModal, codeFieldName, dataFieldName, tableName, conditionSQL);
            SysDictionaryDataAccess dal = new SysDictionaryDataAccess();
            DataTable dt = dal.ExecuteSelectSQL(sql);
            return dt;
        }




        /// <summary>
        /// 运行一个查询语句，返回DataTable
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns></returns>
        public static DataTable ExecuteSelectSQL(string sql)
        {
            SysDictionaryDataAccess dal = new SysDictionaryDataAccess();
            DataTable dt = dal.ExecuteSelectSQL(sql);
            return dt;
        }






    }
}
