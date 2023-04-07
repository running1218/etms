
------------------------------------------------------------------------------------------------------------------------
--Table Notify_MessageType
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageType_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageType_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageType_Insert]
	@MessageTypeID SmallInt,
	@MessageTypeName VarChar(100),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Notify_MessageType]
	(
		[MessageTypeID],
		[MessageTypeName],
		[OrderNum],
		[IsUse]
	)
	VALUES
	(
		@MessageTypeID,
		@MessageTypeName,
		@OrderNum,
		@IsUse
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageType_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageType_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageType_Update]
	@MessageTypeID SmallInt,
	@MessageTypeName VarChar(100),
	@OrderNum Int,
	@IsUse Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Notify_MessageType] SET
		[MessageTypeName] = @MessageTypeName,
		[OrderNum] = @OrderNum,
		[IsUse] = @IsUse
	WHERE [MessageTypeID] = @MessageTypeID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageType_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageType_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageType_Delete]
	@MessageTypeID SmallInt
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Notify_MessageType]
	WHERE [MessageTypeID] = @MessageTypeID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageType_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageType_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_MessageType_GetByPk]
	@MessageTypeID SmallInt
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[MessageTypeID],
		[MessageTypeName],
		[OrderNum],
		[IsUse]
	FROM [dbo].[Notify_MessageType] 
	WHERE [MessageTypeID] = @MessageTypeID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_MessageType_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_MessageType_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Notify_MessageType_GetPagedList]
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
		[MessageTypeID] SmallInt
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [MessageTypeID] FROM [dbo].[Notify_MessageType]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[MessageTypeID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[MessageTypeID],
		biz.[MessageTypeName],
		biz.[OrderNum],
		biz.[IsUse]
	FROM [dbo].[Notify_MessageType] biz
	inner join #PageIndex p on  biz.[MessageTypeID] = p.[MessageTypeID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
