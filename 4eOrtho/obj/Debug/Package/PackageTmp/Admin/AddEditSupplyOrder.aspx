<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditSupplyOrder.aspx.cs" Inherits="_4eOrtho.Admin.AddEditSupplyOrder" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#' + '<%= chkIsDispatch.ClientID %>').focus();
            $('#dvDispatchRemarks').css("display", "none");
        });
        function pageLoad()
        {
            if ($('#' + '<%= chkIsDispatch.ClientID %>').attr('checked'))
                $('#dvDispatchRemarks').css("display", "");
            else
                $('#dvDispatchRemarks').css("display", "none");
        }
        function CheckChanged() {
            if ($('#' + '<%= chkIsDispatch.ClientID %>').attr('checked'))
                $('#dvDispatchRemarks').css("display", "");
            else
                $('#dvDispatchRemarks').css("display", "none");
        }
    </script>
    <div id="container" class="cf">
        <div class="page_title">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        <h2 class="padd">
                            <asp:Label ID="lblHeader" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label>

                        </h2>
                    </td>
                    <td style="width: 50%;">
                        <span class="dark_btn_small">
                            <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/ListSupplyOrder.aspx" TabIndex="10" meta:resourcekey="btnBackResource1" />
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div id="divMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
            </div>
        </div>
        <div class="widecolumn">
            <div class="personal_box alignleft">
                <div id="state" class="parsonal_textfild" runat="server" width="200px">

                    <asp:RadioButtonList ID="rdblSupply" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdblSupply_SelectedIndexChanged" meta:resourcekey="rdblSupplyResource1" Enabled="false">
                        <asp:ListItem Selected="True" Text="Select Product" Value="1" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Select Package" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                    </asp:RadioButtonList>


                    <div class="parsonal_select product-select">
                        <asp:DropDownList ID="ddlSuppply" runat="server" TabIndex="1" meta:resourcekey="ddlSuppplyResource1" Enabled="false">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rqvSupply" ForeColor="Red" runat="server" ControlToValidate="ddlSuppply"
                            SetFocusOnError="True" Display="None"
                            ErrorMessage="Please select Product Name / Package Name" CssClass="errormsg"
                            ValidationGroup="validation" InitialValue="0" meta:resourcekey="rqvSupplyResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveProductName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvSupply" Enabled="True" />
                    </div>
                </div>
                <div id="email" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label runat="server" ID="lblEmail" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <div>
                        <asp:TextBox ID="txtEmailid" runat="server" MaxLength="50" TabIndex="2" meta:resourcekey="txtEmailidResource1" Enabled="false"></asp:TextBox>
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
                    </div>
                </div>

                <div id="amount" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblAmount" runat="server" Text="Amount ($)" meta:resourcekey="lblAmountResource1" Enabled="false"></asp:Label><span
                            class="asteriskclass">*</span><span class="alignright">:</span></label>

                    <asp:TextBox ID="txtAmount" runat="server" MaxLength="15" Style="text-align: right;" TabIndex="3" meta:resourcekey="txtAmountResource1" Enabled="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvAmount" ForeColor="Red" runat="server" SetFocusOnError="True"
                        ControlToValidate="txtAmount" Display="None" ErrorMessage="Please enter amount."
                        CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvAmountResource1" />
                    <asp:RegularExpressionValidator ID="rgvAmount" runat="server" ControlToValidate="txtAmount"
                        SetFocusOnError="True" ValidationExpression="\d+(\.\d{1,2})?" ValidationGroup="validation"
                        CssClass="errormsg" ErrorMessage="Only Numeric Values with two precesion values is allowed"
                        meta:resourcekey="rgvAmountResource1"></asp:RegularExpressionValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteMobile" runat="server" Enabled="True"
                        TargetControlID="txtAmount" ValidChars="0123456789." />
                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveuploadPhoto" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rqvAmount" Enabled="True" Width="140px">
                    </ajaxToolkit:ValidatorCalloutExtender>

                </div>

                <div id="productQuantity" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblProductAmount" runat="server" Text="Quantity" meta:resourcekey="lblProductAmountResource1"></asp:Label><span
                            class="asteriskclass">*</span><span class="alignright">:</span></label>

                    <asp:TextBox ID="txtQuantity" runat="server" MaxLength="15" Style="text-align: right;" TabIndex="4" meta:resourcekey="txtQuantityResource1" Enabled="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvQuantity" ForeColor="Red" runat="server" SetFocusOnError="True"
                        ControlToValidate="txtQuantity" Display="None" ErrorMessage="Please enter Qunatity."
                        CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvQuantityResource1" />
                    <asp:RegularExpressionValidator ID="rgvQuantity" runat="server" ControlToValidate="txtQuantity"
                        SetFocusOnError="True" ValidationExpression="^(0|[1-9][0-9]*)$" ValidationGroup="validationProduct"
                        CssClass="errormsg" ErrorMessage="Only Numeric Values are allowed" meta:resourcekey="rgvQuantityResource1"></asp:RegularExpressionValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteQuantity" runat="server" Enabled="True"
                        TargetControlID="txtQuantity" ValidChars="0123456789" />
                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveQuantity" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rqvQuantity" Enabled="True" Width="140px">
                    </ajaxToolkit:ValidatorCalloutExtender>

                </div>

                <div id="totalAmount" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount" meta:resourcekey="lblTotalAmountResource1"></asp:Label><span
                            class="asteriskclass">*</span><span class="alignright"  >:</span></label>
                    <asp:TextBox ID="txtTotalAmount" runat="server" MaxLength="50" Style="text-align: right;" TabIndex="4" Enabled="false"></asp:TextBox>
                </div>
                <div id="isDispatch" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label runat="server" ID="Label1" Text="Is Dispatch" meta:resourcekey="Label1Resource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>

                    <div style="padding-top: 4px; margin-bottom: 19px;">
                        <asp:CheckBox ID="chkIsDispatch" runat="server" Checked="True" TabIndex="6" meta:resourcekey="chkIsDispatchResource1" onclick="return CheckChanged();" />
                    </div>

                </div>
                <div id="dvDispatchRemarks" class="parsonal_textfild">
                    <label>
                        <asp:Label runat="server" ID="lblRemarks" Text="Dispatch Remarks"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <asp:TextBox ID="txtDispatchRemarks" runat="server" MaxLength="450" TabIndex="5"></asp:TextBox>
                </div>

                <div id="isActive" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div style="padding-top: 4px;">
                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="7" meta:resourcekey="chkIsActiveResource1" Enabled="false" />
                    </div>
                </div>
            </div>
        </div>
        <div class="bottom_btn tpadd alignright">
            <span class="blue_btn">
                <asp:Button ID="btnSubmit" runat="server" Text="Save" ValidationGroup="validation"
                    OnClick="btnSubmit_Click" TabIndex="8" meta:resourcekey="btnSubmitResource1" />
            </span><span class="dark_btn">
                <asp:Button runat="server" ID="btnReset" Text="Reset" TabIndex="9" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
            </span>
        </div>
    </div>

</asp:Content>
