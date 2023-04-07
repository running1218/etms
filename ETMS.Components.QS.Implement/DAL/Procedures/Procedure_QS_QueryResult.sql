
------------------------------------------------------------------------------------------------------------------------
--Table QS_QueryResult
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResult_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResult_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryResult_Insert]
	@BatchID UniqueIdentifier,
	@QueryID UniqueIdentifier,
	@UserID Int,
	@UserName NVarChar(100),
	@CreateTime DateTime,
	@AswerIP NVarChar(60),
	@Score Decimal(8,2)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[QS_QueryResult]
	(
		[BatchID],
		[QueryID],
		[UserID],
		[UserName],
		[CreateTime],
		[AswerIP],
		[Score]
	)
	VALUES
	(
		@BatchID,
		@QueryID,
		@UserID,
		@UserName,
		@CreateTime,
		@AswerIP,
		@Score
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResult_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResult_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryResult_Update]
	@BatchID UniqueIdentifier,
	@QueryID UniqueIdentifier,
	@UserID Int,
	@UserName NVarChar(100),
	@CreateTime DateTime,
	@AswerIP NVarChar(60),
	@Score Decimal(8,2)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[QS_QueryResult] SET
		[QueryID] = @QueryID,
		[UserID] = @UserID,
		[UserName] = @UserName,
		[CreateTime] = @CreateTime,
		[AswerIP] = @AswerIP,
		[Score] = @Score
	WHERE [BatchID] = @BatchID

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResult_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResult_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryResult_Delete]
	@BatchID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[QS_QueryResult]
	WHERE [BatchID] = @BatchID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResult_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResult_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryResult_GetByPk]
	@BatchID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[BatchID],
		[QueryID],
		[UserID],
		[UserName],
		[CreateTime],
		[AswerIP],
		[Score]
	FROM [dbo].[QS_QueryResult] 
	WHERE [BatchID] = @BatchID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResult_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResult_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_QS_QueryResult_GetPagedList]
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
		[BatchID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [BatchID] FROM [dbo].[QS_QueryResult]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[BatchID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[BatchID],
		biz.[QueryID],
		biz.[UserID],
		biz.[UserName],
		biz.[CreateTime],
		biz.[AswerIP],
		biz.[Score]
	FROM [dbo].[QS_QueryResult] biz
	inner join #PageIndex p on  biz.[BatchID] = p.[BatchID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
