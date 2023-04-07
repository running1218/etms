
------------------------------------------------------------------------------------------------------------------------
--Table QS_QueryArea
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryArea_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryArea_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryArea_Insert]
	@QueryAreaID UniqueIdentifier,
	@QueryID UniqueIdentifier,
	@AreaType NVarChar(200),
	@AreaCode NVarChar(100),
	@CreateUserID Int,
	@Creator NVarChar(128),
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[QS_QueryArea]
	(
		[QueryAreaID],
		[QueryID],
		[AreaType],
		[AreaCode],
		[CreateUserID],
		[Creator],
		[CreateTime]
	)
	VALUES
	(
		@QueryAreaID,
		@QueryID,
		@AreaType,
		@AreaCode,
		@CreateUserID,
		@Creator,
		@CreateTime
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryArea_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryArea_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryArea_Update]
	@QueryAreaID UniqueIdentifier,
	@QueryID UniqueIdentifier,
	@AreaType NVarChar(200),
	@AreaCode NVarChar(100),
	@CreateUserID Int,
	@Creator NVarChar(128),
	@CreateTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[QS_QueryArea] SET
		[QueryID] = @QueryID,
		[AreaType] = @AreaType,
		[AreaCode] = @AreaCode,
		[CreateUserID] = @CreateUserID,
		[Creator] = @Creator,
		[CreateTime] = @CreateTime
	WHERE [QueryAreaID] = @QueryAreaID

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryArea_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryArea_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryArea_Delete]
	@QueryAreaID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[QS_QueryArea]
	WHERE [QueryAreaID] = @QueryAreaID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryArea_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryArea_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryArea_GetByPk]
	@QueryAreaID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[QueryAreaID],
		[QueryID],
		[AreaType],
		[AreaCode],
		[CreateUserID],
		[Creator],
		[CreateTime]
	FROM [dbo].[QS_QueryArea] 
	WHERE [QueryAreaID] = @QueryAreaID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryArea_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryArea_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_QS_QueryArea_GetPagedList]
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
		[QueryAreaID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [QueryAreaID] FROM [dbo].[QS_QueryArea]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[QueryAreaID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[QueryAreaID],
		biz.[QueryID],
		biz.[AreaType],
		biz.[AreaCode],
		biz.[CreateUserID],
		biz.[Creator],
		biz.[CreateTime]
	FROM [dbo].[QS_QueryArea] biz
	inner join #PageIndex p on  biz.[QueryAreaID] = p.[QueryAreaID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
