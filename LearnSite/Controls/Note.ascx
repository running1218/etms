<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Note.ascx.cs" Inherits="ETMS.Studying.Controls.Note" %>

<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="cc1" %>
<script>
    var ContentID = '<%=ContentID%>';
    var TrainingItemCourseID = '<%=TrainingItemCourseID%>';
</script>
<div class="note_tab" style="height:45px;">
    <span type="1" class="cur">我的笔记</span>
    <span type="2">大家的笔记</span>
</div>
<div class="u-studying-note-new">
    <span id="NoteAdd">新建笔记<div id="noteEditor" style="width: 330px; height: 110px" hidden></div>
    </span>
    <div class="u-studying-note-new-box">
        <div class="u-studying-note-new-title">标题：<input id="txt_title" type="text" /></div>
        <div class="u-studying-note-new-editor" id="add_Notes_Content">
        </div>
        <label class="u-studying-note-new-box-share">
            <input type="checkbox" />分享
        </label>
        <div class="u-studying-note-new-submit">
            <input type="button" class="u-studying-note-btn" id="btn_note_item_add" value="提交" />
            <input type="button" class="u-studying-note-btn" id="btn_note_item_cancel" value="取消" />
        </div>
    </div>
</div>
<div class="u-studying-note-content-all" id="Noteslist">
</div>
<div class="note-more">
    <input id="btn_noteMore" type="button" value="显示更多笔记" />
</div>
<div id="UEditor" style="display: none">
    <cc1:UEditor ID="UENotes" runat="server" Width="100%" Height="100px" ToolType="NotesAndQuestion"></cc1:UEditor>
</div>


<script id="NotesItem" type="text/x-jquery-tmpl">
    <div class="u-studying-note-box" id="${NotesID}">
        <div class="u-studying-note-text" id="note_text_${NotesID}">
            <div class="u-studying-note-top">{{if RealName}}${RealName}{{else}}${LoginName}{{/if}}<span class="u-studying-note-time">${CreateTime}</span></div>

            <p class="u-studying-note-text-title">${Title}</p>
            <div class="u-studying-note-text-content-show" id="show_${NotesID}">
                <span class="u-questioncontent_show_hide"><span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开</span>
                <span id="Notecontent">${html}{{html NoteContent}}
                </span>
            </div>

            <div class="u-studying-note-bottom1">

                <ul>
                    {{if IsPublic==1}}
                <li><span class="u-studying-note-share" id="share_${NotesID}" title="取消分享"></span></li>
                    {{else}}
                <li><span class="u-studying-note-share-cancel" id="share_${NotesID}" title="分享"></span></li>
                    {{/if}}
                   
                <li><span class="u-studying-note-delete"></span></li>
                    <li><span class="u-studying-note-editor"></span></li>
                </ul>

            </div>

        </div>
        <div class="u-studying-note-text" id="note_edit_${NotesID}" style="display: none">
            <div class="u-studying-note-top">{{if RealName}}${RealName}{{else}}${LoginName}{{/if}}<span class="u-studying-note-time">${CreateTime}</span></div>
            <div class="u-studying-note-text-editor">
                <input class="u-studying-note-text-title" value="${Title}" type="text" />
                <div class="u-studying-note-new-editor" id="content_${NotesID}">
                </div>
                <label class="u-studying-note-new-box-share">
                    <input type="checkbox" />分享
            
                </label>
                <div class="u-studying-note-new-submit">
                    <input type="button" class="u-studying-note-btn" id="notesave" value="保存" />
                    <input type="button" class="u-studying-note-btn" id="notecancel" value="取消" />

                </div>
            </div>
        </div>
    </div>
</script>


<script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/Note.js"></script>



