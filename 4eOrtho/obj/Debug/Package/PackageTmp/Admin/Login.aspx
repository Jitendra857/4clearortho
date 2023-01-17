<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_4eOrtho.Admin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login Page</title>
    <link href="Styles/common.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Ajaxtoolkit.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" type="image/x-icon" href="Images/logo.ico" />
</head>
<body class="body_top">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scrMng" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upLogin" runat="server">
            <ContentTemplate>

                 <h1 class="login_logo">
                        <a href="Login.aspx" title="4eDental">4ClearOrtho</a>
                    </h1>
                <div class="login">
                  
                    

                    <div class="login_main">
                        <div class="login_box alignleft">
                            <h2>
                                <asp:Literal ID="ltrSignIn" runat="server" Text="Sign-In"></asp:Literal>
                            </h2>
                            <p>
                                <asp:Literal ID="ltrSignInUsing" runat="server"
                                    Text="Sign in using your registered account:"></asp:Literal>
                            </p>
                            <div id="divMsg" runat="server">
                                <asp:Label ID="lblMsg" runat="server" Visible="False"><span class="required">*</span></asp:Label>
                            </div>
                            <div class="email_textfild">
                                <img class="log-img" src="Images/email_icon.png" alt="UserName" />
                                <asp:TextBox ID="txtEmailAddress" runat="server" MaxLength="100" TabIndex="1"
                                    meta:resourcekey="txtEmailAddressResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="ReqEmailAddress" ControlToValidate="txtEmailAddress"
                                    Display="None" CssClass="error" ValidationGroup="validation" SetFocusOnError="True"
                                    ErrorMessage="Please Enter Email Address"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vceReqEmailAddress" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="ReqEmailAddress" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:RegularExpressionValidator ID="regEmailAddress" Display="None" runat="server"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="validation"
                                    SetFocusOnError="True" CssClass="error"
                                    ControlToValidate="txtEmailAddress"
                                    ErrorMessage="Your Email Address must be in valid format "></asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vceregEmailAddress" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="regEmailAddress" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </div>
                            <div class="email_textfild ptop">
                                <img class="log-img" src="Images/password_icon.png" alt="dfa" />
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="30"
                                    TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqvPassword" runat="server" ControlToValidate="txtPassword"
                                    SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Password" ForeColor="Red"
                                    CssClass="errormsg" ValidationGroup="validation" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="valCalltxtPassword" runat="server" TargetControlID="rqvPassword"
                                    CssClass="customCalloutStyle" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </div>

                            <br />
                            <div class="bottom_btn padd alignleft">
                                <span class="blue_btn mrgn">
                                    <asp:Button ID="btnLogin" runat="server" ValidationGroup="validation" Text="Login"
                                        OnClick="btnLogin_Click" TabIndex="3" />
                                </span>
                            </div>
                            <div class="forgot_text">
                                <asp:LinkButton ID="lbtnForgotPassword" runat="server" Text="Forgot Password?"
                                    PostBackUrl="~/Admin/ForgotPassword.aspx"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="loginbox_bottom">
                            <img src="Images/bgi/loginbox_bottom.png" alt="bottom img" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
<script type="text/javascript">
    window.onload = function () {
        document.getElementById('<%=txtEmailAddress.ClientID%>').focus();
    };
</script>
</html>

