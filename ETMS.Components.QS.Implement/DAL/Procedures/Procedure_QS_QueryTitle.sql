
------------------------------------------------------------------------------------------------------------------------
--Table QS_QueryTitle
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitle_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitle_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryTitle_Insert]
	@TitleID UniqueIdentifier,
	@QueryID UniqueIdentifier,
	@TitleTypeID Int,
	@TitleName NVarChar(2048),
	@TitleNo Int,
	@MinSelectNum Int,
	@MaxSelectNum Int,
	@CreateUserID Int,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(Max)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[QS_QueryTitle]
	(
		[TitleID],
		[QueryID],
		[TitleTypeID],
		[TitleName],
		[TitleNo],
		[MinSelectNum],
		[MaxSelectNum],
		[CreateUserID],
		[CreateTime],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	)
	VALUES
	(
		@TitleID,
		@QueryID,
		@TitleTypeID,
		@TitleName,
		@TitleNo,
		@MinSelectNum,
		@MaxSelectNum,
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
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitle_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitle_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryTitle_Update]
	@TitleID UniqueIdentifier,
	@QueryID UniqueIdentifier,
	@TitleTypeID Int,
	@TitleName NVarChar(2048),
	@TitleNo Int,
	@MinSelectNum Int,
	@MaxSelectNum Int,
	@CreateUserID Int,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(Max)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[QS_QueryTitle] SET
		[QueryID] = @QueryID,
		[TitleTypeID] = @TitleTypeID,
		[TitleName] = @TitleName,
		[TitleNo] = @TitleNo,
		[MinSelectNum] = @MinSelectNum,
		[MaxSelectNum] = @MaxSelectNum,
		[CreateUserID] = @CreateUserID,
		[CreateTime] = @CreateTime,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark
	WHERE [TitleID] = @TitleID

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitle_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitle_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryTitle_Delete]
	@TitleID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[QS_QueryTitle]
	WHERE [TitleID] = @TitleID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitle_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitle_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_QueryTitle_GetByPk]
	@TitleID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[TitleID],
		[QueryID],
		[TitleTypeID],
		[TitleName],
		[TitleNo],
		[MinSelectNum],
		[MaxSelectNum],
		[CreateUserID],
		[CreateTime],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	FROM [dbo].[QS_QueryTitle] 
	WHERE [TitleID] = @TitleID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_QueryTitle_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_QueryTitle_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_QS_QueryTitle_GetPagedList]
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
		[TitleID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [TitleID] FROM [dbo].[QS_QueryTitle]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[TitleID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[TitleID],
		biz.[QueryID],
		biz.[TitleTypeID],
		biz.[TitleName],
		biz.[TitleNo],
		biz.[MinSelectNum],
		biz.[MaxSelectNum],
		biz.[CreateUserID],
		biz.[CreateTime],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark]
	FROM [dbo].[QS_QueryTitle] biz
	inner join #PageIndex p on  biz.[TitleID] = p.[TitleID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
