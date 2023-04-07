
------------------------------------------------------------------------------------------------------------------------
--Table Inf_dic_InfoLevel
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_dic_InfoLevel_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_dic_InfoLevel_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_dic_InfoLevel_Insert]
	@InfoLevelID Int,
	@InfoLevelName VarChar(50),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Inf_dic_InfoLevel]
	(
		[InfoLevelID],
		[InfoLevelName],
		[OrderNum],
		[IsUse]
	)
	VALUES
	(
		@InfoLevelID,
		@InfoLevelName,
		@OrderNum,
		@IsUse
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_dic_InfoLevel_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_dic_InfoLevel_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_dic_InfoLevel_Update]
	@InfoLevelID Int,
	@InfoLevelName VarChar(50),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Inf_dic_InfoLevel] SET
		[InfoLevelName] = @InfoLevelName,
		[OrderNum] = @OrderNum,
		[IsUse] = @IsUse
	WHERE [InfoLevelID] = @InfoLevelID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_dic_InfoLevel_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_dic_InfoLevel_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_dic_InfoLevel_Delete]
	@InfoLevelID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Inf_dic_InfoLevel]
	WHERE [InfoLevelID] = @InfoLevelID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_dic_InfoLevel_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_dic_InfoLevel_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_dic_InfoLevel_GetByPk]
	@InfoLevelID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[InfoLevelID],
		[InfoLevelName],
		[OrderNum],
		[IsUse]
	FROM [dbo].[Inf_dic_InfoLevel] 
	WHERE [InfoLevelID] = @InfoLevelID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_dic_InfoLevel_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_dic_InfoLevel_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Inf_dic_InfoLevel_GetPagedList]
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
		[InfoLevelID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [InfoLevelID] FROM [dbo].[Inf_dic_InfoLevel]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[InfoLevelID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[InfoLevelID],
		biz.[InfoLevelName],
		biz.[OrderNum],
		biz.[IsUse]
	FROM [dbo].[Inf_dic_InfoLevel] biz
	inner join #PageIndex p on biz.[[~#ColumnName#~]] = p.[[~#ColumnName#~]] AND[~#EndLoopPK#~]
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
