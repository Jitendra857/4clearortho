<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminTopControl.ascx.cs" Inherits="_4eOrtho.UserControls.AdminTopControl" %>

<script type="text/javascript">
    function Test() {
        this.focus(false);
        return false;
    }
</script>
<style type="text/css">
    div ul li a:hover {
        text-decoration: underline;
    }
</style>
<div id="header" class="cf">
    <div class="cf" id="divlogo">
        <!-- start logo -->
        <h1 id="logo">
            <a href="#" onclick="return Test();"></a>
        </h1>
        <div class="right_cont">
            <div class="top_nav cf">
                <ul class="alignright">
                    <li>
                        <asp:DropDownList ID="ddlLanguage" class="dropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" meta:resourcekey="ddlLanguageResource1">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <asp:LinkButton ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" meta:resourcekey="btnLogoutResource1"></asp:LinkButton>                        
                    </li>
                </ul>
            </div>
            <div class="alignright userinfo">
                <asp:ImageButton ID="imgResponse" runat="server" ImageUrl="../Admin/Images/Conatct_notification.gif"
                    AlternateText='Request Informtaion notification' OnClick="imgResponse_Click"
                    Style="" />
                <%= this.GetLocalResourceObject("Welcome")%>
                <asp:Literal ID="lblLoggedUserName" runat="server" meta:resourcekey="lblLoggedUserNameResource1"></asp:Literal>
            </div>
        </div>
    </div>
</div>

