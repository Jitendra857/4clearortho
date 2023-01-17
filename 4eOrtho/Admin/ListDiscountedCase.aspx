<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListDiscountedCase.aspx.cs" Inherits="_4eOrtho.Admin.ListDiscountedCase" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../Styles/Jquery-UI/jquery-ui-1.8.23.custom.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function pageLoad() {
            $('.From-Date').datepicker({
                showOn: "button",
                buttonText: "Select Date",
                buttonImage: "../Content/images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                yearRange: "-100:+20",
                maxDate: new Date()
            });
            $('.not-edit').attr("readonly", "readonly");

            if ($('#<%=ddlFilter.ClientID%>').val() == 'CreatedDate') {
                $('#tdTextBox').css('display', 'none');
                $('#tdDoctor').css('display', 'none');
                $('#tdDate').css('display', '');
            }
            else {
                $('#tdTextBox').css('display', '');
                $('#tdDoctor').css('display', 'none');
                $('#tdDate').css('display', 'none');
            }
        }
        $(document).ready(function () {
            $('#<%=ddlFilter.ClientID%>').change(function () {
                if ($('#<%=ddlFilter.ClientID%>').val() == 'CreatedDate') {
                    $('#tdTextBox').css('display', 'none');
                    $('#tdDate').css('display', '');
                }
                else {
                    $('#tdTextBox').css('display', '');
                    $('#tdDate').css('display', 'none');
                }
            });
        });
        function DeleteMessage(obj) {
            if (confirm("<%=this.GetLocalResourceObject("DeleteMessage").ToString()%>"))
                 return true;
             else
                 return false;
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="cf">
        <div class="page_title">
            <h2 class="padd">
                <asp:Label runat="server" ID="lblNewCaseList" Text="Discounted Case List" meta:resourcekey="lblNewCaseListResource1"></asp:Label></h2>
            <div id="divMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
            </div>
        </div>
        <div class="widecolumn">
            <div class="personal_box alignleft">
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" meta:resourcekey="pnlSearchResource1">
                    <table class="alignright">
                        <tr>
                            <td align="center" valign="middle">
                                <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                    <div class="parsonal_selectSmallSearch">
                                        <asp:DropDownList ID="ddlFilter" runat="server" meta:resourcekey="ddlFilterResource1">
                                            <asp:ListItem Value="0" Text="Select Search Type" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                            <asp:ListItem Value="FirstName" Text="First Name" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                            <asp:ListItem Value="LastName" Text="Last Name" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                            <asp:ListItem Value="CreatedDate" Text="Created Date" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                            <td id="tdTextBox" align="center" valign="middle">
                                <div class="parsonal_textfildLarge">
                                    <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" Width="170px" meta:resourcekey="txtSearchValResource1"></asp:TextBox>
                                </div>
                            </td>
                            <td id="tdDate" align="center" valign="middle">
                                <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                    <asp:TextBox ID="txtDateSelect" CssClass="From-Date not-edit textfild search-datepicker" runat="server" MaxLength="50" Width="170px" meta:resourcekey="txtDateSelectResource1"></asp:TextBox>
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
                            <td align="left" valign="top" class="rgt" width="100%">
                                <asp:ListView ID="lvNewCase" runat="server" DataSourceID="odsNewCase" OnPreRender="lvNewCase_PreRender">
                                    <LayoutTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="center" valign="middle" class="equip">
                                                    <asp:LinkButton ID="lnkCreatedDate" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="CreatedDate" Text="Created Date" meta:resourcekey="lnkCreatedDateResource1" />
                                                </td>
                                                <td align="left" valign="middle" class="equip">
                                                    <asp:Label ID="lblCaseNo" runat="server" Text="Case No" meta:resourcekey="lblCaseNoResource1"></asp:Label>
                                                </td>                                                
                                                <td align="left" valign="middle" class="equip">
                                                    <asp:LinkButton ID="lnkFirstName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="FirstName" Text="First Name" meta:resourcekey="lnkFirstNameResource2" />
                                                </td>
                                                <td align="left" valign="middle" class="equip">
                                                    <asp:LinkButton ID="lnkLastName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="LastName" Text="Last Name" meta:resourcekey="lnkLastNameResource2" />
                                                </td>
                                                <td align="right" valign="middle" class="equip">
                                                    <asp:LinkButton ID="lnkTotalAmount" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="TotalAmount" Text="Total Amount ($)" meta:resourcekey="lnkTotalAmountResource1" />
                                                </td>                                                
                                                <td align="right" valign="middle" class="equip">
                                                    <asp:LinkButton ID="lnkDiscountAmt" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="DiscountAmount" Text="Discount ($)" meta:resourcekey="lnkDiscountAmtResource1" />
                                                </td>
                                                <td align="right" valign="middle" class="equip">
                                                    <asp:LinkButton ID="lnkPaymentAmount" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="PaymentAmount" Text="Payment ($)" meta:resourcekey="lnkPaymentAmountResource1" />
                                                </td>                                                
                                                <td align="center" valign="middle" class="equip">
                                                    <asp:LinkButton ID="lnkPaymentDate" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="TimeStamp" Text="Payment Date" meta:resourcekey="lnkPaymentDateResource1" />
                                                </td>
                                            </tr>
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                            <tr class="equip-paging">
                                                <td colspan="16" align="right">
                                                    <asp:DataPager ID="lvPackageDataPager" runat="server" PagedControlID="lvNewCase">
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
                                            <td align="center" valign="middle" class="equipbg">
                                                <%#Convert.ToDateTime(Eval("CreatedDate")).ToString("MM/dd/yyyy") %>
                                            </td>
                                            <td align="left" valign="middle" class="equipbg">
                                                <%#Eval("CaseNo") %>
                                            </td>                                            
                                            <td align="left" valign="middle" class="equipbg">
                                                <%# Eval("FirstName") %>
                                            </td>
                                            <td align="left" valign="middle" class="equipbg">
                                                <%#Eval("LastName") %>
                                            </td>
                                            <td align="right" valign="middle" class="equipbg">
                                                <%# Eval("TotalAmount") != null ? Convert.ToDecimal(Eval("TotalAmount")).ToString("0.00") : "" %>
                                            </td>
                                            <td align="right" valign="middle" class="equipbg">
                                                <%# Eval("DiscountAmount") != null ? Convert.ToDecimal(Eval("DiscountAmount")).ToString("0.00") : "" %>
                                            </td>
                                            <td align="right" valign="middle" class="equipbg">
                                                <%# Eval("Ammount") != null ? Convert.ToDecimal(Eval("Ammount")).ToString("0.00") : "" %>
                                            </td>
                                            <td align="center" valign="middle" class="equipbg">
                                                <%# Eval("TimeStamp") != null ? Convert.ToDateTime(Eval("TimeStamp")).ToString("MM/dd/yyyy") : "" %>
                                            </td>                                            
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="left" valign="middle" class="equip" width="12%">
                                                    <asp:Literal ID="ltrRegisteredDate" runat="server" Text="Created Date" meta:resourcekey="ltrRegisteredDateResource1"></asp:Literal>
                                                </td>
                                                <td align="left" valign="middle" class="equip" style="width: 15%">
                                                    <asp:Label ID="lblCaseId" runat="server" Text="Case No" meta:resourcekey="lblCaseIdResource1"></asp:Label>
                                                </td>                                                
                                                <td align="left" valign="middle" class="equip">
                                                    <asp:Label ID="lblFirstName" runat="server" Text="First Name" meta:resourcekey="lnkFirstNameResource1" />
                                                </td>
                                                <td align="left" valign="middle" class="equip">
                                                    <asp:Label ID="lblLastName" runat="server" Text="Last Name" meta:resourcekey="lnkLastNameResource1" />
                                                </td>
                                                <td align="left" valign="middle" class="equip" style="width: 15%">
                                                    <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount ($)" meta:resourcekey="lnkTotalAmountResource1"></asp:Label>
                                                </td>
                                                <td align="left" valign="middle" class="equip" style="width: 15%">
                                                    <asp:Label ID="lblDiscountAmt" runat="server" Text="Discount ($)" meta:resourcekey="lblDiscountAmtResource1"></asp:Label>
                                                </td>
                                                <td align="left" valign="middle" class="equip" style="width: 15%">
                                                    <asp:Label ID="lblAmount" runat="server" Text="Payment ($)" meta:resourcekey="lblAmountResource1"></asp:Label>
                                                </td>
                                                <td align="left" valign="middle" class="equip" style="width: 15%">
                                                    <asp:Label ID="lblPaymentDate" runat="server" Text="Payment Date" meta:resourcekey="lblPaymentDateResource1"></asp:Label>
                                                </td>                                                
                                            </tr>
                                            <tr>
                                                <td align="center" valign="middle" class="equipbg" colspan="15">
                                                    <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                                <asp:ObjectDataSource ID="odsNewCase" runat="server" SelectMethod="GetAllNewCase"
                                    SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                    TypeName="_4eOrtho.Admin.ListDiscountedCase" OnSelecting="odsNewCase_Selecting">
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
</asp:Content>
