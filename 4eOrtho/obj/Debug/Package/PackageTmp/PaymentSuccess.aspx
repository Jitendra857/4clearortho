<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="PaymentSuccess.aspx.cs" Inherits="_4eOrtho.PaymentSuccess" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="left_title no-print" class="no-print">
        <h2>
            <asp:Label ID="lblthank" runat="server" Text="Thank you for Payment" meta:resourcekey="lblthankResource1"></asp:Label>
        </h2>
    </div>
    <div id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
    </div>
    <div class="personal_box alignleft no-print" runat="server" id="successmessage">
        <h3>
            <asp:Label ID="lblTransaction" runat="server" Text="Transaction Information" meta:resourcekey="lblTransactionResource1"></asp:Label>
        </h3>
        <div class="parsonal_textfild alignleft">
            <label>
                <asp:Label ID="lblStatus" runat="server" Text="Transaction Status" meta:resourcekey="lblStatusResource1"></asp:Label>
                <span class="alignright">:</span>
            </label>
            <asp:Label ID="lblSuccess" runat="server" Text=": Success" meta:resourcekey="lblSuccessResource1"></asp:Label>
        </div>
        <div class="clear"></div>
        <div class="parsonal_textfild alignleft" id="lblTransactionId" runat="server">
            <label>
                <asp:Label ID="lblTransId" runat="server" Text="Transaction Id" meta:resourcekey="lblTransIdResource1"></asp:Label>
                <span class="alignright">:</span>
            </label>
            <asp:Label ID="lblTransactionIdValue" runat="server" meta:resourcekey="lblTransactionIdValueResource1"></asp:Label>
        </div>
        <div class="clear"></div>
        <div class="parsonal_textfild alignleft" id="lblTransactionDate" runat="server">
            <label>
                <asp:Label ID="lblTransDate" runat="server" Text="Transaction Date" meta:resourcekey="lblTransDateResource1"></asp:Label>
                <span class="alignright">:</span>
            </label>
            <asp:Label ID="lblTransactionDateValue" runat="server" meta:resourcekey="lblTransactionDateValueResource1"></asp:Label>
        </div>
        <div class="clear"></div>
        <div class="parsonal_textfild alignleft" id="lblAmount" runat="server">
            <label>
                <asp:Label ID="lblpayamt" runat="server" Text="Amount" meta:resourcekey="lblpayamtResource1"></asp:Label>
                <span class="alignright">:</span>
            </label>
            <asp:Label ID="lblAmountValue" runat="server" meta:resourcekey="lblAmountValueResource1"></asp:Label>
        </div>
    </div>
    <div class="personal_box alignleft no-print" id="divRegistrationMsg" runat="server" visible="false">
        <h3>
            <asp:Label ID="lblAccountInformation" runat="server" Text="Registration Information"></asp:Label>
        </h3>
        <div class="parsonal_textfild alignleft">
            <label style="width: 100%;">
                <%= this.GetLocalResourceObject("RegistrationPaymentMessage") %>
            </label>
            <label style="width: 100%;">
                <asp:Literal ID="ltrRegistrationMessage" runat="server"></asp:Literal>
            </label>
        </div>
    </div>
    <div class="parsonal_textfild alignleft no-print" id="divGotoHome" runat="server" visible="false">
        <asp:HyperLink ID="hypHomePage" runat="server" NavigateUrl="~/Welcome.aspx" Text=">> Go to Home Page"></asp:HyperLink>
    </div>

    <div class="personal_box alignleft" style="background: none;" id="divCaseDetail" runat="server">
        <h3>
            <asp:Label ID="lblCaseDetail" runat="server" Text="Case Receipt"></asp:Label>
            <a href="#" onclick="printSelected()" class="alignright no-print" style='padding: 5px;'>
                <img src="Content/images/print.png" />
            </a>
        </h3>
        <div class="parsonal_textfild alignleft" style="margin-top: 10px; padding: 10px; width: 90%;">
            <label style="width: 100%;">
                <strong>To<br />
                    <asp:Label ID="lblLocalContact" Text="4eDental Local Contact" runat="server"></asp:Label></strong><br />
                <asp:Label ID="lblName" runat="server"></asp:Label><br />
                <asp:Label ID="lblAddress" runat="server"></asp:Label><br />
                <asp:Label ID="lblContact" runat="server"></asp:Label><br />
                <asp:Label ID="lblLocalEmail" runat="server"></asp:Label>
                <asp:Literal ID="ltr4eDentalAddress" runat="server"></asp:Literal>
            </label>
        </div>
        <div>
            <asp:Repeater ID="rptInvoiceDetails" runat="server" OnItemDataBound="rptInvoiceDetails_ItemDataBound">
                <HeaderTemplate>
                    <table width="100%" cellspacing="0" border="1" cellpadding="0" style="border-collapse: collapse;">
                        <tr>
                            <td align="left" valign="middle" class="parsonal_textfild" style="padding: 3px; font-weight: bold;">
                                <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                            </td>
                            <td align="center" valign="middle" class="parsonal_textfild" style="padding: 3px; font-weight: bold;">
                                <asp:Label ID="lblQty" runat="server" Text="Qty"></asp:Label>
                            </td>
                            <td align="right" valign="middle" class="parsonal_textfild" style="padding: 3px; font-weight: bold;">
                                <asp:Label ID="lblUnitPrice" runat="server" Text="Unit Price ($)"></asp:Label>
                            </td>
                            <td align="right" valign="middle" class="parsonal_textfild" style="padding: 3px; font-weight: bold;">
                                <asp:Label ID="lblTotalPrice" runat="server" Text="Amount ($)"></asp:Label>
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="left" valign="top" class="parsonal_textfild" style="padding: 3px; <%# Container.ItemIndex == (totalCaseDetails - 1) ? "" : "border-bottom-color: white" %>">
                            <%# Eval("description")%>
                        </td>
                        <td align="center" valign="top" class="parsonal_textfild" style="padding: 3px; <%# Container.ItemIndex == (totalCaseDetails - 1) ? "" : "border-bottom-color: white" %>">
                            <%# Eval("qty")%>
                        </td>
                        <td align="right" valign="top" class="parsonal_textfild" style="padding: 3px; <%# Container.ItemIndex == (totalCaseDetails - 1) ? "" : "border-bottom-color: white" %>">
                            <%# Eval("unit_price")%>
                        </td>
                        <td align="right" valign="top" class="parsonal_textfild" style="padding: 3px; <%# Container.ItemIndex == (totalCaseDetails - 1) ? "" : "border-bottom-color: white" %>">
                            <%# Eval("unit_total_amount")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>                   

                    <%--<tr id="trPackageDetails" runat="server">
                        <td align="left" valign="top" class="parsonal_textfild" style="padding: 3px;" colspan="3">
                            Package Discount ($)
                        </td>                        
                        <td align="right" valign="top" class="parsonal_textfild" style="padding: 3px;">
                            - <asp:Label ID="lblPackageDiscount" runat="server"></asp:Label>
                        </td>
                    </tr>--%>

                    <tr>
                        <td align="left" valign="top" class="parsonal_textfild" style="padding: 3px;" colspan="4"></td>
                    </tr>

                    <tr>
                        <td align="left" valign="top" class="parsonal_textfild" style="padding: 3px;" colspan="3">Shipping Charges ($)
                        </td>
                        <td align="right" valign="top" class="parsonal_textfild" style="padding: 3px;">
                            <asp:Label ID="lbl_shipping_charges" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="left" valign="top" class="parsonal_textfild" style="padding: 3px;" colspan="4"></td>
                    </tr>

                    <tr>
                        <td align="left" valign="top" class="parsonal_textfild" style="padding: 3px;" colspan="3">Total Charges ($)
                        </td>
                        <td align="right" valign="top" class="parsonal_textfild" style="padding: 3px;">
                            <asp:Label ID="lbl_total_charges" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="left" valign="top" class="parsonal_textfild" style="padding: 3px;" colspan="4"></td>
                    </tr>

                    <tr>
                        <td align="left" valign="top" class="parsonal_textfild" style="padding: 3px;" colspan="3">Total Paid
                        </td>
                        <td align="right" valign="top" class="parsonal_textfild" style="padding: 3px;">
                            <asp:Label ID="lbl_total_charges_paid" runat="server"></asp:Label>
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <div class="parsonal_textfild alignleft" style="margin-top: 10px; padding: 10px; width: 90%;">
            <label style="width: 100%;">
                <strong>
                    <asp:Label ID="Label1" Text="From" runat="server"></asp:Label></strong><br />
                <asp:Label ID="lblNamed" runat="server"></asp:Label><br />
                <asp:Label ID="lblAddressd" runat="server"></asp:Label><br />
                <asp:Label ID="lblContactd" runat="server"></asp:Label><br />
                <asp:Label ID="lblLocalEmaild" runat="server"></asp:Label>
            </label>
        </div>
    </div>

    <div class="date2 no-print">
        <div class="Search_button" style="margin-left: 0px !important;">
            <asp:Button ID="btnBack" runat="server" Text="Done" OnClick="btnBack_Click" meta:resourcekey="btnBackResource1" />
        </div>
    </div>

    <script type="text/javascript">
        function pageLoad() {
            $('.footer').addClass('no-print');
            $('.contact_footer').addClass('no-print');
            $('.right_section').addClass('no-print');
            $('.header_holder').addClass('no-print');
        }

        function printSelected() {
            $('.no-print').hide();
            $('[id$=divMsg]').hide();
            window.print();
            $('.no-print').show();
        }
    </script>
</asp:Content>
