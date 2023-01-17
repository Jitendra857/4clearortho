<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditCaseCharges.aspx.cs" Inherits="_4eOrtho.Admin.AddEditCaseCharges" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../Styles/Jquery-UI/jquery-ui-1.8.23.custom.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upCaseType" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <h2 class="padd">
                                    <asp:Label ID="lblHeader" runat="server" Text="Add Case Charge" meta:resourcekey="lblHeaderResource1"></asp:Label>
                                    <asp:Label ID="lblHeaderEdit" runat="server" Text="Edit Case Charge" Visible="False" meta:resourcekey="lblHeaderEditResource1"></asp:Label></h2>
                            </td>
                            <td style="width: 50%;">
                                <span class="dark_btn_small">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/ListCaseCharges.aspx" meta:resourcekey="btnBackResource1" />
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
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblCaseType" runat="server" Text="CaseType" meta:resourcekey="lblCaseTypeResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <div class="parsonal_select">
                                <asp:DropDownList ID="ddlCaseType" ClientIDMode="Static" runat="server" TabIndex="1" meta:resourcekey="ddlCaseTypeResource1"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqvCaseType" runat="server" InitialValue="0" ValidationGroup="validation" ErrorMessage="please select case type."
                                    ControlToValidate="ddlCaseType" Display="None" meta:resourcekey="rqvCaseTypeResource1"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="rqveCaseType" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rqvCaseType" Enabled="True" />
                            </div>
                            <asp:ImageButton ID="imgAddEditCaseType" runat="server" ImageUrl="~/Admin/Images/add01.png" PostBackUrl="~/Admin/AddEditCaseTypes.aspx" ToolTip="Add Case Type" Style="width: 28px; height: 28px; padding: 2px;" meta:resourcekey="imgAddEditCaseTypeResource1" />
                        </div>
                        <div class="clear">
                        </div>
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lbldoctor" runat="server" Text="Doctor"></asp:Label>
                            </label>
                            <div class="parsonal_select">
                                <asp:DropDownList ID="DllDoctor" ClientIDMode="Static" runat="server" TabIndex="2"></asp:DropDownList>                               
                                 <asp:CustomValidator runat="server" ID="customcheckdoctotcase" ControlToValidate="DllDoctor"
                                    SetFocusOnError="True" OnServerValidate="customcheckdoctotcase_ServerValidate" Display="None"
                                    ValidationGroup="validation" CssClass="error"
                                    ErrorMessage="The case charge is already created for the selected doctor and selected casetype." />
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcecustxtUserName" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="customcheckdoctotcase" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblCaseCharge" runat="server" Text="Case Charge ($)" meta:resourcekey="lblCaseChargeResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtCaseCharge" runat="server" TabIndex="3" meta:resourcekey="txtCaseChargeResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCaseCharge" runat="server" ValidationGroup="validation" ErrorMessage="Please enter case charge."
                                ControlToValidate="txtCaseCharge" Display="None" meta:resourcekey="rfvCaseChargeResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rfvCaseCharge" Enabled="True" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                TargetControlID="txtCaseCharge" ValidChars="0123456789." />
                        </div>

                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblActiveDiscount" runat="server" Text="Is Discount" meta:resourcekey="lblActiveDiscountResource1"></asp:Label>
                                <span class="alignright">:</span>
                            </label>
                            <asp:CheckBox ID="chkDiscount" ClientIDMode="Static" TabIndex="4" runat="server" Checked="True" onchange="EnableDisableDiscountValidation();" meta:resourcekey="chkDiscountResource1" />
                        </div>
                        <div class="clear">
                        </div>

                        <div id="divDiscount" style="display: none;">
                            <div class="parsonal_textfild">
                                <label>
                                    <asp:Literal ID="ltrDiscountType" runat="server" Text="Type of Discount" meta:resourcekey="ltrDiscountTypeResource1"></asp:Literal>
                                    <span class="alignright">:</span></label>
                                <div class="radio-selection" style="width: 50%;">
                                    <asp:RadioButton ID="rbtnPercentage" runat="server" TabIndex="5" GroupName="Discount" Checked="True" Text="Percentage" meta:resourcekey="rbtnPercentageResource1" />
                                    <asp:RadioButton ID="rbtnAmount" runat="server" TabIndex="6" GroupName="Discount" Text="Amount" meta:resourcekey="rbtnAmountResource1" />
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="parsonal_textfild">
                                <label>
                                    <asp:Literal ID="ltr" runat="server" Text="Coupon Code" meta:resourcekey="ltrResource1"></asp:Literal>
                                    <span class="required">*</span><span class="alignright">:</span></label>
                                <asp:TextBox ID="txtCouponCode" runat="server" MaxLength="30" TabIndex="7" AutoPostBack="True" meta:resourcekey="txtCouponCodeResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rqvCouponCode" ClientIDMode="Static" Display="None" ControlToValidate="txtCouponCode"
                                    SetFocusOnError="True" CssClass="error" ValidationGroup="validation"
                                    ErrorMessage="Please Enter Coupon Code" meta:resourcekey="rqvCouponCodeResource1"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="vcerqvCouponCode" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rqvCouponCode" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:CustomValidator runat="server" ID="custCouponCode" ControlToValidate="txtCouponCode" ClientIDMode="Static"
                                    SetFocusOnError="True" Display="None" OnServerValidate="custCouponCode_ServerValidate"
                                    ValidationGroup="validation" CssClass="error" ErrorMessage="Coupon Code already exist, please try another one" meta:resourcekey="custCouponCodeResource1" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="rgvcustomcodeval" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="custCouponCode" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </div>

                            <div class="clear">
                            </div>
                            <div class="parsonal_textfild" id="divValue" runat="server">
                                <label>
                                    <asp:Literal ID="ltrValue" runat="server" Text="Discount Value" meta:resourcekey="ltrValueResource1"></asp:Literal>
                                    <span class="required">*</span><span class="alignright">:</span></label>
                                <asp:TextBox ID="txtDiscountValue" runat="server" MaxLength="6" TabIndex="8" meta:resourcekey="txtDiscountValueResource1"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtDiscountValue"
                                    ValidChars="0123456789." />
                                <asp:RequiredFieldValidator runat="server" ID="rqvDiscountValue" Display="None" ControlToValidate="txtDiscountValue"
                                    SetFocusOnError="True" CssClass="error" ValidationGroup="validation" ClientIDMode="Static"
                                    ErrorMessage="Please Enter Discount Value." meta:resourcekey="rqvDiscountValueResource1"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rqvDiscountValue" Enabled="True">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="parsonal_textfild">
                                <label id="lblToDate" class="smalltitle">
                                    <asp:Label ID="lblExpiryDate" runat="server" Text="Coupon Expiry Date" meta:resourcekey="lblExpiryDateResource1"></asp:Label><span class="required">*</span><span
                                        class="alignright">:</span>
                                </label>
                                <label id="lblToDateText" class="datepicket-field" style="width: 300px;">
                                    <asp:TextBox ID="txtExpiryDate" ClientIDMode="Static" CssClass="To-Date not-edit textfild search-datepicker" Width="70px" TabIndex="9"
                                        runat="server" meta:resourcekey="txtExpiryDateResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rqvExpiryDate" Display="None" ControlToValidate="txtExpiryDate"
                                        SetFocusOnError="True" CssClass="error" ValidationGroup="validation" ClientIDMode="Static"
                                        ErrorMessage="Please select coupon expiry date." meta:resourcekey="rqvExpiryDateResource1"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="customCalloutStyle"
                                        TargetControlID="rqvExpiryDate" Enabled="True">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="bottom_btn tpadd alignright" style="width: 268px;">
                    <span class="blue_btn">
                        <asp:Button ID="btnSave" runat="server" TabIndex="10" Text="Save" ValidationGroup="validation" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
                    </span><span class="dark_btn">
                        <input type="reset" tabindex="11" title='<%= this.GetLocalResourceObject("Reset") %>' value='<%= this.GetLocalResourceObject("Reset") %>' />
                    </span>
                </div>
            </div>
            <asp:HiddenField ID="hdnAlreadyUseCaseId" runat="server" ClientIDMode="Static" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(document).ready(function () {
            //document.getElementById('<%=ddlCaseType.ClientID%>').focus();
        });

        function pageLoad() {
            showDatePickers();
            EnableDisableDiscountValidation();
            $('.not-edit').attr("readonly", "readonly");

            //var sAlreadyUseCaseId = $('#hdnAlreadyUseCaseId').val().split(',');
            //for (var id in sAlreadyUseCaseId) {
            //    $("#ddlCaseType > option").each(function () {
            //        if (this.value == sAlreadyUseCaseId[id])
            //            this.disabled = true;
            //    });
            //}

        }
        function showDatePickers() {
            $('#txtExpiryDate').datepicker({
                showOn: "button",
                buttonText: '<%= this.GetLocalResourceObject("SelectDate") %>',
                buttonImage: "/admin/images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                minDate: new Date()
            });
        }
        function EnableDisableDiscountValidation() {
            var validationEnable = ($('#chkDiscount').is(':checked'));
            validationEnable ? $('#divDiscount').show() : $('#divDiscount').hide();

            var rqvCouponCode = document.getElementById("rqvCouponCode");
            var custCouponCode = document.getElementById("custCouponCode");
            var rqvDiscountValue = document.getElementById("rqvDiscountValue");
            var rqvExpiryDate = document.getElementById("rqvExpiryDate");

            ValidatorEnable(rqvCouponCode, validationEnable);
            ValidatorEnable(custCouponCode, validationEnable);
            ValidatorEnable(rqvDiscountValue, validationEnable);
            ValidatorEnable(rqvExpiryDate, validationEnable);

            //document.getElementById('<%=ddlCaseType.ClientID%>').focus();
        }
    </script>
</asp:Content>
