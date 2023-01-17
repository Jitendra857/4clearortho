<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListOrthoCharges.aspx.cs" Inherits="_4eOrtho.Admin.ListOrthoCharges" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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
    <asp:UpdatePanel ID="upOrthoCaseChargesList" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label ID="lblHeader" Text="Ortho Case Charges List" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
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
                                        <asp:Button ID="btnAddNew" runat="server" Text="Add New" PostBackUrl="~/Admin/AddEditOrthoCharges.aspx" meta:resourcekey="btnAddNewResource1"></asp:Button>
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
                                        <asp:ListView ID="lvOrthoCaseCharges" DataSourceID="odsOrthoCaseChargesList" runat="server" OnPreRender="lvOrthoCaseCharges_PreRender">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:LinkButton ID="lnkCountryName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="CountryName" Text="Country Name" meta:resourcekey="lnkCountryNameResource1"></asp:LinkButton>
                                                        </td>

                                                        <td align="right" valign="middle" class="equip" width="15%">
                                                            <asp:LinkButton ID="lnkShipmentAmount" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="ShipmentAmount" Text="Express Shipment ($)" meta:resourcekey="lnkShipmentAmountResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label ID="lblCaseNames" runat="server" Text="Case Types" meta:resourcekey="lblCaseNamesResource1"></asp:Label>
                                                        </td>
                                                        <td align="right" valign="middle" class="equip" width="15%">
                                                            <asp:LinkButton ID="lnkCaseShipmentCharge" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="CaseShipmentCharge" Text="Case Shipment ($)" meta:resourcekey="lnkCaseShipmentChargeResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="5%">
                                                            <asp:Label ID="lblStatus" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Satus" Text="Status" meta:resourcekey="lblStatusResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="8%">
                                                            <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource2"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="6" align="right">
                                                            <asp:DataPager ID="dpOrthoCaseChargesPaging" runat="server" PagedControlID="lvOrthoCaseCharges">
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
                                                        <%# Eval("CountryName")%>
                                                    </td>
                                                    <td align="right" valign="top" class="equipbg">
                                                        <%# Convert.ToDecimal(Eval("ShipmentAmount")).ToString("0.00")  %>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("CaseTypeName")%>
                                                    </td>
                                                    <td align="right" valign="top" class="equipbg">
                                                        <%# Convert.ToDecimal(Eval("CaseShipmentCharge")).ToString("0.00")%>
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:ImageButton ID="imgbtnStatus" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>'
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "In-Active" %>' OnCommand="Custom_Command" CommandArgument='<%# Eval("Id") %>' CommandName="Status" meta:resourcekey="imgbtnStatusResource1" />
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:HyperLink ID="hypEdit" NavigateUrl='<%# "~/Admin/AddEditOrthoCharges.aspx?id=" + Server.UrlEncode(_4eOrtho.Utility.CommonLogic.EncryptStringAES(Eval("Id").ToString())) %>'
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
                                                            <asp:Label ID="lblCountryName" runat="server" Text="Country Name" meta:resourcekey="lblCountryNameResource1"></asp:Label>
                                                        </td>

                                                        <td align="right" valign="middle" class="equip">
                                                            <asp:Label ID="lblShipmentAmount" runat="server" Text="Express Shipment ($)" meta:resourcekey="lblShipmentAmountResource1"></asp:Label>
                                                        </td>
                                                        <td align="right" valign="middle" class="equip">
                                                            <asp:Label ID="lblCaseType" runat="server" Text="Case Type(s)" meta:resourcekey="lblCaseTypeResource1"></asp:Label>
                                                        </td>
                                                        <td align="right" valign="middle" class="equip">
                                                            <asp:Label ID="lblCaseShipmentCharge" runat="server" Text="Case Shipment ($)" meta:resourcekey="lblCaseShipmentChargeResource1"></asp:Label>
                                                        </td>

                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblStauts" runat="server" Text="Status" meta:resourcekey="lblStautsResource1"></asp:Label>
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
                                        <asp:ObjectDataSource ID="odsOrthoCaseChargesList" runat="server" SelectMethod="GetOrthoCaseChargesList"
                                            SelectCountMethod="GetOrthoCaseChargesDataCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.Admin.ListOrthoCharges" OnSelecting="odsOrthoCaseChargesList_Selecting"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--    <asp:UpdateProgress ID="processOrthoCaseChargesList" runat="server" AssociatedUpdatePanelID="upOrthoCaseChargesList"
        DisplayAfter="10">
        <ProgressTemplate>
            <div class="processbar1">
                <img src="../Content/images/loading.gif" alt="loading" style="top: 50%; left: 50%; position: absolute;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
