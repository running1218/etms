
------------------------------------------------------------------------------------------------------------------------
--Table Tr_Plan
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_Plan_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_Plan_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_Plan_Insert]
	@PlanID UniqueIdentifier,
	@OrgID Int,
	@TrainingLevelID Int,
	@PlanTypeID Int,
	@SpecialtyTypeCode NVarChar(100),
	@PlanCode NVarChar(100),
	@PlanName NVarChar(200),
	@IsUse Int,
	@PlanBeginTime DateTime,
	@PlanEndTime DateTime,
	@DutyUser NVarChar(128),
	@Mobile NVarChar(100),
	@EMAIL NVarChar(256),
	@BudgetFee Decimal(15,2),
	@StudentNum Int,
	@PlanTarget NVarChar(Max),
	@PlanObjectStudent NVarChar(Max),
	@Remark NVarChar(Max),
	@DutyDeptID Int,
	@PlanStatus Int,
	@AuditUser NVarChar(128),
	@AuditTime DateTime,
	@AuditOpinion NVarChar(Max),
	@PlanEndRemark NVarChar(Max),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@DelFlag Bit,
	@PlanEndModeID Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Tr_Plan]
	(
		[PlanID],
		[OrgID],
		[TrainingLevelID],
		[PlanTypeID],
		[SpecialtyTypeCode],
		[PlanCode],
		[PlanName],
		[IsUse],
		[PlanBeginTime],
		[PlanEndTime],
		[DutyUser],
		[Mobile],
		[EMAIL],
		[BudgetFee],
		[StudentNum],
		[PlanTarget],
		[PlanObjectStudent],
		[Remark],
		[DutyDeptID],
		[PlanStatus],
		[AuditUser],
		[AuditTime],
		[AuditOpinion],
		[PlanEndRemark],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[DelFlag],
		[PlanEndModeID]
	)
	VALUES
	(
		@PlanID,
		@OrgID,
		@TrainingLevelID,
		@PlanTypeID,
		@SpecialtyTypeCode,
		@PlanCode,
		@PlanName,
		@IsUse,
		@PlanBeginTime,
		@PlanEndTime,
		@DutyUser,
		@Mobile,
		@EMAIL,
		@BudgetFee,
		@StudentNum,
		@PlanTarget,
		@PlanObjectStudent,
		@Remark,
		@DutyDeptID,
		@PlanStatus,
		@AuditUser,
		@AuditTime,
		@AuditOpinion,
		@PlanEndRemark,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@DelFlag,
		@PlanEndModeID
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_Plan_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_Plan_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_Plan_Update]
	@PlanID UniqueIdentifier,
	@OrgID Int,
	@TrainingLevelID Int,
	@PlanTypeID Int,
	@SpecialtyTypeCode NVarChar(100),
	@PlanCode NVarChar(100),
	@PlanName NVarChar(200),
	@IsUse Int,
	@PlanBeginTime DateTime,
	@PlanEndTime DateTime,
	@DutyUser NVarChar(128),
	@Mobile NVarChar(100),
	@EMAIL NVarChar(256),
	@BudgetFee Decimal(15,2),
	@StudentNum Int,
	@PlanTarget NVarChar(Max),
	@PlanObjectStudent NVarChar(Max),
	@Remark NVarChar(Max),
	@DutyDeptID Int,
	@PlanStatus Int,
	@AuditUser NVarChar(128),
	@AuditTime DateTime,
	@AuditOpinion NVarChar(Max),
	@PlanEndRemark NVarChar(Max),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@DelFlag Bit,
	@PlanEndModeID Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Tr_Plan] SET
		[OrgID] = @OrgID,
		[TrainingLevelID] = @TrainingLevelID,
		[PlanTypeID] = @PlanTypeID,
		[SpecialtyTypeCode] = @SpecialtyTypeCode,
		[PlanCode] = @PlanCode,
		[PlanName] = @PlanName,
		[IsUse] = @IsUse,
		[PlanBeginTime] = @PlanBeginTime,
		[PlanEndTime] = @PlanEndTime,
		[DutyUser] = @DutyUser,
		[Mobile] = @Mobile,
		[EMAIL] = @EMAIL,
		[BudgetFee] = @BudgetFee,
		[StudentNum] = @StudentNum,
		[PlanTarget] = @PlanTarget,
		[PlanObjectStudent] = @PlanObjectStudent,
		[Remark] = @Remark,
		[DutyDeptID] = @DutyDeptID,
		[PlanStatus] = @PlanStatus,
		[AuditUser] = @AuditUser,
		[AuditTime] = @AuditTime,
		[AuditOpinion] = @AuditOpinion,
		[PlanEndRemark] = @PlanEndRemark,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[DelFlag] = @DelFlag,
		[PlanEndModeID] = @PlanEndModeID
	WHERE [PlanID] = @PlanID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_Plan_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_Plan_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_Plan_Delete]
	@PlanID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Tr_Plan]
	WHERE [PlanID] = @PlanID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_Plan_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_Plan_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_Plan_GetByPk]
	@PlanID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[PlanID],
		[OrgID],
		[TrainingLevelID],
		[PlanTypeID],
		[SpecialtyTypeCode],
		[PlanCode],
		[PlanName],
		[IsUse],
		[PlanBeginTime],
		[PlanEndTime],
		[DutyUser],
		[Mobile],
		[EMAIL],
		[BudgetFee],
		[StudentNum],
		[PlanTarget],
		[PlanObjectStudent],
		[Remark],
		[DutyDeptID],
		[PlanStatus],
		[AuditUser],
		[AuditTime],
		[AuditOpinion],
		[PlanEndRemark],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[DelFlag],
		[PlanEndModeID]
	FROM [dbo].[Tr_Plan] 
	WHERE [PlanID] = @PlanID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_Plan_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_Plan_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Tr_Plan_GetPagedList]
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
		[PlanID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [PlanID] FROM [dbo].[Tr_Plan]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[PlanID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[PlanID],
		biz.[OrgID],
		biz.[TrainingLevelID],
		biz.[PlanTypeID],
		biz.[SpecialtyTypeCode],
		biz.[PlanCode],
		biz.[PlanName],
		biz.[IsUse],
		biz.[PlanBeginTime],
		biz.[PlanEndTime],
		biz.[DutyUser],
		biz.[Mobile],
		biz.[EMAIL],
		biz.[BudgetFee],
		biz.[StudentNum],
		biz.[PlanTarget],
		biz.[PlanObjectStudent],
		biz.[Remark],
		biz.[DutyDeptID],
		biz.[PlanStatus],
		biz.[AuditUser],
		biz.[AuditTime],
		biz.[AuditOpinion],
		biz.[PlanEndRemark],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[DelFlag],
		biz.[PlanEndModeID]
	FROM [dbo].[Tr_Plan] biz
	inner join #PageIndex p on  biz.[PlanID] = p.[PlanID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
