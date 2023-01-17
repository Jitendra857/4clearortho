<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ListContentPage.aspx.cs" Inherits="_4eOrtho.Admin.ListContentPage" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function DeleteMessage(obj) {
            if (confirm("<%= this.GetLocalResourceObject("DeleteMessage") %>"))
                 return true;
             else
                 return false;
         }
    </script>
    <div id="container" class="cf">
        <div class="page_title">
            <h2 class="padd">
                <asp:Label ID="lblHeader" Text="Page Content List" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
            <div id="divMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
            </div>
        </div>
        <div class="widecolumn">
            <div class="personal_box  alignleft">
                <table class="alignleft">
                    <tr>
                        <td align="center" valign="middle">
                            <span class="blue_btn_small">
                                <asp:Button ID="btnAddNew" runat="server" Text="Add New" PostBackUrl="~/Admin/AddEditContentPage.aspx" meta:resourcekey="btnAddNewResource1"></asp:Button>
                            </span>
                        </td>
                    </tr>
                </table>
                <div class="clear">
                </div>
                <div class="list-data">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" valign="top" class="rgt">
                                <asp:Repeater ID="rptListContentPage" runat="server" OnItemCommand="rptListContentPage_ItemCommand" OnItemDataBound="rptListContentPage_ItemDataBound">
                                    <HeaderTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="left" valign="middle" class="equip" width="50%">
                                                    <asp:Label ID="lblMenu" runat="server" Text="Content Page" meta:resourcekey="lblMenuResource1"></asp:Label>
                                                </td>
                                                <td align="center" valign="middle" class="equip" width="25%">
                                                    <asp:Label ID="lblStatus" runat="server" Text="Status" meta:resourcekey="lblStatusResource1"></asp:Label>
                                                </td>
                                                <td align="center" valign="middle" class="equip" width="25%">
                                                    <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource1"></asp:Label>
                                                </td>

                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td align="left" valign="middle" class="equipbg row" width="50%">
                                                <asp:Label ID="lblMenuItem" runat="server" Text='<%# Eval("MenuItem") %>' meta:resourcekey="lblMenuItemResource2"></asp:Label>
                                            </td>
                                            <td align="center" valign="middle" class="equipbg" width="25%">
                                                <img id="imgStatus" runat="server" src='<%# Eval("Status").ToString().Equals("True") ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>'
                                                    title="Active"/>
                                                &nbsp;<img id="imgRequiredAuth" runat="server" src="Images/closed.png"
                                                    title="Required Authentication" visible="False" />
                                            </td>
                                            <td align="center" valign="middle" class="equipbg" width="20%">
                                                <asp:HyperLink ID="hypEdit" NavigateUrl='<%# "AddEditContentPage.aspx?id=" + Eval("PageID") + "&lid=" + Server.UrlEncode(Eval("LanguageID").ToString()) %>'
                                                    runat="server" ToolTip="Edit" ImageUrl="Images/edit.png" meta:resourcekey="hypEditResource2"></asp:HyperLink>
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("PageID") %>'
                                                    ImageUrl="~/Admin/Images/delete.png" OnClientClick="return DeleteMessage(this);" ToolTip="Delete" meta:resourcekey="imgbtnDeleteResource2" />
                                                <a id="orderlink" runat="server" visible="False" class="iframe" href="javascript:void(0);">
                                                    <img src='Images/Order_Menu_Items.png' alt="Order Menu Item" title="Order Menu Items" /></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td align="left" valign="middle" class="equipbg row" width="50%">
                                                <asp:Label ID="lblMenuItem" runat="server" Text='<%# Eval("MenuItem") %>' meta:resourcekey="lblMenuItemResource1"></asp:Label></td>
                                            <td align="center" valign="middle" class="equipbg" width="25%">
                                                <img id="imgStatus" runat="server" src='<%# Eval("Status").ToString().Equals("True") ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>' title="Active" />

                                                &nbsp;<img id="imgRequiredAuth" runat="server" src="Images/closed.png"
                                                    title="Required Authentication" visible="False" />
                                            </td>
                                            <td align="center" valign="middle" class="equipbg" width="25%">
                                                  <asp:HyperLink ID="hypEdit" NavigateUrl='<%# "AddEditContentPage.aspx?id=" + Eval("PageID") + "&lid=" + Server.UrlEncode(Eval("LanguageID").ToString()) %>'
                                                    runat="server" ToolTip="Edit" ImageUrl="Images/edit.png" meta:resourcekey="hypEditResource1"></asp:HyperLink>
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("PageID") %>'
                                                    ImageUrl="~/Admin/Images/delete.png" OnClientClick="return DeleteMessage(this);" ToolTip="Delete" meta:resourcekey="imgbtnDeleteResource1" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
