
------------------------------------------------------------------------------------------------------------------------
--Table IDP_NotCourseData
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_NotCourseData_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_NotCourseData_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_NotCourseData_Insert]
	@IDP_NotCourseDataID UniqueIdentifier,
	@IDPSourceID Int,
	@OrgID Int,
	@DataCode NVarChar(100),
	@DataName NVarChar(200),
	@DataCotent NVarChar(-1),
	@DataOutline NVarChar(-1),
	@TimeLength Decimal(8,2),
	@DataStatus Int,
	@TeachModelID Int,
	@StudyTimes Int,
	@Implementor NVarChar(128),
	@DataURL NVarChar(256),
	@DutyMan NVarChar(128),
	@EvaluationMode NVarChar(256),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@DelFlag Bit,
	@Remark NVarChar(-1)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[IDP_NotCourseData]
	(
		[IDP_NotCourseDataID],
		[IDPSourceID],
		[OrgID],
		[DataCode],
		[DataName],
		[DataCotent],
		[DataOutline],
		[TimeLength],
		[DataStatus],
		[TeachModelID],
		[StudyTimes],
		[Implementor],
		[DataURL],
		[DutyMan],
		[EvaluationMode],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[DelFlag],
		[Remark]
	)
	VALUES
	(
		@IDP_NotCourseDataID,
		@IDPSourceID,
		@OrgID,
		@DataCode,
		@DataName,
		@DataCotent,
		@DataOutline,
		@TimeLength,
		@DataStatus,
		@TeachModelID,
		@StudyTimes,
		@Implementor,
		@DataURL,
		@DutyMan,
		@EvaluationMode,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@DelFlag,
		@Remark
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_NotCourseData_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_NotCourseData_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_NotCourseData_Update]
	@IDP_NotCourseDataID UniqueIdentifier,
	@IDPSourceID Int,
	@OrgID Int,
	@DataCode NVarChar(100),
	@DataName NVarChar(200),
	@DataCotent NVarChar(-1),
	@DataOutline NVarChar(-1),
	@TimeLength Decimal(8,2),
	@DataStatus Int,
	@TeachModelID Int,
	@StudyTimes Int,
	@Implementor NVarChar(128),
	@DataURL NVarChar(256),
	@DutyMan NVarChar(128),
	@EvaluationMode NVarChar(256),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@DelFlag Bit,
	@Remark NVarChar(-1)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[IDP_NotCourseData] SET
		[IDPSourceID] = @IDPSourceID,
		[OrgID] = @OrgID,
		[DataCode] = @DataCode,
		[DataName] = @DataName,
		[DataCotent] = @DataCotent,
		[DataOutline] = @DataOutline,
		[TimeLength] = @TimeLength,
		[DataStatus] = @DataStatus,
		[TeachModelID] = @TeachModelID,
		[StudyTimes] = @StudyTimes,
		[Implementor] = @Implementor,
		[DataURL] = @DataURL,
		[DutyMan] = @DutyMan,
		[EvaluationMode] = @EvaluationMode,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[DelFlag] = @DelFlag,
		[Remark] = @Remark
	WHERE [IDP_NotCourseDataID] = @IDP_NotCourseDataID

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_NotCourseData_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_NotCourseData_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_NotCourseData_Delete]
	@IDP_NotCourseDataID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[IDP_NotCourseData]
	WHERE [IDP_NotCourseDataID] = @IDP_NotCourseDataID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_NotCourseData_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_NotCourseData_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_IDP_NotCourseData_GetByPk]
	@IDP_NotCourseDataID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[IDP_NotCourseDataID],
		[IDPSourceID],
		[OrgID],
		[DataCode],
		[DataName],
		[DataCotent],
		[DataOutline],
		[TimeLength],
		[DataStatus],
		[TeachModelID],
		[StudyTimes],
		[Implementor],
		[DataURL],
		[DutyMan],
		[EvaluationMode],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[DelFlag],
		[Remark]
	FROM [dbo].[IDP_NotCourseData] 
	WHERE [IDP_NotCourseDataID] = @IDP_NotCourseDataID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_IDP_NotCourseData_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_IDP_NotCourseData_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_IDP_NotCourseData_GetPagedList]
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
		[IDP_NotCourseDataID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [IDP_NotCourseDataID] FROM [dbo].[IDP_NotCourseData]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[IDP_NotCourseDataID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[IDP_NotCourseDataID],
		biz.[IDPSourceID],
		biz.[OrgID],
		biz.[DataCode],
		biz.[DataName],
		biz.[DataCotent],
		biz.[DataOutline],
		biz.[TimeLength],
		biz.[DataStatus],
		biz.[TeachModelID],
		biz.[StudyTimes],
		biz.[Implementor],
		biz.[DataURL],
		biz.[DutyMan],
		biz.[EvaluationMode],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[DelFlag],
		biz.[Remark]
	FROM [dbo].[IDP_NotCourseData] biz
	inner join #PageIndex p on  biz.[IDP_NotCourseDataID] = p.[IDP_NotCourseDataID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
