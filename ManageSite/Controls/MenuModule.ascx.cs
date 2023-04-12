using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using System.Text;
using System.Collections;
using ETMS.AppContext;

public partial class Controls_MenuModule : System.Web.UI.UserControl
{
    private static FunctionGroupLogic functionGroupLogic = new FunctionGroupLogic();
    private static RoleLogic roleLogic = new RoleLogic();

    private FunctionGroupFunctionRelationLogic functionGroupRelationManager = new FunctionGroupFunctionRelationLogic(null);
    private RoleFunctionRelationLogic roleFunctionRelationManager = new RoleFunctionRelationLogic(null);
    private FunctionPageUrlRelationLogic functionPageUrlRelationLogic = new FunctionPageUrlRelationLogic(null);
    private UserRoleRelationLogic roleUserRelationLogic = new UserRoleRelationLogic();
    private UserFunctionRelationLogic userFunctionRelationManager = new UserFunctionRelationLogic(null);
    private FunctionLogic functionLogic = new FunctionLogic();

    private List<Node> MenuModule()
    {
        List<Node> menuModuleList = new List<Node>();
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

        //载入功能组、功能菜单、三级菜单
        Node rootFunctionGroup = functionGroupLogic.GetNodeTree(new FunctionGroup() { GroupID = 1 }, false);

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
            
            if (firstLevelFunctionGroup.State == 1 && (findRoleFunctions.Length > 0 || findUserFunctions.Length > 0))
            {
                menuModuleList.Add(firstLevelFunctionGroup);
            }
        }

        return menuModuleList.OrderBy(f=>f.OrderNo).ToList();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rptMenuModule.DataSource = MenuModule();
            rptMenuModule.DataBind();
        }
    }    
}