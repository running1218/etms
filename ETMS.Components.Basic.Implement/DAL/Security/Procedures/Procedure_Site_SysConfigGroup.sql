
------------------------------------------------------------------------------------------------------------------------
--Table Site_SysConfigGroup
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfigGroup_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfigGroup_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_SysConfigGroup_Insert]
	@ConfigGroupID Int,
	@ConfigGroupName VarChar(100),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Site_SysConfigGroup]
	(
		[ConfigGroupID],
		[ConfigGroupName],
		[OrderNum],
		[IsUse]
	)
	VALUES
	(
		@ConfigGroupID,
		@ConfigGroupName,
		@OrderNum,
		@IsUse
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfigGroup_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfigGroup_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_SysConfigGroup_Update]
	@ConfigGroupID Int,
	@ConfigGroupName VarChar(100),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Site_SysConfigGroup] SET
		[ConfigGroupName] = @ConfigGroupName,
		[OrderNum] = @OrderNum,
		[IsUse] = @IsUse
	WHERE [ConfigGroupID] = @ConfigGroupID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfigGroup_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfigGroup_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_SysConfigGroup_Delete]
	@ConfigGroupID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Site_SysConfigGroup]
	WHERE [ConfigGroupID] = @ConfigGroupID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfigGroup_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfigGroup_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Site_SysConfigGroup_GetByPk]
	@ConfigGroupID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ConfigGroupID],
		[ConfigGroupName],
		[OrderNum],
		[IsUse]
	FROM [dbo].[Site_SysConfigGroup] 
	WHERE [ConfigGroupID] = @ConfigGroupID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Site_SysConfigGroup_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Site_SysConfigGroup_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Site_SysConfigGroup_GetPagedList]
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
		[ConfigGroupID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ConfigGroupID] FROM [dbo].[Site_SysConfigGroup]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ConfigGroupID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ConfigGroupID],
		biz.[ConfigGroupName],
		biz.[OrderNum],
		biz.[IsUse]
	FROM [dbo].[Site_SysConfigGroup] biz
	inner join #PageIndex p on  biz.[ConfigGroupID] = p.[ConfigGroupID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
