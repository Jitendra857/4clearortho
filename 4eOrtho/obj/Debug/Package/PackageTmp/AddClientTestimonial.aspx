<%@ Page Title="4ClearOrtho - Add Testimonial" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" UICulture="auto" CodeBehind="AddClientTestimonial.aspx.cs" ValidateRequest="false" Inherits="_4eOrtho.AddClientTestimonial" Culture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Src="~/UserControls/HTMLEditor.ascx" TagName="UCHTMLEditor" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upAddClienttestimonial" runat="server">
        <ContentTemplate>
            <div class="main_right_cont minheigh">
                <div class="title">
                    <h2>
                        <asp:Label ID="lblHeader" runat="server" Text="Add Testimonial" meta:resourcekey="lblHeaderResource1"></asp:Label>
                    </h2>
                </div>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="personal_box alignleft">
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name" meta:resourcekey="lblFirstNameResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvFirstname" runat="server" ValidationGroup="validation" ControlToValidate="txtFirstName" ForeColor="Red" Display="None" ErrorMessage="Please enter first name" meta:resourcekey="rqvFirstnameResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveFirstName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvFirstname" Enabled="True" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="flttxtFirstname" runat="server" Enabled="True" TargetControlID="txtFirstName" ValidChars=".abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblLastname" runat="server" Text="Last Name" meta:resourcekey="lblLastnameResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtLastname" runat="server" MaxLength="50" meta:resourcekey="txtLastnameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvLastName" runat="server" ValidationGroup="validation" ControlToValidate="txtLastname" ForeColor="Red" Display="None" ErrorMessage="Please enter last name" meta:resourcekey="rqvLastNameResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveLastName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvLastName" Enabled="True" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="flttxtLastname" runat="server" Enabled="True" TargetControlID="txtLastname" ValidChars=".abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtEmail" runat="server" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvEmail" runat="server" ValidationGroup="validation" ControlToValidate="txtEmail" Display="None" ForeColor="Red" ErrorMessage="Please enter email" meta:resourcekey="rqvEmailResource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rgevpremail" runat="server" ValidationGroup="validation" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None" meta:resourcekey="rgevpremailResource1"></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveEmail" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvEmail" Enabled="True" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rgevepremail" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rgevpremail" Enabled="True" />
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblPageContent" runat="server" Text="Page Content" meta:resourcekey="lblPageContentResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtPageContent" TextMode="MultiLine" Height="100px" runat="server" meta:resourcekey="txtPageContentResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvPageContent" runat="server" ValidationGroup="validation" ControlToValidate="txtPageContent" Display="None" ForeColor="Red" ErrorMessage="Please enter Page Content" meta:resourcekey="rqvPageContentResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqvePageContent" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvPageContent" Enabled="True" />
                    </div>
                    <div class="date2">
                        <div class="date_cont">
                            <div class="date_cont_right">
                                <div class="supply-button3">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" ValidationGroup="validation" OnClick="btnSubmit_Click" meta:resourcekey="btnSubmitResource1" />
                                </div>
                                <div class="supply-button3">
                                    <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="5" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
