using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Security_BuildSitePathXml : System.Web.UI.Page
{
    private static FunctionGroupLogic functionGroupLogic = new FunctionGroupLogic();
    private FunctionGroupFunctionRelationLogic functionGroupFunctionRelationLogic = new FunctionGroupFunctionRelationLogic(null);
    private FunctionPageUrlRelationLogic functionPageUrlRelationLogic = new FunctionPageUrlRelationLogic(null);

    protected void Button1_Click(object sender, EventArgs e)
    {

        Node parentNode = new FunctionGroup() { GroupID = 1 };
        parentNode = functionGroupLogic.GetNodeTree(parentNode, false);
        System.Text.StringBuilder XmlWriter = new System.Text.StringBuilder();
        XmlWriter.Append(@"<?xml version='1.0' encoding='utf-8' ?>
<sitePath>	
");
        //循环第一级功能组
        foreach (Node firstGroupItem in parentNode.ChildNodes)
        {
            //记录一级菜单-开始
            XmlWriter.AppendFormat(@"<sitePathNode url='' title='{0}'  description='' >", firstGroupItem.NodeName);


            foreach (Node secondGroupItem in functionGroupLogic.GetNodeTree(firstGroupItem, false).ChildNodes)
            {
                //----记录二级菜单--开始
                XmlWriter.AppendFormat(@"<sitePathNode url='' title='{0}'  description='' >", secondGroupItem.NodeName);
                secondGroupItem.KeyName = "FunctionGroupID";
                functionGroupFunctionRelationLogic.Manager = secondGroupItem;
                foreach (Function funItem in functionGroupFunctionRelationLogic.GetAllMembers(""))
                {
                    //------记录功能
                    //找到默认首页
                    functionPageUrlRelationLogic.Manager = funItem;
                    PageUrl[] pageUrls = (PageUrl[])functionPageUrlRelationLogic.GetAllMembers("");
                    if (pageUrls.Length > 0)
                    {
                        XmlWriter.AppendFormat(@"<sitePathNode url='~/{0}' title='{1}'  description=''  />",pageUrls[0].PageURL, funItem.FunctionName);
                    }
                }
                //记录一级菜单-结束
                XmlWriter.Append("</sitePathNode>");
                //----记录二级菜单--结束
            }

            //记录一级菜单-结束
            XmlWriter.AppendFormat("</sitePathNode>");
        }
        XmlWriter.Append(@"
</sitePath>	
");

        System.IO.File.WriteAllText(MapPath("~/temp.xml"), XmlWriter.ToString());

        ETMS.Utility.JsUtility.AlertMessageBox("文件SitePathXml创建成功，存放在~/temp.xml");
    } 
}