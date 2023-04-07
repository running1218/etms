
------------------------------------------------------------------------------------------------------------------------
--Table Res_StudyMapReferCourse
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_StudyMapReferCourse_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_StudyMapReferCourse_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_StudyMapReferCourse_Insert]
	@StudyMapReferCourseID UniqueIdentifier,
	@StudyMapID UniqueIdentifier,
	@CourseID UniqueIdentifier,
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
	
	INSERT INTO [dbo].[Res_StudyMapReferCourse]
	(
		[StudyMapReferCourseID],
		[StudyMapID],
		[CourseID],
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
		@StudyMapReferCourseID,
		@StudyMapID,
		@CourseID,
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
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_StudyMapReferCourse_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_StudyMapReferCourse_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_StudyMapReferCourse_Update]
	@StudyMapReferCourseID UniqueIdentifier,
	@StudyMapID UniqueIdentifier,
	@CourseID UniqueIdentifier,
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

	UPDATE [dbo].[Res_StudyMapReferCourse] SET
		[StudyMapID] = @StudyMapID,
		[CourseID] = @CourseID,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark,
		[DelFlag] = @DelFlag
	WHERE [StudyMapReferCourseID] = @StudyMapReferCourseID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_StudyMapReferCourse_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_StudyMapReferCourse_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_StudyMapReferCourse_Delete]
	@StudyMapReferCourseID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Res_StudyMapReferCourse]
	WHERE [StudyMapReferCourseID] = @StudyMapReferCourseID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_StudyMapReferCourse_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_StudyMapReferCourse_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_StudyMapReferCourse_GetByPk]
	@StudyMapReferCourseID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[StudyMapReferCourseID],
		[StudyMapID],
		[CourseID],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[DelFlag]
	FROM [dbo].[Res_StudyMapReferCourse] 
	WHERE [StudyMapReferCourseID] = @StudyMapReferCourseID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_StudyMapReferCourse_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_StudyMapReferCourse_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Res_StudyMapReferCourse_GetPagedList]
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
		[StudyMapReferCourseID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [StudyMapReferCourseID] FROM [dbo].[Res_StudyMapReferCourse]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[StudyMapReferCourseID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[StudyMapReferCourseID],
		biz.[StudyMapID],
		biz.[CourseID],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark],
		biz.[DelFlag]
	FROM [dbo].[Res_StudyMapReferCourse] biz
	inner join #PageIndex p on  biz.[StudyMapReferCourseID] = p.[StudyMapReferCourseID] AND
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
