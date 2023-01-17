<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="ListNewCase.aspx.cs" Inherits="_4eOrtho.ListNewCase" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Styles/screen.css" type="text/css" media="screen" />
    <style type="text/css">
        .auto-style1 {
            width: 72px;
        }
        .bothsection .right_section {
    float: right;
    width: 239px;
}   
        .bothsection .left_section {
    float: left;
    width: 658px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upNewCase" runat="server">
        <ContentTemplate>
            <div class="rightbar">
                <div class="main_right_cont" style="width: 100%;">
                    <div class="title">
                        <div class="supply-button2 back">
                            <asp:Button ID="btnAddNewCase" runat="server" Text="Add Patient/New Case" ToolTip="Add Patient/New Case" OnClick="btnAddNewCase_Click" meta:resourcekey="btnAddNewCaseResource1" />
                        </div>
                        <h2>
                            <asp:Label runat="server" ID="lblProductList" Text="Patient / Case List" meta:resourcekey="lblProductListResource1"></asp:Label>
                        </h2>
                    </div>
                    <asp:Panel ID="pnlSearch" DefaultButton="btnSearch" runat="server" meta:resourcekey="pnlSearchResource1">
                        <div class="date2">
                            <div class="date_cont" style="width: 46%;">
                                <div class="date_cont_right">
                                    <div class="Sort_by">
                                        <div class="product_dropdown">
                                            <asp:DropDownList ID="ddlFilter" runat="server" CssClass="low_high_search" meta:resourcekey="ddlFilterResource1">
                                                <asp:ListItem Value="0" Text="Select Search Type" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                <asp:ListItem Value="FirstName" Text="First Name" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                <asp:ListItem Value="LastName" Text="Last Name" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                <asp:ListItem Value="Email" Text="Email" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="parsonal_textfild" style="padding: 0; float: right; width: 54%;">
                                <div class="date_cont_right">
                                    <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" meta:resourcekey="txtSearchValResource1"></asp:TextBox>
                                </div>
                                <div class="Search_button" style="margin-left: 0px;">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ToolTip="Search" ValidationGroup="searchValidation" CssClass="btn" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="entry_form">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grid_table">
                            <tbody>
                                <asp:ListView ID="lvNewCase" runat="server" OnItemDataBound="lvNewCase_ItemDataBound" DataSourceID="odsNewCase" DataKeyNames="CaseId">
                                    <LayoutTemplate>
                                        <tr>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblCaseNo" runat="server" Text="Case No." meta:resourcekey="lblCaseNoResource2"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkCreatedDate" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                            CommandArgument="CreatedDate" Text="Created Date" meta:resourcekey="lblCreatedDateResource2" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkFirstName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                            CommandArgument="FirstName" Text="First Name" meta:resourcekey="lnkFirstNameResource2" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkLastName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                            CommandArgument="LastName" Text="Last Name" meta:resourcekey="lnkLastNameResource2" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto; text-align: center" colspan="6">
                                                <div class="topadd_f flex">
                                                    <span class="one">&nbsp;                                                
                                                        <asp:Label ID="lblAction" runat="server" meta:resourcekey="Action"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                        <tr>
                                            <td align="right" colspan="11" class="datapager">
                                                <asp:DataPager ID="lvNewCaseDataPager" runat="server" PagedControlID="lvNewCase">
                                                    <Fields>
                                                        <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page" NumericButtonCssClass="button-page" meta:resourcekey="NumericPagerFieldResource1" />
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                        </tr>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%#Eval("CaseNo") %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%#Convert.ToDateTime(Eval("CreatedDate")).ToString("MM/dd/yyyy") %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("FirstName") %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%#Eval("LastName") %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex" style="text-align: right;">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="imgbtnReceived" ToolTip="Edit" runat="server" ImageUrl="Content/images/submitreceived.png" Width="20"
                                                                CommandName="RECEIVED" CommandArgument='<%# Eval("CaseId") %>' OnCommand="Custom_Command" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex" style="text-align: center;">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="hypEdit" runat="server" ImageUrl='<%# Eval("PaymentId") != null && Convert.ToInt64(Eval("PaymentId")) > 0 ? "Content/images/view.png" : "Content/images/icon_img10.png" %>'
                                                                CommandName="CustomEdit" CommandArgument='<%# Eval("CaseId") %>' OnCommand="Custom_Command" />
                                                            <asp:HiddenField ID="hdnIsPayment" runat="server" Value="false" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex" style="text-align: left;">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image" style="width: auto;">
                                                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="Content/images/print.png"
                                                                ToolTip="Print" CommandName="print" Visible='<%# Eval("PaymentId") != null && Convert.ToInt64(Eval("PaymentId")) > 0 %>'
                                                                CommandArgument='<%# Eval("CaseId") %>' OnCommand="Custom_Command" meta:resourcekey="btnPrintResource2" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex" style="text-align: left;">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image" style="width: auto;">
                                                            <asp:ImageButton ID="btnShareCase" runat="server" ImageUrl="Content/images/share.png" Style='<%# Convert.ToBoolean(Eval("IsShared")) ? "filter: grayscale(100%)": "filter: none" %>'
                                                                ToolTip="Share Case" CommandName="share" Visible='<%# Eval("CaseId") != null && Convert.ToInt64(Eval("CaseId")) > 0 %>'
                                                                CommandArgument='<%# Eval("CaseId") %>' OnClientClick='return ShareValidationMessage(this);' data-isShared='<%# Eval("IsShared") %>'
                                                                data-shareddoctoremailid='<%# Eval("SharedDoctorEmailId") %>' OnCommand="Custom_Command" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                             <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex" style="text-align: left;">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image" style="width: auto;">

                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Content/images/add.png" Style='<%# Convert.ToBoolean(Eval("IsShared")) ? "filter: grayscale(100%)": "filter: none" %>'
                                                                ToolTip="Create Stage" CommandName="Stage" Visible='<%# Eval("CaseId") != null && Convert.ToInt64(Eval("CaseId")) > 0 %>'
                                                                CommandArgument='<%# Eval("CaseId") %>'  data-isShared='<%# Eval("IsShared") %>'
                                                                data-shareddoctoremailid='<%# Eval("SharedDoctorEmailId") %>' OnCommand="Custom_Command" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                              <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex" style="text-align: left;">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image" style="width: auto;">

                                                            <asp:ImageButton ID="ImageButton2" runat="server" Height="26px" ImageUrl="Content/images/uploadfile.png" Style='<%# Convert.ToBoolean(Eval("IsShared")) ? "filter: grayscale(100%)": "filter: none" %>'
                                                                ToolTip="Upload Animation" CommandName="File" Visible='<%# Eval("CaseId") != null && Convert.ToInt64(Eval("CaseId")) > 0 %>'
                                                                CommandArgument='<%# Eval("CaseId") %>'  data-isShared='<%# Eval("IsShared") %>'
                                                                data-shareddoctoremailid='<%# Eval("SharedDoctorEmailId") %>' OnCommand="Custom_Command" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblCaseNo" runat="server" Text="Case No." meta:resourcekey="lblCaseNoResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblCreatedDate" runat="server" Text="Created Date" meta:resourcekey="lblCreatedDateResource2"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lnkFirstName" runat="server" Text="First Name" meta:resourcekey="lnkFirstNameResource1" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lnkLastName" runat="server" Text="Last Name" meta:resourcekey="lnkLastNameResource1" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle" class="equipbg" colspan="10">
                                                <div class="grenchk dark" id="flex">
                                                    <div class="whitetext" style="width: 100%; text-align: center;">
                                                        <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                                <asp:ObjectDataSource ID="odsNewCase" runat="server" SelectMethod="GetAllNewCase"
                                    SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                    TypeName="_4eOrtho.ListNewCase" OnSelecting="odsNewCase_Selecting">
                                    <SelectParameters>
                                        <asp:Parameter Name="sortField" Type="String" />
                                        <asp:Parameter Name="sortDirection" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </tbody>
                        </table>
                    </div>
                    <div class="title">
                        <h2>
                            <asp:Label ID="lblCompletedCaseList" runat="server" Text="Completed Case List" meta:resourcekey="lblCompletedCaseListResource1"></asp:Label>
                        </h2>
                    </div>
                    <div class="entry_form">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grid_table">
                            <tbody>
                                <asp:ListView ID="lvCompletedCaseList" runat="server" DataSourceID="odsCompletedCaseList" DataKeyNames="CaseId" EnableViewState="false">
                                    <LayoutTemplate>
                                        <tr>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblCaseNo" runat="server" Text="Case No." meta:resourcekey="lblCaseNoResource2"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkCreatedDate" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                            CommandArgument="CreatedDate" Text="Created Date" meta:resourcekey="lblCreatedDateResource2" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkFirstName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                            CommandArgument="FirstName" Text="First Name" meta:resourcekey="lnkFirstNameResource2" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkLastName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                            CommandArgument="LastName" Text="Last Name" meta:resourcekey="lnkLastNameResource2" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto; text-align: center" colspan="2">
                                                <div class="topadd_f flex">
                                                    <span class="one">&nbsp;                                                
                                                        <asp:Label ID="lblAction" runat="server" meta:resourcekey="Action"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                        <tr>
                                            <td align="right" colspan="11" class="datapager">
                                                <asp:DataPager ID="lvNewCaseDataPager" runat="server" PagedControlID="lvCompletedCaseList">
                                                    <Fields>
                                                        <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page" NumericButtonCssClass="button-page" meta:resourcekey="NumericPagerFieldResource1" />
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                        </tr>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%#Eval("CaseNo") %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%#Convert.ToDateTime(Eval("CreatedDate")).ToString("MM/dd/yyyy") %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("FirstName") %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%#Eval("LastName") %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex" style="text-align: center;">
                                                    <div class="winicons" style="width: 100%;">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="imgbtnRework" runat="server" Width="25" Style="margin: -5px;" CommandArgument='<%# Eval("CaseId") %>' CommandName="ReworkCaseCharge" OnCommand="Custom_Command" ImageUrl="~/Content/images/rework.png" meta:resourcekey="imgbtnReworkResource" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex" style="text-align: center;">
                                                    <div class="winicons" style="width: 100%;">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="imgbtnRetainer" runat="server" Width="25" Style="margin: -5px;" CommandArgument='<%# Eval("CaseId") %>' CommandName="RetainerCaseCharge" OnCommand="Custom_Command" ImageUrl="~/Content/images/retainer.png" meta:resourcekey="imgbtnRetainerResource" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblCaseNo" runat="server" Text="Case No." meta:resourcekey="lblCaseNoResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblCreatedDate" runat="server" Text="Created Date" meta:resourcekey="lblCreatedDateResource2"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lnkFirstName" runat="server" Text="First Name" meta:resourcekey="lnkFirstNameResource1" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lnkLastName" runat="server" Text="Last Name" meta:resourcekey="lnkLastNameResource1" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lnkDateofBirth" runat="server" Text="Date Of Birth" meta:resourcekey="lnkDateofBirthResource1" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblGender" runat="server" Text="Gender" meta:resourcekey="lblGenderResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblStatus" Text="Status" meta:resourcekey="lblStatusResource1"></asp:Label></span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <%= this.GetLocalResourceObject("Action.Text") %>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle" class="equipbg" colspan="10">
                                                <div class="grenchk dark" id="flex">
                                                    <div class="whitetext" style="width: 100%; text-align: center;">
                                                        <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                                <asp:ObjectDataSource ID="odsCompletedCaseList" runat="server" SelectMethod="GetAllNewCase"
                                    SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                    TypeName="_4eOrtho.ListNewCase" OnSelecting="odsCompletedCaseList_Selecting">
                                    <SelectParameters>
                                        <asp:Parameter Name="sortField" Type="String" />
                                        <asp:Parameter Name="sortDirection" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvPrint" style="display: none">
        <asp:Panel ID="pnlPrint" runat="server">
            <div>
                <h2>
                    <asp:Label ID="printHeader" Text="Patient Case Details" runat="server"></asp:Label></h2>
            </div>
            <table border="1" cellspacing="0px" cellpadding="10px">
                <tr>
                    <td colspan="2">
                        <div style="font-size: 12px; float: right;">
                            <b>
                                <asp:Label ID="lblCreatedBy" runat="server" Text="Doctor Name:"></asp:Label></b>
                            <asp:Label ID="lblPrintCreated" runat="server"></asp:Label>
                            <br />
                            <b>
                                <asp:Label ID="lblCreatedDate" runat="server" Text="Created Date:"></asp:Label></b>
                            <asp:Label ID="lblPrintcreatedDate" runat="server"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="date_cont">
                            <div class="date_cont_right">
                                <b>
                                    <asp:Label ID="lblCaseNo" runat="server" Text="Case No"></asp:Label>
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
                                    <asp:Label ID="lblPrintDN1" runat="server" Text="Doctor Name"></asp:Label>
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
                                    <asp:Label ID="lblPrintDOB1" runat="server" Text="Date Of Birth"></asp:Label>
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
                                    <asp:Label ID="lblPrintGender1" runat="server" Text="Gender"></asp:Label>
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
                        <div class="date_cont">
                            <div class="date_cont_right ">
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
    <script type="text/javascript">
        function pageLoad() {
            $('[id$=ddlFilter]').selectbox();
        }
        function DeleteMessage(obj) {
            if (confirm("<%= Convert.ToString(this.GetLocalResourceObject("DeleteMessage")) %>"))
                return true;
            else
                return false;
        }
        function ShareValidationMessage(obj) {
            if ($(obj).data('isshared') == "False" && !$(obj).data('shareddoctoremailid')) {
                if (confirm("Are you sure you want to share your case with another doctor?"))
                    return true;
                else
                    return false;
            }
            else {
                alert("You already shared this case with another doctor.");
                return false;
            }
        }
    </script>
</asp:Content>
