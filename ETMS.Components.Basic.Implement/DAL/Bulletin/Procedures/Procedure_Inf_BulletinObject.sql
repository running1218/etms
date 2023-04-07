
------------------------------------------------------------------------------------------------------------------------
--Table Inf_BulletinObject
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_BulletinObject_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_BulletinObject_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_BulletinObject_Insert]
	@BulletinObjectID UniqueIdentifier,
	@ArticleID Int,
	@BulletinObjectTypeID Int,
	@IsUse Int,
	@BeginTime DateTime,
	@EndTime DateTime,
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Inf_BulletinObject]
	(
		[BulletinObjectID],
		[ArticleID],
		[BulletinObjectTypeID],
		[IsUse],
		[BeginTime],
		[EndTime],
		[CreateTime]
	)
	VALUES
	(
		@BulletinObjectID,
		@ArticleID,
		@BulletinObjectTypeID,
		@IsUse,
		@BeginTime,
		@EndTime,
		@CreateTime
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_BulletinObject_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_BulletinObject_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_BulletinObject_Update]
	@BulletinObjectID UniqueIdentifier,
	@ArticleID Int,
	@BulletinObjectTypeID Int,
	@IsUse Int,
	@BeginTime DateTime,
	@EndTime DateTime,
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Inf_BulletinObject] SET
		[ArticleID] = @ArticleID,
		[BulletinObjectTypeID] = @BulletinObjectTypeID,
		[IsUse] = @IsUse,
		[BeginTime] = @BeginTime,
		[EndTime] = @EndTime,
		[CreateTime] = @CreateTime
	WHERE [BulletinObjectID] = @BulletinObjectID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_BulletinObject_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_BulletinObject_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_BulletinObject_Delete]
	@BulletinObjectID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Inf_BulletinObject]
	WHERE [BulletinObjectID] = @BulletinObjectID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_BulletinObject_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_BulletinObject_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_BulletinObject_GetByPk]
	@BulletinObjectID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[BulletinObjectID],
		[ArticleID],
		[BulletinObjectTypeID],
		[IsUse],
		[BeginTime],
		[EndTime],
		[CreateTime]
	FROM [dbo].[Inf_BulletinObject] 
	WHERE [BulletinObjectID] = @BulletinObjectID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_BulletinObject_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_BulletinObject_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Inf_BulletinObject_GetPagedList]
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
		[BulletinObjectID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [BulletinObjectID] FROM [dbo].[Inf_BulletinObject]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[BulletinObjectID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[BulletinObjectID],
		biz.[ArticleID],
		biz.[BulletinObjectTypeID],
		biz.[IsUse],
		biz.[BeginTime],
		biz.[EndTime],
		biz.[CreateTime]
	FROM [dbo].[Inf_BulletinObject] biz
	inner join #PageIndex p on biz.[[~#ColumnName#~]] = p.[[~#ColumnName#~]] AND[~#EndLoopPK#~]
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
