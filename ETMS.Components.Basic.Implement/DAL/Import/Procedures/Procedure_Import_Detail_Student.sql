
------------------------------------------------------------------------------------------------------------------------
--Table Import_Detail_Student
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_Detail_Student_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_Detail_Student_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_Detail_Student_Insert]
	@DetailID Int OUTPUT,
	@TaskID Int,
	@Status SmallInt,
	@Remark VarChar(500),
	@LoginName VarChar(50),
	@RealName VarChar(50),
	@DepartmentName VarChar(50),
	@RankName VarChar(50),
	@PostName VarChar(50),
	@Email VarChar(50),
	@Mobile VarChar(50),
	@WorkerNo VarChar(50),
	@SexTypeID Int,
	@Identity NVarChar(36),
	@TitleName NVarChar(200),
	@Superior NVarChar(100),
	@Birthday DateTime,
	@OfficeTelphone NVarChar(40),
	@LastEducation NVarChar(100),
	@Specialty NVarChar(100),
	@JoinTime DateTime
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Import_Detail_Student]
	(
		
		[TaskID],
		[Status],
		[Remark],
		[LoginName],
		[RealName],
		[DepartmentName],
		[RankName],
		[PostName],
		[Email],
		[Mobile],
		[WorkerNo],
		[SexTypeID],
		[Identity],
		[TitleName],
		[Superior],
		[Birthday],
		[OfficeTelphone],
		[LastEducation],
		[Specialty],
		[JoinTime]
	)
	VALUES
	(
		
		@TaskID,
		@Status,
		@Remark,
		@LoginName,
		@RealName,
		@DepartmentName,
		@RankName,
		@PostName,
		@Email,
		@Mobile,
		@WorkerNo,
		@SexTypeID,
		@Identity,
		@TitleName,
		@Superior,
		@Birthday,
		@OfficeTelphone,
		@LastEducation,
		@Specialty,
		@JoinTime
	)
	
	SET @DetailID = SCOPE_IDENTITY()
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_Detail_Student_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_Detail_Student_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_Detail_Student_Update]
	@DetailID Int,
	@TaskID Int,
	@Status SmallInt,
	@Remark VarChar(500),
	@LoginName VarChar(50),
	@RealName VarChar(50),
	@DepartmentName VarChar(50),
	@RankName VarChar(50),
	@PostName VarChar(50),
	@Email VarChar(50),
	@Mobile VarChar(50),
	@WorkerNo VarChar(50),
	@SexTypeID Int,
	@Identity NVarChar(36),
	@TitleName NVarChar(200),
	@Superior NVarChar(100),
	@Birthday DateTime,
	@OfficeTelphone NVarChar(40),
	@LastEducation NVarChar(100),
	@Specialty NVarChar(100),
	@JoinTime DateTime
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Import_Detail_Student] SET
		[TaskID] = @TaskID,
		[Status] = @Status,
		[Remark] = @Remark,
		[LoginName] = @LoginName,
		[RealName] = @RealName,
		[DepartmentName] = @DepartmentName,
		[RankName] = @RankName,
		[PostName] = @PostName,
		[Email] = @Email,
		[Mobile] = @Mobile,
		[WorkerNo] = @WorkerNo,
		[SexTypeID] = @SexTypeID,
		[Identity] = @Identity,
		[TitleName] = @TitleName,
		[Superior] = @Superior,
		[Birthday] = @Birthday,
		[OfficeTelphone] = @OfficeTelphone,
		[LastEducation] = @LastEducation,
		[Specialty] = @Specialty,
		[JoinTime] = @JoinTime
	WHERE [DetailID] = @DetailID

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_Detail_Student_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_Detail_Student_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_Detail_Student_Delete]
	@DetailID Int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Import_Detail_Student]
	WHERE [DetailID] = @DetailID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_Detail_Student_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_Detail_Student_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Import_Detail_Student_GetByPk]
	@DetailID Int
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[DetailID],
		[TaskID],
		[Status],
		[Remark],
		[LoginName],
		[RealName],
		[DepartmentName],
		[RankName],
		[PostName],
		[Email],
		[Mobile],
		[WorkerNo],
		[SexTypeID],
		[Identity],
		[TitleName],
		[Superior],
		[Birthday],
		[OfficeTelphone],
		[LastEducation],
		[Specialty],
		[JoinTime]
	FROM [dbo].[Import_Detail_Student] 
	WHERE [DetailID] = @DetailID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Import_Detail_Student_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Import_Detail_Student_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Import_Detail_Student_GetPagedList]
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
		[DetailID] Int
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [DetailID] FROM [dbo].[Import_Detail_Student]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[DetailID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[DetailID],
		biz.[TaskID],
		biz.[Status],
		biz.[Remark],
		biz.[LoginName],
		biz.[RealName],
		biz.[DepartmentName],
		biz.[RankName],
		biz.[PostName],
		biz.[Email],
		biz.[Mobile],
		biz.[WorkerNo],
		biz.[SexTypeID],
		biz.[Identity],
		biz.[TitleName],
		biz.[Superior],
		biz.[Birthday],
		biz.[OfficeTelphone],
		biz.[LastEducation],
		biz.[Specialty],
		biz.[JoinTime]
	FROM [dbo].[Import_Detail_Student] biz
	inner join #PageIndex p on  biz.[DetailID] = p.[DetailID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
