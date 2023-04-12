<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EvaluationMark.ascx.cs"
    Inherits="Comment_Controls_EvaluationMark" %>
<div class="dv_areaMark">
    <div class="dv_rake">
        <div class="good_score">
            <asp:Literal ID="ltlPercentMark" Text="0" runat="server"></asp:Literal>%</div>
        <h2>
            好评度</h2>
    </div>
    <div class="dv_rangeList">
        <dl>
            <dt>好评：</dt>
            <dd>
                <div class="dv_progessbar">
                    <span class="progressCell" <%=string.Format("style='width:{0}px;'",wMarkGood) %>>
                    </span>
                </div>
            </dd>
            <dd class="progressValue ">
                <%= MarkGood%></dd>
        </dl>
        <dl>
            <dt>中评： </dt>
            <dd>
                <div class="dv_progessbar">
                    <span class="progressCell" <%=string.Format("style='width:{0}px;'",wMarkGeneral) %>>
                    </span>
                </div>
            </dd>
            <dd class="progressValue ">
                <%= MarkGeneral%></dd>
        </dl>
        <dl>
            <dt>差评： </dt>
            <dd>
                <div class="dv_progessbar">
                    <span class="progressCell" <%= string.Format("style='width:{0}px;'",wMarkBad) %>>
                    </span>
                </div>
            </dd>
            <dd class="progressValue ">
                <%= MarkBad%></dd>
        </dl>
    </div>
</div>
<div style="display: none;">
    <asp:Repeater ID="repeaterItems" runat="server">
        <ItemTemplate>
            <div style="width: 250px; text-align: left; float: left;">
                <%# Eval("ItemName") %></div>
            <asp:Literal ID="ltlLink" runat="server" Text='<%# Eval("strLink") %>'></asp:Literal>
        </ItemTemplate>
    </asp:Repeater>
</div>
