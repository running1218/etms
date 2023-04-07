
------------------------------------------------------------------------------------------------------------------------
--Table Res_Teacher
------------------------------------------------------------------------------------------------------------------------
--增加记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_Teacher_Insert')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_Teacher_Insert]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_Teacher_Insert]
	@TeacherID UniqueIdentifier,
	@TeacherSourceID Int,
	@UserID Int,
	@TeacherCode NVarChar(100),
	@TeacherName NVarChar(128),
	@OuterOrgID UniqueIdentifier,
	@TeacherStatus Int,
	@TeacherLevelID Int,
	@TeacherTypeID Int,
	@Sex Bit,
	@ClassReward Decimal(10,2),
	@Position NVarChar(60),
	@BirthDay DateTime,
	@PhotoURL NVarChar(256),
	@ServiceEnterprise NVarChar(max),
	@WorkExperience NVarChar(max),
	@Expertise NVarChar(max),
	@RepresentativeWorks NVarChar(max),
	@TeacherBrief NVarchar(max),
	@CertificateNo NVarChar(100),
	@OrgID Int,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@DelFlag Bit
AS
BEGIN
	SET NOCOUNT ON
	
	INSERT INTO [dbo].[Res_Teacher]
	(
		[TeacherID],
		[TeacherSourceID],
		[UserID],
		[TeacherCode],
		[TeacherName],
		[OuterOrgID],
		[TeacherStatus],
		[TeacherLevelID],
		[TeacherTypeID],
		[Sex],
		[ClassReward],
		[Position],
		[BirthDay],
		[PhotoURL],
		[ServiceEnterprise],
		[WorkExperience],
		[Expertise],
		[RepresentativeWorks],
		[TeacherBrief],
		[CertificateNo],
		[OrgID],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[DelFlag]
	)
	VALUES
	(
		@TeacherID,
		@TeacherSourceID,
		@UserID,
		@TeacherCode,
		@TeacherName,
		@OuterOrgID,
		@TeacherStatus,
		@TeacherLevelID,
		@TeacherTypeID,
		@Sex,
		@ClassReward,
		@Position,
		@BirthDay,
		@PhotoURL,
		@ServiceEnterprise,
		@WorkExperience,
		@Expertise,
		@RepresentativeWorks,
		@TeacherBrief,
		@CertificateNo,
		@OrgID,
		@CreateTime,
		@CreateUserID,
		@CreateUser,
		@ModifyTime,
		@ModifyUser,
		@DelFlag
	)
	
	
	
	SET NOCOUNT OFF
END

GO

--修改记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_Teacher_Update')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_Teacher_Update]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_Teacher_Update]
	@TeacherID UniqueIdentifier,
	@TeacherSourceID Int,
	@UserID Int,
	@TeacherCode NVarChar(100),
	@TeacherName NVarChar(128),
	@OuterOrgID UniqueIdentifier,
	@TeacherStatus Int,
	@TeacherLevelID Int,
	@TeacherTypeID Int,
	@Sex Bit,
	@ClassReward Decimal(10,2),
	@Position NVarChar(60),
	@BirthDay DateTime,
	@PhotoURL NVarChar(256),
	@ServiceEnterprise NVarChar(max),
	@WorkExperience NVarChar(max),
	@Expertise NVarChar(max),
	@RepresentativeWorks NVarChar(max),
	@TeacherBrief NVarchar(max),
	@CertificateNo NVarChar(100),
	@OrgID Int,
	@CreateTime DateTime,
	@CreateUserID Int,
	@CreateUser NVarChar(128),
	@ModifyTime DateTime,
	@ModifyUser NVarChar(128),
	@DelFlag Bit
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Res_Teacher] SET
		[TeacherSourceID] = @TeacherSourceID,
		[UserID] = @UserID,
		[TeacherCode] = @TeacherCode,
		[TeacherName] = @TeacherName,
		[OuterOrgID] = @OuterOrgID,
		[TeacherStatus] = @TeacherStatus,
		[TeacherLevelID] = @TeacherLevelID,
		[TeacherTypeID] = @TeacherTypeID,
		[Sex] = @Sex,
		[ClassReward] = @ClassReward,
		[Position] = @Position,
		[BirthDay] = @BirthDay,
		[PhotoURL] = @PhotoURL,
		[ServiceEnterprise] = @ServiceEnterprise,
		[WorkExperience] = @WorkExperience,
		[Expertise] = @Expertise,
		[RepresentativeWorks] = @RepresentativeWorks,
		[TeacherBrief] = @TeacherBrief,
		[CertificateNo] = @CertificateNo,
		[OrgID] = @OrgID,
		[CreateTime] = @CreateTime,
		[CreateUserID] = @CreateUserID,
		[CreateUser] = @CreateUser,
		[ModifyTime] = @ModifyTime,
		[ModifyUser] = @ModifyUser,
		[DelFlag] = @DelFlag
	WHERE [TeacherID] = @TeacherID

	IF @@ROWCOUNT = 0
	BEGIN
		RAISERROR('Concurrent update error. Updated aborted.', 16, 2)
	END    

	SET NOCOUNT OFF
