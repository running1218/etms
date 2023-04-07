
------------------------------------------------------------------------------------------------------------------------
--Table Inf_Bulletin
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_Bulletin_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_Bulletin_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_Bulletin_Insert]
	@ArticleID Int OUTPUT,
	@InfoLevelID Int,
	@ArticleTypeID Int,
	@MainHead NVarChar(200),
	@Brief NVarChar(400),
	@Keyword NVarChar(200),
	@ArticleContent Text,
	@OrgID Int,
	@BeginDate DateTime,
	@EndDate DateTime,
	@IsTop Bit,
	@IsUse Int,
	@CreateMan NVarChar(128),
	@CreateUserID Int,
	@CreateTime DateTime,
	@UpdateMan NVarChar(128),
	@UpdateTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Inf_Bulletin]
	(
		
		[InfoLevelID],
		[ArticleTypeID],
		[MainHead],
		[Brief],
		[Keyword],
		[ArticleContent],
		[OrgID],
		[BeginDate],
		[EndDate],
		[IsTop],
		[IsUse],
		[CreateMan],
		[CreateUserID],
		[CreateTime],
		[UpdateMan],
		[UpdateTime]
	)
	VALUES
	(
		
		@InfoLevelID,
		@ArticleTypeID,
		@MainHead,
		@Brief,
		@Keyword,
		@ArticleContent,
		@OrgID,
		@BeginDate,
		@EndDate,
		@IsTop,
		@IsUse,
		@CreateMan,
		@CreateUserID,
		@CreateTime,
		@UpdateMan,
		@UpdateTime
	)
	
	SET @ArticleID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_Bulletin_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_Bulletin_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_Bulletin_Update]
	@ArticleID Int,
	@InfoLevelID Int,
	@ArticleTypeID Int,
	@MainHead NVarChar(200),
	@Brief NVarChar(400),
	@Keyword NVarChar(200),
	@ArticleContent Text,
	@OrgID Int,
	@BeginDate DateTime,
	@EndDate DateTime,
	@IsTop Bit,
	@IsUse Int,
	@CreateMan NVarChar(128),
	@CreateUserID Int,
	@CreateTime DateTime,
	@UpdateMan NVarChar(128),
	@UpdateTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Inf_Bulletin] SET
		[InfoLevelID] = @InfoLevelID,
		[ArticleTypeID] = @ArticleTypeID,
		[MainHead] = @MainHead,
		[Brief] = @Brief,
		[Keyword] = @Keyword,
		[ArticleContent] = @ArticleContent,
		[OrgID] = @OrgID,
		[BeginDate] = @BeginDate,
		[EndDate] = @EndDate,
		[IsTop] = @IsTop,
		[IsUse] = @IsUse,
		[CreateMan] = @CreateMan,
		[CreateUserID] = @CreateUserID,
		[CreateTime] = @CreateTime,
		[UpdateMan] = @UpdateMan,
		[UpdateTime] = @UpdateTime
	WHERE [ArticleID] = @ArticleID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_Bulletin_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_Bulletin_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_Bulletin_Delete]
	@ArticleID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Inf_Bulletin]
	WHERE [ArticleID] = @ArticleID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_Bulletin_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_Bulletin_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Inf_Bulletin_GetByPk]
	@ArticleID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ArticleID],
		[InfoLevelID],
		[ArticleTypeID],
		[MainHead],
		[Brief],
		[Keyword],
		[ArticleContent],
		[OrgID],
		[BeginDate],
		[EndDate],
		[IsTop],
		[IsUse],
		[CreateMan],
		[CreateUserID],
		[CreateTime],
		[UpdateMan],
		[UpdateTime]
	FROM [dbo].[Inf_Bulletin] 
	WHERE [ArticleID] = @ArticleID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Inf_Bulletin_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Inf_Bulletin_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Inf_Bulletin_GetPagedList]
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
		[ArticleID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ArticleID] FROM [dbo].[Inf_Bulletin]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ArticleID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ArticleID],
		biz.[InfoLevelID],
		biz.[ArticleTypeID],
		biz.[MainHead],
		biz.[Brief],
		biz.[Keyword],
		biz.[ArticleContent],
		biz.[OrgID],
		biz.[BeginDate],
		biz.[EndDate],
		biz.[IsTop],
		biz.[IsUse],
		biz.[CreateMan],
		biz.[CreateUserID],
		biz.[CreateTime],
		biz.[UpdateMan],
		biz.[UpdateTime]
	FROM [dbo].[Inf_Bulletin] biz
	inner join #PageIndex p on biz.[[~#ColumnName#~]] = p.[[~#ColumnName#~]] AND[~#EndLoopPK#~]
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
