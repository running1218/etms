
------------------------------------------------------------------------------------------------------------------------
--Table Tr_OuterOrg
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_OuterOrg_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_OuterOrg_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_OuterOrg_Insert]
	@OuterOrgID UniqueIdentifier,
	@OuterOrgCode NVarChar(100),
	@OuterOrgName NVarChar(200),
	@OuterOrgStatus Int,
	@LinkMan NVarChar(100),
	@LinkMode NVarChar(100),
	@EMAIL NVarChar(256),
	@CommonPlace NVarChar(200),
	@OrgAssess NVarChar(max),
	@HistoryCooperation NVarChar(max),
	@ContractModal NVarChar(max),
	@ServiceContent NVarChar(max),
	@BestCourse NVarChar(max),
	@ContractURL NVarChar(256),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(max)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Tr_OuterOrg]
	(
		[OuterOrgID],
		[OuterOrgCode],
		[OuterOrgName],
		[OuterOrgStatus],
		[LinkMan],
		[LinkMode],
		[EMAIL],
		[CommonPlace],
		[OrgAssess],
		[HistoryCooperation],
		[ContractModal],
		[ServiceContent],
		[BestCourse],
		[ContractURL],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	)
	VALUES
	(
		@OuterOrgID,
		@OuterOrgCode,
		@OuterOrgName,
		@OuterOrgStatus,
		@LinkMan,
		@LinkMode,
		@EMAIL,
		@CommonPlace,
		@OrgAssess,
		@HistoryCooperation,
		@ContractModal,
		@ServiceContent,
		@BestCourse,
		@ContractURL,
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

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_OuterOrg_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_OuterOrg_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_OuterOrg_Update]
	@OuterOrgID UniqueIdentifier,
	@OuterOrgCode NVarChar(100),
	@OuterOrgName NVarChar(200),
	@OuterOrgStatus Int,
	@LinkMan NVarChar(100),
	@LinkMode NVarChar(100),
	@EMAIL NVarChar(256),
	@CommonPlace NVarChar(200),
	@OrgAssess NVarChar(max),
	@HistoryCooperation NVarChar(max),
	@ContractModal NVarChar(max),
	@ServiceContent NVarChar(max),
	@BestCourse NVarChar(max),
	@ContractURL NVarChar(256),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(max)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Tr_OuterOrg] SET
		[OuterOrgCode] = @OuterOrgCode,
		[OuterOrgName] = @OuterOrgName,
		[OuterOrgStatus] = @OuterOrgStatus,
		[LinkMan] = @LinkMan,
		[LinkMode] = @LinkMode,
		[EMAIL] = @EMAIL,
		[CommonPlace] = @CommonPlace,
		[OrgAssess] = @OrgAssess,
		[HistoryCooperation] = @HistoryCooperation,
		[ContractModal] = @ContractModal,
		[ServiceContent] = @ServiceContent,
		[BestCourse] = @BestCourse,
		[ContractURL] = @ContractURL,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark
	WHERE [OuterOrgID] = @OuterOrgID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_OuterOrg_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_OuterOrg_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_OuterOrg_Delete]
	@OuterOrgID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Tr_OuterOrg]
	WHERE [OuterOrgID] = @OuterOrgID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_OuterOrg_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_OuterOrg_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_OuterOrg_GetByPk]
	@OuterOrgID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[OuterOrgID],
		[OuterOrgCode],
		[OuterOrgName],
		[OuterOrgStatus],
		[LinkMan],
		[LinkMode],
		[EMAIL],
		[CommonPlace],
		[OrgAssess],
		[HistoryCooperation],
		[ContractModal],
		[ServiceContent],
		[BestCourse],
		[ContractURL],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	FROM [dbo].[Tr_OuterOrg] 
	WHERE [OuterOrgID] = @OuterOrgID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_OuterOrg_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_OuterOrg_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Tr_OuterOrg_GetPagedList]
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
		[OuterOrgID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [OuterOrgID] FROM [dbo].[Tr_OuterOrg]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[OuterOrgID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[OuterOrgID],
		biz.[OuterOrgCode],
		biz.[OuterOrgName],
		biz.[OuterOrgStatus],
		biz.[LinkMan],
		biz.[LinkMode],
		biz.[EMAIL],
		biz.[CommonPlace],
		biz.[OrgAssess],
		biz.[HistoryCooperation],
		biz.[ContractModal],
		biz.[ServiceContent],
		biz.[BestCourse],
		biz.[ContractURL],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark]
	FROM [dbo].[Tr_OuterOrg] biz
	inner join #PageIndex p on  biz.[OuterOrgID] = p.[OuterOrgID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
