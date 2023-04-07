
------------------------------------------------------------------------------------------------------------------------
--Table Ex_Contest
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Ex_Contest_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Ex_Contest_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Ex_Contest_Insert]
	@ContestID UniqueIdentifier,
	@OrgID Int,
	@ContestName NVarChar(200),
	@ContestDesc NVarChar(-1),
	@IsShowAnswer Int,
	@ContestStatus Int,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1),
	@DelFlag Bit,
	@TestPaperID NVarChar(100)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Ex_Contest]
	(
		[ContestID],
		[OrgID],
		[ContestName],
		[ContestDesc],
		[IsShowAnswer],
		[ContestStatus],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[DelFlag],
		[TestPaperID]
	)
	VALUES
	(
		@ContestID,
		@OrgID,
		@ContestName,
		@ContestDesc,
		@IsShowAnswer,
		@ContestStatus,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@Remark,
		@DelFlag,
		@TestPaperID
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Ex_Contest_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Ex_Contest_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Ex_Contest_Update]
	@ContestID UniqueIdentifier,
	@OrgID Int,
	@ContestName NVarChar(200),
	@ContestDesc NVarChar(-1),
	@IsShowAnswer Int,
	@ContestStatus Int,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1),
	@DelFlag Bit,
	@TestPaperID NVarChar(100)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Ex_Contest] SET
		[OrgID] = @OrgID,
		[ContestName] = @ContestName,
		[ContestDesc] = @ContestDesc,
		[IsShowAnswer] = @IsShowAnswer,
		[ContestStatus] = @ContestStatus,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark,
		[DelFlag] = @DelFlag,
		[TestPaperID] = @TestPaperID
	WHERE [ContestID] = @ContestID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Ex_Contest_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Ex_Contest_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Ex_Contest_Delete]
	@ContestID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Ex_Contest]
	WHERE [ContestID] = @ContestID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Ex_Contest_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Ex_Contest_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Ex_Contest_GetByPk]
	@ContestID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ContestID],
		[OrgID],
		[ContestName],
		[ContestDesc],
		[IsShowAnswer],
		[ContestStatus],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[DelFlag],
		[TestPaperID]
	FROM [dbo].[Ex_Contest] 
	WHERE [ContestID] = @ContestID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Ex_Contest_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Ex_Contest_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Ex_Contest_GetPagedList]
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
		[ContestID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ContestID] FROM [dbo].[Ex_Contest]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ContestID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ContestID],
		biz.[OrgID],
		biz.[ContestName],
		biz.[ContestDesc],
		biz.[IsShowAnswer],
		biz.[ContestStatus],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark],
		biz.[DelFlag],
		biz.[TestPaperID]
	FROM [dbo].[Ex_Contest] biz
	inner join #PageIndex p on  biz.[ContestID] = p.[ContestID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
