<%@ Page Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="PatientStagePayment.aspx.cs" Inherits="_4eOrtho.PatientStagePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:UpdatePanel ID="upProductMaster" runat="server">
        <ContentTemplate>
            <div class="title main_right_cont" style="width: 100%;">
                <div class="supply-button3 back">
                    <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/PatientStageDetails.aspx" TabIndex="7" meta:resourcekey="btnBackResource1" />
                </div>
                <h2>
                    <asp:Label ID="lblHeader" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label>
                </h2>
            </div>
            <div class="main_right_cont minheigh">
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="personal_box alignleft" >
                   
                    <asp:PlaceHolder ID="phMakePayment" runat="server" Visible="true">
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblPayableAmount" runat="server" Text="Payable Amount" meta:resourcekey="lblPayableAmountResource1"></asp:Label>
                                <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                            </label>
                            <span> <asp:Label ID="lblamount" runat="server"  meta:resourcekey="lblCardNumberResource1">$</asp:Label></span>
                        </div>
                        <div id="divSelectPayment" runat="server">
                            <div class="clear" style="height: 5px;">
                            </div>
                            <div class="parsonal_textfild alignleft" style="text-align: center; width: 100%;">
                                <h2>
                                    <strong>
                                        <asp:Label ID="lblSelectMode" runat="server" Text="Select Payment Mode" meta:resourcekey="lblSelectModeResource1"></asp:Label></strong></h2>
                            </div>
                            <div class="clear" style="height: 5px;">
                            </div>
                            <div style="width: 100%;">
                                <div class="parsonal_textfild alignleft" style="padding-left: 110px;">
                                    <div class="supply-button3" style="width: 100%;">
                                        <asp:Button ID="btnSelectPayment" runat="server" Text="Pay using credit card" OnClick="btnSelectPayment_Click" Style="height: 45px;" meta:resourcekey="btnSelectPaymentResource1" />
                                    </div>
                                </div>
                                <div class="parsonal_textfild alignleft" style="padding-left: 50px; padding-top: 7px;">
                                    <div class="date_cont_login">
                                        <asp:ImageButton ID="imgbtnExpressCheckout" runat="server" OnClick="imgbtnExpressCheckout_Click" ImageUrl="Content/images/btn_xpressCheckout.gif" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divCreaditCard" runat="server" visible="false">
                            <div class="parsonal_textfild alignleft">
                                <label>
                                    <asp:Label ID="lblCardNumber" runat="server" Text="Card Number" meta:resourcekey="lblCardNumberResource1"></asp:Label>
                                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                                </label>
                                <asp:TextBox ID="txtCardNo" runat="server" MaxLength="19" TabIndex="0" autocomplete="off"
                                    meta:resourcekey="txtCardNoResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqvCardNo" ForeColor="Red" runat="server" ControlToValidate="txtCardNo"
                                    Display="None" ErrorMessage="Please Enter Card Number" SetFocusOnError="True"
                                    CssClass="errormsg" ValidationGroup="validation1" meta:resourcekey="rqvCardNoResource1" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerqvCardNo" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rqvCardNo" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:RegularExpressionValidator ID="regCardNo" Display="None" runat="server" ValidationExpression="[0-9]{12,19}"
                                    ValidationGroup="validation1" CssClass="errormsg" ControlToValidate="txtCardNo"
                                    ErrorMessage="Please Enter At-least 12 Digit Card Number" SetFocusOnError="True"
                                    meta:resourcekey="regCardNoResource1"></asp:RegularExpressionValidator>
                                <asp:CustomValidator runat="server" ID="custCardValidation" ControlToValidate="txtCardNo"
                                    Display="None" OnServerValidate="custom_ServerCardValidate" ValidationGroup="validation1"
                                    CssClass="error" ErrorMessage="Card No is not match with Card type, please enter valid Card Number or change Card Type"
                                    SetFocusOnError="True" meta:resourcekey="custCardValidationResource1" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcecustCardValidation" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="custCardValidation" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vceregCardNo" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="regCardNo" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbCardNo" runat="server" Enabled="True"
                                    TargetControlID="txtCardNo" ValidChars="0123456789" />
                            </div>
                            <div class="parsonal_textfild alignleft">
                                <label>
                                    <asp:Label ID="lblNameon" runat="server" Text="Name on Card" meta:resourcekey="lblNameonResource1"></asp:Label>
                                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                                </label>
                                <asp:TextBox ID="txtNameOnCard" runat="server"
                                    MaxLength="50" TabIndex="1" autocomplete="off" meta:resourcekey="txtNameOnCardResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqvNameOnCard" ForeColor="Red" runat="server" ControlToValidate="txtNameOnCard"
                                    Display="None" ErrorMessage="Please Enter Name on Credit Card" SetFocusOnError="True"
                                    CssClass="errormsg" ValidationGroup="validation1" meta:resourcekey="rqvNameOnCardResource1" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerqvNameOnCard" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rqvNameOnCard" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftNameOnCard" runat="server" Enabled="True"
                                    TargetControlID="txtNameOnCard" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                    ValidChars=" " />
                            </div>
                            <div class="parsonal_textfild alignleft">
                                <label>
                                    <asp:Label ID="lblCareType" runat="server" Text="Card Type" meta:resourcekey="lblCareTypeResource1"></asp:Label>
                                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                                </label>
                                <div class="parsonal_selectSmall">
                                    <asp:DropDownList ID="ddlCardType" runat="server" TabIndex="2" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlCardType_SelectedIndexChanged">
                                        <asp:ListItem Value="VISA" Text="Visa" Selected="True" />
                                        <asp:ListItem Value="MASTERCARD" Text="MasterCard" />
                                        <asp:ListItem Value="DISCOVER" Text="Discover" />
                                        <asp:ListItem Value="AMEX" Text="Amex" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="parsonal_textfild alignleft">
                                <label>
                                    <asp:Label ID="lblExpires" runat="server" Text="Expires" meta:resourcekey="lblExpiresResource1"></asp:Label>
                                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                                </label>
                                <div class="parsonal_selectSmall">
                                    <asp:CustomValidator runat="server" ID="custMonth" ControlToValidate="ddlMonth" Display="None"
                                        OnServerValidate="custom_ServerMonthValidate" ValidationGroup="validation1" CssClass="error"
                                        ErrorMessage="Please Select Current or Future Month" SetFocusOnError="True" meta:resourcekey="custMonthResource1" />
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcecustMonth" runat="server" CssClass="customCalloutStyle"
                                        TargetControlID="custMonth" Enabled="True">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:DropDownList ID="ddlYear" runat="server" Style="width: auto;" TabIndex="4" meta:resourcekey="ddlYearResource1">
                                    </asp:DropDownList>
                                </div>
                                <div class="parsonal_selectSmall">
                                    <asp:DropDownList ID="ddlMonth" runat="server" Style="width: auto; margin-left: 5px;" TabIndex="5" meta:resourcekey="ddlMonthResource1">
                                        <asp:ListItem Value="1" Text="January" />
                                        <asp:ListItem Value="2" Text="February" />
                                        <asp:ListItem Value="3" Text="March" />
                                        <asp:ListItem Value="4" Text="April" />
                                        <asp:ListItem Value="5" Text="May" />
                                        <asp:ListItem Value="6" Text="June" />
                                        <asp:ListItem Value="7" Text="July" />
                                        <asp:ListItem Value="8" Text="August" />
                                        <asp:ListItem Value="9" Text="September" />
                                        <asp:ListItem Value="10" Text="October" />
                                        <asp:ListItem Value="11" Text="November" />
                                        <asp:ListItem Value="12" Text="December" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="parsonal_textfild alignleft">
                                <label>
                                    <asp:Label ID="lblCVVCode" runat="server" Text="CVV2/CVC2 Code" meta:resourcekey="lblCVVCodeResource1"></asp:Label>
                                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                                </label>
                                <asp:TextBox ID="txtCCVNo" CssClass="small" runat="server" TabIndex="6" MaxLength="3"
                                    TextMode="Password" autocomplete="off" meta:resourcekey="txtCCVNoResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqvCCVNo" ForeColor="Red" runat="server" ControlToValidate="txtCCVNo"
                                    Display="None" ErrorMessage="Please Enter CCV2/CVC2 Code" SetFocusOnError="True"
                                    CssClass="errormsg" ValidationGroup="validation1" meta:resourcekey="rqvCCVNoResource1" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerqvCCVNo" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rqvCCVNo" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbCCVNo" runat="server" Enabled="True"
                                    TargetControlID="txtCCVNo" ValidChars="0123456789" />
                            </div>
                            <div class="parsonal_textfild alignleft" style="display: none;">
                                <label>
                                    <asp:Label ID="lblAutomatic" runat="server" Text="Automatic Renewal" meta:resourcekey="lblAutomaticResource1"></asp:Label>
                                </label>
                                <asp:CheckBox ID="chkAutoRenewal" runat="server" TabIndex="7" meta:resourcekey="chkAutoRenewalResource1" />
                                <label id="lblAutoRenewal" runat="server" style="float: right; width: 300px; margin-left: 5px; margin-right: 0px;">
                                </label>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                </div>
                <div class="date2" id="divButtons" runat="server">
                    <div>
                        <div>
                            <div class="supply-button3">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="validation"
                                    OnClick="btnSubmit_Click" TabIndex="5" meta:resourcekey="btnSubmitResource1" />
                            </div>
                            <div class="supply-button3" style="width: 150px;">
                                <asp:Button ID="btnMakePayment" runat="server" Text="Make Payment" ValidationGroup="validation1" Visible="false"
                                    OnClick="btnMakePayment_Click" TabIndex="5" meta:resourcekey="btnMakePaymentResource1" />
                            </div>
                            
                            <div class="supply-button3">
                                <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="6" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                            </div>
 <div class="supply-button3">
                    

     </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
       
    </asp:UpdatePanel>
   
   
</asp:Content>
