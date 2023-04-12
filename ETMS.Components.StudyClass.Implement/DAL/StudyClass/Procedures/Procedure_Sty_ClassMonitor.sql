
------------------------------------------------------------------------------------------------------------------------
--Table Sty_ClassMonitor
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_ClassMonitor_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_ClassMonitor_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Sty_ClassMonitor_Insert]
	@ClassMonitorID UniqueIdentifier,
	@ClassStudentID UniqueIdentifier,
	@StudentTypeID Int,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Sty_ClassMonitor]
	(
		[ClassMonitorID],
		[ClassStudentID],
		[StudentTypeID],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	)
	VALUES
	(
		@ClassMonitorID,
		@ClassStudentID,
		@StudentTypeID,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@Remark
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_ClassMonitor_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_ClassMonitor_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Sty_ClassMonitor_Update]
	@ClassMonitorID UniqueIdentifier,
	@ClassStudentID UniqueIdentifier,
	@StudentTypeID Int,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(-1)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Sty_ClassMonitor] SET
		[ClassStudentID] = @ClassStudentID,
		[StudentTypeID] = @StudentTypeID,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark
	WHERE [ClassMonitorID] = @ClassMonitorID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_ClassMonitor_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_ClassMonitor_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Sty_ClassMonitor_Delete]
	@ClassMonitorID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Sty_ClassMonitor]
	WHERE [ClassMonitorID] = @ClassMonitorID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_ClassMonitor_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_ClassMonitor_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Sty_ClassMonitor_GetByPk]
	@ClassMonitorID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ClassMonitorID],
		[ClassStudentID],
		[StudentTypeID],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	FROM [dbo].[Sty_ClassMonitor] 
	WHERE [ClassMonitorID] = @ClassMonitorID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Sty_ClassMonitor_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Sty_ClassMonitor_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Sty_ClassMonitor_GetPagedList]
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
	
	--�������������洢��������
	CREATE TABLE #PageIndex
	(
		[IndexId] int IDENTITY (1, 1) NOT NULL,
		[ClassMonitorID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ClassMonitorID] FROM [dbo].[Sty_ClassMonitor]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ClassMonitorID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ClassMonitorID],
		biz.[ClassStudentID],
		biz.[StudentTypeID],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark]
	FROM [dbo].[Sty_ClassMonitor] biz
	inner join #PageIndex p on  biz.[ClassMonitorID] = p.[ClassMonitorID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 