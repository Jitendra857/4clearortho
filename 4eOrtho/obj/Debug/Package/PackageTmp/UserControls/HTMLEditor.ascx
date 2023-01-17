<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HTMLEditor.ascx.cs" Inherits="_4eOrtho.UserControls.HTMLEditor" %>
<script type="text/javascript">
    tinyMCE.init({
        // General options
        <%=modeHtmlCode%>

        theme: "advanced",
        plugins: "emotions,media, preview,autolink,advhr",
        language: "<%=LanguageName%>",

        // Theme options
        theme_advanced_buttons1: "fontselect,fontsizeselect,|,forecolor,backcolor,|,undo,redo,|,charmap,emotions, media,advhr,|,preview, code",
        theme_advanced_buttons2: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,bullist,numlist,|,outdent,indent,| <%=ImageAndLink %>",
        theme_advanced_buttons3: "",

        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",

        //        content_css:"<%=StylePath %>",

        relative_urls: false,
        remove_script_host: false,
        document_base_url: "",

        // Replace values for the template plugin
        template_replace_values: {
            username: "Some User",
            staffid: "991234"
        }

    });

    var <%=editor.ClientID %>_buffer = "";

    function getEditorContent() {
        var editor = tinyMCE.activeEditor;
        return editor.getContent();
    }

    function <%=editor.ClientID %>_checkLength(editor, maxLength) {
        if (editor.getContent().length > maxLength) {
            //if (editor.getBody().innerText.length > maxLength) {
            alert("The text that you have entered is too long (" + editor.getContent().length + " characters). Please shorten it to " + maxLength + " characters long.");
            //alert("You have typed the maximum text allowed!");
            editor.setContent(buffer);
        }
        else {
            <%=editor.ClientID %>_buffer = editor.getContent();
        }
    }

</script>
<div>
    <asp:TextBox ID="editor" runat="server" TextMode="MultiLine" CssClass="HTMLEditor" Rows="15" Columns="60" style="width:77px;height:400px; background-color:#fff"></asp:TextBox>
</div>