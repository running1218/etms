using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using ETMS.Controls;

namespace ETMS.WebApp.Manage.Controls
{
    public partial class PageSet : System.Web.UI.UserControl
    {
        private object m_DataControl = null;
        private IPageDataSource m_dataSource;

        #region 参数定义
        /// <summary>
        /// 页面记录数(分页尺寸)
        /// </summary>
        public int PageSize
        {
            get
            {
                if (ViewState["pageSize"] == null)
                {
                    ViewState["pageSize"] = 10;
                }
                return (int)ViewState["pageSize"];
            }
            set
            {
                ViewState["pageSize"] = value;
            }
        }

        /// <summary>
        /// 当前页码(第几页)
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (ViewState["pageIndex"] != null)
                {
                    return (int)ViewState["pageIndex"];
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                ViewState["pageIndex"] = (value <= 0 ? 1 : value);
            }
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        private int TotalCount
        {
            get
            {
                if (ViewState["totalCount"] != null)
                {
                    return (int)ViewState["totalCount"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["totalCount"] = value;
            }
        }

        /// <summary>
        /// 页数(总共多少页)
        /// </summary>
        public int PageCount
        {
            get
            {
                int _PageCount = TotalCount / PageSize;
                if (TotalCount % PageSize > 0)
                {
                    _PageCount++;
                }

                return (_PageCount == 0 ? 1 : _PageCount);
            }
        }

        /// <summary>
        /// 页面形式：0 默认后台风格 1 灰色简单风格 2 灰色复杂风格
        /// </summary>
        public int PageType
        {
            get
            {
                if (ViewState["PageType"] == null)
                {
                    ViewState["PageType"] = 0;
                }
                return (int)ViewState["PageType"];
            }
            set
            {
                ViewState["PageType"] = value;
            }
        }

        /// <summary>
        /// 是否显示页次
        /// </summary>
        public bool ShowPanelPageNumber
        {
            get
            {
                if (ViewState["ShowPanelPageNumber"] == null)
                {
                    ViewState["ShowPanelPageNumber"] = true;
                }
                return (bool)ViewState["ShowPanelPageNumber"];
            }
            set
            {
                ViewState["ShowPanelPageNumber"] = value;
            }
        }

        /// <summary>
        /// 是否显示每页数
        /// </summary>
        public bool ShowPanelPageBulk
        {
            get
            {
                if (ViewState["ShowPanelPageBulk"] == null)
                {
                    ViewState["ShowPanelPageBulk"] = true;
                }
                return (bool)ViewState["ShowPanelPageBulk"];
            }
            set
            {
                ViewState["ShowPanelPageBulk"] = value;
            }
        }

        /// <summary>
        /// 是否显示总记录数
        /// </summary>
        public bool ShowPanelRecordNumber
        {
            get
            {
                if (ViewState["ShowPanelRecordNumber"] == null)
                {
                    ViewState["ShowPanelRecordNumber"] = true;
                }
                return (bool)ViewState["ShowPanelRecordNumber"];
            }
            set
            {
                ViewState["ShowPanelRecordNumber"] = value;
            }
        }

        /// <summary>
        /// 是否显示跳转
        /// </summary>
        public bool ShowPanelSelectPage
        {
            get
            {
                if (ViewState["ShowPanelSelectPage"] == null)
                {
                    ViewState["ShowPanelSelectPage"] = true;
                }
                return (bool)ViewState["ShowPanelSelectPage"];
            }
            set
            {
                ViewState["ShowPanelSelectPage"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region 分页控件初始化与数据控件绑定
        /// <summary>
        /// 控件初始化
        /// </summary>
        /// <param name="Control">需要绑定数据的控件</param>
        /// <param name="dataSource">绑定控件的数据源</param>
        public void pageInit(object DataControl, IPageDataSource dataSource)
        {
            m_dataSource = new IPageDataSource(dataSource);
            m_DataControl = DataControl;

            this.Previous.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
            this.Next.Click += new System.Web.UI.ImageClickEventHandler(this.NavigationButton_Click);
        }

        /// <summary>
        /// 翻页时记忆选择数据
        /// </summary>
        /// <param name="DataControl"></param>
        public void RemeberCheckValues(object DataControl)
        {
            if (DataControl is GridView)
            {
                var sourceControl = (CustomGridView)DataControl;
                if (sourceControl.IsRemeberChecks)
                {
                    foreach (GridViewRow row in sourceControl.Rows)
                    {
                        foreach (Control cell in row.Controls)
                        {
                            foreach (Control control in cell.Controls)
                            {
                                if (control is CheckBox)
                                {
                                    string index = string.Empty;
                                    foreach (DictionaryEntry key in sourceControl.DataKeys[row.RowIndex].Values)
                                    {
                                        index = key.Value.ToString();
                                        var checkValues = sourceControl.CheckValues;

                                        if (!sourceControl.CheckValues.Contains(index) && ((CheckBox)control).Checked)
                                        {
                                            checkValues.Add(index);
                                        }

                                        if (sourceControl.CheckValues.Contains(index) && !((CheckBox)control).Checked)
                                        {
                                            checkValues.Remove(index);
                                        }
                                        sourceControl.CheckValues = checkValues;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 翻页后，之前选择的数据重新选中
        /// </summary>
        /// <param name="DataControl"></param>
        public void RecoverPageCheckedValue(object DataControl)
        {
            if (DataControl is GridView)
            {
                var sourceControl = (CustomGridView)DataControl;
                if (sourceControl.IsRemeberChecks)
                {
                    foreach (GridViewRow row in sourceControl.Rows)
                    {
                        foreach (Control cell in row.Controls)
                        {
                            foreach (Control control in cell.Controls)
                            {
                                if (control is CheckBox)
                                {
                                    string index = string.Empty;
                                    foreach (DictionaryEntry key in sourceControl.DataKeys[row.RowIndex].Values)
                                    {
                                        index = key.Value.ToString();
                                        if (sourceControl.CheckValues.Contains(index))
                                        {
                                            ((CheckBox)control).Checked = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void QueryChange()
        {
            PageIndex = 1;

            if (m_DataControl is GridView)
            {
                ((CustomGridView)m_DataControl).CheckValues = null;
            }

            DataBind();
        }
        /// <summary>
        /// 控件数据绑定
        /// </summary>
        public void DataBind()
        {
            int _totalCount = 0;

            if (m_DataControl is BaseDataBoundControl)
            {
                ((BaseDataBoundControl)m_DataControl).DataSource = m_dataSource(PageIndex, PageSize, out _totalCount);
                ((BaseDataBoundControl)m_DataControl).DataBind();
            }
            else if (m_DataControl is Repeater)
            {
                ((Repeater)m_DataControl).DataSource = m_dataSource(PageIndex, PageSize, out _totalCount);
                ((Repeater)m_DataControl).DataBind();
            }
            else
            {
                throw new Exception("不支持传入控件的数据绑定");
            }

            TotalCount = _totalCount;
            PageOperate();
            RecoverPageCheckedValue(m_DataControl);
        }
        #endregion
        #region 分页控件事件
        protected void Go_Click(object sender, ImageClickEventArgs e)
        {
            if (SelectPage.Text.Trim() != "")
            {
                int iPage = 1;
                try
                {
                    iPage = int.Parse(SelectPage.Text.Trim().Split(',')[0]);
                }
                catch
                {
                    iPage = 1;
                }

                if (iPage >= 1 && iPage <= PageCount)
                {
                    PageIndex = iPage;
                }
                else if (iPage > PageCount)
                {
                    PageIndex = PageCount;
                }
                else
                    PageIndex = 1;
            }
            else
            {
                ImageButton1.EnableViewState = true;
            }
            RemeberCheckValues(m_DataControl);
            DataBind();
        }

        //分页按钮事件
        private void NavigationButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string strCommandName = ((ImageButton)sender).ID;//取得事件的对象名

            switch (strCommandName)
            {
                case "First":
                    PageIndex = 1;//跳转到首页
                    break;
                case "Previous":
                    PageIndex = Math.Max(PageIndex - 1, 1);//跳转到上一页
                    break;
                case "Next":
                    PageIndex = Math.Min(PageIndex + 1, PageCount);//跳转到下一页
                    break;
                case "Lastly":
                    PageIndex = PageCount;//跳转到最后一页
                    break;
            }
            RemeberCheckValues(m_DataControl);
            DataBind();
        }
        #endregion

        //页次显示改变
        private void PageOperate()
        {
            //如果数据源的记录总数改变，则判断当前页面索引号是否〉页面总数，如果是则设置当前页面索引号为最后一页。
            if (this.PageIndex > this.PageCount)
            {
                this.PageIndex = this.PageCount == 0 ? 1 : this.PageCount;
                if (this.PageCount > 0) this.DataBind();
            }

            RecordNumber.Text = TotalCount.ToString();
            SelectPage.Text = PageIndex.ToString();

            Literal1.Text = "";

            int beginNum = 1;
            int endNum = 1;

            if (PageCount < 8)
            {
                beginNum = 1;
                endNum = PageCount;
            }
            else
            {
                beginNum = PageIndex - 3;
                if (beginNum < 1)
                {
                    beginNum = 1;
                }
                if (beginNum > PageCount - 6)
                {
                    beginNum = PageCount - 6;
                }

                endNum = beginNum + 6;
            }

            Literal1.Text = "";
            for (int i = beginNum; i <= endNum; i++)
            {
                if (i == PageIndex)
                {
                    Literal1.Text += "<a href='javascript:goClick_" + this.ClientID + "(" + i + ")' class='pageNumber pageSelected'>" + i + "</a>";
                }
                else
                {
                    Literal1.Text += "<a href='javascript:goClick_" + this.ClientID + "(" + i + ")' class='pageNumber'>" + i + "</a>";
                }
            }
        }
    }
}
