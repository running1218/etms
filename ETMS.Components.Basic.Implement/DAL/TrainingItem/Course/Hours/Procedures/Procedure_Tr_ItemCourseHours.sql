
------------------------------------------------------------------------------------------------------------------------
--Table Tr_ItemCourseHours
------------------------------------------------------------------------------------------------------------------------
--���Ӽ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourseHours_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourseHours_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourseHours_Insert]
	@ItemCourseHoursID UniqueIdentifier,
	@TrainingItemCourseID UniqueIdentifier,
	@ClassRoomID UniqueIdentifier,
	@CourseHoursStatusID Int,
	@TrainingTimeDescID Int,
	@TrainingDate DateTime,
	@TrainingBeginTime DateTime,
	@TrainingEndTime DateTime,
	@CourseFee Decimal(15,2),
	@CourseHours Decimal(8,2),
	@CourseHoursDesc NVarChar(Max),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(Max),
	@DelFlag Bit,
	@RealCourseHours Decimal(8,2),
	@RealCourseFee Decimal(15,2),
	@TeacherID Int,
	@PayStatus Int,
	@CourseHoursStatusDesc NVarChar(Max)
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Tr_ItemCourseHours]
	(
		[ItemCourseHoursID],
		[TrainingItemCourseID],
		[ClassRoomID],
		[CourseHoursStatusID],
		[TrainingTimeDescID],
		[TrainingDate],
		[TrainingBeginTime],
		[TrainingEndTime],
		[CourseFee],
		[CourseHours],
		[CourseHoursDesc],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[DelFlag],
		[RealCourseHours],
		[RealCourseFee],
		[TeacherID],
		[PayStatus],
		[CourseHoursStatusDesc]
	)
	VALUES
	(
		@ItemCourseHoursID,
		@TrainingItemCourseID,
		@ClassRoomID,
		@CourseHoursStatusID,
		@TrainingTimeDescID,
		@TrainingDate,
		@TrainingBeginTime,
		@TrainingEndTime,
		@CourseFee,
		@CourseHours,
		@CourseHoursDesc,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@Remark,
		@DelFlag,
		@RealCourseHours,
		@RealCourseFee,
		@TeacherID,
		@PayStatus,
		@CourseHoursStatusDesc
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--�޸ļ�¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourseHours_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourseHours_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourseHours_Update]
	@ItemCourseHoursID UniqueIdentifier,
	@TrainingItemCourseID UniqueIdentifier,
	@ClassRoomID UniqueIdentifier,
	@CourseHoursStatusID Int,
	@TrainingTimeDescID Int,
	@TrainingDate DateTime,
	@TrainingBeginTime DateTime,
	@TrainingEndTime DateTime,
	@CourseFee Decimal(15,2),
	@CourseHours Decimal(8,2),
	@CourseHoursDesc NVarChar(Max),
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@Remark NVarChar(Max),
	@DelFlag Bit,
	@RealCourseHours Decimal(8,2),
	@RealCourseFee Decimal(15,2),
	@TeacherID Int,
	@PayStatus Int,
	@CourseHoursStatusDesc NVarChar(Max)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Tr_ItemCourseHours] SET
		[TrainingItemCourseID] = @TrainingItemCourseID,
		[ClassRoomID] = @ClassRoomID,
		[CourseHoursStatusID] = @CourseHoursStatusID,
		[TrainingTimeDescID] = @TrainingTimeDescID,
		[TrainingDate] = @TrainingDate,
		[TrainingBeginTime] = @TrainingBeginTime,
		[TrainingEndTime] = @TrainingEndTime,
		[CourseFee] = @CourseFee,
		[CourseHours] = @CourseHours,
		[CourseHoursDesc] = @CourseHoursDesc,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[Remark] = @Remark,
		[DelFlag] = @DelFlag,
		[RealCourseHours] = @RealCourseHours,
		[RealCourseFee] = @RealCourseFee,
		[TeacherID] = @TeacherID,
		[PayStatus] = @PayStatus,
		[CourseHoursStatusDesc] = @CourseHoursStatusDesc
	WHERE [ItemCourseHoursID] = @ItemCourseHoursID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--ɾ����¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourseHours_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourseHours_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourseHours_Delete]
	@ItemCourseHoursID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Tr_ItemCourseHours]
	WHERE [ItemCourseHoursID] = @ItemCourseHoursID
	
	SET NOCOUNT OFF
END

GO

--����������ȡ������¼
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourseHours_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourseHours_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourseHours_GetByPk]
	@ItemCourseHoursID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[ItemCourseHoursID],
		[TrainingItemCourseID],
		[ClassRoomID],
		[CourseHoursStatusID],
		[TrainingTimeDescID],
		[TrainingDate],
		[TrainingBeginTime],
		[TrainingEndTime],
		[CourseFee],
		[CourseHours],
		[CourseHoursDesc],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[Remark],
		[DelFlag],
		[RealCourseHours],
		[RealCourseFee],
		[TeacherID],
		[PayStatus],
		[CourseHoursStatusDesc]
	FROM [dbo].[Tr_ItemCourseHours] 
	WHERE [ItemCourseHoursID] = @ItemCourseHoursID

	SET NOCOUNT OFF
END

GO

--��ȡ�б�(��ҳ������)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Tr_ItemCourseHours_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Tr_ItemCourseHours_GetPagedList]')
END

GO

--NOTE:����ʾ����ʵ����Ŀ������
--�����ʵ�ʲ�ѯ��Ҫ�������洢������
CREATE PROCEDURE [dbo].[Pr_Tr_ItemCourseHours_GetPagedList]
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
		[ItemCourseHoursID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [ItemCourseHoursID] FROM [dbo].[Tr_ItemCourseHours]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[ItemCourseHoursID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[ItemCourseHoursID],
		biz.[TrainingItemCourseID],
		biz.[ClassRoomID],
		biz.[CourseHoursStatusID],
		biz.[TrainingTimeDescID],
		biz.[TrainingDate],
		biz.[TrainingBeginTime],
		biz.[TrainingEndTime],
		biz.[CourseFee],
		biz.[CourseHours],
		biz.[CourseHoursDesc],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[Remark],
		biz.[DelFlag],
		biz.[RealCourseHours],
		biz.[RealCourseFee],
		biz.[TeacherID],
		biz.[PayStatus],
		biz.[CourseHoursStatusDesc]
	FROM [dbo].[Tr_ItemCourseHours] biz
	inner join #PageIndex p on  biz.[ItemCourseHoursID] = p.[ItemCourseHoursID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
