using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace ETMS.Controls
{
    #region 枚举数据
    /// <summary>
    /// 验证数据类型
    /// </summary>
    public enum DataType
    {
        Never,     //不验证
        String,     //字符串
        Int,     //整数
        IntPostive,    //大于0的整数
        IntZeroPostive,   //大于等于0的整数
        Float,     //数字
        FloatPostive,   //大于0的数字
        FloatZeroPostive, //大于等于0的数字
        Url,
        Mail,
        //ChineseChars,   //汉字
        EnglishChars,   //英文
        EngNum,     //英文和数字
        EngNumUnerline,   //英文、数字和下划线
        PhoneNumber,   //电话号码
        MobileNumber,   //手机号码
        PostalCode,    //邮政编码
        Custom
    }
    #endregion

    public class ExtTextBox : System.Web.UI.WebControls.TextBox
    {
        #region 子控件
        //private System.Web.UI.WebControls.TextBox txtDataInput = new TextBox();
        private System.Web.UI.WebControls.RequiredFieldValidator rfvDataInput = new RequiredFieldValidator();
        private System.Web.UI.WebControls.RegularExpressionValidator revDataInput = new RegularExpressionValidator();
        private Panel pnlFrame = new Panel();    //承载其它控件的容器Panel控件
        #endregion

        private string error = "";

        #region 控件自定义属性

        [Bindable(true)]
        [Category("自定义信息区")]
        [Browsable(true)]
        [Description("是否允许空值")]
        [DefaultValue("true")]
        public bool AllowEmpty
        {
            get { return ViewState["AllowEmpty"] == null ? true : (bool) ViewState["AllowEmpty"]; }
            set { ViewState["AllowEmpty"] = value; }
        }
        [Bindable(true)]
        [Category("自定义信息区")]
        [Browsable(true)]
        [Description("验证数据类型，默认为不验证")]
        [DefaultValue("IntPostive")]
        public DataType ValidType
        {
            get { return ViewState["ValidType"] == null ? DataType.Never : (DataType) ViewState["ValidType"]; }
            set { ViewState["ValidType"] = value; }
        }
        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("自定义验证错误信息")]
        [DefaultValue("")]
        public string ValidError
        {
            get { return ViewState["ValidError"] == null ? "" : (string) ViewState["ValidError"]; }
            set { ViewState["ValidError"] = value; }
        }
        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("自定义用于验证的正则表达式，ValidType 为 Custom 时有效")]
        [DefaultValue("")]
        public string ValidExpressionCustom
        {
            get { return ViewState["ValidExpressionCustom"] == null ? "" : (string) ViewState["ValidExpressionCustom"]; }
            set { ViewState["ValidExpressionCustom"] = value; }
        }
        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("错误信息提示的CSS类名")]
        [DefaultValue("")]
        public string CssError
        {
            get { return ViewState["CssError"] == null ? "" : (string) ViewState["CssError"]; }
            set { ViewState["CssError"] = value; }
        }

        protected override void EnsureChildControls()
        {
            this.rfvDataInput.CssClass = this.CssError;
            this.rfvDataInput.ErrorMessage = "*输入不能为空";
            this.rfvDataInput.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            this.rfvDataInput.EnableViewState = true;
            this.rfvDataInput.ControlToValidate = base.ID;
            this.revDataInput.CssClass = this.CssError;
            this.revDataInput.ErrorMessage = "*输入格式错误";
            this.revDataInput.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            this.revDataInput.EnableViewState = true;
            this.revDataInput.ControlToValidate = base.ID;
            //将子控件添加到此自定义控件中
            this.Controls.Add(rfvDataInput);
            this.Controls.Add(revDataInput);
            this.Controls.Add(pnlFrame);
        }
        #endregion

        /// <summary>
        /// 根据设置的验证数据类型返回不同的正则表达式样
        /// </summary>
        /// <returns></returns>
        #region GetRegex
        private string GetValidRegex()
        {
            string regex = @"(\S)";
            switch (this.ValidType)
            {
            case DataType.Never:
                break;
            case DataType.Int:
                error = "*必须为整数";
                regex = @"(-)?(\d+)";
                break;
            case DataType.IntPostive:
                error = "*必须为大于0的整数";
                regex = @"([1-9]{1}\d*)";
                break;
            case DataType.IntZeroPostive:
                error = "*必须为不小于0的整数";
                regex = @"(\d+)";
                break;
            case DataType.Float:
                error = "*必须为数字";
                regex = @"(-)?(\d+)(((\.)(\d)+))?";
                break;
            case DataType.FloatPostive:
                error = "*必须为大于0的数字";
                regex = @"(\d+)(((\.)(\d)+))?";
                break;
            case DataType.FloatZeroPostive:
                error = "*必须为不小于0的数字";
                regex = @"(\d+)(((\.)(\d)+))?";
                break;
            case DataType.Url:
                error = "*URL格式错误";
                regex = @"(http://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                break;
            case DataType.Mail:
                error = "*EMail格式错误";
                regex = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                break;
            //    case DataType.ChineseChars :
            //     error = "*包含中文字符";
            //     regex = @"[^\x00-\xff]";
            //     break;
            case DataType.EnglishChars:
                error = "*只能输入英文字符";
                regex = @"[a-zA-Z]*";
                break;
            case DataType.EngNum:
                error = "*只能输入英文字符和数字";
                regex = @"[a-zA-Z0-9]*";
                break;
            case DataType.EngNumUnerline:
                error = "*只能输入英文字符、数字和下划线";
                regex = @"[a-zA-Z0-9_]*";
                break;
            case DataType.PhoneNumber:
                error = "*电话号码格式错误";
                regex = @"(86)?(-)?(0\d{2,3})?(-)?(\d{7,8})(-)?(\d{1,5})?";
                break;
            case DataType.MobileNumber:
                error = "*手机号码格式错误";
                regex = @"(0)?(13)\d{9}";
                break;
            case DataType.PostalCode:
                error = "*邮编格式错误";
                regex = @"\d{6}";
                break;
            case DataType.Custom:
                error = "*格式错误";
                regex = this.ValidExpressionCustom;
                break;
            default:
                break;
            }
            if (this.ValidError.Trim() != "")
                error = this.ValidError;
            return regex;
        }
        #endregion

        #region 将此控件呈现给指定的输出参数
        /// <summary> 
        /// 将此控件呈现给指定的输出参数。
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            base.Render(output);
            output.Write("&nbsp;");
            if (!this.AllowEmpty)
            {
                this.rfvDataInput.ID = "rfv" + base.ID;
                this.rfvDataInput.ControlToValidate = base.ID;
                this.rfvDataInput.RenderControl(output);
            }
            if (this.ValidType != DataType.Never && this.ValidType != DataType.String)
            {
                this.revDataInput.ID = "rev" + base.ID;
                this.revDataInput.ControlToValidate = base.ID;
                this.revDataInput.ValidationExpression = this.GetValidRegex();
                this.revDataInput.ErrorMessage = error;
                this.revDataInput.RenderControl(output);
            }
        }
        #endregion
    }
}
