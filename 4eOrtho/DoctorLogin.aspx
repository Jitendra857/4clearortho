<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoctorLogin.aspx.cs" Inherits="_4eOrtho.DoctorLogin" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>4ClearOrtho - Doctor Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!--styles -->
    <script type="text/javascript" src="Scripts/jquery-1.7.1.min.js"></script>
    <link href="Styles/Ajaxtoolkit.css" rel="stylesheet" />
    <link runat="server" id="lnk_style" href="Styles/style.css" rel="stylesheet" />
    <link href="Styles/flexslider.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="Scripts/loadingoverlay.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtEmail').focus();
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
                        <li><a href="PatientLogin.aspx"><%= this.GetLocalResourceObject("Patient") %>  </a></li>
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
        <asp:UpdatePanel ID="upDoctorLogin" runat="server">
            <ContentTemplate>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="midportion minhgt" style="min-height: 380px;">
                    <div class="wrapper">
                        <div class="bothsection">
                            <div class="patientlogin_main">
                                <div class="middlesection">
                                    <div class="doctorlogin">
                                        <h2><%= this.GetLocalResourceObject("DoctorLogin") %>
                                        </h2>
                                    </div>
                                    <div class="email_textfild">
                                        <img class="log-img" src="Content/images/email_icon.png" alt="dfa" />
                                        <asp:TextBox ID="txtEmailId" runat="server" meta:resourcekey="txtEmailIdResource1"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbUserName" runat="server" Enabled="True"
                                            TargetControlID="txtEmailID" ValidChars="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@#$%&_-." />
                                        <asp:RequiredFieldValidator ID="reqemail" runat="server" ValidationGroup="validation" ControlToValidate="txtEmailId" ErrorMessage="Please enter Email" Display="None" meta:resourcekey="reqemailResource1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regexpremail" runat="server" ValidationGroup="validation" ControlToValidate="txtEmailId" ErrorMessage="Please enter valid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None" meta:resourcekey="regexpremailResource1"></asp:RegularExpressionValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="reqvemail" runat="server" CssClass="customCalloutStyle"
                                            TargetControlID="reqemail" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="regvexpremail" runat="server" CssClass="customCalloutStyle"
                                            TargetControlID="regexpremail" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </div>
                                    <div class="email_textfild ptop">
                                        <img class="log-img" src="Content/images/password_icon.png" alt="dfa" />
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbtxtPassword" runat="server" Enabled="True"
                                            TargetControlID="txtPassword" ValidChars="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+-=.,<>?;:'/|`~{}" />
                                        <asp:RequiredFieldValidator ID="reqPassword" ValidationGroup="validation" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please enter Password" Display="None" meta:resourcekey="reqPasswordResource1"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="reqvPassword" runat="server" TargetControlID="reqPassword"
                                            CssClass="customCalloutStyle" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </div>
                                    <div class="date2">
                                        <div class="date_cont_login">
                                            <div class="date_cont_right doctorlogin-submit">
                                                <div class="supply-button3">
                                                    <asp:Button ID="btnLogin" runat="server" Text="Login" ValidationGroup="validation" OnClick="btnLogin_Click" meta:resourcekey="btnLoginResource1" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="date2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="forgot_text_doctor" style="padding-right: 25px">
                                                        <div class="date_cont_right marginlf">
                                                            <a href="ForgotPassword.aspx?doctor=d" id="lbtnForgotPassword" title='<%= this.GetLocalResourceObject("ForgotPassword") %>'><%= this.GetLocalResourceObject("ForgotPassword") %></a>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="forgot_text_doctor">
                                                        <div class="date_cont_right marginlf">
                                                            <a href="DoctorRegistration.aspx" id="aDoctorRegistration" title="Register Doctor"><%=this.GetLocalResourceObject("DoctorRegistration") %></a>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
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
            <div class="footer_left">©<% = DateTime.Now.Year.ToString()%> 4ClearOrtho.com <%= this.GetLocalResourceObject("Rights") %>.</div>
            <div class="footer_right"><%= this.GetLocalResourceObject("PoweredBy") %> 4edental.com</div>
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
