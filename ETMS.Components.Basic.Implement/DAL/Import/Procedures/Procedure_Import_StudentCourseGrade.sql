
------------------------------------------------------------------------------------------------------------------------
--Table Import_StudentCourseGrade
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_StudentCourseGrade_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_StudentCourseGrade_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_StudentCourseGrade_Insert]
	@DetailID Int OUTPUT,
	@TaskID Int,
	@Status SmallInt,
	@Remark NVarChar(1000),
	@UserID Int,
	@LoginName NVarChar(100),
	@RealName NVarChar(100),
	@SumGrade NVarChar(100),
	@TrainingItemID NVarChar(100),
	@ItemCode NVarChar(100),
	@ItemName NVarChar(200),
	@CourseID NVarChar(100),
	@CourseCode NVarChar(100),
	@CourseName NVarChar(200),
	@StudentCourseID NVarChar(100),
	@OrgID Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Import_StudentCourseGrade]
	(
		
		[TaskID],
		[Status],
		[Remark],
		[UserID],
		[LoginName],
		[RealName],
		[SumGrade],
		[TrainingItemID],
		[ItemCode],
		[ItemName],
		[CourseID],
		[CourseCode],
		[CourseName],
		[StudentCourseID],
		[OrgID]
	)
	VALUES
	(
		
		@TaskID,
		@Status,
		@Remark,
		@UserID,
		@LoginName,
		@RealName,
		@SumGrade,
		@TrainingItemID,
		@ItemCode,
		@ItemName,
		@CourseID,
		@CourseCode,
		@CourseName,
		@StudentCourseID,
		@OrgID
	)
	
	SET @DetailID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_StudentCourseGrade_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_StudentCourseGrade_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_StudentCourseGrade_Update]
	@DetailID Int,
	@TaskID Int,
	@Status SmallInt,
	@Remark NVarChar(1000),
	@UserID Int,
	@LoginName NVarChar(100),
	@RealName NVarChar(100),
	@SumGrade NVarChar(100),
	@TrainingItemID NVarChar(100),
	@ItemCode NVarChar(100),
	@ItemName NVarChar(200),
	@CourseID NVarChar(100),
	@CourseCode NVarChar(100),
	@CourseName NVarChar(200),
	@StudentCourseID NVarChar(100),
	@OrgID Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Import_StudentCourseGrade] SET
		[TaskID] = @TaskID,
		[Status] = @Status,
		[Remark] = @Remark,
		[UserID] = @UserID,
		[LoginName] = @LoginName,
		[RealName] = @RealName,
		[SumGrade] = @SumGrade,
		[TrainingItemID] = @TrainingItemID,
		[ItemCode] = @ItemCode,
		[ItemName] = @ItemName,
		[CourseID] = @CourseID,
		[CourseCode] = @CourseCode,
		[CourseName] = @CourseName,
		[StudentCourseID] = @StudentCourseID,
		[OrgID] = @OrgID
	WHERE [DetailID] = @DetailID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_StudentCourseGrade_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_StudentCourseGrade_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_StudentCourseGrade_Delete]
	@DetailID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Import_StudentCourseGrade]
	WHERE [DetailID] = @DetailID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_StudentCourseGrade_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_StudentCourseGrade_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_StudentCourseGrade_GetByPk]
	@DetailID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[DetailID],
		[TaskID],
		[Status],
		[Remark],
		[UserID],
		[LoginName],
		[RealName],
		[SumGrade],
		[TrainingItemID],
		[ItemCode],
		[ItemName],
		[CourseID],
		[CourseCode],
		[CourseName],
		[StudentCourseID],
		[OrgID]
	FROM [dbo].[Import_StudentCourseGrade] 
	WHERE [DetailID] = @DetailID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_StudentCourseGrade_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_StudentCourseGrade_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Import_StudentCourseGrade_GetPagedList]
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
		[DetailID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [DetailID] FROM [dbo].[Import_StudentCourseGrade]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[DetailID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[DetailID],
		biz.[TaskID],
		biz.[Status],
		biz.[Remark],
		biz.[UserID],
		biz.[LoginName],
		biz.[RealName],
		biz.[SumGrade],
		biz.[TrainingItemID],
		biz.[ItemCode],
		biz.[ItemName],
		biz.[CourseID],
		biz.[CourseCode],
		biz.[CourseName],
		biz.[StudentCourseID],
		biz.[OrgID]
	FROM [dbo].[Import_StudentCourseGrade] biz
	inner join #PageIndex p on  biz.[DetailID] = p.[DetailID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
