<%@ Page Title="4ClearOrtho - Add Recommend Dentist" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="AddRecommendedDentist.aspx.cs" Inherits="_4eOrtho.AddRecommendedDentist" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="main_right_cont minheigh">
                <div class="title">
                    <h2>
                        <asp:Label ID="lblheader" runat="server" Text="Add Recommend Dentist" meta:resourcekey="lblheaderResource1"></asp:Label>
                    </h2>
                </div>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="personal_box alignleft" style="width: 100%">
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
                            <asp:Label ID="lblEmail" runat="server" Text="Email Id" meta:resourcekey="lblEmailResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtEmail" runat="server" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ValidationGroup="validation" ControlToValidate="txtEmail" ForeColor="Red" Display="None" ErrorMessage="Please enter email id." meta:resourcekey="rfvEmailResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="customCalloutStyle" TargetControlID="rfvEmail" Enabled="True" />
                        <asp:RegularExpressionValidator ID="rgevpremail" runat="server" ValidationGroup="validation" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None" meta:resourcekey="rgevpremailResource1"></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rgevepremail" runat="server" CssClass="customCalloutStyle" TargetControlID="rgevpremail" Enabled="True" />
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblCountry" runat="server" Text="Country" meta:resourcekey="lblCountryResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <div class="parsonal_select">
                            <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddlCountryResource1"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rqvcountry" runat="server" InitialValue="0" ErrorMessage="Please select country" ValidationGroup="validation" Display="None" ForeColor="Red" ControlToValidate="ddlCountry" meta:resourcekey="rqvcountryResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqvecountry" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvcountry" Enabled="True" />
                        </div>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblState" runat="server" Text="State" meta:resourcekey="lblStateResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <div class="parsonal_select ">
                            <asp:DropDownList ID="ddlState" runat="server" meta:resourcekey="ddlStateResource1"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rqvstate" runat="server" InitialValue="0" ErrorMessage="Please select state" ValidationGroup="validation" Display="None" ForeColor="Red" ControlToValidate="ddlState" meta:resourcekey="rqvstateResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqvestate" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvstate" Enabled="True" />
                        </div>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblCity" runat="server" Text="City" meta:resourcekey="lblCityResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtCity" runat="server" meta:resourcekey="txtCityResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ErrorMessage="Please select city." ValidationGroup="validation" Display="None" ForeColor="Red" ControlToValidate="txtCity" meta:resourcekey="rfvCityResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rfvCity" Enabled="True" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtCity" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"></ajaxToolkit:FilteredTextBoxExtender>
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
