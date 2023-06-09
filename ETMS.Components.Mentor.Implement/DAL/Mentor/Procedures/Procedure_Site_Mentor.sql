
------------------------------------------------------------------------------------------------------------------------
--Table Site_Mentor
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_Mentor_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_Mentor_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_Mentor_Insert]
	@MentorID Int,
	@IsUse Int,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@CreateUserID Int,
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Site_Mentor]
	(
		[MentorID],
		[IsUse],
		[CreateTime],
		[CreateUser],
		[CreateUserID],
		[ModifyTime],
		[ModifyUser]
	)
	VALUES
	(
		@MentorID,
		@IsUse,
		@CreateTime,
		@CreateUser,
		@CreateUserID,
		@ModifyTime,
		@ModifyUser
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_Mentor_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_Mentor_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_Mentor_Update]
	@MentorID Int,
	@IsUse Int,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@CreateUserID Int,
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Site_Mentor] SET
		[IsUse] = @IsUse,
		[CreateTime] = @CreateTime,
		[CreateUser] = @CreateUser,
		[CreateUserID] = @CreateUserID,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser
	WHERE [MentorID] = @MentorID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_Mentor_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_Mentor_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_Mentor_Delete]
	@MentorID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Site_Mentor]
	WHERE [MentorID] = @MentorID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_Mentor_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_Mentor_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_Mentor_GetByPk]
	@MentorID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[MentorID],
		[IsUse],
		[CreateTime],
		[CreateUser],
		[CreateUserID],
		[ModifyTime],
		[ModifyUser]
	FROM [dbo].[Site_Mentor] 
	WHERE [MentorID] = @MentorID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_Mentor_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_Mentor_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Site_Mentor_GetPagedList]
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
		[MentorID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [MentorID] FROM [dbo].[Site_Mentor]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[MentorID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[MentorID],
		biz.[IsUse],
		biz.[CreateTime],
		biz.[CreateUser],
		biz.[CreateUserID],
		biz.[ModifyTime],
		biz.[ModifyUser]
	FROM [dbo].[Site_Mentor] biz
	inner join #PageIndex p on  biz.[MentorID] = p.[MentorID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
