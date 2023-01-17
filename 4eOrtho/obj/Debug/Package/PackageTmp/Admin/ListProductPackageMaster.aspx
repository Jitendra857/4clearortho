<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListProductPackageMaster.aspx.cs" Inherits="_4eOrtho.Admin.ListProductPackageMaster" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#' + '<%= btnAddProductMaster.ClientID %>').focus();
        });
        function DeleteMessage(obj) {
            if (confirm("<%= this.GetLocalResourceObject("DeleteMessage") %>"))
                return true;
            else
                return false;
        }
    </script>

    <asp:UpdatePanel ID="upPackageMaster" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label runat="server" ID="lblPackageList" Text="Product Package List" meta:resourcekey="lblPackageListResource1"></asp:Label></h2>
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
                                        <asp:Button ID="btnAddProductMaster" runat="server" Text="Add Package" OnClick="btnAddProductMaster_Click" meta:resourcekey="btnAddProductMasterResource1" />
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" meta:resourcekey="pnlSearchResource1">
                            <table class="alignright">
                                <tr>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <div class="parsonal_selectSmallSearch">
                                                <asp:DropDownList ID="ddlProductPackageMaster" runat="server" meta:resourcekey="ddlProductPackageMasterResource1">
                                                    <asp:ListItem Value="0" Text="-- Select Search Type --" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                    <asp:ListItem Value="PackageName" Text="Package Name" meta:resourcekey="ListItemResource2"></asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfildLarge">
                                            <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" Width="170px" meta:resourcekey="txtSearchValResource1"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rqvSearchVal" runat="server" Display="None" ValidationGroup="searchValidation" ControlToValidate="txtSearchVal" ErrorMessage="Please enter atleast one Search Value." meta:resourcekey="rqvSearchValResource1"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveSearchVal" runat="server" CssClass="customCalloutStyle"
                                                TargetControlID="rqvSearchVal" Enabled="True" />--%>
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
                        <div class="list-data">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left" valign="top" class="rgt" width="100%">
                                        <asp:ListView ID="lvPackageMaster" runat="server" DataKeyNames="PackageId" DataSourceID="odsPackageMaster"
                                            OnPreRender="lvPackageMaster_PreRender" OnItemDataBound="lvPackageMaster_ItemDataBound">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:LinkButton ID="lnkSortPackageName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="PackageName" Text="Package Name" meta:resourcekey="lnkSortPackageNameResource1" />
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblAmount" Text="Amount" meta:resourcekey="lblAmountResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblIsCase" Text="Is Case?" meta:resourcekey="lblIsCaseResource2"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource2"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="15%">
                                                            <asp:Label runat="server" ID="lblAction" Text="Edit" meta:resourcekey="lblEditResource2"></asp:Label>
                                                            <%--<asp:Label runat="server" ID="lblDelete" Text="Delete" meta:resourcekey="lblDeleteResource2"></asp:Label>--%>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="5" align="right">
                                                            <asp:DataPager ID="lvPackageDataPager" runat="server" PagedControlID="lvPackageMaster">
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
                                                    <td align="left" valign="middle" class="equipbg" runat="server" id="tdPackageName"
                                                        onclick="PackageItemSelected(<%# btnSelectRow.ClientID%>)">
                                                        <%# Eval("PackageName")%>
                                                    </td>
                                                    <td align="right" valign="middle" class="equipbg" runat="server" id="tdAmount">
                                                        <%# Eval("Amount")%>
                                                    </td>
                                                    <td align="center" valign="middle" class="equipbg" runat="server" id="td1">
                                                        <%# Convert.ToBoolean(Eval("IsCase")) ? "<b>Yes</b>" : "No" %>
                                                    </td>
                                                    <td align="center" valign="middle" class="equipbg" runat="server" id="tdIsActive">
                                                        <asp:Image ID="imgStatus" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>'
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "In-Active" %>' meta:resourcekey="imgStatusResource1" />
                                                    </td>
                                                    <td align="center" valign="middle" class="equipbg">
                                                        <asp:ImageButton ID="hypEdit" ToolTip="Edit" runat="server" ImageUrl="Images/edit.png"
                                                            meta:resourcekey="hypEditResource1"
                                                            CommandName="CustomEdit" CommandArgument='<%# Eval("PackageId") %>' OnCommand="Custom_Command" />
                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Admin/Images/delete.png"
                                                            OnClientClick="return DeleteMessage(this);"
                                                            CommandName="CustomDelete" CommandArgument='<%# Eval("PackageId") %>' OnCommand="Custom_Command" meta:resourcekey="imgbtnDeleteResource1" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label ID="lblPackageName" runat="server" Text="Package Name" meta:resourcekey="lblPackageNameResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label ID="lblPackageAmount" runat="server" Text="Amount" meta:resourcekey="lblPackageAmountResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblIsCase" Text="Is Case?" meta:resourcekey="lblIsCaseResource2"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="15%">
                                                            <asp:Label runat="server" ID="lblAction" Text="Action" meta:resourcekey="lblActionResource1"></asp:Label>
                                                            <%-- <asp:Label runat="server" ID="lblDelete" Text="Delete" meta:resourcekey="lblDeleteResource1"></asp:Label>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="middle" class="equipbg" colspan="5">
                                                            <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                        <asp:ObjectDataSource ID="odsPackageMaster" runat="server" SelectMethod="GetPackageMasterDetails"
                                            SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.Admin.ListProductPackageMaster" OnSelecting="odsPackageMaster_Selecting">
                                            <SelectParameters>
                                                <asp:Parameter Name="sortField" Type="String" />
                                                <asp:Parameter Name="sortDirection" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPackageMaster"
        DisplayAfter="10">
        <ProgressTemplate>
            <div class="processbar1">
                <img src="../Content/images/loading.gif" alt="loading" style="top: 50%; left: 50%; position: absolute;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
