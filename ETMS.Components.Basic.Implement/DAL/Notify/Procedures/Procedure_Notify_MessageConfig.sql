
------------------------------------------------------------------------------------------------------------------------
--Table Notify_MessageConfig
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageConfig_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageConfig_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageConfig_Insert]
	@ConfigID Int OUTPUT,
	@MessageClassID SmallInt,
	@OrganizationID Int,
	@EmailSubjectTemplate NVarChar(200),
	@EmailBodyTemplate NVarChar(max),
	@IsEnableEmail Bit,
	@SMSSubjectTemplate NVarChar(100),
	@SMSBodyTemplate NVarChar(1000),
	@IsEnableSMS Bit,
	@SiteInfoSubjectTemplate NVarChar(200),
	@SiteInfoBodyTemplate NVarChar(max),
	@IsEnableSiteInfo Bit,
	@TemplateVariableDefine VarChar(5000),
	@Status SmallInt,
	@CreatorID Int,
	@CreateTime DateTime,
	@UpdaterID Int,
	@UpdateTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Notify_MessageConfig]
	(
		
		[MessageClassID],
		[OrganizationID],
		[EmailSubjectTemplate],
		[EmailBodyTemplate],
		[IsEnableEmail],
		[SMSSubjectTemplate],
		[SMSBodyTemplate],
		[IsEnableSMS],
		[SiteInfoSubjectTemplate],
		[SiteInfoBodyTemplate],
		[IsEnableSiteInfo],
		[TemplateVariableDefine],
		[Status],
		[CreatorID],
		[CreateTime],
		[UpdaterID],
		[UpdateTime]
	)
	VALUES
	(
		
		@MessageClassID,
		@OrganizationID,
		@EmailSubjectTemplate,
		@EmailBodyTemplate,
		@IsEnableEmail,
		@SMSSubjectTemplate,
		@SMSBodyTemplate,
		@IsEnableSMS,
		@SiteInfoSubjectTemplate,
		@SiteInfoBodyTemplate,
		@IsEnableSiteInfo,
		@TemplateVariableDefine,
		@Status,
		@CreatorID,
		@CreateTime,
		@UpdaterID,
		@UpdateTime
	)
	
	SET @ConfigID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageConfig_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageConfig_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageConfig_Update]
	@ConfigID Int,
	@MessageClassID SmallInt,
	@OrganizationID Int,
	@EmailSubjectTemplate NVarChar(200),
	@EmailBodyTemplate NVarChar(max),
	@IsEnableEmail Bit,
	@SMSSubjectTemplate NVarChar(100),
	@SMSBodyTemplate NVarChar(1000),
	@IsEnableSMS Bit,
	@SiteInfoSubjectTemplate NVarChar(200),
	@SiteInfoBodyTemplate NVarChar(max),
	@IsEnableSiteInfo Bit,
	@TemplateVariableDefine VarChar(5000),
	@Status SmallInt,
	@CreatorID Int,
	@CreateTime DateTime,
	@UpdaterID Int,
	@UpdateTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Notify_MessageConfig] SET
		[MessageClassID] = @MessageClassID,
		[OrganizationID] = @OrganizationID,
		[EmailSubjectTemplate] = @EmailSubjectTemplate,
		[EmailBodyTemplate] = @EmailBodyTemplate,
		[IsEnableEmail] = @IsEnableEmail,
		[SMSSubjectTemplate] = @SMSSubjectTemplate,
		[SMSBodyTemplate] = @SMSBodyTemplate,
		[IsEnableSMS] = @IsEnableSMS,
		[SiteInfoSubjectTemplate] = @SiteInfoSubjectTemplate,
		[SiteInfoBodyTemplate] = @SiteInfoBodyTemplate,
		[IsEnableSiteInfo] = @IsEnableSiteInfo,
		[TemplateVariableDefine] = @TemplateVariableDefine,
		[Status] = @Status,
		[CreatorID] = @CreatorID,
		[CreateTime] = @CreateTime,
		[UpdaterID] = @UpdaterID,
		[UpdateTime] = @UpdateTime
	WHERE [ConfigID] = @ConfigID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageConfig_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageConfig_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageConfig_Delete]
	@ConfigID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Notify_MessageConfig]
	WHERE [ConfigID] = @ConfigID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageConfig_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageConfig_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageConfig_GetByPk]
	@ConfigID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ConfigID],
		[MessageClassID],
		[OrganizationID],
		[EmailSubjectTemplate],
		[EmailBodyTemplate],
		[IsEnableEmail],
		[SMSSubjectTemplate],
		[SMSBodyTemplate],
		[IsEnableSMS],
		[SiteInfoSubjectTemplate],
		[SiteInfoBodyTemplate],
		[IsEnableSiteInfo],
		[TemplateVariableDefine],
		[Status],
		[CreatorID],
		[CreateTime],
		[UpdaterID],
		[UpdateTime]
	FROM [dbo].[Notify_MessageConfig] 
	WHERE [ConfigID] = @ConfigID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageConfig_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageConfig_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Notify_MessageConfig_GetPagedList]
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
		[ConfigID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ConfigID] FROM [dbo].[Notify_MessageConfig]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ConfigID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ConfigID],
		biz.[MessageClassID],
		biz.[OrganizationID],
		biz.[EmailSubjectTemplate],
		biz.[EmailBodyTemplate],
		biz.[IsEnableEmail],
		biz.[SMSSubjectTemplate],
		biz.[SMSBodyTemplate],
		biz.[IsEnableSMS],
		biz.[SiteInfoSubjectTemplate],
		biz.[SiteInfoBodyTemplate],
		biz.[IsEnableSiteInfo],
		biz.[TemplateVariableDefine],
		biz.[Status],
		biz.[CreatorID],
		biz.[CreateTime],
		biz.[UpdaterID],
		biz.[UpdateTime]
	FROM [dbo].[Notify_MessageConfig] biz
	inner join #PageIndex p on  biz.[ConfigID] = p.[ConfigID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
