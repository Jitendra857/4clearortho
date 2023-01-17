<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditCountry.aspx.cs" Inherits="_4eOrtho.Admin.AddEditCountry" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#' + '<%= txtCountryName.ClientID %>').focus();
        });
    </script>
    <div id="container" class="cf">
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
                            <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/ListCountry.aspx"
                                TabIndex="5" meta:resourcekey="btnBackResource1"></asp:Button>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgNameResource1"></asp:Label>
        </div>
        <div class="widecolumn">
            <div class="personal_box alignleft">
                <div id="country" class="parsonal_textfild" runat="server">
                    <label style="width: 128px;">
                        <asp:Label ID="lblCountryName" runat="server" Text="Country Name" meta:resourcekey="lblCountryNameResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <asp:TextBox ID="txtCountryName" runat="server" meta:resourcekey="txtCountryNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvCountryName" ValidationGroup="validation" runat="server" ErrorMessage="Please enter country name" ControlToValidate="txtCountryName" Display="None" meta:resourcekey="rqvCountryNameResource1"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveCountryName" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rqvCountryName" Enabled="True" />
                    <asp:CustomValidator runat="server" ID="custxtCountryName" Display="None" ControlToValidate="txtCountryName"
                        OnServerValidate="custxtCountryName_ServerValidate" ValidationGroup="validation"
                        SetFocusOnError="True" CssClass="error"
                        ErrorMessage="This Country is already exist, please try another one"
                        meta:resourcekey="custxtCountryNameResource1" />
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcecustxtCountryName" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="custxtCountryName" Enabled="True">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </div>
                <div id="isactive" class="parsonal_textfild" runat="server">
                    <label style="width: 128px;">
                        <asp:Label ID="lblActive" runat="server" Text="IsActive" meta:resourcekey="lblActiveResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <asp:CheckBox ID="chkActive" runat="server" Checked="true" meta:resourcekey="chkActiveResource1" />
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

</asp:Content>
