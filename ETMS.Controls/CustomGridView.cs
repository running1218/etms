
namespace ETMS.Controls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Data;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    /// �Զ����ҳ�ؼ�
    /// ˵�����˿ؼ��̳���GridView����ˣ��߱�GridView�����г��ֹ��ܣ�
    /// ���Զ�������ԣ�
    ///     EmptyDataSourceTip
    ///     FirstPageImageUrl
    ///     PrePageImageUrl
    ///     NextPageImageUrl
    ///     LastPageImageUrl
    ///     GoToPageImageUrl
    /* ʹ�÷�����
    protected void Page_Load(object sender, EventArgs e)
    {       
	    //�����Ҫ��ҳ����������Ϊtrue
        this.customGridViewExample.CustomAllowPaging = false;
	    //ȡ���Զ������й���
        this.customGridViewExample.AutoGenerateColumns = false;
	    //�����ѯ������Ϊ��ʱ��Ĭ���ṩ�Ŀ��������ͣ��ؼ��Զ����մ���������ģ��һ�������ݡ�
        this.customGridViewExample.EmptyDataType = typeof(System.Data.DataView)
	    //�����Զ����ҳ�ռ�ĳ�ʼ��������ÿ��Page_Load�¼�����Ҫִ�У���
        this.customGridViewExample.Initialization(new IPageDataSource(delegate(int pageIndex, int pageSize, out int totalRecordCount)
        {
            //�����Ҫ��ҳ�Ļ��������ô�ֵ��
            totalRecordCount = 0;
            //�ڴ˵����������ҵ�񷽷���ǰ����
            return new Object[]{new object()};
        }), null);

	    //�ɸ���ʵ����������Ƿ���ҳ������ʱ������
        if (!Page.IsPostBack)
        {
            this.customGridViewExample.CustomDataBind();
        }
    }
    */
    /// </summary>
    [SupportsEventValidation, Designer("System.Web.UI.Design.WebControls.GridViewDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultEvent("SelectedIndexChanged"), ControlValueProperty("SelectedValue")]
    public class CustomGridView : GridView
    {
        /// <summary>
        ///  Ĭ��ȡ����ҳ,����ҳ�ţ��Զ����ҳ��
        /// </summary>
        public CustomGridView()
        {
            if (DesignMode)
                return;

            this.AllowPaging = false;
            this.ShowFooter = false;
        }

        #region �¼�����
        void Page_LoadComplete(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            if (!string.IsNullOrEmpty(this.Page.Request.Form[this.RequestPageClientID]))
            {
                //����з�ҳ��������з�ҳ������
                int newPageIndex = Convert.ToInt32(this.Page.Request.Form[this.RequestPageClientID]);
                newPageIndex = newPageIndex >= this.CurrentPageCount ? this.CurrentPageCount : newPageIndex;
                //����ҳ������
                this.CurrentPageIndex = newPageIndex;
                this.OnPageIndexChanging(new GridViewPageEventArgs(newPageIndex));
            }
        }
        #endregion

        #region ˽�г�Ա
        private string ScriptFormatString
        {
            get
            {
                return string.Format("javascript:document.getElementById(\"{0}\").value={1};document.forms[\"{2}\"].submit();", this.RequestPageClientID, "{0}", this.Page.Form.ClientID);
            }
        }
        private string PageFormatString = @"<a href='{0}'><img border='0' src='{1}' /></a>";

        private bool IsCallComstomDataBoundFunction = false;

        private string RequestPageClientID
        {
            get
            {
                return string.Format("{0}_CustomGridView_RequestPageClientID", this.ID);
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��ǰ��¼���Ƿ�Ϊ�գ���������GridView_DataRowBound�¼�����
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (ViewState["IsEmpty"] == null)
                    ViewState["IsEmpty"] = false;
                return (bool)ViewState["IsEmpty"];
            }
            set
            {
                ViewState["IsEmpty"] = value;
            }
        }

        private IList m_EmptyData;
        /// <summary>
        /// ������ʱ�ṩ�İ�ʵ��
        /// ע�⣺EmptyData����ʵ��System.Collections.ICollection�ӿڣ�
        /// </summary>
        public IList EmptyData
        {
            get
            {
                return m_EmptyData;
            }
            set
            {
                m_EmptyData = value;
            }
        }
        private Type m_EmptyDataType = typeof(DataView);//Ĭ�ϰ�����ԴΪDataView
        /// <summary>
        /// ������������
        /// </summary>
        public Type EmptyDataType
        {
            get
            {
                return m_EmptyDataType;
            }
            set
            {
                if (value.IsAbstract)
                {
                    throw new Exception(string.Format("{0}�������ͣ�������ʵ��������ͨ������EmptyData����ɿ�������䣡", value.ToString()));
                }
                bool isFindDefaultConstructorInfo = false;
                foreach (System.Reflection.ConstructorInfo constructorInfo in value.GetConstructors(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (constructorInfo.GetParameters().Length == 0)
                    {
                        isFindDefaultConstructorInfo = true;
                        break;
                    }
                }
                if (!isFindDefaultConstructorInfo)
                {
                    throw new Exception(string.Format("{0}��û�й�����Ĭ�Ϲ��캯����", value.ToString()));
                }
                m_EmptyDataType = value;
            }
        }

        /// <summary>
        /// ������Դ��ʾ��Ϣ
        /// Ĭ�ϣ���û���κμ�¼��"
        /// </summary>
        public string EmptyDataSourceTip
        {
            get
            {
                if (ViewState["EmptyDataSourceTip"] == null)
                    ViewState["EmptyDataSourceTip"] = "û���κμ�¼��";
                return (string)ViewState["EmptyDataSourceTip"];
            }
            set
            {
                ViewState["EmptyDataSourceTip"] = value;
            }
        }
        /// <summary>
        /// ��ҳͼƬ·��
        /// </summary>
        public string FirstPageImageUrl
        {
            get
            {
                return (string)ViewState["FirstPageImageUrl"];
            }
            set
            {
                ViewState["FirstPageImageUrl"] = value;
            }
        }
        /// <summary>
        /// ǰҳ��ͼƬ·��
        /// </summary>
        public string PrePageImageUrl
        {
            get
            {
                return (string)ViewState["PrePageImageUrl"];
            }
            set
            {
                ViewState["PrePageImageUrl"] = value;
            }
        }
        /// <summary>
        /// ��ҳ��ͼƬ·��
        /// </summary>
        public string NextPageImageUrl
        {
            get
            {
                return (string)ViewState["NextPageImageUrl"];
            }
            set
            {
                ViewState["NextPageImageUrl"] = value;
            }
        }
        /// <summary>
        /// βҳ��ͼƬ·��
        /// </summary>
        public string LastPageImageUrl
        {
            get
            {
                return (string)ViewState["LastPageImageUrl"];
            }
            set
            {
                ViewState["LastPageImageUrl"] = value;
            }
        }
        /// <summary>
        /// GOͼƬ·��
        /// </summary>
        public string GoToPageImageUrl
        {
            get
            {
                return (string)ViewState["GoToPageImageUrl"];
            }
            set
            {
                ViewState["GoToPageImageUrl"] = value;
            }
        }
        /// <summary>
        /// ҳ��
        /// </summary>
        public int CurrentPageCount
        {
            get
            {
                if ((this.TotalRecordCount / this.PageSize) > 0)//�����¼����ÿҳ������
                {
                    if ((TotalRecordCount % PageSize) == 0)
                        return TotalRecordCount / PageSize;
                    else
                    {
                        return TotalRecordCount / PageSize + 1;
                    }
                }
                else��//����һҳ
                    return 1;
            }
        }
        /// <summary>
        /// ��ǰҳ������
        /// Ĭ�Ϸ��ص�һҳ
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                if (ViewState["CurrentPageIndex"] == null)
                {
                    return 1;
                }
                return (int)ViewState["CurrentPageIndex"];
            }
            set
            {
                ViewState["CurrentPageIndex"] = value;
            }
        }
        /// <summary>
        /// �Ƿ������ҳ
        /// ע�������˱����AllowPaging����
        /// </summary>
        public bool CustomAllowPaging
        {
            get
            {
                if (ViewState["CustomAllowPaging"] == null)
                {
                    return false;
                }
                return (bool)ViewState["CustomAllowPaging"];
            }
            set
            {
                ViewState["CustomAllowPaging"] = value;
                this.ShowFooter = value;
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public int TotalRecordCount
        {
            get
            {
                if (ViewState["TotalRecordCount"] == null)
                {
                    return 0;
                }
                return (int)ViewState["TotalRecordCount"];
            }
            set
            {
                this.ViewState["TotalRecordCount"] = value;
            }
        }

        /// <summary>
        /// ��ҳѡ����
        /// </summary>
        public List<string> CheckValues
        {
            get
            {
                if (null != ViewState["CheckValues"])
                {
                    return (List<string>)ViewState["CheckValues"];
                }
                else
                {
                    return new List<string>();
                }
            }
            set
            {
                ViewState["CheckValues"] = value;
            }
        }

        /// <summary>
        /// ����ѡ�е�������
        /// </summary>
        public List<string> AllCheckValues
        {
            get
            {
                List<string> checkedValues = this.CheckValues;

                if (this.IsRemeberChecks)
                {
                    foreach (GridViewRow row in this.Rows)
                    {
                        foreach (Control cell in row.Controls)
                        {
                            foreach (Control control in cell.Controls)
                            {
                                if (control is CheckBox)
                                {
                                    string index = string.Empty;
                                    foreach (DictionaryEntry key in this.DataKeys[row.RowIndex].Values)
                                    {
                                        index = key.Value.ToString();

                                        if (!checkedValues.Contains(index) && ((CheckBox)control).Checked)
                                        {
                                            checkedValues.Add(index);
                                        }

                                        if (checkedValues.Contains(index) && !((CheckBox)control).Checked)
                                        {
                                            checkedValues.Remove(index);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return checkedValues;
            }
        }

        /// <summary>
        /// �Ƿ��ҳ��¼ѡ������
        /// </summary>
        public bool IsRemeberChecks
        {
            get
            {
                if (null != ViewState["IsRemeberChecks"])
                {
                    return (bool)ViewState["IsRemeberChecks"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                ViewState["IsRemeberChecks"] = value;
            }
        }

        private IPageDataSource m_PageDataSourceImpl;
        public IPageDataSource PageDataSourceImpl
        {
            get
            {
                return m_PageDataSourceImpl;
                //return (IPageDataSource)ViewState["PageDataSourceImpl"];
            }
            set
            {
                m_PageDataSourceImpl = value;
                //ViewState["PageDataSourceImpl"] = value;
            }
        }
        private GridViewCommandEventHandler m_RowComandImpl;
        public GridViewCommandEventHandler RowComandImpl
        {
            get
            {
                return m_RowComandImpl;
                //return (GridViewCommandEventHandler)ViewState["RowComandImpl"];
            }
            set
            {
                m_RowComandImpl = value;
                //ViewState["RowComandImpl"] = value;
            }
        }
        /// <summary>
        /// �Զ������в���λ�ã�������0��ʼ
        /// </summary>
        public int AutoCreateColumnInsertIndex
        {
            get
            {
                if (ViewState["AutoCreateColumnInsertIndex"] == null)
                {
                    return 0;
                }
                return (int)ViewState["AutoCreateColumnInsertIndex"];
            }
            set
            {
                this.ViewState["AutoCreateColumnInsertIndex"] = value;
            }
        }

        /// <summary>
        /// ��","�ָ�������
        /// </summary>
        public string FootableColIndexs
        { 
           get
           {
               if (ViewState["FootableColIndexs"] == null)
                   return string.Empty;
               else
                   return (string)ViewState["FootableColIndexs"];
           }
            set
            {
                this.ViewState["FootableColIndexs"] = value;
            }
           
        }

        /// <summary>
        /// �ؼ���ʼ��
        /// ˵���������Ҫʹ�ô˿ؼ��ķ�ҳ���ܣ�����ҳ��Page_Load�¼��е��ô˺�����ʼ���οؼ���
        /// </summary>
        /// <param name="pageDataSourceImpl">��ҳ����Դ</param>
        /// <param name="rowComandImpl">���������</param>
        public void Initialization(IPageDataSource pageDataSourceImpl, GridViewCommandEventHandler rowComandImpl)
        {
            Initialization(pageDataSourceImpl, rowComandImpl, null, null);
        }
        /// <summary>
        /// �ؼ���ʼ��
        /// ˵���������Ҫʹ�ô˿ؼ��ķ�ҳ���ܣ�����ҳ��Page_Load�¼��е��ô˺�����ʼ���οؼ���
        /// </summary>
        /// <param name="pageDataSourceImpl">��ҳ����Դ</param>
        /// <param name="rowComandImpl">���������</param>
        /// <param name="emptyDataType">���󶨵��������ͣ�Ҫ�󣺴����ͱ����й�����Ĭ�Ϲ��캯�����÷��������ȡ������Ϊ�գ�������������ͨ�����䷽ʽ�Զ�����һ������ʵ������</param>
        public void Initialization(IPageDataSource pageDataSourceImpl, GridViewCommandEventHandler rowComandImpl, Type emptyDataType)
        {
            Initialization(pageDataSourceImpl, rowComandImpl, emptyDataType, null);
        }
        /// <summary>
        /// �ؼ���ʼ��
        /// ˵���������Ҫʹ�ô˿ؼ��ķ�ҳ���ܣ�����ҳ��Page_Load�¼��е��ô˺�����ʼ���οؼ���
        /// </summary>
        /// <param name="pageDataSourceImpl">��ҳ����Դ</param>
        /// <param name="rowComandImpl">���������</param>
        /// <param name="emptyData">ģ��һ��������</param>
        public void Initialization(IPageDataSource pageDataSourceImpl, GridViewCommandEventHandler rowComandImpl, IList emptyData)
        {
            Initialization(pageDataSourceImpl, rowComandImpl, null, emptyData);
        }
        /// <summary>
        /// �ؼ���ʼ��
        /// </summary>
        /// <param name="pageDataSourceImpl"></param>
        /// <param name="rowComandImpl"></param>
        /// <param name="emptyDataType"></param>
        /// <param name="emptyData"></param>
        private void Initialization(IPageDataSource pageDataSourceImpl, GridViewCommandEventHandler rowComandImpl, Type emptyDataType, IList emptyData)
        {
            this.FirstPageImageUrl = "~/Resources/Images/Static/FirstPage.gif";
            this.NextPageImageUrl = "~/Resources/Images/Static/NextPage.gif";
            this.PrePageImageUrl = "~/Resources/Images/Static/PrePage.gif";
            this.LastPageImageUrl = "~/Resources/Images/Static/LastPage.gif";
            this.GoToPageImageUrl = "~/Resources/Images/Static/go.gif";
            this.PageDataSourceImpl = pageDataSourceImpl;
            this.RowComandImpl = rowComandImpl;
            if (emptyDataType != null)
                this.EmptyDataType = emptyDataType;
            if (emptyData != null)
                this.EmptyData = emptyData;
        }
        /// <summary>
        /// �Զ������ݰ��
        /// ���GridView�Դ���DataBind()����
        /// </summary>
        public void CustomDataBind()
        {
            if (this.PageDataSourceImpl != null)
            {
                int Total;
                IList dataList = this.PageDataSourceImpl(this.CurrentPageIndex, this.PageSize, out Total);
                //�����ȡ��ǰҳ������Ϊ�գ��ҵ�ǰҳ������ҳ����ǰҳ��-1����ȡǰһҳ���ݡ�
                if (dataList.Count == 0 && this.CurrentPageIndex > 1)
                {
                    this.CurrentPageIndex--;
                    dataList = this.PageDataSourceImpl(this.CurrentPageIndex, this.PageSize, out Total);
                }
                this.DataSource = dataList;
                this.TotalRecordCount = Total;
                base.DataBind();
                this.IsCallComstomDataBoundFunction = true;
            }
        }
        #endregion

        #region ���ػ��෽��
        /// <summary>
        /// Ĭ��CssClass����"gridview"
        /// </summary>
        public override string CssClass
        {
            get
            {
                if (string.IsNullOrEmpty(base.CssClass))
                {
                    base.CssClass = "gridview";
                }
                return base.CssClass;
            }
            set
            {
                base.CssClass = value;
            }
        }
        [DefaultValue((string)null), Editor("System.Web.UI.Design.WebControls.DataControlFieldTypeEditor, System.Design", typeof(UITypeEditor)), Description("DataControls_Columns"), MergableProperty(false), PersistenceMode(PersistenceMode.InnerProperty), Category("Default")]
        public override DataControlFieldCollection Columns
        {
            get
            {
                return base.Columns;
            }
        }
        [DefaultValue((string)null), TypeConverter(typeof(StringArrayConverter)), Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design", typeof(UITypeEditor))]
        public override string[] DataKeyNames
        {
            get
            {
                return base.DataKeyNames;
            }
            set
            {
                base.DataKeyNames = value;
            }
        }
        [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0", typeof(UITypeEditor)), DefaultValue(""), UrlProperty]
        public override string BackImageUrl
        {
            get
            {
                return base.BackImageUrl;
            }
            set
            {
                base.BackImageUrl = value;
            }
        }
        protected override void OnInit(EventArgs e)
        {
            if (DesignMode)
                return;

            this.Page.LoadComplete += new EventHandler(Page_LoadComplete);
            this.PageIndexChanging += new GridViewPageEventHandler(CustomGridView_PageIndexChanging);
            this.RowCancelingEdit += new GridViewCancelEditEventHandler(CustomGridView1_RowCancelingEdit);
            this.RowEditing += new GridViewEditEventHandler(CustomGridView1_RowEditing);
            this.RowUpdating += new GridViewUpdateEventHandler(CustomGridView1_RowUpdating);
            this.RowCommand += new GridViewCommandEventHandler(CustomGridView_RowCommand);
            this.RowDataBound += new GridViewRowEventHandler(CustomGridView_RowDataBound);
            base.OnInit(e);
        }
        void CustomGridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        void CustomGridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        void CustomGridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
        void CustomGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (this.RowComandImpl != null)
            {
                this.RowComandImpl(sender, e);
                this.CustomDataBind();
            }
        }

        void CustomGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //footable style
                SetFootableRowStyle(e.Row);
            }
        }

        protected void CustomGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.CurrentPageIndex = e.NewPageIndex;
            this.CustomDataBind();
        }
        protected override void OnPreRender(EventArgs e)
        {
            if (DesignMode)
                return;

            this.AllowPaging = false;
            if (!this.IsCallComstomDataBoundFunction && this.IsEmpty)
            {
                OnDataBound(e);
            }

            if (string.IsNullOrEmpty(this.CssClass))//Ĭ��Ƥ����ʽ
            {
                //�ؼ�Ĭ�Ϸ����
                this.CellPadding = 4;
                this.CellSpacing = 1;
                this.GridLines = GridLines.None;
                this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#333333");
                this.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                this.Font.Size = new FontUnit("9pt");
                this.Width = new Unit("100%");

                this.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#F3F8F8");
                this.HeaderStyle.Font.Bold = false;
                this.HeaderStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#165967");

                this.RowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff");
                this.RowStyle.HorizontalAlign = HorizontalAlign.Left;
                this.RowStyle.Height = new Unit("25px");

                this.AlternatingRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FAFAFA");

                this.EditRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff");

                this.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#D1DDF1");
                this.SelectedRowStyle.Font.Bold = true;
                this.SelectedRowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#333333");

                this.FooterStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#F3F8F8");
                this.FooterStyle.Font.Bold = true;
                this.FooterStyle.Height = new Unit("25px");

                this.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#507C81");
                this.PagerStyle.HorizontalAlign = HorizontalAlign.Right;
                this.PagerStyle.VerticalAlign = VerticalAlign.Middle;
                this.PagerStyle.Wrap = false;
                this.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
                this.EmptyDataRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#F3F8F8");
            }          

            base.OnPreRender(e);
        }
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                if (!DesignMode)
                {
                    IList dataList = (IList)value;
                    if (dataList.Count == 0)
                    {
                        if (this.m_EmptyData == null && this.m_EmptyDataType == null)
                        {
                            throw new Exception("���ٵ����ã�EmptyData,EmptyDataType�е�һ�");
                        }
                        this.IsEmpty = true;
                        if (this.EmptyData == null)
                        {
                            //׷�ӿ�������
                            if (value is DataView)
                            {
                                DataRowView rowView = ((DataView)value).AddNew();
                                foreach (DataColumn col in ((DataView)value).Table.Columns)
                                {
                                    object defaultValue = null;
                                    if (col.DataType.Equals(typeof(System.String)))
                                    {
                                        defaultValue = string.Empty;
                                    }
                                    else if (col.DataType.Equals(typeof(System.Guid)))
                                    {
                                        defaultValue = Guid.Empty;
                                    }
                                    else if (col.DataType.Equals(typeof(System.Boolean)))
                                    {
                                        defaultValue = false;
                                    }
                                    else if (col.DataType.Equals(typeof(System.Int64)) || col.DataType.Equals(typeof(System.Int32)) || col.DataType.Equals(typeof(System.Int16)))
                                    {
                                        defaultValue = 0;
                                    }
                                    else if (col.DataType.Equals(typeof(System.DateTime)))
                                    {
                                        defaultValue = DateTime.MinValue;
                                    }
                                    else if (col.DataType.Equals(typeof(System.Single)) || col.DataType.Equals(typeof(System.Decimal)) || col.DataType.Equals(typeof(System.Double)))
                                    {
                                        defaultValue = 0;
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format("��֧������Ϊ{0}�У������޸ĳ�String���ͣ�", col.DataType));
                                    }
                                    rowView[col.ColumnName] = defaultValue;
                                }
                            }
                            else
                            {
                                IList ilist = (IList)value;
                                Type dataType = this.m_EmptyDataType;

                                if (value.GetType().IsArray)
                                {
                                    object emptyItem = Activator.CreateInstance(value.GetType().GetElementType());
                                    ilist = Array.CreateInstance(emptyItem.GetType(), 1);
                                    ilist[0] = emptyItem;
                                    value = ilist;
                                }
                                else
                                {
                                    object emptyItem = Activator.CreateInstance(value.GetType().GetGenericArguments()[0]);
                                    ilist.Add(emptyItem);
                                }
                            }
                        }
                        else
                        {
                            while (this.EmptyData.Count > 1)
                            {
                                this.EmptyData.RemoveAt(this.EmptyData.Count - 1);
                            }
                            value = this.EmptyData;
                        }
                    }
                    else
                    {
                        this.IsEmpty = false;
                    }
                }
                base.DataSource = value;
            }
        }

        protected override void OnDataBound(EventArgs e)
        {
            if (DesignMode)
                return;

            base.OnDataBound(e);
            if (IsEmpty)
            {
                if (this.Rows.Count > 0)
                {
                    this.Rows[0].Cells.Clear();
                    TableCell tc = new TableCell();
                    tc.Text = this.EmptyDataSourceTip;
                    tc.ColumnSpan = CellVisibleCount();
                    tc.HorizontalAlign = HorizontalAlign.Center;
                    this.Rows[0].Cells.Add(tc);
                }
            }
            if (CustomAllowPaging)
            {
                this.FooterRow.Cells.Clear();
                TableCell tc = new TableCell();
                this.FooterRow.Cells.Add(tc);
                tc.ColumnSpan = this.HeaderRow.Cells.Count;
                tc.HorizontalAlign = HorizontalAlign.Center;
                if (!IsEmpty)
                {
                    tc.Text += string.Format("��{0}��&nbsp;&nbsp;", this.TotalRecordCount, this.CurrentPageCount);
                    tc.Text += string.Format(PageFormatString, this.CurrentPageIndex == 1 ? "#" : string.Format(ScriptFormatString, 1), GetNormalRelationPath(this.FirstPageImageUrl));
                    tc.Text += string.Format(PageFormatString, this.CurrentPageIndex == 1 ? "#" : string.Format(ScriptFormatString, this.CurrentPageIndex - 1), GetNormalRelationPath(this.PrePageImageUrl), this.CurrentPageIndex > 1 ? "" : "return;");
                    tc.Text += string.Format("&nbsp;{0}/{1}&nbsp;", this.CurrentPageIndex, this.CurrentPageCount);
                    tc.Text += string.Format(PageFormatString, this.CurrentPageIndex == this.CurrentPageCount ? "#" : string.Format(ScriptFormatString, this.CurrentPageIndex + 1), GetNormalRelationPath(this.NextPageImageUrl), this.CurrentPageIndex < this.CurrentPageCount ? "" : "return;");
                    tc.Text += string.Format(PageFormatString, this.CurrentPageIndex == this.CurrentPageCount ? "#" : string.Format(ScriptFormatString, this.CurrentPageCount), GetNormalRelationPath(this.LastPageImageUrl), this.CurrentPageIndex < this.CurrentPageCount ? "" : "return;");
                    tc.Text += string.Format("&nbsp;<input type='text' id='{0}' name='{0}' style='width:40px' maxlength='4' value='{1}'/>", this.RequestPageClientID, this.CurrentPageIndex);
                    tc.Text += string.Format("<a href='javascript:document.forms[\"{1}\"].submit()'><img border='0' src='{0}' /></a>", GetNormalRelationPath(this.GoToPageImageUrl), this.Page.Form.ClientID);
                }
            }
        }

        private int CellVisibleCount()
        {
            int cells = 0;
            int columns = 0;

            foreach (TableCell cell in this.HeaderRow.Cells)
            {
                var dataCell = (System.Web.UI.WebControls.DataControlFieldCell)cell;
                if (cell.Visible && !dataCell.ContainingField.HeaderStyle.CssClass.Contains("hide"))
                    cells++;
            }

            foreach (DataControlField col in this.Columns)
            {
                if (col.Visible)
                    columns++;
            }

            return cells <= columns ? cells : columns;
        }
        protected override ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
        {
            ArrayList list = base.CreateColumns(dataSource, useDataSource) as ArrayList;
            if (AutoCreateColumnInsertIndex > this.Columns.Count - 1)//�������ָ������λ�ã�����ϵͳĬ�Ϸ�ʽ�ṩ
            {
                return list;
            }
            ArrayList newList = new ArrayList();
            int autoCreateColumnsStartIndex = this.Columns.Count;
            for (int i = 0; i <= AutoCreateColumnInsertIndex; i++)
            {
                newList.Add(list[i]);
            }
            for (int i = autoCreateColumnsStartIndex; i < list.Count; i++)
            {
                newList.Add(list[i]);
            }
            for (int i = AutoCreateColumnInsertIndex + 1; i < this.Columns.Count; i++)
            {
                newList.Add(list[i]);
            }
            return newList;
        }
        #endregion

        public static string GetNormalRelationPath(string ASPNETRelationPath)
        {
            if (string.IsNullOrEmpty(ASPNETRelationPath))
            {
                return ASPNETRelationPath;
            }
            string root = System.Web.HttpContext.Current.Request.ApplicationPath;
            return ASPNETRelationPath.Replace("~", root == "/" ? "" : root);
        }

        /// <summary>
        /// ��ȡѡ���������ֵ
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="checkboxColumnIndex"></param>
        /// <param name="checkboxID"></param>
        /// <returns></returns>
        public static T[] GetSelectedValues<T>(GridView gridView, int checkboxColumnIndex = 0, string checkboxID = "CheckBox1")
        {
            List<T> list = new List<T>();
            foreach (GridViewRow row in gridView.Rows)
            {
                CheckBox control = row.Cells[checkboxColumnIndex].FindControl(checkboxID) as CheckBox;
                if (control != null && control.Checked)
                {
                    list.Add((T)gridView.DataKeys[row.RowIndex].Value);
                }
            }
            return list.ToArray();
        }

        private void SetFootableRowStyle(GridViewRow row)
        {
            Regex reg = new Regex(@"^[-]?\d+[.]?\d*$");            
            TableCellCollection headerCells = row.Cells;

            if (string.IsNullOrEmpty(FootableColIndexs) && this.CssClass == "footable")
            { 
                FootableColIndexs = string.Join(",", new string[]{"0", (headerCells.Count -1).ToString()});
            }

            string[] cellIndexs = FootableColIndexs.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < headerCells.Count; i++)
            {
                for (int j = 0; j < cellIndexs.Length; j++)
                {
                    if (reg.IsMatch(cellIndexs[j]) && i == int.Parse(cellIndexs[j]))
                    {
                        headerCells[i].Attributes.Add("data-ignore", "true");
                        continue;
                    }

                    headerCells[i].Attributes.Add("data-hide", "tablet");
                }
            }
        }

    }
}
