
------------------------------------------------------------------------------------------------------------------------
--Table QS_QueryResultAnswer
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResultAnswer_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_Insert]
	@AnswerResultID UniqueIdentifier,
	@BatchID UniqueIdentifier,
	@TitleID UniqueIdentifier,
	@Answer NVarChar(2048)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[QS_QueryResultAnswer]
	(
		[AnswerResultID],
		[BatchID],
		[TitleID],
		[Answer]
	)
	VALUES
	(
		@AnswerResultID,
		@BatchID,
		@TitleID,
		@Answer
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResultAnswer_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_Update]
	@AnswerResultID UniqueIdentifier,
	@BatchID UniqueIdentifier,
	@TitleID UniqueIdentifier,
	@Answer NVarChar(2048)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[QS_QueryResultAnswer] SET
		[BatchID] = @BatchID,
		[TitleID] = @TitleID,
		[Answer] = @Answer
	WHERE [AnswerResultID] = @AnswerResultID

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResultAnswer_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_Delete]
	@AnswerResultID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[QS_QueryResultAnswer]
	WHERE [AnswerResultID] = @AnswerResultID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResultAnswer_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_GetByPk]
	@AnswerResultID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[AnswerResultID],
		[BatchID],
		[TitleID],
		[Answer]
	FROM [dbo].[QS_QueryResultAnswer] 
	WHERE [AnswerResultID] = @AnswerResultID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryResultAnswer_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_QS_QueryResultAnswer_GetPagedList]
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
		[AnswerResultID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [AnswerResultID] FROM [dbo].[QS_QueryResultAnswer]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[AnswerResultID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[AnswerResultID],
		biz.[BatchID],
		biz.[TitleID],
		biz.[Answer]
	FROM [dbo].[QS_QueryResultAnswer] biz
	inner join #PageIndex p on  biz.[AnswerResultID] = p.[AnswerResultID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
