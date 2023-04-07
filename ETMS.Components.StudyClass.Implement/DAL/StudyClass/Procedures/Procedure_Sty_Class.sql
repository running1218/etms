
------------------------------------------------------------------------------------------------------------------------
--Table Sty_Class
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_Class_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_Class_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Sty_Class_Insert]
	@ClassID UniqueIdentifier,
	@TrainingItemID UniqueIdentifier,
	@ClassName NVarChar(200),
	@ClassDesc NVarChar(2048),
	@StudentNum Int,
	@DutyUser NVarChar(128),
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
	
	INSERT INTO [dbo].[Sty_Class]
	(
		[ClassID],
		[TrainingItemID],
		[ClassName],
		[ClassDesc],
		[StudentNum],
		[DutyUser],
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
		@ClassID,
		@TrainingItemID,
		@ClassName,
		@ClassDesc,
		@StudentNum,
		@DutyUser,
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
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_Class_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_Class_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Sty_Class_Update]
	@ClassID UniqueIdentifier,
	@TrainingItemID UniqueIdentifier,
	@ClassName NVarChar(200),
	@ClassDesc NVarChar(2048),
	@StudentNum Int,
	@DutyUser NVarChar(128),
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

	UPDATE [dbo].[Sty_Class] SET
		[TrainingItemID] = @TrainingItemID,
		[ClassName] = @ClassName,
		[ClassDesc] = @ClassDesc,
		[StudentNum] = @StudentNum,
		[DutyUser] = @DutyUser,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark,
		[DelFlag] = @DelFlag
	WHERE [ClassID] = @ClassID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_Class_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_Class_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Sty_Class_Delete]
	@ClassID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Sty_Class]
	WHERE [ClassID] = @ClassID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_Class_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_Class_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Sty_Class_GetByPk]
	@ClassID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ClassID],
		[TrainingItemID],
		[ClassName],
		[ClassDesc],
		[StudentNum],
		[DutyUser],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[DelFlag]
	FROM [dbo].[Sty_Class] 
	WHERE [ClassID] = @ClassID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_Class_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_Class_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Sty_Class_GetPagedList]
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
		[ClassID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ClassID] FROM [dbo].[Sty_Class]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ClassID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ClassID],
		biz.[TrainingItemID],
		biz.[ClassName],
		biz.[ClassDesc],
		biz.[StudentNum],
		biz.[DutyUser],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark],
		biz.[DelFlag]
	FROM [dbo].[Sty_Class] biz
	inner join #PageIndex p on  biz.[ClassID] = p.[ClassID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
