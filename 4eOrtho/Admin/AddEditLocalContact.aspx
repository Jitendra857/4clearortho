<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditLocalContact.aspx.cs" Inherits="_4eOrtho.Admin.AddEditLocalContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .parsonal_textfild label {
            width: 250px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upLocalContact" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <h2 class="padd">
                                    <asp:Label ID="lblHeader" runat="server" Text="Add Local Contact User" meta:resourcekey="lblHeaderResource1"></asp:Label>
                                    <asp:Label ID="lblHeaderEdit" runat="server" Text="Edit Local Contact User" meta:resourcekey="lblHeaderEditResource1" Visible="False"></asp:Label></h2>
                            </td>
                            <td style="width: 50%;">
                                <span class="dark_btn_small">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" meta:resourcekey="btnBackResource1" PostBackUrl="~/Admin/ListLocalContact.aspx" />
                                </span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <div class="widecolumn">
                    <div class="personal_box alignleft">

                        <div class="parsonal_textfild">
                            <label class="lbluserWidth">
                                <asp:Literal ID="ltrEmailAddress" runat="server" Text="Email Address" meta:resourcekey="ltrEmailAddressResource1"></asp:Literal>
                                <span class="required">*</span><span class="alignright">:</span></label>
                            <asp:TextBox ID="txtEmailAddress" runat="server" MaxLength="100" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="ReqEmailAddress" TabIndex="1" ControlToValidate="txtEmailAddress"
                                Display="None" CssClass="error" ValidationGroup="validation" SetFocusOnError="True"
                                ErrorMessage="Please Enter Email Address" meta:resourcekey="ReqEmailAddressResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceReqEmailAddress" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="ReqEmailAddress" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="regEmailAddress" Display="None" runat="server" TabIndex="1"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="validation"
                                SetFocusOnError="True" CssClass="error"
                                ControlToValidate="txtEmailAddress"
                                ErrorMessage="Your Email Address must be in valid format " meta:resourcekey="regEmailAddressResource1"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceregEmailAddress" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="regEmailAddress" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator runat="server" ID="cstmtxtEmailAddress" ControlToValidate="txtEmailAddress" TabIndex="1"
                                OnServerValidate="cstmtxtEmailAddress_ServerValidate" ValidationGroup="validation"
                                SetFocusOnError="True" Display="None" CssClass="error"
                                ErrorMessage="Your Email Address has already been taken, please try another one" meta:resourcekey="cstmtxtEmailAddressResource1" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecstmtxtEmailAddress" runat="server"
                                CssClass="customCalloutStyle" TargetControlID="cstmtxtEmailAddress" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>

                        <div class="parsonal_textfild">
                            <label class="lbluser">
                                <asp:Literal ID="ltrOrganisationName" runat="server" Text="Organisation Name" meta:resourcekey="ltrOrganisationNameResource1"></asp:Literal>
                                <span class="required">*</span><span class="alignright">:</span></label>
                            <asp:TextBox ID="txtOrganisationName" runat="server" MaxLength="30" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="ReqOrganisationName" Display="None" ControlToValidate="txtOrganisationName"
                                SetFocusOnError="True" CssClass="error" ValidationGroup="validation" TabIndex="2"
                                ErrorMessage="Please Enter Organisation Name" meta:resourcekey="ReqOrganisationNameResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceReqOrganisationName" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="ReqOrganisationName" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="lbluser">
                                <asp:Literal ID="ltrFirstName" runat="server" Text="ltrFirstNameResource1" meta:resourcekey="ltrFirstNameResource1"></asp:Literal>
                                <span class="required">*</span><span class="alignright">:</span></label>
                            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="30" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="ReqFirstName" Display="None" ControlToValidate="txtFirstName"
                                SetFocusOnError="True" CssClass="error" ValidationGroup="validation" TabIndex="2"
                                ErrorMessage="Please Enter First Name" meta:resourcekey="ReqFirstNameResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceReqFirstName" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="ReqFirstName" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="lbluserWidth">
                                <asp:Literal ID="ltrLastName" runat="server" Text="Last Name" meta:resourcekey="ltrLastNameResource1"></asp:Literal>
                                <span class="required">*</span><span class="alignright">:</span></label>
                            <asp:TextBox ID="txtLastName" runat="server" MaxLength="30" TabIndex="3"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="ReqLastName" Display="None" ControlToValidate="txtLastName"
                                SetFocusOnError="True" CssClass="error" ValidationGroup="validation" TabIndex="3"
                                ErrorMessage="Please Enter Last Name" meta:resourcekey="ReqLastNameResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceReqLastName" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="ReqLastName" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>

                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <strong>
                                    <asp:Label runat="server" ID="lblAddress" Text="Address" meta:resourcekey="lblAddressResource1"></asp:Label>
                                    <span class="alignright">:</span> </strong>
                            </label>
                        </div>
                        <div class="clear">
                            &nbsp;
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblCountry" Text="Country" meta:resourcekey="lblCountryResource1"></asp:Label>
                                <span class="asteriskclass" id="span1" runat="server">*</span> <span class="alignright">:</span></label>
                            <asp:RequiredFieldValidator ID="rqvddlCountry" ForeColor="Red" runat="server" SetFocusOnError="True"
                                ControlToValidate="ddlCountry" Display="None" InitialValue="0" ErrorMessage="Please select country." TabIndex="4"
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvddlCountryResource1"></asp:RequiredFieldValidator>
                            <div class="parsonal_select">
                                <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                    AutoPostBack="True" TabIndex="4">
                                </asp:DropDownList>
                                <ajaxToolkit:ValidatorCalloutExtender ID="cmverqvddlCountry" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rqvddlCountry" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="clear">
                            &nbsp;
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblState" Text="State" meta:resourcekey="lblStateResource1"></asp:Label>
                                <span class="required">*</span><span class="alignright">:</span></label>
                            <div class="parsonal_select">
                                <asp:DropDownList ID="ddlState" runat="server" TabIndex="5">
                                    <asp:ListItem Text="Select State" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvState" ForeColor="Red" runat="server" SetFocusOnError="True"
                                    ControlToValidate="ddlState" Display="None" InitialValue="0" ErrorMessage="Please select state." TabIndex="4"
                                    CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rfvStateResource"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcestate" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rfvState" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="clear">
                            &nbsp;
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblCity" Text="City" meta:resourcekey="lblCityResource1"></asp:Label>
                                <span class="required">*</span><span class="alignright">:</span></label>
                            <asp:TextBox ID="txtCity" runat="server" MaxLength="50" TabIndex="6"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCity" ForeColor="Red" runat="server" SetFocusOnError="True"
                                ControlToValidate="txtCity" Display="None" ErrorMessage="Please enter city." TabIndex="4"
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rfvCityResource"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceCity" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rfvCity" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="clear">
                            &nbsp;
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblStreet" Text="Street" meta:resourcekey="lblStreetResource1"></asp:Label>
                                <span class="required">*</span><span class="alignright">:</span></label>
                            <asp:TextBox ID="txtStreet" runat="server" MaxLength="100" TabIndex="7"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStreet" ForeColor="Red" runat="server" SetFocusOnError="True"
                                ControlToValidate="txtStreet" Display="None" ErrorMessage="Please enter street." TabIndex="4"
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rfvStreetResource"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceStreet" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rfvStreet" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="clear">
                            &nbsp;
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblZipCode" Text="Zip Code" meta:resourcekey="lblZipCodeResource1"></asp:Label>
                                <span class="required">*</span> <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtZip" runat="server" MaxLength="10" TabIndex="8"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtZip" ForeColor="Red" runat="server" SetFocusOnError="True"
                                ControlToValidate="txtZip" Display="None" ErrorMessage="Please enter zip code." TabIndex="4"
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rfvtxtZipResource"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcerfvtxtZip" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rfvtxtZip" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteZip" runat="server" Enabled="True" TargetControlID="txtZip"
                                ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz- " />
                            <asp:RegularExpressionValidator ID="rgvZipCode" Display="None" runat="server" ControlToValidate="txtZip" TabIndex="8"
                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9-]+\s?){5,10}$" ValidationGroup="validation" meta:resourcekey="onlyNumericResource"
                                CssClass="errormsg" ErrorMessage="only letters,digits,hyphen with single space allowed."></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="valZipCode" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rgvZipCode" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="clear">
                            &nbsp;
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <strong>
                                    <asp:Label runat="server" ID="lblContact" Text="Contact" meta:resourcekey="lblContactResource1"></asp:Label>
                                    <span class="alignright">: </span></strong>
                            </label>
                        </div>
                        <div class="clear">
                            &nbsp;
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblMobile" Text="Mobile" meta:resourcekey="lblMobileResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtmobile" runat="server" MaxLength="20" TabIndex="9"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvtxtMobile" ForeColor="Red" runat="server" ControlToValidate="txtmobile"
                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Mobile Number" TabIndex="9"
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvtxtMobileResource" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqvetxtMobile" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvtxtMobile" Enabled="True" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteMobile" runat="server" Enabled="True"
                                TargetControlID="txtmobile" ValidChars="0123456789-+()" />
                            <asp:RegularExpressionValidator ID="rgvMobile" Display="None" runat="server" ControlToValidate="txtMobile" TabIndex="9"
                                SetFocusOnError="True" ValidationExpression="[0-9-+()]{6,15}$" ValidationGroup="validation"
                                CssClass="errormsg" ErrorMessage="Only Numeric Values,-, +, () are allowed" meta:resourcekey="onlyNumericResource"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="valMobile" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rgvMobile" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblHome" Text="Home" meta:resourcekey="lblHomeResource1"></asp:Label><span
                                    class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txthome" runat="server" MaxLength="20" TabIndex="10"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteHome" runat="server" Enabled="True" TargetControlID="txthome"
                                ValidChars="0123456789-+()" />
                            <asp:RegularExpressionValidator ID="rgvHome" Display="None" runat="server" ControlToValidate="txtHome"
                                SetFocusOnError="True" ValidationExpression="[0-9-+()]{6,15}$" ValidationGroup="validation"
                                CssClass="errormsg" ErrorMessage="Only Numeric Values,-, +, () are allowed" meta:resourcekey="onlyNumericResource"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="valHome" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rgvHome" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblWork" Text="Work" meta:resourcekey="lblWorkResource1"></asp:Label><span
                                    class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtwork" runat="server" MaxLength="20" TabIndex="11"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteWork" runat="server" Enabled="True" TargetControlID="txtwork"
                                ValidChars="0123456789-+()" />
                            <asp:RegularExpressionValidator ID="rgvWork" Display="None" runat="server" ControlToValidate="txtWork"
                                SetFocusOnError="True" ValidationExpression="[0-9-+()]{6,15}$" ValidationGroup="validation"
                                CssClass="errormsg" ErrorMessage="Only Numeric Values,-, +, () are allowed" meta:resourcekey="onlyNumericResource"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="val" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rgvWork" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                    </div>
                </div>
                <div class="bottom_btn tpadd alignright" style="width: 268px;">
                    <span class="blue_btn">
                        <asp:Button ID="btnSave" runat="server" Text="Save" TabIndex="12" meta:resourcekey="btnSaveResource1" ValidationGroup="validation" OnClick="btnSave_Click" />
                    </span><span class="dark_btn">
                        <asp:Button ID="btnReset" runat="server" Text="Reset" TabIndex="12" CausesValidation="false" OnClick="btnReset_Click" />
                    </span>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $('[id$=txtEmailAddress]').focus();
    </script>
</asp:Content>
