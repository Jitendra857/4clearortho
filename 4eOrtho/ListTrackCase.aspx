<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="ListTrackCase.aspx.cs" Inherits="_4eOrtho.ListTrackCase" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upNewCase" runat="server">
        <ContentTemplate>
            <div class="rightbar">
                <div class="main_right_cont" style="width: 100%;">
                    <div class="title">
                        <h2>
                            <asp:Label runat="server" ID="lblTrackCase" Text="Track Case List" meta:resourcekey="lblTrackCaseResource1"></asp:Label>
                        </h2>
                    </div>
                    <asp:Panel ID="pnlSearch" DefaultButton="btnSearch" runat="server" meta:resourcekey="pnlSearchResource1">
                        <div class="date2">
                            <div class="date_cont" style="width: 46%;">
                                <div class="date_cont_right">
                                    <div class="Sort_by">
                                        <asp:DropDownList ID="ddlFilter" runat="server" CssClass="low_high_search" meta:resourcekey="ddlFilterResource1">
                                            <asp:ListItem Value="0" Text="Select Search Type" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                            <asp:ListItem Value="TrackNo" Text="Track No" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                            <asp:ListItem Value="CaseNo" Text="Case No" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="parsonal_textfild" style="padding: 0; float: right; width: 54%">
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
                                <asp:ListView ID="lvTrackCase" runat="server" DataSourceID="odsTrackCase" DataKeyNames="CaseId" OnItemDataBound="lvTrackCase_ItemDataBound">
                                    <LayoutTemplate>
                                        <tr>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lbtnTrackNo" runat="server" Text="Track No"
                                                            CommandArgument="TrackNo" CommandName="CustomSort" OnCommand="Custom_Command" meta:resourcekey="lbtnTrackNoResource1"></asp:LinkButton>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lbtnCaseNo" runat="server" Text="Case No"
                                                            CommandArgument="CaseNo" CommandName="CustomSort" OnCommand="Custom_Command" meta:resourcekey="lbtnCaseNoResource1"></asp:LinkButton>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lbtnPatientName" runat="server" Text="Patient Name"
                                                            CommandArgument="PatientName" CommandName="CustomSort" OnCommand="Custom_Command" meta:resourcekey="lbtnPatientNameResource1"></asp:LinkButton>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lbtnShippingStatus" runat="server" Text="Shipping Detail"
                                                            CommandArgument="ShippingStatus" CommandName="CustomSort" OnCommand="Custom_Command" meta:resourcekey="lbtnShippingStatusResource1"></asp:LinkButton>
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="text-align: center">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblStatus" Text="Status" meta:resourcekey="lblStatusResource2"></asp:Label></span>
                                                </div>
                                            </td>
                                            <td style="text-align: center">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblEdit"><%= this.GetLocalResourceObject("Action") %> </asp:Label></span>
                                                </div>
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                        <tr>
                                            <td align="right" colspan="10" class="datapager">
                                                <asp:DataPager ID="lvTrackCaseDataPager" runat="server" PagedControlID="lvTrackCase">
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
                                                        <%#Eval("TrackNo") %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("CaseNo") %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%#Eval("PatientName") %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <asp:Label ID="lblBindStatus" runat="server" meta:resourcekey="lblBindStatusResource2"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="text-align: center">
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="winicons" style="width: 100%;">
                                                        <div class="editicon grid-image">
                                                            <%--<asp:Image ID="imgStatus" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive"))?"Content/images/icon-active.gif" :"Content/images/icon-inactive.gif" %>' meta:resourcekey="imgStatusResource2" />--%>
                                                            <asp:ImageButton ID="hypReceived" runat="server" Width="20" CommandName="CUSTOMRECEIVED" CommandArgument='<%# Eval("TrackId") %>'
                                                                OnCommand="Custom_Command" ImageUrl="Content/images/submitreceived.png" meta:resourcekey="hypEditResource1" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <%--<div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="hypReceived" runat="server" Width="20" CommandName="CUSTOMRECEIVED" CommandArgument='<%# Eval("TrackId") %>'
                                                                OnCommand="Custom_Command" ImageUrl="Content/images/submitreceived.png" meta:resourcekey="hypEditResource1" />
                                                        </div>
                                                    </div>--%>
                                                    <div class="winicons" style="margin-top: 5px;">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="hypView" runat="server" ImageUrl="Content/images/view.png" CommandName="CustomView" CommandArgument='<%# Eval("CaseId") %>' OnCommand="Custom_Command" meta:resourcekey="hypViewResource1" />
                                                        </div>
                                                    </div>
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="Content/images/print.png" ToolTip="Print" CommandName="print" CommandArgument='<%# Eval("CaseId") %>' OnCommand="Custom_Command" meta:resourcekey="btnPrintResource2" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td style="width: 15%">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblTrackNo" runat="server" Text="Track No." meta:resourcekey="lblTrackNoResource1" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: 15%">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblCaseNo" runat="server" Text="Case No." meta:resourcekey="lblCaseNoResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: 25%">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblLastUpdatedBy" runat="server" Text="Patient Name" meta:resourcekey="lblLastUpdatedByResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: 15%">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblShipingStatus" runat="server" Text="Shipping Status" meta:resourcekey="lblShipingStatusResource1" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: 10%">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblStatus" Text="Status" meta:resourcekey="lblStatusResource1"></asp:Label></span>
                                                </div>
                                            </td>
                                            <td style="width: 10%">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblEdit" Text="Edit" meta:resourcekey="lblEditResource1"></asp:Label></span>
                                                </div>
                                            </td>
                                            <td style="width: 10%">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblDelete" Text="Delete" meta:resourcekey="lblDeleteResource1"></asp:Label></span>
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
                                <asp:ObjectDataSource ID="odsTrackCase" runat="server" SelectMethod="GetAllTrackCaseDetail"
                                    SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                    TypeName="_4eOrtho.ListTrackCase" OnSelecting="odsTrackCase_Selecting">
                                    <SelectParameters>
                                        <asp:Parameter Name="sortBy" Type="String" />
                                        <asp:Parameter Name="sortOrder" Type="String" />
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
            $("#" + "<%= ddlFilter.ClientID %>").selectbox();
        }
    </script>
</asp:Content>
