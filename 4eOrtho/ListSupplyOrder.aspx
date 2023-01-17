<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="ListSupplyOrder.aspx.cs" Inherits="_4eOrtho.ListSupplyOrder" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="rightbar">
        <div class="main_right_cont" style="width: 100%;">
            <div class="title">
                <div class="supply-button2 back">
                    <asp:Button ID="btnAddProductMaster" runat="server" Text="Add Order Supply" PostBackUrl="~/AddEditSupplyOrder.aspx" meta:resourcekey="btnAddProductMasterResource1" />
                </div>
                <h2>
                    <asp:Label runat="server" ID="Label1" Text="Order Supply List" meta:resourcekey="lblProductListResource1"></asp:Label>
                </h2>
            </div>
            <div id="divMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
            </div>
            <div class="date2">
                <div class="date_cont" style="width: 46%;">
                    <div class="date_cont_right">
                        <div class="Sort_by">
                            <asp:DropDownList ID="ddlSupplyOrder" name="SupplyOrder" runat="server" CssClass="low_high_search" meta:resourcekey="ddlSupplyOrderResource1">
                                <asp:ListItem Value="0" Text="Select Search Type" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                <asp:ListItem Value="SupplyName" Text="Product / Package" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                <asp:ListItem Value="Amount" Text="Amount" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                <asp:ListItem Value="Quantity" Text="Quantity" meta:resourcekey="ListItemResource4"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="parsonal_textfild" style="padding: 0; float: right; width: 54%;">
                    <div class="date_cont_right">
                        <asp:TextBox ID="txtSearchValue" runat="server" MaxLength="50" meta:resourcekey="txtSearchValueResource1"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rqvSearchVal" runat="server" Display="None" ValidationGroup="searchValidation" ControlToValidate="txtSearchValue" ErrorMessage="Please enter atleast one Search Value."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveSearchVal" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvSearchVal" Enabled="True" />--%>
                    </div>
                    <div class="Search_button" style="margin-left: 0px;">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="searchValidation" CssClass="btn" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1"></asp:Button>
                    </div>
                </div>
            </div>
            <div class="entry_form">
                <asp:UpdatePanel ID="upProductMaster" runat="server">
                    <ContentTemplate>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grid_table">
                            <tbody>
                                <asp:ListView ID="lvSupplyOrder" runat="server" DataKeyNames="SupplyOrderId" DataSourceID="odsOrderSupply"
                                    OnPreRender="lvSupplyOrder_PreRender">
                                    <%--OnItemDataBound="lvSupplyOrder_ItemDataBound">--%>
                                    <LayoutTemplate>
                                        <tr>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkSortOrderSupply" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                            CommandArgument="SupplyName" Text="Product / Package" meta:resourcekey="lnkSortOrderSupplyResource1" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex" style="text-align: right;">
                                                    <span class="one" style="margin-left: 0px;">
                                                        <asp:Label runat="server" ID="lblAmount" Text="Amount" meta:resourcekey="lblAmountResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex" style="text-align: center;">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblQuantity" Text="Quantity" meta:resourcekey="lblQuantityResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto">
                                                <div class="topadd_f flex" style="text-align: center;">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblStatus" Text="Status" meta:resourcekey="lblStatusResource2"></asp:Label></span>
                                                </div>
                                            </td>
                                            <%--<td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblEdit" Text="Edit" meta:resourcekey="lblEditResource2"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblDelete" Text="Delete" meta:resourcekey="lblDeleteResource2"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>--%>
                                        </tr>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                        <tr>
                                            <td colspan="11" align="right" class="datapager">
                                                <div class="clearfix">
                                                    <asp:DataPager ID="lvGalleryDataPager" runat="server" PagedControlID="lvSupplyOrder" PageSize="5">
                                                        <Fields>
                                                            <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page" NumericButtonCssClass="button-page" meta:resourcekey="NumericPagerFieldResource1" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </div>
                                            </td>
                                        </tr>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("SupplyName")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="whitetext" style="text-align: right; width: 100%; margin-left: 0px !important;">
                                                        <%# Eval("Amount")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="whitetext" style="text-align: center; width: 100%;">
                                                        <%# Eval("Quantity")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="winicons" style="text-align: center; width: 100%;">
                                                        <div class="editicon grid-image">
                                                            <asp:Image ID="imgStatus" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive"))?"Content/images/icon-active.gif" :"Content/images/icon-inactive.gif" %>' ToolTip='<%# Convert.ToBoolean(Eval("IsActive"))?"Active" :"In-Active" %>' meta:resourcekey="imgStatusResource2" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <%--<td>
                                                <div class="grenchk dark drow8" id="flex">
                                                    <div class="winicons">
                                                        <div class="editicon" id="dvEdit" runat="server">
                                                            <asp:HyperLink ID="hypEdit" runat="server" NavigateUrl='<%# "AddEditSupplyOrder.aspx?id=" + Eval("SupplyOrderId") %>'
                                                                ImageUrl="Content/images/icon_img10.png" meta:resourcekey="hypEditResource1" Text="
                                            &lt;img src=&quot;Content/images/bgi/edit.png&quot; alt=&quot;edit&quot; /&gt;
                                                "></asp:HyperLink>
                                                        </div>
                                                        <div class="editicon" id="dvEditDispatch" runat="server">
                                                            <asp:HyperLink ID="imgEditDispatch" runat="server" NavigateUrl='<%# "AddEditSupplyOrder.aspx?id=" + Eval("SupplyOrderId")+"&dispatched=true" %>'
                                                                ImageUrl="~/Content/images/order_dispatched.png" Width="22" Height="22" ToolTip="Dispatched" meta:resourcekey="imgDispatchResource1"></asp:HyperLink>
                                                        </div>
                                                        <div class="editicon" id="dvEditRecieve" runat="server">
                                                            <asp:Image ImageUrl="~/Content/images/order_recieved.png" Width="22" Height="22" ID="imgEditReceived" runat="server" ToolTip="Received" meta:resourcekey="imgRecievedResource1" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="grenchk dark drow8" id="flex">
                                                    <div class="winicons">
                                                        <div class="editicon" id="dvDelete" runat="server">
                                                            <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="Content/images/icon_img09.png"
                                                                OnClientClick="return DeleteMessage(this);"
                                                                CommandName="CustomDelete" CommandArgument='<%# Eval("SupplyOrderId") %>' OnCommand="Custom_Command" meta:resourcekey="imgbtnDeleteResource2" />
                                                        </div>
                                                        <div class="editicon" id="dvDeleteDispatch" runat="server">
                                                            <asp:HyperLink ID="imgDeleteDispatch" runat="server" NavigateUrl='<%# "AddEditSupplyOrder.aspx?id=" + Eval("SupplyOrderId")+"&dispatched=true" %>'
                                                                ImageUrl="~/Content/images/order_dispatched.png" Width="22" Height="22" ToolTip="Dispatched" meta:resourcekey="imgDispatchResource1"></asp:HyperLink>
                                                        </div>
                                                        <div class="editicon" id="dvDeleteRecieve" runat="server">
                                                            <asp:Image ImageUrl="~/Content/images/order_recieved.png" Width="22" Height="22" ID="imgDeleteReceived" runat="server" ToolTip="Recieved" meta:resourcekey="imgRecievedResource1" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                    <%--<AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <div class="grenchk light" id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("SupplyName")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="grenchk light drow" id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("Amount")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="grenchk light drow" id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("Quantity")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="grenchk light drow" id="flex">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:Image ID="imgStatus" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive"))?"Content/images/icon-active.gif" :"Content/images/icon-inactive.gif" %>' ToolTip='<%# Convert.ToBoolean(Eval("IsActive"))?"Active" :"In-Active" %>' />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>--%>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblProductName" runat="server" Text="Product Name" meta:resourcekey="lblProductNameResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblProductAmount" runat="server" Text="Amount" meta:resourcekey="lblProductAmountResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblStatus" Text="Status" meta:resourcekey="lblStatusResource2"></asp:Label></span>
                                                </div>
                                            </td>
                                            <%--<td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblEdit" Text="Edit" meta:resourcekey="lblEditResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblDelete" Text="Delete" meta:resourcekey="lblDeleteResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle" class="equipbg" colspan="4">
                                                <div class="grenchk dark" id="flex">
                                                    <div class="whitetext" style="width: 100%; text-align: center;">
                                                        <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                                <asp:ObjectDataSource ID="odsOrderSupply" runat="server" SelectMethod="GetSupplyOrderDetails"
                                    SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                    TypeName="_4eOrtho.ListSupplyOrder" OnSelecting="odsOrderSupply_Selecting">
                                    <SelectParameters>
                                        <asp:Parameter Name="sortField" Type="String" />
                                        <asp:Parameter Name="sortDirection" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upProductMaster"
        DisplayAfter="10">
        <ProgressTemplate>
            <div class="processbar1">
                <img src="Content/images/ajax-loading.gif" alt="loading" style="top: 50%; left: 50%; position: absolute;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <script type="text/javascript">
        function pageLoad() {
            $('[id$=ddlSupplyOrder]').selectbox();
        }
        function DeleteMessage(obj) {
            if (confirm("<%= this.GetLocalResourceObject("DeleteMessage") %>"))
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
