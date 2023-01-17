<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="AddNewCase.aspx.cs" Inherits="_4eOrtho.AddNewCase" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-steps/jquery.steps.js"></script>
    <link rel="Stylesheet" type="text/css" href="Scripts/jquery-steps/jquery.steps.css" />
    <link href="Styles/common.css" rel="stylesheet" />
    <script src="Scripts/jquery.simpleFilePreview.js"></script>
    <link rel="stylesheet" href="Styles/simpleFilePreview.css" type="text/css" />
    <link href="Styles/lightbox.min.css" rel="stylesheet" />
    <style type="text/css">
        .wizard > .steps a, .wizard > .steps a:hover, .wizard > .steps a:active {
            background: none repeat scroll 0 0 #eee;
            color: #aaa;
            cursor: default;
            min-height: 35px;
        }
    </style>
    <style type="text/css">
        .UploadImgBox {
            width: 175px;
            height: 175px;
            float: left;
            overflow: hidden;
            margin: 5px;
        }

            .UploadImgBox img {
                width: 100%;
                height: 100%;
                float: left;
                overflow: hidden;
            }

        .imgbox {
            height: 173px;
            list-style-type: none;
            margin: 5px;
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main_right_cont minheigh" style="width: 100%;">
        <div class="title">
            <div class="supply-button2 back">
                <asp:Button ID="btnBack" runat="server" Text="Back" ToolTip="Back" CausesValidation="false" OnClientClick="javascript:window.location='ListNewCase.aspx';" PostBackUrl="~/ListNewCase.aspx" Width="100px" TabIndex="16" meta:resourcekey="btnBackResource1" />
            </div>
            <h2>
                <asp:Label ID="lblHeader" runat="server" Text="Add Patient / New Case" meta:resourcekey="lblHeaderResource1"></asp:Label>
            </h2>
        </div>
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </div>
        <div class="widecolumn" id="widget">
            <h3>
                <asp:Label ID="lblHeaderSelectPatient" runat="server" Text="Select Patient" meta:resourcekey="lblHeaderSelectPatientResource1"></asp:Label>
            </h3>
            <fieldset>
                <div class="parsonal_textfild alignleft" id="dvType" runat="server">
                    <label>
                        <asp:Label ID="lblExisting" runat="server" Text="Patient Type" meta:resourcekey="lblExistingResource1"></asp:Label>
                        <span class="alignright">:<span class="asteriskclass">*</span></span>
                    </label>
                    <div class="radio-selection">
                        <asp:RadioButton ID="rbtnNew" Text="New" runat="server" GroupName="Patient" Checked="True" TabIndex="0" meta:resourcekey="rbtnNewResource1" />
                        <asp:RadioButton ID="rbtnExisting" Text="Existing" runat="server" GroupName="Patient" TabIndex="1" meta:resourcekey="rbtnExistingResource1" />
                    </div>
                </div>
                <div class="parsonal_textfild alignleft" id="dvExistingPatientName" runat="server" style="display: none">
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
                </div>
                <div class="parsonal_textfild alignleft">
                    <label>
                        <asp:Label ID="lblEmail" runat="server" Text="EmailId" meta:resourcekey="lblEmailResource1"></asp:Label>
                        <span class="alignright">:<span class="asteriskclass">*</span></span>
                    </label>
                    <asp:TextBox ID="txtEmail" runat="server" TabIndex="3" meta:resourcekey="txtEmailResource1" onchange="txtEmailChange(this)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtEmail" ForeColor="Red" runat="server" ControlToValidate="txtEmail"
                        Display="None" ErrorMessage="Please enter Email Id." CssClass="errormsg"
                        SetFocusOnError="True" ValidationGroup="validation1" meta:resourcekey="rfvtxtEmailResource1" />
                    <asp:RegularExpressionValidator ID="rgvEmailAddressCheck" Display="None" runat="server"
                        SetFocusOnError="True" ValidationExpression="[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,5}"
                        ValidationGroup="validation1" CssClass="errormsg" ControlToValidate="txtEmail"
                        ErrorMessage="Please Enter Valid Email ID eg: 'abc@yahoo.com'" meta:resourcekey="rgvEmailAddressCheckResource1"></asp:RegularExpressionValidator>
                </div>
                <div class="parsonal_textfild alignleft">
                    <label>
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name" meta:resourcekey="lblFirstNameResource1"></asp:Label>
                        <span class="alignright">:<span class="asteriskclass">*</span></span>
                    </label>
                    <asp:TextBox ID="txtFirstName" runat="server" TabIndex="4" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" ForeColor="Red" runat="server" ControlToValidate="txtFirstName"
                        Display="None" ErrorMessage="Please enter first name" CssClass="errormsg"
                        SetFocusOnError="True" ValidationGroup="validation1" meta:resourcekey="rfvFirstNameResource1" />
                </div>
                <div class="parsonal_textfild alignleft">
                    <label>
                        <asp:Label ID="lblLastName" runat="server" Text="Last Name" meta:resourcekey="lblLastNameResource1"></asp:Label>
                        <span class="alignright">:<span class="asteriskclass">*</span></span>
                    </label>
                    <asp:TextBox ID="txtLastName" runat="server" TabIndex="5" meta:resourcekey="txtLastNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtLastName" ForeColor="Red" runat="server" ControlToValidate="txtLastName"
                        Display="None" ErrorMessage="Please enter last name" CssClass="errormsg"
                        SetFocusOnError="True" ValidationGroup="validation1" meta:resourcekey="rfvtxtLastNameResource1" />
                </div>
                <div class="parsonal_textfild alignleft">
                    <label>
                        <asp:Label ID="lblDateofBirth" runat="server" Text="Date Of Birth" meta:resourcekey="lblDateofBirthResource1"></asp:Label>
                        <span class="alignright">:<span class="asteriskclass">*</span></span>
                    </label>
                    <asp:TextBox ID="txtDateofBirth" runat="server" CssClass="From-Date not-edit textfild search-datepicker" TabIndex="6" Style="margin-right: 10px" meta:resourcekey="txtDateofBirthResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtDateofBirth" ForeColor="Red" runat="server" ControlToValidate="txtDateofBirth"
                        Display="None" ErrorMessage="Please select date of birth." CssClass="errormsg"
                        SetFocusOnError="True" ValidationGroup="validation1" meta:resourcekey="rfvtxtDateofBirthResource1" />
                </div>
                <div class="parsonal_textfild alignleft">
                    <label>
                        <asp:Label ID="lblGender" runat="server" Text="Gender" meta:resourcekey="lblGenderResource1"></asp:Label>
                        <span class="alignright">:<span class="asteriskclass">*</span></span>
                    </label>
                    <div class="radio-selection">
                        <asp:RadioButton ID="rbtnMale" Text="Male" runat="server" GroupName="Gender" Checked="True" TabIndex="7" meta:resourcekey="rbtnMaleResource1" />
                        <asp:RadioButton ID="rbtnFemale" Text="Female" runat="server" GroupName="Gender" TabIndex="5" meta:resourcekey="rbtnFemaleResource1" />
                    </div>
                </div>
            </fieldset>
            <h3>
                <asp:Label ID="lblCaseDetail" runat="server" Text="Case Detail" meta:resourcekey="lblCaseDetailResource1"></asp:Label>
            </h3>
            <fieldset>
                <asp:UpdatePanel ID="up3" runat="server">
                    <ContentTemplate>
                        <div class="wdt alignleft">
                            <div class="parsonal_textfild" id="divCaseType" runat="server">
                                <label>
                                    <asp:Label ID="lblCaseType" runat="server" Text="CaseType" meta:resourcekey="lblCareTypeResource1"></asp:Label>
                                    <span class="asteriskclass">*</span><span class="alignright">:</span>
                                </label>
                                <div class="parsonal_select">
                                    <asp:DropDownList ID="ddlCaseType" onchange="ddlCaseTypeChange(this)" ClientIDMode="Static" runat="server" TabIndex="1"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="parsonal_textfild alignleft">
                                <label>
                                    <asp:Label ID="lblOrthoCondition" runat="server" Text="Ortho Condition" meta:resourcekey="lblOrthoConditionResource1"></asp:Label>
                                    <span class="alignright">:</span>
                                </label>
                                <div class="radio-selection">
                                    <asp:RadioButton ID="rbtnAnterior" Text="Anterior" Checked="true" runat="server" GroupName="Condition" TabIndex="9" meta:resourcekey="rbtnAnteriorResource1" />
                                    <asp:RadioButton ID="rbtnPosterior" Text="Posterior" runat="server" GroupName="Condition" TabIndex="10" meta:resourcekey="rbtnPosteriorResource1" />
                                </div>
                            </div>
                            <div class="parsonal_textfild alignleft">
                                <label>
                                </label>
                                <div class="radio-selection">
                                    <asp:CheckBoxList ID="chkOrthoCondition" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" TabIndex="9" meta:resourcekey="chkOrthoConditionResource1">
                                        <asp:ListItem Text="Crowding" Value="CROWDING" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                        <asp:ListItem Text="Spacing" Value="SPACING" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                        <asp:ListItem Text="Cross Bite" Value="CROSSBITE" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                        <asp:ListItem Text="Open Bite" Value="OPENBITE" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                        <asp:ListItem Text="Deep Bite" Value="DEEPBITE" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                        <asp:ListItem Text="Narrow Arch" Value="NARROWARCH" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                    </asp:CheckBoxList><br />
                                </div>
                            </div>
                            <div class="parsonal_textfild alignleft">
                                <label>
                                </label>
                                <div class="radio-selection">
                                    <asp:CheckBox ID="chkOtherCondition" runat="server" Text="other" Style="margin-left: 3px;" TabIndex="11" meta:resourcekey="chkOtherConditionResource1" />
                                    &nbsp; &nbsp;                                    
                                </div>
                                <div id="dvother" style="display: none; float: left;">
                                    <asp:TextBox ID="txtOtherCondition" runat="server" Style="width: 230px" TabIndex="12" MaxLength="250" meta:resourcekey="txtOtherConditionResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="parsonal_textfild alignleft">
                                <label>
                                    <asp:Label ID="lblNotes" runat="server" Text="Case Detail" meta:resourcekey="lblNotesResource1"></asp:Label>
                                    <span class="alignright">:</span>
                                </label>
                                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Style="height: 100px" TabIndex="13" meta:resourcekey="txtNotesResource1"></asp:TextBox>
                            </div>
                            <div class="parsonal_textfild alignleft">
                                <label>
                                    <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                                    <span class="alignright">:</span>
                                </label>
                                <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="14" meta:resourcekey="chkIsActiveResource1" />
                            </div>
                            <div class="parsonal_textfild alignleft">
                                <label>
                                </label>
                            </div>
                            <div class="date2" style="display: none;">
                                <div class="date_cont">
                                    <div class="date_cont_right">
                                        <div class="supply-button3">
                                            <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="15" ToolTip="Cancel" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <h3>
                <asp:Label ID="lblBeforeTemplate" runat="server" Text="Before Template" meta:resourcekey="lblBeforeTemplateResource1"></asp:Label>
            </h3>
            <fieldset>
                <asp:Panel ID="pnlImage" runat="server">
                    <div class="wdt alignleft">
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile1" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" Style="height: 100% !important; width: 100% !important;" />
                            <asp:RequiredFieldValidator ID="rfvFile1" runat="server" ControlToValidate="fuFile1" Display="Dynamic" ValidationGroup="validation2"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile2" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvFile2" runat="server" ControlToValidate="fuFile2" Display="Dynamic" ValidationGroup="validation2"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile3" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvFile3" runat="server" ControlToValidate="fuFile3" Display="Dynamic" ValidationGroup="validation2"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile4" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvFile4" runat="server" ControlToValidate="fuFile4" Display="Dynamic" ValidationGroup="validation2"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <div class="imgbox" style="text-align: center; background-color: #164D8E;">
                                <div style="padding-top: 10px;">
                                    <asp:Label ID="lblBeforeAfter" runat="server" Style="font-size: 15px; color: white;" Text="Before Treatment" meta:resourcekey="lblBeforeTemplateResource1"></asp:Label>
                                    <div class="clear"></div>
                                    <img src="Content/images/logo.png" style="width: auto !important; height: 60px; margin: 10px 0 6px 35px;" />
                                    <div class="clear"></div>
                                    <asp:Label ID="lblDoctorName" runat="server" Style="font-size: 15px; color: white;"></asp:Label>
                                    <div class="clear"></div>
                                    <asp:Label ID="lblPatientName" runat="server" Style="font-size: 13px; color: white;"></asp:Label>
                                    <div class="clear"></div>
                                    <asp:Label ID="lblCreated" runat="server" Style="font-size: 13px; color: white;" Text="Created Dt.:" meta:resourcekey="lblCreatedResource1"></asp:Label>
                                    <asp:Label ID="lblDate" runat="server" Style="font-size: 13px; color: white;"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile5" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvFile5" runat="server" ControlToValidate="fuFile5" Display="Dynamic" ValidationGroup="validation2"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile6" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvFile6" runat="server" ControlToValidate="fuFile6" Display="Dynamic" ValidationGroup="validation2"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile7" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvFile7" runat="server" ControlToValidate="fuFile7" Display="Dynamic" ValidationGroup="validation2"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile8" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvFile8" runat="server" ControlToValidate="fuFile8" Display="Dynamic" ValidationGroup="validation2"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </asp:Panel>
            </fieldset>
            <h3>
                <asp:Label ID="lblHeaderSelectPackage" runat="server" Text="Select Package" meta:resourcekey="lblHeaderSelectPackageResource1"></asp:Label>
            </h3>
            <fieldset>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="wdt alignleft">
                            <asp:PlaceHolder ID="phSelectPackage" runat="server">
                                <div class="parsonal_textfild alignleft">
                                    <label>
                                        <asp:Label ID="lblSelectPackage" runat="server" Text="Select Package" meta:resourcekey="lblHeaderSelectPackageResource1"></asp:Label>
                                        <span class="alignright">:<span class="asteriskclass">*</span></span>
                                    </label>
                                    <div class="parsonal_select">
                                        <asp:DropDownList ID="ddlPackage" runat="server" TabIndex="16" onchange="ddlPackageChange(this)" meta:resourcekey="ddlPackageResource1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rqvPackage" ForeColor="Red" runat="server" ControlToValidate="ddlPackage"
                                            SetFocusOnError="True" Display="None"
                                            ErrorMessage="Please select Package Name" CssClass="errormsg"
                                            ValidationGroup="validation4" InitialValue="0" meta:resourcekey="rqvPackageResource1" />
                                    </div>
                                </div>
                                <div class="parsonal_textfild alignleft">
                                    <label>
                                        <asp:Label ID="lblPackageAmount" runat="server" Text="Package Amount($)" meta:resourcekey="lblPackageAmtResource1"></asp:Label>
                                        <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                                    </label>
                                    <asp:TextBox ID="txtPackageAmount" Text="0" ClientIDMode="Static" CssClass="amount" Enabled="false" ReadOnly="true" runat="server" MaxLength="15" Style="text-align: right; min-width: 145px;" TabIndex="17" meta:resourcekey="txtAmountResource1"></asp:TextBox>
                                </div>
                                <div class="parsonal_textfild alignleft">
                                    <label>
                                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity" meta:resourcekey="lblProductAmountResource1"></asp:Label>
                                        <span class="alignright">:<span class="asteriskclass">*</span></span>
                                    </label>
                                    <asp:TextBox ID="txtQuantity" ClientIDMode="Static" runat="server" Text="1" onblur="CalculateTotalAmount()" MaxLength="15" Style="text-align: right; min-width: 145px;" TabIndex="3" meta:resourcekey="txtQuantityResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rqvQuantity" ForeColor="Red" runat="server" SetFocusOnError="True"
                                        ControlToValidate="txtQuantity" Display="None" ErrorMessage="Please enter Qunatity."
                                        CssClass="errormsg" ValidationGroup="validation4" meta:resourcekey="rqvQuantityResource1" />
                                </div>
                                <div class="parsonal_textfild alignleft">
                                    <label>
                                        <asp:Label ID="lblAmount" runat="server" Text="Amount ($)" meta:resourcekey="lblAmountResource1"></asp:Label>
                                        <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                                    </label>
                                    <asp:TextBox ID="txtAmount" ClientIDMode="Static" CssClass="amount" Enabled="false" ReadOnly="true" runat="server" MaxLength="15" Style="text-align: right; min-width: 145px;" TabIndex="17" meta:resourcekey="txtAmountResource1"></asp:TextBox>
                                </div>
                                <div class="parsonal_textfild alignleft" id="dvRecieved" runat="server" visible="false">
                                    <label>
                                        <asp:Label ID="lblRecieved" runat="server" Text="Recieved?" meta:resourcekey="lblReceivedResource1"></asp:Label>
                                        <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                                    </label>
                                    <div class="radio-selection">
                                        <asp:CheckBox ID="chkIsRecieved" runat="server" TabIndex="4" />
                                    </div>
                                </div>
                                <div class="parsonal_textfild alignleft" id="dvDispatchRemarks" runat="server" visible="false">
                                    <label>
                                        <asp:Label ID="lblRemarks" runat="server" Text="Remarks" meta:resourcekey="lblRemarksResource1"></asp:Label>
                                    </label>
                                    <asp:Label ID="lblDispatchRemarks" runat="server" Text="Remarks" meta:resourcekey="lblRemarksResource1"></asp:Label>
                                </div>
                                <div class="date2">
                                    <div id="dvPackageImagelist" runat="server">
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <fieldset id="flPackageDetails" runat="server" style="width: 535px;">
                                        <legend><%= this.GetLocalResourceObject("PackageDetails") %></legend>
                                        <table id="tblPackageDetail" runat="server" class="grid_table" style="display: none;" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <span><%= this.GetLocalResourceObject("ProductName") %></span>
                                                        </span>
                                                    </div>

                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <span><%= this.GetLocalResourceObject("Quantity") %></span>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <span><%= this.GetLocalResourceObject("Amount") %></span>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <span><%= this.GetLocalResourceObject("Total") %></span>
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:Label ID="lblErrorMsg" runat="server" CssClass="errMsg" Text="No Data Found" meta:resourcekey="lblErrorMsgResource1">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Repeater ID="rptPackageImage" runat="server" Visible="false">
                                            <HeaderTemplate>
                                                <table id="tblrptPackageImage" class="grid_table" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <div class="topadd_f flex">
                                                                <span class="one">
                                                                    <span><%= this.GetLocalResourceObject("ProductName") %></span>
                                                                </span>
                                                            </div>

                                                        </td>
                                                        <td>
                                                            <div class="topadd_f flex">
                                                                <span class="one">
                                                                    <span><%= this.GetLocalResourceObject("Quantity") %></span>
                                                                </span>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="topadd_f flex">
                                                                <span class="one">
                                                                    <span><%= this.GetLocalResourceObject("Amount") %></span>
                                                                </span>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="topadd_f flex">
                                                                <span class="one">
                                                                    <span><%= this.GetLocalResourceObject("Total") %></span>
                                                                </span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <div class="grenchk dark" id="flex">
                                                            <div class="whitetext">
                                                                <%# Eval("ProductName")%>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="grenchk dark" id="flex">
                                                            <div class="whitetext">
                                                                <%# Eval("Quantity")%>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="grenchk dark" id="flex">
                                                            <div class="whitetext">
                                                                <%# Eval("Amount")%>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="grenchk dark" id="flex">
                                                            <div class="whitetext">
                                                                <%# Convert.ToDecimal(Eval("Amount")) * Convert.ToInt32(Eval("Quantity")) %>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblErrorMsg" runat="server" CssClass="errMsg" Text="No Data Found" Visible="false" meta:resourcekey="lblErrorMsgResource1">
                                                </asp:Label>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </fieldset>
                                </div>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="phOrderSupply" runat="server" Visible="false">
                                <div class="parsonal_textfild alignleft">
                                    <div class="parsonal_textfild alignleft">
                                        <a href="AddEditSupplyOrder.aspx">Click here</a>&nbsp;to order package or product.
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlPackage" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </fieldset>
            <h3>
                <asp:Label ID="lblHeadPayment" runat="server" Text="Make Payment" meta:resourcekey="lblHeadPaymentResource1"></asp:Label>
            </h3>
            <fieldset>
                <asp:PlaceHolder ID="phCreditCard" runat="server">
                    <div class="parsonal_textfild alignleft" id="divCaseTypeDiscount" runat="server">
                        <label>
                            <asp:Label ID="lblDiscountCouponCode" runat="server" Text="  Discount Coupon Code" meta:resourcekey="lblDiscountCouponCodeResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                        </label>
                        <asp:TextBox ID="txtDiscountCouponCode" ClientIDMode="Static" CssClass="amount" runat="server"></asp:TextBox>
                        <input type="button" id="btnApply" onclick="javascript: return DiscountApply();" value="Apply" />
                        <input type="button" id="btnCancel" onclick="javascript: return DiscountCancel();" value="Cancel" />
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <asp:Label ID="lblDiscountMsg" runat="server"></asp:Label>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblCaseCharge" runat="server" Text="  Case Charge ($)" meta:resourcekey="lblCaseChargeResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                        </label>
                        <asp:TextBox ID="txtCaseCharge" Text="0" ClientIDMode="Static" CssClass="amount" Enabled="false" ReadOnly="true" runat="server"></asp:TextBox>
                    </div>
                    <%--Case Type Discount Start--%>
                    <div id="divCaseTypeDiscountCal" runat="server" style="display: none;">
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblCaseTypeDiscount" runat="server" Text="- Discount ($)" meta:resourcekey="lblDiscountResource1"></asp:Label>
                                <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                            </label>
                            <asp:TextBox ID="txtCaseTypeDiscount" Text="0" ClientIDMode="Static" CssClass="amount" Enabled="false" ReadOnly="true" runat="server" MaxLength="15"></asp:TextBox>
                        </div>
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="Label2" runat="server" Text="  Discounted Case Charge ($)"></asp:Label>
                                <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                            </label>
                            <asp:TextBox ID="txtTotalCasePackage1" Text="0" ClientIDMode="Static" CssClass="amount total " Enabled="false" ReadOnly="true" runat="server" MaxLength="15"></asp:TextBox>
                        </div>
                    </div>
                    <%--Case Type Discount End--%>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            +&nbsp;<asp:Label ID="lblPackageAmt" runat="server" Text="Package Amount ($)" meta:resourcekey="lblPackageAmtResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                        </label>
                        <asp:TextBox ID="txtPackageAmt" Text="0" ClientIDMode="Static" CssClass="amount" Enabled="false" ReadOnly="true" runat="server"></asp:TextBox>
                    </div>
                    <div class="parsonal_textfild alignleft" id="divExpressShipment" runat="server">
                        <label>
                            +&nbsp;<asp:Label ID="lblExpressShipment" runat="server" Text="Express Shipment ($)" meta:resourcekey="lblExpressShipmentResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                        </label>
                        <asp:TextBox ID="txtExpressShipment" Text="0" ClientIDMode="Static" CssClass="amount" Enabled="false" ReadOnly="true" runat="server"></asp:TextBox>
                        <div class="radio-selection" style="float: right;">
                            <asp:CheckBox ID="chkIsRegularShipment" runat="server" Text="Regular Shipment" onchange="RegularShipment(this)" meta:resourcekey="chkIsRegularShipmentResource1" />
                        </div>
                    </div>
                    <div id="divDiscount" runat="server" visible="false">
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblTotal" runat="server" Text="  Total (Case + Package) ($)" meta:resourcekey="lblTotalResource1"></asp:Label>
                                <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                            </label>
                            <asp:TextBox ID="txtTotalCasePackage" Text="0" ClientIDMode="Static" CssClass="amount total " Enabled="false" ReadOnly="true" runat="server" MaxLength="15"></asp:TextBox>
                        </div>
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblDiscount" runat="server" Text="- Promotional Discount ($)"></asp:Label>
                                <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                            </label>
                            <asp:TextBox ID="txtPromoDiscount" Text="0" ClientIDMode="Static" CssClass="amount" Enabled="false" ReadOnly="true" runat="server" MaxLength="15"></asp:TextBox>
                        </div>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblTotalPayable" runat="server" Text="  Payable Amount" meta:resourcekey="lblPayableAmountResource1"></asp:Label>($)
                            <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                        </label>
                        <asp:TextBox ID="txtPayableAmt" Text="0" ClientIDMode="Static" CssClass="amount total" Enabled="false" ReadOnly="true" runat="server"></asp:TextBox>
                    </div>
                    <div runat="server" id="divpaymenttobedone">
                        <div class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblpaymenttobedone" runat="server" Text="Payment To Be Done In :"></asp:Label>
                            </label>
                            <div class="radio-selection">
                                <asp:RadioButton ID="rbtcashpayment" Text="Cash" Checked="true" CssClass="paymentmode" runat="server" GroupName="PaymentSelectMode" TabIndex="11" />
                                <asp:RadioButton ID="rbtonlinepayment" Text="Select Payment Mode" CssClass="paymentmode" runat="server" GroupName="PaymentSelectMode" TabIndex="12" />
                            </div>
                        </div>
                        <div id="CashPayment" class="parsonal_textfild alignleft">
                            <label>
                                <asp:Label ID="lblpayablecashamount" runat="server" Text="Payable Cash Amount : "></asp:Label>
                            </label>
                            <asp:TextBox ID="txtCashAmount" Text="0" ClientIDMode="Static" CssClass="amount total" Enabled="false" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="OnlinePayment" style="display: none;">
                        <div id="divSelectPayment" runat="server">
                            <div class="clear" style="height: 5px;">
                            </div>
                            <div class="parsonal_textfild alignleft" style="text-align: center; width: 100%;">
                                <h2>
                                    <strong>
                                        <asp:Label ID="ltrSelectMode" runat="server" Text="Select Payment Mode" meta:resourcekey="ltrSelectModeResource1"></asp:Label></strong></h2>
                            </div>
                            <div class="clear" style="height: 5px;">
                            </div>
                            <div style="width: 100%;">
                                <div class="parsonal_textfild alignleft" style="padding-left: 110px;">
                                    <div class="payment" style="width: 100%;">
                                        <input type="button" value="<%= this.GetLocalResourceObject("PayusingCreditCard") %>" onclick="SetPaymentMode(true)" style="height: 45px;" />
                                    </div>
                                </div>
                                <div class="parsonal_textfild alignleft" style="padding-left: 50px; padding-top: 7px;">
                                    <div class="date_cont_login">
                                        <asp:ImageButton ID="imgbtnExpressCheckout" runat="server" OnClick="imgbtnExpressCheckout_Click" ImageUrl="Content/images/btn_xpressCheckout.gif" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="phCreditCardMode" style="display: none;">
                            <div class="parsonal_textfild alignleft" runat="server" id="divCardNo">
                                <label>
                                    <asp:Label ID="lblCardNumber" runat="server" Text="Card Number" meta:resourcekey="lblCardNumberResource1"></asp:Label>
                                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                                </label>
                                <asp:TextBox ID="txtCardNo" ClientIDMode="Static" runat="server" MaxLength="19" TabIndex="1" autocomplete="off" onchange="javascript: return CardNoValidate();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqvCardNo" ForeColor="Red" runat="server" ControlToValidate="txtCardNo"
                                    Display="None" ErrorMessage="Please Enter Card Number" SetFocusOnError="True"
                                    CssClass="errormsg" ValidationGroup="validation5" meta:resourcekey="rqvCardNoResource1" />
                            </div>
                            <div class="parsonal_textfild alignleft" runat="server" id="divnameoncard">
                                <label>
                                    <asp:Label ID="lblNameon" runat="server" Text="Name on Card" meta:resourcekey="lblNameonResource1"></asp:Label>
                                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                                </label>
                                <asp:TextBox ID="txtNameOnCard" ClientIDMode="Static" runat="server"
                                    MaxLength="50" TabIndex="2" autocomplete="off" meta:resourcekey="txtNameOnCardResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqvNameOnCard" ForeColor="Red" runat="server" ControlToValidate="txtNameOnCard"
                                    Display="None" ErrorMessage="Please Enter Name on Credit Card" SetFocusOnError="True"
                                    CssClass="errormsg" ValidationGroup="validation5" meta:resourcekey="rqvNameOnCardResource1" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftNameOnCard" runat="server" Enabled="True"
                                    TargetControlID="txtNameOnCard" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                    ValidChars=" " />
                            </div>
                            <div class="parsonal_textfild alignleft" runat="server" id="divCardtype">
                                <label>
                                    <asp:Label ID="lblCareType" runat="server" Text="Card Type" meta:resourcekey="lblCareTypeResource1"></asp:Label>
                                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                                </label>
                                <div class="parsonal_selectSmall">
                                    <asp:DropDownList ID="ddlCardType" runat="server" TabIndex="3" onchange="javascript: return CardNoValidate();" CssClass="low_high2" Style="font-size: 14px; margin-top: 0px; width: auto !important;">
                                        <asp:ListItem Value="VISA" Text="Visa" Selected="True" />
                                        <asp:ListItem Value="MASTERCARD" Text="MasterCard" />
                                        <asp:ListItem Value="DISCOVER" Text="Discover" />
                                        <asp:ListItem Value="AMEX" Text="Amex" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="parsonal_textfild alignleft" runat="server" id="divExpires">
                                <label>
                                    <asp:Label ID="lblExpires" runat="server" Text="Expires" meta:resourcekey="lblExpiresResource1"></asp:Label>
                                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                                </label>
                                <div class="parsonal_selectSmall">
                                    <asp:DropDownList ID="ddlYear" runat="server" onchange="javascript: return ServerMonthValidate();" TabIndex="4" meta:resourcekey="ddlYearResource1" CssClass="low_high2" Style="font-size: 14px; margin-top: 0px; width: auto !important;">
                                    </asp:DropDownList>
                                </div>
                                <div class="parsonal_selectSmall">
                                    <asp:DropDownList ID="ddlMonth" runat="server" onchange="javascript: return ServerMonthValidate();" TabIndex="5" CssClass="low_high2" Style="font-size: 14px; margin-top: 0px; width: auto !important;">
                                        <asp:ListItem Value="1" Text="January" />
                                        <asp:ListItem Value="2" Text="February" />
                                        <asp:ListItem Value="3" Text="March" />
                                        <asp:ListItem Value="4" Text="April" />
                                        <asp:ListItem Value="5" Text="May" />
                                        <asp:ListItem Value="6" Text="June" />
                                        <asp:ListItem Value="7" Text="July" />
                                        <asp:ListItem Value="8" Text="August" />
                                        <asp:ListItem Value="9" Text="September" />
                                        <asp:ListItem Value="10" Text="October" />
                                        <asp:ListItem Value="11" Text="November" />
                                        <asp:ListItem Value="12" Text="December" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="parsonal_textfild alignleft" runat="server" id="divcvvcode">
                                <label>
                                    <asp:Label ID="lblCVVCode" runat="server" Text="CVV2/CVC2 Code" meta:resourcekey="lblCVVCodeResource1"></asp:Label>
                                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                                </label>
                                <asp:TextBox ID="txtCCVNo" ClientIDMode="Static" CssClass="small" runat="server" TabIndex="6" MaxLength="3" onchange="javascript: return checkccvno();"
                                    TextMode="Password" autocomplete="off" meta:resourcekey="txtCCVNoResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqvCCVNo" ForeColor="Red" runat="server" ControlToValidate="txtCCVNo"
                                    Display="None" ErrorMessage="Please Enter CCV2/CVC2 Code" SetFocusOnError="True"
                                    CssClass="errormsg" ValidationGroup="validation5" meta:resourcekey="rqvCCVNoResource1" />
                            </div>
                            <div class="parsonal_textfild alignleft" style="display: none;">
                                <label>
                                    <asp:Label ID="lblAutomatic" runat="server" Text="Automatic Renewal" meta:resourcekey="lblAutomaticResource1"></asp:Label>
                                </label>
                                <asp:CheckBox ID="chkAutoRenewal" runat="server" TabIndex="7" meta:resourcekey="chkAutoRenewalResource1" />
                                <label id="lblAutoRenewal" runat="server" style="float: right; width: 300px; margin-left: 5px; margin-right: 0px;">
                                </label>
                            </div>
                            <div class="clear" style="height: 30px;"></div>
                            <div class="parsonal_textfild alignleft">
                                <a href="#" onclick="SetPaymentMode(false)" style="text-decoration: none;">
                                    <img src="Content/images/back.png" />
                                    <asp:Label runat="server" ID="lblBacktopayment" Text="Back to payment mode selection." meta:resourcekey="lblBacktopaymentResource1"></asp:Label>
                                </a>
                            </div>
                            <div class="clear" style="height: 30px;"></div>
                        </div>
                    </div>
                </asp:PlaceHolder>
                <div class="parsonal_textfild alignleft" id="divLocalContact" runat="server" style="border: 1px solid gray; margin-top: 10px; padding: 10px; width: 90%;">
                    <label style="width: 100%;">
                        <strong>
                            <asp:Label ID="lblLocalContact" Text="Local Contact :" runat="server"></asp:Label></strong><br />
                        <asp:Label ID="lblName" runat="server"></asp:Label><br />
                        <asp:Label ID="lblAddress" runat="server"></asp:Label><br />
                        <asp:Label ID="lblContact" runat="server"></asp:Label><br />
                        <asp:Label ID="lblLocalEmail" runat="server"></asp:Label>
                    </label>
                </div>
            </fieldset>
            <div style="display: none;">
                <h3>
                    <asp:Label ID="lblAfterTemplate" runat="server" Text="After Template" meta:resourcekey="lblAfterTemplateResource1"></asp:Label>
                </h3>
                <fieldset>
                    <asp:Panel ID="pnlImage1" runat="server">
                        <div class="personal_box wdt alignleft">
                            <div class="UploadImgBox">
                                <asp:FileUpload ID="fuFile9" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvFile9" runat="server" ControlToValidate="fuFile9" Display="Dynamic" ValidationGroup="validation3"></asp:RequiredFieldValidator>
                            </div>
                            <div class="UploadImgBox">
                                <asp:FileUpload ID="fuFile10" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvFile10" runat="server" ControlToValidate="fuFile10" Display="Dynamic" ValidationGroup="validation3"></asp:RequiredFieldValidator>
                            </div>
                            <div class="UploadImgBox">
                                <asp:FileUpload ID="fuFile11" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvFile11" runat="server" ControlToValidate="fuFile11" Display="Dynamic" ValidationGroup="validation3"></asp:RequiredFieldValidator>
                            </div>
                            <div class="UploadImgBox">
                                <asp:FileUpload ID="fuFile12" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvFile12" runat="server" ControlToValidate="fuFile12" Display="Dynamic" ValidationGroup="validation3"></asp:RequiredFieldValidator>
                            </div>
                            <div class="UploadImgBox">
                                <div class="imgbox" style="text-align: center; background-color: #164D8E;">
                                    <div style="padding-top: 10px;">
                                        <asp:Label ID="Label5" runat="server" Style="font-size: 15px; color: white;">After Treatment</asp:Label>
                                        <div class="clear"></div>
                                        <img src="Content/images/logo.png" style="padding-top: 5px; width: auto !important; height: 60px; margin: 15px 15px 15px 35px;" />
                                        <div class="clear"></div>
                                        <asp:Label ID="lblDoctorName1" ClientIDMode="Static" runat="server" Style="font-size: 15px; color: white;"></asp:Label>
                                        <div class="clear"></div>
                                        <asp:Label ID="lblPatientName1" ClientIDMode="Static" runat="server" Style="font-size: 13px; color: white;"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="UploadImgBox">
                                <asp:FileUpload ID="fuFile13" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvFile13" runat="server" ControlToValidate="fuFile13" Display="Dynamic" ValidationGroup="validation3"></asp:RequiredFieldValidator>
                            </div>
                            <div class="UploadImgBox">
                                <asp:FileUpload ID="fuFile14" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvFile14" runat="server" ControlToValidate="fuFile14" Display="Dynamic" ValidationGroup="validation3"></asp:RequiredFieldValidator>
                            </div>
                            <div class="UploadImgBox">
                                <asp:FileUpload ID="fuFile15" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvFile15" runat="server" ControlToValidate="fuFile15" Display="Dynamic" ValidationGroup="validation3"></asp:RequiredFieldValidator>
                            </div>
                            <div class="UploadImgBox">
                                <asp:FileUpload ID="fuFile16" CssClass="FileUpload" onchange="return validateImage(this)" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvFile16" runat="server" ControlToValidate="fuFile16" Display="Dynamic" ValidationGroup="validation3"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </asp:Panel>
                </fieldset>
                <div id="dvPrint" style="display: none;">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" OnClientClick="loadingoverlayShow()"
                        OnClick="btnSubmit_Click" TabIndex="14" meta:resourcekey="btnSubmitResource1" />
                    <asp:Panel ID="pnlPrint" runat="server">
                        <div>
                            <h2>
                                <asp:Label ID="printHeader" Text="Patient Case Details" runat="server"></asp:Label></h2>
                        </div>
                        <table border="1">
                            <tr>
                                <td colspan="2">
                                    <div style="font-size: 12px; float: right;">
                                        <b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text="Doctor Name:"></asp:Label></b>
                                        <asp:Label ID="lblPrintCreated" runat="server"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblCreatedDate" runat="server" Text="Created Date:"></asp:Label>
                                        <asp:Label ID="lblPrintcreatedDate" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right">
                                            <b>
                                                <asp:Label ID="Label1" runat="server" Text="Case No"></asp:Label>
                                            </b>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right">
                                            <asp:Label ID="lblPrintCaseNo" runat="server" Style="margin-left: 4px"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right">
                                            <b>
                                                <asp:Label ID="lblPrintPN1" runat="server" Text="Patient Name"></asp:Label>
                                            </b>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right">
                                            <asp:Label ID="lblPrintPN" runat="server" Style="margin-left: 4px"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right ">
                                            <b>
                                                <asp:Label ID="lblPrintDN1" runat="server" Text="Doctor Name"></asp:Label></b>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right ">
                                            <asp:Label ID="lblPrintDN" runat="server" Style="margin-left: 4px"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right ">
                                            <b>
                                                <asp:Label ID="lblPrintDOB1" runat="server" Text="Date Of Birth"></asp:Label></b>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right ">
                                            <asp:Label ID="lblPrintDOB" runat="server" Style="margin-left: 4px"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right ">
                                            <b>
                                                <asp:Label ID="lblPrintGender1" runat="server" Text="Gender"></asp:Label></b>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right ">
                                            <asp:Label ID="lblPrintGender" runat="server" Style="margin-left: 4px"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right ">
                                            <b>
                                                <asp:Label ID="lblPrintOS1" runat="server" Text="Ortho System"></asp:Label></b>
                                        </div>
                                    </div>
                                </td>
                                <td>

                                    <div class="date_cont">
                                        <div class="date_cont_right ">
                                            <asp:Literal ID="ltrPrintOS" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right ">
                                            <b>
                                                <asp:Label ID="lblPrintOC" runat="server" Text="Ortho Condition"></asp:Label></b>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="date_cont" style="width: 200px;">
                                        <div class="date_cont_right " style="width: 365px;">
                                            <asp:Literal ID="ltrPrintOC" runat="server"></asp:Literal>
                                            <asp:Literal ID="ltrPrintOther" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="date_cont">
                                        <div class="date_cont_right ">
                                            <b>
                                                <asp:Label ID="lblPrintNotes" runat="server" Text="Notes/Instructions"></asp:Label></b>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="date_cont" style="width: 200px;">
                                        <div class="date_cont_right " style="width: 365px;">
                                            <asp:Literal ID="ltrPrintNotes" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnImages" runat="server" />
    <asp:HiddenField ID="select_tab" runat="server" />
    <asp:HiddenField ID="hdnPassword" runat="server" />
    <asp:HiddenField ID="hdnPatientId" runat="server" />
    <asp:HiddenField ID="hdnBeforeId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnAfterId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnSupplyId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnSkip" runat="server" Value="false" />
    <asp:HiddenField ID="hdnTotalAmount" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnCaseNo" runat="server" />
    <asp:HiddenField ID="hdnReworkRetainer" runat="server" />
    <asp:HiddenField ID="hdnPackageAmt" runat="server" />
    <asp:HiddenField ID="hdnCaseCharge" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnExpressShipment" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnCaseType" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnCaseTypeDiscount" ClientIDMode="Static" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCountryId" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="hdnLocalContact" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnCaseTypeID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnIsSkipPayment" runat="server" ClientIDMode="Static" Value="0" />
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CreateWizard);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(resizeJquerySteps);
            $.getScript("Scripts/lightbox-plus-jquery.min.js");
            $.getScript("Scripts/jquery-ui.js");
            $("#btnApply").val("<%= this.GetLocalResourceObject("btnApplyDiscount")  %>");
            $("#btnCancel").val("<%= this.GetLocalResourceObject("btnCancelDiscount")  %>");
        });

        function pageLoad() {
            CreateWizard();
            resizeJquerySteps();

            $('#' + '<%= fuFile1.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("FullFace") %>" });
            $('#' + '<%= fuFile2.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Smile") %>" });
            $('#' + '<%= fuFile3.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Profile") %>" });
            $('#' + '<%= fuFile4.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("RightLateral") %>" });
            $('#' + '<%= fuFile5.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("LeftLateral") %>" });
            $('#' + '<%= fuFile6.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Maxillary") %>" });
            $('#' + '<%= fuFile7.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Anterior") %>" });
            $('#' + '<%= fuFile8.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Mandibulary") %>" });

            $('.From-Date').datepicker({
                showOn: "button",
                buttonText: "<%=this.GetLocalResourceObject("SelectDate").ToString()%>",
                buttonImage: "Content/images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                yearRange: "-100:+20",
                maxDate: new Date()
            });
            $('.not-edit').attr("readonly", "readonly");
            if ($('#<%=chkOtherCondition.ClientID%>').attr('checked'))
                $('#dvother').css("display", "inline");
            else
                $('#dvother').css("display", "none");

            $('#<%=chkOtherCondition.ClientID%>').click(function () {
                if (this.checked)
                    $('#dvother').css("display", "inline");
                else
                    $('#dvother').css("display", "none");
            });

            $('.paymentmode').change(function () {
                if ($("[id$='rbtcashpayment']").is(":checked")) {
                    $("[id$='CashPayment']").show();
                    $("[id$='OnlinePayment']").hide();
                }
                else {
                    $("[id$='CashPayment']").hide();
                    $("[id$='OnlinePayment']").show();
                }
            });

            <%--$(".FileUpload").change(function () {
                if (typeof (FileReader) != "undefined") {
                    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                    $($(this)[0].files).each(function () {
                        var file = $(this);
                        if (file.length > 0) {
                            var size = parseFloat(file[0].size / 1024).toFixed(2);
                            if (regex.test(file[0].name.toLowerCase())) {
                                if (size > 2024) {
                                    alert("<%= this.GetLocalResourceObject("FileSize") %>");
                                    return false;
                                }
                            } else {
                                alert(file[0].name + "<%= this.GetLocalResourceObject("Isnotavalidimage") %>");
                                return false;
                            }
                        }
                    });
                } else {
                    alert("This browser does not support HTML5 FileReader.");
                }
            });--%>

            $('#' + '<%= txtQuantity.ClientID %>').keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
                return true;
            });
            $('#' + '<%= txtCardNo.ClientID %>').keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $('#' + '<%= txtCCVNo.ClientID %>').keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $('[id$=txtCCVNo]').change(function () {
                if ($(this).hasClass('validate-required') && $(this).val() != '') {
                    $(this).removeClass('validate-required');
                }
            });
            $('[id$=txtNameOnCard]').change(function () {
                if ($(this).hasClass('validate-required') && $(this).val() != '') {
                    $(this).removeClass('validate-required');
                }
            });
            $('[id$=txtCardNo]').change(function () {
                if ($(this).hasClass('validate-required') && $(this).val() != '') {
                    $(this).removeClass('validate-required');
                }
            });
            if ($('[id$=dvExistingPatientName]').length > 0) {
                if ($('#' + '<%= rbtnNew.ClientID %>').attr('checked')) {
                    $('#<%=dvExistingPatientName.ClientID%>').css('display', 'none');
                    var valName = document.getElementById("<%=rfvPatient.ClientID%>");
                    ValidatorEnable(valName, false);
                }
                else if ($('#' + '<%= rbtnExisting.ClientID %>').attr('checked')) {
                    $('#<%=dvExistingPatientName.ClientID%>').css('display', '');
                    $('[id$=txtFirstName]').attr("disabled", "disabled");
                    $('#<%=txtLastName.ClientID%>').attr("disabled", "disabled");
                    $('#<%=txtEmail.ClientID%>').attr("disabled", "disabled");

                    var valName = document.getElementById("<%=rfvPatient.ClientID%>");
                    ValidatorEnable(valName, true);
                }
                $('#<%=rbtnNew.ClientID%>').change(function () {
                    if (this.checked) {
                        $('[id$=txtFirstName]').focus();
                        $('#<%=dvExistingPatientName.ClientID%>').css('display', 'none');
                        $('#<%=txtEmail.ClientID%>').removeAttr("disabled");
                        $('[id$=txtFirstName]').removeAttr('disabled');
                        $('#<%=txtLastName.ClientID%>').removeAttr('disabled');
                        $('[id$=txtFirstName]').val('');
                        $('#<%=txtLastName.ClientID%>').val('');
                        $('#<%=txtDateofBirth.ClientID%>').val('');
                        $('#<%=rbtnMale.ClientID%>').attr('checked', 'checked');
                        $('#<%=txtEmail.ClientID%>').val('');
                        ValidatorEnable(document.getElementById("<%=rfvPatient.ClientID%>"), false);
                    }
                });
                $('#<%=rbtnExisting.ClientID%>').change(function () {
                    if (this.checked) {
                        $('#<%=ddlPatient.ClientID%>').focus();
                        $('#<%=dvExistingPatientName.ClientID%>').css('display', '');
                        $('#<%=txtEmail.ClientID%>').attr("disabled", "disabled");
                        $('[id$=txtFirstName]').attr("disabled", "disabled");
                        $('#<%=txtLastName.ClientID%>').attr("disabled", "disabled");
                        $('#<%=ddlPatient.ClientID%>').val('0');
                        $('[id$=txtFirstName]').val('');
                        $('#<%=txtLastName.ClientID%>').val('');
                        $('#<%=txtDateofBirth.ClientID%>').val('');
                        $('#<%=txtEmail.ClientID%>').val('');
                        $('#<%=rbtnMale.ClientID%>').attr('checked', 'checked');

                        ValidatorEnable(document.getElementById("<%=rfvPatient.ClientID%>"), true);
                    }
                });
            }
            //CalculateTotalAmount();
            RegularShipment();
            if ($('#txtExpressShipment').length > 0)
                $('#divLocalContact').hide();
        }

        var currenttab;

        function CreateWizard() {
            $.getScript("Scripts/jquery-ui.js");
            var form = $("#widget").show();
            var allowvalidonStepChanged = true;
            form.steps({
                labels: {
                    cancel: "<%=this.GetLocalResourceObject("SkipSubmit")%>",
                    finish: "<%=this.GetLocalResourceObject("Finish")%>",
                    next: "<%=this.GetLocalResourceObject("lblCaseDetailResource1.Text")%>",
                    previous: "<%=this.GetLocalResourceObject("Previous")%>"
                },
                enablePagination: '<%= lCaseId > 0 ? false : true %>',
                enableAllSteps: '<%= lCaseId > 0 ? true : false%>',
                enableCancelButton: true,
                headerTag: "h3",
                bodyTag: "fieldset",
                transitionEffect: "none",
                titleTemplate: "#title#",
                onStepChanged: function (event, currentIndex, newIndex) {
                    switch (currentIndex) {
                        case 0:
                            $("#widget").find(".actions a:eq(1)").text("<%=this.GetLocalResourceObject("lblCaseDetailResource1.Text")%>");
                            if (currentIndex > newIndex) {
                                if (allowvalidonStepChanged) {
                                    //form.steps("setStep", CheckAllVaidation());
                                } else {
                                    form.steps("setStep", CheckAllVaidation());
                                }
                            }
                            //if (!currentIndex == 1) {
                            //    if (currentIndex > newIndex) {
                            //        form.steps("setStep", CheckAllVaidation());
                            //    } 
                            //}
                            break;
                        case 1:
                            $("#widget").find(".actions a:eq(1)").text("<%=this.GetLocalResourceObject("lblBeforeTemplateResource1.Text")%>");
                            if (currentIndex > newIndex) {
                                if (!allowvalidonStepChanged) {
                                    //form.steps("setStep", CheckAllVaidation());
                                } else {
                                    form.steps("setStep", CheckAllVaidation());
                                }
                            }
                            //if (!currentIndex == 1) {
                            //    if (currentIndex > newIndex) {
                            //        form.steps("setStep", CheckAllVaidation());
                            //    }
                            //}
                            break;
                        case 2:
                            $("#widget").find(".actions a:eq(1)").text("<%=this.GetLocalResourceObject("lblHeaderSelectPackageResource1.Text")%>");
                            $('.actions > ul > li:last-child').attr('style', 'display:block');
                            $('.actions > ul > li:last-child a').html("<%=this.GetLocalResourceObject("SkipSubmit")%>");
                            $('.actions > ul > li:last-child a').attr('title', "<%=this.GetLocalResourceObject("SkipSubmit")%>");
                            $('.actions > ul > li:last-child a').data('skip_payment', false);
                            if (currentIndex > newIndex) {
                                if (allowvalidonStepChanged) {
                                    //form.steps("setStep", CheckAllVaidation());
                                } else {
                                    form.steps("setStep", CheckAllVaidation());
                                }
                            }
                            //if (!currentIndex == 2) {
                            //    if (currentIndex > newIndex) {
                            //        form.steps("setStep", CheckAllVaidation());
                            //    }
                            //}
                            break;
                        case 3:
                            $("#widget").find(".actions a:eq(1)").text("<%=this.GetLocalResourceObject("lblHeadPaymentResource1.Text")%>");
                            if (currentIndex > newIndex) {
                                if (allowvalidonStepChanged) {
                                    //form.steps("setStep", CheckAllVaidation());
                                }
                                else {
                                    form.steps("setStep", CheckAllVaidation());
                                }
                            }
                            //if (!currentIndex == 3) {
                            //    if (currentIndex > newIndex) {
                            //        form.steps("setStep", CheckAllVaidation());
                            //    }
                            //}
                            break;
                        case 4:
                            if (currentIndex > newIndex) {
                                if (allowvalidonStepChanged) {
                                    //form.steps("setStep", CheckAllVaidation());
                                }
                                else {
                                    form.steps("setStep", CheckAllVaidation());
                                }
                            }
                            //if (!currentIndex == 4) {
                            //    if (currentIndex > newIndex) {
                            //        form.steps("setStep", CheckAllVaidation());
                            //    }
                            //}
                            break;
                    }
                },
                onStepChanging: function (event, currentIndex, newIndex) {
                    currenttab = newIndex;
                    if (currentIndex > newIndex) {
                        $('.actions > ul > li:last-child').attr('style', 'display:none');
                        return true;
                    }
                    var isValid = false;
                    switch (currentIndex) {
                        case 0:
                            allowvalidonStepChanged = isValid = SelectPatientValidation();
                            if (isValid)
                                $('.actions > ul > li:last-child').attr('style', 'display:none');
                            break;
                        case 1:
                            allowvalidonStepChanged = isValid = CaseDetailValidation();
                            if (isValid)
                                $('.actions > ul > li:last-child').attr('style', 'display:none');
                            break;
                        case 2:
                            if (BeforeTemplateValidation()) {
                                $('#lblDoctorName1').text = "Dr. " + $('#lblUser')[0].innerHTML;
                                $('#lblPatientName1').text = 'Patient : ' + $('[id$=txtFirstName]')[0].value + ' ' + $('#' + '<%= txtLastName.ClientID %>')[0].value;
                                $('.actions > ul > li:last-child').attr('style', 'display:none');
                                allowvalidonStepChanged = isValid = true;
                            }
                            else {
                                $('.actions > ul > li:last-child').attr('style', 'display:block');
                                $('.actions > ul > li:last-child a').html("<%=this.GetLocalResourceObject("SkipSubmit")%>");
                                $('.actions > ul > li:last-child a').attr('title', "<%=this.GetLocalResourceObject("SkipSubmit")%>");
                                $('.actions > ul > li:last-child a').data('skip_payment', false);
                                allowvalidonStepChanged = isValid = false;
                            }
                            break;
                        case 3:
                            allowvalidonStepChanged = isValid = SelectPackageValidation();
                            if (isValid)
                                $('.actions > ul > li:last-child').attr('style', 'display:none');
                            break;
                        case 4:
                            allowvalidonStepChanged = isValid = MakePaymentValidation();
                            if (isValid)
                                $('.actions > ul > li:last-child').attr('style', 'display:none');
                            break;
                    }

                    if (isValid) {
                        if (currenttab === 4) {
                            $('.actions > ul > li:last-child').attr('style', 'display:block');
                            $('.actions > ul > li:last-child a').html('Skip Payment & Save');
                            $('.actions > ul > li:last-child a').attr('title', 'Skip Payment & Save');
                            $('.actions > ul > li:last-child a').data('skip_payment', true);
                        }
                        else {
                            $('.actions > ul > li:last-child a').html("<%=this.GetLocalResourceObject("SkipSubmit")%>");
                            $('.actions > ul > li:last-child a').attr('title', "<%=this.GetLocalResourceObject("SkipSubmit")%>");
                            $('.actions > ul > li:last-child a').data('skip_payment', false);
                        }
                    }
                    return isValid;
                },
                onFinishing: function (event, currentIndex) {
                    return true;
                },
                onFinished: function (event, currentIndex) {
                    if (!$('#' + '<%= txtCardNo.ClientID %>')[0].disabled) {

                        var txtCCVNo = $('#txtCCVNo');
                        var txtNameOnCard = $('#txtNameOnCard');
                        var txtCardNo = $('#txtCardNo');

                        if (txtCCVNo.val() == "")
                            txtCCVNo.addClass('validate-required');
                        if (txtNameOnCard.val() == "")
                            txtNameOnCard.addClass('validate-required');
                        if (txtCardNo.val() == "")
                            txtCardNo.addClass('validate-required');

                        if (SelectPatientValidation()) {
                            if (CaseDetailValidation()) {
                                if (BeforeTemplateValidation()) {
                                    if (MakePaymentValidation()) {
                                        $("#<%=btnSubmit.ClientID %>")[0].click();
                                        return true;
                                    }
                                    else {
                                        alert("<%=this.GetLocalResourceObject("SelectPaymentMethod") %>");
                                        return false;
                                    }
                                }
                                else {
                                    return false;
                                }
                            }
                            else {
                                return false;
                            }
                        }
                        else {
                            return false;
                        }
                    }
                    else
                        window.location = "ListNewCase.aspx";
                    return true;
                },
                onCanceled: function (event) {
                    if ($('.actions > ul > li:last-child a').data('skip_payment')) {
                        $('#hdnIsSkipPayment').val(1);
                        $("#<%=btnSubmit.ClientID %>")[0].click();
                    }
                    else {
                        if (!$('[id$=txtNotes]')[0].disabled) {
                            if ($('#txtPackageAmount').val() != "" && $('#txtPackageAmount').val() != "0") {
                                if (!confirm("<%=this.GetLocalResourceObject("AlreadyPackage") %>")) {
                                    $('#txtPackageAmount').val("0");
                                    $('#txtAmount').val("0.00");
                                    $('[id$=ddlPackage]').val("0");
                                    $('#' + '<%= tblPackageDetail.ClientID %>')[0].innerHTML = "";
                                    $('#' + '<%= dvPackageImagelist.ClientID %>')[0].innerHTML = "";
                                }
                            }
                            CalculateTotalAmount();
                        }
                        form.steps("setStep", 4);
                    }
                }
            });
            if ($("#<%=select_tab.ClientID%>")[0].value != "") {
                currentIndex = $("#<%=select_tab.ClientID%>")[0].value;
                form.steps("setStep", currentIndex);
            }
            else
                currentIndex = 0;
            $('.actions > ul > li:last-child').attr('style', 'display:none');
        }

        function CheckAllVaidation() {
            if (SelectPatientValidation()) {
                if (CaseDetailValidationNoAlert()) {
                    if (BeforeTemplateValidationNoAlert()) {
                        if (MakePaymentValidation()) {
                            return 4;
                        }
                        else {
                            return currenttab;
                        }
                    }
                    else {
                        return 2;
                    }
                }
                else {
                    return 1;
                }
            }
            else {
                return 0;
            }
        }


        function BeforeTemplateValidation() {
            var caseTypeIndex;
            if ($("#ddlCaseType").length > 0) {
                caseTypeIndex = $("#ddlCaseType option:selected").index() - 1;
            }
            else {
                caseTypeIndex = $("#hdnCaseTypeID").val();
            }
            var IsBeforeTemplateRequired = $("#hdnCaseType").val().split(',')[caseTypeIndex];

            if (IsBeforeTemplateRequired == "True") {
                Page_ClientValidate("validation2");
                if (Page_IsValid) {
                    for (var i = 0; i <= 7; i++) {
                        if ($("#simpleFilePreview_" + i).find('img').length > 0) {
                        }
                        else {
                            alert("<%=this.GetLocalResourceObject("Pleaseuploadallimages") %>");
                            return false;
                        }
                    }
                    return true;
                }
                else {
                    alert("<%=this.GetLocalResourceObject("Pleaseuploadallimages") %>");
                    return false;
                }
            }
            return true;
        }

        function BeforeTemplateValidationNoAlert() {
            var caseTypeIndex;
            if ($("#ddlCaseType").length > 0) {
                caseTypeIndex = $("#ddlCaseType option:selected").index() - 1;
            }
            else {
                caseTypeIndex = $("#hdnCaseTypeID").val();
            }
            var IsBeforeTemplateRequired = $("#hdnCaseType").val().split(',')[caseTypeIndex];

            if (IsBeforeTemplateRequired == "True") {
                Page_ClientValidate("validation2");
                if (Page_IsValid) {
                    for (var i = 0; i <= 7; i++) {
                        if ($("#simpleFilePreview_" + i).find('img').length > 0) {
                        }
                        else {
                            return false;
                        }
                    }
                    return true;
                }
                else {
                    return false;
                }
            }
            return true;
        }

        function SelectPatientValidation() {
            $('#' + '<%= lblDoctorName.ClientID %>')[0].innerHTML = "Dr. " + $('#lblUser')[0].innerHTML;
            $('#' + '<%= lblPatientName.ClientID %>')[0].innerHTML = 'Patient : ' + $('[id$=txtFirstName]')[0].value + ' ' + $('#' + '<%= txtLastName.ClientID %>')[0].value;
            Page_ClientValidate("validation1");
            if (Page_IsValid) {
                BindBeforeImages();
                return true;
            }
            else {
                return false;
            }
        }

        function SelectPackageValidation() {
            if (BeforeTemplateValidation()) {
                if ($('#' + '<%= txtQuantity.ClientID %>').val() == "0") {
                    alert("<%=this.GetLocalResourceObject("QuantityErrorMessage") %>");
                    return false;
                }

                Page_ClientValidate("validation4");
                if (Page_IsValid) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
            return true;
        }

        function CaseDetailValidation() {
            var pagevalid = false;
            var chkbox = document.getElementById('<%= chkOrthoCondition.ClientID %>');
            var options = chkbox.getElementsByTagName('input');
            var listofspans = chkbox.getElementsByTagName('span');
            for (var i = 0; i < options.length; i++) {
                if (options[i].checked) {
                    pagevalid = true;
                    break;
                }
            }
            if ($("#ddlCaseType option:selected").val() == "0") {
                alert("<%=this.GetLocalResourceObject("Pleaseselectcase") %>");
                return false;
            }
            if (pagevalid == false && $('#' + '<%= chkOtherCondition.ClientID %>').is(':checked') && $('#' + '<%= txtOtherCondition.ClientID %>').val().length > 0) {
                return true;
            }
            if (!pagevalid) {
                alert("<%=this.GetLocalResourceObject("Pleaseselectanyoneorthocondition") %>");
                return false;
            }
            return true;
        }

        function CaseDetailValidationNoAlert() {
            var pagevalid = false;
            var chkbox = document.getElementById('<%= chkOrthoCondition.ClientID %>');
            var options = chkbox.getElementsByTagName('input');
            var listofspans = chkbox.getElementsByTagName('span');
            for (var i = 0; i < options.length; i++) {
                if (options[i].checked) {
                    pagevalid = true;
                    break;
                }
            }
            if ($("#ddlCaseType option:selected").val() == "0") {
                return false;
            }
            if (pagevalid == false && $('#' + '<%= chkOtherCondition.ClientID %>').is(':checked') && $('#' + '<%= txtOtherCondition.ClientID %>').val().length > 0) {
                return true;
            }
            if (!pagevalid) {
                return false;
            }
            return true;
        }

        function MakePaymentValidation() {
            if (BeforeTemplateValidation()) {
                if (!$('#' + '<%= txtCardNo.ClientID %>')[0].disabled) {
                    if ($("[id$='rbtcashpayment']").is(":checked")) {
                        return true;
                    }
                    else {
                        Page_ClientValidate("validation5");
                        if (Page_IsValid) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                }
            }
            else {
                return false;
            }
        }

        function validateImage(objFile) {
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp|.jfif)$/;
            if (typeof (FileReader) != "undefined") {                
                var file = $(objFile)[0];
                if (file.files.length > 0) {                    
                    if (regex.test(file.files[0].name.toLowerCase())) {
                        var size = parseFloat(file.files[0].size / 1024).toFixed(2);
                        if (size > 2024) {
                            alert("<%= this.GetLocalResourceObject("FileSize") %>");
                            objFile.value = "";
                            return false;
                        }

                        var reader = new FileReader();
                        reader.onload = function (e) {
                        }
                        reader.readAsDataURL(file.files[0]);
                    } else {                        
                        alert(file.files[0].name + " <%= this.GetLocalResourceObject("Isnotavalidimage") %>");
                        $(objFile).val('');
                        setTimeout(function () {                            
                            $(objFile).prev().show();
                        });                        
                        return false;
                    }
                }
            } else {
                alert("This browser does not support HTML5 FileReader.");
            }
            return true;
        }
        function txtEmailChange(EmailObject) {
            emailId = EmailObject.value;
            $.ajax({
                type: "POST",
                url: "AddNewCase.aspx/txtEmailChange",
                data: "{emailId : '" + emailId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != null) {
                        $(".From-Date").datepicker("option", "disabled", true);
                        $('[id$=txtEmail]').val(emailId);
                        $('[id$=txtFirstName]').val(msg.d.FirstName);
                        $('[id$=txtLastName]').val(msg.d.LastName);
                        $('[id$=txtDateofBirth]').val(msg.d.BirthDate);
                        $('[id$=hdnPatientId]').val(msg.d.PatientId);
                        $('[id$=rbtnMale]')[0].Checked = (msg.d.Gender == "M");
                        $('[id$=rbtnFemale]')[0].Checked = !(msg.d.Gender == "M");
                        $('[id$=txtFirstName]')[0].disabled = true;
                        $('[id$=txtLastName]')[0].disabled = true;
                        $('[id$=txtDateofBirth]')[0].disabled = true;
                        $('[id$=txtDateofBirth]')[0].read = true;
                    }
                    else {
                        $('[id$=txtFirstName]').val("");
                        $('[id$=txtLastName]').val("");
                        $('[id$=txtDateofBirth]').val("");
                        $('[id$=hdnPatientId]').val("");
                        $('[id$=rbtnMale]')[0].Checked = true;
                        $('[id$=txtFirstName]')[0].disabled = false;
                        $('[id$=txtLastName]')[0].disabled = false;
                        $('[id$=txtDateofBirth]')[0].disabled = false;
                        $(".From-Date").datepicker("option", "disabled", false);

                        var today = new Date();
                        var dd = today.getDate();
                        var mm = today.getMonth() + 1; //January is 0!

                        var yyyy = today.getFullYear();
                        if (dd < 10) {
                            dd = '0' + dd
                        }
                        if (mm < 10) {
                            mm = '0' + mm
                        }
                        var today = mm + '/' + dd + '/' + yyyy;
                    }
                }
            });
        }

        function ddlPackageChange(ddlPackage) {
            var packageId = parseFloat(ddlPackage.value);
            var tblPackageDetail = $('[id$=tblPackageDetail]');
            var dvPackageImagelist = $('[id$=dvPackageImagelist]');
            var tblrptPackageImage = $('[id$=tblrptPackageImage]');

            if (packageId > 0) {
                $.ajax({
                    type: "POST",
                    url: "AddNewCase.aspx/ddlPackageChange",
                    data: "{packageId : '" + packageId + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (tblPackageDetail.length > 0)
                            tblPackageDetail[0].innerHTML = "";
                        if (dvPackageImagelist.length > 0)
                            dvPackageImagelist[0].innerHTML = "";
                        if (tblrptPackageImage.length > 0)
                            tblrptPackageImage[0].innerHTML = "";

                        if (msg.d != null) {

                            $('#txtPackageAmount').val(eval(msg.d.Amount));

                            msg.d.ImageHtml = msg.d.ImageHtml.replace("##lblProductImage##", "<%=this.GetLocalResourceObject("PackageImage")%>");
                            dvPackageImagelist[0].innerHTML = msg.d.ImageHtml;

                            msg.d.PackageDetailsHtml = msg.d.PackageDetailsHtml.replace("##ProductName##", "<%=this.GetLocalResourceObject("ProductName")%>");
                            msg.d.PackageDetailsHtml = msg.d.PackageDetailsHtml.replace("##Quantity##", "<%=this.GetLocalResourceObject("Quantity")%>");
                            msg.d.PackageDetailsHtml = msg.d.PackageDetailsHtml.replace("##Amount##", "<%=this.GetLocalResourceObject("Amount")%>");
                            msg.d.PackageDetailsHtml = msg.d.PackageDetailsHtml.replace("##Total##", "<%=this.GetLocalResourceObject("Total")%>");

                            tblPackageDetail[0].innerHTML = msg.d.PackageDetailsHtml;
                            tblPackageDetail[0].style.display = "";

                            CalculateTotalAmount();
                            resizeJquerySteps();
                        }
                    }
                });
            }
            else {
                $('#txtPackageAmount').val(eval("0.00"));
                CalculateTotalAmount();

                if (tblPackageDetail.length > 0)
                    tblPackageDetail[0].innerHTML = "";
                if (dvPackageImagelist.length > 0)
                    dvPackageImagelist[0].innerHTML = "";
                if (tblrptPackageImage.length > 0)
                    tblrptPackageImage[0].innerHTML = "";
            }
        }

        function ddlCaseTypeChange(ddlCaseType) {
            DiscountCancel();
            var CaseId = ddlCaseType.value;
            var CountryId = $('#hdnCountryId').val();
            $.ajax({
                type: "POST",
                url: "AddNewCase.aspx/ddlCaseTypeChange",
                data: JSON.stringify({ caseId: CaseId, countryId: CountryId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != null) {
                        $('#txtCaseCharge').val(msg.d.split(',')[0]);
                        $('#hdnCaseCharge').val(msg.d.split(',')[0]);
                        $('#txtExpressShipment').val(msg.d.split(',')[1]);
                        $('#hdnExpressShipment').val(msg.d.split(',')[1]);
                        CalculateTotalAmount();
                    }
                }
            });
        }

        function resizeJquerySteps() {
            $('.wizard .content').animate({ height: $('.body.current').outerHeight() }, "slow");
        }

        function CalculateTotalAmount() {
            var qty = $('#txtQuantity').length > 0 ? parseFloat($('#txtQuantity').val()) : 0;
            var pkgAmt = $('#txtPackageAmount').length > 0 ? parseFloat($('#txtPackageAmount').val()) : 0;

            pkgAmt = pkgAmt == '' ? 0 : pkgAmt;

            var casecharge = parseFloat($('#txtCaseCharge').val());
            var isDiscount = $('#txtPromoDiscount').length;

            var pkgTotalAmt = (parseFloat(pkgAmt) * eval(qty));
            var totalCasePackage = (pkgTotalAmt + eval(casecharge));
            var payableAmt;

            $('#txtAmount').val(pkgTotalAmt.toFixed(2));
            $('#txtPackageAmt').val(pkgTotalAmt.toFixed(2));
            $('[id$=hdnPackageAmt]').val(pkgAmt);
            if (isDiscount == 0) {
                payableAmt = totalCasePackage;
            }
            else {
                var discount = $('#txtPromoDiscount').val();
                payableAmt = (totalCasePackage - eval(discount));
                $('#txtTotalCasePackage').val(totalCasePackage.toFixed(2));
            }

            if ($('#txtExpressShipment').length > 0)
                payableAmt = (payableAmt + parseFloat($('#txtExpressShipment').val()));

            if (parseFloat($('#txtCaseTypeDiscount').val()) > 0)
                payableAmt = payableAmt - parseFloat($('#txtCaseTypeDiscount').val());

            $('#hdnTotalAmount').val(payableAmt.toFixed(2));
            $('#txtPayableAmt').val(payableAmt.toFixed(2));
            $('#txtCashAmount').val(payableAmt.toFixed(2));
        }

        function CardNoValidate() {
            var cardType = $('#' + '<%= ddlCardType.ClientID %>').val();
            var cardNo = $('#' + '<%= txtCardNo.ClientID %>').val();

            if (cardType == "AMEX")
                $('#' + '<%= txtCCVNo.ClientID %>')[0].MaxLength = 4;
            else
                $('#' + '<%= txtCCVNo.ClientID %>')[0].MaxLength = 3;

            if (cardNo != null || cardNo != "") {
                $.ajax({
                    type: "POST",
                    url: "AddNewCase.aspx/ValidateCardNoByCardType",
                    data: JSON.stringify({ cardNo: cardNo, cardType: cardType }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d != null) {
                            if (msg.d == true) {
                                return true;
                            }
                            else {
                                alert("<%= this.GetLocalResourceObject("CardNoIsNotMatch") %>");
                                return false;
                            }
                        }
                    }
                });
            }
        }

        function ServerMonthValidate() {
            var month = $('#' + '<%= ddlMonth.ClientID %>').val();
            var year = $('#' + '<%= ddlYear.ClientID %>').val();

            $.ajax({
                type: "POST",
                url: "AddNewCase.aspx/ServerMonthValidate",
                data: JSON.stringify({ month: month, year: year }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != null) {
                        if (msg.d == true) {
                            return true;
                        }
                        else {
                            alert("<%= this.GetLocalResourceObject("MonthValidMessage") %>");
                            return false;
                        }
                    }
                }
            });
        }

        function BindBeforeImages() {
            var caseId = '<%= lCaseId %>';
            if (caseId > 0) {
                var hdnImages = document.getElementById('<%= hdnImages.ClientID %>').value;
                hdnImages = hdnImages.toString().split(',');
                for (var i = 0; i < 8; i++) {
                    var img = $('<img>');
                    img.attr('src', "/PatientFiles/slides/" + hdnImages[i]);
                    img.attr('class', 'simpleFilePreview_preview');
                    img.appendTo("#simpleFilePreview_" + i);
                    document.getElementById("simpleFilePreview_" + i).getElementsByTagName("a")['0'].style.display = "none";
                }
            }
        }
        function BindAfterImages() {
            var caseId = '<%= lCaseId %>';
            if (caseId > 0) {
                var hdnImages = document.getElementById('<%= hdnImages.ClientID %>').value;
                hdnImages = hdnImages.toString().split(',');
                if (hdnImages.length > 8) {
                    for (var i = 8; i < 16; i++) {
                        var img = $('<img>');
                        img.attr('src', "/PatientFiles/slides/" + hdnImages[i]);
                        img.attr('class', 'simpleFilePreview_preview');
                        img.appendTo("#simpleFilePreview_" + i);
                        document.getElementById("simpleFilePreview_" + i).getElementsByTagName("a")['0'].style.display = "none";
                    }
                }
            }
        }
        function checkccvno() {
            var maxlength = $('#' + '<%= txtCCVNo.ClientID %>')[0].MaxLength;
            var ccvno = $('#' + '<%= txtCCVNo.ClientID %>').val();
            if (maxlength != ccvno.length) {
                return false;
            }
            return true;
        }
        function SetPaymentMode(mode) {
            if (mode) {
                $('[id$=divSelectPayment]').hide();
                $('[id$=phCreditCardMode]').show();
            }
            else {
                $('[id$=divSelectPayment]').show();
                $('[id$=phCreditCardMode]').hide();
            }
            resizeJquerySteps();
        }
        function RegularShipment() {

            //$('#hdnLocalContact').val($('[id$=divLocalContact]')[0].innerHTML);

            if ($('[id$=chkIsRegularShipment]').length > 0) {
                if ($('[id$=chkIsRegularShipment]')[0].checked) {
                    $('#hdnExpressShipment').val($('#txtExpressShipment').val());
                    $('#txtExpressShipment').val("0.00");
                    //$('[id$=divLocalContact]').show();
                }
                else {
                    $('#txtExpressShipment').val($('#hdnExpressShipment').val());
                    //$('[id$=divLocalContact]').hide();
                }
            }
            CalculateTotalAmount();
            resizeJquerySteps();
        }
        function DiscountApply() {
            var CouponCode = $('#txtDiscountCouponCode').val();
            var CurrentCaseCharge = $('#txtCaseCharge').val();
            var CaseTypeId = $("#ddlCaseType option:selected").val();

            if (CouponCode == "") {
                alert("<%= this.GetLocalResourceObject("Entercouponcode") %>");
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "AddNewCase.aspx/DiscountValidate",
                    data: JSON.stringify({ couponCode: CouponCode, currentCaseCharge: CurrentCaseCharge, caseTypeId: CaseTypeId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d != null) {
                            if (msg.d != "") {
                                $('#txtTotalCasePackage1').val(msg.d);
                                $('#txtDiscountCouponCode').attr("disable", "disable");
                                $('#txtCaseTypeDiscount').val((parseFloat(CurrentCaseCharge) - parseFloat(msg.d)).toFixed(2));
                                $('[id$=divCaseTypeDiscountCal]').show();
                                $('#hdnCaseTypeDiscount').val($('#txtCaseTypeDiscount').val());

                                CalculateTotalAmount();
                                return true;
                            }
                            else {
                                alert("<%= this.GetLocalResourceObject("Couponisnotvalidorexpired") %>");
                                return false;
                            }
                        }
                    }
                });
            }
        }

        function DiscountCancel() {
            $('#txtDiscountCouponCode').val("");
            $('#txtCaseTypeDiscount').val("");
            $('[id$=divCaseTypeDiscountCal]').hide();
            $('#hdnCaseCharge').val($('#txtCaseCharge').val());
            CalculateTotalAmount();
        }
    </script>
</asp:Content>
