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

using System.Text;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
public partial class Admin_Controls_FunctionTool : System.Web.UI.UserControl
{
    private static FunctionGroupLogic functionGroupLogic = new FunctionGroupLogic();
    private static RoleLogic roleLogic = new RoleLogic();

    private FunctionGroupFunctionRelationLogic functionGroupRelationManager = new FunctionGroupFunctionRelationLogic(null);
    private RoleFunctionRelationLogic roleFunctionRelationManager = new RoleFunctionRelationLogic(null);
    private FunctionPageUrlRelationLogic functionPageUrlRelationLogic = new FunctionPageUrlRelationLogic(null);
    private UserRoleRelationLogic roleUserRelationLogic = new UserRoleRelationLogic();
    private UserFunctionRelationLogic userFunctionRelationManager = new UserFunctionRelationLogic(null);
    private FunctionLogic functionLogic = new FunctionLogic();
    protected string Tools = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Text.StringBuilder HtmlWriter = new StringBuilder();
        //LoadTreeFuntion();
        //载入用户:角色权限
        //角色对应的功能列表
        RoleFunction[] rolefunctions = null;
        ArrayList sumRoleFunctions = new ArrayList();
        foreach (Role item in roleUserRelationLogic.Query(ETMS.AppContext.UserContext.Current.UserID))
        {
            //角色禁用，则跳过
            if (item.State == 0)
            {
                continue;
            }

            Role parentRole = new Role();
            //当前子角色对应的功能列表
            parentRole.RoleID = item.RoleID;
            roleFunctionRelationManager.Manager = parentRole;
            sumRoleFunctions.AddRange((RoleFunction[])roleFunctionRelationManager.GetAllMembers(""));
        }
        rolefunctions = (RoleFunction[])sumRoleFunctions.ToArray(typeof(RoleFunction));

        //载入用户权限 
        User settingUser = new User();
        settingUser.UserID = ETMS.AppContext.UserContext.Current.UserID;
        userFunctionRelationManager.Manager = settingUser;
        UserFunction[] userfunctions = (UserFunction[])userFunctionRelationManager.GetAllMembers("");

        bool hasFunction = false;

