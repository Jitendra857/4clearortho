<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" UICulture="auto" ValidateRequest="false" CodeBehind="AddEditContentPage.aspx.cs" Inherits="_4eOrtho.Admin.AddEditContentPage" Culture="auto" meta:resourcekey="PageResource2" %>

<%@ Register Src="~/UserControls/HTMLEditor.ascx" TagName="UCHTMLEditor" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .parsonal_textfild label {
            width: 236px;
        }
    </style>
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
                            <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/ListContentPage.aspx"
                                TabIndex="5" meta:resourcekey="btnBackResource1"></asp:Button>
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
                <div id="language" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblLanguage" runat="server" Text="Language" meta:resourcekey="lblLanguageResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="parsonal_select">
                        <asp:DropDownList ID="ddlLanguage" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" meta:resourcekey="ddlLanguageResource1">
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <br />
                <br />
                <div id="menuname" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblMenuName" runat="server" Text="Menu Name" meta:resourcekey="lblMenuNameResource1" />
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <div class="ContentTextalign">
                        <asp:TextBox ID="txtMenuName" runat="server" Width="250px" MaxLength="200" CssClass="input-text" meta:resourcekey="txtMenuNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rqvMenuName" ValidationGroup="validation" ControlToValidate="txtMenuName"
                            CssClass="error" ErrorMessage="Menu Name is required" Display="None" meta:resourcekey="rqvMenuNameResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveMenuName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvMenuName" Enabled="True" />
                    </div>
                </div>

                <div id="urlmenuname" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblURLMenuName" runat="server" Text="Page Name(Appear in URL)" meta:resourcekey="lblURLMenuNameResource1" />
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <div class="ContentTextalign">
                        <asp:TextBox ID="txtURLMenuName" runat="server" Width="250px" MaxLength="200" CssClass="input-text" meta:resourcekey="txtURLMenuNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rqvPageName" ValidationGroup="validation" ControlToValidate="txtURLMenuName"
                            CssClass="error" ForeColor="Red" ErrorMessage="Please Enter Page Name" Display="None" meta:resourcekey="rqvPageNameResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqvePageName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvPageName" Enabled="True" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="flttxtURLMenuName" runat="server" Enabled="True" TargetControlID="txtURLMenuName" ValidChars=".abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-"></ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                </div>


                <div id="pagetitle" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblPageTitle" runat="server" Text="Page Title" meta:resourcekey="lblPageTitleResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ContentTextalign">
                        <asp:TextBox ID="txtPageTitle" runat="server" Width="250px" MaxLength="200" CssClass="input-text" meta:resourcekey="txtPageTitleResource1"></asp:TextBox>
                    </div>
                </div>


                <div id="pagekeyword" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblPageKeyword" runat="server" Text='Page Keyword' meta:resourcekey="lblPageKeywordResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <div class="ContentTextalign">
                        <asp:TextBox ID="txtPageKeyword" runat="server" Width="250px" MaxLength="300" TextMode="MultiLine"
                            CssClass="input-text-multiline" meta:resourcekey="txtPageKeywordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RqvPageKeyword" ValidationGroup="validation" ControlToValidate="txtPageKeyword"
                            CssClass="error" Display="None" ErrorMessage="Page Keyword is required!" meta:resourcekey="RqvPageKeywordResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="RqvePageKeyword" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="RqvPageKeyword" Enabled="True" />
                    </div>
                </div>

                <div id="description" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblPageDescription" runat="server" Text='Page Description' meta:resourcekey="lblPageDescriptionResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span>
                    </label>
                    <div class="ContentTextalign">
                        <asp:TextBox ID="txtPageDescription" runat="server" Width="250px" MaxLength="500"
                            TextMode="MultiLine" CssClass="input-text-multiline" meta:resourcekey="txtPageDescriptionResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RqvPageDescription" ValidationGroup="validation" ControlToValidate="txtPageDescription"
                            CssClass="error" Display="None" ErrorMessage="Page Description is required!" meta:resourcekey="RqvPageDescriptionResource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="RqvePageDescription" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="RqvPageDescription" Enabled="True" />
                    </div>
                </div>


                <%--<div id="parentpage" class="parsonal_textfild" runat="server" style="margin-bottom: 33px;">
                    <label>
                        <asp:Label ID="lblParentPage" runat="server" Text="Parent Page" meta:resourcekey="lblParentPageResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="parsonal_select">
                        <asp:DropDownList ID="ddlParentPage" runat="server" meta:resourcekey="ddlParentPageResource1">
                        </asp:DropDownList>
                    </div>
                </div>--%>

                <div id="status" class="parsonal_textfild" runat="server" style="margin-bottom: 25px;">
                    <label>
                        <asp:Label ID="lblStatus" runat="server" Text="Is Active?" meta:resourcekey="lblStatusResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>

                    <asp:CheckBox ID="chkStatus" runat="server" Checked="true" meta:resourcekey="chkStatusResource1" />

                </div>

                <div id="Rolechks" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblRole" runat="server" Text="Role" meta:resourcekey="lblRoleResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>

                    <asp:CheckBoxList ID="chklstRole" runat="server" RepeatDirection="Horizontal" meta:resourcekey="chklstRoleResource1" CssClass="chkwidth">
                        <asp:ListItem Value="P" meta:resourcekey="ListItemResource1">Patient</asp:ListItem>
                        <asp:ListItem Value="D" meta:resourcekey="ListItemResource2">Doctor</asp:ListItem>
                        <asp:ListItem Value="A" meta:resourcekey="ListItemResource3">Public</asp:ListItem>
                    </asp:CheckBoxList>

                </div>


                <div id="pagecontent" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblPageContent" runat="server" Text="Page Content" meta:resourcekey="lblPageContentResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="CommonTexalign">
                        <UC:UCHTMLEditor ID="ucHTMLEditorControl" runat="server" Mode="bandbyidorname" />
                    </div>
                </div>
            </div>
        </div>

        <div class="bottom_btn tpadd alignright" style="width: 268px;">
            <span class="blue_btn">
                <asp:Button ID="btnSubmit" runat="server" Text="Save" ValidationGroup="validation" OnClick="btnSubmit_Click" meta:resourcekey="btnSubmitResource1" />
            </span><span class="dark_btn">
                <input type="reset" tabindex="7" title='<%= this.GetLocalResourceObject("Reset") %>' value='<%= this.GetLocalResourceObject("Reset") %>' />
            </span>
        </div>

    </div>
</asp:Content>
