<%@ WebHandler Language="C#" Class="FunctionService" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Common;

public class FunctionService : IHttpHandler {

    private static RoleLogic roleLogic = new RoleLogic();
    private static FunctionGroupLogic functionGroupLogic = new FunctionGroupLogic();
    private FunctionGroupFunctionRelationLogic functionGroupRelationManager = new FunctionGroupFunctionRelationLogic(null);
    private RoleFunctionRelationLogic roleFunctionRelationManager = new RoleFunctionRelationLogic(null);
    private FunctionPageUrlRelationLogic functionPageUrlRelationLogic = new FunctionPageUrlRelationLogic(null);
    private UserRoleRelationLogic roleUserRelationLogic = new UserRoleRelationLogic();
    private UserFunctionRelationLogic userFunctionRelationManager = new UserFunctionRelationLogic(null);
    private FunctionLogic functionLogic = new FunctionLogic();
    private HttpContext context = null;
    
    public void ProcessRequest (HttpContext context) {
        this.context = context;
        context.Response.ContentType = "text/plain";
        context.Response.Write(GetFunctions());
        context.Response.End();
    }

    private string GetFunctions()
    {
        var nodes = InitialData();
        List<GroupFunction> groupFunction = new List<GroupFunction>();

        foreach (var node in nodes)
        {
            var gf = new GroupFunction() { GroupID = node.NodeID, GroupCode = node.NodeCode, GroupName = node.NodeName, OrderNo = node.OrderNo };

            if (node.Functions != null && node.Functions.Count > 0)
            {
                gf.Functions = new List<GFunction>();
                foreach (var f in node.Functions)
                {
                    gf.Functions.Add(new GFunction() { FunctionID = f.FunctionID, FunctionName = f.FunctionName, OrderNo = f.OrderNo, PageUrl = f.PageUrl != null ? HrefUtility.ActionHrefNoControl("~/" + f.PageUrl.PageURL) : string.Empty });
                }
            }

            groupFunction.Add(gf);
        }
        return JsonHelper.GetInvokeSuccessJson(groupFunction);
    }

    public List<Node> InitialData()
    {
        List<Node> menuLevel2List = new List<Node>();
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
        
        foreach (Node secondFunctionGroup in functionGroupLogic.GetNodeTree(new FunctionGroup() { GroupID = context.Request["GroupID"].ToInt() }, false).ChildNodes)
        {
            RoleFunction[] findRoleFunctions = Array.FindAll<RoleFunction>(rolefunctions, (item) =>
            {
                return item.FunctionGroupCode.StartsWith(secondFunctionGroup.NodeCode);
            });

            UserFunction[]  findUserFunctions = Array.FindAll<UserFunction>(userfunctions, (item) =>
            {
                return item.FunctionGroupCode.StartsWith(secondFunctionGroup.NodeCode);
            });

           if (secondFunctionGroup.State == 1 && (findRoleFunctions.Length > 0 || findUserFunctions.Length > 0))
           {
               menuLevel2List.Add(secondFunctionGroup);
           }
        }

        Function[] userAllFunctions = UserAllFunctions(rolefunctions, userfunctions);
        foreach (Node node in menuLevel2List)
        {
            node.Functions = userAllFunctions.Where(s=>s.FunctionGroupID.Equals(node.NodeID) && s.Status.Equals(1)).ToList();
        }

        return menuLevel2List.Where(f=>f.Functions != null && f.Functions.Count > 0).ToList();
    }

    private Function[] UserAllFunctions(RoleFunction[] findRoleFunctions, UserFunction[] findUserFunctions)
    {
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

        foreach (var obj in sortFunctions)
        { 
            functionPageUrlRelationLogic.Manager = new Function() { FunctionID = obj.FunctionID };
            PageUrl[] pageUrls = (PageUrl[])functionPageUrlRelationLogic.GetAllMembers("");
            if (pageUrls != null && pageUrls.Length > 0)
            {                
                obj.PageUrl = pageUrls[0];
            }
        }
        return sortFunctions;
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}