
------------------------------------------------------------------------------------------------------------------------
--Table Notify_Message
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_Message_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_Message_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_Message_Insert]
	@MessageID Int OUTPUT,
	@MessageClassID SmallInt,
	@MessageTypeID SmallInt,
	@OrganizationID Int,
	@Subject NVarChar(400),
	@Body NVarChar(max),
	@Receiver VarChar(max),
	@Status SmallInt,
	@Remark VarChar(max),
	@CreatorID Int,
	@CreateTime DateTime,
	@ReceiveTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Notify_Message]
	(
		
		[MessageClassID],
		[MessageTypeID],
		[OrganizationID],
		[Subject],
		[Body],
		[Receiver],
		[Status],
		[Remark],
		[CreatorID],
		[CreateTime],
		[ReceiveTime]
	)
	VALUES
	(
		
		@MessageClassID,
		@MessageTypeID,
		@OrganizationID,
		@Subject,
		@Body,
		@Receiver,
		@Status,
		@Remark,
		@CreatorID,
		@CreateTime,
		@ReceiveTime
	)
	
	SET @MessageID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_Message_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_Message_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_Message_Update]
	@MessageID Int,
	@MessageClassID SmallInt,
	@MessageTypeID SmallInt,
	@OrganizationID Int,
	@Subject NVarChar(400),
	@Body NVarChar(max),
	@Receiver VarChar(max),
	@Status SmallInt,
	@Remark VarChar(max),
	@CreatorID Int,
	@CreateTime DateTime,
	@ReceiveTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Notify_Message] SET
		[MessageClassID] = @MessageClassID,
		[MessageTypeID] = @MessageTypeID,
		[OrganizationID] = @OrganizationID,
		[Subject] = @Subject,
		[Body] = @Body,
		[Receiver] = @Receiver,
		[Status] = @Status,
		[Remark] = @Remark,
		[CreatorID] = @CreatorID,
		[CreateTime] = @CreateTime,
		[ReceiveTime] = @ReceiveTime
	WHERE [MessageID] = @MessageID

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_Message_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_Message_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_Message_Delete]
	@MessageID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Notify_Message]
	WHERE [MessageID] = @MessageID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_Message_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_Message_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Notify_Message_GetByPk]
	@MessageID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[MessageID],
		[MessageClassID],
		[MessageTypeID],
		[OrganizationID],
		[Subject],
		[Body],
		[Receiver],
		[Status],
		[Remark],
		[CreatorID],
		[CreateTime],
		[ReceiveTime]
	FROM [dbo].[Notify_Message] 
	WHERE [MessageID] = @MessageID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Notify_Message_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Notify_Message_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Notify_Message_GetPagedList]
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
		[MessageID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [MessageID] FROM [dbo].[Notify_Message]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[MessageID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[MessageID],
		biz.[MessageClassID],
		biz.[MessageTypeID],
		biz.[OrganizationID],
		biz.[Subject],
		biz.[Body],
		biz.[Receiver],
		biz.[Status],
		biz.[Remark],
		biz.[CreatorID],
		biz.[CreateTime],
		biz.[ReceiveTime]
	FROM [dbo].[Notify_Message] biz
	inner join #PageIndex p on  biz.[MessageID] = p.[MessageID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
