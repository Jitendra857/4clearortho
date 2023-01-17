<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="_4eOrtho.Admin.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Forgot Password</title>
    <link href="Styles/common.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminStyle.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Ajaxtoolkit.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ClearText() {
            //$find('BehaveHideShow').hide();
            document.getElementById('txtEmail').value = "";
            return false;
        }
    </script>
</head>
<body class="body_top">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="forgotPasswordScriptManager" runat="server">
        </asp:ScriptManager>
        <div>
            <h1 class="login_logo">
                <a href="#" title="4eDental">4eDental</a>
            </h1>
            <div class="login">
                <div class="login_main">
                    <div class="login_box alignleft">
                        <h2>
                            <asp:Label ID="lblForgotPassword" runat="server" Text="Forgot Password?"></asp:Label>
                        </h2>
                        <p>
                            <asp:Label ID="ltrGetYour" runat="server"
                                Text="Get your password using your registered Email-Id."></asp:Label>
                        </p>
                        <div id="divMsg" runat="server">
                            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                        </div>
                        <div class="email_textfild madd">
                            <img class="log-img" src="Images/email_icon.png" alt="Email Address" />
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvtxtEmail" ForeColor="Red" runat="server" SetFocusOnError="True"
                                ControlToValidate="txtEmail" Display="None" ErrorMessage="Please Enter Email-Id"
                                CssClass="errormsg" ValidationGroup="validation" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="valCalltxtEmailAddress" runat="server"
                                CssClass="customCalloutStyle" TargetControlID="rqvtxtEmail"
                                Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="regEmailAddress" Display="None" runat="server"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="validation"
                                SetFocusOnError="true" CssClass="error" ControlToValidate="txtEmail" ErrorMessage="Your Email-Id must be in valid format">
                            </asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceregEmailAddress" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="regEmailAddress" Enabled="True" BehaviorID="BehaveHideShow">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="bottom_btn alignleft">
                            <span class="blue_btn forgot_password_button">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                    ValidationGroup="validation" TabIndex="2" />
                            </span><span class="dark_btn">
                                <%-- <input type="reset" tabindex="3" value='<%= this.GetLocalResourceObject("Reset") %>' onclick="return ClearText();" />--%>
                            </span>
                        </div>
                    </div>
                    <div class="loginbox_bottom">
                        <img src="Images/bgi/loginbox_bottom.png" alt="bottom img" />
                    </div>
                </div>
                <div class="back_link">
                    <asp:LinkButton ID="lbtnBacktoLogin" runat="server" Text="Back to Login Page"
                        PostBackUrl="~/Admin/Login.aspx"></asp:LinkButton>
                </div>
            </div>
        </div>
    </form>
</body>
<script type="text/javascript">
    window.onload = function () {
        document.getElementById('<%=txtEmail.ClientID%>').focus();
    };
</script>
</html>
