<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListLocalContact.aspx.cs" Inherits="_4eOrtho.Admin.ListLocalContact" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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
    <asp:UpdatePanel ID="upLocalContactList" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label ID="lblHeader" Text="Local Contact User List" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
                    <div id="divMsg" runat="server">
                        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                    </div>
                </div>
                <div class="widecolumn">
                    <div class="personal_box alignleft">
                        <table class="alignleft">
                            <tr>
                                <td align="center" valign="middle">
                                    <span class="blue_btn_small">
                                        <asp:Button ID="btnAddNew" runat="server" Text="Add New" PostBackUrl="~/Admin/AddEditLocalContact.aspx" meta:resourcekey="btnAddNewResource1"></asp:Button>
                                    </span>
                                </td>
                                <td align="center" valign="middle">
                                    <span class="blue_btn_small">
                                        <asp:Button ID="btnShowAll" runat="server" Text="Show All" OnClick="btnShowAll_Click" meta:resourcekey="btnShowAllResource1"></asp:Button>
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" meta:resourcekey="pnlSearchResource1">
                            <table class="alignright">
                                <tr>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfildLarge">
                                            <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" Width="170px" meta:resourcekey="txtSearchValResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqvSearchVal" runat="server" Display="None" ValidationGroup="searchValidation"
                                                ControlToValidate="txtSearchVal" ErrorMessage="Please enter search Value." meta:resourcekey="rqvSearchValResource1"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveSearchVal" runat="server" CssClass="customCalloutStyle"
                                                TargetControlID="rqvSearchVal" Enabled="True" />
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <span class="dark_btn_small">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="searchValidation" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1"></asp:Button>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div class="clear">
                        </div>
                        <div class="list-data">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left" valign="top" class="rgt">
                                        <asp:ListView ID="lvLocalContact" DataSourceID="odsLocalContactList" runat="server" OnPreRender="lvLocalContact_PreRender">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:LinkButton ID="lnkOrganisationName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="OrganizationName" Text="Organisation Name" meta:resourcekey="lnkOrganisationNameResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:LinkButton ID="lnkFirstName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="FirstName" Text="First Name" meta:resourcekey="lnkFirstNameResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:LinkButton ID="lnkLastName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="LastName" Text="Last Name" meta:resourcekey="lnkLastNameResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="20%">
                                                            <asp:LinkButton ID="lnkEmailID" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="EmailID" Text="EmailID" meta:resourcekey="lnkEmailIDResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:LinkButton ID="lnkCountryName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="CountryName" Text="Country" meta:resourcekey="lnkCountryNameResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblStatus" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Satus" Text="Status" meta:resourcekey="lblStatusResource2"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource2"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="10" align="right">
                                                            <asp:DataPager ID="dpLocalContactPaging" runat="server" PagedControlID="lvLocalContact">
                                                                <Fields>
                                                                    <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page" NumericButtonCssClass="button-page" meta:resourcekey="NumericPagerFieldResource1" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("OrganizationName")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("FirstName")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("LastName")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("EmailID")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("CountryName")%>
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:ImageButton ID="imgbtnStatus" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>'
                                                            OnCommand="Custom_Command" CommandArgument='<%# Eval("Id") %>' CommandName="Status" 
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? this.GetLocalResourceObject("clicktoinactive"): this.GetLocalResourceObject("clicktoactive") %>'
                                                            ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) ? this.GetLocalResourceObject("clicktoinactive"): this.GetLocalResourceObject("clicktoactive") %>' />
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:HyperLink ID="hypEdit" NavigateUrl='<%# "~/Admin/AddEditLocalContact.aspx?id=" + Server.UrlEncode(_4eOrtho.Utility.CommonLogic.EncryptStringAES(Eval("Id").ToString())) %>'
                                                            runat="server" ToolTip="Edit" ImageUrl="Images/edit.png" meta:resourcekey="hypEditResource1"></asp:HyperLink>
                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("Id") %>'
                                                            ImageUrl="~/Admin/Images/delete.png" ToolTip="Delete" Style="padding-right: 10px;" OnCommand="Custom_Command"
                                                            OnClientClick="return DeleteMessage(this);" meta:resourcekey="imgbtnDeleteResource1" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label ID="lblOrganisationName" runat="server" Text="Organisation Name" meta:resourcekey="lblOrganisationNameResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label ID="lblFirstName" runat="server" Text="First Name" meta:resourcekey="lblFirstNameResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label ID="lblLastName" runat="server" Text="Last Name" meta:resourcekey="lblLastNameResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="20%">
                                                            <asp:Label ID="lblEmailID" runat="server" Text="EmailID" meta:resourcekey="lblEmailIDResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblCountryName" runat="server" Text="Country" meta:resourcekey="lblCountryNameResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblStatus" runat="server" Text="Status" meta:resourcekey="lblStatusResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="middle" class="equipbg" colspan="10">
                                                            <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                        <table>
                                        </table>
                                        <asp:ObjectDataSource ID="odsLocalContactList" runat="server" SelectMethod="GetLocalContactList"
                                            SelectCountMethod="GetLocalContactDataCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.Admin.ListLocalContact" OnSelecting="odsLocalContactList_Selecting"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
