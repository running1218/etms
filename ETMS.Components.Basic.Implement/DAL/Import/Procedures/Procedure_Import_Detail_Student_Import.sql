go

--exec [Pr_Import_Detail_Student_Valid] 47
CREATE proc [dbo].[Pr_Import_Detail_Student_Valid]
 @TaskID int
as
Begin
  --ҵ����֤����
  update  Import_Detail_Student set 
   [Remark]=(result.LoginNameInfo+result.RealNameInfo+result.WorkerNoInfo+result.DepartmentNameInfo+result.RankNameInfo+result.PostNameInfo)
  ,[status]=case when len(result.LoginNameInfo+result.RealNameInfo+result.WorkerNoInfo+result.DepartmentNameInfo+result.RankNameInfo+result.PostNameInfo)=0 then 1 else 0 end
  from (
	 select a.DetailID,
		   case when LEN(a.LoginName)=0 then 'ѧԱ�˺�Ϊ��;' when exists(select 1 from Site_User where Site_User.LoginName=a.loginName) then '�˺��Ѵ���;' WHEN exists(select LoginName from Import_Detail_Student where TaskID=@TaskID AND LoginName=a.LoginName GROUP by LoginName having COUNT(*)>1 ) then 'Excel���˺��ظ�;' else '' end as LoginNameInfo
		  ,case when LEN(a.RealName)=0 then 'ѧԱ����Ϊ��;' else '' end as RealNameInfo
		  /*���ι���ΨһԼ��*/      
		  ,'' as WorkerNoInfo --,case when LEN(a.WorkerNo)=0 then '����Ϊ��;' when exists(select 1 from Site_Student where Site_Student.WorkerNo=a.WorkerNo) then '�����Ѵ���;' WHEN exists(select WorkerNo from Import_Detail_Student where TaskID=@TaskID AND WorkerNo=a.WorkerNo GROUP BY WorkerNo having COUNT(*)>1 ) then 'Excel�й����ظ�;' else '' end as WorkerNoInfo
		  ,case when LEN(a.DepartmentName)=0 then '����Ϊ��;' when not exists(select 1 from Site_Department where Site_Department.DisplayPath=a.DepartmentName) then '���Ų�����;' else '' end as DepartmentNameInfo
		  ,case when LEN(a.RankName)=0 then 'ְ��Ϊ��;' when not exists(select 1 from vw_Dic_Sys_Rank where vw_Dic_Sys_Rank.RankName=a.RankName) then 'ְ��������;' else '' end as RankNameInfo
		  ,case when LEN(a.PostName)=0 then '��λΪ��;' when not exists(select 1 from vw_Post where vw_Post.PostName=a.PostName) then '��λ������;' else '' end as PostNameInfo
		  from Import_Detail_Student a
	  where [TaskID]=@TaskID
	  ) result 
	 where Import_Detail_Student.DetailID=result.DetailID  and  [TaskID]=@TaskID 
  --�����֤���
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
   --���뵽�����û���  
   insert into Site_User(LoginName,RealName,[PassWord],Email,MobilePhone, OrganizationID, DepartmentID,[Status],[Creator],[Modifier],[Identity],[Birthday],[SexTypeID],[OfficeTelphone],[TitleName])
   select  a.LoginName  
   ,A.RealName  
   ,@DefaultPassWord as [Password]  
   ,a.Email  
   ,A.mobile  
   ,b.OrganizationID  
   ,(select DepartmentID from Site_Department where Site_Department.DisplayPath=a.DepartmentName and Site_Department.OrganizationID=b.OrganizationID) as DepartmentID   
   ,1 --����״̬  
   ,c.LoginName --������  
   ,c.LoginName --�޸��� 
   ,a.[Identity]
   ,a.[Birthday]
   ,a.[SexTypeID]
   ,a.[OfficeTelphone] 
   ,a.[TitleName]
   from Import_Detail_Student a   
    inner join Import_Task b on a.TaskID=b.TaskID  
    inner join Site_User c on b.CreatorID=c.UserID  
   where a.TaskID=@TaskID  
   
   --����ѧԱ��չ��  
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