
------------------------------------------------------------------------------------------------------------------------
--Table QS_QueryArea
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryArea_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryArea_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryArea_Insert]
	@QueryAreaID UniqueIdentifier,
	@QueryID UniqueIdentifier,
	@AreaType NVarChar(200),
	@AreaCode NVarChar(100),
	@CreateUserID Int,
	@Creator NVarChar(128),
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[QS_QueryArea]
	(
		[QueryAreaID],
		[QueryID],
		[AreaType],
		[AreaCode],
		[CreateUserID],
		[Creator],
		[CreateTime]
	)
	VALUES
	(
		@QueryAreaID,
		@QueryID,
		@AreaType,
		@AreaCode,
		@CreateUserID,
		@Creator,
		@CreateTime
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryArea_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryArea_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryArea_Update]
	@QueryAreaID UniqueIdentifier,
	@QueryID UniqueIdentifier,
	@AreaType NVarChar(200),
	@AreaCode NVarChar(100),
	@CreateUserID Int,
	@Creator NVarChar(128),
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[QS_QueryArea] SET
		[QueryID] = @QueryID,
		[AreaType] = @AreaType,
		[AreaCode] = @AreaCode,
		[CreateUserID] = @CreateUserID,
		[Creator] = @Creator,
		[CreateTime] = @CreateTime
	WHERE [QueryAreaID] = @QueryAreaID

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryArea_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryArea_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryArea_Delete]
	@QueryAreaID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[QS_QueryArea]
	WHERE [QueryAreaID] = @QueryAreaID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryArea_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryArea_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryArea_GetByPk]
	@QueryAreaID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[QueryAreaID],
		[QueryID],
		[AreaType],
		[AreaCode],
		[CreateUserID],
		[Creator],
		[CreateTime]
	FROM [dbo].[QS_QueryArea] 
	WHERE [QueryAreaID] = @QueryAreaID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryArea_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryArea_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_QS_QueryArea_GetPagedList]
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
		[QueryAreaID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [QueryAreaID] FROM [dbo].[QS_QueryArea]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[QueryAreaID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[QueryAreaID],
		biz.[QueryID],
		biz.[AreaType],
		biz.[AreaCode],
		biz.[CreateUserID],
		biz.[Creator],
		biz.[CreateTime]
	FROM [dbo].[QS_QueryArea] biz
	inner join #PageIndex p on  biz.[QueryAreaID] = p.[QueryAreaID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
