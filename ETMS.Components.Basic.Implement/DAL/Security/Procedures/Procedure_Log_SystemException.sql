
------------------------------------------------------------------------------------------------------------------------
--Table Log_SystemException
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Log_SystemException_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Log_SystemException_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Log_SystemException_Insert]
	@SysExLogID BigInt OUTPUT,
	@ApplicationName VarChar(50),
	@Message VarChar(500),
	@BaseMessage VarChar(500),
	@StackTrace VarChar(max),
	@LoginName VarChar(50),
	@CreateTime DateTime,
	@ServerName VarChar(50),
	@ClientIP VarChar(20),
	@PageUrl VarChar(1024)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Log_SystemException]
	(
		
		[ApplicationName],
		[Message],
		[BaseMessage],
		[StackTrace],
		[LoginName],
		[CreateTime],
		[ServerName],
		[ClientIP],
		[PageUrl]
	)
	VALUES
	(
		
		@ApplicationName,
		@Message,
		@BaseMessage,
		@StackTrace,
		@LoginName,
		@CreateTime,
		@ServerName,
		@ClientIP,
		@PageUrl
	)
	
	SET @SysExLogID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Log_SystemException_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Log_SystemException_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Log_SystemException_Update]
	@SysExLogID BigInt,
	@ApplicationName VarChar(50),
	@Message VarChar(500),
	@BaseMessage VarChar(500),
	@StackTrace VarChar(max),
	@LoginName VarChar(50),
	@CreateTime DateTime,
	@ServerName VarChar(50),
	@ClientIP VarChar(20),
	@PageUrl VarChar(1024)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Log_SystemException] SET
		[ApplicationName] = @ApplicationName,
		[Message] = @Message,
		[BaseMessage] = @BaseMessage,
		[StackTrace] = @StackTrace,
		[LoginName] = @LoginName,
		[CreateTime] = @CreateTime,
		[ServerName] = @ServerName,
		[ClientIP] = @ClientIP,
		[PageUrl] = @PageUrl
	WHERE [SysExLogID] = @SysExLogID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Log_SystemException_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Log_SystemException_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Log_SystemException_Delete]
	@SysExLogID BigInt
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Log_SystemException]
	WHERE [SysExLogID] = @SysExLogID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Log_SystemException_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Log_SystemException_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Log_SystemException_GetByPk]
	@SysExLogID BigInt
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[SysExLogID],
		[ApplicationName],
		[Message],
		[BaseMessage],
		[StackTrace],
		[LoginName],
		[CreateTime],
		[ServerName],
		[ClientIP],
		[PageUrl]
	FROM [dbo].[Log_SystemException] 
	WHERE [SysExLogID] = @SysExLogID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Log_SystemException_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Log_SystemException_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Log_SystemException_GetPagedList]
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
		[SysExLogID] BigInt
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [SysExLogID] FROM [dbo].[Log_SystemException]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[SysExLogID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[SysExLogID],
		biz.[ApplicationName],
		biz.[Message],
		biz.[BaseMessage],
		biz.[StackTrace],
		biz.[LoginName],
		biz.[CreateTime],
		biz.[ServerName],
		biz.[ClientIP],
		biz.[PageUrl]
	FROM [dbo].[Log_SystemException] biz
	inner join #PageIndex p on  biz.[SysExLogID] = p.[SysExLogID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
