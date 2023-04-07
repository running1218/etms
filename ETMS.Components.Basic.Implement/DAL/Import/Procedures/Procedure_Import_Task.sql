
------------------------------------------------------------------------------------------------------------------------
--Table Import_Task
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_Task_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_Task_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_Task_Insert]
	@TaskID Int OUTPUT,
	@TaskName VarChar(300),
	@OrganizationID Int,
	@ImportTypeID Int,
	@Status SmallInt,
	@Remark VarChar(MAX),
	@FilePath VarChar(256),
	@FilleName VarChar(100),
	@CreatorID Int,
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Import_Task]
	(
		
		[TaskName],
		[OrganizationID],
		[ImportTypeID],
		[Status],
		[Remark],
		[FilePath],
		[FilleName],
		[CreatorID],
		[CreateTime]
	)
	VALUES
	(
		
		@TaskName,
		@OrganizationID,
		@ImportTypeID,
		@Status,
		@Remark,
		@FilePath,
		@FilleName,
		@CreatorID,
		@CreateTime
	)
	
	SET @TaskID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_Task_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_Task_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_Task_Update]
	@TaskID Int,
	@TaskName VarChar(300),
	@OrganizationID Int,
	@ImportTypeID Int,
	@Status SmallInt,
	@Remark VarChar(MAX),
	@FilePath VarChar(256),
	@FilleName VarChar(100),
	@CreatorID Int,
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Import_Task] SET
		[TaskName] = @TaskName,
		[OrganizationID] = @OrganizationID,
		[ImportTypeID] = @ImportTypeID,
		[Status] = @Status,
		[Remark] = @Remark,
		[FilePath] = @FilePath,
		[FilleName] = @FilleName,
		[CreatorID] = @CreatorID,
		[CreateTime] = @CreateTime
	WHERE [TaskID] = @TaskID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_Task_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_Task_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_Task_Delete]
	@TaskID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Import_Task]
	WHERE [TaskID] = @TaskID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_Task_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_Task_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_Task_GetByPk]
	@TaskID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[TaskID],
		[TaskName],
		[OrganizationID],
		[ImportTypeID],
		[Status],
		[Remark],
		[FilePath],
		[FilleName],
		[CreatorID],
		[CreateTime]
	FROM [dbo].[Import_Task] 
	WHERE [TaskID] = @TaskID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_Task_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_Task_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Import_Task_GetPagedList]
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
		[TaskID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [TaskID] FROM [dbo].[Import_Task]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[TaskID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[TaskID],
		biz.[TaskName],
		biz.[OrganizationID],
		biz.[ImportTypeID],
		biz.[Status],
		biz.[Remark],
		biz.[FilePath],
		biz.[FilleName],
		biz.[CreatorID],
		biz.[CreateTime]
	FROM [dbo].[Import_Task] biz
	inner join #PageIndex p on  biz.[TaskID] = p.[TaskID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
