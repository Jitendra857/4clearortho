<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditRegistrationFees.aspx.cs" Inherits="_4eOrtho.Admin.EditRegistrationFees" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../Styles/Jquery-UI/jquery-ui-1.8.23.custom.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        var ShouldAllowPrefUpdate = true;
        window.onload = function () {
            document.getElementById('<%=txtFees.ClientID%>').focus();
        };
        function pageLoad() {
            var currentDate = new Date('<%= _4eOrtho.BAL.BaseEntity.GetServerDateTime %>');
            currentDate.setFullYear(currentDate.getFullYear() - 18, 11, currentDate.getDate());
            $('.From-Date').datepicker({
                showOn: "button",
                buttonText: "Select Date",
                buttonImage: "images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                minDate: currentDate
            });
        }
    </script>
    <style>
        .parsonal_textfild label {
            width:250px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upFees" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <h2 class="padd">
                                    <asp:Label ID="lblHeaderEdit" runat="server" Text="Case & Registration Configuration" meta:resourcekey="lblHeaderEditResource1" ></asp:Label></h2>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="widecolumn">
                    <div class="personal_box alignleft">
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblRegistrationFees" runat="server" Text="Registration Fees ($)" meta:resourcekey="lblRegistrationFeesResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtFees" runat="server" meta:resourcekey="txtFeesResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvtxtFees" runat="server" ValidationGroup="validation" ErrorMessage="Please enter fees." ControlToValidate="txtFees" Display="None" meta:resourcekey="rqvtxtFeesResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqvetxtFees" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvtxtFees" Enabled="True" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteFees" runat="server" Enabled="True"
                                TargetControlID="txtFees" ValidChars="0123456789." />
                        </div>
                    </div>
                    <div class="personal_box alignleft">
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblPromotionalDiscount" runat="server" Text="First Case Discount ($)" meta:resourcekey="lblFirstCaseDiscountResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtPromotionalDiscount" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvtxtPromotionalDiscount" runat="server" ValidationGroup="validation" ErrorMessage="Please enter promotional discount." ControlToValidate="txtPromotionalDiscount"
                                Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvtxtPromotionalDiscount" Enabled="True" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                TargetControlID="txtPromotionalDiscount" ValidChars="0123456789." />
                        </div>
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblPromotionalDate" runat="server" Text="Discount Expiry Date" meta:resourcekey="lblDiscountExpiryDateResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtDate" Enabled="false" CssClass="From-Date" runat="server" meta:resourcekey="txtFeesResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="personal_box alignleft">
                        <div class="parsonal_textfild" style="display: none;">
                            <label>
                                <asp:Label ID="lblCaseCharge" runat="server" Text="Case Charge ($)" meta:resourcekey="lblCaseChargeResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtCaseCharge" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCaseCharge" runat="server" ValidationGroup="validation" ErrorMessage="Please enter case charge." ControlToValidate="txtCaseCharge" Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rfvCaseCharge" Enabled="True" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                TargetControlID="txtCaseCharge" ValidChars="0123456789." />
                        </div>
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblRetainerCaseCharge" runat="server" Text="Retainer Case Charge ($)" meta:resourcekey="lblRetainerCaseChargeResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtRetainerCaseCharge" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRetainerCaseCharge" runat="server" ValidationGroup="validation" ErrorMessage="Please enter Retainer Case Charge." ControlToValidate="txtRetainerCaseCharge" Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rfvRetainerCaseCharge" Enabled="True" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                TargetControlID="txtRetainerCaseCharge" ValidChars="0123456789." />
                        </div>
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblReworkCaseCharge" runat="server" Text="Rework Case Charge ($)" meta:resourcekey="lblReworkCaseChargeResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtReworkCaseCharge" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtReworkCaseCharge" runat="server" ValidationGroup="validation" ErrorMessage="Please enter Rework Case Charge." ControlToValidate="txtReworkCaseCharge" Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rfvtxtReworkCaseCharge" Enabled="True" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                TargetControlID="txtReworkCaseCharge" ValidChars="0123456789." />
                        </div>
                    </div>                    
                </div>
                <div class="bottom_btn tpadd alignright" style="width: 268px;">
                    <span class="blue_btn">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="validation" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
                    </span><span class="dark_btn">
                        <input type="reset" tabindex="7" title='<%= this.GetLocalResourceObject("Reset") %>' value='<%= this.GetLocalResourceObject("Reset") %>' />
                    </span>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
