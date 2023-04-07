
------------------------------------------------------------------------------------------------------------------------
--Table QS_QueryTitleOption
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitleOption_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitleOption_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryTitleOption_Insert]
	@OptionID UniqueIdentifier,
	@TitleID UniqueIdentifier,
	@OptionName NVarChar(2048),
	@OptionNo Int,
	@OptionScore Decimal(8,2),
	@OtherLength Int,
	@CreateUserID Int,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(Max)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[QS_QueryTitleOption]
	(
		[OptionID],
		[TitleID],
		[OptionName],
		[OptionNo],
		[OptionScore],
		[OtherLength],
		[CreateUserID],
		[CreateTime],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	)
	VALUES
	(
		@OptionID,
		@TitleID,
		@OptionName,
		@OptionNo,
		@OptionScore,
		@OtherLength,
		@CreateUserID,
		@CreateTime,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@Remark
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitleOption_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitleOption_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryTitleOption_Update]
	@OptionID UniqueIdentifier,
	@TitleID UniqueIdentifier,
	@OptionName NVarChar(2048),
	@OptionNo Int,
	@OptionScore Decimal(8,2),
	@OtherLength Int,
	@CreateUserID Int,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(Max)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[QS_QueryTitleOption] SET
		[TitleID] = @TitleID,
		[OptionName] = @OptionName,
		[OptionNo] = @OptionNo,
		[OptionScore] = @OptionScore,
		[OtherLength] = @OtherLength,
		[CreateUserID] = @CreateUserID,
		[CreateTime] = @CreateTime,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark
	WHERE [OptionID] = @OptionID

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitleOption_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitleOption_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryTitleOption_Delete]
	@OptionID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[QS_QueryTitleOption]
	WHERE [OptionID] = @OptionID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitleOption_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitleOption_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryTitleOption_GetByPk]
	@OptionID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[OptionID],
		[TitleID],
		[OptionName],
		[OptionNo],
		[OptionScore],
		[OtherLength],
		[CreateUserID],
		[CreateTime],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	FROM [dbo].[QS_QueryTitleOption] 
	WHERE [OptionID] = @OptionID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitleOption_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitleOption_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_QS_QueryTitleOption_GetPagedList]
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
		[OptionID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [OptionID] FROM [dbo].[QS_QueryTitleOption]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[OptionID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[OptionID],
		biz.[TitleID],
		biz.[OptionName],
		biz.[OptionNo],
		biz.[OptionScore],
		biz.[OtherLength],
		biz.[CreateUserID],
		biz.[CreateTime],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark]
	FROM [dbo].[QS_QueryTitleOption] biz
	inner join #PageIndex p on  biz.[OptionID] = p.[OptionID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
