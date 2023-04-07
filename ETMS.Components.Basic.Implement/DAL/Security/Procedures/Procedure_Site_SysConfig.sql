
------------------------------------------------------------------------------------------------------------------------
--Table Site_SysConfig
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfig_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfig_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_SysConfig_Insert]
	@ConfigID Int OUTPUT,
	@OrganizationID Int,
	@Name VarChar(50),
	@ConfigGroupID Int,
	@DisplayName VarChar(100),
	@DefaultValue VarChar(1000),
	@OrderNo Int,
	@UserValue VarChar(1000),
	@Modifier VarChar(50),
	@ModifyTime DateTime,
	@Description VarChar(500)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Site_SysConfig]
	(
		
		[OrganizationID],
		[Name],
		[ConfigGroupID],
		[DisplayName],
		[DefaultValue],
		[OrderNo],
		[UserValue],
		[Modifier],
		[ModifyTime],
		[Description]
	)
	VALUES
	(
		
		@OrganizationID,
		@Name,
		@ConfigGroupID,
		@DisplayName,
		@DefaultValue,
		@OrderNo,
		@UserValue,
		@Modifier,
		@ModifyTime,
		@Description
	)
	
	SET @ConfigID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfig_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfig_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_SysConfig_Update]
	@ConfigID Int,
	@OrganizationID Int,
	@Name VarChar(50),
	@ConfigGroupID Int,
	@DisplayName VarChar(100),
	@DefaultValue VarChar(1000),
	@OrderNo Int,
	@UserValue VarChar(1000),
	@Modifier VarChar(50),
	@ModifyTime DateTime,
	@Description VarChar(500)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Site_SysConfig] SET
		[OrganizationID] = @OrganizationID,
		[Name] = @Name,
		[ConfigGroupID] = @ConfigGroupID,
		[DisplayName] = @DisplayName,
		[DefaultValue] = @DefaultValue,
		[OrderNo] = @OrderNo,
		[UserValue] = @UserValue,
		[Modifier] = @Modifier,
		[ModifyTime] = @ModifyTime,
		[Description] = @Description
	WHERE [ConfigID] = @ConfigID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfig_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfig_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_SysConfig_Delete]
	@ConfigID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Site_SysConfig]
	WHERE [ConfigID] = @ConfigID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfig_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfig_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_SysConfig_GetByPk]
	@ConfigID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ConfigID],
		[OrganizationID],
		[Name],
		[ConfigGroupID],
		[DisplayName],
		[DefaultValue],
		[OrderNo],
		[UserValue],
		[Modifier],
		[ModifyTime],
		[Description]
	FROM [dbo].[Site_SysConfig] 
	WHERE [ConfigID] = @ConfigID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfig_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfig_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Site_SysConfig_GetPagedList]
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
	
	SET @SqlGet = 'SELECT [ConfigID] FROM [dbo].[Site_SysConfig]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ConfigID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ConfigID],
		biz.[OrganizationID],
		biz.[Name],
		biz.[ConfigGroupID],
		biz.[DisplayName],
		biz.[DefaultValue],
		biz.[OrderNo],
		biz.[UserValue],
		biz.[Modifier],
		biz.[ModifyTime],
		biz.[Description]
	FROM [dbo].[Site_SysConfig] biz
	inner join #PageIndex p on  biz.[ConfigID] = p.[ConfigID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