        //载入功能组、功能菜单、三级菜单
        Node rootFunctionGroup = functionGroupLogic.GetNodeTree(new FunctionGroup() { GroupID = 1 }, false);
        //循环一级菜单
        foreach (Node firstLevelFunctionGroup in rootFunctionGroup.ChildNodes)
        {
            RoleFunction[] findRoleFunctions = Array.FindAll<RoleFunction>(rolefunctions, (item) =>
            {
                return item.FunctionGroupCode.StartsWith(firstLevelFunctionGroup.NodeCode);
            });

            UserFunction[] findUserFunctions = Array.FindAll<UserFunction>(userfunctions, (item) =>
            {
                return item.FunctionGroupCode.StartsWith(firstLevelFunctionGroup.NodeCode);
            });
            //如果有子功能，则显示一级菜单
            if (firstLevelFunctionGroup.State == 1
                && (findRoleFunctions.Length > 0 || findUserFunctions.Length > 0))
            {

                //如果一级功能组设置了组件ID，并且组件ID是枚举ETMS.Product.ExtendComponentType成员
                string firstFunctionGroupComponentID = (firstLevelFunctionGroup as FunctionGroup).ComponentID;
                ETMS.Product.ExtendComponentType firstMenuComponentType;              
                if (!string.IsNullOrEmpty(firstFunctionGroupComponentID) && Enum.TryParse<ETMS.Product.ExtendComponentType>(firstFunctionGroupComponentID, true, out firstMenuComponentType))
                {
                    //未启用的组件功能则跳过
                    if (!ETMS.Product.ProductComponentStrategy.IsSupport(firstMenuComponentType))
                    {
                        continue;
                    }
                }

                //一级菜单开始
                HtmlWriter.Append(@"
<li class='level1'>
    <div onclick='menuClick(this);' id='MEMU_FUNC${FirstMenuID}' class='level1Style'>
        <span class='ico-menu Func${FirstMenuCode}'>${FirstMenuName}</span>
    </div>
    <ul style='display:none;' id='MEMU_FUNC${FirstMenuID}d' class='MenuLevel2'>
      <li class='level2'>
".Replace("${FirstMenuID}", firstLevelFunctionGroup.NodeID.ToString())
           .Replace("${FirstMenuCode}", firstLevelFunctionGroup.NodeCode)
           .Replace("${FirstMenuName}", firstLevelFunctionGroup.NodeName));

                //  循环二级菜单             
                foreach (Node secondFunctionGroup in functionGroupLogic.GetNodeTree(firstLevelFunctionGroup, false).ChildNodes)
                {
                    findRoleFunctions = Array.FindAll<RoleFunction>(rolefunctions, (item) =>
                   {
                       return item.FunctionGroupCode.StartsWith(secondFunctionGroup.NodeCode);
                   });

                    findUserFunctions = Array.FindAll<UserFunction>(userfunctions, (item) =>
                    {
                        return item.FunctionGroupCode.StartsWith(secondFunctionGroup.NodeCode);
                    });
                    //如果有子功能，则显示二级菜单                  
                    if (secondFunctionGroup.State == 1
                        && (findRoleFunctions.Length > 0 || findUserFunctions.Length > 0))
                    {

                        //如果二级功能组设置了组件ID，并且组件ID是枚举ETMS.Product.ExtendComponentType成员
                        string secondFunctionGroupComponentID = (secondFunctionGroup as FunctionGroup).ComponentID;
                        ETMS.Product.ExtendComponentType secondMenuComponentType;
                        if (!string.IsNullOrEmpty(secondFunctionGroupComponentID) && Enum.TryParse<ETMS.Product.ExtendComponentType>(secondFunctionGroupComponentID, true, out secondMenuComponentType))
                        {
                            //未启用的组件功能则跳过
                            if (!ETMS.Product.ProductComponentStrategy.IsSupport(secondMenuComponentType))
                            {
                                continue;
                            }
                        }


                        //功能项
                        //合并功能
                        System.Collections.Generic.IDictionary<int, Function> allFunctions = new System.Collections.Generic.Dictionary<int, Function>();
                        foreach (RoleFunction roleFunctionItem in findRoleFunctions)
                        {
                            if (!allFunctions.ContainsKey(roleFunctionItem.FunctionID))
                            {
                                allFunctions.Add(roleFunctionItem.FunctionID, functionLogic.GetFunctionByID(roleFunctionItem.FunctionID));
                            }
                        }
                        foreach (UserFunction userFunctionItem in findUserFunctions)
                        {
                            if (!allFunctions.ContainsKey(userFunctionItem.FunctionID))
                            {
                                allFunctions.Add(userFunctionItem.FunctionID, functionLogic.GetFunctionByID(userFunctionItem.FunctionID));
                            }
                        }
                        Function[] sortFunctions = new Function[allFunctions.Count];
                        allFunctions.Values.CopyTo(sortFunctions, 0);

                        //排序
                        Array.Sort<Function>(sortFunctions, (A, B) =>
                        {
                            return A.OrderNo.CompareTo(B.OrderNo);
                        });
                        bool isAppendSecondMenu = false;
                        foreach (Function showItem in sortFunctions)
                        {
                            if (showItem.Status == 0)//停用功能跳过
                                continue;


                            //如果功能设置了组件ID，并且组件ID是枚举ETMS.Product.ExtendComponentType成员
                            ETMS.Product.ExtendComponentType componentType;
                            if (!string.IsNullOrEmpty(showItem.ComponentID) && Enum.TryParse<ETMS.Product.ExtendComponentType>(showItem.ComponentID, true, out componentType))
                            {
                                //未启用的组件功能则跳过
                                if (!ETMS.Product.ProductComponentStrategy.IsSupport(componentType))
                                {
                                    continue;
                                }
                            }

                            functionPageUrlRelationLogic.Manager = new Function() { FunctionID = showItem.FunctionID };
                            PageUrl[] pageUrls = (PageUrl[])functionPageUrlRelationLogic.GetAllMembers("");
                            if (pageUrls.Length > 0)
                            {
                                if (!isAppendSecondMenu)//如果没有追加二级菜单（头），则追加，作用：如果没有可用功能，则不显示二级菜单
                                {
                                    isAppendSecondMenu = true;
                                    //      二级菜单开始
                                    HtmlWriter.Append(@"   
                <div onclick='subMenuClick(this);' id='MEMU_FUNC${SecondMenuID}' class='level2Style'>
                    <img align='absmiddle' src='App_Themes/ThemeAdmin/Images/menu_arrow_close.gif' id='MEMU_FUNC${SecondMenuID}_img'>
                    ${SecondMenuName}</div>
                <ul style='display: none' id='MEMU_FUNC${SecondMenuID}d' class='MenuLevel2'>"
                                        .Replace("${SecondMenuID}", secondFunctionGroup.NodeID.ToString())
                                        .Replace("${SecondMenuName}", secondFunctionGroup.NodeName));
                                }

                                HtmlWriter.Append("  <li class='level3Head nobj' onclick='gotoUrl(\"${ItemURL}\",\"myframe\" ,this);'><span class='arrow-left'>${ItemName}</span> </li>"
                                    .Replace("${FirstMenuID}", firstLevelFunctionGroup.NodeID.ToString())
                                   .Replace("${ItemURL}", this.ActionHref("~/" + pageUrls[0].PageURL))
                                   .Replace("${ItemName}", showItem.FunctionName));

                                //标识用户有功能菜单
                                hasFunction = true;
                            }
                        }
                        if (isAppendSecondMenu)//如果追加二级菜单（头），则追加尾
                        {
                            //      二级菜单结束
                            HtmlWriter.Append(@"
                </ul>");
                        }
                    }
                }
                HtmlWriter.Append(@"
     </li>
  </ul>
</li>");

                //一级菜单结束
            }
        }
        if (hasFunction)
        {
            ltMenuTree.Text = HtmlWriter.ToString();
        }
        else
        {
            if(ETMS.AppContext.UserContext.Current.UserID != 1)
                throw new ETMS.Security.AuthorizationException(this.Request.Path);
        }

    }


}
