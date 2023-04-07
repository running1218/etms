
------------------------------------------------------------------------------------------------------------------------
--Table Dic_Post
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Dic_Post_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Dic_Post_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Dic_Post_Insert]
	@PostID Int OUTPUT,
	@PostCode VarChar(10),
	@PostName VarChar(50),
	@PostTypeCode VarChar(20),
	@Description VarChar(500),
	@Liability VarChar(500),
	@OrganizationID Int,
	@Status SmallInt
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Dic_Post]
	(
		
		[PostCode],
		[PostName],
		[PostTypeCode],
		[Description],
		[Liability],
		[OrganizationID],
		[Status]
	)
	VALUES
	(
		
		@PostCode,
		@PostName,
		@PostTypeCode,
		@Description,
		@Liability,
		@OrganizationID,
		@Status
	)
	
	SET @PostID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Dic_Post_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Dic_Post_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Dic_Post_Update]
	@PostID Int,
	@PostCode VarChar(10),
	@PostName VarChar(50),
	@PostTypeCode VarChar(20),
	@Description VarChar(500),
	@Liability VarChar(500),
	@OrganizationID Int,
	@Status SmallInt
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Dic_Post] SET
		[PostCode] = @PostCode,
		[PostName] = @PostName,
		[PostTypeCode] = @PostTypeCode,
		[Description] = @Description,
		[Liability] = @Liability,
		[OrganizationID] = @OrganizationID,
		[Status] = @Status
	WHERE [PostID] = @PostID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Dic_Post_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Dic_Post_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Dic_Post_Delete]
	@PostID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Dic_Post]
	WHERE [PostID] = @PostID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Dic_Post_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Dic_Post_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Dic_Post_GetByPk]
	@PostID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[PostID],
		[PostCode],
		[PostName],
		[PostTypeCode],
		[Description],
		[Liability],
		[OrganizationID],
		[Status]
	FROM [dbo].[Dic_Post] 
	WHERE [PostID] = @PostID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Dic_Post_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Dic_Post_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Dic_Post_GetPagedList]
	@StartRowIndex int, --起始记录号
	@MaximumRows int, --记录数	
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
		[IndexId] int IDENTITY (0, 1) NOT NULL,
		[PostID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [PostID] FROM [dbo].[Dic_Post]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[PostID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	SELECT 
		biz.[PostID],
		biz.[PostCode],
		biz.[PostName],
		biz.[PostTypeCode],
		biz.[Description],
		biz.[Liability],
		biz.[OrganizationID],
		biz.[Status]
	FROM [dbo].[Dic_Post] biz, #PageIndex p
	WHERE
		biz.[PostID] = p.[PostID] AND
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @MaximumRows
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
