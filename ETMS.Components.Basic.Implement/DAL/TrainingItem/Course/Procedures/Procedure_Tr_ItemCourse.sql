
------------------------------------------------------------------------------------------------------------------------
--Table Tr_ItemCourse
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourse_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourse_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourse_Insert]
	@TrainingItemCourseID UniqueIdentifier,
	@TrainingItemID UniqueIdentifier,
	@CourseID UniqueIdentifier,
	@TeachModelID Int,
	@CourseStatus Int,
	@CourseBeginTime DateTime,
	@CourseEndTime DateTime,
	@TrainingModelID Int,
	@OuterOrgID UniqueIdentifier,
	@OuterOrgDutyUser NVarChar(128),
	@OuterOrgEMAIL NVarChar(256),
	@CourseAttrID Int,
	@Score Int,
	@CourseHours Decimal(8,2),
	@BudgetFee Decimal(15,2),
	@Remark NVarChar(Max),
	@IsNeedApply Bit,
	@IsPlanCourse Bit,
	@IsLimit Bit,
	@MaxNum Int,
	@IsInputGrade Bit,
	@IsIssueGrade Bit,
	@GradeIssueTime DateTime,
	@GradeIssueUser NVarChar(128),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@DelFlag Bit,
	@PassLine Decimal(6,2),
	@TotalScore Decimal(6,2),
	@IsIssueScore Bit,
	@ScoreIssueUser NVarChar(128),
	@ScoreIssueTime DateTime,
	@IsComputeScore Bit,
	@ScoreComputeUser NVarChar(128),
	@ScoreComputeTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Tr_ItemCourse]
	(
		[TrainingItemCourseID],
		[TrainingItemID],
		[CourseID],
		[TeachModelID],
		[CourseStatus],
		[CourseBeginTime],
		[CourseEndTime],
		[TrainingModelID],
		[OuterOrgID],
		[OuterOrgDutyUser],
		[OuterOrgEMAIL],
		[CourseAttrID],
		[Score],
		[CourseHours],
		[BudgetFee],
		[Remark],
		[IsNeedApply],
		[IsPlanCourse],
		[IsLimit],
		[MaxNum],
		[IsInputGrade],
		[IsIssueGrade],
		[GradeIssueTime],
		[GradeIssueUser],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[DelFlag],
		[PassLine],
		[TotalScore],
		[IsIssueScore],
		[ScoreIssueUser],
		[ScoreIssueTime],
		[IsComputeScore],
		[ScoreComputeUser],
		[ScoreComputeTime]
	)
	VALUES
	(
		@TrainingItemCourseID,
		@TrainingItemID,
		@CourseID,
		@TeachModelID,
		@CourseStatus,
		@CourseBeginTime,
		@CourseEndTime,
		@TrainingModelID,
		@OuterOrgID,
		@OuterOrgDutyUser,
		@OuterOrgEMAIL,
		@CourseAttrID,
		@Score,
		@CourseHours,
		@BudgetFee,
		@Remark,
		@IsNeedApply,
		@IsPlanCourse,
		@IsLimit,
		@MaxNum,
		@IsInputGrade,
		@IsIssueGrade,
		@GradeIssueTime,
		@GradeIssueUser,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@DelFlag,
		@PassLine,
		@TotalScore,
		@IsIssueScore,
		@ScoreIssueUser,
		@ScoreIssueTime,
		@IsComputeScore,
		@ScoreComputeUser,
		@ScoreComputeTime
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourse_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourse_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourse_Update]
	@TrainingItemCourseID UniqueIdentifier,
	@TrainingItemID UniqueIdentifier,
	@CourseID UniqueIdentifier,
	@TeachModelID Int,
	@CourseStatus Int,
	@CourseBeginTime DateTime,
	@CourseEndTime DateTime,
	@TrainingModelID Int,
	@OuterOrgID UniqueIdentifier,
	@OuterOrgDutyUser NVarChar(128),
	@OuterOrgEMAIL NVarChar(256),
	@CourseAttrID Int,
	@Score Int,
	@CourseHours Decimal(8,2),
	@BudgetFee Decimal(15,2),
	@Remark NVarChar(Max),
	@IsNeedApply Bit,
	@IsPlanCourse Bit,
	@IsLimit Bit,
	@MaxNum Int,
	@IsInputGrade Bit,
	@IsIssueGrade Bit,
	@GradeIssueTime DateTime,
	@GradeIssueUser NVarChar(128),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@DelFlag Bit,
	@PassLine Decimal(6,2),
	@TotalScore Decimal(6,2),
	@IsIssueScore Bit,
	@ScoreIssueUser NVarChar(128),
	@ScoreIssueTime DateTime,
	@IsComputeScore Bit,
	@ScoreComputeUser NVarChar(128),
	@ScoreComputeTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Tr_ItemCourse] SET
		[TrainingItemID] = @TrainingItemID,
		[CourseID] = @CourseID,
		[TeachModelID] = @TeachModelID,
		[CourseStatus] = @CourseStatus,
		[CourseBeginTime] = @CourseBeginTime,
		[CourseEndTime] = @CourseEndTime,
		[TrainingModelID] = @TrainingModelID,
		[OuterOrgID] = @OuterOrgID,
		[OuterOrgDutyUser] = @OuterOrgDutyUser,
		[OuterOrgEMAIL] = @OuterOrgEMAIL,
		[CourseAttrID] = @CourseAttrID,
		[Score] = @Score,
		[CourseHours] = @CourseHours,
		[BudgetFee] = @BudgetFee,
		[Remark] = @Remark,
		[IsNeedApply] = @IsNeedApply,
		[IsPlanCourse] = @IsPlanCourse,
		[IsLimit] = @IsLimit,
		[MaxNum] = @MaxNum,
		[IsInputGrade] = @IsInputGrade,
		[IsIssueGrade] = @IsIssueGrade,
		[GradeIssueTime] = @GradeIssueTime,
		[GradeIssueUser] = @GradeIssueUser,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[DelFlag] = @DelFlag,
		[PassLine] = @PassLine,
		[TotalScore] = @TotalScore,
		[IsIssueScore] = @IsIssueScore,
		[ScoreIssueUser] = @ScoreIssueUser,
		[ScoreIssueTime] = @ScoreIssueTime,
		[IsComputeScore] = @IsComputeScore,
		[ScoreComputeUser] = @ScoreComputeUser,
		[ScoreComputeTime] = @ScoreComputeTime
	WHERE [TrainingItemCourseID] = @TrainingItemCourseID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourse_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourse_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourse_Delete]
	@TrainingItemCourseID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Tr_ItemCourse]
	WHERE [TrainingItemCourseID] = @TrainingItemCourseID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourse_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourse_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourse_GetByPk]
	@TrainingItemCourseID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[TrainingItemCourseID],
		[TrainingItemID],
		[CourseID],
		[TeachModelID],
		[CourseStatus],
		[CourseBeginTime],
		[CourseEndTime],
		[TrainingModelID],
		[OuterOrgID],
		[OuterOrgDutyUser],
		[OuterOrgEMAIL],
		[CourseAttrID],
		[Score],
		[CourseHours],
		[BudgetFee],
		[Remark],
		[IsNeedApply],
		[IsPlanCourse],
		[IsLimit],
		[MaxNum],
		[IsInputGrade],
		[IsIssueGrade],
		[GradeIssueTime],
		[GradeIssueUser],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[DelFlag],
		[PassLine],
		[TotalScore],
		[IsIssueScore],
		[ScoreIssueUser],
		[ScoreIssueTime],
		[IsComputeScore],
		[ScoreComputeUser],
		[ScoreComputeTime]
	FROM [dbo].[Tr_ItemCourse] 
	WHERE [TrainingItemCourseID] = @TrainingItemCourseID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourse_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourse_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourse_GetPagedList]
	@PageIndex int, --页号
	@PageSize int, --页面大小	
	@SortExpression varchar(1000) = '', --排序字段
	@Criteria varchar(1000) = '' --以AND开头的查询条件
