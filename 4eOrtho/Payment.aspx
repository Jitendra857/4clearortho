<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="_4eOrtho.Payment" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="Scripts/loadingoverlay.min.js"></script>
    <link href="Styles/Ajaxtoolkit.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtCardNo').focus();
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(loadingoverlayShow);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(loadingoverlayHide);
        });
        function loadingoverlayShow() {
            $.LoadingOverlay("show", {
                image: "content/images/loader.gif"
            });
        }
        function loadingoverlayHide() {
            $.LoadingOverlay("hide", {
                image: "content/images/loader.gif"
            });
        }
        function callErrorDiv() {
            setTimeout(function () { window.location.href = "#divErrorMessage"; }, 100);
        }
    </script>
    <style>
        .information {
            background-image: url("Content/images/information.png");
            background-repeat: no-repeat;
            padding-top: 5px;
            padding-left: 35px;
            color: green;
            float: left;
            text-align: left;
        }
    </style>
</head>
<body>
    <!-- Header -->
    <form id="form1" runat="server" defaultbutton="btnSubmit">
        <asp:UpdatePanel ID="upPayment" runat="server">
            <ContentTemplate>
                <div class="header_holder headheight">
                    <div class="header in">
                        <div class="wrapper">
                            <div class="logo">
                                <a href="Welcome.aspx">
                                    <img src="Content/images/logo.png" alt="logo"></a>
                                <a href="PatientBrochureEstimate.aspx" id="aPatientBrochure" class="aPatientBrochure"
                                    style="display: none;"></a>
                            </div>
                            <div class="header_right_menu_section">
                                <div class="header_right_bottom_content">
                                    <div class="header_right_inner">
                                        <div class="header_right1">
                                            <span class="welcome"><b>
                                                <%= this.GetLocalResourceObject("Welcome").ToString() %>:
                                            </b>
                                                <asp:Label ID="lblUser" runat="server" meta:resourcekey="lblUserResource1"></asp:Label></span>
                                            <span class="logout"><a href="#">
                                                <asp:ImageButton ID="imgLogOut" ImageUrl="~/Content/images/logout.png" runat="server" OnClick="imgLogOut_Click" meta:resourcekey="imgLogOutResource1" /></a></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                </div>
                <!-- Header -->
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div id="container" class="midportion minhgt" style="min-height: 350px;">
                    <div class="wrapper">
                        <div class="bothsection">
                            <div class="patientlogin_main" style="width: 550px;">
                                <div class="middlesection" style="background: none; border: none;">
                                    <div class="doctorlogin">
                                        <h2>
                                            <strong>
                                                <asp:Literal ID="ltrPayment" runat="server" Text="Registration Fees Payment" meta:resourcekey="ltrPaymentResource1"></asp:Literal></strong>
                                        </h2>
                                        <div class="information">
                                            <asp:Label ID="ltrMessage" runat="server" meta:resourcekey="ltrMessageResource1"></asp:Label>
                                        </div>
                                    </div>
                                    <asp:PlaceHolder ID="phMakePayment" runat="server">
                                        <div class="widecolumn">
                                            <div class="personal_box alignleft" style="width: 96%; background: none; border: none;">
                                                <div class="parsonal_textfild alignleft">
                                                    <label class="commondoc">
                                                        <asp:Label ID="lblPayableAmount" runat="server" Text="Payable Amount" meta:resourcekey="lblPayableAmountResource1"></asp:Label><span class="alignright">:</span>
                                                    </label>
                                                    <span>$<asp:Literal ID="ltrFees" runat="server" meta:resourcekey="ltrFeesResource1"></asp:Literal></span>
                                                </div>
                                                <%--Payment Mode--%>
                                                <asp:PlaceHolder ID="phSelectPayment" runat="server">
                                                    <div class="clear" style="height: 5px;">
                                                    </div>
                                                    <div class="parsonal_textfild alignleft" style="text-align: center; width: 100%;">
                                                        <h2>
                                                            <strong>
                                                                <asp:Literal ID="ltrSelectMode" runat="server" Text="Select Payment Mode"></asp:Literal></strong></h2>
                                                    </div>
                                                    <div class="clear" style="height: 5px;">
                                                    </div>
                                                    <div style="width: 100%;">
                                                        <div class="parsonal_textfild alignleft" style="padding-left: 50px;">
                                                            <div class="supply-button3" style="width: 100%;">
                                                                <asp:Button ID="btnSelectPayment" runat="server" Text="Pay using Credit Card" OnClick="btnSelectPayment_Click" Style="height: 47px;" />
                                                            </div>
                                                        </div>
                                                        <div class="parsonal_textfild alignleft" style="padding-left: 50px;">
                                                            <div class="date_cont_login">
                                                                <asp:ImageButton ID="imgbtnExpressCheckout" runat="server" OnClick="imgbtnExpressCheckout_Click" ImageUrl="Content/images/btn_xpressCheckout.gif" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:PlaceHolder>
                                                <%--Payment Mode--%>
                                                <asp:PlaceHolder ID="phCreditCard" runat="server" Visible="false">
                                                    <div class="parsonal_textfild alignleft">
                                                        <label class="commondoc">
                                                            <asp:Label ID="lblCardNumber" runat="server" Text="Card Number" meta:resourcekey="lblCardNumberResource1"></asp:Label>
                                                            <span class="starcl">*</span><span class="alignright">:</span>
                                                        </label>
                                                        <asp:TextBox ID="txtCardNo" runat="server" MaxLength="19" TabIndex="1" autocomplete="off" meta:resourcekey="txtCardNoResource1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rqvCardNo" ForeColor="Red" runat="server" ControlToValidate="txtCardNo"
                                                            Display="None" ErrorMessage="Please Enter Card Number" SetFocusOnError="True"
                                                            CssClass="errormsg" ValidationGroup="validation1" meta:resourcekey="rqvCardNoResource1" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcerqvCardNo" runat="server" CssClass="customCalloutStyle"
                                                            TargetControlID="rqvCardNo" Enabled="True">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        <asp:RegularExpressionValidator ID="regCardNo" Display="None" runat="server" ValidationExpression="[0-9]{12,19}"
                                                            ValidationGroup="validation1" CssClass="errormsg" ControlToValidate="txtCardNo"
                                                            ErrorMessage="Please Enter At-least 12 Digit Card Number" SetFocusOnError="True" meta:resourcekey="regCardNoResource1"></asp:RegularExpressionValidator>
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
                                                        <label class="commondoc">
                                                            <asp:Label ID="lblNameon" runat="server" Text="Name on Card" meta:resourcekey="lblNameonResource1"></asp:Label>
                                                            <span class="starcl">*</span><span class="alignright">:</span>
                                                        </label>
                                                        <asp:TextBox ID="txtNameOnCard" runat="server"
                                                            MaxLength="50" TabIndex="2" autocomplete="off" meta:resourcekey="txtNameOnCardResource1"></asp:TextBox>
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
                                                        <label class="commondoc">
                                                            <asp:Label ID="lblCareType" runat="server" Text="Card Type" meta:resourcekey="lblCareTypeResource1"></asp:Label>
                                                            <span class="starcl">*</span><span class="alignright">:</span>
                                                        </label>
                                                        <div class="parsonal_select">
                                                            <asp:DropDownList ID="ddlCardType" runat="server" TabIndex="3" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlCardType_SelectedIndexChanged" meta:resourcekey="ddlCardTypeResource1">
                                                                <asp:ListItem Value="VISA" Text="Visa" Selected="True" meta:resourcekey="ListItemResource1" />
                                                                <asp:ListItem Value="MASTERCARD" Text="MasterCard" meta:resourcekey="ListItemResource2" />
                                                                <asp:ListItem Value="DISCOVER" Text="Discover" meta:resourcekey="ListItemResource3" />
                                                                <asp:ListItem Value="AMEX" Text="Amex" meta:resourcekey="ListItemResource4" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="parsonal_textfild alignleft">
                                                        <label class="commondoc">
                                                            <asp:Label ID="lblExpires" runat="server" Text="Expires" meta:resourcekey="lblExpiresResource1"></asp:Label>
                                                            <span class="starcl">*</span><span class="alignright">:</span>
                                                        </label>
                                                        <div class="parsonal_selectSmall">
                                                            <asp:CustomValidator runat="server" ID="custMonth" ControlToValidate="ddlMonth" Display="None"
                                                                OnServerValidate="custom_ServerMonthValidate" ValidationGroup="validation1" CssClass="error"
                                                                ErrorMessage="Please Select Current or Future Month" SetFocusOnError="True" meta:resourcekey="custMonthResource1" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecustMonth" runat="server" CssClass="customCalloutStyle"
                                                                TargetControlID="custMonth" Enabled="True">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:DropDownList ID="ddlYear" runat="server" TabIndex="4" meta:resourcekey="ddlYearResource1">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="parsonal_selectSmall">
                                                            <asp:DropDownList ID="ddlMonth" runat="server" TabIndex="5" meta:resourcekey="ddlMonthResource1">
                                                                <asp:ListItem Value="1" Text="January" meta:resourcekey="ListItemResource5" />
                                                                <asp:ListItem Value="2" Text="February" meta:resourcekey="ListItemResource6" />
                                                                <asp:ListItem Value="3" Text="March" meta:resourcekey="ListItemResource7" />
                                                                <asp:ListItem Value="4" Text="April" meta:resourcekey="ListItemResource8" />
                                                                <asp:ListItem Value="5" Text="May" meta:resourcekey="ListItemResource9" />
                                                                <asp:ListItem Value="6" Text="June" meta:resourcekey="ListItemResource10" />
                                                                <asp:ListItem Value="7" Text="July" meta:resourcekey="ListItemResource11" />
                                                                <asp:ListItem Value="8" Text="August" meta:resourcekey="ListItemResource12" />
                                                                <asp:ListItem Value="9" Text="September" meta:resourcekey="ListItemResource13" />
                                                                <asp:ListItem Value="10" Text="October" meta:resourcekey="ListItemResource14" />
                                                                <asp:ListItem Value="11" Text="November" meta:resourcekey="ListItemResource15" />
                                                                <asp:ListItem Value="12" Text="December" meta:resourcekey="ListItemResource16" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="parsonal_textfild alignleft">
                                                        <label class="commondoc">
                                                            <asp:Label ID="lblCVVCode" runat="server" Text="CVV2/CVC2 Code" meta:resourcekey="lblCVVCodeResource1"></asp:Label>
                                                            <span class="starcl">*</span><span class="alignright">:</span>
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
                                                    <div class="date2">
                                                        <div class="date_cont_login">
                                                            <div class="date_cont_right doctorlogin-submit">
                                                                <div class="supply-button3" style="width: 120px;">
                                                                    <asp:Button ID="btnSubmit" runat="server" Text="Make Payment" ValidationGroup="validation1" OnClick="btnSubmit_Click"
                                                                        TabIndex="7" meta:resourcekey="btnSubmitResource1" />
                                                                </div>
                                                                <div class="supply-button3">
                                                                    <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="8" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:PlaceHolder>
                                            </div>
                                        </div>
                                        <div class="date2">
                                            <div class="date_cont_login">
                                                <asp:Label ID="lblMessage" runat="server" Style="color: red;"></asp:Label>
                                            </div>
                                        </div>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder ID="phReviewPayment" runat="server" Visible="false">
                                        <div class="widecolumn">
                                            <div class="personal_box alignleft">
                                                <div class="parsonal_textfild alignleft">
                                                    <label>
                                                        <asp:Literal ID="ltrReviewMessage" Text="Your registration fees is" runat="server"></asp:Literal>
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="clear">
                                            </div>
                                            <div class="date2">
                                                <div class="date_cont_login">
                                                    <div class="date_cont_right doctorlogin-submit">
                                                        <div class="supply-button3" style="width: 120px;">
                                                            <asp:Button ID="btnDoExpressCheckout" runat="server" Text="Make Payment" OnClick="btnDoExpressCheckout_Click" meta:resourcekey="btnSubmitResource1" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:PlaceHolder>
                                    <div id="divErrorMessage" runat="server" visible="false">
                                        <div id="divMsg" runat="server">
                                            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                                        </div>
                                        <div class="date2">
                                            <h4 class="section-ttl">
                                                <asp:Label ID="lblErrorMsg" runat="server" Text="Error Message" meta:resourcekey="lblErrorMsgResource1"></asp:Label>
                                            </h4>
                                        </div>
                                        <div class="date2">
                                            <asp:Literal ID="ltrErrorMsg" runat="server" meta:resourcekey="ltrErrorMsgResource1"></asp:Literal>
                                        </div>
                                        <div class="date2">
                                            <p>
                                                <asp:Label ID="lblDifferentCard" runat="server"
                                                    Text="Try again with the same or a different card" meta:resourcekey="lblDifferentCardResource1"></asp:Label>
                                            </p>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hdnLookupId" runat="server" />
                <div class="wrapper">
                    <div class="footer">
                        <div class="footer_left">©<% = DateTime.Now.Year.ToString()%> 4ClearOrtho.com All Rights Reserved.</div>
                        <div class="footer_right">Powered by 4edental.com</div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script>
            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments)
                }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
            })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

            ga('create', 'UA-100101373-1', 'auto');
            ga('send', 'pageview');

        </script>
    </form>
</body>
</html>
