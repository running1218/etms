
------------------------------------------------------------------------------------------------------------------------
--Table Evaluation_Plate
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_Plate_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_Plate_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_Plate_Insert]
	@PlateID UniqueIdentifier,
	@ObjectTypeID Int,
	@PlateName NVarChar(200),
	@IsUse Int,
	@MaxRepeat Int,
	@IsViewResult Bit,
	@IsOther Bit,
	@OtherTitle NVarChar(200),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Evaluation_Plate]
	(
		[PlateID],
		[ObjectTypeID],
		[PlateName],
		[IsUse],
		[MaxRepeat],
		[IsViewResult],
		[IsOther],
		[OtherTitle],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	)
	VALUES
	(
		@PlateID,
		@ObjectTypeID,
		@PlateName,
		@IsUse,
		@MaxRepeat,
		@IsViewResult,
		@IsOther,
		@OtherTitle,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@Remark
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_Plate_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_Plate_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_Plate_Update]
	@PlateID UniqueIdentifier,
	@ObjectTypeID Int,
	@PlateName NVarChar(200),
	@IsUse Int,
	@MaxRepeat Int,
	@IsViewResult Bit,
	@IsOther Bit,
	@OtherTitle NVarChar(200),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Evaluation_Plate] SET
		[ObjectTypeID] = @ObjectTypeID,
		[PlateName] = @PlateName,
		[IsUse] = @IsUse,
		[MaxRepeat] = @MaxRepeat,
		[IsViewResult] = @IsViewResult,
		[IsOther] = @IsOther,
		[OtherTitle] = @OtherTitle,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark
	WHERE [PlateID] = @PlateID

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_Plate_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_Plate_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_Plate_Delete]
	@PlateID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Evaluation_Plate]
	WHERE [PlateID] = @PlateID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_Plate_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_Plate_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_Plate_GetByPk]
	@PlateID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[PlateID],
		[ObjectTypeID],
		[PlateName],
		[IsUse],
		[MaxRepeat],
		[IsViewResult],
		[IsOther],
		[OtherTitle],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	FROM [dbo].[Evaluation_Plate] 
	WHERE [PlateID] = @PlateID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_Plate_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_Plate_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Evaluation_Plate_GetPagedList]
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
		[PlateID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [PlateID] FROM [dbo].[Evaluation_Plate]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[PlateID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[PlateID],
		biz.[ObjectTypeID],
		biz.[PlateName],
		biz.[IsUse],
		biz.[MaxRepeat],
		biz.[IsViewResult],
		biz.[IsOther],
		biz.[OtherTitle],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark]
	FROM [dbo].[Evaluation_Plate] biz
	inner join #PageIndex p on  biz.[PlateID] = p.[PlateID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
