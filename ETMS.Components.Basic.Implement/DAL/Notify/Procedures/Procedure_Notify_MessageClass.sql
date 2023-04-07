
------------------------------------------------------------------------------------------------------------------------
--Table Notify_MessageClass
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageClass_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageClass_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageClass_Insert]
	@MessageClassID SmallInt,
	@MessageClassName VarChar(100),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Notify_MessageClass]
	(
		[MessageClassID],
		[MessageClassName],
		[OrderNum],
		[IsUse]
	)
	VALUES
	(
		@MessageClassID,
		@MessageClassName,
		@OrderNum,
		@IsUse
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageClass_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageClass_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageClass_Update]
	@MessageClassID SmallInt,
	@MessageClassName VarChar(100),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Notify_MessageClass] SET
		[MessageClassName] = @MessageClassName,
		[OrderNum] = @OrderNum,
		[IsUse] = @IsUse
	WHERE [MessageClassID] = @MessageClassID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageClass_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageClass_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageClass_Delete]
	@MessageClassID SmallInt
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Notify_MessageClass]
	WHERE [MessageClassID] = @MessageClassID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageClass_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageClass_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageClass_GetByPk]
	@MessageClassID SmallInt
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[MessageClassID],
		[MessageClassName],
		[OrderNum],
		[IsUse]
	FROM [dbo].[Notify_MessageClass] 
	WHERE [MessageClassID] = @MessageClassID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageClass_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageClass_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Notify_MessageClass_GetPagedList]
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
		[MessageClassID] SmallInt
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [MessageClassID] FROM [dbo].[Notify_MessageClass]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[MessageClassID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[MessageClassID],
		biz.[MessageClassName],
		biz.[OrderNum],
		biz.[IsUse]
	FROM [dbo].[Notify_MessageClass] biz
	inner join #PageIndex p on  biz.[MessageClassID] = p.[MessageClassID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
