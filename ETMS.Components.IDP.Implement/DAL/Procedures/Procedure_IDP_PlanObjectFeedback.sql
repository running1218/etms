
------------------------------------------------------------------------------------------------------------------------
--Table IDP_PlanObjectFeedback
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObjectFeedback_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObjectFeedback_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanObjectFeedback_Insert]
	@IDPPlanObjectFeedbackID UniqueIdentifier,
	@IDPPlanObjectID UniqueIdentifier,
	@StudentEvaluation NVarChar(-1),
	@StudentEvaluationTime DateTime,
	@SuperiorEvaluation NVarChar(-1),
	@SuperiorEvaluationTime DateTime,
	@SuperiorName NVarChar(128),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[IDP_PlanObjectFeedback]
	(
		[IDPPlanObjectFeedbackID],
		[IDPPlanObjectID],
		[StudentEvaluation],
		[StudentEvaluationTime],
		[SuperiorEvaluation],
		[SuperiorEvaluationTime],
		[SuperiorName],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	)
	VALUES
	(
		@IDPPlanObjectFeedbackID,
		@IDPPlanObjectID,
		@StudentEvaluation,
		@StudentEvaluationTime,
		@SuperiorEvaluation,
		@SuperiorEvaluationTime,
		@SuperiorName,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@Remark
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObjectFeedback_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObjectFeedback_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanObjectFeedback_Update]
	@IDPPlanObjectFeedbackID UniqueIdentifier,
	@IDPPlanObjectID UniqueIdentifier,
	@StudentEvaluation NVarChar(-1),
	@StudentEvaluationTime DateTime,
	@SuperiorEvaluation NVarChar(-1),
	@SuperiorEvaluationTime DateTime,
	@SuperiorName NVarChar(128),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[IDP_PlanObjectFeedback] SET
		[IDPPlanObjectID] = @IDPPlanObjectID,
		[StudentEvaluation] = @StudentEvaluation,
		[StudentEvaluationTime] = @StudentEvaluationTime,
		[SuperiorEvaluation] = @SuperiorEvaluation,
		[SuperiorEvaluationTime] = @SuperiorEvaluationTime,
		[SuperiorName] = @SuperiorName,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark
	WHERE [IDPPlanObjectFeedbackID] = @IDPPlanObjectFeedbackID

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObjectFeedback_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObjectFeedback_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanObjectFeedback_Delete]
	@IDPPlanObjectFeedbackID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[IDP_PlanObjectFeedback]
	WHERE [IDPPlanObjectFeedbackID] = @IDPPlanObjectFeedbackID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObjectFeedback_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObjectFeedback_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanObjectFeedback_GetByPk]
	@IDPPlanObjectFeedbackID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[IDPPlanObjectFeedbackID],
		[IDPPlanObjectID],
		[StudentEvaluation],
		[StudentEvaluationTime],
		[SuperiorEvaluation],
		[SuperiorEvaluationTime],
		[SuperiorName],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	FROM [dbo].[IDP_PlanObjectFeedback] 
	WHERE [IDPPlanObjectFeedbackID] = @IDPPlanObjectFeedbackID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObjectFeedback_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObjectFeedback_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_IDP_PlanObjectFeedback_GetPagedList]
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
		[IDPPlanObjectFeedbackID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [IDPPlanObjectFeedbackID] FROM [dbo].[IDP_PlanObjectFeedback]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[IDPPlanObjectFeedbackID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[IDPPlanObjectFeedbackID],
		biz.[IDPPlanObjectID],
		biz.[StudentEvaluation],
		biz.[StudentEvaluationTime],
		biz.[SuperiorEvaluation],
		biz.[SuperiorEvaluationTime],
		biz.[SuperiorName],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark]
	FROM [dbo].[IDP_PlanObjectFeedback] biz
	inner join #PageIndex p on  biz.[IDPPlanObjectFeedbackID] = p.[IDPPlanObjectFeedbackID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
