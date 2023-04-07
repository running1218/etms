
------------------------------------------------------------------------------------------------------------------------
--Table Site_SysConfigGroup
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfigGroup_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfigGroup_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_SysConfigGroup_Insert]
	@ConfigGroupID Int,
	@ConfigGroupName VarChar(100),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Site_SysConfigGroup]
	(
		[ConfigGroupID],
		[ConfigGroupName],
		[OrderNum],
		[IsUse]
	)
	VALUES
	(
		@ConfigGroupID,
		@ConfigGroupName,
		@OrderNum,
		@IsUse
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfigGroup_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfigGroup_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_SysConfigGroup_Update]
	@ConfigGroupID Int,
	@ConfigGroupName VarChar(100),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Site_SysConfigGroup] SET
		[ConfigGroupName] = @ConfigGroupName,
		[OrderNum] = @OrderNum,
		[IsUse] = @IsUse
	WHERE [ConfigGroupID] = @ConfigGroupID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfigGroup_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfigGroup_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_SysConfigGroup_Delete]
	@ConfigGroupID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Site_SysConfigGroup]
	WHERE [ConfigGroupID] = @ConfigGroupID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfigGroup_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfigGroup_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_SysConfigGroup_GetByPk]
	@ConfigGroupID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ConfigGroupID],
		[ConfigGroupName],
		[OrderNum],
		[IsUse]
	FROM [dbo].[Site_SysConfigGroup] 
	WHERE [ConfigGroupID] = @ConfigGroupID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfigGroup_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfigGroup_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Site_SysConfigGroup_GetPagedList]
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
		[ConfigGroupID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ConfigGroupID] FROM [dbo].[Site_SysConfigGroup]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ConfigGroupID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ConfigGroupID],
		biz.[ConfigGroupName],
		biz.[OrderNum],
		biz.[IsUse]
	FROM [dbo].[Site_SysConfigGroup] biz
	inner join #PageIndex p on  biz.[ConfigGroupID] = p.[ConfigGroupID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
