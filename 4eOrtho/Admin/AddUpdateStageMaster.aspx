<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddUpdateStageMaster.aspx.cs" Inherits="_4eOrtho.Admin.AddUpdateStageMaster" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
                                    <asp:Label ID="lblHeader" runat="server" Text="Add Stage" meta:resourcekey="lblHeaderResource1"></asp:Label>
                                    <asp:Label ID="lblHeaderEdit" runat="server" Text="Edit Stage" Visible="False" meta:resourcekey="lblHeaderEditResource1"></asp:Label></h2>
                            </td>
                            <td style="width: 50%;">
                                <span class="dark_btn_small">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/StageList.aspx" meta:resourcekey="btnBackResource1" />
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
                    
                        <div class="clear">
                        </div>
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblCaseCharge" runat="server" Text="Stage Name" meta:resourcekey="lblCaseChargeResource1"></asp:Label>
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
                                <asp:Label ID="Label1" runat="server" Text="Stage Price" meta:resourcekey="lblCaseChargeResource1"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="TextBox1" runat="server" TabIndex="3" meta:resourcekey="txtCaseChargeResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validation" ErrorMessage="Please enter case charge."
                                ControlToValidate="txtCaseCharge" Display="None" meta:resourcekey="rfvCaseChargeResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rfvCaseCharge" Enabled="True" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                TargetControlID="txtCaseCharge" ValidChars="0123456789." />
                        </div>

                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblActiveDiscount" runat="server" Text="Is Active" meta:resourcekey="lblActiveDiscountResource1"></asp:Label>
                                <span class="alignright">:</span>
                            </label>
                            <asp:CheckBox ID="chkDiscount" ClientIDMode="Static" TabIndex="4" runat="server" Checked="True" onchange="EnableDisableDiscountValidation();" meta:resourcekey="chkDiscountResource1" />
                        </div>
                        <div class="clear">
                        </div>

                    </div>
                </div>
                <div class="bottom_btn tpadd alignright" style="width: 268px;">
                    <span class="blue_btn">
                        <asp:Button ID="btnSave" runat="server" TabIndex="10" Text="Save" ValidationGroup="validation" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
                    </span><span class="dark_btn">
                       
                    </span>
                </div>
            </div>
            <asp:HiddenField ID="hdnAlreadyUseCaseId" runat="server" ClientIDMode="Static" />
        </ContentTemplate>
       </asp:UpdatePanel>
    </asp:Content>
   
  