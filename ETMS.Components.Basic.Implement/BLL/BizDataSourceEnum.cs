namespace ETMS.Components.Basic.Implement.BLL
{
    public enum BizDataSourceEnum
    {
        #region  系统管理

        //组织机构管理
        tb_Organization,

        //职级管理	
        tb_Profession,

        //部门管理	
        tb_Department,

        //岗位管理	
        tb_Position,

        //用户管理
        tb_User,

        //用户角色管理	
        tb_Role,

        //学员管理	
        tb_Student,

        #endregion

        #region 资源管理

        //课程        
        tb_Course,

        //课程查询	
        tb_CourseSearch,

        //课件
        tb_Courseware,

        //学习地图类型
        tb_ElearningMapType,
        
        //学习地图
        tb_ElearningMap,

        //教室
        tb_ClassRoom,

        //讲师管理
        tb_Teacher,
        ProfessorListInner,
        ProfessorListOutside,

        //新增内部讲师
        tb_NewInsideTeacher,

        #endregion

        #region  题库
        //课程列表
        qu_SubjectList,
        //分类列表
        qu_Catalog,
        //单选
        qu_SingleSelection,
        qu_SingleSelectionView,
        //多选
        qu_MultipleChoice,
        qu_MultipleChoiceView,
        //判断
        qu_Judge,
        qu_JudgeView,
        //填空
        qu_FillBlanks,
        qu_FillBlanksView,
        //简答
        qu_QuestionAndAnswer,
        qu_QuestionAndAnswerView,

        //题目列表
        qu_QuestionList,

        //闯关竞赛
        qu_ExContestList,
        //离线作业
        qu_ExOfflineHomeworkList,
        //在线作业
        qu_ExOnlineHomeworkList,
        //在线练习
        qu_ExOnlinePracticeList,
        //在线测试
        qu_ExOnlineTestList,
        //抽题练习
        qu_ExRandomPracticeList,

        //试卷
        qu_Testpaper,

        qu_ChooseQuestion,

        GradeEntry,
        GradeList,
        GradeView,
        GradePublish,
        //试卷评阅
        MarkingList,
        #endregion



        //课程资源信息	
        tb_CourseResource,
        //查看讲师	
        tb_SearchTeacher,
        //讲授课程	
        tb_TeachCourse,
        //新增授课	
        tb_NewTeach,
        //查看资源页面（在线课件）	
        tb_SearchResource,
        //公告管理	
        tb_Affiche,

        //发布对象（按个人）	
        tb_AffichePerson,
        //发布对象（按群组）	
        tb_AfficheGroup,
        ////学习地图类型	
        //tb_ElearningMapType,
        //学习地图编辑	
        tb_ElearningMapEdit,
        //学习地图课程选择	
        tb_ElearningMapCourseChoose,

        //培训需求管理	
        tb_TrainingRequire,
        //培训需求结果分析	
        tb_TrainingRequireanAnalysis,
        //培训计划	
        tb_TrainingPlan,
        //培训计划-设置课程	
        tb_PlanSetCourse,
        //培训计划-设置课程-新增课程	
        tb_TrainingPlanCourseAdd,
        //培训计划详情/审核培训计划	
        tb_TrainingPlanDetail,
        //培训计划审核	
        tb_TrainingPlanAudit,
        //培训计划实施结果管理	
        tb_TrainingPlanResultManage,
        //培训项目管理	
        tb_TrainingProject,

        //培训项目-设置课程/项目详细信息	
        tb_ProjectSetCourse,
        //培训项目-设置课程-新增课程	
        tb_ProjectCourseAdd,
        //培训项目-设置课程资源	
        tb_ProjectSetCourseResource,
        //培训项目-课时安排/查看课时安排/培训项目审核-课时安排	
        tb_ProjectCourseTimes,
        //培训项目-课程授课安排	
        tb_ProjectTeachArrange,
        //培训项目-课程学习资源/培训项目审核-课程资源	
        tb_ProjectCourseResource,
        //上级培训项目对象设置	
        tb_SuperiorTrainingProject,
        //培训项目审核	
        tb_TrainingProjectAudit,
        //审核培训项目/查看项目详细信息	
        tb_TrainingProjectAuditDetail,
        //项目审核-课程资源	
        tb_ProjectAuditCourseResource,
        //培训项目发布/查询下级培训学员	
        tb_TrainingProjectRelease,
        //培训项目实施结果管理	
        tb_TrainingProjectResultManage,
        //管理学员报名	
        tb_EnterStudentManage,
        //管理学员报名-学员/新增学员	
        tb_EnterStudent,
        //管理学员报名-学员详细信息	
        tb_StudentDetail,
        //管理学员报名-复制学员	
        tb_CopyStudent,
        //管理平台－培训管理－学习管理－课程申请审批/课程申请审批信息查询	
        tb_CourseApplyAudit,
        //课酬标准管理	
        tb_CourseFeeManage,
        //课酬标准管理-设置讲师（内部讲师）	
        tb_CourseFeeSetInsideTeacher,
        //课酬标准管理-设置讲师（外部讲师）	
        tb_CourseFeeSetOutsideTeacher,
        //课酬管理－课时费用确认单	
        tb_CourseFeeConfirm,
        //课酬管理－新增课时费用确认单	
        tb_CourseFeeConfirmAdd,
        //课酬管理－课时费用明细
        tb_CourseHoursDetails,
        //课酬管理－课时费用明细查看
        tb_CourseHoursDetails2,
        tb_CourseFeeConfirmView,
        //课酬管理-编辑课时费用确认单	
        tb_CourseFeeConfirmEdit,
        //课酬管理－课时费用确认单审核	
        tb_CourseFeeConfirmAudit,
        //课酬管理-审核\取消审核	
        tb_CourseFeeConfirmAudit11,
        //课酬管理－课时费用查询统计	
        tb_CourseFeeSearch,
        //费用管理－费用流水管理	
        tb_FeeFlow,
        //培训管理－管理签到	
        tb_ManageRegistration,
        //培训管理－管理签到01	
        tb_ManageRegistration01,
        //培训管理－请假申请审批	
        tb_AskApplyAudit,

        //导师管理	
        tb_MentorManage,
        //导师管理	
        tb_MentorManage11,
        //导学资料管理	
        tb_GuidanceResource,
        //导师辅导计划	
        tb_MentorGuidancePlan,
        //导师辅导计划	
        tb_MentorGuidancePlan11,

        //班级管理	
        tb_ClassesManage,
        //班级管理01	
        tb_ClassesManage01,
        //班级管理-设置学员	
        tb_ClassesSetStudent,
        //班级管理-设置学员01	
        tb_ClassesSetStudent01,
        //学习过程监控	
        tb_EearnProcessControl,
        //外部培训机构管理	
        tb_TraningOrgManager,
        //设置培训学员	
        tb_TrainingStudent,
        //查看培训项目-课程信息
        tb_TrainingProjectCourse,
        //查看培训项目-课程资源信息	
        tb_TrainingProjectResource,
        //查看培训项目-查看课时安排	
        tb_TrainingProjectPeriod,
        //培训项目>>课程辅助信息管理
        tb_ProjectCourseInfo,
        //培训项目>>课程辅助信息管理	
        tb_ProjectCourseAuxiliary,
        //培训项目>>课程辅助信息管理>>已选择在线课件	
        tb_ProjectCourseWare,
        //培训项目>>课程辅助信息管理>>已选择在线课件>>选择	
        tb_ProjectCourseWareAdd,
        //培训管理>>培训实施>>项目课时安排管理	
        tb_ProjectCoursePeriod,
        //培训管理>>培训实施>>项目课时安排管理>>课时安排	
        tb_CoursePeriodArrangement,
        //培训管理>>培训实施>>培训项目实施结果管理>>课时列表	
        tb_CoursePeriodEditState,
        //培训计划>>计划课时安排管理	
        tb_PlanCoursePeriod,
        //培训计划>>计划课时安排管理>>课时管理	
        tb_PlanCoursePeriodArrangement,
        //导师管理>>查看	
        tb_MentorManageView,
        //培训计划>>培训计划管理>>详细信息>>课时安排信息	
        TraningCoursePeriodList,
        //培训管理 >> 培训实施 >> 培训项目审核 >> 培训包含课程信息	
        tb_TrainingProjectCourseView,
        //培训管理>>培训实施>>培训项目>>已选择在线作业	
        tb_OnLineHomeWorkList,
        //培训管理>>培训实施>>培训项目>>已选择离线作业	
        tb_OffLineHomeWorkList,
        //培训管理>>培训实施>>培训项目>>已选择闯关竞赛	
        tb_CompetitionList,
        //培训管理>>培训实施>>培训项目>>已选择在线测试	
        tb_OnLineTestList,
        //查询与统计>>学员培训明细表 	
        tb_QueryStudentParticulars,
        //查询与统计>>学员培训汇总表
        tb_QueryStudentTotal,
        //查询与统计>>机构培训汇总 	
        tb_QueryOrgTraningTotal,
        //学习管理>>学习群组管理>>项目
        tb_LearningGroupProjectList,
        //学习管理>>学习群组管理>>群组	
        tb_LearningGroupList,
        //学习管理>>群组管理>>设置学员列表	
        tb_LearningGroupSetStudentList,
        //学习管理>>群组管理>>添加学员列表	
        tb_LearningGroupAddStudentList,
        //资源管理系统>>学习资源管理>>导学资料管理	
        tb_GuidanceManagerList,
        //教学管理>>离线作业批改	
        tb_TeachingManagerList,
        //教学管理>>离线作业批改>>批改	
        tb_TeachingManager,
        //教学管理>>离线作业批改>>离线作业批阅及查看	
        tb_TeachingViewList







    }
}
