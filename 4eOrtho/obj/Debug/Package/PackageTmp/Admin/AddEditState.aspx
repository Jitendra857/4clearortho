<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditState.aspx.cs" Inherits="_4eOrtho.Admin.AddEditState" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="cf">
        <div class="page_title">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        <h2 class="padd">
                            <asp:Label ID="lblHeader" runat="server" Text="Add State Details"></asp:Label>
                            <asp:Label ID="lblHeaderEdit" runat="server" Text="Edit State Details" Visible="False"></asp:Label></h2>
                    </td>
                    <td style="width: 50%;">
                        <span class="dark_btn_small">
                            <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/ListState.aspx" meta:resourcekey="btnBackResource1"  />
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
                    <label style="width: 128px;">
                        <asp:Label ID="lblState" runat="server" Text="State" meta:resourcekey="lblStateResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <asp:TextBox ID="txtState" runat="server" meta:resourcekey="txtStateResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvState" runat="server" ValidationGroup="validation" ErrorMessage="Please enter state" ControlToValidate="txtState" Display="None" meta:resourcekey="rqvStateResource1"></asp:RequiredFieldValidator>
                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveState" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rqvState" Enabled="True" />
                    <asp:CustomValidator runat="server" ID="custxtStateName" ControlToValidate="txtState"
                        SetFocusOnError="True"
                        OnServerValidate="custxtStateName_ServerValidate" Display="None"
                        ValidationGroup="validation" CssClass="error"
                        ErrorMessage="This State is already exist, please try another one"
                        meta:resourcekey="custxtStateNameResource1" />
                    <ajaxToolkit:ValidatorCalloutExtender ID="vcecustxtStateName" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="custxtStateName" Enabled="True">
                    </ajaxToolkit:ValidatorCalloutExtender>
                </div>

                <div id="country" class="parsonal_textfild" runat="server" style="margin-bottom: 33px;">
                    <label style="width: 128px;">
                        <asp:Label ID="lblCountry" runat="server" Text="Country" meta:resourcekey="lblCountryResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>

                    <div class="parsonal_select">
                        <asp:DropDownList ID="ddlCountry" runat="server" meta:resourcekey="ddlCountryResource1"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rqvCountry" runat="server" InitialValue="0" ValidationGroup="validation" ErrorMessage="please select Country" ControlToValidate="ddlCountry" Display="None" meta:resourcekey="rqvCountryResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveCountry" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvCountry" Enabled="True" />
                    </div>
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