AS
BEGIN
	--@Criteria参数：请根据实际查询需要进行增、删
	--@SortExpression参数：请根据实际查询需要进行默认值设定

	SET NOCOUNT ON
	
	DECLARE @SqlGet varchar(1600)
	DECLARE @TotalRecords int
	
	--创建表变量，存储数据主键
	CREATE TABLE #PageIndex
	(
		[IndexId] int IDENTITY (1, 1) NOT NULL,
		[TrainingItemCourseID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [TrainingItemCourseID] FROM [dbo].[Tr_ItemCourse]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[TrainingItemCourseID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[TrainingItemCourseID],
		biz.[TrainingItemID],
		biz.[CourseID],
		biz.[TeachModelID],
		biz.[CourseStatus],
		biz.[CourseBeginTime],
		biz.[CourseEndTime],
		biz.[TrainingModelID],
		biz.[OuterOrgID],
		biz.[OuterOrgDutyUser],
		biz.[OuterOrgEMAIL],
		biz.[CourseAttrID],
		biz.[Score],
		biz.[CourseHours],
		biz.[BudgetFee],
		biz.[Remark],
		biz.[IsNeedApply],
		biz.[IsPlanCourse],
		biz.[IsLimit],
		biz.[MaxNum],
		biz.[IsInputGrade],
		biz.[IsIssueGrade],
		biz.[GradeIssueTime],
		biz.[GradeIssueUser],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[DelFlag],
		biz.[PassLine],
		biz.[TotalScore],
		biz.[IsIssueScore],
		biz.[ScoreIssueUser],
		biz.[ScoreIssueTime],
		biz.[IsComputeScore],
		biz.[ScoreComputeUser],
		biz.[ScoreComputeTime]
	FROM [dbo].[Tr_ItemCourse] biz
	inner join #PageIndex p on  biz.[TrainingItemCourseID] = p.[TrainingItemCourseID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
