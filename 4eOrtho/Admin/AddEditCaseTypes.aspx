<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditCaseTypes.aspx.cs" Inherits="_4eOrtho.Admin.AddEditCaseType" Culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upLocalContact" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <h2 class="padd">
                                    <asp:Label ID="lblHeader" runat="server" Text="Case Types" meta:resourcekey="lblHeaderResource1"></asp:Label>
                                </h2>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="widecolumn">
                    <div class="personal_box alignleft">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="50%">
                                    <div class="personal_box alignleft" style="width: 90%;">
                                        <div id="country" class="parsonal_textfild" runat="server">
                                            <label style="width: 128px;">
                                                <asp:Label ID="lblCaseType" runat="server" Text="Case Type" meta:resourcekey="lblCaseTypeResource1"></asp:Label>
                                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                                            </label>
                                            <asp:TextBox ID="txtCaseType" runat="server" meta:resourcekey="txtCaseTypeResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqvCaseType" ValidationGroup="validation" runat="server" ErrorMessage="Please enter case type." ControlToValidate="txtCaseType" Display="None" meta:resourcekey="rqvCaseTypeResource1"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="rqveCaseType" runat="server" CssClass="customCalloutStyle"
                                                TargetControlID="rqvCaseType" Enabled="True" />
                                            <asp:CustomValidator runat="server" ID="custxtCaseType" Display="None" ControlToValidate="txtCaseType"
                                                OnServerValidate="custxtCaseType_ServerValidate" ValidationGroup="validation"
                                                SetFocusOnError="True" CssClass="error"
                                                ErrorMessage="This Case type is already exist, please try another one" meta:resourcekey="custxtCaseTypeResource1" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcecustxtCaseType" runat="server" CssClass="customCalloutStyle"
                                                TargetControlID="custxtCaseType" Enabled="True">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </div>
                                        <div class="parsonal_textfild">
                                            <label>
                                                <asp:Label runat="server" ID="lblIsBTRequired" Text="Before Template IsRequired" meta:resourcekey="lblIsBTRequiredResource1"></asp:Label>
                                                <span class="alignright">:</span>
                                            </label>
                                            <asp:CheckBox ID="chkIsBTRequired" runat="server" Checked="True" TabIndex="4" meta:resourcekey="chkIsBTRequiredResource1" />
                                        </div>
                                        <div class="bottom_btn tpadd alignright" style="width: 268px;">
                                            <span class="blue_btn">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="validation" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
                                            </span><span class="dark_btn">
                                                <asp:Button runat="server" ID="btnReset" Text="Reset" TabIndex="9" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                                            </span>
                                        </div>
                                    </div>
                                </td>
                                <td align="left" valign="top">
                                    <div>
                                        <asp:ListView ID="lvCaseType" runat="server">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="rgt">
                                                    <tr>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Literal ID="ltrCaseType" runat="server" Text="Case Type" meta:resourcekey="ltrCaseTypeResource2"></asp:Literal>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="20%">
                                                            <asp:Literal ID="ltrIsBTRequired" runat="server" Text="Before Template IsRequired?" meta:resourcekey="ltrIsBTRequiredResource1"></asp:Literal>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="20%">
                                                            <asp:Literal ID="ltrAction" runat="server" Text="Action" meta:resourcekey="ltrActionResource2"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="3">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <asp:Label ID="lblLookupName" runat="server" Text='<%# Eval("LookupName") %>' meta:resourcekey="lblLookupNameResource1"></asp:Label>
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <%# !string.IsNullOrEmpty(Convert.ToString(Eval("LookupDescription"))) && Convert.ToBoolean(Eval("LookupDescription").ToString().Split('|')[1]) ? "<img src='Images/icon-active.gif'>" : "<img src='Images/icon-inactive.gif'>"  %>
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:ImageButton ID="imgbtnStatus" runat="server" OnClientClick="return DeactivateMessage(this);" CommandName="CUSTOMISACTIVE" CommandArgument='<%# Eval("LookupId") %>' OnCommand="Custom_Command"
                                                            ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>' meta:resourcekey="imgbtnStatusResource1" />
                                                        &nbsp;&nbsp;
                                                        <asp:ImageButton ID="hypEdit" runat="server" ImageUrl="images/edit.png" CommandName="CUSTOMEDIT" CommandArgument='<%# Eval("LookupId") %>' OnCommand="Custom_Command" meta:resourcekey="hypEditResource1" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <tr>
                                                    <td align="center" valign="middle" class="equip">
                                                        <asp:Literal ID="ltrCaseType" runat="server" Text="Case Type" meta:resourcekey="ltrCaseTypeResource1"></asp:Literal>
                                                    </td>
                                                    <td align="center" valign="middle" class="equip" width="20%">
                                                        <asp:Literal ID="ltrAction" runat="server" Text="Action" meta:resourcekey="ltrActionResource1"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="center" valign="middle" class="equip">
                                                        <asp:Literal ID="ltrNodatafound" runat="server" Text="No data found." meta:resourcekey="ltrNodatafoundResource1"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnCaseTypeId" runat="server" />
            <asp:HiddenField ID="hdnLookupCaseTypeId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
