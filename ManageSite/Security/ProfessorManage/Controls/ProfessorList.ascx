<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProfessorList.ascx.cs" Inherits="Resource_ProfessorManage_Controls_ProfessorList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：资源管理系统&gt;&gt;讲师资源库&gt;&gt;内部讲师管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            内部讲师管理
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray"  border="0" cellpadding="0" cellspacing="0">
                <tr>
                <th style="width:15%;">
                        工　　号：
                    </th>
                    <td style="width:30%;"><asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_120"></asp:TextBox></td>
                    <th style="width:15%;">
                        讲师姓名：
                    </th>
                    <td style="width:40%;">
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        <input class="btn_Search" type="button" value="查询"/><a href="javascript:hideGridview()" class="dropdownico"
                            id="Highsearch">高级搜索</a>
                    </td>
                </tr>

                <tr>
            <th>
                部　　门：
            </th>
            <td>
                            <select name="ctl00$ContentPlaceHolder1$ds_selector$ddl_Department" id="ctl00_ContentPlaceHolder1_ds_selector_ddl_Department" style="width:270px;">
	
</select>
            </td>
    
            <th>
                岗　　位：
            </th>
            <td>
                <select id="Select2" class="select_190">
                    <option>计算机硬件</option>
                    <option>计算机软件</option>
                    <option>互联网/网游</option>
                    <option>IT-管理</option>
                    <option>IT-品管、技术支持及其它</option>
                    <option>通信技术开发及应用</option>
                    <option>电子/电器/半导体/仪器仪表</option>
                    <option>销售/客服/技术支持</option>
                </select>
            </td>
        </tr>
                <tr>
                    <th>
                        讲师分类：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_ProfessorClassification1" DictionaryType="Dic_ProfessorClassification" />
                    </td>
                    <th>
                        讲师等级：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_ProfessorGrade1" DictionaryType="Dic_ProfessorGrade" />
                    </td>
                    
                </tr>
                <tr>
                    
                    <th>
                        负责课程：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="inputbox_210"></asp:TextBox>
                    </td>
              <th>
                        状　　态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_Status1" DictionaryType="Dic_Status" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">

                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <input type="button" runat="server" id="btn_Add1" class="btn_Add" value="新增" onclick="javascript:window.open('ProfessorAddInner.aspx','_self')" />
                    <input type="button" runat="server" id="btn_Add2" class="btn_Add" value="新增" onclick="javascript:showWindow('新增外聘讲师','ProfessorAddOutside.aspx')" />
                    <input type="button" class="btn_Del" value="删除" onclick="popConfirmMsg('确信要删除么','提示','');" />
                </div>
                <div class="dv_pageControl" style="float:right;">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                CustomAllowPaging="false" ShowFooter="false" 
                AutoCreateColumnInsertIndex="0"  
                onrowdatabound="CustomGridView1_RowDataBound" 
                onrowcommand="CustomGridView1_RowCommand" >
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" Width="40" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                        <% if (ProfessorType == 1)
                           { %>
                            <a href="javascript:showWindow('编辑内部讲师','ProfessorEditInner.aspx',800,380)">编辑</a><a href="javascript:showWindow('讲授课程','TeachingCourse.aspx',800,480)">课程</a><a href="javascript:showWindow('查看内部讲师','ProfessorViewInner.aspx',800,380)">查看</a><% } else { %><a href="javascript:showWindow('编辑外聘讲师','ProfessorEditOutside.aspx',800,300)">编辑</a><a href="javascript:showWindow('讲授课程','TeachingCourse.aspx',800,480)">课程</a><a href="javascript:showWindow('查看外聘讲师','ProfessorViewOutside.aspx',800,300)">查看</a><% } %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage2">
            </div>
            <script language="javascript" type="text/javascript">
                divPage2.innerHTML = divPage1.innerHTML;
            </script>
        </div>

    </div>