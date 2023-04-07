
------------------------------------------------------------------------------------------------------------------------
--Table IDP_PlanContent
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanContent_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanContent_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanContent_Insert]
	@IDPPlanContentID UniqueIdentifier,
	@IDP_PlanID UniqueIdentifier,
	@StudyContent NVarChar(-1),
	@StudyMode NVarChar(-1),
	@PlanFinishingTime DateTime,
	@FinishedState Int,
	@FinishedTime DateTime,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@CreateUserID Int,
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[IDP_PlanContent]
	(
		[IDPPlanContentID],
		[IDP_PlanID],
		[StudyContent],
		[StudyMode],
		[PlanFinishingTime],
		[FinishedState],
		[FinishedTime],
		[CreateTime],
		[CreateUser],
		[CreateUserID],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	)
	VALUES
	(
		@IDPPlanContentID,
		@IDP_PlanID,
		@StudyContent,
		@StudyMode,
		@PlanFinishingTime,
		@FinishedState,
		@FinishedTime,
		@CreateTime,
		@CreateUser,
		@CreateUserID,
		@ModifyTime,
		@ModifyUser,
		@Remark
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanContent_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanContent_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanContent_Update]
	@IDPPlanContentID UniqueIdentifier,
	@IDP_PlanID UniqueIdentifier,
	@StudyContent NVarChar(-1),
	@StudyMode NVarChar(-1),
	@PlanFinishingTime DateTime,
	@FinishedState Int,
	@FinishedTime DateTime,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@CreateUserID Int,
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[IDP_PlanContent] SET
		[IDP_PlanID] = @IDP_PlanID,
		[StudyContent] = @StudyContent,
		[StudyMode] = @StudyMode,
		[PlanFinishingTime] = @PlanFinishingTime,
		[FinishedState] = @FinishedState,
		[FinishedTime] = @FinishedTime,
		[CreateTime] = @CreateTime,
		[CreateUser] = @CreateUser,
		[CreateUserID] = @CreateUserID,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark
	WHERE [IDPPlanContentID] = @IDPPlanContentID

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanContent_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanContent_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanContent_Delete]
	@IDPPlanContentID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[IDP_PlanContent]
	WHERE [IDPPlanContentID] = @IDPPlanContentID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanContent_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanContent_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanContent_GetByPk]
	@IDPPlanContentID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[IDPPlanContentID],
		[IDP_PlanID],
		[StudyContent],
		[StudyMode],
		[PlanFinishingTime],
		[FinishedState],
		[FinishedTime],
		[CreateTime],
		[CreateUser],
		[CreateUserID],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	FROM [dbo].[IDP_PlanContent] 
	WHERE [IDPPlanContentID] = @IDPPlanContentID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanContent_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanContent_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_IDP_PlanContent_GetPagedList]
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
		[IDPPlanContentID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [IDPPlanContentID] FROM [dbo].[IDP_PlanContent]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[IDPPlanContentID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[IDPPlanContentID],
		biz.[IDP_PlanID],
		biz.[StudyContent],
		biz.[StudyMode],
		biz.[PlanFinishingTime],
		biz.[FinishedState],
		biz.[FinishedTime],
		biz.[CreateTime],
		biz.[CreateUser],
		biz.[CreateUserID],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark]
	FROM [dbo].[IDP_PlanContent] biz
	inner join #PageIndex p on  biz.[IDPPlanContentID] = p.[IDPPlanContentID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
