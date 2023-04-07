using System;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using ETMS.Utility.BizCache;

namespace ETMS.Components.Basic.Implement.BLL
{/// <summary>
 /// 表名映射关系
 /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class TableNameMap : System.Attribute
    {
        public string ChinaName { get; set; }
        public string EnglishName { get; set; }
        public TableNameMap(string chinaName, string englishName)
        {
            this.EnglishName = englishName;
            this.ChinaName = chinaName;
        }
        public TableNameMap()
            : this(string.Empty, string.Empty)
        {
        }
        public TableNameMap(string chinaName)
            : this(chinaName, string.Empty)
        {
        }
    }
    /// <summary>
    /// 枚举值单项描述，主要用于字典信息==>数据表
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumItemDescription : System.Attribute
    {
        public string Description { get; set; }
        public EnumItemDescription(string description)
        {
            this.Description = description;
        }
    }

    /// <summary>
    /// 试卷类型
    /// </summary>
    public enum BizExerciseType
    {
        ExContest = 1,          //闯关竞赛
        ExOfflineHomework = 2,  //离线作业
        ExOnlineHomework = 3,   //在线作业
        ExOnlinePractice = 4,   //在线练习
        ExOnlineTest = 5,       //在线测试
        ExRandomPractice = 6    //抽题练习
    }

    public enum ObjectCourseRelation
    {
        StudyMapReferCourse = 1,    //学习地图与课程关联
        TeacherCourse = 2,         //讲师与课程关联
        PlanCourse = 3,              //培训计划与课程关联
        ItemCourse = 4,             //培训项目与课程关联
        RecommendCourse =5          //推荐课程与课程关联
    }

    /// <summary>
    /// 培训类型
    /// </summary>
    public enum BizQuestionnaireType
    {
        Questionnaire = 1,          //问卷调查
        Satisfaction = 2,  //满意度调查 
        Demand = 3,   //培训需求调查
    }

    /// <summary>
    /// 评价对象类型
    /// </summary>
    public enum BizEvaluationObjectType
    {
        ItemCourse = 1, //培训项目课程
        Teacher = 2     //讲师 
    }

    /// <summary>
    /// 字典类型定义
    /// </summary>
    public enum BizDicionaryType
    {
        #region 外部机构

        /// <summary>
        /// 外部机构
        /// </summary>
        Tr_OuterOrg,

        #endregion

        #region 组织机构、部门、岗位、职级
        /// <summary>
        ///  所有组织结构
        /// </summary>
        Dic_AllOrganizations,
        /// <summary>
        /// 当前登录者机构及下级机构
        /// </summary>
        Dic_CurrentAndSubOrganization,

        /// <summary>
        /// 当前登录者机构(不带路径,鼠标放上去显示带路径)
        /// </summary>
        Dic_CurrentOrganization,

        /// <summary>
        /// 部门：当前机构下所属部门（树型）
        /// </summary>       
        Site_DepartmentByOrgID,

        /// <summary>
        /// 岗位：当前机构下所属岗位（树型）
        /// </summary>        
        Dic_PostByOrgID,

        #endregion

        #region 评比活动
        /// <summary>
        /// 组别
        /// </summary>
        Activity_Dic_Group,
        /// <summary>
        /// 奖别
        /// </summary>
        Activity_Dic_Prize,
        /// <summary>
        /// 作品类别
        /// </summary>
        Activity_Dic_ProductType,
        /// <summary>
        /// 区域
        /// </summary>
        Activity_Dic_Region,
        /// <summary>
        /// 活动形式
        /// </summary>
        Activity_Dic_Shape,
        /// <summary>
        /// 活动类型
        /// </summary>
        Activity_Dic_Type,
        #endregion

        #region 业务字典 培训计划

        //培训计划管理 计划状态
        [TableNameMap("培训计划管理 计划状态")]
        [EnumItemDescription("10:待审核,20:审核通过,40:审核不通过,90:已归档")]
        Dic_TraningPlanState,

        //培训计划审核 计划审核状态
        [TableNameMap("培训计划审核 计划审核状态")]
        [EnumItemDescription("10:待审核,20:审核通过,40:审核不通过")]
        Dic_TraningPlanAuditState,

