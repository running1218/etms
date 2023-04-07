
------------------------------------------------------------------------------------------------------------------------
--Table Import_StudentSignup
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_StudentSignup_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_StudentSignup_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_StudentSignup_Insert]
	@DetailID Int OUTPUT,
	@TaskID Int,
	@Status SmallInt,
	@Remark NVarChar(1000),
	@ItemCode NVarChar(100),
	@ItemName NVarChar(200),
	@TrainingItemID UniqueIdentifier,
	@OrgID Int,
	@UserID Int,
	@LoginName NVarChar(100),
	@RealName NVarChar(100),
	@DepartmentName NVarChar(100),
	@RankName NVarChar(100),
	@PostName NVarChar(100)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Import_StudentSignup]
	(
		
		[TaskID],
		[Status],
		[Remark],
		[ItemCode],
		[ItemName],
		[TrainingItemID],
		[OrgID],
		[UserID],
		[LoginName],
		[RealName],
		[DepartmentName],
		[RankName],
		[PostName]
	)
	VALUES
	(
		
		@TaskID,
		@Status,
		@Remark,
		@ItemCode,
		@ItemName,
		@TrainingItemID,
		@OrgID,
		@UserID,
		@LoginName,
		@RealName,
		@DepartmentName,
		@RankName,
		@PostName
	)
	
	SET @DetailID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_StudentSignup_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_StudentSignup_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_StudentSignup_Update]
	@DetailID Int,
	@TaskID Int,
	@Status SmallInt,
	@Remark NVarChar(1000),
	@ItemCode NVarChar(100),
	@ItemName NVarChar(200),
	@TrainingItemID UniqueIdentifier,
	@OrgID Int,
	@UserID Int,
	@LoginName NVarChar(100),
	@RealName NVarChar(100),
	@DepartmentName NVarChar(100),
	@RankName NVarChar(100),
	@PostName NVarChar(100)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Import_StudentSignup] SET
		[TaskID] = @TaskID,
		[Status] = @Status,
		[Remark] = @Remark,
		[ItemCode] = @ItemCode,
		[ItemName] = @ItemName,
		[TrainingItemID] = @TrainingItemID,
		[OrgID] = @OrgID,
		[UserID] = @UserID,
		[LoginName] = @LoginName,
		[RealName] = @RealName,
		[DepartmentName] = @DepartmentName,
		[RankName] = @RankName,
		[PostName] = @PostName
	WHERE [DetailID] = @DetailID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_StudentSignup_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_StudentSignup_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_StudentSignup_Delete]
	@DetailID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Import_StudentSignup]
	WHERE [DetailID] = @DetailID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_StudentSignup_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_StudentSignup_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_StudentSignup_GetByPk]
	@DetailID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[DetailID],
		[TaskID],
		[Status],
		[Remark],
		[ItemCode],
		[ItemName],
		[TrainingItemID],
		[OrgID],
		[UserID],
		[LoginName],
		[RealName],
		[DepartmentName],
		[RankName],
		[PostName]
	FROM [dbo].[Import_StudentSignup] 
	WHERE [DetailID] = @DetailID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_StudentSignup_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_StudentSignup_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Import_StudentSignup_GetPagedList]
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
	
	SET @SqlGet = 'SELECT [DetailID] FROM [dbo].[Import_StudentSignup]  WHERE 1=1 ' + @Criteria + @SortExpression
	
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
		biz.[ItemCode],
		biz.[ItemName],
		biz.[TrainingItemID],
		biz.[OrgID],
		biz.[UserID],
		biz.[LoginName],
		biz.[RealName],
		biz.[DepartmentName],
		biz.[RankName],
		biz.[PostName]
	FROM [dbo].[Import_StudentSignup] biz
	inner join #PageIndex p on  biz.[DetailID] = p.[DetailID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
