
------------------------------------------------------------------------------------------------------------------------
--Table Fee_FeeCostDetails
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Fee_FeeCostDetails_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Fee_FeeCostDetails_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Fee_FeeCostDetails_Insert]
	@FeeCostDetailID UniqueIdentifier,
	@TrainingItemID UniqueIdentifier,
	@FeeCostDetailNo NVarChar(100),
	@FeeCostDetailName NVarChar(200),
	@CostDate DateTime,
	@Amount Decimal(10,2),
	@Purpose NVarChar(200),
	@PRNo NVarChar(200),
	@IsGetInvoice Bit,
	@ReimbursementDate DateTime,
	@Handler NVarChar(128),
	@Remark NVarChar(-1),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@DelFlag Bit
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Fee_FeeCostDetails]
	(
		[FeeCostDetailID],
		[TrainingItemID],
		[FeeCostDetailNo],
		[FeeCostDetailName],
		[CostDate],
		[Amount],
		[Purpose],
		[PRNo],
		[IsGetInvoice],
		[ReimbursementDate],
		[Handler],
		[Remark],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[DelFlag]
	)
	VALUES
	(
		@FeeCostDetailID,
		@TrainingItemID,
		@FeeCostDetailNo,
		@FeeCostDetailName,
		@CostDate,
		@Amount,
		@Purpose,
		@PRNo,
		@IsGetInvoice,
		@ReimbursementDate,
		@Handler,
		@Remark,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@DelFlag
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Fee_FeeCostDetails_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Fee_FeeCostDetails_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Fee_FeeCostDetails_Update]
	@FeeCostDetailID UniqueIdentifier,
	@TrainingItemID UniqueIdentifier,
	@FeeCostDetailNo NVarChar(100),
	@FeeCostDetailName NVarChar(200),
	@CostDate DateTime,
	@Amount Decimal(10,2),
	@Purpose NVarChar(200),
	@PRNo NVarChar(200),
	@IsGetInvoice Bit,
	@ReimbursementDate DateTime,
	@Handler NVarChar(128),
	@Remark NVarChar(-1),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@DelFlag Bit
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Fee_FeeCostDetails] SET
		[TrainingItemID] = @TrainingItemID,
		[FeeCostDetailNo] = @FeeCostDetailNo,
		[FeeCostDetailName] = @FeeCostDetailName,
		[CostDate] = @CostDate,
		[Amount] = @Amount,
		[Purpose] = @Purpose,
		[PRNo] = @PRNo,
		[IsGetInvoice] = @IsGetInvoice,
		[ReimbursementDate] = @ReimbursementDate,
		[Handler] = @Handler,
		[Remark] = @Remark,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[DelFlag] = @DelFlag
	WHERE [FeeCostDetailID] = @FeeCostDetailID

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Fee_FeeCostDetails_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Fee_FeeCostDetails_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Fee_FeeCostDetails_Delete]
	@FeeCostDetailID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Fee_FeeCostDetails]
	WHERE [FeeCostDetailID] = @FeeCostDetailID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Fee_FeeCostDetails_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Fee_FeeCostDetails_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Fee_FeeCostDetails_GetByPk]
	@FeeCostDetailID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[FeeCostDetailID],
		[TrainingItemID],
		[FeeCostDetailNo],
		[FeeCostDetailName],
		[CostDate],
		[Amount],
		[Purpose],
		[PRNo],
		[IsGetInvoice],
		[ReimbursementDate],
		[Handler],
		[Remark],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[DelFlag]
	FROM [dbo].[Fee_FeeCostDetails] 
	WHERE [FeeCostDetailID] = @FeeCostDetailID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Fee_FeeCostDetails_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Fee_FeeCostDetails_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Fee_FeeCostDetails_GetPagedList]
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
		[FeeCostDetailID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [FeeCostDetailID] FROM [dbo].[Fee_FeeCostDetails]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[FeeCostDetailID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[FeeCostDetailID],
		biz.[TrainingItemID],
		biz.[FeeCostDetailNo],
		biz.[FeeCostDetailName],
		biz.[CostDate],
		biz.[Amount],
		biz.[Purpose],
		biz.[PRNo],
		biz.[IsGetInvoice],
		biz.[ReimbursementDate],
		biz.[Handler],
		biz.[Remark],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[DelFlag]
	FROM [dbo].[Fee_FeeCostDetails] biz
	inner join #PageIndex p on  biz.[FeeCostDetailID] = p.[FeeCostDetailID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
