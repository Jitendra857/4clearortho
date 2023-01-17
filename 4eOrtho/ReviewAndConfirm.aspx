<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="ReviewAndConfirm.aspx.cs" Inherits="_4eOrtho.ReviewAndConfirm" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upNewCase" runat="server">
        <ContentTemplate>
            <div class="rightbar">
                <div class="main_right_cont" style="width: 100%;">
                    <div class="title">
                        <h2>
                            <asp:Label runat="server" ID="lblPageHeader" Text="Review & Confirm Payment" meta:resourcekey="lblPageHeaderResource1"></asp:Label>
                        </h2>
                    </div>
                    <div id="divMsg" runat="server">
                        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                    </div>
                    <div class="personal_box alignleft">
                        <div class="parsonal_textfild alignleft" id="divCase" runat="server" visible="False">
                            <label>
                                <asp:Label ID="lblCaseCharge" runat="server" Text="  Case Charge ($)" meta:resourcekey="lblCaseChargeResource1"></asp:Label>
                                <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                            </label>
                            <asp:TextBox ID="txtCaseCharge" Text="0" ClientIDMode="Static" CssClass="amount" Enabled="False" ReadOnly="True" runat="server" meta:resourcekey="txtCaseChargeResource1"></asp:TextBox>
                        </div>

                        <div class="parsonal_textfild alignleft" id="divPackage" runat="server" visible="false">
                            <label>
                                <asp:Label ID="lblPackageAmt" runat="server" meta:resourcekey="lblPackageAmtResource1"></asp:Label>
                                <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                            </label>
                            <asp:TextBox ID="txtPackageAmt" Text="0" ClientIDMode="Static" CssClass="amount" Enabled="False" ReadOnly="True" runat="server" meta:resourcekey="txtPackageAmtResource1"></asp:TextBox>
                        </div>

                        <div id="divDiscount" runat="server" visible="False">
                            <div class="parsonal_textfild alignleft">
                                <label>
                                    <asp:Label ID="lblTotal" runat="server" Text="  Total ($)" meta:resourcekey="lblTotalResource1"></asp:Label>
                                    <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                                </label>
                                <asp:TextBox ID="txtTotalCasePackage" Text="0" ClientIDMode="Static" CssClass="amount total" Enabled="False" ReadOnly="True" runat="server" MaxLength="15" meta:resourcekey="txtTotalCasePackageResource1"></asp:TextBox>
                            </div>
                            <div class="parsonal_textfild alignleft">
                                <label>
                                    <asp:Label ID="lblDiscount" runat="server" Text="- Discount ($)" meta:resourcekey="lblDiscountResource1"></asp:Label>
                                    <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                                </label>
                                <asp:TextBox ID="txtPromoDiscount" Text="0" ClientIDMode="Static" CssClass="amount" Enabled="False" ReadOnly="True" runat="server" MaxLength="15" meta:resourcekey="txtPromoDiscountResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="parsonal_textfild alignleft" id="divExpressShipment" runat="server">
                            <label>
                                <asp:Label ID="lblExpressShipment" Text="Express Shipment" runat="server"></asp:Label>
                                <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                            </label>
                            <asp:TextBox ID="txtExpressShipment" Text="0" ClientIDMode="Static" CssClass="amount" Enabled="False" ReadOnly="True" runat="server"></asp:TextBox>
                        </div>
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblTotalPayable" runat="server" Text="  Payable Amount ($)" meta:resourcekey="lblTotalPayableResource1"></asp:Label>
                                <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                            </label>
                            <asp:TextBox ID="txtPayableAmt" Text="0" ClientIDMode="Static" CssClass="amount total" Enabled="False" ReadOnly="True" runat="server" meta:resourcekey="txtPayableAmtResource1"></asp:TextBox>
                        </div>
                        <div class="date2">
                            <div class="date_cont_login">
                                <div class="supply-button3" style="width: 120px;">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Make Payment" OnClick="btnSubmit_Click" meta:resourcekey="btnSubmitResource1" />
                                </div>
                                <div class="supply-button3">
                                    <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="8" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnProductPackage" runat="server" />
            <div id="dvPrint" style="display: none;">
                <asp:Panel ID="pnlPrint" runat="server" meta:resourcekey="pnlPrintResource1">
                    <div>
                        <h2>
                            <asp:Label ID="printHeader" Text="Patient Case Details" runat="server" meta:resourcekey="printHeaderResource1"></asp:Label></h2>
                    </div>
                    <table border="1">
                        <tr>
                            <td colspan="2">
                                <div style="font-size: 12px; float: right;">
                                    <b>
                                        <asp:Label ID="lblCreatedBy" runat="server" Text="Doctor Name:" meta:resourcekey="lblCreatedByResource1"></asp:Label></b>
                                    <asp:Label ID="lblPrintCreated" runat="server" meta:resourcekey="lblPrintCreatedResource1"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblCreatedDate" runat="server" Text="Created Date:" meta:resourcekey="lblCreatedDateResource1"></asp:Label>
                                    <asp:Label ID="lblPrintcreatedDate" runat="server" meta:resourcekey="lblPrintcreatedDateResource1"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right">
                                        <b>
                                            <asp:Label ID="Label1" runat="server" Text="Case No" meta:resourcekey="Label1Resource1"></asp:Label>
                                        </b>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right">
                                        <asp:Label ID="lblPrintCaseNo" runat="server" Style="margin-left: 4px" meta:resourcekey="lblPrintCaseNoResource1"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right">
                                        <b>
                                            <asp:Label ID="lblPrintPN1" runat="server" Text="Patient Name" meta:resourcekey="lblPrintPN1Resource1"></asp:Label>
                                        </b>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right">
                                        <asp:Label ID="lblPrintPN" runat="server" Style="margin-left: 4px" meta:resourcekey="lblPrintPNResource1"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right ">
                                        <b>
                                            <asp:Label ID="lblPrintDN1" runat="server" Text="Doctor Name" meta:resourcekey="lblPrintDN1Resource1"></asp:Label></b>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right ">
                                        <asp:Label ID="lblPrintDN" runat="server" Style="margin-left: 4px" meta:resourcekey="lblPrintDNResource1"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right ">
                                        <b>
                                            <asp:Label ID="lblPrintDOB1" runat="server" Text="Date Of Birth" meta:resourcekey="lblPrintDOB1Resource1"></asp:Label></b>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right ">
                                        <asp:Label ID="lblPrintDOB" runat="server" Style="margin-left: 4px" meta:resourcekey="lblPrintDOBResource1"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right ">
                                        <b>
                                            <asp:Label ID="lblPrintGender1" runat="server" Text="Gender" meta:resourcekey="lblPrintGender1Resource1"></asp:Label></b>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right ">
                                        <asp:Label ID="lblPrintGender" runat="server" Style="margin-left: 4px" meta:resourcekey="lblPrintGenderResource1"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right ">
                                        <b>
                                            <asp:Label ID="lblPrintOS1" runat="server" Text="Ortho System" meta:resourcekey="lblPrintOS1Resource1"></asp:Label></b>
                                    </div>
                                </div>
                            </td>
                            <td>

                                <div class="date_cont">
                                    <div class="date_cont_right ">
                                        <asp:Literal ID="ltrPrintOS" runat="server" meta:resourcekey="ltrPrintOSResource1"></asp:Literal>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right ">
                                        <b>
                                            <asp:Label ID="lblPrintOC" runat="server" Text="Ortho Condition" meta:resourcekey="lblPrintOCResource1"></asp:Label></b>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="date_cont" style="width: 200px;">
                                    <div class="date_cont_right " style="width: 365px;">
                                        <asp:Literal ID="ltrPrintOC" runat="server" meta:resourcekey="ltrPrintOCResource1"></asp:Literal>
                                        <asp:Literal ID="ltrPrintOther" runat="server" meta:resourcekey="ltrPrintOtherResource1"></asp:Literal>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="date_cont">
                                    <div class="date_cont_right ">
                                        <b>
                                            <asp:Label ID="lblPrintNotes" runat="server" Text="Notes/Instructions" meta:resourcekey="lblPrintNotesResource1"></asp:Label></b>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="date_cont" style="width: 200px;">
                                    <div class="date_cont_right " style="width: 365px;">
                                        <asp:Literal ID="ltrPrintNotes" runat="server" meta:resourcekey="ltrPrintNotesResource1"></asp:Literal>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
