
------------------------------------------------------------------------------------------------------------------------
--Table Tr_PlanCourse
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_PlanCourse_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_PlanCourse_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_PlanCourse_Insert]
	@PlanCourseID UniqueIdentifier,
	@PlanID UniqueIdentifier,
	@CourseID UniqueIdentifier,
	@OuterOrgID UniqueIdentifier,
	@CourseAttrID Int,
	@TrainingModelID Int,
	@TeachModelID Int,
	@OrgID Int,
	@BudgetFee Decimal(15,2),
	@CourseStatus Bit,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(Max),
	@DelFlag Bit
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Tr_PlanCourse]
	(
		[PlanCourseID],
		[PlanID],
		[CourseID],
		[OuterOrgID],
		[CourseAttrID],
		[TrainingModelID],
		[TeachModelID],
		[OrgID],
		[BudgetFee],
		[CourseStatus],
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
		@PlanCourseID,
		@PlanID,
		@CourseID,
		@OuterOrgID,
		@CourseAttrID,
		@TrainingModelID,
		@TeachModelID,
		@OrgID,
		@BudgetFee,
		@CourseStatus,
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

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_PlanCourse_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_PlanCourse_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_PlanCourse_Update]
	@PlanCourseID UniqueIdentifier,
	@PlanID UniqueIdentifier,
	@CourseID UniqueIdentifier,
	@OuterOrgID UniqueIdentifier,
	@CourseAttrID Int,
	@TrainingModelID Int,
	@TeachModelID Int,
	@OrgID Int,
	@BudgetFee Decimal(15,2),
	@CourseStatus Bit,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(Max),
	@DelFlag Bit
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Tr_PlanCourse] SET
		[PlanID] = @PlanID,
		[CourseID] = @CourseID,
		[OuterOrgID] = @OuterOrgID,
		[CourseAttrID] = @CourseAttrID,
		[TrainingModelID] = @TrainingModelID,
		[TeachModelID] = @TeachModelID,
		[OrgID] = @OrgID,
		[BudgetFee] = @BudgetFee,
		[CourseStatus] = @CourseStatus,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark,
		[DelFlag] = @DelFlag
	WHERE [PlanCourseID] = @PlanCourseID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_PlanCourse_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_PlanCourse_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_PlanCourse_Delete]
	@PlanCourseID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Tr_PlanCourse]
	WHERE [PlanCourseID] = @PlanCourseID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_PlanCourse_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_PlanCourse_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_PlanCourse_GetByPk]
	@PlanCourseID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[PlanCourseID],
		[PlanID],
		[CourseID],
		[OuterOrgID],
		[CourseAttrID],
		[TrainingModelID],
		[TeachModelID],
		[OrgID],
		[BudgetFee],
		[CourseStatus],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[DelFlag]
	FROM [dbo].[Tr_PlanCourse] 
	WHERE [PlanCourseID] = @PlanCourseID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_PlanCourse_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_PlanCourse_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Tr_PlanCourse_GetPagedList]
	@PageIndex int, --ҳ��
	@PageSize int, --ҳ���С	
	@SortExpression varchar(1000) = '', --�����ֶ�
	@Criteria varchar(1000) = '' --��AND��ͷ�Ĳ�ѯ����
AS
BEGIN
	--@Criteria�����������ʵ�ʲ�ѯ��Ҫ��������ɾ
	--@SortExpression�����������ʵ�ʲ�ѯ��Ҫ����Ĭ��ֵ�趨

	SET NOCOUNT ON
	
	DECLARE @SqlGet varchar(1600)
	DECLARE @TotalRecords int
	
	--������������洢��������
	CREATE TABLE #PageIndex
	(
		[IndexId] int IDENTITY (1, 1) NOT NULL,
		[PlanCourseID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [PlanCourseID] FROM [dbo].[Tr_PlanCourse]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[PlanCourseID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[PlanCourseID],
		biz.[PlanID],
		biz.[CourseID],
		biz.[OuterOrgID],
		biz.[CourseAttrID],
		biz.[TrainingModelID],
		biz.[TeachModelID],
		biz.[OrgID],
		biz.[BudgetFee],
		biz.[CourseStatus],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark],
		biz.[DelFlag]
	FROM [dbo].[Tr_PlanCourse] biz
	inner join #PageIndex p on  biz.[PlanCourseID] = p.[PlanCourseID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
