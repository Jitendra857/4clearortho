<%@ Page Title="Doctor Registration" Language="C#" AutoEventWireup="true" CodeBehind="DoctorRegistration.aspx.cs" Inherits="_4eOrtho.DoctorRegistration" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html>

<html lang="en">
<head id="head1" runat="server">
    <meta charset="utf-8">
    <title>4ClearOrtho - Doctor Registration</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script type="text/javascript" src="Scripts/jquery-1.7.1.min.js"></script>
    <link href="Styles/Ajaxtoolkit.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
    <link href="Styles/flexslider.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="Styles/Jquery-UI/jquery-ui-1.8.23.custom.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/Colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="Scripts/Colorbox/colorbox.css" rel="stylesheet" />
    <script src="Scripts/loadingoverlay.min.js"></script>
    <style type="text/css">
        .personal_box {
            width: 425px;
        }

        .parsonal_textfild input[type="text"], .parsonal_textfild input[type="password"] {
            width: 175px;
        }

        .parsonal_select select {
            width: 200px;
        }

        .parsonal_select {
            width: 200px;
        }
    </style>
</head>
<body>
    <!-- Header -->
    <div class="header_holder headheight">
        <div class="header in">
            <div class="wrapper">
                <div class="logo">
                    <a href="Home.aspx">
                        <img src="Content/images/logo.png" alt="logo"></a>
                </div>

                <div class="inner_links">
                    <ul id="inner_links">
                        <li><a href="Home.aspx">Home</a></li>
                        <li><a href="PatientLogin.aspx">Patient</a></li>
                        <li><a href="DoctorLogin.aspx">Doctor</a></li>
                    </ul>
                </div>

                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!-- Header -->
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="5000" EnablePageMethods="true"></asp:ScriptManager>
        <div class="midportion minhgt" style="min-height: 380px;">
            <div class="wrapper">
                <div class="bothsection">
                    <div id="container" class="cf">
                        <asp:HiddenField ID="hdDoctorId" runat="server" />
                        <asp:HiddenField ID="ContentPlaceHolder1_hdMalePhotoName" runat="server" Value="male_sm.jpg" />
                        <asp:HiddenField ID="ContentPlaceHolder1_hdFemalePhotoName" runat="server" Value="female_sm.jpg" />

                        <a href="PhotoUpload.aspx" id="aPhotoUploadLink" class="iframe-Photo" style="display: none;"></a>
                        <asp:UpdatePanel ID="upAddEditDoctor" runat="server">
                            <ContentTemplate>
                                <div class="middlesection">
                                    <div class="doctorlogin">
                                        <h2>
                                            <asp:Label ID="lblHeader" runat="server" Text="Doctor Registration" meta:resourcekey="lblHeaderResource1"></asp:Label>
                                            <asp:Label ID="lblHeaderEdit" runat="server" Text="Edit Doctor Detail"
                                                Visible="False" meta:resourcekey="lblHeaderEditResource1"></asp:Label>
                                        </h2>
                                    </div>
                                    <div id="divMsg" runat="server" style="width: auto;">
                                        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                                        <asp:LinkButton ID="lbtnClickhere" Visible="false" runat="server" OnClick="lbtnClickhere_Click" CssClass="linkbuttonclass" Text="Click here" meta:resourcekey="lbtnClickhereResource1"></asp:LinkButton>
                                        <asp:Label ID="lblBecomecerty" Visible="false" runat="server" Text="to become certified." meta:resourcekey="lblBecomecertyResource1"></asp:Label>
                                    </div>
                                    <div class="widecolumn">
                                        <div class="personal_box alignright" style="clear: both">
                                            <asp:Image ImageUrl="Photograph/no-photo.jpg" ID="ContentPlaceHolder1_imgPatientPhoto" Width="175px"
                                                runat="server" Height="204px" meta:resourcekey="imgPatientPhotoResource1" />
                                            <div style="float: right;">
                                                <asp:LinkButton ID="lnkPhotoChange" Style="display: inline;" Text="Change Photo"
                                                    runat="server" CssClass="linkbuttonclass" OnClientClick="return OpenPhotoUpload();" meta:resourcekey="lnkPhotoChangeResource1" />&nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="lnkRemovePhoto" Width="102px" Style="display: inline;" Text="Remove Photo" runat="server" CssClass="linkbuttonclass" OnClientClick="return RemovePhoto();" meta:resourcekey="lnkRemovePhotoResource1" />
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
                                                    SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
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
                                                <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" TabIndex="2" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
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
                                                <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" TabIndex="3" meta:resourcekey="txtLastNameResource1"></asp:TextBox>
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
                                                <asp:TextBox ID="txtMi" runat="server" MaxLength="5" TabIndex="4" meta:resourcekey="txtMiResource1"></asp:TextBox>
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
                                                    <span class="asteriskclass">*</span><span class="alignright">:</span></label>
                                                <asp:TextBox ID="txtDOB" runat="server" CssClass="From-Date not-edit textfild search-datepicker"
                                                    TabIndex="7" meta:resourcekey="txtDOBResource1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rqvDOB" ForeColor="Red" runat="server" ControlToValidate="txtDOB"
                                                    SetFocusOnError="True" Display="None" ErrorMessage="Please select DOB." CssClass="errormsg"
                                                    ValidationGroup="validation" meta:resourcekey="rqvDOBResource1" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="customCalloutStyle"
                                                    TargetControlID="rqvtxtDoctorID" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
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
                                                <input type="password" id="txtDoctorPassword1" style="display: none;" />
                                                <asp:TextBox ID="txtDoctorPassword" runat="server" MaxLength="50" TabIndex="8" TextMode="Password" CssClass="password"
                                                    autocomplete="off" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftePassword" runat="server" Enabled="True"
                                                    TargetControlID="txtDoctorPassword" ValidChars="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+-=.,<>?;:'/|`~{}" />
                                                <asp:RequiredFieldValidator ID="rqvtxtPassword" ForeColor="Red" runat="server" ControlToValidate="txtDoctorPassword"
                                                    SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Password" CssClass="errormsg"
                                                    ValidationGroup="validation" meta:resourcekey="rqvtxtPasswordResource1" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="rqveCalltxtPassword" runat="server" CssClass="customCalloutStyle"
                                                    TargetControlID="rqvtxtPassword" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:RegularExpressionValidator ID="rqvPassword" runat="server" ControlToValidate="txtDoctorPassword"
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
                                                <input type="password" id="txtConfirmPassword1" style="display: none;" />
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="50" CssClass="password"
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
                                                    SetFocusOnError="True" ValidationGroup="validation" ControlToCompare="txtDoctorPassword"
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
                                                <asp:CheckBox ID="chkshowPassword" runat="server" CausesValidation="false" TabIndex="10" meta:resourcekey="chkshowPasswordResource1" />
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
                                                    <asp:Label runat="server" ID="lblState" Text="State" meta:resourcekey="lblStateResource1"></asp:Label>
                                                    <span class="asteriskclass">*</span><span class="alignright">:</span></label>
                                                <div class="parsonal_select">
                                                    <asp:DropDownList ID="ddlState" runat="server" TabIndex="12" meta:resourcekey="ddlStateResource1">
                                                        <asp:ListItem Text="Select State" Value="0" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvState" ForeColor="Red" runat="server" SetFocusOnError="True"
                                                        ControlToValidate="ddlState" Display="None" InitialValue="0" ErrorMessage="Please select state."
                                                        CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvddlStateResource1"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="customCalloutStyle"
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
                                        <%--<div class="personal_box alignright">
                                            <div class="parsonal_textfild">
                                                <label class="commondoc">
                                                    <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                                                    <span class="alignright">:</span></label>
                                                <div style="padding-top: 6px;">
                                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="35" meta:resourcekey="chkIsActiveResource1" />
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="clear">
                                        </div>
                                        <div class="bottom_btn tpadd alignright patientlogin_main" style="width: 310px;">
                                            <span class="supply-button3">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="validation" OnClick="btnSubmit_Click"
                                                    TabIndex="36" meta:resourcekey="btnSubmitResource1" />
                                            </span><span class="supply-button3">
                                                <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="37" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtEmailid" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upAddEditDoctor"
            DisplayAfter="10">
            <ProgressTemplate>
                <div class="processbar1">
                    <img src="../Content/images/loader.gif" alt="loading" style="top: 50%; left: 50%; position: absolute;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
    </form>
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-100101373-1', 'auto');
        ga('send', 'pageview');

    </script>
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('txtEmailid').focus();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(LoadImageByRole);
            LoadImageByRole();
        };
        function pageLoad() {
            var currentDate = new Date();
            currentDate.setFullYear(currentDate.getFullYear() - 18, 11, currentDate.getDate());
            $('.From-Date').datepicker({
                showOn: "button",
                buttonText: "Select Date",
                buttonImage: "Content/images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                yearRange: "1950:-18",
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
            $('[id$=chkshowPassword]').click(function () {
                $(".password").prop("type", ($(".password").prop("type") == "password") ? "text" : "password");
            });
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(loadingoverlayShow);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(loadingoverlayHide);
        }
        function CheckConfirmPassword(sender, args) {
            args.IsValid = !(document.getElementById('txtDoctorPassword').value != "" && args.Value == "");
        }
        function OpenPhotoUploadbyPatientId(doctorID) {
            $('#aPhotoUploadLink').attr("href", "../PhotoUpload.aspx?did=" + doctorID);
            $('#aPhotoUploadLink').click();
            return false;
        }
        function OpenPhotoUpload() {
            OpenPhotoUploadbyPatientId($("#hdDoctorId").val());
            return false;
        }
        function RemovePhoto() {
            if (confirm('Are you sure you want to remove this photo?')) {
                $('#ContentPlaceHolder1_hdMalePhotoName').val("male_sm.jpg");
                $('#ContentPlaceHolder1_hdFemalePhotoName').val("female_sm.jpg");
                LoadImageByRole();
                return true;
            }
            else
                return false;
        }
        function LoadImageByRole() {
            var male = $('#ContentPlaceHolder1_hdMalePhotoName').val();
            var female = $('#ContentPlaceHolder1_hdFemalePhotoName').val();
            var imgPatientPhoto = $('[id$=imgPatientPhoto]');

            if (male == "male_sm.jpg" && female == "female_sm.jpg")
                imgPatientPhoto.attr("src", ($('[id$=rbtnMale]').prop("checked")) ? "../Photograph/" + male : "../Photograph/" + female);
            else
                imgPatientPhoto.attr("src", (male != "male_sm.jpg") ? "../Photograph/" + male : "../Photograph/" + female);
        }
        function loadingoverlayShow() {
            $.LoadingOverlay("show", {
                image: "content/images/loader.gif"
            });
        }
        function loadingoverlayHide() {
            $.LoadingOverlay("hide", {
                image: "content/images/loader.gif"
            });
        }
    </script>
</body>
</html>
