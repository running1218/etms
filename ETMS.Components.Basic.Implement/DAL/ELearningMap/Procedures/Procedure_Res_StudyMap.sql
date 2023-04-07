
------------------------------------------------------------------------------------------------------------------------
--Table Res_StudyMap
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_StudyMap_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_StudyMap_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_StudyMap_Insert]
	@StudyMapID UniqueIdentifier,
	@ELearningMapTypeID Int,
	@StudyMapCode NVarChar(100),
	@StudyMapName NVarChar(200),
	@DeptID NVarChar(100),
	@PostID NVarChar(100),
	@RankID NVarChar(100),
	@StudyMapDesc NVarChar(2048),
	@Status Int,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1),
	@OrgID Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Res_StudyMap]
	(
		[StudyMapID],
		[ELearningMapTypeID],
		[StudyMapCode],
		[StudyMapName],
		[DeptID],
		[PostID],
		[RankID],
		[StudyMapDesc],
		[Status],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[OrgID]
	)
	VALUES
	(
		@StudyMapID,
		@ELearningMapTypeID,
		@StudyMapCode,
		@StudyMapName,
		@DeptID,
		@PostID,
		@RankID,
		@StudyMapDesc,
		@Status,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@Remark,
		@OrgID
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_StudyMap_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_StudyMap_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_StudyMap_Update]
	@StudyMapID UniqueIdentifier,
	@ELearningMapTypeID Int,
	@StudyMapCode NVarChar(100),
	@StudyMapName NVarChar(200),
	@DeptID NVarChar(100),
	@PostID NVarChar(100),
	@RankID NVarChar(100),
	@StudyMapDesc NVarChar(2048),
	@Status Int,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1),
	@OrgID Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Res_StudyMap] SET
		[ELearningMapTypeID] = @ELearningMapTypeID,
		[StudyMapCode] = @StudyMapCode,
		[StudyMapName] = @StudyMapName,
		[DeptID] = @DeptID,
		[PostID] = @PostID,
		[RankID] = @RankID,
		[StudyMapDesc] = @StudyMapDesc,
		[Status] = @Status,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark,
		[OrgID] = @OrgID
	WHERE [StudyMapID] = @StudyMapID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_StudyMap_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_StudyMap_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_StudyMap_Delete]
	@StudyMapID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Res_StudyMap]
	WHERE [StudyMapID] = @StudyMapID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_StudyMap_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_StudyMap_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_StudyMap_GetByPk]
	@StudyMapID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[StudyMapID],
		[ELearningMapTypeID],
		[StudyMapCode],
		[StudyMapName],
		[DeptID],
		[PostID],
		[RankID],
		[StudyMapDesc],
		[Status],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[OrgID]
	FROM [dbo].[Res_StudyMap] 
	WHERE [StudyMapID] = @StudyMapID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_StudyMap_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_StudyMap_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Res_StudyMap_GetPagedList]
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
		[StudyMapID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [StudyMapID] FROM [dbo].[Res_StudyMap]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[StudyMapID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[StudyMapID],
		biz.[ELearningMapTypeID],
		biz.[StudyMapCode],
		biz.[StudyMapName],
		biz.[DeptID],
		biz.[PostID],
		biz.[RankID],
		biz.[StudyMapDesc],
		biz.[Status],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark],
		biz.[OrgID]
	FROM [dbo].[Res_StudyMap] biz
	inner join #PageIndex p on  biz.[StudyMapID] = p.[StudyMapID] AND
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
