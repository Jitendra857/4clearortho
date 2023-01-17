<%@ Page Title="Add AAD Course" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddCource.aspx.cs" Inherits="_4eOrtho.Admin.AddCource" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="cf">
        <div class="title">
            <h2>
                <asp:Label ID="lblHeader" runat="server" Text="Course" meta:resourcekey="lblHeaderResource1"></asp:Label>
            </h2>
        </div>
        <asp:UpdatePanel ID="upRegister" runat="server">
            <ContentTemplate>
                <div class="main_right_cont minheigh">
                    <%--<div class="title">
                    <h2>
                        <asp:Label ID="lblHeader" runat="server" Text="Cource" meta:resourcekey="lblHeaderResource1"></asp:Label>
                    </h2>
                </div>--%>
                    <div class="widecolumn">
                        <div class="personal_box alignleft">
                            <div id="divMsg" runat="server" style="width: 563px;">
                                <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                            </div>
                            <div class="parsonal_textfild">
                                <div class="parsonal_textfild">
                                    <label>
                                        <asp:Label ID="lblConfirmPassword" runat="server" Text="Category" meta:resourcekey="lblConfirmPasswordResource1"></asp:Label>
                                        <span class="alignright">:<span class="asteriskclass">*</span></span>
                                    </label>
                                    <asp:DropDownList runat="server" ID="ddlcource" OnTextChanged="ddlcource_TextChanged" AutoPostBack="True" meta:resourcekey="ddlcourceResource1"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="reqCource" ControlToValidate="ddlcource" Display="None" ErrorMessage="Please Select Cource" InitialValue="0" ValidationGroup="validation" meta:resourcekey="reqCourceResource1"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValreqCource" Enabled="True" CssClass="customCalloutStyle" TargetControlID="reqCource"></ajaxToolkit:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="parsonal_textfild">
                                <div class="parsonal_textfild">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Course" meta:resourcekey="Label2Resource1"></asp:Label>
                                        <span class="alignright">:<span class="asteriskclass">*</span></span>
                                    </label>
                                    <asp:DropDownList runat="server" ID="DDl_SubCource" OnTextChanged="DDl_SubCource_TextChanged" AutoPostBack="True" meta:resourcekey="DDl_SubCourceResource1"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="ReqDDl_SubCource" ControlToValidate="DDl_SubCource" Display="None" ErrorMessage="Please Select Sub Cource" InitialValue="0" ValidationGroup="validation" meta:resourcekey="ReqDDl_SubCourceResource1"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValReqDDl_SubCource" Enabled="True" CssClass="customCalloutStyle" TargetControlID="ReqDDl_SubCource"></ajaxToolkit:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="parsonal_textfild">
                                <div class="parsonal_textfild">
                                    <label>
                                        <asp:Label ID="lblLink" runat="server" Text="Link" meta:resourcekey="lblLinkResource1"></asp:Label>
                                        <span class="alignright">:<span class="asteriskclass">*</span></span>
                                    </label>
                                    <asp:TextBox runat="server" ID="txtAADCourcelink" Style="width: 700px" meta:resourcekey="txtAADCourcelinkResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="REqtxtAADCourcelink" ControlToValidate="txtAADCourcelink" Display="None" ErrorMessage="Please Enter Link." ValidationGroup="validation" meta:resourcekey="REqtxtAADCourcelinkResource1"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender1" Enabled="True" CssClass="customCalloutStyle" TargetControlID="REqtxtAADCourcelink"></ajaxToolkit:ValidatorCalloutExtender>

                                </div>
                            </div>

                            <%--<div class="date2">
                                <div class="date_cont">
                                    <div class="date_cont_right">
                                        <div class="supply-button3">
                                            <asp:Button ID="btnSubmit" class="blue_btn" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="validation" OnClick="btnSubmit_Click"
                                                TabIndex="14" meta:resourcekey="btnSubmitResource1" />
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                    <div class="clear">
                </div>
                     <div>
                    <span class="blue_btn">
                        <asp:Button ID="btnSubmit" class="blue_btn" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="validation" OnClick="btnSubmit_Click"
                                                TabIndex="14" meta:resourcekey="btnSubmitResource1" />
                    </span>
                         <%--<span class="dark_btn">
                        <input type="reset" tabindex="4" title='<%= this.GetLocalResourceObject("Reset") %>' value='<%= this.GetLocalResourceObject("Reset") %>' />                        
                    </span>--%>
                </div>
                </div>
                <asp:HiddenField ID="hdnPassword" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
