go

--exec [Pr_Import_Detail_Student_Valid] 47
CREATE proc [dbo].[Pr_Import_Detail_Student_Valid]
 @TaskID int
as
Begin
  --业务验证规则
  update  Import_Detail_Student set 
   [Remark]=(result.LoginNameInfo+result.RealNameInfo+result.WorkerNoInfo+result.DepartmentNameInfo+result.RankNameInfo+result.PostNameInfo)
  ,[status]=case when len(result.LoginNameInfo+result.RealNameInfo+result.WorkerNoInfo+result.DepartmentNameInfo+result.RankNameInfo+result.PostNameInfo)=0 then 1 else 0 end
  from (
	 select a.DetailID,
		   case when LEN(a.LoginName)=0 then '学员账号为空;' when exists(select 1 from Site_User where Site_User.LoginName=a.loginName) then '账号已存在;' WHEN exists(select LoginName from Import_Detail_Student where TaskID=@TaskID AND LoginName=a.LoginName GROUP by LoginName having COUNT(*)>1 ) then 'Excel中账号重复;' else '' end as LoginNameInfo
		  ,case when LEN(a.RealName)=0 then '学员姓名为空;' else '' end as RealNameInfo
		  /*屏蔽工号唯一约束*/      
		  ,'' as WorkerNoInfo --,case when LEN(a.WorkerNo)=0 then '工号为空;' when exists(select 1 from Site_Student where Site_Student.WorkerNo=a.WorkerNo) then '工号已存在;' WHEN exists(select WorkerNo from Import_Detail_Student where TaskID=@TaskID AND WorkerNo=a.WorkerNo GROUP BY WorkerNo having COUNT(*)>1 ) then 'Excel中工号重复;' else '' end as WorkerNoInfo
		  ,case when LEN(a.DepartmentName)=0 then '部门为空;' when not exists(select 1 from Site_Department where Site_Department.DisplayPath=a.DepartmentName) then '部门不存在;' else '' end as DepartmentNameInfo
		  ,case when LEN(a.RankName)=0 then '职级为空;' when not exists(select 1 from vw_Dic_Sys_Rank where vw_Dic_Sys_Rank.RankName=a.RankName) then '职级不存在;' else '' end as RankNameInfo
		  ,case when LEN(a.PostName)=0 then '岗位为空;' when not exists(select 1 from vw_Post where vw_Post.PostName=a.PostName) then '岗位不存在;' else '' end as PostNameInfo
		  from Import_Detail_Student a
	  where [TaskID]=@TaskID
	  ) result 
	 where Import_Detail_Student.DetailID=result.DetailID  and  [TaskID]=@TaskID 
  --输出验证结果
  declare @TotalRecords int
  select @TotalRecords=COUNT(*) from Import_Detail_Student where [TaskID]=@TaskID and [status]=0
  return @TotalRecords
End


GO

  
create proc [dbo].[Pr_Import_Detail_Student_Import]  
 @TaskID int,  
 @DefaultPassWord varchar(50)  
as  
Begin   
   --插入到基本用户表  
   insert into Site_User(LoginName,RealName,[PassWord],Email,MobilePhone, OrganizationID, DepartmentID,[Status],[Creator],[Modifier],[Identity],[Birthday],[SexTypeID],[OfficeTelphone],[TitleName])
   select  a.LoginName  
   ,A.RealName  
   ,@DefaultPassWord as [Password]  
   ,a.Email  
   ,A.mobile  
   ,b.OrganizationID  
   ,(select DepartmentID from Site_Department where Site_Department.DisplayPath=a.DepartmentName and Site_Department.OrganizationID=b.OrganizationID) as DepartmentID   
   ,1 --启用状态  
   ,c.LoginName --创建人  
   ,c.LoginName --修改人 
   ,a.[Identity]
   ,a.[Birthday]
   ,a.[SexTypeID]
   ,a.[OfficeTelphone] 
   ,a.[TitleName]
   from Import_Detail_Student a   
    inner join Import_Task b on a.TaskID=b.TaskID  
    inner join Site_User c on b.CreatorID=c.UserID  
   where a.TaskID=@TaskID  
   
   --插入学员扩展表  
   insert into site_student(userid,WorkerNo,RankID,PostID, Specialty,Superior,LastEducation, JoinTime)  
   select   
      b.UserID  
     ,a.WorkerNo   
     ,(select RankID from vw_Dic_Sys_Rank where vw_Dic_Sys_Rank.RankName=a.RankName) as RankID  
     ,(select PostID from vw_Post where vw_Post.PostName=a.PostName and vw_Post.OrganizationID=c.OrganizationID) as PostID  
     ,a.Specialty
     ,a.Superior
     ,a.LastEducation
     ,a.JoinTime
   from Import_Detail_Student a   
   inner join Site_User b on a.LoginName=b.LoginName     
   inner join Import_Task c on c.TaskID=a.TaskID  
   where a.TaskID=@TaskID  
     
   return @@ROWCOUNT     
End