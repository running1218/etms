<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Activity.master" AutoEventWireup="true" CodeBehind="MyActivityList.aspx.cs" Inherits="ETMS.Studying.Activity.MyActivityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/myactivity.css" type="text/css" rel="stylesheet" />
    <div class="view-area">
        <div class="con_my_right">
            <div class="con_my_tab">
                <span class="con_my_tab1 con_my_tab_active">参与中</span>
                <span class="con_my_tab2">已结束</span>
            </div>
            <div id="tab1">
                <asp:Repeater ID="rptGoing" runat="server">
                    <ItemTemplate>
                        <div class="con_my_a">
                            <div class="con_my_a_l">
                                <a target="_blank" href="<%=WebUtility.AppPath %>/activity/Activity.aspx?ID=<%# Eval("AppraisalID") %>"><img src="<%#Eval("ImageUrl")%>" /></a>
                            </div>
                            <div class="con_my_a_r">
                                <div class="con_my_a_title"><%#Eval("AppraisalTitle") %></div>
                                <div class="con_my_a_date">活动时间：<%#Eval("BeginTime")%>--<%#Eval("EndTime") %></div>
                                <div class="con_my_a_no">
                                    <div class="con_my_no_left">报名编号：<%# Eval("SiginupNo") %></div>
                                    <div class="con_my_no_right" date="<%# Eval("BeginTime")%>">                                
                                        <a href="MyActivity.aspx?id=<%#Eval("SiginupID") %>" target="_blank">进入活动</a>
                                    </div>
                                </div>
                            </div>
                        </div>  
                    </ItemTemplate>
                </asp:Repeater>              
            </div>
            <div id="tab2" hidden>
                <asp:Repeater ID="rptHistory" runat="server">
                    <ItemTemplate>
                        <div class="con_my_a">
                            <div class="con_my_a_l">
                                <a target="_blank" href="<%=WebUtility.AppPath %>/activity/Activity.aspx?ID=<%# Eval("AppraisalID") %>"><img src="<%#Eval("ImageUrl")%>" /></a>
                            </div>
                            <div class="con_my_a_r">
                                <div class="con_my_a_title"><%#Eval("AppraisalTitle") %></div>
                                <div class="con_my_a_date">活动时间：<%#Eval("BeginTime")%>--<%#Eval("EndTime") %></div>
                                <div class="con_my_a_no">
                                    <div class="con_my_no_left">报名编号：<%# Eval("SiginupNo") %></div>
                                    <div class="con_my_no_right">
                                        <a href="MyActivity.aspx?id=<%#Eval("SiginupID") %>" target="_blank">查看活动</a>
                                    </div>
                                </div>
                            </div>
                        </div>  
                    </ItemTemplate>
                </asp:Repeater> 
            </div>
        </div>
    </div>
    <script>
        $(".con_my_tab1").on("click", function () {
            $("#tab1").show();
            $("#tab2").hide();
            $(this).addClass('con_my_tab_active');
            $('.con_my_tab2').removeClass('con_my_tab_active');
        });
        $(".con_my_tab2").on("click", function () {
            $("#tab1").hide();
            $("#tab2").show();
            $(this).addClass('con_my_tab_active');
            $('.con_my_tab1').removeClass('con_my_tab_active');
        });

        $(document).ready(function () {
            $('.con_my_no_right').each(function () {
                var time = $(this).attr('date');
                if (new Date(time.replace(/-/g,"\/")) > new Date())
                {
                    $(this).find('a').addClass('activity-disabled').removeAttr('href').html('未开始');
                }
            })
        });
    </script>
</asp:Content>
