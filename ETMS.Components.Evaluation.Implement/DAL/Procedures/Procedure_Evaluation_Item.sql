
------------------------------------------------------------------------------------------------------------------------
--Table Evaluation_Item
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_Item_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_Item_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_Item_Insert]
	@ItemID UniqueIdentifier,
	@PlateID UniqueIdentifier,
	@ItemName NVarChar(200),
	@EvaluationLevel Int,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Evaluation_Item]
	(
		[ItemID],
		[PlateID],
		[ItemName],
		[EvaluationLevel],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser]
	)
	VALUES
	(
		@ItemID,
		@PlateID,
		@ItemName,
		@EvaluationLevel,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_Item_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_Item_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_Item_Update]
	@ItemID UniqueIdentifier,
	@PlateID UniqueIdentifier,
	@ItemName NVarChar(200),
	@EvaluationLevel Int,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Evaluation_Item] SET
		[PlateID] = @PlateID,
		[ItemName] = @ItemName,
		[EvaluationLevel] = @EvaluationLevel,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser
	WHERE [ItemID] = @ItemID

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_Item_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_Item_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_Item_Delete]
	@ItemID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Evaluation_Item]
	WHERE [ItemID] = @ItemID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_Item_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_Item_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Evaluation_Item_GetByPk]
	@ItemID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ItemID],
		[PlateID],
		[ItemName],
		[EvaluationLevel],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser]
	FROM [dbo].[Evaluation_Item] 
	WHERE [ItemID] = @ItemID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Evaluation_Item_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Evaluation_Item_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Evaluation_Item_GetPagedList]
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
		[ItemID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ItemID] FROM [dbo].[Evaluation_Item]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ItemID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ItemID],
		biz.[PlateID],
		biz.[ItemName],
		biz.[EvaluationLevel],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser]
	FROM [dbo].[Evaluation_Item] biz
	inner join #PageIndex p on  biz.[ItemID] = p.[ItemID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
