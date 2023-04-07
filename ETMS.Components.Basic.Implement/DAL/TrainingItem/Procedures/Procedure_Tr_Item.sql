
------------------------------------------------------------------------------------------------------------------------
--Table Tr_Item
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_Item_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_Item_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_Item_Insert]
	@TrainingItemID UniqueIdentifier,
	@SpecialtyTypeCode NVarChar(100),
	@IsPlanItem Bit,
	@PlanID UniqueIdentifier,
	@OrgID Int,
	@TrainingLevelID Int,
	@DutyDeptID Int,
	@ItemCode NVarChar(100),
	@ItemName NVarChar(200),
	@ItemStatus Int,
	@ItemBeginTime DateTime,
	@ItemEndTime DateTime,
	@BudgetFee Decimal(15,2),
	@DutyUser NVarChar(128),
	@Mobile NVarChar(100),
	@EMAIL NVarChar(256),
	@ItemTarget NVarChar(Max),
	@ItemObjectStudent NVarChar(Max),
	@Remark NVarChar(Max),
	@AuditUser NVarChar(128),
	@AuditTime DateTime,
	@AuditOpinion NVarChar(Max),
	@IsIssue Bit,
	@IssueUser NVarChar(128),
	@IssueTime DateTime,
	@ItemEndModeID Int,
	@SignupModeID Int,
	@ItemEndReMark NVarChar(Max),
	@IsUse Int,
	@IsOrgItem Bit,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@DelFlag Bit,
	@SignupBeginTime DateTime,
	@SignupEndTime DateTime,
	@IsAllowSignup Bit,
	@IsIssuePoint Bit,
	@PointIssueUser NVarChar(128),
	@PointIssueTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Tr_Item]
	(
		[TrainingItemID],
		[SpecialtyTypeCode],
		[IsPlanItem],
		[PlanID],
		[OrgID],
		[TrainingLevelID],
		[DutyDeptID],
		[ItemCode],
		[ItemName],
		[ItemStatus],
		[ItemBeginTime],
		[ItemEndTime],
		[BudgetFee],
		[DutyUser],
		[Mobile],
		[EMAIL],
		[ItemTarget],
		[ItemObjectStudent],
		[Remark],
		[AuditUser],
		[AuditTime],
		[AuditOpinion],
		[IsIssue],
		[IssueUser],
		[IssueTime],
		[ItemEndModeID],
		[SignupModeID],
		[ItemEndReMark],
		[IsUse],
		[IsOrgItem],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[DelFlag],
		[SignupBeginTime],
		[SignupEndTime],
		[IsAllowSignup],
		[IsIssuePoint],
		[PointIssueUser],
		[PointIssueTime]
	)
	VALUES
	(
		@TrainingItemID,
		@SpecialtyTypeCode,
		@IsPlanItem,
		@PlanID,
		@OrgID,
		@TrainingLevelID,
		@DutyDeptID,
		@ItemCode,
		@ItemName,
		@ItemStatus,
		@ItemBeginTime,
		@ItemEndTime,
		@BudgetFee,
		@DutyUser,
		@Mobile,
		@EMAIL,
		@ItemTarget,
		@ItemObjectStudent,
		@Remark,
		@AuditUser,
		@AuditTime,
		@AuditOpinion,
		@IsIssue,
		@IssueUser,
		@IssueTime,
		@ItemEndModeID,
		@SignupModeID,
		@ItemEndReMark,
		@IsUse,
		@IsOrgItem,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@DelFlag,
		@SignupBeginTime,
		@SignupEndTime,
		@IsAllowSignup,
		@IsIssuePoint,
		@PointIssueUser,
		@PointIssueTime
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_Item_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_Item_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_Item_Update]
	@TrainingItemID UniqueIdentifier,
	@SpecialtyTypeCode NVarChar(100),
	@IsPlanItem Bit,
	@PlanID UniqueIdentifier,
	@OrgID Int,
	@TrainingLevelID Int,
	@DutyDeptID Int,
	@ItemCode NVarChar(100),
	@ItemName NVarChar(200),
	@ItemStatus Int,
	@ItemBeginTime DateTime,
	@ItemEndTime DateTime,
	@BudgetFee Decimal(15,2),
	@DutyUser NVarChar(128),
	@Mobile NVarChar(100),
	@EMAIL NVarChar(256),
	@ItemTarget NVarChar(Max),
	@ItemObjectStudent NVarChar(Max),
	@Remark NVarChar(Max),
	@AuditUser NVarChar(128),
	@AuditTime DateTime,
	@AuditOpinion NVarChar(Max),
	@IsIssue Bit,
	@IssueUser NVarChar(128),
	@IssueTime DateTime,
	@ItemEndModeID Int,
	@SignupModeID Int,
	@ItemEndReMark NVarChar(Max),
	@IsUse Int,
	@IsOrgItem Bit,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@DelFlag Bit,
	@SignupBeginTime DateTime,
	@SignupEndTime DateTime,
	@IsAllowSignup Bit,
	@IsIssuePoint Bit,
	@PointIssueUser NVarChar(128),
	@PointIssueTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Tr_Item] SET
		[SpecialtyTypeCode] = @SpecialtyTypeCode,
		[IsPlanItem] = @IsPlanItem,
		[PlanID] = @PlanID,
		[OrgID] = @OrgID,
		[TrainingLevelID] = @TrainingLevelID,
		[DutyDeptID] = @DutyDeptID,
		[ItemCode] = @ItemCode,
		[ItemName] = @ItemName,
		[ItemStatus] = @ItemStatus,
		[ItemBeginTime] = @ItemBeginTime,
		[ItemEndTime] = @ItemEndTime,
		[BudgetFee] = @BudgetFee,
		[DutyUser] = @DutyUser,
		[Mobile] = @Mobile,
		[EMAIL] = @EMAIL,
		[ItemTarget] = @ItemTarget,
		[ItemObjectStudent] = @ItemObjectStudent,
		[Remark] = @Remark,
		[AuditUser] = @AuditUser,
		[AuditTime] = @AuditTime,
		[AuditOpinion] = @AuditOpinion,
		[IsIssue] = @IsIssue,
		[IssueUser] = @IssueUser,
		[IssueTime] = @IssueTime,
		[ItemEndModeID] = @ItemEndModeID,
		[SignupModeID] = @SignupModeID,
		[ItemEndReMark] = @ItemEndReMark,
		[IsUse] = @IsUse,
		[IsOrgItem] = @IsOrgItem,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[DelFlag] = @DelFlag,
		[SignupBeginTime] = @SignupBeginTime,
		[SignupEndTime] = @SignupEndTime,
		[IsAllowSignup] = @IsAllowSignup,
		[IsIssuePoint] = @IsIssuePoint,
		[PointIssueUser] = @PointIssueUser,
		[PointIssueTime] = @PointIssueTime
	WHERE [TrainingItemID] = @TrainingItemID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_Item_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_Item_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_Item_Delete]
	@TrainingItemID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Tr_Item]
	WHERE [TrainingItemID] = @TrainingItemID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_Item_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_Item_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_Item_GetByPk]
	@TrainingItemID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[TrainingItemID],
		[SpecialtyTypeCode],
		[IsPlanItem],
		[PlanID],
		[OrgID],
		[TrainingLevelID],
		[DutyDeptID],
		[ItemCode],
		[ItemName],
		[ItemStatus],
		[ItemBeginTime],
		[ItemEndTime],
		[BudgetFee],
		[DutyUser],
		[Mobile],
		[EMAIL],
		[ItemTarget],
		[ItemObjectStudent],
		[Remark],
		[AuditUser],
		[AuditTime],
		[AuditOpinion],
		[IsIssue],
		[IssueUser],
		[IssueTime],
		[ItemEndModeID],
		[SignupModeID],
		[ItemEndReMark],
		[IsUse],
		[IsOrgItem],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[DelFlag],
		[SignupBeginTime],
		[SignupEndTime],
		[IsAllowSignup],
		[IsIssuePoint],
		[PointIssueUser],
		[PointIssueTime]
	FROM [dbo].[Tr_Item] 
	WHERE [TrainingItemID] = @TrainingItemID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_Item_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_Item_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Tr_Item_GetPagedList]
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
		[TrainingItemID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [TrainingItemID] FROM [dbo].[Tr_Item]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[TrainingItemID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[TrainingItemID],
		biz.[SpecialtyTypeCode],
		biz.[IsPlanItem],
		biz.[PlanID],
		biz.[OrgID],
		biz.[TrainingLevelID],
		biz.[DutyDeptID],
		biz.[ItemCode],
		biz.[ItemName],
		biz.[ItemStatus],
		biz.[ItemBeginTime],
		biz.[ItemEndTime],
		biz.[BudgetFee],
		biz.[DutyUser],
		biz.[Mobile],
		biz.[EMAIL],
		biz.[ItemTarget],
		biz.[ItemObjectStudent],
		biz.[Remark],
		biz.[AuditUser],
		biz.[AuditTime],
		biz.[AuditOpinion],
		biz.[IsIssue],
		biz.[IssueUser],
		biz.[IssueTime],
		biz.[ItemEndModeID],
		biz.[SignupModeID],
		biz.[ItemEndReMark],
		biz.[IsUse],
		biz.[IsOrgItem],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[DelFlag],
		biz.[SignupBeginTime],
		biz.[SignupEndTime],
		biz.[IsAllowSignup],
		biz.[IsIssuePoint],
		biz.[PointIssueUser],
		biz.[PointIssueTime]
	FROM [dbo].[Tr_Item] biz
	inner join #PageIndex p on  biz.[TrainingItemID] = p.[TrainingItemID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
