<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditDoctor.aspx.cs" Inherits="_4eOrtho.Admin.AddEditDoctor" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/Colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="../Styles/Jquery-UI/jquery-ui-1.8.23.custom.css" rel="Stylesheet" type="text/css" />
    <link href="../Scripts/Colorbox/colorbox.css" rel="stylesheet" />
    <script type="text/javascript">
        var ShouldAllowPrefUpdate = true;
        window.onload = function () {
            document.getElementById('<%=txtEmailid.ClientID%>').focus();
        };
        function pageLoad() {
            var currentDate = new Date('<%= _4eOrtho.BAL.BaseEntity.GetServerDateTime %>');
            var PreferredValue = $('#' + '<%= txtTitle.ClientID %>').val();
            if (PreferredValue == '') {
                ShouldAllowPrefUpdate = true;
            }
            else {
                ShouldAllowPrefUpdate = false;
            }

            currentDate.setFullYear(currentDate.getFullYear() - 18, 11, currentDate.getDate());
            $('.From-Date').datepicker({
                showOn: "button",
                buttonText: "Select Date",
                buttonImage: "images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                yearRange: "-100:+0",
                maxDate: currentDate
            });
            $('.not-edit').attr("readonly", "readonly");

            jQuery(".iframe-Photo").colorbox({
                iframe: true,
                width: "530px",
                height: "248",
                overlayClose: false,
                escKey: true
            });
            LoadImageByRole();
        }
        function CheckConfirmPassword(sender, args) {
            if (document.getElementById('<%= txtPassword.ClientID%>').value != "" && args.Value == "") {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }
        function CharacterChecking(sender, args) {
            var counter = 0;
            var pattern = new RegExp("[a-z]");
            for (var i = 0; i < args.Value.length; i++) {
                if (pattern.test(args.Value[i].toLowerCase())) {
                    counter++;
                }
            }
            if (counter >= 3) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        function OpenPhotoUploadbyPatientId(doctorID) {
            $('#aPhotoUploadLink').attr("href", "../PhotoUpload.aspx?did=" + doctorID);
            $('#aPhotoUploadLink').click();
            return false;
        }

        function OpenPhotoUpload() {
            OpenPhotoUploadbyPatientId($("#" + '<% = hdDoctorId.ClientID %>').val());
            return false;
        }
        function RemovePhoto() {
            if (confirm('Are you sure you want to remove this photo?')) {
                if ($('#ContentPlaceHolder1_rbtnMale')[0].checked) {
                    $('#ContentPlaceHolder1_imgPatientPhoto')[0].src = "../Photograph/male_sm.jpg";
                    $('#ContentPlaceHolder1_hdMalePhotoName')[0].value = "male_sm.jpg";
                }
                else if ($('#ContentPlaceHolder1_rbtnFemale')[0].checked) {
                    $('#ContentPlaceHolder1_imgPatientPhoto')[0].src = "../Photograph/female_sm.jpg";
                    $('#ContentPlaceHolder1_hdFemalePhotoName')[0].value = "female_sm.jpg";
                }
                return true;
            }
            else
                return false;
        }
        function LoadImageByRole() {
            var male = $('#' + '<%= hdMalePhotoName.ClientID %>')[0].value;
            var female = $('#' + '<%= hdFemalePhotoName.ClientID %>')[0].value;

            if (male == "male_sm.jpg" && $(female == "female_sm.jpg")) {
                if ($('#' + '<%= rbtnMale.ClientID %>')[0].checked) {
                    $('#' + '<%= imgPatientPhoto.ClientID %>')[0].src = "../Photograph/" + male;
                }
                else if ($('#' + '<%= rbtnFemale.ClientID %>')[0].checked) {
                    $('#' + '<%= imgPatientPhoto.ClientID %>')[0].src = "../Photograph/" + female;
                }
            }
            else {
                if (male != "male_sm.jpg")
                    $('#' + '<%= imgPatientPhoto.ClientID %>')[0].src = "../Photograph/" + male;
                else
                    $('#' + '<%= imgPatientPhoto.ClientID %>')[0].src = "../Photograph/" + female;
            }
        }
        function FillPreferred() {
            var FistNameValue = $('#' + '<%= txtFirstName.ClientID %>').val();
            var LastNameValue = $('#' + '<%= txtLastName.ClientID %>').val();
            var MIValue = $('#' + '<%= txtMi.ClientID %>').val();
            var PreferredValue = $('#' + '<%= txtTitle.ClientID %>').val();

            //alert(ShouldAllowPrefUpdate);
            if (ShouldAllowPrefUpdate) {
                $('#' + '<%= txtTitle.ClientID %>').val(FistNameValue + " " + MIValue + " " + LastNameValue);
            }
        }
    </script>
    <style type="text/css">
        .personal_box {
            width: 475px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="cf">
        <asp:HiddenField ID="hdDoctorId" runat="server" />
        <%--<asp:HiddenField ID="hdPhotoName" runat="server" Value="no-photo.jpg" />--%>
        <asp:HiddenField ID="hdMalePhotoName" runat="server" Value="male_sm.jpg" />
        <asp:HiddenField ID="hdFemalePhotoName" runat="server" Value="female_sm.jpg" />

        <a href="PhotoUpload.aspx" id="aPhotoUploadLink" class="iframe-Photo" style="display: none;"></a>
        <asp:UpdatePanel ID="upAddEditDoctor" runat="server">
            <ContentTemplate>
                <div class="page_title">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <h2 class="padd">
                                    <asp:Label ID="lblHeader" runat="server" Text="Add Doctor Detail" meta:resourcekey="lblHeaderResource1"></asp:Label>
                                    <asp:Label ID="lblHeaderEdit" runat="server" Text="Edit Doctor Detail"
                                        Visible="False" meta:resourcekey="lblHeaderEditResource1"></asp:Label>
                                </h2>
                            </td>
                            <td style="width: 50%;">
                                <span class="dark_btn_small">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/admin/ListDoctors.aspx"
                                        TabIndex="39" meta:resourcekey="btnBackResource1" Style="width: 100px;" />
                                </span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="widecolumn">
                    <div class="personal_box alignright">
                        <asp:Image ImageUrl="Photograph/no-photo.jpg" ID="imgPatientPhoto" Width="175px"
                            runat="server" Height="204px" meta:resourcekey="imgPatientPhotoResource1" />
                        <div style="float: right; padding: 50px;">
                            <asp:LinkButton ID="lnkPhotoChange" Style="display: inline;" Text="Change Photo"
                                runat="server" CssClass="linkbuttonclass" OnClientClick="return OpenPhotoUpload();" meta:resourcekey="lnkPhotoChangeResource1" />&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkRemovePhoto" Width="102px" Style="display: inline;" Text="Remove Photo"
                                runat="server" CssClass="linkbuttonclass" OnClientClick="return RemovePhoto();" meta:resourcekey="lnkRemovePhotoResource1" />
                        </div>
                    </div>
                    <div class="personal_box alignleft">
                        <h3>
                            <asp:Label runat="server" ID="lblPersonalDetail" Text="Personal Detail" meta:resourcekey="lblPersonalDetailResource1"></asp:Label></h3>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblEmail" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtEmailid" runat="server" MaxLength="50" TabIndex="1" AutoPostBack="True" OnTextChanged="txtEmailid_TextChanged" meta:resourcekey="txtEmailidResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvtxtEmailId" ForeColor="Red" runat="server" ControlToValidate="txtEmailid"
                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Email ID" CssClass="errormsg"
                                ValidationGroup="validation" meta:resourcekey="rqvtxtEmailIdResource1" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveCalltxtEmailAddress" runat="server"
                                CssClass="customCalloutStyle" TargetControlID="rqvtxtEmailId" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="rgvEmailAddressCheck" Display="None" runat="server"
                                SetFocusOnError="True" ValidationExpression="[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,5}"
                                ValidationGroup="validation" CssClass="errormsg" ControlToValidate="txtEmailid"
                                ErrorMessage="Please Enter Valid Email ID eg: 'abc@yahoo.com'" meta:resourcekey="rgvEmailAddressCheckResource1"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rgveCalltxtEmailAddressCheck" runat="server"
                                CssClass="customCalloutStyle" TargetControlID="rgvEmailAddressCheck" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator runat="server" ID="custxtEmailid" ControlToValidate="txtEmailid"
                                SetFocusOnError="True" Display="None" OnServerValidate="custxtEmailid_ServerValidate"
                                ValidationGroup="validation" CssClass="error" ErrorMessage="Email ID already exist, please try another one" meta:resourcekey="custxtEmailidResource1" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="rgveCallCustomtxtEmailid" runat="server"
                                CssClass="customCalloutStyle" TargetControlID="custxtEmailid" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblFirstName" Text="First Name" meta:resourcekey="lblFirstNameResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span></label>
                            <asp:TextBox ID="txtFirstName" runat="server" OnBlur="return FillPreferred();return false;" MaxLength="50" TabIndex="2" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvtxtFirstName" ForeColor="Red" runat="server" ControlToValidate="txtFirstName"
                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter First Name"
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvtxtFirstNameResource1" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveCalltxtFirstName" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvtxtFirstName" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:FilteredTextBoxExtender ID="flttxtFirstName" runat="server" Enabled="True"
                                TargetControlID="txtFirstName" ValidChars=".abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblLastName" Text="Last Name" meta:resourcekey="lblLastNameResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span></label>
                            <asp:TextBox ID="txtLastName" runat="server" OnBlur="return FillPreferred();return false;" MaxLength="50" TabIndex="3" meta:resourcekey="txtLastNameResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvtxtLastName" ForeColor="Red" runat="server" ControlToValidate="txtLastName"
                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Last Name" CssClass="errormsg"
                                ValidationGroup="validation" meta:resourcekey="rqvtxtLastNameResource1" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveCalltxtLastName" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvtxtLastName" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:FilteredTextBoxExtender ID="flttxtLastName" runat="server" Enabled="True"
                                TargetControlID="txtLastName" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>
                        <div class="parsonal_textfild  phonetab">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblMiddleInitial" Text="MI (Middle Initial)" meta:resourcekey="lblMiddleInitialResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtMi" runat="server" OnBlur="return FillPreferred();return false;" MaxLength="5" TabIndex="4" meta:resourcekey="txtMiResource1"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="flttxtMi" runat="server" Enabled="True"
                                TargetControlID="txtMi" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>
                        <div class="parsonal_textfild phonetab">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblTitle" Text="Title" meta:resourcekey="lblTitleResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" TabIndex="5" meta:resourcekey="txtTitleResource1"></asp:TextBox>
                        </div>

                        <div class="parsonal_textfild phonetab">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblGender" Text="Gender" meta:resourcekey="lblGenderResource"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <div class="radio-selection">
                                <asp:RadioButton ID="rbtnMale" runat="server" GroupName="Gender" Checked="True" Text="Male" onclick="return LoadImageByRole();" meta:resourcekey="rbtnMaleResource" />
                                <asp:RadioButton ID="rbtnFemale" runat="server" GroupName="Gender" Text="Female" onclick="return LoadImageByRole();" meta:resourcekey="rbtnFemaleResource" />
                            </div>

                        </div>
                        <div class="clear"></div>

                        <div class="parsonal_textfild phonetab">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblDoctorID" Text="Doctor ID" meta:resourcekey="lblDoctorIDResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span></label>
                            <asp:TextBox ID="txtDoctorNo" runat="server" MaxLength="15" TabIndex="6" meta:resourcekey="txtDoctorNoResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvtxtDoctorID" ForeColor="Red" runat="server" ControlToValidate="txtDoctorNo"
                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Doctor ID" CssClass="errormsg"
                                ValidationGroup="validation" meta:resourcekey="rqvtxtDoctorIDResource1" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveCalltxtDoctorID" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvtxtDoctorID" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator runat="server" ID="custxtDoctorNo" ControlToValidate="txtDoctorNo"
                                SetFocusOnError="True" Display="None"
                                ValidationGroup="validation" CssClass="error" ErrorMessage="Doctor ID already exist, please try another one" meta:resourcekey="custxtDoctorNoResource1" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveCallCustomtxtDoctorNo" runat="server"
                                CssClass="customCalloutStyle" TargetControlID="custxtDoctorNo" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbtxtDoctorNo" runat="server" Enabled="True"
                                TargetControlID="txtDoctorNo" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" />
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblDOB" Text="DOB" meta:resourcekey="lblDOBResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtDOB" runat="server" CssClass="From-Date not-edit textfild search-datepicker"
                                TabIndex="7" meta:resourcekey="txtDOBResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="personal_box alignright">
                        <h3>
                            <asp:Label runat="server" ID="lblSpecialtyFees" Text="Specialty Information" meta:resourcekey="lblSpecialtyFeesResource1"></asp:Label>
                        </h3>
                        <%--<div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblDegree" Text="Degree" meta:resourcekey="lblDegreeResource1"></asp:Label><span
                                    class="alignright">:</span></label>
                            <div class="parsonal_select">
                                <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="19" meta:resourcekey="ddlDegreeResource1">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="clear"></div>--%>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblSpecialty" Text="Specialty" meta:resourcekey="lblSpecialtyResource1"></asp:Label><span
                                    class="alignright">:</span></label>
                            <div class="parsonal_select">
                                <asp:DropDownList ID="ddlSpecialty" runat="server" TabIndex="19" meta:resourcekey="ddlSpecialtyResource1">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="clear">
                            &nbsp;
                        </div>
                    </div>
                    <div class="personal_box alignleft" id="dvAccountInformation" runat="server">
                        <h3>
                            <asp:Label runat="server" ID="lblAccountInformation" Text="Account Information" meta:resourcekey="lblAccountInformationResource1"></asp:Label>
                        </h3>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblPassword" Text="Password" meta:resourcekey="lblPasswordResource1"></asp:Label>
                                <span class="asteriskclass" id="spanPassword" runat="server">*</span> <span class="alignright">:</span></label>
                            <input style="display: none" type="password" name="txtPassword" />
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
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblConfirmPassword" Text="Confirm Password" meta:resourcekey="lblConfirmPasswordResource1"></asp:Label>
                                <span class="asteriskclass" id="spanConfirmPassword" runat="server">*</span> <span
                                    class="alignright">:</span></label>
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
                            <asp:CustomValidator ID="cvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                ErrorMessage="Please enter confirm password" ForeColor="Red" SetFocusOnError="True"
                                Display="None" CssClass="errormsg" ValidateEmptyText="True" ValidationGroup="validation"
                                ClientValidationFunction="CheckConfirmPassword" meta:resourcekey="cvConfirmPasswordResource1"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                CssClass="customCalloutStyle" TargetControlID="cvConfirmPassword" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <span class="alignright">&nbsp;</span>
                            </label>
                            <asp:CheckBox ID="chkshowPassword" runat="server" OnCheckedChanged="chkshowpassowrdtextchange" AutoPostBack="True" TabIndex="10" meta:resourcekey="chkshowPasswordResource1" />
                            <asp:HiddenField ID="hdPassword" runat="server" />
                            <asp:Label ID="lblShowPassword" runat="server" Text="Show Password " meta:resourcekey="lblShowPasswordResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="personal_box alignright">
                        <h3>
                            <asp:Label runat="server" ID="lblNumbersInformation" Text="Numbers Information" meta:resourcekey="lblNumbersInformationResource1"></asp:Label>
                        </h3>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblSSN" Text="SSN#" meta:resourcekey="lblSSNResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtSSNNo" runat="server" MaxLength="11" TabIndex="22" meta:resourcekey="txtSSNNoResource1"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbtxtSSNNo" runat="server" Enabled="True"
                                TargetControlID="txtSSNNo" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" />
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblStateID" Text="State ID#" meta:resourcekey="lblStateIDResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtStateIDNo" runat="server" MaxLength="15" TabIndex="23" meta:resourcekey="txtStateIDNoResource1"></asp:TextBox>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblTIN" Text="TIN#" meta:resourcekey="lblTINResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtTINNo" runat="server" MaxLength="15" TabIndex="24" meta:resourcekey="txtTINNoResource1"></asp:TextBox>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblMedical" Text="Medical#" meta:resourcekey="lblMedicalResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtMedicalNo" runat="server" MaxLength="15" TabIndex="25" meta:resourcekey="txtMedicalNoResource1"></asp:TextBox>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblDrugID" Text="Drug ID#" meta:resourcekey="lblDrugIDResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtDrugNo" runat="server" MaxLength="15" TabIndex="26" meta:resourcekey="txtDrugNoResource1"></asp:TextBox>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblNPI" Text="NPI#" meta:resourcekey="lblNPIResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtNPINo" runat="server" MaxLength="15" TabIndex="27" meta:resourcekey="txtNPINoResource1"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbtxtNPINo" runat="server" Enabled="True"
                                TargetControlID="txtNPINo" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" />
                            <asp:RegularExpressionValidator ID="rgvtxtNPINo" Display="None" runat="server" ControlToValidate="txtNPINo"
                                SetFocusOnError="True" ValidationExpression="^[0-9a-zA-Z]{10}([0-9a-zA-Z]{5})?$"
                                ValidationGroup="validation" CssClass="errormsg" ErrorMessage="NPI number must be 10 or 15 length" meta:resourcekey="rgvtxtNPINoResource1"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveCalltxtNPINo" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rgvtxtNPINo" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblCrossOrShield" Text="Cross Or Shield" meta:resourcekey="lblCrossOrShieldResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <div class="radio-selection" style="width: auto;">
                                <asp:RadioButton ID="rdbblueCross" runat="server" GroupName="rdbgender"
                                    Checked="True" Text="Blue Cross" TabIndex="28" meta:resourcekey="rdbblueCrossResource1" />
                                <asp:RadioButton ID="rdbblueSchield" runat="server" GroupName="rdbgender"
                                    Text="Blue Shield" TabIndex="30" meta:resourcekey="rdbblueSchieldResource1" />
                            </div>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblCrossShieldValue" Text="Cross/Shield Value" meta:resourcekey="lblCrossShieldValueResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtCrossOrSchieldValue" runat="server" MaxLength="15" TabIndex="29" meta:resourcekey="txtCrossOrSchieldValueResource1"></asp:TextBox>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblProv" Text="Prov#" meta:resourcekey="lblProvResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtProv" runat="server" MaxLength="15" TabIndex="30" meta:resourcekey="txtProvResource1"></asp:TextBox>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblOffice" Text="Office#" meta:resourcekey="lblOfficeResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtOffice" runat="server" MaxLength="15" TabIndex="31" meta:resourcekey="txtOfficeResource1"></asp:TextBox>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblMedicare" Text="Medicare#" meta:resourcekey="lblMedicareResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtMedicare" runat="server" MaxLength="15" TabIndex="32" meta:resourcekey="txtMedicareResource1"></asp:TextBox>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblOtherID" Text="Other ID#" meta:resourcekey="lblOtherIDResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtOtherID" runat="server" MaxLength="15" TabIndex="33" meta:resourcekey="txtOtherIDResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="personal_box alignleft">
                        <h3>
                            <asp:Label runat="server" ID="lblContactInformation" Text="Contact Information" meta:resourcekey="lblContactInformationResource1"></asp:Label>
                        </h3>
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
                                ControlToValidate="ddlCountry" Display="None" InitialValue="0" ErrorMessage="Please select country."
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvddlCountryResource1"></asp:RequiredFieldValidator>
                            <div class="parsonal_select">
                                <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                    AutoPostBack="True" TabIndex="11" meta:resourcekey="ddlCountryResource2">
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
                                <asp:Label runat="server" ID="lblState" Text="State" meta:resourcekey="lblStateResource1"></asp:Label><span
                                    class="alignright">:</span></label>
                            <div class="parsonal_select">
                                <asp:DropDownList ID="ddlState" runat="server" TabIndex="12" meta:resourcekey="ddlStateResource1">
                                    <asp:ListItem Text="Select State" Value="0" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="clear">
                            &nbsp;
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblCity" Text="City" meta:resourcekey="lblCityResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtCity" runat="server" MaxLength="50" TabIndex="13" meta:resourcekey="txtCityResource1"></asp:TextBox>
                        </div>
                        <div class="clear">
                            &nbsp;
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblStreet" Text="Street" meta:resourcekey="lblStreetResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtStreet" runat="server" MaxLength="100" TabIndex="14" meta:resourcekey="txtStreetResource1"></asp:TextBox>
                        </div>
                        <div class="clear">
                            &nbsp;
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblZipCode" Text="Zip Code" meta:resourcekey="lblZipCodeResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <asp:TextBox ID="txtZip" runat="server" MaxLength="10" TabIndex="15" meta:resourcekey="txtZipResource1"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteZip" runat="server" Enabled="True" TargetControlID="txtZip"
                                ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz- " />
                            <asp:RegularExpressionValidator ID="rgvZipCode" Display="None" runat="server" ControlToValidate="txtZip"
                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9-]+\s?){5,10}$" ValidationGroup="validation"
                                CssClass="errormsg" ErrorMessage="only letters,digits,hyphen with single space allowed." meta:resourcekey="rgvZipCodeResource1"></asp:RegularExpressionValidator>
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
                            <asp:TextBox ID="txtmobile" runat="server" MaxLength="20" TabIndex="16" meta:resourcekey="txtmobileResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvtxtMobile" ForeColor="Red" runat="server" ControlToValidate="txtmobile"
                                SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Mobile Number"
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvtxtMobileResource1" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqvetxtMobile" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvtxtMobile" Enabled="True" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteMobile" runat="server" Enabled="True"
                                TargetControlID="txtmobile" ValidChars="0123456789-+()" />
                            <asp:RegularExpressionValidator ID="rgvMobile" Display="None" runat="server" ControlToValidate="txtMobile"
                                SetFocusOnError="True" ValidationExpression="[0-9-+()]{6,15}$" ValidationGroup="validation"
                                CssClass="errormsg" ErrorMessage="Only Numeric Values,-, +, () are allowed" meta:resourcekey="rgvMobileResource1"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="valMobile" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rgvMobile" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblHome" Text="Home" meta:resourcekey="lblHomeResource1"></asp:Label><span
                                    class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txthome" runat="server" MaxLength="20" TabIndex="17" meta:resourcekey="txthomeResource1"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteHome" runat="server" Enabled="True" TargetControlID="txthome"
                                ValidChars="0123456789-+()" />
                            <asp:RegularExpressionValidator ID="rgvHome" Display="None" runat="server" ControlToValidate="txtHome"
                                SetFocusOnError="True" ValidationExpression="[0-9-+()]{6,15}$" ValidationGroup="validation"
                                CssClass="errormsg" ErrorMessage="Only Numeric Values,-, +, () are allowed" meta:resourcekey="rgvHomeResource1"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="valHome" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rgvHome" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblWork" Text="Work" meta:resourcekey="lblWorkResource1"></asp:Label><span
                                    class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtwork" runat="server" MaxLength="20" TabIndex="18" meta:resourcekey="txtworkResource1"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteWork" runat="server" Enabled="True" TargetControlID="txtwork"
                                ValidChars="0123456789-+()" />
                            <asp:RegularExpressionValidator ID="rgvWork" Display="None" runat="server" ControlToValidate="txtWork"
                                SetFocusOnError="True" ValidationExpression="[0-9-+()]{6,15}$" ValidationGroup="validation"
                                CssClass="errormsg" ErrorMessage="Only Numeric Values,-, +, () are allowed" meta:resourcekey="rgvWorkResource1"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="val" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rgvWork" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="personal_box alignright">
                        <div class="parsonal_textfild">
                            <label class="commondoc">
                                <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                                <span class="alignright">:</span></label>
                            <div style="padding-top: 6px;">
                                <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="35" meta:resourcekey="chkIsActiveResource1" />
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="bottom_btn tpadd alignright" style="width: 310px;">
                        <span class="blue_btn">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="validation" OnClick="btnSubmit_Click"
                                TabIndex="36" meta:resourcekey="btnSubmitResource1" />
                        </span><span class="dark_btn">
                            <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="37" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                        </span>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upAddEditDoctor"
        DisplayAfter="10">
        <ProgressTemplate>
            <div class="processbar1">
                <img src="../Content/images/loading.gif" alt="loading" style="top: 50%; left: 50%; position: absolute;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%--<div id="container" class="cf">
        <div class="page_title">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        <h2 class="padd">
                            <asp:Label ID="lblHeader" runat="server"></asp:Label>
                            <asp:Label ID="lblHeaderEdit" runat="server" Visible="False"></asp:Label></h2>
                    </td>
                    <td style="width: 50%;">
                        <span class="dark_btn_small">
                            <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/ListDoctors.aspx"
                                TabIndex="5"></asp:Button>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" ></asp:Label>
        </div>
        <div class="widecolumn">
            <div class="personal_box alignleft">
                <div id="language" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblLanguage" runat="server" Text="Language"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="parsonal_select">
                        <asp:DropDownList ID="ddlLanguage" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" meta:resourcekey="ddlLanguageResource1">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>
