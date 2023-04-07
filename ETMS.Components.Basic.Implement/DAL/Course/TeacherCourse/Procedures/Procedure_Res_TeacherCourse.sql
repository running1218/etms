
------------------------------------------------------------------------------------------------------------------------
--Table Res_TeacherCourse
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_TeacherCourse_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_TeacherCourse_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_TeacherCourse_Insert]
	@TeacherCourseID UniqueIdentifier,
	@TeacherID UniqueIdentifier,
	@CourseID UniqueIdentifier,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@CreateUserID Int
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Res_TeacherCourse]
	(
		[TeacherCourseID],
		[TeacherID],
		[CourseID],
		[CreateTime],
		[CreateUser],
		[CreateUserID]
	)
	VALUES
	(
		@TeacherCourseID,
		@TeacherID,
		@CourseID,
		@CreateTime,
		@CreateUser,
		@CreateUserID
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_TeacherCourse_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_TeacherCourse_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_TeacherCourse_Update]
	@TeacherCourseID UniqueIdentifier,
	@TeacherID UniqueIdentifier,
	@CourseID UniqueIdentifier,
	@CreateTime DateTime,
	@CreateUser NVarChar(128),
	@CreateUserID Int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Res_TeacherCourse] SET
		[TeacherID] = @TeacherID,
		[CourseID] = @CourseID,
		[CreateTime] = @CreateTime,
		[CreateUser] = @CreateUser,
		[CreateUserID] = @CreateUserID
	WHERE [TeacherCourseID] = @TeacherCourseID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_TeacherCourse_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_TeacherCourse_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_TeacherCourse_Delete]
	@TeacherID UniqueIdentifier,
	@CourseID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Res_TeacherCourse]
	WHERE [TeacherID] = @TeacherID
		  AND [CourseID] = @CourseID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_TeacherCourse_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_TeacherCourse_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_TeacherCourse_GetByPk]
	@TeacherCourseID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[TeacherCourseID],
		[TeacherID],
		[CourseID],
		[CreateTime],
		[CreateUser],
		[CreateUserID]
	FROM [dbo].[Res_TeacherCourse] 
	WHERE [TeacherCourseID] = @TeacherCourseID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_TeacherCourse_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_TeacherCourse_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Res_TeacherCourse_GetPagedList]
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
		[TeacherCourseID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [TeacherCourseID] FROM [dbo].[Res_TeacherCourse]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[TeacherCourseID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[TeacherCourseID],
		biz.[TeacherID],
		biz.[CourseID],
		biz.[CreateTime],
		biz.[CreateUser],
		biz.[CreateUserID]
	FROM [dbo].[Res_TeacherCourse] biz
	inner join #PageIndex p on  biz.[TeacherCourseID] = p.[TeacherCourseID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
