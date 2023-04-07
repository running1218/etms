
------------------------------------------------------------------------------------------------------------------------
--Table IDP_PlanObject
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObject_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObject_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanObject_Insert]
	@IDPPlanObjectID UniqueIdentifier,
	@IDP_PlanID UniqueIdentifier,
	@PlanDevelopment NVarChar(-1),
	@Ability NVarChar(-1),
	@HopeLevel NVarChar(-1),
	@SuperiorOpinion NVarChar(-1),
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@CreateUserID Int,
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1),
	@IsContinue Bit
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[IDP_PlanObject]
	(
		[IDPPlanObjectID],
		[IDP_PlanID],
		[PlanDevelopment],
		[Ability],
		[HopeLevel],
		[SuperiorOpinion],
		[CreateTime],
		[CreateUser],
		[CreateUserID],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[IsContinue]
	)
	VALUES
	(
		@IDPPlanObjectID,
		@IDP_PlanID,
		@PlanDevelopment,
		@Ability,
		@HopeLevel,
		@SuperiorOpinion,
		@CreateTime,
		@CreateUser,
		@CreateUserID,
		@ModifyTime,
		@ModifyUser,
		@Remark,
		@IsContinue
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObject_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObject_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanObject_Update]
	@IDPPlanObjectID UniqueIdentifier,
	@IDP_PlanID UniqueIdentifier,
	@PlanDevelopment NVarChar(-1),
	@Ability NVarChar(-1),
	@HopeLevel NVarChar(-1),
	@SuperiorOpinion NVarChar(-1),
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@CreateUserID Int,
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1),
	@IsContinue Bit
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[IDP_PlanObject] SET
		[IDP_PlanID] = @IDP_PlanID,
		[PlanDevelopment] = @PlanDevelopment,
		[Ability] = @Ability,
		[HopeLevel] = @HopeLevel,
		[SuperiorOpinion] = @SuperiorOpinion,
		[CreateTime] = @CreateTime,
		[CreateUser] = @CreateUser,
		[CreateUserID] = @CreateUserID,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark,
		[IsContinue] = @IsContinue
	WHERE [IDPPlanObjectID] = @IDPPlanObjectID

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObject_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObject_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanObject_Delete]
	@IDPPlanObjectID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[IDP_PlanObject]
	WHERE [IDPPlanObjectID] = @IDPPlanObjectID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObject_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObject_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_PlanObject_GetByPk]
	@IDPPlanObjectID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[IDPPlanObjectID],
		[IDP_PlanID],
		[PlanDevelopment],
		[Ability],
		[HopeLevel],
		[SuperiorOpinion],
		[CreateTime],
		[CreateUser],
		[CreateUserID],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[IsContinue]
	FROM [dbo].[IDP_PlanObject] 
	WHERE [IDPPlanObjectID] = @IDPPlanObjectID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_PlanObject_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_PlanObject_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_IDP_PlanObject_GetPagedList]
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
		[IDPPlanObjectID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [IDPPlanObjectID] FROM [dbo].[IDP_PlanObject]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[IDPPlanObjectID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[IDPPlanObjectID],
		biz.[IDP_PlanID],
		biz.[PlanDevelopment],
		biz.[Ability],
		biz.[HopeLevel],
		biz.[SuperiorOpinion],
		biz.[CreateTime],
		biz.[CreateUser],
		biz.[CreateUserID],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark],
		biz.[IsContinue]
	FROM [dbo].[IDP_PlanObject] biz
	inner join #PageIndex p on  biz.[IDPPlanObjectID] = p.[IDPPlanObjectID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
