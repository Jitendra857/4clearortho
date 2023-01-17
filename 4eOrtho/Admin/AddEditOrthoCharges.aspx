<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditOrthoCharges.aspx.cs" Inherits="_4eOrtho.Admin.AddEditExpressShipment" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="cf">
        <div class="page_title">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        <h2 class="padd">
                            <asp:Label ID="lblHeader" runat="server" Text="Add Express Shipment" meta:resourcekey="lblHeaderResource1"></asp:Label>
                            <asp:Label ID="lblHeaderEdit" runat="server" Text="Edit Express Shipment" Visible="False" meta:resourcekey="lblHeaderEditResource1"></asp:Label></h2>
                    </td>
                    <td style="width: 50%;">
                        <span class="dark_btn_small">
                            <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/ListOrthoCharges.aspx" meta:resourcekey="btnBackResource1" />
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

                <div id="country" class="parsonal_textfild" runat="server" style="margin-bottom: 33px;">
                    <label>
                        <asp:Label ID="lblCountry" runat="server" Text="Country" meta:resourcekey="lblCountryResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>

                    <div class="parsonal_select">
                        <asp:DropDownList ID="ddlCountry" runat="server" meta:resourcekey="ddlCountryResource1"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rqvCountry" runat="server" InitialValue="0" ValidationGroup="validation"
                            ErrorMessage="Please select Country." ControlToValidate="ddlCountry" Display="None" meta:resourcekey="rqvCountryResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveCountry" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvCountry" Enabled="True" />
                    </div>
                </div>

                <div class="parsonal_textfild">
                    <label>
                        <asp:Label ID="lblExpressShipmentAmount" runat="server" Text="Express Shipment Amount ($)" meta:resourcekey="lblExpressShipmentAmountResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <asp:TextBox ID="txtExpressShipmentAmount" runat="server" meta:resourcekey="txtExpressShipmentAmountResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvExpressShipmentAmount" runat="server" ValidationGroup="validation" ErrorMessage="Please enter Express Shipment Amount." ControlToValidate="txtExpressShipmentAmount"
                        Display="None" meta:resourcekey="rqvExpressShipmentAmountResource1"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveExpressShipmentAmount" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rqvExpressShipmentAmount" Enabled="True" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                        TargetControlID="txtExpressShipmentAmount" ValidChars="0123456789." />
                </div>
                <div class="parsonal_textfild">
                    <label>
                        <asp:Label ID="lblCaseType" runat="server" Text="CaseType" meta:resourcekey="lblCaseTypeResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <div class="personal_select">
                        <asp:ListBox ID="lstbxCaseType" ClientIDMode="Static" runat="server" Rows="10" TabIndex="1" SelectionMode="Multiple" meta:resourcekey="lstbxCaseTypeResource1"></asp:ListBox>
                        <asp:RequiredFieldValidator ID="rqvCaseType" runat="server" ValidationGroup="validation" ErrorMessage="Please Select Atleast one Case Type."
                            ControlToValidate="lstbxCaseType" Display="None" meta:resourcekey="rqvCaseTypeResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveCaseType" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvCaseType" Enabled="True" />
                    </div>
                    <%--<div class="personal_select">
                        <asp:ListBox ID="lstbxCaseType" ClientIDMode="Static" runat="server" TabIndex="1" SelectionMode="Multiple"></asp:ListBox>
                    </div>--%>
                </div>
                <div class="parsonal_textfild">
                    <label>
                        <asp:Label ID="lblCaseShipment" runat="server" Text=" Case Shipment ($)" meta:resourcekey="lblCaseShipmentResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <asp:TextBox ID="txtCaseShipment" runat="server" meta:resourcekey="txtCaseShipmentResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvCaseShipment" runat="server" ValidationGroup="validation" ErrorMessage="Please enter Case Shipment charge." ControlToValidate="txtCaseShipment"
                        Display="None" meta:resourcekey="rqvCaseShipmentResource1"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveCaseShipment" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rqvCaseShipment" Enabled="True" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                        TargetControlID="txtCaseShipment" ValidChars="0123456789." />
                </div>
            </div>
        </div>
        <div class="bottom_btn tpadd alignright" style="width: 268px;">
            <span class="blue_btn">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="validation" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
            </span><span class="dark_btn">
                <input type="reset" tabindex="7" title='<%= this.GetLocalResourceObject("Reset") %>' value='<%= this.GetLocalResourceObject("Reset") %>' />
            </span>
        </div>
    </div>
    <asp:HiddenField ID="hdnAlreadyUseCaseId" runat="server" Value="0" />
</asp:Content>
