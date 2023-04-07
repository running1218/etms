
------------------------------------------------------------------------------------------------------------------------
--Table Ex_StudentContest
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Ex_StudentContest_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Ex_StudentContest_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Ex_StudentContest_Insert]
	@StudentContestID UniqueIdentifier,
	@ContestID UniqueIdentifier,
	@StudentID Int,
	@TrainingItemCourseID UniqueIdentifier,
	@Score Decimal(18,0),
	@BeginTime DateTime,
	@EndTime DateTime,
	@UserExamID UniqueIdentifier,
	@StudentCourseID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Ex_StudentContest]
	(
		[StudentContestID],
		[ContestID],
		[StudentID],
		[TrainingItemCourseID],
		[Score],
		[BeginTime],
		[EndTime],
		[UserExamID],
		[StudentCourseID]
	)
	VALUES
	(
		@StudentContestID,
		@ContestID,
		@StudentID,
		@TrainingItemCourseID,
		@Score,
		@BeginTime,
		@EndTime,
		@UserExamID,
		@StudentCourseID
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Ex_StudentContest_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Ex_StudentContest_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Ex_StudentContest_Update]
	@StudentContestID UniqueIdentifier,
	@ContestID UniqueIdentifier,
	@StudentID Int,
	@TrainingItemCourseID UniqueIdentifier,
	@Score Decimal(18,0),
	@BeginTime DateTime,
	@EndTime DateTime,
	@UserExamID UniqueIdentifier,
	@StudentCourseID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Ex_StudentContest] SET
		[ContestID] = @ContestID,
		[StudentID] = @StudentID,
		[TrainingItemCourseID] = @TrainingItemCourseID,
		[Score] = @Score,
		[BeginTime] = @BeginTime,
		[EndTime] = @EndTime,
		[UserExamID] = @UserExamID,
		[StudentCourseID] = @StudentCourseID
	WHERE [StudentContestID] = @StudentContestID

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Ex_StudentContest_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Ex_StudentContest_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Ex_StudentContest_Delete]
	@StudentContestID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Ex_StudentContest]
	WHERE [StudentContestID] = @StudentContestID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Ex_StudentContest_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Ex_StudentContest_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Ex_StudentContest_GetByPk]
	@StudentContestID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[StudentContestID],
		[ContestID],
		[StudentID],
		[TrainingItemCourseID],
		[Score],
		[BeginTime],
		[EndTime],
		[UserExamID],
		[StudentCourseID]
	FROM [dbo].[Ex_StudentContest] 
	WHERE [StudentContestID] = @StudentContestID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Ex_StudentContest_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Ex_StudentContest_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Ex_StudentContest_GetPagedList]
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
		[StudentContestID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [StudentContestID] FROM [dbo].[Ex_StudentContest]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[StudentContestID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[StudentContestID],
		biz.[ContestID],
		biz.[StudentID],
		biz.[TrainingItemCourseID],
		biz.[Score],
		biz.[BeginTime],
		biz.[EndTime],
		biz.[UserExamID],
		biz.[StudentCourseID]
	FROM [dbo].[Ex_StudentContest] biz
	inner join #PageIndex p on  biz.[StudentContestID] = p.[StudentContestID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
