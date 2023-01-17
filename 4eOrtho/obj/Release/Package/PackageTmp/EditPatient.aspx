<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="EditPatient.aspx.cs" Inherits="_4eOrtho.EditPatient" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .floatright {
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main_right_cont minheigh">
        <div class="title">
            <h2>
                <asp:Label ID="lblHeader" runat="server" Text="Edit Patient" meta:resourcekey="lblHeaderResource1" ></asp:Label>
                <div class="supply-button3 floatright">
                    <asp:Button runat="server" Text="Back" ID="btn_back" Visible="False" OnClick="btn_back_Click" meta:resourcekey="btn_backResource1" />
                </div>
            </h2>
        </div>
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1" ></asp:Label>
        </div>
        <div class="personal_box alignleft">

            <%-- <div class="parsonal_textfild alignleft" id="dvType" runat="server">
                <label>
                    <asp:Label ID="lblExisting" runat="server" Text="Patient Type" meta:resourcekey="lblExistingResource1"></asp:Label>
                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                </label>
                <div class="radio-selection">
                    <asp:RadioButton ID="rbtnNew" Text="New" runat="server" GroupName="Patient" Checked="True" TabIndex="0" meta:resourcekey="rbtnNewResource1" />
                    <asp:RadioButton ID="rbtnExisting" Text="Existing" runat="server" GroupName="Patient" TabIndex="1" meta:resourcekey="rbtnExistingResource1" />
                </div>
            </div>--%>
            <%-- <div class="parsonal_textfild alignleft" id="dvExistingPatientName" runat="server" style="display: none">
                <label>
                    <asp:Label ID="lblSelectPatient" runat="server" Text="Select Patient" meta:resourcekey="lblSelectPatientResource1"></asp:Label>
                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                </label>
                <div class="parsonal_select">
                    <asp:DropDownList ID="ddlPatient" runat="server" onchange="txtEmailChange(this)" TabIndex="2" meta:resourcekey="ddlPatientResource1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvPatient" ForeColor="Red" runat="server" ControlToValidate="ddlPatient"
                        Display="None" ErrorMessage="Please select patient" CssClass="errormsg"
                        SetFocusOnError="True" ValidationGroup="validation1" InitialValue="0" Enabled="False" meta:resourcekey="rfvPatientResource1" />
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcePatient" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rfvPatient" Enabled="True" />
                </div>
            </div>--%>
            <div class="parsonal_textfild alignleft" style="display: none;">
                <label>
                    <asp:Label ID="lblEmail" runat="server" Text="Email Id" meta:resourcekey="lblEmailResource1" ></asp:Label>
                    <%--<span class="alignright">:<span class="asteriskclass">*</span></span>--%>
                </label>
                <asp:TextBox ID="txtEmail" runat="server" Enabled="False" meta:resourcekey="txtEmailResource1"></asp:TextBox>
            </div>
            <div class="parsonal_textfild alignleft">
                <label>
                    <asp:Label ID="lblFirstName" runat="server" Text="First Name" meta:resourcekey="lblFirstNameResource1" ></asp:Label>
                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                </label>
                <asp:TextBox ID="txtFirstName" runat="server" TabIndex="1" meta:resourcekey="txtFirstNameResource1" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" ForeColor="Red" runat="server" ControlToValidate="txtFirstName"
                    Display="None" ErrorMessage="Please enter first name" CssClass="errormsg"
                    SetFocusOnError="True" ValidationGroup="validation1" meta:resourcekey="rfvFirstNameResource1" />
                <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValrfvFirstName" TargetControlID="rfvFirstName" CssClass="customCalloutStyle" Enabled="True"></ajaxToolkit:ValidatorCalloutExtender>
            </div>
            <div class="parsonal_textfild alignleft">
                <label>
                    <asp:Label ID="lblLastName" runat="server" Text="Last Name" meta:resourcekey="lblLastNameResource1" ></asp:Label>
                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                </label>
                <asp:TextBox ID="txtLastName" runat="server" TabIndex="2" meta:resourcekey="txtLastNameResource1" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtLastName" ForeColor="Red" runat="server" ControlToValidate="txtLastName"
                    Display="None" ErrorMessage="Please enter last name" CssClass="errormsg"
                    SetFocusOnError="True" ValidationGroup="validation1" meta:resourcekey="rfvtxtLastNameResource1" />
                <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValrfvtxtLastName" TargetControlID="rfvtxtLastName" CssClass="customCalloutStyle" Enabled="True"></ajaxToolkit:ValidatorCalloutExtender>
            </div>
            <div class="parsonal_textfild alignleft">
                <label>
                    <asp:Label ID="lblNewPassword" runat="server" Text="New Password" meta:resourcekey="lblNewPasswordResource1" ></asp:Label>
                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                </label>
                <asp:TextBox ID="txtnewpassword" runat="server" TextMode="Password" TabIndex="3" meta:resourcekey="txtnewpasswordResource1" ></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                    TargetControlID="txtnewpassword" ValidChars="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+-=.,<>?;:'/|`~{}" />
                <asp:RequiredFieldValidator ID="ReqNewPassword" ForeColor="Red" runat="server" ControlToValidate="txtnewpassword"
                    Display="None" ErrorMessage="Please enter new password" CssClass="errormsg"
                    SetFocusOnError="True" ValidationGroup="validation1" meta:resourcekey="ReqNewPasswordResource1" />
                <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="Valtxtnewpassword" TargetControlID="ReqNewPassword" CssClass="customCalloutStyle" Enabled="True"></ajaxToolkit:ValidatorCalloutExtender>
                <asp:RegularExpressionValidator ID="rqvPassword" runat="server" ControlToValidate="txtnewpassword"
                    SetFocusOnError="True" Display="None" ErrorMessage="Password must be 7 characters long with at least 4 alphabets and 3 digits."
                    ValidationExpression="(?=(.*[0-9]){3,})(?=(.*[a-zA-Z]){4,})^[0-9a-zA-Z!@#$%^&*()_+-=.,<>?;:'/|`~{}]{7,50}$"
                    ValidationGroup="validation1" meta:resourcekey="rqvPasswordResource1" />
                <ajaxToolkit:ValidatorCalloutExtender ID="rqvePassword" runat="server" CssClass="customCalloutStyle"
                    TargetControlID="rqvPassword" Enabled="True">
                </ajaxToolkit:ValidatorCalloutExtender>
            </div>
            <div class="parsonal_textfild alignleft">
                <label>
                    <asp:Label ID="lblconfirmpassword" runat="server" Text="Confirm Password" meta:resourcekey="lblconfirmpasswordResource1"></asp:Label>
                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                </label>
                <asp:TextBox ID="txtconfirmpassword" TextMode="Password" runat="server" TabIndex="4" meta:resourcekey="txtconfirmpasswordResource1" ></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="ftePassword" runat="server" Enabled="True"
                    TargetControlID="txtconfirmpassword" ValidChars="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+-=.,<>?;:'/|`~{}" />
                <asp:RequiredFieldValidator ID="Reqtxtconfirmpassword" ForeColor="Red" runat="server" ControlToValidate="txtconfirmpassword"
                    Display="None" ErrorMessage="Please enter confirm password" CssClass="errormsg"
                    SetFocusOnError="True" ValidationGroup="validation1" meta:resourcekey="ReqtxtconfirmpasswordResource1" />
                <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValReqtxtconfirmpassword" TargetControlID="Reqtxtconfirmpassword" CssClass="customCalloutStyle" Enabled="True"></ajaxToolkit:ValidatorCalloutExtender>
                <asp:CompareValidator ID="ComPasswords" runat="server" ControlToCompare="txtnewpassword" ControlToValidate="txtconfirmpassword" ForeColor="Red"
                    Display="None" ErrorMessage="Password does not match." CssClass="errormsg"
                    SetFocusOnError="True" ValidationGroup="validation1" meta:resourcekey="ComPasswordsResource1" ></asp:CompareValidator>
                <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValComPasswords" TargetControlID="ComPasswords" CssClass="customCalloutStyle" Enabled="True"></ajaxToolkit:ValidatorCalloutExtender>
            </div>
            <div class="date2">
                <div class="date_cont">
                    <div class="date_cont_right">
                        <div class="supply-button3">
                            <asp:Button ID="btnSubmit" runat="server" Text="Save" ValidationGroup="validation1" OnClick="btnSubmit_Click" meta:resourcekey="btnSubmitResource1" />
                        </div>
                        <div class="supply-button3">
                            <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="5" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
