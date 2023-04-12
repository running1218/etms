using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

using ETMS.Controls;
using ETMS.Components.QS.API.Entity;
using ETMS.Components.QS.Implement.BLL;

public partial class QS_QueryPreView : BasePage
{
    //调查问卷
    QS_QueryLogic logicQuery = new QS_QueryLogic();
    QS_QueryTitleLogic logicQueryTitle = new QS_QueryTitleLogic();
    QS_QueryTitleOptionLogic logicQueryTitleOption = new QS_QueryTitleOptionLogic();

    //调查结果
    QS_QueryResultLogic logicQueryResult = new QS_QueryResultLogic();
    QS_QueryResultAnswerLogic logicQueryResultAnswer = new QS_QueryResultAnswerLogic();
    QS_QueryResultOptionLogic logicQueryResultOption = new QS_QueryResultOptionLogic();

    protected static Guid QueryID ;

    protected void Page_Load(object sender, EventArgs e)
    {

        //获取时间html片段
        //ltContent.Text = PollManager.GetResponseViewPreView(int.Parse(Request.QueryString["QueryID"])).ToString();

        //if (!this.Page.IsPostBack)
        {
            if (Request.QueryString["QueryID"] != null)
            {
                string guid = Request.QueryString["QueryID"];
                if ((guid != "") && (guid != null))
                {
                    QueryID = new Guid(guid);
                    ShowQuery(QueryID);
                }
            }
        }

    }



    /// <summary>
    /// 显示调查问卷
    /// </summary>
    /// <param name="queryID"></param>
    private void ShowQuery(Guid queryID)
    {
        ShowQueryAnswer(-1, queryID);
    }



