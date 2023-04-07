
------------------------------------------------------------------------------------------------------------------------
--Table QS_QueryTitle
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitle_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitle_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryTitle_Insert]
	@TitleID UniqueIdentifier,
	@QueryID UniqueIdentifier,
	@TitleTypeID Int,
	@TitleName NVarChar(2048),
	@TitleNo Int,
	@MinSelectNum Int,
	@MaxSelectNum Int,
	@CreateUserID Int,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(Max)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[QS_QueryTitle]
	(
		[TitleID],
		[QueryID],
		[TitleTypeID],
		[TitleName],
		[TitleNo],
		[MinSelectNum],
		[MaxSelectNum],
		[CreateUserID],
		[CreateTime],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	)
	VALUES
	(
		@TitleID,
		@QueryID,
		@TitleTypeID,
		@TitleName,
		@TitleNo,
		@MinSelectNum,
		@MaxSelectNum,
		@CreateUserID,
		@CreateTime,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@Remark
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitle_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitle_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryTitle_Update]
	@TitleID UniqueIdentifier,
	@QueryID UniqueIdentifier,
	@TitleTypeID Int,
	@TitleName NVarChar(2048),
	@TitleNo Int,
	@MinSelectNum Int,
	@MaxSelectNum Int,
	@CreateUserID Int,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(Max)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[QS_QueryTitle] SET
		[QueryID] = @QueryID,
		[TitleTypeID] = @TitleTypeID,
		[TitleName] = @TitleName,
		[TitleNo] = @TitleNo,
		[MinSelectNum] = @MinSelectNum,
		[MaxSelectNum] = @MaxSelectNum,
		[CreateUserID] = @CreateUserID,
		[CreateTime] = @CreateTime,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark
	WHERE [TitleID] = @TitleID

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitle_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitle_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryTitle_Delete]
	@TitleID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[QS_QueryTitle]
	WHERE [TitleID] = @TitleID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitle_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitle_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryTitle_GetByPk]
	@TitleID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[TitleID],
		[QueryID],
		[TitleTypeID],
		[TitleName],
		[TitleNo],
		[MinSelectNum],
		[MaxSelectNum],
		[CreateUserID],
		[CreateTime],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	FROM [dbo].[QS_QueryTitle] 
	WHERE [TitleID] = @TitleID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitle_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitle_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_QS_QueryTitle_GetPagedList]
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
		[TitleID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [TitleID] FROM [dbo].[QS_QueryTitle]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[TitleID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[TitleID],
		biz.[QueryID],
		biz.[TitleTypeID],
		biz.[TitleName],
		biz.[TitleNo],
		biz.[MinSelectNum],
		biz.[MaxSelectNum],
		biz.[CreateUserID],
		biz.[CreateTime],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark]
	FROM [dbo].[QS_QueryTitle] biz
	inner join #PageIndex p on  biz.[TitleID] = p.[TitleID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
