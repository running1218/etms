
------------------------------------------------------------------------------------------------------------------------
--Table Res_TeacherCourse
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_TeacherCourse_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_TeacherCourse_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_TeacherCourse_Insert]
	@TeacherCourseID UniqueIdentifier,
	@TeacherID UniqueIdentifier,
	@CourseID UniqueIdentifier,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@CreateUserID Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Res_TeacherCourse]
	(
		[TeacherCourseID],
		[TeacherID],
		[CourseID],
		[CreateTime],
		[CreateUser],
		[CreateUserID]
	)
	VALUES
	(
		@TeacherCourseID,
		@TeacherID,
		@CourseID,
		@CreateTime,
		@CreateUser,
		@CreateUserID
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_TeacherCourse_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_TeacherCourse_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_TeacherCourse_Update]
	@TeacherCourseID UniqueIdentifier,
	@TeacherID UniqueIdentifier,
	@CourseID UniqueIdentifier,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@CreateUserID Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Res_TeacherCourse] SET
		[TeacherID] = @TeacherID,
		[CourseID] = @CourseID,
		[CreateTime] = @CreateTime,
		[CreateUser] = @CreateUser,
		[CreateUserID] = @CreateUserID
	WHERE [TeacherCourseID] = @TeacherCourseID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_TeacherCourse_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_TeacherCourse_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_TeacherCourse_Delete]
	@TeacherID UniqueIdentifier,
	@CourseID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Res_TeacherCourse]
	WHERE [TeacherID] = @TeacherID
		  AND [CourseID] = @CourseID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_TeacherCourse_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_TeacherCourse_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_TeacherCourse_GetByPk]
	@TeacherCourseID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[TeacherCourseID],
		[TeacherID],
		[CourseID],
		[CreateTime],
		[CreateUser],
		[CreateUserID]
	FROM [dbo].[Res_TeacherCourse] 
	WHERE [TeacherCourseID] = @TeacherCourseID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_TeacherCourse_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_TeacherCourse_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Res_TeacherCourse_GetPagedList]
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
		[TeacherCourseID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [TeacherCourseID] FROM [dbo].[Res_TeacherCourse]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[TeacherCourseID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[TeacherCourseID],
		biz.[TeacherID],
		biz.[CourseID],
		biz.[CreateTime],
		biz.[CreateUser],
		biz.[CreateUserID]
	FROM [dbo].[Res_TeacherCourse] biz
	inner join #PageIndex p on  biz.[TeacherCourseID] = p.[TeacherCourseID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
