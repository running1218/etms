
------------------------------------------------------------------------------------------------------------------------
--Table Log_BusinessOperate
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Log_BusinessOperate_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Log_BusinessOperate_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Log_BusinessOperate_Insert]
	@BizLogID BigInt OUTPUT,
	@ModuleName VarChar(100),
	@MethodName VarChar(50),
	@TargetID VarChar(50),
	@Action VarChar(max),
	@LoginName VarChar(50),
	@CreateTime DateTime,
	@ServerName VarChar(50),
	@ClientIP VarChar(20),
	@PageUrl VarChar(1024),
	@OrganizationID Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Log_BusinessOperate]
	(
		
		[ModuleName],
		[MethodName],
		[TargetID],
		[Action],
		[LoginName],
		[CreateTime],
		[ServerName],
		[ClientIP],
		[PageUrl],
		[OrganizationID]
	)
	VALUES
	(
		
		@ModuleName,
		@MethodName,
		@TargetID,
		@Action,
		@LoginName,
		@CreateTime,
		@ServerName,
		@ClientIP,
		@PageUrl,
		@OrganizationID
	)
	
	SET @BizLogID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Log_BusinessOperate_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Log_BusinessOperate_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Log_BusinessOperate_Update]
	@BizLogID BigInt,
	@ModuleName VarChar(100),
	@MethodName VarChar(50),
	@TargetID VarChar(50),
	@Action VarChar(max),
	@LoginName VarChar(50),
	@CreateTime DateTime,
	@ServerName VarChar(50),
	@ClientIP VarChar(20),
	@PageUrl VarChar(1024),
	@OrganizationID Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Log_BusinessOperate] SET
		[ModuleName] = @ModuleName,
		[MethodName] = @MethodName,
		[TargetID] = @TargetID,
		[Action] = @Action,
		[LoginName] = @LoginName,
		[CreateTime] = @CreateTime,
		[ServerName] = @ServerName,
		[ClientIP] = @ClientIP,
		[PageUrl] = @PageUrl,
		[OrganizationID] = @OrganizationID
	WHERE [BizLogID] = @BizLogID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Log_BusinessOperate_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Log_BusinessOperate_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Log_BusinessOperate_Delete]
	@BizLogID BigInt
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Log_BusinessOperate]
	WHERE [BizLogID] = @BizLogID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Log_BusinessOperate_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Log_BusinessOperate_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Log_BusinessOperate_GetByPk]
	@BizLogID BigInt
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[BizLogID],
		[ModuleName],
		[MethodName],
		[TargetID],
		[Action],
		[LoginName],
		[CreateTime],
		[ServerName],
		[ClientIP],
		[PageUrl],
		[OrganizationID]
	FROM [dbo].[Log_BusinessOperate] 
	WHERE [BizLogID] = @BizLogID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Log_BusinessOperate_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Log_BusinessOperate_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Log_BusinessOperate_GetPagedList]
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
		[BizLogID] BigInt
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [BizLogID] FROM [dbo].[Log_BusinessOperate]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[BizLogID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[BizLogID],
		biz.[ModuleName],
		biz.[MethodName],
		biz.[TargetID],
		biz.[Action],
		biz.[LoginName],
		biz.[CreateTime],
		biz.[ServerName],
		biz.[ClientIP],
		biz.[PageUrl],
		biz.[OrganizationID]
	FROM [dbo].[Log_BusinessOperate] biz
	inner join #PageIndex p on  biz.[BizLogID] = p.[BizLogID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
