<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditRecommendedDentist.aspx.cs" Inherits="_4eOrtho.Admin.AddEditRecommendedDentist" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="cf">
        <div class="page_title">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        <h2 class="padd">
                            <asp:Label ID="lblHeader" runat="server" ></asp:Label>
                            <asp:Label ID="lblHeaderEdit" runat="server" Visible="False"></asp:Label></h2>
                    </td>
                    <td style="width: 50%;">
                        <span class="dark_btn_small">
                            <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/Admin/ListRecommendedDentist.aspx" meta:resourcekey="btnBackResource1"  />
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </div>
        <div class="widecolumn">
            <div class="personal_box alignleft">
                <div  class="parsonal_textfild" >
                    <label style="width: 128px;">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name" meta:resourcekey="lblFirstNameResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>

                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvFirstname" runat="server" ControlToValidate="txtFirstName" ValidationGroup="validation" ForeColor="Red" Display="None" ErrorMessage="Please enter first name" meta:resourcekey="rqvFirstnameResource1"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveFirstName" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rqvFirstname" Enabled="True" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="flttxtFirstname" runat="server" Enabled="True" TargetControlID="txtFirstName" ValidChars=".abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                </div>


                <div class="parsonal_textfild" >
                    <label style="width: 128px;">
                        <asp:Label ID="lblLastname" runat="server" Text="Last Name" meta:resourcekey="lblLastnameResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <asp:TextBox ID="txtLastname" runat="server" MaxLength="50" meta:resourcekey="txtLastnameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvLastName" ValidationGroup="validation" runat="server" ControlToValidate="txtLastname" ForeColor="Red" Display="None" ErrorMessage="Please enter last name" meta:resourcekey="rqvLastNameResource1"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveLastName" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rqvLastName" Enabled="True" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="flttxtLastname" runat="server" Enabled="True" TargetControlID="txtLastname" ValidChars=".abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890"></ajaxToolkit:FilteredTextBoxExtender>
                </div>


                <div  class="parsonal_textfild">
                    <label style="width: 128px;">
                        <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <asp:TextBox ID="txtEmail" runat="server" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvEmail" ValidationGroup="validation" runat="server" ControlToValidate="txtEmail" Display="None" ForeColor="Red" ErrorMessage="Please enter email" meta:resourcekey="rqvEmailResource1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rgevpremail" ValidationGroup="validation" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None" meta:resourcekey="rgevpremailResource1"></asp:RegularExpressionValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveEmail" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rqvEmail" Enabled="True" />
                    <ajaxToolkit:ValidatorCalloutExtender ID="rgevepremail" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rgevpremail" Enabled="True" />
                  
                </div>

                <div class="parsonal_textfild">
                    <label style="width: 128px;">

                        <asp:Label ID="lblCountry" runat="server" Text="Country" meta:resourcekey="lblCountryResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <div class="parsonal_select">
                        <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddlCountryResource1"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rqvcountry" runat="server" ValidationGroup="validation" ErrorMessage="Please select country" Display="None" ForeColor="Red" ControlToValidate="ddlCountry" meta:resourcekey="rqvcountryResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqvecountry" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvcountry" Enabled="True" />
                    </div>
                </div>

                <div  class="parsonal_textfild">
                    <label style="width: 128px;">
                        <asp:Label ID="lblState" runat="server" Text="State" meta:resourcekey="lblStateResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <div class="parsonal_select">
                        <asp:DropDownList ID="ddlState" runat="server" meta:resourcekey="ddlStateResource1"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rqvstate" runat="server" ValidationGroup="validation" ErrorMessage="Please select state" Display="None" ForeColor="Red" ControlToValidate="ddlState" meta:resourcekey="rqvstateResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqvestate" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvstate" Enabled="True" />
                    </div>
                </div>

                <div  class="parsonal_textfild">
                    <label style="width: 128px;">
                        <asp:Label ID="lblCity" runat="server" Text="City" meta:resourcekey="lblCityResource1"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtCity" runat="server" meta:resourcekey="txtCityResource1"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="bottom_btn tpadd alignright" style="width: 268px;">
            <span class="blue_btn">
                <asp:Button ID="btnSubmit" runat="server" Text="Save" ValidationGroup="validation" OnClick="btnSubmit_Click" meta:resourcekey="btnSubmitResource1" />
            </span><span class="dark_btn">
                <input type="reset" tabindex="7" title='<%= this.GetLocalResourceObject("Reset") %>'  value='<%= this.GetLocalResourceObject("Reset") %>' />
            </span>
        </div>
    </div>
</asp:Content>
