<%@ Page Title="Admin - Add User" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="_4eOrtho.Admin.AddUser" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('<%=txtFirstName.ClientID%>').focus();
        };
    </script>
    <asp:Panel ID="pnlAddUser" runat="server" DefaultButton="btnAdd"
        meta:resourcekey="pnlAddUserResource1">
        <div id="container" class="cf">
            <div class="page_title">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%;">
                            <h2 class="padd">
                                <asp:Label ID="lblHeader" runat="server" Text="Add User"
                                    meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
                        </td>
                        <td style="width: 50%;">
                            <span class="dark_btn_small">
                                <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/UserList.aspx"
                                    TabIndex="8" CausesValidation="False"
                                    meta:resourcekey="btnBackResource1"></asp:Button>
                            </span>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="upUser" runat="server">
                <ContentTemplate>
                    <div id="divMsg" runat="server">
                        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                    </div>
                    <div class="widecolumn">
                        <div class="personal_box alignleft" style="width: 97.5%;">
                            <div class="parsonal_textfild">
                                <label class="lbluser">
                                    <asp:Literal ID="ltrFirstName" runat="server" Text="First Name"
                                        meta:resourcekey="ltrFirstNameResource1"></asp:Literal>
                                    <span class="required">*</span><span class="alignright">:</span></label>
                                <asp:TextBox ID="txtFirstName" runat="server" MaxLength="30" TabIndex="1"
                                    meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="ReqFirstName" Display="None" ControlToValidate="txtFirstName"
                                    SetFocusOnError="True" CssClass="error" ValidationGroup="Checkvalidation"
                                    ErrorMessage="Please Enter First Name" meta:resourcekey="ReqFirstNameResource1"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vceReqFirstName" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="ReqFirstName" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </div>
                            <div class="parsonal_textfild">
                                <label class="lbluserWidth">
                                    <asp:Literal ID="ltrLastName" runat="server" Text="Last Name"
                                        meta:resourcekey="ltrLastNameResource1"></asp:Literal>
                                    <span class="required">*</span><span class="alignright">:</span></label>
                                <asp:TextBox ID="txtLastName" runat="server" MaxLength="30" TabIndex="2"
                                    meta:resourcekey="txtLastNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="ReqLastName" Display="None" ControlToValidate="txtLastName"
                                    SetFocusOnError="True" CssClass="error" ValidationGroup="Checkvalidation"
                                    ErrorMessage="Please Enter Last Name" meta:resourcekey="ReqLastNameResource1"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vceReqLastName" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="ReqLastName" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </div>
                            <%--<div class="parsonal_textfild">
                                <label class="lbluserWidth">
                                    <asp:Literal ID="ltrUserName" runat="server" Text="User Name"
                                        meta:resourcekey="ltrUserNameResource1"></asp:Literal>
                                    <span class="required">*</span><span class="alignright">:</span></label>
                                <asp:TextBox ID="txtUserName" runat="server" MaxLength="30" TabIndex="3"
                                    meta:resourcekey="txtUserNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="ReqUserName" ControlToValidate="txtUserName"
                                    Display="None" CssClass="error" ValidationGroup="Checkvalidation" SetFocusOnError="True"
                                    ErrorMessage="Please Enter User Name"
                                    meta:resourcekey="ReqUserNameResource1"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vceReqUserName" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="ReqUserName" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:RegularExpressionValidator ID="rgvUsernameLength" Display="None" runat="server"
                                    ValidationExpression="^.{6,30}$" ValidationGroup="Checkvalidation" SetFocusOnError="True"
                                    CssClass="error" ControlToValidate="txtUserName"
                                    ErrorMessage="Please Enter Minimum 6 character"
                                    meta:resourcekey="rgvUsernameLengthResource1"></asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vceUsernameLength" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rgvUsernameLength" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:CustomValidator runat="server" ID="custxtUserName" ControlToValidate="txtUserName"
                                    SetFocusOnError="True" OnServerValidate="cusCustom_ServerUsernameValidate" Display="None"
                                    ValidationGroup="Checkvalidation" CssClass="error"
                                    ErrorMessage="This User Name is already exist, please try another one"
                                    meta:resourcekey="custxtUserNameResource1" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcecustxtUserName" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="custxtUserName" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fteUserName" runat="server" Enabled="True"
                                    TargetControlID="txtUserName" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                    ValidChars=".-_" />
                            </div>--%>
                            <div class="parsonal_textfild">
                                <label class="lbluserWidth">
                                    <asp:Literal ID="ltrEmailAddress" runat="server" Text="Email Address"
                                        meta:resourcekey="ltrEmailAddressResource1"></asp:Literal>
                                    <span class="required">*</span><span class="alignright">:</span></label>
                                <asp:TextBox ID="txtEmailAddress" runat="server" MaxLength="100" TabIndex="4"
                                    meta:resourcekey="txtEmailAddressResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="ReqEmailAddress" ControlToValidate="txtEmailAddress"
                                    Display="None" CssClass="error" ValidationGroup="Checkvalidation" SetFocusOnError="True"
                                    ErrorMessage="Please Enter Email Address"
                                    meta:resourcekey="ReqEmailAddressResource1"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vceReqEmailAddress" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="ReqEmailAddress" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:RegularExpressionValidator ID="regEmailAddress" Display="None" runat="server"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Checkvalidation"
                                    SetFocusOnError="True" CssClass="error"
                                    ControlToValidate="txtEmailAddress"
                                    ErrorMessage="Your Email Address must be in valid format "
                                    meta:resourcekey="regEmailAddressResource1"></asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vceregEmailAddress" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="regEmailAddress" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:CustomValidator runat="server" ID="cstmtxtEmailAddress" ControlToValidate="txtEmailAddress"
                                    OnServerValidate="cusCustom_ServerEmailValidate" ValidationGroup="Checkvalidation"
                                    SetFocusOnError="True" Display="None" CssClass="error"
                                    ErrorMessage="Your Email Address has already been taken, please try another one"
                                    meta:resourcekey="cstmtxtEmailAddressResource1" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcecstmtxtEmailAddress" runat="server"
                                    CssClass="customCalloutStyle" TargetControlID="cstmtxtEmailAddress" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </div>
                            <div class="parsonal_textfild">
                                <label class="lbluserWidth">
                                    <asp:Literal ID="ltrIsActive" runat="server" Text="Is Active?"
                                        meta:resourcekey="ltrIsActiveResource1"></asp:Literal><span class="alignright">:</span></label>
                                <asp:CheckBox ID="chkStatus" runat="server" TabIndex="5"
                                    meta:resourcekey="chkStatusResource1" />
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="alignright">
                        <span class="blue_btn">
                            <asp:Button ID="btnAdd" runat="server" Text="Save" ValidationGroup="Checkvalidation"
                                OnClick="btnSave_Click" TabIndex="6"
                                meta:resourcekey="btnAddResource1" />
                        </span><span class="dark_btn">
                            <input type="reset" tabindex="7" value='<%= this.GetLocalResourceObject("Reset") %>' title='<%= this.GetLocalResourceObject("Reset") %>' />
                        </span>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%--<asp:UpdateProgress ID="processUser" runat="server" AssociatedUpdatePanelID="upUser"
            DisplayAfter="10">
            <ProgressTemplate>
                <div class="processbar1">
                    <img src="../Content/images/loading.gif" alt="loading" width="150" height="150" style="top: 30%; left: 45%; position: absolute;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
    </asp:Panel>
</asp:Content>
