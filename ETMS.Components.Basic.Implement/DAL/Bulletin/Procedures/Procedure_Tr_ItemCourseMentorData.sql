
------------------------------------------------------------------------------------------------------------------------
--Table Tr_ItemCourseMentorData
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourseMentorData_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourseMentorData_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourseMentorData_Insert]
	@ItemCourseMentorDataID UniqueIdentifier,
	@TrainingItemCourseID UniqueIdentifier,
	@ArticleID Int,
	@IsUse Int,
	@BeginTime DateTime,
	@EndTime DateTime,
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Tr_ItemCourseMentorData]
	(
		[ItemCourseMentorDataID],
		[TrainingItemCourseID],
		[ArticleID],
		[IsUse],
		[BeginTime],
		[EndTime],
		[CreateTime]
	)
	VALUES
	(
		@ItemCourseMentorDataID,
		@TrainingItemCourseID,
		@ArticleID,
		@IsUse,
		@BeginTime,
		@EndTime,
		@CreateTime
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourseMentorData_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourseMentorData_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourseMentorData_Update]
	@ItemCourseMentorDataID UniqueIdentifier,
	@TrainingItemCourseID UniqueIdentifier,
	@ArticleID Int,
	@IsUse Int,
	@BeginTime DateTime,
	@EndTime DateTime,
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Tr_ItemCourseMentorData] SET
		[TrainingItemCourseID] = @TrainingItemCourseID,
		[ArticleID] = @ArticleID,
		[IsUse] = @IsUse,
		[BeginTime] = @BeginTime,
		[EndTime] = @EndTime,
		[CreateTime] = @CreateTime
	WHERE [ItemCourseMentorDataID] = @ItemCourseMentorDataID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourseMentorData_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourseMentorData_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourseMentorData_Delete]
	@ItemCourseMentorDataID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Tr_ItemCourseMentorData]
	WHERE [ItemCourseMentorDataID] = @ItemCourseMentorDataID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourseMentorData_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourseMentorData_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourseMentorData_GetByPk]
	@ItemCourseMentorDataID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ItemCourseMentorDataID],
		[TrainingItemCourseID],
		[ArticleID],
		[IsUse],
		[BeginTime],
		[EndTime],
		[CreateTime]
	FROM [dbo].[Tr_ItemCourseMentorData] 
	WHERE [ItemCourseMentorDataID] = @ItemCourseMentorDataID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourseMentorData_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourseMentorData_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourseMentorData_GetPagedList]
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
		[ItemCourseMentorDataID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ItemCourseMentorDataID] FROM [dbo].[Tr_ItemCourseMentorData]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ItemCourseMentorDataID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ItemCourseMentorDataID],
		biz.[TrainingItemCourseID],
		biz.[ArticleID],
		biz.[IsUse],
		biz.[BeginTime],
		biz.[EndTime],
		biz.[CreateTime]
	FROM [dbo].[Tr_ItemCourseMentorData] biz
	inner join #PageIndex p on biz.[[~#ColumnName#~]] = p.[[~#ColumnName#~]] AND[~#EndLoopPK#~]
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
