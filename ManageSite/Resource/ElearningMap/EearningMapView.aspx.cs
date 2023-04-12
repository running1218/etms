using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;

public partial class Resource_ElearningMap_EearningMapView : System.Web.UI.Page
{  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ElearningMapInfoView1.StudyMapID = new Guid(Request.QueryString["StudyMapID"]);
            MapCourseList1.StudyMapID = new Guid(Request.QueryString["StudyMapID"]);
    
        }
        
    }

}