    protected void  radioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList radioButtonList = (RadioButtonList)sender;
        if (radioButtonList.SelectedValue == "1")
        {
            ListItem item = radioButtonList.SelectedItem;
            item.Enabled = false;
        }
    }


    /// <summary>
    /// 显示某个用户的问卷调查结果
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="queryID"></param>
    private void ShowQueryAnswer(int userID, Guid queryID )
    {
        //string otherTextModal = "<input id=\"{0}\" type=\"text\" value=\"{1}\" />";
        tableQueryTitle.Rows.Clear();

        QS_Query entityQuery = logicQuery.GetById(queryID);
        if (entityQuery == null)
        {
            return;
        }

        //取界面的作答结果
        Hashtable hashTableResultAnswer = new Hashtable();//问答题
        Hashtable hashTableResultOption = new Hashtable();//选择题
        if (userID >= 0)
        {
            //查询该用户的当前问卷调查结果(返回三个表.依次是:用户作答结果表QS_QueryResult,问答题作答结果表QS_QueryResultAnswer,选择题作答结果表QS_QueryResultOption)
            DataSet ds = logicQueryResult.GetResultByUserIDQueryID(userID, queryID);
            if (ds.Tables.Count >= 3)
            {
                foreach (DataRow dataRowAnswer in ds.Tables[1].Rows)
                {
                    string titleID = dataRowAnswer["TitleID"].ToString();
                    string answer = dataRowAnswer["Answer"].ToString();
                    if (hashTableResultAnswer[titleID] == null)
                        hashTableResultAnswer.Add(titleID, answer);
                }

                foreach (DataRow dataRowOption in ds.Tables[2].Rows)
                {
                    string optionID = dataRowOption["OptionID"].ToString();
                    string otherText = dataRowOption["OtherText"].ToString();
                    if (hashTableResultOption[optionID] == null)
                        hashTableResultOption.Add(optionID, otherText);
                }
            }
        }

        //问卷标题
        lblQueryTitle.Text = entityQuery.QueryName;
        //问卷调查时间范围
        string timeModal = "调查开始时间:{0},调查结束时间:{1}";
        string beginTime = "无";
        string endTime = "无";
        if (entityQuery.BeginTime != null)
        {
            beginTime = entityQuery.BeginTime.ToString("yyyy-MM-dd");
        }
        if (entityQuery.EndTime != null)
        {
            endTime = entityQuery.EndTime.ToString("yyyy-MM-dd");
        }

        lblQueryTime.Text = string.Format(timeModal, beginTime, endTime);

        TableRow tr;
        TableCell tcLabel;

        System.Web.UI.WebControls.TextBox textBoxOther = new TextBox();//其他输入项

        //获取调查问卷的标题
        DataTable dtQueryTitle = logicQueryTitle.GetQueryTitleAllInfoByQueryID(entityQuery.QueryID);
        for (int row = 0; row < dtQueryTitle.Rows.Count; row++)
        {
            string queryTitleID = dtQueryTitle.Rows[row]["TitleID"].ToString().Trim();
            if (queryTitleID == "")
                continue;

            //加题目
            QS_QueryTitle entityTitle = logicQueryTitle.GetById(new Guid(queryTitleID));
            System.Web.UI.WebControls.Label labelTitle = new Label();
            labelTitle.ID = entityTitle.TitleID.ToString();
            labelTitle.Text = "<b>" + (row + 1).ToString() + "." + entityTitle.TitleName + "<b/>";//粗体显示

            tr = new TableRow();
            tcLabel = new TableCell();
            tcLabel.Wrap = true; //是否允许换行
            tcLabel.HorizontalAlign = HorizontalAlign.Left;//对齐方式
            tcLabel.Controls.Add(labelTitle);
            tr.Cells.Add(tcLabel);


            //取选项
            DataTable dtOption = logicQueryTitleOption.GetQueryTitleOptionAllInfoByTitle(entityTitle.TitleID);
            //判断题型
            switch (entityTitle.TitleTypeID)
            {
                case 1://单选
                    textBoxOther = new TextBox();
                    textBoxOther.ID = "0";//用来做是否有"其他"选项的判断
                    System.Web.UI.WebControls.RadioButtonList radioButtonList = new RadioButtonList();
                    foreach (DataRow dataRow in dtOption.Rows)
                    {
                        int OtherLength = int.Parse(dataRow["OtherLength"].ToString());
                        ListItem item = new ListItem();
                        item.Value = dataRow["OptionID"].ToString();
                        item.Text = dataRow["OptionName"].ToString();
                        //显示作答结果
                        if (hashTableResultOption[item.Value] != null)
                            item.Selected = true;
                        if (OtherLength > 0)
                        {
                            //加其他输入项
                            textBoxOther.ID = "otherTxt" + item.Value; ;//应该加前缀
                            textBoxOther.Width = Unit.Percentage(50);
                            //显示作答结果
                            if (hashTableResultOption[item.Value] != null)
                                textBoxOther.Text = hashTableResultOption[item.Value].ToString();
                        }
                        radioButtonList.Items.Add(item);
                    }

                    tcLabel.Controls.Add(radioButtonList);
                    //加"其他"选择项的录入框到表格中
                    if (textBoxOther.ID != "0")
                    {
                        tcLabel.Controls.Add(textBoxOther);
                    }
                    tr.Cells.Add(tcLabel);
                    //加入到表格中
                    tableQueryTitle.Rows.Add(tr);
                    break;
                case 2://多选
                    textBoxOther = new TextBox();
                    textBoxOther.ID = "0";
                    System.Web.UI.WebControls.CheckBoxList checkBoxList = new CheckBoxList();
                    //加事件
                    checkBoxList.SelectedIndexChanged += new EventHandler(radioButtonList_SelectedIndexChanged);
                    foreach (DataRow dataRow in dtOption.Rows)
                    {
                        int OtherLength = int.Parse(dataRow["OtherLength"].ToString());
                        ListItem item = new ListItem();
                        item.Value = dataRow["OptionID"].ToString();
                        item.Text = dataRow["OptionName"].ToString();
                        //显示作答结果
                        if (hashTableResultOption[item.Value] != null)
                            item.Selected = true;
                        checkBoxList.Items.Add(item);
                        if (OtherLength > 0)
                        {
                            //加其他输入项
                            textBoxOther.ID = "otherTxt" + item.Value; ;//应该加前缀
                            textBoxOther.Width = Unit.Percentage(50);
                            //显示作答结果
                            if (hashTableResultOption[item.Value] != null)
                                textBoxOther.Text = hashTableResultOption[item.Value].ToString();
                        }
                    }

                    tcLabel.Controls.Add(checkBoxList);
                    if (textBoxOther.ID != "0")
                    {
                        tcLabel.Controls.Add(textBoxOther);
                    }
                    tr.Cells.Add(tcLabel);
                    //加入到表格中
                    tableQueryTitle.Rows.Add(tr);
                    break;
                case 3://矩阵（暂不支持）
                    break;
                case 4://简答
                    System.Web.UI.WebControls.TextBox textBox = new TextBox();
                    textBox.ID = "txt" + entityTitle.TitleID.ToString(); //应该加前缀
                    textBox.TextMode = TextBoxMode.MultiLine;
                    textBox.MaxLength = 1024;
                    textBox.Rows = 6;
                    textBox.Width = Unit.Percentage(85);
                    //显示作答结果
                    if (hashTableResultAnswer[entityTitle.TitleID] != null)
                        textBox.Text = hashTableResultAnswer[entityTitle.TitleID].ToString();
                    //加换行
                    Literal br = new Literal();
                    br.Text = "<br/>";
                    tcLabel.Controls.Add(br);
                    tcLabel.Controls.Add(textBox);
                    tr.Cells.Add(tcLabel);
                    //加入到表格中
                    tableQueryTitle.Rows.Add(tr);
                    break;
            }
        }
    }





    /// <summary>
    /// 查看结果
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnResult_Click(object sender, EventArgs e)
    {
        ShowQueryAnswer(ETMS.AppContext.UserContext.Current.UserID, QueryID);
    }


    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (QueryID == null)
        {
            return;
        }

        //取界面录入的数据
        Hashtable hashTableInputAnswer = new Hashtable();
        Hashtable hashTableSelectOption = new Hashtable();
        for (int i = 0; i < tableQueryTitle.Rows.Count; i++)
        {
            TableRow tr = tableQueryTitle.Rows[i];
            for (int j = 0; j < tr.Cells.Count; j++)
            {
                TableCell tc = tr.Cells[j];
                for (int id = 0; id < tc.Controls.Count; id++)
                {
                    string controlType = tc.Controls[id].GetType().Name;
                    switch (controlType)
                    {
                        case "TextBox"://文本框查包含
                            //取内容
                            TextBox textBox = (TextBox)tc.Controls[id];
                            string txtBoxID = textBox.ID;
                            if (txtBoxID.StartsWith("txt"))
                            {
                                string answerID = txtBoxID.Substring(3);
                                if (hashTableInputAnswer[answerID] == null)
                                {
                                    hashTableInputAnswer.Add(answerID, textBox.Text);
                                }
                            }
                            if (txtBoxID.StartsWith("otherTxt"))
                            {
                                string fieldName = txtBoxID.Substring(8);
                                string fieldValue = textBox.Text;
                                if (hashTableSelectOption[fieldName] == null)
                                    hashTableSelectOption.Add(fieldName, fieldValue);
                                else
                                {
                                    //修改,原来在选项的选择时已经保存过
                                    hashTableSelectOption[fieldName] = fieldValue;
                                }

                            }
                            break;
                        case "CheckBoxList":
                            //取内容
                            CheckBoxList checkBoxList = (CheckBoxList)tc.Controls[id];
                            foreach (ListItem item in checkBoxList.Items)
                            {
                                if (item.Selected)
                                {
                                    string selectID = item.Value.ToString();
                                    if (hashTableSelectOption[selectID] == null)
                                        hashTableSelectOption.Add(selectID, selectID);
                                }
                            }
                            break;
                        case "RadioButtonList":
                            //取内容
                            RadioButtonList radioButtonList = (RadioButtonList)tc.Controls[id];
                            foreach (ListItem item in radioButtonList.Items)
                            {
                                if (item.Selected)
                                {
                                    string selectID = item.Value.ToString();
                                    if (hashTableSelectOption[selectID] == null)
                                        hashTableSelectOption.Add(selectID, selectID);
                                }
                            }
                            break;
                    }
                }
            }

        }
        //先保存用户调查批次
        QS_QueryResult entityQueryResult = new QS_QueryResult();
        entityQueryResult.BatchID = Guid.NewGuid();
        entityQueryResult.QueryID = QueryID;
        entityQueryResult.UserID = ETMS.AppContext.UserContext.Current.UserID;
        entityQueryResult.UserName = ETMS.AppContext.UserContext.Current.UserName;
        entityQueryResult.CreateTime = DateTime.Now;
        //entityQueryResult.AswerIP//获取IP方法
        logicQueryResult.Add(entityQueryResult);

        //再保存问答题到数据库
        System.Collections.IDictionaryEnumerator myEnumerator = hashTableInputAnswer.GetEnumerator();
        while (myEnumerator.MoveNext())
        {
            QS_QueryResultAnswer entityQueryResultAnswer = new QS_QueryResultAnswer();
            entityQueryResultAnswer.BatchID = entityQueryResult.BatchID;
            entityQueryResultAnswer.AnswerResultID = Guid.NewGuid();
            entityQueryResultAnswer.TitleID = new Guid(myEnumerator.Key.ToString());
            entityQueryResultAnswer.Answer = myEnumerator.Value.ToString();
            logicQueryResultAnswer.Add(entityQueryResultAnswer);

        }

        string selectOption = "";//用户选择的选择题选项ID数组
        //最后保存选择题到数据库
        myEnumerator = hashTableSelectOption.GetEnumerator();
        while (myEnumerator.MoveNext())
        {
            selectOption += myEnumerator.Key.ToString() + ",";
            QS_QueryResultOption entityQueryResultOption = new QS_QueryResultOption();
            entityQueryResultOption.BatchID = entityQueryResult.BatchID;
            entityQueryResultOption.QueryResultID = Guid.NewGuid();
            entityQueryResultOption.OptionID = new Guid(myEnumerator.Key.ToString());
            entityQueryResultOption.OtherText = myEnumerator.Value.ToString();
            logicQueryResultOption.Add(entityQueryResultOption);

        }
    }




}