END

GO

--删除记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_Teacher_Delete')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_Teacher_Delete]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_Teacher_Delete]
	@TeacherID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [dbo].[Res_Teacher]
	WHERE [TeacherID] = @TeacherID
	
	SET NOCOUNT OFF
END

GO

--根据主键获取单个记录
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_Teacher_GetByPk')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_Teacher_GetByPk]')
END

GO

CREATE PROCEDURE [dbo].[Pr_Res_Teacher_GetByPk]
	@TeacherID UniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT
		[TeacherID],
		[TeacherSourceID],
		[UserID],
		[TeacherCode],
		[TeacherName],
		[OuterOrgID],
		[TeacherStatus],
		[TeacherLevelID],
		[TeacherTypeID],
		[Sex],
		[ClassReward],
		[Position],
		[BirthDay],
		[PhotoURL],
		[ServiceEnterprise],
		[WorkExperience],
		[Expertise],
		[RepresentativeWorks],
		[TeacherBrief],
		[CertificateNo],
		[OrgID],
		[CreateTime],
		[CreateUserID],
		[CreateUser],
		[ModifyTime],
		[ModifyUser],
		[DelFlag]
	FROM [dbo].[Res_Teacher] 
	WHERE [TeacherID] = @TeacherID

	SET NOCOUNT OFF
END

GO

--获取列表(分页、排序)
IF EXISTS (SELECT NAME FROM sys.objects WHERE TYPE = 'P' AND NAME = 'Pr_Res_Teacher_GetPagedList')
BEGIN
	EXEC('DROP PROCEDURE [dbo].[Pr_Res_Teacher_GetPagedList]')
END

GO

--NOTE:仅供示例，实际项目不适用
--请根据实际查询需要重命名存储过程名
CREATE PROCEDURE [dbo].[Pr_Res_Teacher_GetPagedList]
	@PageIndex int, --页号
	@PageSize int, --页面大小	
	@SortExpression varchar(1000) = '', --排序字段
	@Criteria varchar(1000) = '' --以AND开头的查询条件
AS
BEGIN
	--@Criteria参数：请根据实际查询需要进行增、删
	--@SortExpression参数：请根据实际查询需要进行默认值设定

	SET NOCOUNT ON
	
	DECLARE @SqlGet varchar(1600)
	DECLARE @TotalRecords int
	
	--创建表变量，存储数据主键
	CREATE TABLE #PageIndex
	(
		[IndexId] int IDENTITY (1, 1) NOT NULL,
		[TeacherID] UniqueIdentifier
	)
	
	IF @SortExpression IS NULL
		SET @SortExpression = ''
	
	IF @Criteria IS NULL
		SET @Criteria = ''
	
	IF @SortExpression <> ''
		SET @SortExpression = ' ORDER BY ' + @SortExpression
	
	SET @SqlGet = 'SELECT [TeacherID] FROM [dbo].[Res_Teacher]  WHERE 1=1 ' + @Criteria + @SortExpression
	
	INSERT INTO #PageIndex
	(
		[TeacherID]
	) EXEC (@SqlGet)
	
	SET @TotalRecords = @@ROWCOUNT
	
	DECLARE @StartRowIndex int
	SET @StartRowIndex=(@PageIndex-1)*@PageSize+1

	SELECT 
		biz.[TeacherID],
		biz.[TeacherSourceID],
		biz.[UserID],
		biz.[TeacherCode],
		biz.[TeacherName],
		biz.[OuterOrgID],
		biz.[TeacherStatus],
		biz.[TeacherLevelID],
		biz.[TeacherTypeID],
		biz.[Sex],
		biz.[ClassReward],
		biz.[Position],
		biz.[BirthDay],
		biz.[PhotoURL],
		biz.[ServiceEnterprise],
		biz.[WorkExperience],
		biz.[Expertise],
		biz.[RepresentativeWorks],
		biz.[TeacherBrief],
		biz.[CertificateNo],
		biz.[OrgID],
		biz.[CreateTime],
		biz.[CreateUserID],
		biz.[CreateUser],
		biz.[ModifyTime],
		biz.[ModifyUser],
		biz.[DelFlag]
	FROM [dbo].[Res_Teacher] biz
	inner join #PageIndex p on  biz.[TeacherID] = p.[TeacherID] 
	WHERE 
		p.IndexId >= @StartRowIndex AND
		p.IndexId < @StartRowIndex + @PageSize
	ORDER BY p.IndexId
	
	SET NOCOUNT OFF
	
	RETURN @TotalRecords
END

GO 
