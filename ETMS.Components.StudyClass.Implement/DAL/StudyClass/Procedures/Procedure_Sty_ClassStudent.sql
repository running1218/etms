
------------------------------------------------------------------------------------------------------------------------
--Table Sty_ClassStudent
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_ClassStudent_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_ClassStudent_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Sty_ClassStudent_Insert]
	@ClassStudentID UniqueIdentifier,
	@ClassID UniqueIdentifier,
	@StudentSignupID UniqueIdentifier,
	@UserID Int,
	@IsDuty NVarChar(100),
	@IsBamboo Bit,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1),
	@DelFlag Bit
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Sty_ClassStudent]
	(
		[ClassStudentID],
		[ClassID],
		[StudentSignupID],
		[UserID],
		[IsDuty],
		[IsBamboo],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[DelFlag]
	)
	VALUES
	(
		@ClassStudentID,
		@ClassID,
		@StudentSignupID,
		@UserID,
		@IsDuty,
		@IsBamboo,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@Remark,
		@DelFlag
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_ClassStudent_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_ClassStudent_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Sty_ClassStudent_Update]
	@ClassStudentID UniqueIdentifier,
	@ClassID UniqueIdentifier,
	@StudentSignupID UniqueIdentifier,
	@UserID Int,
	@IsDuty NVarChar(100),
	@IsBamboo Bit,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1),
	@DelFlag Bit
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Sty_ClassStudent] SET
		[ClassID] = @ClassID,
		[StudentSignupID] = @StudentSignupID,
		[UserID] = @UserID,
		[IsDuty] = @IsDuty,
		[IsBamboo] = @IsBamboo,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark,
		[DelFlag] = @DelFlag
	WHERE [ClassStudentID] = @ClassStudentID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_ClassStudent_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_ClassStudent_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Sty_ClassStudent_Delete]
	@ClassStudentID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Sty_ClassStudent]
	WHERE [ClassStudentID] = @ClassStudentID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_ClassStudent_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_ClassStudent_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Sty_ClassStudent_GetByPk]
	@ClassStudentID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ClassStudentID],
		[ClassID],
		[StudentSignupID],
		[UserID],
		[IsDuty],
		[IsBamboo],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[DelFlag]
	FROM [dbo].[Sty_ClassStudent] 
	WHERE [ClassStudentID] = @ClassStudentID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_ClassStudent_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_ClassStudent_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Sty_ClassStudent_GetPagedList]
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
		[ClassStudentID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ClassStudentID] FROM [dbo].[Sty_ClassStudent]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ClassStudentID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ClassStudentID],
		biz.[ClassID],
		biz.[StudentSignupID],
		biz.[UserID],
		biz.[IsDuty],
		biz.[IsBamboo],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark],
		biz.[DelFlag]
	FROM [dbo].[Sty_ClassStudent] biz
	inner join #PageIndex p on  biz.[ClassStudentID] = p.[ClassStudentID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
