<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="_4eOrtho.ChangePassword" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('<%=txtOldPassword.ClientID%>').focus();
        };
    </script>
    <asp:UpdatePanel ID="upRegister" runat="server">
        <ContentTemplate>
            <div class="main_right_cont minheigh">
                <div class="title">
                    <h2>
                        <asp:Label ID="lblHeader" runat="server" Text="Change Password" meta:resourcekey="lblHeaderResource1"></asp:Label>
                    </h2>
                </div>
                <div id="divMsg" runat="server" style="width:563px;">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="personal_box alignleft" style="width: 100%">
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblOldPassword" runat="server" Text="Old Password" meta:resourcekey="lblOldPasswordResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtOldPassword" runat="server" TabIndex="2" TextMode="Password" MaxLength="50"
                            autocomplete="off" meta:resourcekey="txtOldPasswordResource1"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtOldPassword" runat="server" Enabled="True"
                            TargetControlID="txtOldPassword" ValidChars="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+-=.,<>?;:'/|`~{}" />
                        <asp:RequiredFieldValidator ID="rfvtxtOldPassword" ForeColor="Red" runat="server" ControlToValidate="txtOldPassword"
                            Display="None" ErrorMessage="Please enter last name" CssClass="errormsg"
                            SetFocusOnError="True" ValidationGroup="validation" meta:resourcekey="rfvtxtOldPasswordResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtOldPassword" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rfvtxtOldPassword" Enabled="True" />
                        <asp:CustomValidator ID="customtxtOldPassword" runat="server" ControlToValidate="txtOldPassword"
                            SetFocusOnError="True" Display="None" ErrorMessage="Your old password did not matched." OnServerValidate="customtxtOldPassword_ServerValidate"
                            ValidationGroup="validation" meta:resourcekey="customtxtOldPasswordResource1"></asp:CustomValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vcecustomtxtOldPassword" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="customtxtOldPassword" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblPassword" runat="server" Text="Password" meta:resourcekey="lblPasswordResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="50" TabIndex="8"
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
                        <asp:CustomValidator ID="customtxtPassword" runat="server" ControlToValidate="txtPassword"
                            SetFocusOnError="True" Display="None" ErrorMessage="Old password and new password must be different." OnServerValidate="customtxtPassword_ServerValidate"
                            ValidationGroup="validation" meta:resourcekey="customtxtPasswordResource1"></asp:CustomValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vcecustomtxtPassword" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="customtxtPassword" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" meta:resourcekey="lblConfirmPasswordResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="50"
                            TabIndex="9" autocomplete="off" meta:resourcekey="txtConfirmPasswordResource1"></asp:TextBox>
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
                    <div class="date2">
                        <div class="date_cont">
                            <div class="date_cont_right">
                                <div class="supply-button3">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="validation"
                                        OnClick="btnSubmit_Click" TabIndex="14" meta:resourcekey="btnSubmitResource1" />
                                </div>
                                <div class="supply-button3">
                                    <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="15" ToolTip="Cancel" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnPassword" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="uprogRegister" runat="server" AssociatedUpdatePanelID="upRegister"
        DisplayAfter="10">
        <ProgressTemplate>
            <div class="processbar1">
                <img src="Content/images/ajax-loading.gif" alt="loading" style="top: 50%; left: 50%; position: absolute;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
