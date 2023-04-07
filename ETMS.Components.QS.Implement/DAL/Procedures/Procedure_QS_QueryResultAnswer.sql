
------------------------------------------------------------------------------------------------------------------------
--Table QS_QueryResultAnswer
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResultAnswer_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_Insert]
	@AnswerResultID UniqueIdentifier,
	@BatchID UniqueIdentifier,
	@TitleID UniqueIdentifier,
	@Answer NVarChar(2048)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[QS_QueryResultAnswer]
	(
		[AnswerResultID],
		[BatchID],
		[TitleID],
		[Answer]
	)
	VALUES
	(
		@AnswerResultID,
		@BatchID,
		@TitleID,
		@Answer
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResultAnswer_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_Update]
	@AnswerResultID UniqueIdentifier,
	@BatchID UniqueIdentifier,
	@TitleID UniqueIdentifier,
	@Answer NVarChar(2048)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[QS_QueryResultAnswer] SET
		[BatchID] = @BatchID,
		[TitleID] = @TitleID,
		[Answer] = @Answer
	WHERE [AnswerResultID] = @AnswerResultID

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResultAnswer_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_Delete]
	@AnswerResultID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[QS_QueryResultAnswer]
	WHERE [AnswerResultID] = @AnswerResultID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResultAnswer_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_GetByPk]
	@AnswerResultID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[AnswerResultID],
		[BatchID],
		[TitleID],
		[Answer]
	FROM [dbo].[QS_QueryResultAnswer] 
	WHERE [AnswerResultID] = @AnswerResultID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResultAnswer_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_GetPagedList]
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
		[AnswerResultID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [AnswerResultID] FROM [dbo].[QS_QueryResultAnswer]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[AnswerResultID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[AnswerResultID],
		biz.[BatchID],
		biz.[TitleID],
		biz.[Answer]
	FROM [dbo].[QS_QueryResultAnswer] biz
	inner join #PageIndex p on  biz.[AnswerResultID] = p.[AnswerResultID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
