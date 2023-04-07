
------------------------------------------------------------------------------------------------------------------------
--Table IDP_PlanObject
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObject_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObject_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanObject_Insert]
	@IDPPlanObjectID UniqueIdentifier,
	@IDP_PlanID UniqueIdentifier,
	@PlanDevelopment NVarChar(-1),
	@Ability NVarChar(-1),
	@HopeLevel NVarChar(-1),
	@SuperiorOpinion NVarChar(-1),
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@CreateUserID Int,
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1),
	@IsContinue Bit
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[IDP_PlanObject]
	(
		[IDPPlanObjectID],
		[IDP_PlanID],
		[PlanDevelopment],
		[Ability],
		[HopeLevel],
		[SuperiorOpinion],
		[CreateTime],
		[CreateUser],
		[CreateUserID],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[IsContinue]
	)
	VALUES
	(
		@IDPPlanObjectID,
		@IDP_PlanID,
		@PlanDevelopment,
		@Ability,
		@HopeLevel,
		@SuperiorOpinion,
		@CreateTime,
		@CreateUser,
		@CreateUserID,
		@ModifyTime,
		@ModifyUser,
		@Remark,
		@IsContinue
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObject_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObject_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanObject_Update]
	@IDPPlanObjectID UniqueIdentifier,
	@IDP_PlanID UniqueIdentifier,
	@PlanDevelopment NVarChar(-1),
	@Ability NVarChar(-1),
	@HopeLevel NVarChar(-1),
	@SuperiorOpinion NVarChar(-1),
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@CreateUserID Int,
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1),
	@IsContinue Bit
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[IDP_PlanObject] SET
		[IDP_PlanID] = @IDP_PlanID,
		[PlanDevelopment] = @PlanDevelopment,
		[Ability] = @Ability,
		[HopeLevel] = @HopeLevel,
		[SuperiorOpinion] = @SuperiorOpinion,
		[CreateTime] = @CreateTime,
		[CreateUser] = @CreateUser,
		[CreateUserID] = @CreateUserID,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark,
		[IsContinue] = @IsContinue
	WHERE [IDPPlanObjectID] = @IDPPlanObjectID

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObject_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObject_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanObject_Delete]
	@IDPPlanObjectID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[IDP_PlanObject]
	WHERE [IDPPlanObjectID] = @IDPPlanObjectID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObject_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObject_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanObject_GetByPk]
	@IDPPlanObjectID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[IDPPlanObjectID],
		[IDP_PlanID],
		[PlanDevelopment],
		[Ability],
		[HopeLevel],
		[SuperiorOpinion],
		[CreateTime],
		[CreateUser],
		[CreateUserID],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[IsContinue]
	FROM [dbo].[IDP_PlanObject] 
	WHERE [IDPPlanObjectID] = @IDPPlanObjectID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObject_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObject_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_IDP_PlanObject_GetPagedList]
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
		[IDPPlanObjectID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [IDPPlanObjectID] FROM [dbo].[IDP_PlanObject]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[IDPPlanObjectID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[IDPPlanObjectID],
		biz.[IDP_PlanID],
		biz.[PlanDevelopment],
		biz.[Ability],
		biz.[HopeLevel],
		biz.[SuperiorOpinion],
		biz.[CreateTime],
		biz.[CreateUser],
		biz.[CreateUserID],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark],
		biz.[IsContinue]
	FROM [dbo].[IDP_PlanObject] biz
	inner join #PageIndex p on  biz.[IDPPlanObjectID] = p.[IDPPlanObjectID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
