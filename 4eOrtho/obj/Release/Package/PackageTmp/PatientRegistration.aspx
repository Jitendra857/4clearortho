<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientRegistration.aspx.cs" Inherits="_4eOrtho.PatientRegistration" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<!DOCTYPE html>

<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>4ClearOrtho - Patient Registration</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script type="text/javascript" src="Scripts/jquery-1.7.1.min.js"></script>
    <link href="Styles/Ajaxtoolkit.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
    <link href="Styles/flexslider.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="Styles/Jquery-UI/jquery-ui-1.8.23.custom.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/Colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="Scripts/Colorbox/colorbox.css" rel="stylesheet" />
    <script src="Scripts/loadingoverlay.min.js"></script>
</head>
<body>
    <!-- Header -->
    <div class="header_holder headheight">
        <div class="header in">
            <div class="wrapper">
                <div class="logo">
                    <a href="Home.aspx">
                        <img src="Content/images/logo.png" alt="logo"></a>
                </div>
                <div class="inner_links">
                    <ul id="inner_links">
                        <li><a href="Home.aspx">Home</a></li>
                        <li><a href="PatientLogin.aspx">Patient</a></li>
                        <li><a href="DoctorLogin.aspx">Doctor</a></li>
                    </ul>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!-- Header -->
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="5000" EnablePageMethods="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="upRegister" runat="server">
            <ContentTemplate>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div id="container" class="midportion minhgt" style="min-height: 380px;">
                    <div class="wrapper">
                        <div class="bothsection">
                            <div class="patientlogin_main patientreg_main">
                                <div class="middlesection">
                                    <div class="doctorlogin">
                                        <h2>
                                            <asp:Label ID="lblHeader" runat="server" Text="Patient Registration" meta:resourcekey="lblHeaderResource1"></asp:Label>
                                        </h2>
                                    </div>
                                    <div class="widecolumn">
                                        <div class="personal_box">
                                            <div class="parsonal_textfild">
                                                <label class="commondoc">
                                                    <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                    <span class="asteriskclass">*</span><span class="alignright">:</span>
                                                </label>
                                                <asp:TextBox ID="txtEmail" runat="server" TabIndex="1" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                                                <asp:CustomValidator ID="cvtxtEmail" runat="server" ForeColor="Red" ControlToValidate="txtEmail"
                                                    Display="None" ErrorMessage="Email Already Exist." CssClass="errormsg"
                                                    SetFocusOnError="True" ValidationGroup="validation" OnServerValidate="cvtxtEmail_ServerValidate" meta:resourcekey="cvtxtEmailResource1"></asp:CustomValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcecvtxtEmail" runat="server" CssClass="customCalloutStyle"
                                                    TargetControlID="cvtxtEmail" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtEmail" ForeColor="Red" runat="server" ControlToValidate="txtEmail"
                                                    Display="None" ErrorMessage="Please enter Email Id." CssClass="errormsg"
                                                    SetFocusOnError="True" ValidationGroup="validation" meta:resourcekey="rfvtxtEmailResource1" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtEmail" runat="server" CssClass="customCalloutStyle"
                                                    TargetControlID="rfvtxtEmail" Enabled="True" />
                                                <asp:RegularExpressionValidator ID="rgvEmailAddressCheck" Display="None" runat="server"
                                                    SetFocusOnError="True" ValidationExpression="[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,5}"
                                                    ValidationGroup="validation" CssClass="errormsg" ControlToValidate="txtEmail"
                                                    ErrorMessage="Please Enter Valid Email ID eg: 'abc@yahoo.com'" meta:resourcekey="rgvEmailAddressCheckResource1"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rgveCalltxtEmailAddressCheck" runat="server"
                                                    CssClass="customCalloutStyle" TargetControlID="rgvEmailAddressCheck" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </div>
                                            <div class="parsonal_textfild">
                                                <label class="commondoc">
                                                    <asp:Label ID="lblFirstName" runat="server" Text="First Name" meta:resourcekey="lblFirstNameResource1"></asp:Label>
                                                    <span class="asteriskclass">*</span><span class="alignright">:</span>
                                                </label>
                                                <asp:TextBox ID="txtFirstName" runat="server" TabIndex="1" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFirstName" ForeColor="Red" runat="server" ControlToValidate="txtFirstName"
                                                    Display="None" ErrorMessage="Please enter first name" CssClass="errormsg"
                                                    SetFocusOnError="True" ValidationGroup="validation" meta:resourcekey="rfvFirstNameResource1" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vceFirstName" runat="server" CssClass="customCalloutStyle"
                                                    TargetControlID="rfvFirstName" Enabled="True" />
                                            </div>
                                            <div class="parsonal_textfild">
                                                <label class="commondoc">
                                                    <asp:Label ID="lblLastName" runat="server" Text="Last Name" meta:resourcekey="lblLastNameResource1"></asp:Label>
                                                    <span class="asteriskclass">*</span><span class="alignright">:</span>
                                                </label>
                                                <asp:TextBox ID="txtLastName" runat="server" TabIndex="2" meta:resourcekey="txtLastNameResource1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtLastName" ForeColor="Red" runat="server" ControlToValidate="txtLastName"
                                                    Display="None" ErrorMessage="Please enter last name" CssClass="errormsg"
                                                    SetFocusOnError="True" ValidationGroup="validation" meta:resourcekey="rfvtxtLastNameResource1" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vceLastName" runat="server" CssClass="customCalloutStyle"
                                                    TargetControlID="rfvtxtLastName" Enabled="True" />
                                            </div>
                                            <div class="parsonal_textfild">
                                                <label class="commondoc">
                                                    <asp:Label ID="lblPassword" runat="server" Text="Password" meta:resourcekey="lblPasswordResource1"></asp:Label>
                                                    <span class="asteriskclass">*</span><span class="alignright">:</span>
                                                </label>
                                                <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" TabIndex="3" TextMode="Password"
                                                    autocomplete="off" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftePassword" runat="server" Enabled="True"
                                                    TargetControlID="txtPassword" ValidChars="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+-=.,<>?;:'/|`~{}" />
                                                <asp:RequiredFieldValidator ID="rqvtxtPassword" ForeColor="Red" runat="server" ControlToValidate="txtPassword"
                                                    SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Password" CssClass="errormsg"
                                                    ValidationGroup="validation" meta:resourcekey="rqvtxtPasswordResource1" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rqveCalltxtPassword" runat="server" CssClass="customCalloutStyle"
                                                    TargetControlID="rqvtxtPassword" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="rqvPassword" runat="server" ControlToValidate="txtPassword"
                                                    SetFocusOnError="True" Display="None" ErrorMessage="Password must be 7 characters long with at least 4 alphabets and 3 digits."
                                                    ValidationExpression="(?=(.*[0-9]){3,})(?=(.*[a-zA-Z]){4,})^[0-9a-zA-Z!@#$%^&*()_+-=.,<>?;:'/|`~{}]{7,50}$"
                                                    ValidationGroup="validation" meta:resourcekey="rqvPasswordResource1" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rqvePassword" runat="server" CssClass="customCalloutStyle"
                                                    TargetControlID="rqvPassword" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </div>
                                            <div class="parsonal_textfild">
                                                <label class="commondoc">
                                                    <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" meta:resourcekey="lblConfirmPasswordResource1"></asp:Label>
                                                    <span class="asteriskclass">*</span><span class="alignright">:</span>
                                                </label>
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="50"
                                                    TabIndex="4" autocomplete="off" meta:resourcekey="txtConfirmPasswordResource1"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteConfirmPassword" runat="server" Enabled="True"
                                                    TargetControlID="txtConfirmPassword" ValidChars="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+-=.,<>?;:'/|`~{}" />
                                                <asp:RequiredFieldValidator ID="rqvtxtConfirmPassword" ForeColor="Red" runat="server"
                                                    SetFocusOnError="True" ControlToValidate="txtConfirmPassword" Display="None"
                                                    ErrorMessage="Please Enter Confirm Password" CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvtxtConfirmPasswordResource1" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rqveCalltxtConfirmPassword" runat="server"
                                                    CssClass="customCalloutStyle" TargetControlID="rqvtxtConfirmPassword" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:CompareValidator ID="cmptxtConfirmNewPassword" Display="None" ControlToValidate="txtConfirmPassword"
                                                    SetFocusOnError="True" ValidationGroup="validation" ControlToCompare="txtPassword"
                                                    ForeColor="Red" runat="server" ErrorMessage="Please make sure you are typing your password in both fields." meta:resourcekey="cmptxtConfirmNewPasswordResource1" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="cmveCompConfirmNewPassword" runat="server"
                                                    CssClass="customCalloutStyle" TargetControlID="cmptxtConfirmNewPassword" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </div>
                                            <div class="parsonal_textfild">
                                                <label class="commondoc">
                                                    <asp:Label ID="lblGender" runat="server" Text="Gender" meta:resourcekey="lblGenderResource1"></asp:Label>
                                                    <span class="alignright">:</span>
                                                </label>
                                                <div class="radio-selection">
                                                    <asp:RadioButton ID="rbtnMale" Text="Male" runat="server" GroupName="Gender" Checked="True" TabIndex="5" meta:resourcekey="rbtnMaleResource1" />
                                                    <asp:RadioButton ID="rbtnFemale" Text="Female" runat="server" GroupName="Gender" TabIndex="6" meta:resourcekey="rbtnFemaleResource1" />
                                                </div>
                                            </div>
                                            <div class="clear"></div>
                                            <div class="parsonal_textfild">
                                                <label class="commondoc">
                                                    <asp:Label ID="lblDateofBirth" runat="server" Text="Date Of Birth" meta:resourcekey="lblDateofBirthResource1"></asp:Label>
                                                    <span class="asteriskclass">*</span><span class="alignright">:</span>
                                                </label>
                                                <asp:TextBox ID="txtDateofBirth" runat="server" CssClass="From-Date not-edit textfild search-datepicker" TabIndex="7" Style="margin-right: 10px" meta:resourcekey="txtDateofBirthResource1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtDateofBirth" ForeColor="Red" runat="server" ControlToValidate="txtDateofBirth"
                                                    Display="None" ErrorMessage="Please select date of birth." CssClass="errormsg"
                                                    SetFocusOnError="True" ValidationGroup="validation" meta:resourcekey="rfvtxtDateofBirthResource1" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="vceDateofBirth" runat="server" CssClass="customCalloutStyle"
                                                    TargetControlID="rfvtxtDateofBirth" Enabled="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="date2">
                                        <div class="date_cont forgot_text_doctor" style="padding-right: 25px">
                                            <div class="date_cont_right marginlf">
                                                <a href="PatientLogin.aspx" id="lbtnPatientLogin" tabindex="9" title="Already Registered?"><%=this.GetLocalResourceObject("AlreadyRegistered") %></a>
                                            </div>
                                        </div>
                                        <div class="date_cont_login">
                                            <div class="date_cont_right doctorlogin-submit">
                                                <div class="supply-button3">
                                                    <asp:Button ID="btnRegister" runat="server" Text="Register" ValidationGroup="validation" TabIndex="8" OnClick="btnRegister_Click" meta:resourcekey="btnRegisterResource1" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <div class="wrapper">
        <div class="footer">
            <div class="footer_left">©<% = DateTime.Now.Year.ToString()%> 4ClearOrtho.com All Rights Reserved.</div>
            <div class="footer_right">Powered by 4edental.com</div>
        </div>
        <div class="clear"></div>
    </div>
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
    <script type="text/javascript">
        function pageLoad() {
            $('.From-Date').datepicker({
                showOn: "button",
                buttonText: "Select Date",
                buttonImage: "Content/images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                yearRange: "1950:-0",
                maxDate: new Date()
            });
            $('.not-edit').attr("readonly", "readonly");
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(loadingoverlayShow);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(loadingoverlayHide);
        }
        $(document).ready(function () {
            $('#txtEmail').focus()
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
    </script>
</body>
</html>