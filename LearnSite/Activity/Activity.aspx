<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Activity.master" AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="ETMS.Studying.Activity.Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/activity.css" type="text/css" rel="stylesheet" />
    <div class="view-area">
        <div class="con_title">
            <asp:Literal ID="lit_Name" runat="server"></asp:Literal>
        </div>
        <div class="con_hd">
            <div class="con_left1">
                <asp:Image ID="Picture" runat="server" />
            </div>
            <div class="con_right1">
                <div class="title">活动简介</div>
                <div class="content">
                    <asp:Literal ID="lit_Abstract" runat="server"></asp:Literal>
                </div>
                <div class="info">
                    <span>评比时间：<asp:Literal ID="lit_Start" runat="server"></asp:Literal>
                        --
                        <asp:Literal ID="lit_End" runat="server"></asp:Literal></span>
                    <span>报名人数：<asp:Literal ID="lit_LimitNum" runat="server"></asp:Literal></span>
                </div>
                <div class="con-div1">
                    <input type="button" class="con-bm <%=SiginupBtn %>" value="<%=SiginupBtnText %>" />
                </div>
            </div>
        </div>
        <div class="con_hd2">
            <input type="button" class="con-b1" />
            <input type="button" class="con-b2" />
            <input type="button" class="con-b3" />
        </div>
        <div class="con_hd3">
            <div id="tab1">
                <div class="act_l1">
                    活动形式：<img src="<%=ETMS.Utility.WebUtility.AppPath %>/Styles/images/common/study_end.jpg" /><asp:Literal ID="lit_Type" runat="server"></asp:Literal>
                    <img src="<%=ETMS.Utility.WebUtility.AppPath %>/Styles/images/common/study_end.jpg" /><asp:Literal ID="lit_Shape" runat="server"></asp:Literal>
                </div>
                <asp:Literal ID="lit_Address" runat="server"></asp:Literal>
                <div class="act_l1">
                    活动区域：
                    <asp:Repeater ID="Area_List" runat="server">
                        <ItemTemplate>
                            <span class="con-area"><%# Eval("RegionName") %></span>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="act_l1">
                    活动组别：
                    <asp:Repeater ID="Group_List" runat="server">
                        <ItemTemplate>
                            <span class="con-area"><%# Eval("GroupName") %></span>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <div class="act_opus_t">
                    <div class="act_opus_title">详细介绍</div>
                </div>
                <div class="act_oups_i">
                    <asp:Literal ID="lit_Details" runat="server"></asp:Literal>
                </div>
                <div class="act_opus_t">
                    <div class="act_opus_title">评分规则</div>
                </div>
                <div class="act_oups_i">
                    <asp:Literal ID="lit_ReviewRule" runat="server"></asp:Literal>
                </div>

            </div>
            <div id="tab2" style="display: none;">
                <%--<div class="act_opus_t">
                    <div class="act_opus_title">华北赛区</div>
                    <div class="act_opus_more"><a href="#">更多>></a></div>
                </div>
                <div class="act_oups_i">
                    <div class="act_opus1">
                        <div class="act_opus1_m1">
                            <div class="act_opus_icon">我的作品不告诉你</div>
                            <div class="act_opus_info">
                                <div class="act_opus_info_l">作者：田金宇</div>
                                <div class="act_opus_info_r">时间：2017-01-01</div>
                            </div>
                        </div>
                    </div>
                    <div class="act_opus1">
                        <div class="act_opus1_m1">
                            <div class="act_opus_icon">我的作品不告诉你</div>
                            <div class="act_opus_info">
                                <div class="act_opus_info_l">作者：田金宇</div>
                                <div class="act_opus_info_r">时间：2017-01-01</div>
                            </div>
                        </div>
                    </div>
                    <div class="act_opus1">
                        <div class="act_opus1_m1">
                            <div class="act_opus_icon">我的作品不告诉你</div>
                            <div class="act_opus_info">
                                <div class="act_opus_info_l">作者：田金宇</div>
                                <div class="act_opus_info_r">时间：2017-01-01</div>
                            </div>
                        </div>
                    </div>
                    <div class="act_opus1">
                        <div class="act_opus1_m1">
                            <div class="act_opus_icon">我的作品不告诉你</div>
                            <div class="act_opus_info">
                                <div class="act_opus_info_l">作者：田金宇</div>
                                <div class="act_opus_info_r">时间：2017-01-01</div>
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>
            <div id="tab3" style="display: none;">
                <asp:Repeater ID="PrizeRegionRepeater" runat="server" OnItemDataBound="PrizeRegionRepeater_ItemDataBound">
                    <ItemTemplate>
                        <div class="act_tab3">
                            <div style="margin: 0 auto; width: 500px;">
                                <div class="act_list_title"><%# Eval("RegionName") %>排行榜</div>
                                <div class="act_list">
                                    <table class="act_tablelist">
                                        <thead>
                                            <tr>
                                                <th class="th1">奖项</th>
                                                <th class="th2">姓名</th>
                                                <th class="th3">作品</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="PrizeRepeater" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="act_golden"><%# Eval("PrizeName") %></td>
                                                        <td><%# Eval("Name") %></td>
                                                        <td>&lt;<%# Eval("ProductName") %>&gt;</td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    <div class="act_more"><a href="#?id=<%=ActivityID %>&regionid=<%# Eval("RegionID") %>">更多&gt;&gt;</a></div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <%--<div class="act_tab3">
                    <div style="margin: 0 auto; width: 500px;">
                        <div class="act_list_title">华北区排行榜</div>
                        <div class="act_list">
                            <table class="act_tablelist">
                                <thead>
                                    <tr>
                                        <th class="th1">奖项</th>
                                        <th class="th2">姓名</th>
                                        <th class="th3">作品</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="act_golden">一等奖是什么</td>
                                        <td>田金宇田金宇</td>
                                        <td>&lt;市场在乱，我要回农村市场在乱，我要回农村&gt;</td>
                                    </tr>
                                    <tr>
                                        <td class="act_golden">一等奖是什么</td>
                                        <td>田金宇</td>
                                        <td>&lt;市场在乱，我要回农村市场在乱，我要回农村&gt;</td>
                                    </tr>
                                    <tr>
                                        <td class="act_golden">sss</td>
                                        <td>田金宇</td>
                                        <td>&lt;市场在乱，我要回农村市场在乱，我要回农村&gt;</td>
                                    </tr>
                                    <tr>
                                        <td class="act_golden">sss</td>
                                        <td>田金宇</td>
                                        <td>&lt;市场在乱，我要回农村&gt;</td>
                                    </tr>
                                    <tr>
                                        <td class="act_golden">sss</td>
                                        <td>田金宇</td>
                                        <td>&lt;市场在乱，我要回农村&gt;</td>
                                    </tr>
                                    <tr>
                                        <td class="act_golden">sss</td>
                                        <td>田金宇</td>
                                        <td>&lt;市场在乱，我要回农村&gt;</td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="act_more"><a href="#">更多&gt;&gt;</a></div>
                        </div>
                    </div>
                </div>
                <div class="act_tab3">
                    <div style="margin: 0 auto; width: 500px;">
                        <div class="act_list_title">华北区排行榜</div>
                        <div class="act_list">
                            <table class="act_tablelist">
                                <thead>
                                    <tr>
                                        <th class="th1">奖项</th>
                                        <th class="th2">姓名</th>
                                        <th class="th3">作品</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="act_golden">sss</td>
                                        <td>田金宇</td>
                                        <td>&lt;市场在乱，我要回农村&gt;</td>
                                    </tr>
                                    <tr>
                                        <td class="act_golden">sss</td>
                                        <td>田金宇</td>
                                        <td>&lt;市场在乱，我要回农村&gt;</td>
                                    </tr>
                                    <tr>
                                        <td class="act_golden">sss</td>
                                        <td>田金宇</td>
                                        <td>&lt;市场在乱，我要回农村&gt;</td>
                                    </tr>
                                    <tr>
                                        <td class="act_golden">sss</td>
                                        <td>田金宇</td>
                                        <td>&lt;市场在乱，我要回农村&gt;</td>
                                    </tr>
                                    <tr>
                                        <td class="act_golden">sss</td>
                                        <td>田金宇</td>
                                        <td>&lt;市场在乱，我要回农村&gt;</td>
                                    </tr>
                                    <tr>
                                        <td class="act_golden">sss</td>
                                        <td>田金宇</td>
                                        <td>&lt;市场在乱，我要回农村&gt;</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var activityID = "<%=ActivityID%>";
        $(".con-bm").on("click", function () {
            var root = '<%=ETMS.Utility.WebUtility.AppPath%>';
            var isAuthenticated = '<%=ETMS.Studying.BaseUtility.IsLogin%>';
            if (isAuthenticated == "True") {
                if ("<%=SiginupBtn%>" == "") {
                    layer.open({
                        type: 2,
                        title: '报名',
                        skin: 'layui-layer-rim',
                        area: ['500px', '425px'],
                        content: root + '/Activity/ActivitySignup.aspx?activityID=' + activityID
                    });
                }
            } else {
                var root = '<%=ETMS.Utility.WebUtility.AppPath%>';
                if ("<%=SiginupBtn%>" == "") {
                    layer.open({
                        type: 2,
                        title: '登录',
                        skin: 'layui-layer-rim',
                        area: ['360px', '325px'],
                        content: root + '/Login2.aspx?callbackJS=login',
                        end: function () {
                            window.location.href = window.location.href;
                        }
                    });
                }
            }
        });
        $(".con-b1").on("click", function () {
            $(".con-b1").css("background", "url('" + "<%= ETMS.Utility.WebUtility.AppPath %>" + "/Styles/images/Activity/activityindex1.png') no-repeat");
            $(".con-b2").css("background", "url('" + "<%= ETMS.Utility.WebUtility.AppPath %>" + "/Styles/images/Activity/activityzs2.png') no-repeat");
            $(".con-b3").css("background", "url('" + "<%= ETMS.Utility.WebUtility.AppPath %>" + "/Styles/images/Activity/activitygs2.png') no-repeat");
            $("#tab1").show();
            $("#tab2").hide();
            $("#tab3").hide();
        });
        $(".con-b2").on("click", function () {
            $(".con-b1").css("background", "url('" + "<%= ETMS.Utility.WebUtility.AppPath %>" + "/Styles/images/Activity/activityindex2.png') no-repeat");
            $(".con-b2").css("background", "url('" + "<%= ETMS.Utility.WebUtility.AppPath %>" + "/Styles/images/Activity/activityzs1.png') no-repeat");
            $(".con-b3").css("background", "url('" + "<%= ETMS.Utility.WebUtility.AppPath %>" + "/Styles/images/Activity/activitygs2.png') no-repeat");
            $("#tab1").hide();
            $("#tab2").show();
            $("#tab3").hide();
        });
        $(".con-b3").on("click", function () {
            $(".con-b1").css("background", "url('" + "<%= ETMS.Utility.WebUtility.AppPath %>" + "/Styles/images/Activity/activityindex2.png') no-repeat");
            $(".con-b2").css("background", "url('" + "<%= ETMS.Utility.WebUtility.AppPath %>" + "/Styles/images/Activity/activityzs2.png') no-repeat");
            $(".con-b3").css("background", "url('" + "<%= ETMS.Utility.WebUtility.AppPath %>" + "/Styles/images/Activity/activitygs1.png') no-repeat");
            $("#tab1").hide();
            $("#tab2").hide();
            $("#tab3").show();
        });
    </script>
</asp:Content>
