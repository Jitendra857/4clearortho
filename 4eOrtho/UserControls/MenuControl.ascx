<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuControl.ascx.cs" Inherits="_4eOrtho.UserControls.MenuControl" %>
<style type="text/css">
    .menu_button
    {
        display: none;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $('#aRegister').click(function () {
            $('#' + '<%=hdSubscribe.ClientID %>').val('Registered');
            $('#' + '<%=btnSubmit.ClientID %>').click();

        });

        $('#aRegisterCourse').click(function () {
            $('#' + '<%=hdSubscribe.ClientID %>').val('Registered');
            $('#' + '<%=btnSubmit.ClientID %>').click();

        });

        $('#aSubsribe').click(function () {
            alert('You have not clear the 4eClearCourse yet.please clear the 4eClearCourse to Become Provider.');
            return false;

        });
        $('#aCertified').click(function () {
            var result = confirm("Are you sure you want to become provider?");
            if (result == true) {
                $('#' + '<%=hdSubscribe.ClientID %>').val('Certified');
                $('#' + '<%=btnSubmit.ClientID %>').click();
            }
        });
    });
</script>
<div class="left_menu">
    <ul id="vert_menu">
        <asp:Label ID="lblMenuitem" runat="server"></asp:Label>
        <asp:Button ID="btnSubmit" CssClass="menu_button" runat="server" OnClick="btnSubmit_Click" />
        <asp:HiddenField ID="hdSubscribe" runat="server" Value="" />
    </ul>
</div>
