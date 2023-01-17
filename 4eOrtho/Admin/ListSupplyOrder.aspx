<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListSupplyOrder.aspx.cs" Inherits="_4eOrtho.Admin.ListSupplyOrder" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#' + '<%= ddlSupplyOrder.ClientID %>').focus();
            $('#dvYesNo').css("display", "none");

        });
        function pageLoad() {
            $('#' + '<%= ddlSupplyOrder.ClientID %>').change(function () {
                if (this.value == "Is Dispatched?" || this.value == "Is Received?") {
                    $('#dvSearch').css("display", "none");
                    $('#dvYesNo').css("display", "");
                }
                else {
                    if (this.value == "0") {
                        $('#' + '<%= txtSearchValue.ClientID %>').val("");
                }
                $('#dvSearch').css("display", "");
                $('#dvYesNo').css("display", "none");
            }
            });
        if ($('#' + '<%= ddlSupplyOrder.ClientID %>').val() == "Is Dispatched?" || $('#' + '<%= ddlSupplyOrder.ClientID %>').val() == "Is Received?") {
                $('#dvSearch').css("display", "none");
                $('#dvYesNo').css("display", "");
            }
            else {
                $('#dvSearch').css("display", "");
                $('#dvYesNo').css("display", "none");
            }
        }
    </script>

    <asp:UpdatePanel ID="upProductMaster" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label runat="server" ID="lblProductList" Text="Order Supply List" meta:resourcekey="lblProductListResource1"></asp:Label></h2>
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
                                        <%--<asp:Button ID="btnAddProductMaster" runat="server" Text="Add Order Supply" PostBackUrl="~/Admin/AddEditSupplyOrder.aspx" meta:resourcekey="btnAddProductMasterResource1" />--%>
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" meta:resourcekey="pnlSearchResource1">
                            <table class="alignright">
                                <tr>
                                    <td align="center" valign="middle" style="width: 182px">
                                        <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <div class="parsonal_selectSmall">
                                                <asp:DropDownList ID="ddlSupplyOrder" runat="server" meta:resourcekey="ddlSupplyOrderResource1">
                                                    <asp:ListItem Value="0" Text="-- Select Search Type --" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                    <asp:ListItem Value="SupplyName" Text="Product / Package" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                    <asp:ListItem Value="Amount" Text="Amount" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    <asp:ListItem Value="Quantity" Text="Quantity" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                                    <asp:ListItem Value="TotalAmount" Text="Total Amount" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                                    <asp:ListItem Value="Is Dispatched?" Text="Is Dispatched" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                                    <asp:ListItem Value="Is Received?" Text="Is Recieved?" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td align="center" valign="middle" style="width: 182px">
                                        <div class="parsonal_textfildLarge" id="dvSearch">
                                            <asp:TextBox ID="txtSearchValue" runat="server" MaxLength="50" Width="170px" meta:resourcekey="txtSearchValueResource1"></asp:TextBox>
                                        </div>
                                        <div class="radio-selection" id="dvYesNo">
                                            <asp:RadioButtonList ID="rblYesNo" runat="server" RepeatDirection="Horizontal" Width="170" meta:resourcekey="rblYesNoResource1" CellPadding="0" CellSpacing="0">
                                                <asp:ListItem Selected="True" meta:resourcekey="rblYesNoListItemResource1" Value="1"></asp:ListItem>
                                                <asp:ListItem meta:resourcekey="rblYesNoListItemResource2" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </td>

                                    <td align="center" valign="middle" style="width: 71px">
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
                                        <asp:ListView ID="lvSupplyOrder" runat="server" DataKeyNames="SupplyOrderId" DataSourceID="odsOrderSupply"
                                            OnPreRender="lvSupplyOrder_PreRender" OnItemDataBound="lvSupplyOrder_ItemDataBound">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:LinkButton ID="lnkSortOrderSupply" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="SupplyName" Text="Product / Package" meta:resourcekey="lnkSortOrderSupplyResource1" />
                                                        </td>
                                                         <td align="left" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblorderername" Text="Ordered By" meta:resourcekey="lblorderernameResource1"  ></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblAmount" Text="Amount" meta:resourcekey="lblAmountResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblQuantity" Text="Quantity" meta:resourcekey="lblQuantityResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblTotalAmount" Text="Total Amount" meta:resourcekey="ListItemResource7"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblIsCase" Text="Is Case?" meta:resourcekey="lblIsCaseResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblIsDispatch" Text="Is Dispatched?" meta:resourcekey="lblIsDispatchResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblRecieved" Text="Is Recieved?" meta:resourcekey="lblRecievedResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource2"></asp:Label>
                                                        </td>

                                                        <td align="center" valign="middle" class="equip" width="15%">
                                                            <asp:Label runat="server" ID="lblAction" Text="Action" meta:resourcekey="lblActionResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="10" align="right">
                                                            <asp:DataPager ID="lvSupplyOrderDataPager" runat="server" PagedControlID="lvSupplyOrder">
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
                                                    <td align="left" valign="middle" class="equipbg">
                                                        <%# Eval("SupplyName")%>
                                                    </td>
                                                     <td align="left" valign="middle" class="equipbg">
                                                        <%# Eval("OrdererName")%>
                                                    </td>
                                                    <td align="right" valign="middle" class="equipbg">
                                                        <%# Eval("Amount")%>
                                                    </td>
                                                    <td align="left" valign="middle" class="equipbg">
                                                        <%# Eval("Quantity")%>
                                                    </td>
                                                    <td align="right" valign="middle" class="equipbg">
                                                        <%# Eval("TotalAmount")%>
                                                    </td>
                                                     <td align="center" valign="middle" class="equipbg">
                                                        <%--<%# Eval("IsDispatch")%>--%>
                                                        <%# Convert.ToBoolean(Eval("IsCase")) ? "<b>Yes</b>" : "No" %>                                                        
                                                    </td>
                                                    <td align="center" valign="middle" class="equipbg">
                                                        <%--<%# Eval("IsDispatch")%>--%>
                                                        <%--<%# Convert.ToBoolean(Eval("IsDispatch")) ? "Yes" : "No" %>--%>
                                                        <asp:Image ID="imgIsDispatch" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsDispatch")) ? "Images/icon-active.gif" : "Images/delete.png" %>'
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsDispatch")) ? this.GetLocalResourceObject("Yes").ToString() : this.GetLocalResourceObject("No").ToString() %>' 
                                                            ToolTip='<%# Convert.ToBoolean(Eval("IsDispatch")) ? this.GetLocalResourceObject("Yes").ToString() : this.GetLocalResourceObject("No").ToString() %>' />
                                                    </td>
                                                    <td align="center" valign="middle" class="equipbg">
                                                        <%--<%# Eval("IsDispatch")%>--%>
                                                        <%--<%# Convert.ToBoolean(Eval("IsRecieved")) ? "Yes" : "No" %>--%>
                                                        <asp:Image ID="imgIsReceived" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsRecieved")) ? "Images/icon-active.gif" : "Images/delete.png" %>'
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsRecieved")) ? this.GetLocalResourceObject("Yes").ToString() : this.GetLocalResourceObject("No").ToString() %>' 
                                                            ToolTip='<%# Convert.ToBoolean(Eval("IsDispatch")) ? this.GetLocalResourceObject("Yes").ToString() : this.GetLocalResourceObject("No").ToString() %>' />
                                                    </td>
                                                    <td align="center" valign="middle" class="equipbg">
                                                        <asp:Image ID="imgStatus" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>'
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "In-Active" %>' meta:resourcekey="imgStatusResource1" />
                                                    </td>
                                                    <td align="center" valign="middle" class="equipbg">
                                                        <asp:HyperLink ID="hypEdit" runat="server" NavigateUrl='<%# "AddEditSupplyOrder.aspx?id=" + Eval("SupplyOrderId") %>'
                                                            meta:resourcekey="hypEditResource1" Text="
                                            &lt;img src=&quot;images/edit.png&quot; alt=&quot;edit&quot; /&gt;
                                                "></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label ID="lblProductName" runat="server" Text="Product/Package" meta:resourcekey="lnkSortOrderSupplyResource1"></asp:Label>
                                                        </td>
                                                         <td align="left" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblorderername" Text="Ordered By" ></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label ID="lblProductAmount" runat="server" Text="Amount" meta:resourcekey="lblAmountResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblQuantity" Text="Quantity" meta:resourcekey="lblQuantityResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label ID="Label1" runat="server" Text="Total Amount" meta:resourcekey="ListItemResource7"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblIsCase" Text="Is Case?" meta:resourcekey="lblIsCaseResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblDispatch" Text="Is Dispatch" meta:resourcekey="lblIsDispatchResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblRecieved" Text="Is Recieved" meta:resourcekey="lblRecievedResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource2"></asp:Label>
                                                        </td>

                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblActive" Text="Action" meta:resourcekey="lblActiveResource1"></asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="middle" class="equipbg" colspan="8">
                                                            <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                        <asp:ObjectDataSource ID="odsOrderSupply" runat="server" SelectMethod="GetSupplyOrderDetails"
                                            SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.Admin.ListSupplyOrder" OnSelecting="odsOrderSupply_Selecting">
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

    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upProductMaster"
        DisplayAfter="10">
        <ProgressTemplate>
            <div class="processbar1">
                <img src="../Content/images/loading.gif" alt="loading" style="top: 50%; left: 50%; position: absolute;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
