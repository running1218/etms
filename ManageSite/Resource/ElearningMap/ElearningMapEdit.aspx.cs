﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETMS.WebApp.Manage.Resource.ElearningMap
{
    public partial class ElearningMapEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ElearningMapInfo1.Action = ETMS.AppContext.OperationAction.Edit;
            ElearningMapInfo1.StudyMapID = new Guid(Request.QueryString["StudyMapID"]);
        }
    }
}