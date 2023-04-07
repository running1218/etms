
------------------------------------------------------------------------------------------------------------------------
--Table IDP_Plan
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_Plan_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_Plan_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_Plan_Insert]
	@IDP_PlanID UniqueIdentifier,
	@MentorID Int,
	@StudentID Int,
	@IDPTypeID Int,
	@IDPPlanBeginTime DateTime,
	@IDPPlanEndTime DateTime,
	@SuperiorName NVarChar(128),
	@SuperiorPosition NVarChar(100),
	@FillingDate DateTime,
	@CompletionRate Int,
	@Evaluation NVarChar(-1),
	@IsClose Bit,
	@CloseTime DateTime,
	@CloseUser NVarChar(128),
	@OrgID Int,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[IDP_Plan]
	(
		[IDP_PlanID],
		[MentorID],
		[StudentID],
		[IDPTypeID],
		[IDPPlanBeginTime],
		[IDPPlanEndTime],
		[SuperiorName],
		[SuperiorPosition],
		[FillingDate],
		[CompletionRate],
		[Evaluation],
		[IsClose],
		[CloseTime],
		[CloseUser],
		[OrgID],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	)
	VALUES
	(
		@IDP_PlanID,
		@MentorID,
		@StudentID,
		@IDPTypeID,
		@IDPPlanBeginTime,
		@IDPPlanEndTime,
		@SuperiorName,
		@SuperiorPosition,
		@FillingDate,
		@CompletionRate,
		@Evaluation,
		@IsClose,
		@CloseTime,
		@CloseUser,
		@OrgID,
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
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_Plan_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_Plan_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_Plan_Update]
	@IDP_PlanID UniqueIdentifier,
	@MentorID Int,
	@StudentID Int,
	@IDPTypeID Int,
	@IDPPlanBeginTime DateTime,
	@IDPPlanEndTime DateTime,
	@SuperiorName NVarChar(128),
	@SuperiorPosition NVarChar(100),
	@FillingDate DateTime,
	@CompletionRate Int,
	@Evaluation NVarChar(-1),
	@IsClose Bit,
	@CloseTime DateTime,
	@CloseUser NVarChar(128),
	@OrgID Int,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[IDP_Plan] SET
		[MentorID] = @MentorID,
		[StudentID] = @StudentID,
		[IDPTypeID] = @IDPTypeID,
		[IDPPlanBeginTime] = @IDPPlanBeginTime,
		[IDPPlanEndTime] = @IDPPlanEndTime,
		[SuperiorName] = @SuperiorName,
		[SuperiorPosition] = @SuperiorPosition,
		[FillingDate] = @FillingDate,
		[CompletionRate] = @CompletionRate,
		[Evaluation] = @Evaluation,
		[IsClose] = @IsClose,
		[CloseTime] = @CloseTime,
		[CloseUser] = @CloseUser,
		[OrgID] = @OrgID,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark
	WHERE [IDP_PlanID] = @IDP_PlanID

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_Plan_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_Plan_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_Plan_Delete]
	@IDP_PlanID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[IDP_Plan]
	WHERE [IDP_PlanID] = @IDP_PlanID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_Plan_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_Plan_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_Plan_GetByPk]
	@IDP_PlanID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[IDP_PlanID],
		[MentorID],
		[StudentID],
		[IDPTypeID],
		[IDPPlanBeginTime],
		[IDPPlanEndTime],
		[SuperiorName],
		[SuperiorPosition],
		[FillingDate],
		[CompletionRate],
		[Evaluation],
		[IsClose],
		[CloseTime],
		[CloseUser],
		[OrgID],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	FROM [dbo].[IDP_Plan] 
	WHERE [IDP_PlanID] = @IDP_PlanID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_Plan_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_Plan_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_IDP_Plan_GetPagedList]
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
		[IDP_PlanID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [IDP_PlanID] FROM [dbo].[IDP_Plan]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[IDP_PlanID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[IDP_PlanID],
		biz.[MentorID],
		biz.[StudentID],
		biz.[IDPTypeID],
		biz.[IDPPlanBeginTime],
		biz.[IDPPlanEndTime],
		biz.[SuperiorName],
		biz.[SuperiorPosition],
		biz.[FillingDate],
		biz.[CompletionRate],
		biz.[Evaluation],
		biz.[IsClose],
		biz.[CloseTime],
		biz.[CloseUser],
		biz.[OrgID],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark]
	FROM [dbo].[IDP_Plan] biz
	inner join #PageIndex p on  biz.[IDP_PlanID] = p.[IDP_PlanID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
