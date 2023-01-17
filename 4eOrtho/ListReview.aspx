<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="ListReview.aspx.cs" Inherits="_4eOrtho.ListReview" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upReview" runat="server">
        <ContentTemplate>
            <div class="rightbar">
                <div class="main_right_cont" style="width: 100%;">
                    <div class="title">
                        <h2>
                            <asp:Label runat="server" ID="lblProductList" Text="Review List" meta:resourcekey="lblProductListResource1"></asp:Label>
                        </h2>
                    </div>
                    <div class="date2">
                        <div class="date_cont" style="width: 46%;">
                            <div class="date_cont_right">
                                <div class="Sort_by">
                                    <div class="product_dropdown">
                                        <asp:DropDownList ID="ddlReview" runat="server" CssClass="low_high_search" meta:resourcekey="ddlReviewResource1">
                                            <asp:ListItem Value="0" Text="Select Search Type" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                            <asp:ListItem Value="PatientName" Text="Patient Name" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                            <asp:ListItem Value="ReviewContent" Text="Review Content" meta:resourcekey="ListItemResource3"></asp:ListItem>
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
                                <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="searchValidation" CssClass="btn" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1"></asp:Button>
                            </div>
                        </div>
                    </div>
                    <div class="entry_form">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grid_table">
                            <tbody>
                                <asp:ListView ID="lvReview" runat="server" DataKeyNames="ReviewId" DataSourceID="odsReview"
                                    OnPreRender="lvReview_PreRender" OnItemDataBound="lvReview_ItemDataBound">
                                    <LayoutTemplate>
                                        <tr>
                                            <td style="width: 200px">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkSortPatient" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                            CommandArgument="PatientName" Text="Patient Name" meta:resourcekey="lnkSortPatientResource1" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblReview" Text="Review" meta:resourcekey="lblReviewResource1"></asp:Label></span>
                                                    </span>
                                                </div>
                                            </td>

                                            <td style="width: 50px">
                                                <div class="topadd_f flex" style="text-align: center; width: 100%;">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblStatus" Text="Status" meta:resourcekey="lblStatusResource2"></asp:Label></span>
                                                </div>
                                            </td>
                                            <td style="width: 50px">
                                                <div class="topadd_f flex" style="text-align: center; width: 100%;">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblEdit" Text="Edit" meta:resourcekey="lblEditResource1"></asp:Label></span>
                                                </div>
                                            </td>
                                            <td style="width: 70px">
                                                <div class="topadd_f flex" style="text-align: center; width: 100%;">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblDelete" Text="Delete" meta:resourcekey="lblDeleteResource2"></asp:Label></span>
                                                </div>
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                        <tr>
                                            <td align="right" colspan="5" class="datapager">
                                                <asp:DataPager ID="lvReviewDataPager" runat="server" PagedControlID="lvReview">
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
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("PatientName")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="whitetext">
                                                        <span title='<%# Eval("ReviewContent")%>'>
                                                            <%# Convert.ToString(Eval("ReviewContent")).Length > 30 ? Convert.ToString(Eval("ReviewContent")).Substring(0,30) + "..." : Convert.ToString(Eval("ReviewContent"))%>
                                                        </span>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark drow8" : "grenchk light drow8" %>' id="flex">
                                                    <div class="winicons" style="text-align: center; width: 100%;">
                                                        <div class="editicon grid-image">
                                                            <asp:Image ID="imgStatus" runat="server" ImageUrl="Content/images/icon_img10.png" meta:resourcekey="imgStatusResource2" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark drow8" : "grenchk light drow8" %>' id="flex">
                                                    <div class="winicons" style="text-align: center; width: 100%;">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="hypEdit" ToolTip="Edit" runat="server" ImageUrl="Content/images/icon_img10.png"
                                                                meta:resourcekey="hypEditResource1"
                                                                CommandName="CustomEdit" CommandArgument='<%# Eval("ReviewId") %>' OnCommand="Custom_Command" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark drow8" : "grenchk light drow8" %>' id="flex">
                                                    <div class="winicons" style="text-align: center; width: 100%;">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="Content/images/icon_img09.png"
                                                                OnClientClick="return DeleteMessage(this);"
                                                                CommandName="CustomDelete" CommandArgument='<%# Eval("ReviewId") %>' OnCommand="Custom_Command" meta:resourcekey="imgbtnDeleteResource2" />
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
                                                        <asp:Label ID="lblPatientName" runat="server" Text="Patient Name" meta:resourcekey="lblPatientNameResource1"></asp:Label>
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
                                                        <asp:Label runat="server" ID="lblDelete" Text="Delete" meta:resourcekey="lblDeleteResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle" class="equipbg" colspan="5">
                                                <div class="grenchk dark" id="flex">
                                                    <div class="whitetext" style="width: 100%; text-align: center;">
                                                        <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                                <asp:ObjectDataSource ID="odsReview" runat="server" SelectMethod="GetAllReviewDetails"
                                    SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                    TypeName="_4eOrtho.ListReview" OnSelecting="odsReview_Selecting">
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
    <script type="text/javascript">
        function pageLoad() {
            $('[id$=ddlReview]').selectbox();
        }
        function DeleteMessage(obj) {
            if (confirm("<%= this.GetLocalResourceObject("DeleteMessage") %>"))
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
