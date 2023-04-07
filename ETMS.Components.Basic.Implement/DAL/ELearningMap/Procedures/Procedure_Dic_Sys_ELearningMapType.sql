
------------------------------------------------------------------------------------------------------------------------
--Table Dic_Sys_ELearningMapType
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Dic_Sys_ELearningMapType_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Dic_Sys_ELearningMapType_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Dic_Sys_ELearningMapType_Insert]
	@ELearningMapTypeID Int,
	@ELearningMapTypeName NVarChar(200),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Dic_Sys_ELearningMapType]
	(
		[ELearningMapTypeID],
		[ELearningMapTypeName],
		[OrderNum],
		[IsUse]
	)
	VALUES
	(
		@ELearningMapTypeID,
		@ELearningMapTypeName,
		@OrderNum,
		@IsUse
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Dic_Sys_ELearningMapType_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Dic_Sys_ELearningMapType_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Dic_Sys_ELearningMapType_Update]
	@ELearningMapTypeID Int,
	@ELearningMapTypeName NVarChar(200),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Dic_Sys_ELearningMapType] SET
		[ELearningMapTypeName] = @ELearningMapTypeName,
		[OrderNum] = @OrderNum,
		[IsUse] = @IsUse
	WHERE [ELearningMapTypeID] = @ELearningMapTypeID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Dic_Sys_ELearningMapType_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Dic_Sys_ELearningMapType_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Dic_Sys_ELearningMapType_Delete]
	@ELearningMapTypeID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Dic_Sys_ELearningMapType]
	WHERE [ELearningMapTypeID] = @ELearningMapTypeID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Dic_Sys_ELearningMapType_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Dic_Sys_ELearningMapType_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Dic_Sys_ELearningMapType_GetByPk]
	@ELearningMapTypeID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ELearningMapTypeID],
		[ELearningMapTypeName],
		[OrderNum],
		[IsUse]
	FROM [dbo].[Dic_Sys_ELearningMapType] 
	WHERE [ELearningMapTypeID] = @ELearningMapTypeID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Dic_Sys_ELearningMapType_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Dic_Sys_ELearningMapType_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Dic_Sys_ELearningMapType_GetPagedList]
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
		[ELearningMapTypeID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ELearningMapTypeID] FROM [dbo].[Dic_Sys_ELearningMapType]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ELearningMapTypeID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ELearningMapTypeID],
		biz.[ELearningMapTypeName],
		biz.[OrderNum],
		biz.[IsUse]
	FROM [dbo].[Dic_Sys_ELearningMapType] biz
	inner join #PageIndex p on  biz.[ELearningMapTypeID] = p.[ELearningMapTypeID] AND
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
