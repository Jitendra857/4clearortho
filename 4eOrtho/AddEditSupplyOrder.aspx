<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="AddEditSupplyOrder.aspx.cs" Inherits="_4eOrtho.AddEditSupplyOrder" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/lightbox.min.css" rel="stylesheet" />   
    <asp:UpdatePanel ID="upProductMaster" runat="server">
        <ContentTemplate>
            <div class="title main_right_cont" style="width: 100%;">
                <div class="supply-button3 back">
                    <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/ListSupplyOrder.aspx" TabIndex="7" meta:resourcekey="btnBackResource1" />
                </div>
                <h2>
                    <asp:Label ID="lblHeader" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label>
                </h2>
            </div>
            <div class="main_right_cont minheigh">
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="personal_box alignleft">
                    <asp:PlaceHolder ID="phDetail" runat="server">
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblSelect" runat="server" Text="Select" meta:resourcekey="lblSelectResource1"></asp:Label>
                                <span class="alignright">:</span>
                            </label>
                            <div class="radio-selection">
                                <asp:RadioButtonList ID="rdblSupply" runat="server" TabIndex="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdblSupply_SelectedIndexChanged" meta:resourcekey="rdblSupplyResource1">
                                    <asp:ListItem Selected="True" Text="Product" Value="1" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                    <asp:ListItem Text="Package" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="parsonal_textfild alignleft">
                            <label>
                                &nbsp;
                            </label>
                            <div class="parsonal_select">
                                <asp:DropDownList ID="ddlSuppply" runat="server" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlSuppply_SelectedIndexChanged" meta:resourcekey="ddlSuppplyResource1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqvSupply" ForeColor="Red" runat="server" ControlToValidate="ddlSuppply"
                                    SetFocusOnError="True" Display="None"
                                    ErrorMessage="Please select Product Name / Package Name" CssClass="errormsg"
                                    ValidationGroup="validation" InitialValue="0" meta:resourcekey="rqvSupplyResource1" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="rqveProductName" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rqvSupply" Enabled="True" />
                            </div>
                        </div>
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblAmount" runat="server" Text="Amount ($)" meta:resourcekey="lblAmountResource1"></asp:Label>
                                <span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtAmount" Enabled="false" CssClass="amount" runat="server" MaxLength="15" Style="text-align: right;" TabIndex="2" meta:resourcekey="txtAmountResource1" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvAmount" ForeColor="Red" runat="server" SetFocusOnError="True"
                                ControlToValidate="txtAmount" Display="None" ErrorMessage="Please enter amount."
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvAmountResource1" />
                            <asp:RegularExpressionValidator ID="rgvAmount" runat="server" ControlToValidate="txtAmount"
                                SetFocusOnError="True" ValidationExpression="\d+(\.\d{1,2})?" ValidationGroup="validation"
                                CssClass="errormsg" ErrorMessage="Only Numeric Values with two precesion values is allowed"
                                meta:resourcekey="rgvAmountResource1"></asp:RegularExpressionValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteMobile" runat="server" Enabled="True"
                                TargetControlID="txtAmount" ValidChars="0123456789." />
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveuploadPhoto" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvAmount" Enabled="True" Width="140px">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblProductAmount" runat="server" Text="Quantity" meta:resourcekey="lblProductAmountResource1"></asp:Label>
                                <span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtQuantity" runat="server" Text="1" MaxLength="15" Style="text-align: right;" TabIndex="3" meta:resourcekey="txtQuantityResource1" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:RangeValidator ID="rvQuantity" runat="server" ForeColor="Red" Display="None" SetFocusOnError="True" ControlToValidate="txtQuantity"
                                ErrorMessage="Please enter valid quantity." ValidationGroup="validation" MinimumValue="1" MaximumValue="987654321" meta:resourcekey="rvQuantityResource1"></asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="rqvQuantity" ForeColor="Red" runat="server" SetFocusOnError="True"
                                ControlToValidate="txtQuantity" Display="None" ErrorMessage="Please enter Qunatity."
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvQuantityResource1" />
                            <asp:RegularExpressionValidator ID="rgvQuantity" runat="server" ControlToValidate="txtQuantity"
                                SetFocusOnError="True" ValidationExpression="^(0|[1-9][0-9]*)$" ValidationGroup="validationProduct"
                                CssClass="errormsg" ErrorMessage="Only Numeric Values are allowed" meta:resourcekey="rgvQuantityResource1"></asp:RegularExpressionValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteQuantity" runat="server" Enabled="True"
                                TargetControlID="txtQuantity" ValidChars="0123456789" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveQuantity" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvQuantity" Enabled="True" Width="140px">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rvQuantity" Enabled="True" Width="140px">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount" meta:resourcekey="lblTotalAmountResource1"></asp:Label>
                                <span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtTotalAmount" CssClass="amount" Enabled="false" Text="0" runat="server" MaxLength="15" Style="text-align: right;" TabIndex="4"></asp:TextBox>
                        </div>
                        <div class="parsonal_textfild alignleft" id="dvRecieved" runat="server" visible="false">
                            <label>
                                <asp:Label ID="lblRecieved" runat="server" Text="Recieved?" meta:resourcekey="lblReceivedResource1"></asp:Label>
                            </label>
                            <asp:CheckBox ID="chkIsRecieved" runat="server" TabIndex="4" />
                        </div>
                        <div class="parsonal_textfild alignleft" id="dvDispatchRemarks" runat="server" visible="false">
                            <label>
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
                            </label>
                            <asp:Label ID="lblDispatchRemarks" runat="server" Text="Remarks"></asp:Label>
                        </div>
                        <div class="date2" id="dvProductImagelist" runat="server">
                        </div>
                        <div class="date2">
                            <div id="dvPackageImagelist" runat="server">
                            </div>
                            <div class="clear">
                            </div>
                            <fieldset id="flPackageDetails" runat="server" visible="false">
                                <legend><%= this.GetLocalResourceObject("PackageDetails") %></legend>
                                <asp:Repeater ID="rptPackageImage" runat="server">
                                    <HeaderTemplate>
                                        <table class="grid_table" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <span><%= this.GetLocalResourceObject("ProductName") %></span>
                                                        </span>
                                                    </div>

                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <span><%= this.GetLocalResourceObject("Quantity") %></span>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <span><%= this.GetLocalResourceObject("Amount") %></span>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <span><%= this.GetLocalResourceObject("Total") %></span>
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <div class="grenchk dark" id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("ProductName")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="grenchk dark" id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("Quantity")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="grenchk dark" id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("Amount")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="grenchk dark" id="flex">
                                                    <div class="whitetext">
                                                        <%# Convert.ToDecimal(Eval("Amount")) * Convert.ToInt32(Eval("Quantity")) %>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%-- used for showing Error Message --%>
                                        <asp:Label ID="lblErrorMsg" runat="server" CssClass="errMsg" Text="No Data Found" Visible="false">
                                        </asp:Label>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </fieldset>
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phMakePayment" runat="server" Visible="false">
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblPayableAmount" runat="server" Text="Payable Amount" meta:resourcekey="lblPayableAmountResource1"></asp:Label>
                                <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                            </label>
                            <span>$<%= txtTotalAmount.Text %></span>
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
                                <asp:Button ID="btnBackPayment" runat="server" Text="Back" Visible="false" CausesValidation="false"
                                    OnClick="btnBack_Click" TabIndex="5" meta:resourcekey="btnBackPaymentResource1" />
                            </div>
                            <div class="supply-button3">
                                <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="6" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtQuantity" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upProductMaster"
        DisplayAfter="10">
        <ProgressTemplate>
            <div class="processbar1">
                <img src="Content/images/ajax-loading.gif" alt="loading" style="top: 50%; left: 50%; position: absolute;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('<%=ddlSuppply.ClientID%>').focus();
            $('.amount').attr('readonly', 'true');
        };
        function pageLoad() {
            $('.amount').attr('readonly', 'true');
        }
    </script>
    <script type="text/javascript"  src="Scripts/lightbox-plus-jquery.min.js"></script>
</asp:Content>
