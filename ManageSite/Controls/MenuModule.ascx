<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuModule.ascx.cs" Inherits="Controls_MenuModule" %>
<asp:Repeater ID="rptMenuModule" runat="server">
    <HeaderTemplate>
        <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li id="<%# Eval("NodeID") %>" class="ico-menu Func<%# Eval("NodeCode") %>" accesskey="<%# Eval("NodeCode") %>"><%# Eval("NodeName") %></li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>

<script lang="javascript" type="text/javascript">
    $(document).ready(function () {
        $('.headeright ul li').click(function () {
            $('.active-module').removeClass('active-module');
            $(this).addClass('active-module');
            gotoUrl('<%=ETMS.Utility.WebUtility.AppPath%>' + '/Home.aspx', 'myframe', this);
            loadFunctions(this)
        });

        $('.headeright ul li:first').trigger('click');        
    });

    function loadFunctions(obj)
    {
        $('#dv_Menu').empty();
        var id = $(obj).attr('id');
        $.ajax({
            url: '<%=ETMS.Utility.WebUtility.AppPath%>' + '/services/functionService.ashx',
            type: 'POST',
            data: { GroupID: id },
            dataType: "json",
            async: false,
            success: function (result) {
                $("#group_fuction").tmpl(result).appendTo('#dv_Menu');
            },
            error: function (err) {
                
            }
        });

        registerEvent();
    }

    function registerEvent()
    {
        $('.menu-level3-function').click(function () {
            $('.menu-level3-function').each(function () { $(this).removeClass('active-function'); });
            $(this).addClass('active-function');
        });

        $('.menu-level2').click(function () {
            if ($(this).hasClass('menu-level2-toggle')) {
                $(this).removeClass('menu-level2-toggle').addClass('menu-level2-collapse');
                $('#f_' + $(this).attr('id')).fadeOut(500);
            }
            else {
                $(this).removeClass('menu-level2-collapse').addClass('menu-level2-toggle');
                $('#f_' + $(this).attr('id')).fadeIn(500);
            }
        });
    }
</script>
<script id="group_fuction" type="text/x-jquery-tmpl">
    {{each Data}} 
    <div class="menu-level2 menu-level2-toggle" id="${GroupID}">${GroupName}</div>
    <ul class="menu-level3" id="f_${GroupID}">
        {{each Functions}}
        <li class="menu-level3-function" onclick="<%= string.Format("gotoUrl('{0}', 'myframe', this);", "${PageUrl}") %>">
            <span class="arrow-left">${FunctionName}</span>
        </li>
        {{/each}}
    </ul>
    {{/each}}              
</script>