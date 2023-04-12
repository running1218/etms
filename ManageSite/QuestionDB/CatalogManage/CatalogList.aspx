<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="CatalogList.aspx.cs" Inherits="QuestionDB_CatalogManage_CatalogList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：题库管理&gt;&gt;试题资源&gt;&gt;试题分类
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            试题分类
        </h2>
       
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('教室基本信息','CatalogAdd.aspx')" />
                    <input type="button" class="btn_Del" value="删除" onclick="popConfirmMsg('确信要删除么','提示','');" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                CustomAllowPaging="false" ShowFooter="false" 
                AutoCreateColumnInsertIndex="1" onrowdatabound="CustomGridView1_RowDataBound">
                <Columns>
          <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" Width="40" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号">
                        <ItemStyle HorizontalAlign="Center" Width="60" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                    
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel">
            </div>
        </div>

        <div class="dv_submit">
           
           <a href="SubjectList.aspx" class="btn_Cancel">返回</a>
        </div>
    </div>
</asp:Content>

