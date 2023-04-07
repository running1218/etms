
------------------------------------------------------------------------------------------------------------------------
--Table Evaluation_ItemResult
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_ItemResult_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_ItemResult_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_ItemResult_Insert]
	@ResultID UniqueIdentifier,
	@ItemID UniqueIdentifier,
	@UserID Int,
	@ObjectID NVarChar(100),
	@Score Int,
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Evaluation_ItemResult]
	(
		[ResultID],
		[ItemID],
		[UserID],
		[ObjectID],
		[Score],
		[CreateTime]
	)
	VALUES
	(
		@ResultID,
		@ItemID,
		@UserID,
		@ObjectID,
		@Score,
		@CreateTime
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_ItemResult_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_ItemResult_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_ItemResult_Update]
	@ResultID UniqueIdentifier,
	@ItemID UniqueIdentifier,
	@UserID Int,
	@ObjectID NVarChar(100),
	@Score Int,
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Evaluation_ItemResult] SET
		[ItemID] = @ItemID,
		[UserID] = @UserID,
		[ObjectID] = @ObjectID,
		[Score] = @Score,
		[CreateTime] = @CreateTime
	WHERE [ResultID] = @ResultID

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_ItemResult_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_ItemResult_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_ItemResult_Delete]
	@ResultID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Evaluation_ItemResult]
	WHERE [ResultID] = @ResultID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_ItemResult_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_ItemResult_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_ItemResult_GetByPk]
	@ResultID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ResultID],
		[ItemID],
		[UserID],
		[ObjectID],
		[Score],
		[CreateTime]
	FROM [dbo].[Evaluation_ItemResult] 
	WHERE [ResultID] = @ResultID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_ItemResult_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_ItemResult_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Evaluation_ItemResult_GetPagedList]
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
		[ResultID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ResultID] FROM [dbo].[Evaluation_ItemResult]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ResultID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ResultID],
		biz.[ItemID],
		biz.[UserID],
		biz.[ObjectID],
		biz.[Score],
		biz.[CreateTime]
	FROM [dbo].[Evaluation_ItemResult] biz
	inner join #PageIndex p on  biz.[ResultID] = p.[ResultID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
