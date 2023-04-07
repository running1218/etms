
------------------------------------------------------------------------------------------------------------------------
--Table QS_Query
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_Query_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_Query_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_Query_Insert]
	@QueryID UniqueIdentifier,
	@PollTypeID Int,
	@QueryName NVarChar(2048),
	@Header NVarChar(2048),
	@TitlePrefix NVarChar(20),
	@IsDisplayColumn Bit,
	@Location Int,
	@OrganizationID Int,
	@BeginTime DateTime,
	@EndTime DateTime,
	@Footer NVarChar(2048),
	@QueryStatus SmallInt,
	@IsAllSave Bit,
	@IsTitleNoSort Bit,
	@IsRepeat Bit,
	@IsDisplayResult Bit,
	@IsTemplate Bit,
	@IsPublish Bit,
	@IsHasScore Bit,
	@DutyUser NVarChar(100),
	@CreateUserID Int,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(Max)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[QS_Query]
	(
		[QueryID],
		[PollTypeID],
		[QueryName],
		[Header],
		[TitlePrefix],
		[IsDisplayColumn],
		[Location],
		[OrganizationID],
		[BeginTime],
		[EndTime],
		[Footer],
		[QueryStatus],
		[IsAllSave],
		[IsTitleNoSort],
		[IsRepeat],
		[IsDisplayResult],
		[IsTemplate],
		[IsPublish],
		[IsHasScore],
		[DutyUser],
		[CreateUserID],
		[CreateTime],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	)
	VALUES
	(
		@QueryID,
		@PollTypeID,
		@QueryName,
		@Header,
		@TitlePrefix,
		@IsDisplayColumn,
		@Location,
		@OrganizationID,
		@BeginTime,
		@EndTime,
		@Footer,
		@QueryStatus,
		@IsAllSave,
		@IsTitleNoSort,
		@IsRepeat,
		@IsDisplayResult,
		@IsTemplate,
		@IsPublish,
		@IsHasScore,
		@DutyUser,
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
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_Query_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_Query_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_Query_Update]
	@QueryID UniqueIdentifier,
	@PollTypeID Int,
	@QueryName NVarChar(2048),
	@Header NVarChar(2048),
	@TitlePrefix NVarChar(20),
	@IsDisplayColumn Bit,
	@Location Int,
	@OrganizationID Int,
	@BeginTime DateTime,
	@EndTime DateTime,
	@Footer NVarChar(2048),
	@QueryStatus SmallInt,
	@IsAllSave Bit,
	@IsTitleNoSort Bit,
	@IsRepeat Bit,
	@IsDisplayResult Bit,
	@IsTemplate Bit,
	@IsPublish Bit,
	@IsHasScore Bit,
	@DutyUser NVarChar(100),
	@CreateUserID Int,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(Max)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[QS_Query] SET
		[PollTypeID] = @PollTypeID,
		[QueryName] = @QueryName,
		[Header] = @Header,
		[TitlePrefix] = @TitlePrefix,
		[IsDisplayColumn] = @IsDisplayColumn,
		[Location] = @Location,
		[OrganizationID] = @OrganizationID,
		[BeginTime] = @BeginTime,
		[EndTime] = @EndTime,
		[Footer] = @Footer,
		[QueryStatus] = @QueryStatus,
		[IsAllSave] = @IsAllSave,
		[IsTitleNoSort] = @IsTitleNoSort,
		[IsRepeat] = @IsRepeat,
		[IsDisplayResult] = @IsDisplayResult,
		[IsTemplate] = @IsTemplate,
		[IsPublish] = @IsPublish,
		[IsHasScore] = @IsHasScore,
		[DutyUser] = @DutyUser,
		[CreateUserID] = @CreateUserID,
		[CreateTime] = @CreateTime,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark
	WHERE [QueryID] = @QueryID

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_Query_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_Query_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_Query_Delete]
	@QueryID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[QS_Query]
	WHERE [QueryID] = @QueryID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_Query_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_Query_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_QS_Query_GetByPk]
	@QueryID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[QueryID],
		[PollTypeID],
		[QueryName],
		[Header],
		[TitlePrefix],
		[IsDisplayColumn],
		[Location],
		[OrganizationID],
		[BeginTime],
		[EndTime],
		[Footer],
		[QueryStatus],
		[IsAllSave],
		[IsTitleNoSort],
		[IsRepeat],
		[IsDisplayResult],
		[IsTemplate],
		[IsPublish],
		[IsHasScore],
		[DutyUser],
		[CreateUserID],
		[CreateTime],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark]
	FROM [dbo].[QS_Query] 
	WHERE [QueryID] = @QueryID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_QS_Query_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_QS_Query_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_QS_Query_GetPagedList]
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
		[QueryID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [QueryID] FROM [dbo].[QS_Query]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[QueryID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[QueryID],
		biz.[PollTypeID],
		biz.[QueryName],
		biz.[Header],
		biz.[TitlePrefix],
		biz.[IsDisplayColumn],
		biz.[Location],
		biz.[OrganizationID],
		biz.[BeginTime],
		biz.[EndTime],
		biz.[Footer],
		biz.[QueryStatus],
		biz.[IsAllSave],
		biz.[IsTitleNoSort],
		biz.[IsRepeat],
		biz.[IsDisplayResult],
		biz.[IsTemplate],
		biz.[IsPublish],
		biz.[IsHasScore],
		biz.[DutyUser],
		biz.[CreateUserID],
		biz.[CreateTime],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark]
	FROM [dbo].[QS_Query] biz
	inner join #PageIndex p on  biz.[QueryID] = p.[QueryID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
