<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Study.master" AutoEventWireup="true" CodeBehind="Notes.aspx.cs" Inherits="ETMS.Studying.Study.Notes" %>

<%@ Register Src="~/Controls/Course/LeftChapterTree.ascx" TagPrefix="uc1" TagName="LeftChapterTree" %>

<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StudyPlaceHolder" runat="server">
    <script>
        var TrainingItemCourseID = '<%=TrainingItemCourseID%>';
    </script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery-3.1.1.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/layer/layer.min.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery.tmpl.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/Notes.js"></script>
    <div class="Notes-Context">
        <div class="Notes-Context-left">
            <uc1:LeftChapterTree runat="server" ID="LeftChapterTree" />
        </div>
        <div id="divContext" class="Notes-Context-right">
            <div class="Notes-coursmenu">
                <ul>
                    <li>
                        <a class="activemenu" href="javascript:void(0)" id="selectnotes_1">我的笔记</a></li>
                    <li class="Notes-split"></li>
                    <li>
                        <a href="javascript:void(0)" id="selectnotes_2">大家的笔记</a></li>
                </ul>
                <a href="javascript:void(0)" class="Notes-add">新建</a>
            </div>
            <div class="Notes-item-add">


                <div class="Notes-item-edit-title">
                    <input type="text" class="txt-title">
                </div>

                <div class="Notes-item-edit-content" id="add_content"></div>
                <div class="Notes-item-content-operating">
                    <input type="checkbox" class="bnt-share" /><span>分享</span><a href="javascript:void(0)" title="取消" class="cancel">取消</a><a href="javascript:void(0)" title="保存" class="save">保存</a>
                </div>

            </div>
            <div class="Notes-sort">
                <div class="Notes-sort-Context">
                    <span>排序：</span>
                    <ul class="Notes-sort-column">
                        <li class="selectup" sort_way="desc">日期
                        </li>
                        <%--<li sort_way="asc">日期
                        </li>--%>
                    </ul>
                </div>
            </div>
            <div class="Notes-list">
            </div>
            <div class="Notes-Context-bottom">
                <a class="Notes-More" href="javascript:void(0)">显示更多笔记</a>
            </div>
        </div>
    </div>

    <div style="display: none" id="UEContainer">
        <cc1:UEditor ID="Editor1" runat="server" Width="780px" Height="120px" ToolType="NotesAndQuestion" AutoHeightEnabled="false"></cc1:UEditor>
    </div>

    <script id="NotesItem" type="text/x-jquery-tmpl">
        <div class="Notes-item" id="${NotesID}">
            <div class="Notes-item-edit">
                <div class="Notes-item-edit-title">
                    <input type="text" class="txt-title" value="${Title}">
                </div>

                <div id="content_${NotesID}" class="Notes-item-edit-content"></div>
                <div class="Notes-item-content-operating">
                    <input type="checkbox" class="bnt-share" {{if IsPublic==1}} checked="checked" {{/if}} /><span>分享</span><a href="javascript:void(0)" title="取消" class="cancel">取消</a><a href="javascript:void(0)" title="保存" class="save">保存</a>
                </div>
            </div>
            <div class="Notes-item-content">
                <div class="Notes-item-title">
                    ${Title}
                </div>
                <div class='QuestionTmpl-item-content-show' id="show_${NotesID}">
                    <span class="span_show_hide"><span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开</span>
                    <div id="content">
                        ${html}{{html NoteContent}}

                    </div>
                </div>


            </div>
            <p class="title   u-notes-release ">
                {{if RealName}}${RealName}{{else}}${LoginName}{{/if}} &nbsp&nbsp&nbsp ${CreateTime}
            </p>
            <div class="u-studying-note-bottom">

                <ul class="Notes-item-operating">
                    {{if UserID == Current_UserID}}
                  <li class="remove" title="删除"></li>
                    <li class="update" title="修改"></li>
                    {{/if}}                 
                    
                </ul>
            </div>
        </div>
    </script>
    <script>
        $('.study_modular li').removeClass('cur').eq(5).addClass('cur');
    </script>
</asp:Content>
