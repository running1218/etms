﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuestionDB_ExRandomPractice_ExerciseListRead : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ExerciseList1.ExerciseType = ETMS.Components.Basic.Implement.BLL.BizExerciseType.ExRandomPractice;
        ExerciseList1.isReadOnly = true;
    }
}