        //培训计划归档管理 计划归档状态
        [TableNameMap("培训计划归档管理 计划归档状态")]
        [EnumItemDescription("20:审核通过,90:已归档")]
        Dic_TraningPlanResultState,

        //培训计划归档管理 归档状态
        [TableNameMap("培训计划归档 计划归档方式")]
        [EnumItemDescription("1:正常结束,2:异常结束,3:审核通过结束,4:审核不通过结束")]
        Dic_TraningPlanResultEndMode,

        #endregion

        #region 业务字典 培训项目

        //培训项目管理>设置课程>新增课程
        [TableNameMap("课程范围-培训计划内，培训计划外")]
        [EnumItemDescription("1:培训计划内,0:培训计划外")]
        Dic_CourseRangeType,

        //培训项目管理 项目状态
        [TableNameMap("培训项目管理 项目状态")]
        [EnumItemDescription("10:待审核,20:审核通过,40:审核不通过,90:已归档")]
        Dic_TraningProjectType,


        //培训项目导师管理 项目状态
        [TableNameMap("培训项目导师管理 项目状态")]
        [EnumItemDescription("10:待审核,20:审核通过,40:审核不通过")]
        Dic_TeacherTraningProjectType,

        //培训项目管理 项目发布状态
        [TableNameMap("培训项目管理 项目发布状态")]
        [EnumItemDescription("0:未发布,1:已发布")]
        Dic_TraningProjectReleaseState,

        //培训项目管理 项目发布状态
        [TableNameMap("培训项目管理 项目发布状态")]
        [EnumItemDescription("false:未发布,true:已发布")]
        Dic_TraningProjectReleaseStateBool,

        //培训项目归档管理 归档状态
        [TableNameMap("培训项目归档管理 项目状态")]
        [EnumItemDescription("20:审核通过,40:审核不通过,90:已归档")]
        Dic_TraningProjectFileType,

        #endregion

        #region 业务字典 学习管理

        //课程申请状态
        [TableNameMap("课程申请状态")]
        [EnumItemDescription("待审核,审核通过,审核不通过")]
        Dic_CourseApplyStatus,

        #endregion

        #region 离线作业提交批阅情况

        [TableNameMap("提交批阅情况")]
        [EnumItemDescription("未提交人数,已批阅人数,待批阅人数")]
        Dic_TeachingManagerState,

        #endregion


        #region 题库

        /// <summary>
        /// 题目难易度:1易 2中 3难
        /// </summary>
        [EnumItemDescription("1:易,2:中,3:难")]
        Dic_DegreeDifficulty,

        /// <summary>
        /// 题型
        /// </summary>
        //[EnumItemDescription("1:单选题,2:多选题,3:填空,4:判断,5:问答,6:匹配,7:归类")]
        [EnumItemDescription("1:单选题,2:多选题,4:判断题,5:简答题")]
        Dic_QuestionType,

        #endregion

        #region IDP
        /// <summary>
        /// 学习内容完成情况
        /// </summary>
        [EnumItemDescription("0:未开始,1:进行中,2:已完成")]
        Dic_FinishedState,
        #endregion

        #region 公用的枚举：是/否, 启用/停用, 已发布/未发布, 男/女
        /// <summary>
        /// 是与否
        /// </summary>
        [TableNameMap("公用-是否")]
        [EnumItemDescription("1:是,0:否")]
        Dic_TrueOrFalse,

        /// <summary>
        /// 是与否
        /// </summary>
        [TableNameMap("公用-是否")]
        [EnumItemDescription("true:是,false:否")]
        Dic_TrueOrFalseBool,

        /// <summary>
        /// 状态
        /// </summary>
        [TableNameMap("公用-状态")]
        [EnumItemDescription("1:启用,0:停用")]
        Dic_Status,

        /// <summary>
        /// 媒体类型
        /// </summary>
        [TableNameMap("公用-类型")]
        [EnumItemDescription("1:视频,2:音频")]
        Dic_MediaType,

        /// <summary>
        /// 发布状态
        /// </summary>
        [TableNameMap("公用-发布状态")]
        [EnumItemDescription("1:已发布,0:未发布")]
        Dic_PublishStatus,

