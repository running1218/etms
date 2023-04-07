
------------------------------------------------------------------------------------------------------------------------
--Table Evaluation_PlateResult
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_PlateResult_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_PlateResult_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_PlateResult_Insert]
	@ResultSubID UniqueIdentifier,
	@PlateID UniqueIdentifier,
	@UserID Int,
	@ObjectID NVarChar(100),
	@EvaluationContent NVarChar(-1),
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Evaluation_PlateResult]
	(
		[ResultSubID],
		[PlateID],
		[UserID],
		[ObjectID],
		[EvaluationContent],
		[CreateTime]
	)
	VALUES
	(
		@ResultSubID,
		@PlateID,
		@UserID,
		@ObjectID,
		@EvaluationContent,
		@CreateTime
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_PlateResult_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_PlateResult_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_PlateResult_Update]
	@ResultSubID UniqueIdentifier,
	@PlateID UniqueIdentifier,
	@UserID Int,
	@ObjectID NVarChar(100),
	@EvaluationContent NVarChar(-1),
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Evaluation_PlateResult] SET
		[PlateID] = @PlateID,
		[UserID] = @UserID,
		[ObjectID] = @ObjectID,
		[EvaluationContent] = @EvaluationContent,
		[CreateTime] = @CreateTime
	WHERE [ResultSubID] = @ResultSubID

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_PlateResult_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_PlateResult_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_PlateResult_Delete]
	@ResultSubID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Evaluation_PlateResult]
	WHERE [ResultSubID] = @ResultSubID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_PlateResult_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_PlateResult_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_PlateResult_GetByPk]
	@ResultSubID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ResultSubID],
		[PlateID],
		[UserID],
		[ObjectID],
		[EvaluationContent],
		[CreateTime]
	FROM [dbo].[Evaluation_PlateResult] 
	WHERE [ResultSubID] = @ResultSubID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_PlateResult_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_PlateResult_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Evaluation_PlateResult_GetPagedList]
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
		[ResultSubID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ResultSubID] FROM [dbo].[Evaluation_PlateResult]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ResultSubID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ResultSubID],
		biz.[PlateID],
		biz.[UserID],
		biz.[ObjectID],
		biz.[EvaluationContent],
		biz.[CreateTime]
	FROM [dbo].[Evaluation_PlateResult] biz
	inner join #PageIndex p on  biz.[ResultSubID] = p.[ResultSubID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
