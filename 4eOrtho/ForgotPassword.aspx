<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="_4eOrtho.ForgotPassword" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>4ClearOrtho - Forgot Password</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!--styles -->
    <script type="text/javascript" src="Scripts/jquery-1.7.1.min.js"></script>
    <link href="Styles/Ajaxtoolkit.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
    <link href="Styles/flexslider.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtEmailId').focus();
        });
    </script>
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
                        <li><a href="Home.aspx"><%= this.GetLocalResourceObject("Home") %></a></li>
                        <li><a href="PatientLogin.aspx"><%= this.GetLocalResourceObject("Patient") %></a></li>
                        <li><a href="DoctorLogin.aspx"><%= this.GetLocalResourceObject("Doctor") %></a></li>
                    </ul>
                </div>

                <div class="clear"></div>
            </div>
        </div>

    </div>
    <!-- Header -->

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div class="midportion minhgt" style="min-height: 380px;">
                    <div class="wrapper">
                        <div class="bothsection">
                            <div class="patientlogin_main">
                                <div id="divMsg" runat="server" style="margin-bottom: 10px; width: 87%;">
                                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                                </div>
                                <div class="middlesection">
                                    <div class="doctorlogin">
                                        <h2><%= this.GetLocalResourceObject("ForgotPassword")%>
                                        </h2>
                                    </div>
                                    <div class="email_textfild">
                                        <img class="log-img" src="Content/images/email_icon.png" alt="dfa" />
                                        <asp:TextBox ID="txtEmailId" runat="server" meta:resourcekey="txtEmailIdResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqemail" runat="server" ValidationGroup="validation" ControlToValidate="txtEmailId" ErrorMessage="Please enter Email" Display="None" meta:resourcekey="reqemailResource1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regexpremail" runat="server" ControlToValidate="txtEmailId" ErrorMessage="Please enter valid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None" meta:resourcekey="regexpremailResource1"></asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="reqvmail" runat="server" CssClass="customCalloutStyle"
                                            TargetControlID="reqemail" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="regvexpremail" runat="server" CssClass="customCalloutStyle"
                                            TargetControlID="regexpremail" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </div>
                                    <div class="date2">
                                        <div class="">
                                            <div class="date_cont_right forgotpassword-submit">
                                                <div class="supply-button3">
                                                    <asp:Button ID="btnSendPassword" runat="server" ValidationGroup="validation" Text="Submit" OnClick="btnSendPassWord_Click" meta:resourcekey="btnSendPasswordResource1" />
                                                </div>
                                                <div class="supply-button3">
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" meta:resourcekey="btnCancelResource1" OnClientClick="window.open(window.location.href,'_self');return false;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="date2">
                                        <div class="forgot_text_doctor" style="padding-right: 25px">
                                            <div class="date_cont_right marginlf">
                                                <a onclick="window.history.back()" style="cursor: pointer;">Back to Login Page</a>
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
            <div class="footer_left">©2014 4ClearOrtho.com All Rights Reserved.</div>
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
</body>
</html>
