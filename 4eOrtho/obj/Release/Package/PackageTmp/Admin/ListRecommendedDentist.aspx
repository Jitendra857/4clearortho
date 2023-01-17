<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListRecommendedDentist.aspx.cs" Inherits="_4eOrtho.Admin.ListRecommendedDentist" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#' + '<%= ddlRecommendeddentist.ClientID %>').focus();
        });
    </script>

    <asp:UpdatePanel ID="upListRecommendedDentist" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label ID="lblHeader" Text="Recommended Dentist List" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
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
                                        <%--<asp:Button ID="btnAddNew" runat="server" Text="Add New" PostBackUrl="~/Admin/AddEditRecommendedDentist.aspx"></asp:Button>--%>
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                            <table class="alignright">
                                <tr>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <div class="parsonal_selectSmallSearch">
                                                <asp:DropDownList ID="ddlRecommendeddentist" runat="server">
                                                    <asp:ListItem Value="0" Text="-- Select Search Type --" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                    <asp:ListItem Value="Name" Text="Name" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                    <asp:ListItem Value="Email" Text="Email" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    <asp:ListItem Value="CountryName" Text="Country" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                                    <asp:ListItem Value="StateName" Text="State" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                                    <asp:ListItem Value="City" Text="City" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfildLarge">
                                            <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" Width="170px"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rqvSearchVal" runat="server" Display="None" ValidationGroup="searchValidation" ControlToValidate="txtSearchVal" 
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
                                        <asp:ListView ID="lvrecommendedDentist" DataSourceID="odsrecommendedDentist" runat="server" OnPreRender="lvrecommendedDentist_PreRender">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <%--<td align="left" valign="middle" class="equip" width="20%">
                                                    <asp:LinkButton ID="lnkRecommendId" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="RecommendId" Text="RecommendId" meta:resourcekey="lnkRecommendIdResource1"></asp:LinkButton>
                                                </td>--%>
                                                        <td align="left" valign="middle" class="equip" width="28%">
                                                            <asp:LinkButton ID="lnkName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Name" Text="Name" meta:resourcekey="lnkNameResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="27%">
                                                            <asp:LinkButton ID="lnkEmail" runat="server" CommandArgument="Email" OnCommand="Custom_Command"
                                                                CommandName="CustomSort" Text="Email" meta:resourcekey="lnkEmailResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:LinkButton ID="lnkCountry" runat="server" CommandArgument="CountryName" OnCommand="Custom_Command"
                                                                CommandName="CustomSort" Text="Country" meta:resourcekey="lnkCountryResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:LinkButton ID="lnkState" runat="server" CommandArgument="StateName" OnCommand="Custom_Command"
                                                                CommandName="CustomSort" Text="State" meta:resourcekey="lnkStateResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:LinkButton ID="lnkCity" runat="server" CommandArgument="City" OnCommand="Custom_Command"
                                                                CommandName="CustomSort" Text="City" meta:resourcekey="lnkCityResource1"></asp:LinkButton>
                                                        </td>
                                                        <%--<td align="left" valign="middle" class="equip" width="40%">
                                                    <asp:Literal ID="ltrAction" runat="server" Text="Action" meta:resourcekey="ltrActionResource1"></asp:Literal>
                                                </td>--%>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="5" align="right">
                                                            <asp:DataPager ID="dprecommenddentistPaging" runat="server" PagedControlID="lvrecommendedDentist">
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
                                                <%# Eval("RecommendId")%>
                                            </td>--%>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("Name")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("Email") %>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("CountryName") %>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("StateName") %>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("City") %>
                                                    </td>
                                                    <%--<td align="left" valign="top" class="equipbg">
                                                <asp:HyperLink ID="hypEdit" NavigateUrl='<%# "~/Admin/AddEditRecommendedDentist.aspx?id=" + Server.UrlEncode(Eval("RecommendId").ToString()) %>'
                                                    runat="server" ToolTip="Edit" ImageUrl="Images/edit.png" meta:resourcekey="hypEditResource1"></asp:HyperLink>
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("RecommendId") %>'
                                                    ImageUrl="~/Admin/Images/delete.png" ToolTip="Delete" Style="padding-right: 10px;" OnCommand="Custom_Command"
                                                    OnClientClick="return confirm('Are you sure you want to delete this record?');" meta:resourcekey="imgbtnDeleteResource1" />
                                            </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <%--<td align="left" valign="middle" class="equip" width="40%">
                                                    <asp:Label ID="lblRecommendId" runat="server" Text="RecommendId" meta:resourcekey="lblRecommendIdResource1"></asp:Label>
                                                </td>--%>
                                                        <td align="left" valign="middle" class="equip" width="28%">
                                                            <asp:Label ID="lblFullName" runat="server" Text="Name" meta:resourcekey="lblFullNameResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="27%">
                                                            <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:Label ID="lblCountry" runat="server" Text="Country" meta:resourcekey="lblCountryResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:Label ID="lblState" runat="server" Text="State" meta:resourcekey="lblStateResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:Label ID="lblCity" runat="server" Text="City" meta:resourcekey="lblCityResource1"></asp:Label>
                                                        </td>
                                                        <%--<td align="left" valign="middle" class="equip" width="40%">
                                                    <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource1"></asp:Label>
                                                </td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="middle" class="equipbg" colspan="10">
                                                            <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>

                                        <asp:ObjectDataSource ID="odsrecommendedDentist" runat="server" SelectMethod="GetRecommendedDentistListBySearch"
                                            SelectCountMethod="GetRecommendedDentistDataCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.Admin.ListRecommendedDentist" OnSelecting="odsrecommendedDentist_Selecting"></asp:ObjectDataSource>
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