        /// <summary>
        /// 性别
        /// </summary>
        [TableNameMap("公用-性别")]
        [EnumItemDescription("1:男,2:女")]
        Dic_Sex,

        /// <summary>
        /// 非Scorm课件类型
        /// </summary>
        [TableNameMap("公用-非Scorm课件类型")]
        [EnumItemDescription("0:非地址,1:地址")]
        Dic_IsUrl,

        [TableNameMap("公用-讲师")]
        [EnumItemDescription("0:未合作,1:已合作")]
        Dic_IsCorpation,

        [TableNameMap("公用-讲师来源")]
        [EnumItemDescription("2:讲师,3:导师,4:讲师/导师")]
        Dic_Sys_TeacherSource,

        [TableNameMap("公用-是否限制")]
        [EnumItemDescription("0:不限制,1:限制")]
        Dic_Sys_Limited,
        #endregion

        /// <summary>
        /// 报名方式
        /// </summary>
        [TableNameMap("公用-报名方式")]
        [EnumItemDescription("3:项目集中填报,4:课程集中填报,1:项目自主报名,2:课程自主报名")]
        Dic_SignupMode,

        [TableNameMap("订单状态")]
        [EnumItemDescription("0:待支付,1:支付完成,2:支付失败")]
        Pay_PaymentStatus,

        [TableNameMap("订单状态")]
        [EnumItemDescription("0:订单无效,1:订单有效")]
        Order_OrderStatus,

        [TableNameMap("课程点评状态")]
        [EnumItemDescription("0:待审批,1:审批通过,2:审批不通过")]
        ApproveStatus,

        [TableNameMap("直播类型")]
        [EnumItemDescription("0:普通直播,1:精品直播")]
        LivingType
    }

