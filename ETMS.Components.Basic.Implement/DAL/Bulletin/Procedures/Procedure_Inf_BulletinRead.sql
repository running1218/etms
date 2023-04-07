
------------------------------------------------------------------------------------------------------------------------
--Table Inf_BulletinRead
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_BulletinRead_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_BulletinRead_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_BulletinRead_Insert]
	@ArticleClickID Int OUTPUT,
	@ArticleID Int,
	@UserID Int,
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Inf_BulletinRead]
	(
		
		[ArticleID],
		[UserID],
		[CreateTime]
	)
	VALUES
	(
		
		@ArticleID,
		@UserID,
		@CreateTime
	)
	
	SET @ArticleClickID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_BulletinRead_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_BulletinRead_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_BulletinRead_Update]
	@ArticleClickID Int,
	@ArticleID Int,
	@UserID Int,
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Inf_BulletinRead] SET
		[ArticleID] = @ArticleID,
		[UserID] = @UserID,
		[CreateTime] = @CreateTime
	WHERE [ArticleClickID] = @ArticleClickID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_BulletinRead_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_BulletinRead_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_BulletinRead_Delete]
	@ArticleClickID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Inf_BulletinRead]
	WHERE [ArticleClickID] = @ArticleClickID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_BulletinRead_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_BulletinRead_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_BulletinRead_GetByPk]
	@ArticleClickID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ArticleClickID],
		[ArticleID],
		[UserID],
		[CreateTime]
	FROM [dbo].[Inf_BulletinRead] 
	WHERE [ArticleClickID] = @ArticleClickID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_BulletinRead_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_BulletinRead_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Inf_BulletinRead_GetPagedList]
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
		[ArticleClickID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ArticleClickID] FROM [dbo].[Inf_BulletinRead]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ArticleClickID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ArticleClickID],
		biz.[ArticleID],
		biz.[UserID],
		biz.[CreateTime]
	FROM [dbo].[Inf_BulletinRead] biz
	inner join #PageIndex p on biz.[[~#ColumnName#~]] = p.[[~#ColumnName#~]] AND[~#EndLoopPK#~]
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
