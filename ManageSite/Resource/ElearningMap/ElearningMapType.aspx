<%@ Page Title="学习地图类型设置" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ElearningMapType.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.ElearningMap.ElearningMapType" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：资源管理系统>>学习地图管理>>学习地图类型设置
        </div>
        <h2 class="dv_title">
            学习地图类型设置
        </h2>

       <!--查找结果-->
        <div class="dv_searchlist">
           <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                   
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                CustomAllowPaging="false" ShowFooter="false" 
            >
                <Columns>
           
               
                  
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
           
        </div>

    </div>
</asp:Content>
