<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListState.aspx.cs" Inherits="_4eOrtho.Admin.ListState" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
    <asp:UpdatePanel ID="upStateList" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label ID="lblHeader" Text="State List" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
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
                                        <asp:Button ID="btnAddNew" runat="server" Text="Add New" PostBackUrl="~/Admin/AddEditState.aspx" meta:resourcekey="btnAddNewResource1"></asp:Button>
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                            <table class="alignright">
                                <tr>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <div class="parsonal_selectSmall">
                                                <asp:DropDownList ID="ddlState" runat="server">
                                                    <asp:ListItem Value="0" Text="-- Select Search Type --" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                    <asp:ListItem Value="CountryName" Text="Country Name" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                    <asp:ListItem Value="StateName" Text="State Name" meta:resourcekey="ListItemResource3"></asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfildLarge">
                                            <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" Width="170px"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rqvSearchVal" runat="server" Display="None" ValidationGroup="searchValidation"
                                                ControlToValidate="txtSearchVal"
                                                meta:resourcekey="rqvSearchValResource1" ErrorMessage="Please enter atleast one Search Value."></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveSearchVal" runat="server" CssClass="customCalloutStyle"
                                                TargetControlID="rqvSearchVal" Enabled="True" />--%>
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <span class="dark_btn_small">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="searchValidation" OnClick="btnSearch_Click"
                                                meta:resourcekey="btnSearchResource1"></asp:Button>

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
                                        <asp:ListView ID="lvState" DataSourceID="odsStateList" runat="server" OnPreRender="lvState_PreRender" OnItemDataBound="lvState_ItemDataBound">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <%--<td align="left" valign="middle" class="equip" width="20%">
                                                    <asp:LinkButton ID="lnkStateId" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="StateId" Text="StateId" meta:resourcekey="lnkStateIdResource1"></asp:LinkButton>
                                                </td>--%>
                                                        <td align="left" valign="middle" class="equip" width="20%">
                                                            <asp:LinkButton ID="lnkStateName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="StateName" Text="State Name" meta:resourcekey="lnkStateNameResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="20%">
                                                            <asp:LinkButton ID="lnkCountryName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="CountryName" Text="Country Name" meta:resourcekey="lnkCountryNameResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblStatus" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Satus" Text="Status" meta:resourcekey="lnkStatusResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="30%">
                                                            <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource2"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="5" align="right">
                                                            <asp:DataPager ID="dpStatePaging" runat="server" PagedControlID="lvState">
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
                                                    <%--<td align="left" valign="top" class="equipbg">
                                                <%# Eval("StateId")%>
                                            </td>--%>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("StateName")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("CountryName")%>
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:Image ID="imgStatus" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>'
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "In-Active" %>' meta:resourcekey="imgStatusResource1" />
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:HyperLink ID="hypEdit" NavigateUrl='<%# "~/Admin/AddEditState.aspx?id=" + Server.UrlEncode(Eval("StateId").ToString()) %>'
                                                            runat="server" ToolTip="Edit" ImageUrl="Images/edit.png" meta:resourcekey="hypEditResource1"></asp:HyperLink>
                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("StateId") %>'
                                                            ImageUrl="~/Admin/Images/delete.png" ToolTip="Delete" Style="padding-right: 10px;" OnCommand="Custom_Command"
                                                            OnClientClick="return DeleteMessage(this);" meta:resourcekey="imgbtnDeleteResource1" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <%--<td align="left" valign="middle" class="equip" width="10%">
                                                    <asp:Label ID="lblStateId" runat="server" Text="StateId" meta:resourcekey="lblStateIdResource1"></asp:Label>
                                                </td>--%>
                                                        <td align="left" valign="middle" class="equip" width="40%">
                                                            <asp:Label ID="lblStateName" runat="server" Text="State Name" meta:resourcekey="lblStateNameResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="40%">
                                                            <asp:Label ID="lblCountryName" runat="server" Text="Country Name" meta:resourcekey="lblCountryNameResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="40%">
                                                            <asp:Label ID="lblStauts" runat="server" Text="Status" meta:resourcekey="lblStatusResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="40%">
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
                                        <asp:ObjectDataSource ID="odsStateList" runat="server" SelectMethod="GetStateListBySearch"
                                            SelectCountMethod="GetStateDataCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.Admin.ListState" OnSelecting="odsStateList_Selecting"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="processStateList" runat="server" AssociatedUpdatePanelID="upStateList"
        DisplayAfter="10">
        <ProgressTemplate>
            <div class="processbar1">                
                <img src="../Content/images/loading.gif" alt="loading" style="top: 50%; left: 50%; position: absolute;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
