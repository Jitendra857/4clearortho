<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="_4eOrtho.Admin.ChangePassword" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="cf">
        <div class="page_title">
            <h2 class="padd">
                <asp:Label ID="lblHeader" runat="server" Text="Change Password" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
        </div>
        <asp:UpdatePanel ID="upChangePassword" runat="server">
            <ContentTemplate>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="widecolumn">
                    <div class="personal_box alignleft">
                        <div class="parsonal_textfild">
                            <label style="width: 185px">
                                <asp:Literal ID="ltrCurrentPassword" runat="server" Text="Current Password" meta:resourcekey="ltrCurrentPasswordResource1"></asp:Literal>
                                <span class="required">*</span><span class="alignright">:</span></label>
                            <asp:TextBox ID="txtCurrentPassword" runat="server" MaxLength="30" TextMode="Password" autocomplete="off"
                                TabIndex="1" meta:resourcekey="txtCurrentPasswordResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="ReqtxtCurrentPassword" Display="None"
                                SetFocusOnError="True" ControlToValidate="txtCurrentPassword" CssClass="error"
                                ValidationGroup="Checkvalidation"
                                ErrorMessage="Please Enter Current Password" meta:resourcekey="ReqtxtCurrentPasswordResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceReqtxtCurrentPassword" runat="server"
                                CssClass="customCalloutStyle" TargetControlID="ReqtxtCurrentPassword" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator runat="server" ID="custxtCurrentPassword" Display="None" ControlToValidate="txtCurrentPassword"
                                OnServerValidate="custxtCurrentPassword_ServerValidate" ValidationGroup="Checkvalidation"
                                CssClass="error" ErrorMessage="Entered current password is invalid"
                                SetFocusOnError="True" meta:resourcekey="custxtCurrentPasswordResource1"/>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecustxtCurrentPassword" runat="server"
                                CssClass="customCalloutStyle" TargetControlID="custxtCurrentPassword" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild">
                            <label style="width: 185px">
                                <asp:Literal ID="ltrNewPassword" runat="server" Text="New Password"  meta:resourcekey="ltrNewPasswordResource1"></asp:Literal>
                                <span class="required">*</span><span class="alignright">:</span></label>
                            <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="30" autocomplete="off" TextMode="Password"
                                TabIndex="2" meta:resourcekey="txtNewPasswordResource1"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbtxtPassword" runat="server" Enabled="True"
                                TargetControlID="txtNewPassword"
                                ValidChars="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+-=.,<>?;:'/|`~{}" />
                            <asp:RequiredFieldValidator runat="server" ID="ReqtxtNewPassword" Display="None"
                                SetFocusOnError="True" ControlToValidate="txtNewPassword" CssClass="error" ValidationGroup="Checkvalidation"
                                ErrorMessage="Please Enter New Password" ForeColor="Red" meta:resourcekey="ReqtxtNewPasswordResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceReqtxtNewPassword" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="ReqtxtNewPassword" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="rgvPasswordCheck" Display="None" runat="server"
                                ForeColor="Red" ValidationExpression="(?=(.*[0-9]){3,})(?=(.*[a-zA-Z]){4,})^[0-9a-zA-Z!@#$%^&*()_+-=.,<>?;:'/|`~{}]{7,50}$"
                                ValidationGroup="Checkvalidation" SetFocusOnError="True" CssClass="errormsg"
                                ControlToValidate="txtNewPassword"
                                ErrorMessage="New Password must be 7 characters long with at least 4 alphabets and 3 digits." meta:resourcekey="rgvPasswordCheckResource1"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rgveCalltxtNewPassword" runat="server"
                                CssClass="customCalloutStyle" TargetControlID="rgvPasswordCheck" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild">
                            <label style="width: 185px">
                                <asp:Literal ID="ltrConfirmNewPassword" runat="server"
                                    Text="Confirm New Password"  meta:resourcekey="ltrConfirmNewPasswordResource1"></asp:Literal>
                                <span class="required">*</span><span class="alignright">:</span></label>
                            <asp:TextBox ID="txtReEnterPassword" runat="server" MaxLength="30" TextMode="Password" autocomplete="off"
                                TabIndex="3" meta:resourcekey="txtReEnterPasswordResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="ReqtxtReEnterPassword" Display="None"
                                SetFocusOnError="True" ControlToValidate="txtReEnterPassword" CssClass="error"
                                ValidationGroup="Checkvalidation"
                                ErrorMessage="Please Enter Confirm New Password" meta:resourcekey="ReqtxtReEnterPasswordResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtReEnterPassword" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="ReqtxtReEnterPassword" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="CompPassword" runat="server" ControlToCompare="txtNewPassword"
                                ValidationGroup="Checkvalidation" ControlToValidate="txtReEnterPassword" Display="None"
                                ErrorMessage="Confirm New Password is not match with New Password" CssClass="error"
                                SetFocusOnError="True" meta:resourcekey="CompPasswordResource1"></asp:CompareValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceCompPassword" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="CompPassword" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div class="alignright">
                    <span class="blue_btn">
                        <asp:Button ID="btnAdd" runat="server" Text="Save" ValidationGroup="Checkvalidation"
                            OnClick="btnAdd_Click" TabIndex="4" meta:resourcekey="btnAddResource1" />
                    </span><span class="dark_btn">
                        <input type="reset" tabindex="4" title='<%= this.GetLocalResourceObject("Reset") %>' value='<%= this.GetLocalResourceObject("Reset") %>' />
                        
                    </span>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('<%=txtCurrentPassword.ClientID%>').focus();
        };
    </script>
</asp:Content>
