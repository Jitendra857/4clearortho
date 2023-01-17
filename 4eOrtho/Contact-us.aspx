<%@ Page Title="4ClearOrtho - Contact Us" Language="C#" MasterPageFile="~/Ortho.Master" AutoEventWireup="true" CodeBehind="Contact-Us.aspx.cs" Inherits="_4eOrtho.Contact_Us" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#' + '<%= txtName.ClientID %>').focus();
        });
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="main_right_cont">
                <div class="left_title">
                    <h2>
                        <asp:Label ID="lblheader" runat="server" Text="Contact Us" meta:resourcekey="lblheaderResource1"></asp:Label>
                    </h2>
                </div>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="personal_box alignleft">
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblName" runat="server" Text="Full Name" meta:resourcekey="lblNameResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtName" runat="server" MaxLength="50" TabIndex="0" meta:resourcekey="txtNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvName" runat="server" TabIndex="0" ValidationGroup="validation" ControlToValidate="txtName" SetFocusOnError="true"
                            ForeColor="Red" Display="None" ErrorMessage="Please enter name" meta:resourcekey="rqvNameResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvName" Enabled="True" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="flttxtname" runat="server" Enabled="True" TargetControlID="txtName" ValidChars=".abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtEmail" runat="server" TabIndex="1" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvEmail" runat="server" TabIndex="1" ValidationGroup="validation" SetFocusOnError="true" ControlToValidate="txtEmail" Display="None" ForeColor="Red" ErrorMessage="Please enter email" meta:resourcekey="rqvEmailResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rgevpremail" runat="server" ValidationGroup="validation" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None" meta:resourcekey="rgevpremailResource1"></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveEmail" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvEmail" Enabled="True" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rgevepremail" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rgevpremail" Enabled="True" />
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblCountry" runat="server" Text="Country" meta:resourcekey="lblCountryResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <div class="parsonal_select">
                            <asp:DropDownList ID="ddlCountry" runat="server" TabIndex="2" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddlCountryResource1"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rqvcountry" InitialValue="0" runat="server" TabIndex="2" SetFocusOnError="true" ValidationGroup="validation" ErrorMessage="Please select country" Display="None" ForeColor="Red" ControlToValidate="ddlCountry" meta:resourcekey="rqvcountryResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqvecountry" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvcountry" Enabled="True" />
                        </div>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblState" runat="server" Text="State" meta:resourcekey="lblStateResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <div class="parsonal_select">
                            <asp:DropDownList ID="ddlState" runat="server" TabIndex="3" meta:resourcekey="ddlStateResource1"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rqvstate" InitialValue="0" runat="server" TabIndex="3" SetFocusOnError="true" ValidationGroup="validation" ErrorMessage="Please select state" Display="None" ForeColor="Red" ControlToValidate="ddlState" meta:resourcekey="rqvstateResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqvestate" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvstate" Enabled="True" />
                        </div>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblCity" runat="server" Text="City" meta:resourcekey="lblCityResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                        </label>
                        <asp:TextBox ID="txtCity" runat="server" TabIndex="4" meta:resourcekey="txtCityResource1"></asp:TextBox>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblMobile" runat="server" Text="Mobile" meta:resourcekey="lblMobileResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtMobile" runat="server" TabIndex="5" meta:resourcekey="txtmobileResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvMobile" runat="server" TabIndex="5" SetFocusOnError="true" ValidationGroup="validation" ErrorMessage="Please enter mobile number."
                            ControlToValidate="txtMobile" Display="None" ForeColor="Red" meta:resourcekey="rqvMobileResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvMobile" Enabled="True" />
                        <asp:RegularExpressionValidator ID="rgvMobile" Display="None" runat="server" ControlToValidate="txtMobile"
                            SetFocusOnError="True" ValidationExpression="[0-9-+()]{1,20}$" ValidationGroup="validation"
                            CssClass="errormsg" ErrorMessage="Only Numeric Values,-, +, () are allowed" meta:resourcekey="rgvMobileResource1"></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="valMobile" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rgvMobile" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblSubject" runat="server" Text="Subject" meta:resourcekey="lblSubjectResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtSubject" runat="server" TabIndex="6" meta:resourcekey="txtSubjectResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvSubject" TabIndex="6" runat="server" SetFocusOnError="true" ValidationGroup="validation" ErrorMessage="please enter subject" ControlToValidate="txtSubject" Display="None" ForeColor="Red" meta:resourcekey="rqvSubjectResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveSubject" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvSubject" Enabled="True" />
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblComment" runat="server" Text="Comment/Query" meta:resourcekey="lblCommentResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtComment" runat="server" TabIndex="7" TextMode="MultiLine" Height="100" meta:resourcekey="txtCommentResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvComment" runat="server" TabIndex="7" SetFocusOnError="true" ValidationGroup="validation" ErrorMessage="Please enter comment/query" ControlToValidate="txtComment" Display="None" ForeColor="Red" meta:resourcekey="rqvCommentResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveComment" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvComment" Enabled="True" />
                    </div>
                    <div class="date2">
                        <div class="date_cont">
                            <div class="date_cont_right">
                                <div class="supply-button3">
                                    <asp:Button ID="btnSend" runat="server" Text="Save" TabIndex="8" ToolTip="Save" ValidationGroup="validation" OnClick="btnSend_Click" meta:resourcekey="btnsendResource1" />
                                </div>
                                <div class="supply-button3">
                                    <asp:Button runat="server" ID="btnReset" Text="Cancel" ToolTip="Cancel" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
