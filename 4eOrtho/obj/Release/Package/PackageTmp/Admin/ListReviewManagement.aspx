<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListReviewManagement.aspx.cs" Inherits="_4eOrtho.Admin.ListReviewManagement" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#' + '<%= ddlReviewManagmentType.ClientID %>').focus();
        });
    </script>
    <script type="text/javascript">
        function DeleteMessage(obj) {
            if (confirm("<%= this.GetLocalResourceObject("DeleteMessage") %>"))
                return true;
            else
                return false;
        }
    </script>
    <asp:UpdatePanel ID="UpListReviewManagement" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label ID="lblHeader" Text="Review Management" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
                    <div id="divMsg" runat="server">
                        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                    </div>
                </div>
                <div class="widecolumn">
                    <div class="personal_box  alignleft">
                        <table class="alignleft">
                            <tr>
                                <%--<td align="center" valign="middle">
                                    <span class="blue_btn_small">
                                        <asp:Button ID="btnAddNew" runat="server" Text="Add New" PostBackUrl="~/Admin/AddEditRecommendedDentist.aspx"></asp:Button>
                                    </span>
                                </td>--%>
                                <td align="center" valign="middle">
                                    <span class="blue_btn_small">
                                        <asp:Button ID="btnShowAll" runat="server" Text="Show All"
                                            OnClick="btnShowAll_Click" meta:resourcekey="btnShowAllResource1"></asp:Button>
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" meta:resourcekey="pnlSearchResource1">
                            <table class="alignright">
                                <tr>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <div class="parsonal_selectSmallSearch">
                                                <asp:DropDownList ID="ddlReviewManagmentType" runat="server" meta:resourcekey="ddlReviewManagmentTypeResource1">
                                                    <asp:ListItem Value="0" Text="-- Select Search Type --" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                    <asp:ListItem Value="ReviewContent" Text="Review Content" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                    <asp:ListItem Value="PatientName" Text="Patient Name" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    <asp:ListItem Value="DoctorEmail" Text="Doctor Email" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                                    <asp:ListItem Value="PatientEmail" Text="Patient Email" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfildLarge">
                                            <asp:TextBox ID="txtSearchValue" runat="server" MaxLength="50" Width="170px" meta:resourcekey="txtSearchValueResource1"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <span class="dark_btn_small">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="searchValidation" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1"></asp:Button>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div class="clear">
                        </div>
                        <div class="list-data">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left" valign="top" class="rgt">
                                        <asp:ListView ID="lvReview" runat="server" DataSourceID="odsReview" OnPreRender="lvReview_PreRender" OnItemDataBound="lvReview_ItemDataBound">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>

                                                        <td align="left" valign="middle" class="equip" width="30%">
                                                            <asp:LinkButton ID="lnkReviewContent" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="ReviewContent" Text="Review Content" meta:resourcekey="lnkReviewContentResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:LinkButton ID="lnkPatientName" runat="server" CommandArgument="PatientName" OnCommand="Custom_Command"
                                                                CommandName="CustomSort" Text="Patient Name" meta:resourcekey="lnkPatientNameResource1"></asp:LinkButton>
                                                            <%--  <asp:Label runat="server" ID="lnkPatientName" Text="Patient Name" meta:resourcekey="lnkPatientNameResource1"></asp:Label>--%>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="15%">
                                                            <asp:Label runat="server" ID="lblPatientEmail" Text="Patient Email" meta:resourcekey="lblPatientEmailResource2"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="15%">
                                                            <asp:Label runat="server" ID="lblDoctorEmail" Text="Doctor Email" meta:resourcekey="lblDoctorEmailResource2"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label runat="server" ID="lblIsActive" Text="Is Active?" meta:resourcekey="lblIsActiveResource2"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label runat="server" ID="lblAction" Text="Action" meta:resourcekey="lblActionResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="6" align="right">
                                                            <asp:DataPager ID="dpReviewManagementPaging" runat="server" PagedControlID="lvReview">
                                                                <Fields>
                                                                    <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page" NumericButtonCssClass="button-page" meta:resourcekey="NumericPagerFieldResource1" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("ReviewContent")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("PatientName") %>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("PatientEmail") %>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("DoctorEmail") %>
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:Image ID="imgStatus" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>'
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "In-Active" %>' meta:resourcekey="imgStatusResource1" />
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:HyperLink ID="hypEdit" NavigateUrl='<%# "~/Admin/AddEditReviewManagement.aspx?id=" + Server.UrlEncode(Eval("ReviewId").ToString()) %>'
                                                            runat="server" ToolTip="Edit" ImageUrl="Images/edit.png" meta:resourcekey="hypEditResource1"></asp:HyperLink>
                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="CUSTOMDELETE" CommandArgument='<%# Eval("ReviewId") %>'
                                                            ImageUrl="~/Admin/Images/delete.png" ToolTip="Delete" Style="padding-right: 10px;" OnCommand="Custom_Command"
                                                            OnClientClick="return DeleteMessage(this);" meta:resourcekey="imgbtnDeleteResource1" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip" width="30%">
                                                            <asp:Label ID="lblReviewContent" runat="server" Text="Review Content" meta:resourcekey="lblReviewContentResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name" meta:resourcekey="lblPatientNameResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:Label ID="lblPatientEmail" runat="server" Text="Patient Email" meta:resourcekey="lblPatientEmailResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:Label ID="lblDoctorEmail" runat="server" Text="Doctor Email" meta:resourcekey="lblDoctorEmailResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblIsActive" runat="server" Text="Is Active?" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="middle" class="equipbg" colspan="5">
                                                            <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                        <asp:ObjectDataSource ID="odsReview" runat="server" SelectMethod="GetAllReviewDetails"
                                            SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.Admin.ListReviewManagement" OnSelecting="odsReview_Selecting"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </div>

                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
