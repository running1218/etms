﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETMS.WebApp.Manage.Resource.ProfessorManage
{
    public partial class ProfessorAddInner : ETMS.Controls.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            aBack.HRef = "ProfessorListInner.aspx";
        }
    }
}