    public class Dictionarys
    {
        /// <summary>
        /// 获取业务字典数据
        /// </summary>
        /// <param name="dicType">业务字典项</param>
        /// <returns></returns>
        public static string GetDictionaryItemInfoByID(BizDicionaryType dicType, string dicIDValue)
        {
            if (string.IsNullOrEmpty(dicIDValue))
            {
                return "";
            }
            //config/BizCache.config中定义缓存过期策略
            string key = "SysDic";
            string cacheKey = string.Format("{0}/item/{1}", dicType, dicIDValue);
            return BizCacheHelper.GetOrInsertItem<string>(key, cacheKey, () =>
            {
                DataTable dt = GetDictionaryDataTable(dicType);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return "";
                }

                System.Data.DataRow[] DicRow = dt.Select(string.Format(" ColumnCodeValue='{0}'", dicIDValue));
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




        /// <summary>
        /// 获取业务字典数据ToolTip
        /// </summary>
        /// <param name="dicType">业务字典项</param>
        /// <returns></returns>
        public static string GetDictionaryItemToolTipInfoByID(BizDicionaryType dicType, string dicIDValue)
        {
            if (string.IsNullOrEmpty(dicIDValue))
            {
                return "";
            }
            //config/BizCache.config中定义缓存过期策略
            string key = "SysDicToolTip";
            string cacheKey = string.Format("{0}/item/{1}", dicType, dicIDValue);
            return BizCacheHelper.GetOrInsertItem<string>(key, cacheKey, () =>
            {
                DataTable dt = GetDictionaryDataTable(dicType);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return "";
                }

                System.Data.DataRow[] DicRow = dt.Select(string.Format(" ColumnCodeValue='{0}'", dicIDValue));
                if (DicRow.Length > 0)
                {
                    if (DicRow[0]["ColumnToolTipValue"] == null)
                        return "";
                    else
                        return DicRow[0]["ColumnToolTipValue"].ToString();
                }
                else
                {
                    return "";
                }



            });
        }


        /// <summary>
        /// 获取业务字典数据
        /// </summary>
        /// <param name="dicType">业务字典项</param>
        /// <returns></returns>
        public static DataTable GetDictionaryDataTable(BizDicionaryType dicType)
        {
            EnumItemDescription[] descritions = (EnumItemDescription[])dicType.GetType().GetField(dicType.ToString()).GetCustomAttributes(typeof(EnumItemDescription), false);
            if (descritions.Length > 0)
            {
                //config/BizCache.config中定义缓存过期策略
                string key = "SysDic";
                return BizCacheHelper.GetOrInsertItem<DataTable>(key, dicType.ToString(), () =>
                {
                    return CreateDictionaryDataTable(descritions[0].Description);
                });
            }
            else
            {
                DataTable dt = null;
                //课程类型
                switch (dicType)
                {
                    case BizDicionaryType.Dic_AllOrganizations:
                        {
                            //config/BizCache.config中定义缓存过期策略
                            string key = "SysDic";
                            dt = BizCacheHelper.GetOrInsertItem<DataTable>(key, dicType.ToString(), () =>
                            {
                                OrganizationLogic organizationLogic = new OrganizationLogic();
                                return organizationLogic.GetAllEnableOrganizationByID();  // 所有组织机构   
                            });
                            break;
                        }
                    case BizDicionaryType.Dic_CurrentAndSubOrganization:
                        {
                            //config/BizCache.config中定义缓存过期策略
                            string key = "SysDic";
                            string cacheItemKey = string.Format("{0}/{1}", dicType, ETMS.AppContext.UserContext.Current.OrganizationID);
                            dt = BizCacheHelper.GetOrInsertItem<DataTable>(key, cacheItemKey, () =>
                            {
                                OrganizationLogic organizationLogic = new OrganizationLogic();
                                return organizationLogic.GetAllEnableOrganizationByID(ETMS.AppContext.UserContext.Current.OrganizationID);//当前用户所处机构 
                            });
                            break;
                        }
                    case BizDicionaryType.Dic_CurrentOrganization://当前机构,黄中福加
                        {
                            //config/BizCache.config中定义缓存过期策略
                            string key = "SysDic";
                            string cacheItemKey = string.Format("{0}/{1}", dicType, ETMS.AppContext.UserContext.Current.OrganizationID);
                            dt = BizCacheHelper.GetOrInsertItem<DataTable>(key, cacheItemKey, () =>
                            {
                                OrganizationLogic organizationLogic = new OrganizationLogic();
                                return organizationLogic.GetAllEnableOrganizationByID(ETMS.AppContext.UserContext.Current.OrganizationID);//当前用户所处机构 
                            });
                            break;
                        }
                    case BizDicionaryType.Site_DepartmentByOrgID:
                        {
                            string key = "SysDic";
                            string cacheItemKey = string.Format("{0}/{1}", dicType, ETMS.AppContext.UserContext.Current.OrganizationID);
                            dt = BizCacheHelper.GetOrInsertItem<DataTable>(key, cacheItemKey, () =>
                            {
                                DepartmentLogic departmentLogic = new DepartmentLogic();
                                return departmentLogic.GetAllEnableDepartmentsByOrganizationID(ETMS.AppContext.UserContext.Current.OrganizationID);//当前用户所处机构  
                            });
                            break;
                        }
                    case BizDicionaryType.Dic_PostByOrgID:
                        {
                            string key = "SysDic";
                            string cacheItemKey = string.Format("{0}/{1}", dicType, ETMS.AppContext.UserContext.Current.OrganizationID);
                            dt = BizCacheHelper.GetOrInsertItem<DataTable>(key, cacheItemKey, () =>
                            {
                                Dic_PostLogic postLogic = new Dic_PostLogic();
                                return postLogic.GetAllEnablePostByOrgID(ETMS.AppContext.UserContext.Current.OrganizationID);
                            });
                            break;
                        }
                    case BizDicionaryType.Dic_TraningProjectType:
                        {
                            //dt = new ETMS.Components.Basic.Implement.BLL.TraningImplement.Tr_ItemStatusLogic().GetStateList();
                            break;
                        }
                    case BizDicionaryType.Tr_OuterOrg:
                        {
                            string key = "SysDic";
                            string cacheItemKey = string.Format("{0}/{1}", dicType, ETMS.AppContext.UserContext.Current.OrganizationID);
                            dt = BizCacheHelper.GetOrInsertItem<DataTable>(key, cacheItemKey, () =>
                            {
                                TraningOrgnization.Tr_OuterOrgLogic tr_OuterOrgLogic = new TraningOrgnization.Tr_OuterOrgLogic();
                                return tr_OuterOrgLogic.GetOutOrgList(ETMS.AppContext.UserContext.Current.OrganizationID);
                            });
                            break;
                        }
                    case BizDicionaryType.Activity_Dic_Group:
                        {
                            string key = "SysDic";
                            string cacheItemKey = string.Format("{0}/{1}", dicType, ETMS.AppContext.UserContext.Current.OrganizationID);
                            dt = BizCacheHelper.GetOrInsertItem<DataTable>(key, cacheItemKey, () =>
                            {
                                return new ActivityDirectoryLogic().GetGroupList();
                            });
                            break;
                        }
                    case BizDicionaryType.Activity_Dic_Prize:
                        {
                            string key = "SysDic";
                            string cacheItemKey = string.Format("{0}/{1}", dicType, ETMS.AppContext.UserContext.Current.OrganizationID);
                            dt = BizCacheHelper.GetOrInsertItem<DataTable>(key, cacheItemKey, () =>
                            {
                                return new ActivityDirectoryLogic().GetPrizeList();
                            });
                            break;
                        }
                    case BizDicionaryType.Activity_Dic_ProductType:
                        {
                            string key = "SysDic";
                            string cacheItemKey = string.Format("{0}/{1}", dicType, ETMS.AppContext.UserContext.Current.OrganizationID);
                            dt = BizCacheHelper.GetOrInsertItem<DataTable>(key, cacheItemKey, () =>
                            {
                                return new ActivityDirectoryLogic().GetProductTypeList();
                            });
                            break;
                        }
                    case BizDicionaryType.Activity_Dic_Region:
                        {
                            string key = "SysDic";
                            string cacheItemKey = string.Format("{0}/{1}", dicType, ETMS.AppContext.UserContext.Current.OrganizationID);
                            dt = BizCacheHelper.GetOrInsertItem<DataTable>(key, cacheItemKey, () =>
                            {
                                return new ActivityDirectoryLogic().GetRegionList();
                            });
                            break;
                        }
                    case BizDicionaryType.Activity_Dic_Shape:
                        {
                            string key = "SysDic";
                            string cacheItemKey = string.Format("{0}/{1}", dicType, ETMS.AppContext.UserContext.Current.OrganizationID);
                            dt = BizCacheHelper.GetOrInsertItem<DataTable>(key, cacheItemKey, () =>
                            {
                                return new ActivityDirectoryLogic().GetShapeList();
                            });
                            break;
                        }
                    case BizDicionaryType.Activity_Dic_Type:
                        {
                            string key = "SysDic";
                            string cacheItemKey = string.Format("{0}/{1}", dicType, ETMS.AppContext.UserContext.Current.OrganizationID);
                            dt = BizCacheHelper.GetOrInsertItem<DataTable>(key, cacheItemKey, () =>
                            {
                                return new ActivityDirectoryLogic().GetActivityTypeList();
                            });
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                return dt;
            }
        }
        /// <summary>
        /// 字典数据创建过程
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static DataTable CreateDictionaryDataTable(string values)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ColumnCodeValue");
            dt.Columns.Add("ColumnNameValue");
            dt.Columns.Add("Remark");
            int i = 1;
            foreach (string item in values.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                //课程类型
                DataRow dr = dt.NewRow();

                string[] DicValue = item.Split(':');

                if (DicValue.Length > 1)
                {
                    dr["ColumnCodeValue"] = DicValue[0];
                    dr["ColumnNameValue"] = DicValue[1];
                    dr["Remark"] = "";
                }
                else
                {
                    dr["ColumnCodeValue"] = i;
                    dr["ColumnNameValue"] = item;
                    dr["Remark"] = "";
                }
                dt.Rows.Add(dr);
                i++;
            }

            return dt;
        }

        /// <summary>
        /// 读取业务枚举字典对应的表映射关系
        /// </summary>
        /// <param name="dicType"></param>
        /// <returns></returns>
        public static TableNameMap GetTablleNameMap(BizDicionaryType dicType)
        {
            TableNameMap[] descritions = (TableNameMap[])dicType.GetType().GetField(dicType.ToString()).GetCustomAttributes(typeof(TableNameMap), false);
            if (descritions.Length > 0)
            {
                return descritions[0];
            }
            return new TableNameMap(dicType.ToString(), dicType.ToString());
        }
    }
}
