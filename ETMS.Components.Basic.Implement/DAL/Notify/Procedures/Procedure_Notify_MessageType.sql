
------------------------------------------------------------------------------------------------------------------------
--Table Notify_MessageType
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageType_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageType_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageType_Insert]
	@MessageTypeID SmallInt,
	@MessageTypeName VarChar(100),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Notify_MessageType]
	(
		[MessageTypeID],
		[MessageTypeName],
		[OrderNum],
		[IsUse]
	)
	VALUES
	(
		@MessageTypeID,
		@MessageTypeName,
		@OrderNum,
		@IsUse
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageType_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageType_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageType_Update]
	@MessageTypeID SmallInt,
	@MessageTypeName VarChar(100),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Notify_MessageType] SET
		[MessageTypeName] = @MessageTypeName,
		[OrderNum] = @OrderNum,
		[IsUse] = @IsUse
	WHERE [MessageTypeID] = @MessageTypeID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageType_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageType_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageType_Delete]
	@MessageTypeID SmallInt
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Notify_MessageType]
	WHERE [MessageTypeID] = @MessageTypeID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageType_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageType_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageType_GetByPk]
	@MessageTypeID SmallInt
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[MessageTypeID],
		[MessageTypeName],
		[OrderNum],
		[IsUse]
	FROM [dbo].[Notify_MessageType] 
	WHERE [MessageTypeID] = @MessageTypeID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageType_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageType_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Notify_MessageType_GetPagedList]
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
		[MessageTypeID] SmallInt
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [MessageTypeID] FROM [dbo].[Notify_MessageType]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[MessageTypeID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[MessageTypeID],
		biz.[MessageTypeName],
		biz.[OrderNum],
		biz.[IsUse]
	FROM [dbo].[Notify_MessageType] biz
	inner join #PageIndex p on  biz.[MessageTypeID] = p.[MessageTypeID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
