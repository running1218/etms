﻿using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Utility;
using System;
using System.Data;
using University.Mooc.AppContext;

namespace ETMS.Studying.Study
{
    public partial class StudyCertificate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                //Response.Write(((ETMS.Studying.Master.Study)this.Master).StudyProgress+"-----");
                //Response.End();
                Guid TrainingItemCourseID = Request.QueryString["TrainingItemCourseID"].ToGuid();
                decimal progress = ((ETMS.Studying.Master.Study)this.Master).GetCourseProgressPercentage(TrainingItemCourseID);
                if (progress == 100)
                {
                    Bind();
                    CertificateDiv.Style.Add("display", "");
                    AlterDiv.Style.Add("display", "none");
                }
                else {
                    CertificateDiv.Style.Add("display", "none");
                    AlterDiv.Style.Add("display", "");
                }
            }
        }

        private void Bind() {
            Guid TrainingItemCourseID = Request.QueryString["TrainingItemCourseID"].ToGuid();
            Logo.ImageUrl = BaseUtility.LogoImage;
            CourseName.Text = "";
            Tr_ItemCourseLogic Logic = new Tr_ItemCourseLogic();
            DataTable dt = Logic.GetItemCourseListByTrainingItemCourseID(TrainingItemCourseID);
            if (dt.Rows.Count == 1)
            {
                CourseName.Text = dt.Rows[0]["CourseName"].ToString();
                StartDate.Text = Convert.ToDateTime(dt.Rows[0]["CourseBeginTime"]).ToString("yyyy年MM月dd日");
                EndDate.Text = Convert.ToDateTime(dt.Rows[0]["CourseEndTime"]).ToString("yyyy年MM月dd日");
                SmallCouresName.Text = dt.Rows[0]["CourseName"].ToString();
                FinalDate.Text = Convert.ToDateTime(dt.Rows[0]["CourseEndTime"]).ToString("yyyy年MM月dd日");
            }
            StudyUserName.Text = UserContext.Current.RealName;
            OrganizationName.Text = SiteOrganizationName();
        }

        private string SiteOrganizationName() {
            //BaseUtility.SiteOrganizationID
            var entity = new ETMS.Components.Basic.Implement.BLL.Security.OrganizationLogic().GetNodeByID(BaseUtility.SiteOrganizationID);
            return entity.NodeName;
        }
    }

}