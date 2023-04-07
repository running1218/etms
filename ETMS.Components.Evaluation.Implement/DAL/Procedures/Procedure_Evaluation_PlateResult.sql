
------------------------------------------------------------------------------------------------------------------------
--Table Evaluation_PlateResult
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_PlateResult_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_PlateResult_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_PlateResult_Insert]
	@ResultSubID UniqueIdentifier,
	@PlateID UniqueIdentifier,
	@UserID Int,
	@ObjectID NVarChar(100),
	@EvaluationContent NVarChar(-1),
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Evaluation_PlateResult]
	(
		[ResultSubID],
		[PlateID],
		[UserID],
		[ObjectID],
		[EvaluationContent],
		[CreateTime]
	)
	VALUES
	(
		@ResultSubID,
		@PlateID,
		@UserID,
		@ObjectID,
		@EvaluationContent,
		@CreateTime
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_PlateResult_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_PlateResult_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_PlateResult_Update]
	@ResultSubID UniqueIdentifier,
	@PlateID UniqueIdentifier,
	@UserID Int,
	@ObjectID NVarChar(100),
	@EvaluationContent NVarChar(-1),
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Evaluation_PlateResult] SET
		[PlateID] = @PlateID,
		[UserID] = @UserID,
		[ObjectID] = @ObjectID,
		[EvaluationContent] = @EvaluationContent,
		[CreateTime] = @CreateTime
	WHERE [ResultSubID] = @ResultSubID

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_PlateResult_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_PlateResult_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_PlateResult_Delete]
	@ResultSubID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Evaluation_PlateResult]
	WHERE [ResultSubID] = @ResultSubID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_PlateResult_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_PlateResult_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_PlateResult_GetByPk]
	@ResultSubID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ResultSubID],
		[PlateID],
		[UserID],
		[ObjectID],
		[EvaluationContent],
		[CreateTime]
	FROM [dbo].[Evaluation_PlateResult] 
	WHERE [ResultSubID] = @ResultSubID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_PlateResult_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_PlateResult_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Evaluation_PlateResult_GetPagedList]
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
		[ResultSubID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ResultSubID] FROM [dbo].[Evaluation_PlateResult]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ResultSubID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ResultSubID],
		biz.[PlateID],
		biz.[UserID],
		biz.[ObjectID],
		biz.[EvaluationContent],
		biz.[CreateTime]
	FROM [dbo].[Evaluation_PlateResult] biz
	inner join #PageIndex p on  biz.[ResultSubID] = p.[ResultSubID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
