<%@ Page Title="4ClearOrtho - Patient List" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="ListPatient.aspx.cs" Inherits="_4eOrtho.ListPatient" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upPatient" runat="server">
        <ContentTemplate>
            <div class="rightbar">
                <div class="main_right_cont" style="width: 100%;">
                    <div class="title">
                        <h2>
                            <asp:Label runat="server" ID="lblProductList" Text="Patient List" meta:resourcekey="lblProductListResource1"></asp:Label>                            
                        </h2>
                    </div>
                    <div class="date2">
                        <div class="date_cont" style="width: 46%;">
                            <div class="date_cont_right">
                                <div class="Sort_by">
                                    <div class="product_dropdown">
                                        <asp:DropDownList ID="ddlReview" runat="server" CssClass="low_high_search" meta:resourcekey="ddlReviewResource1">
                                            <asp:ListItem Value="0" Text="Select Search Type" meta:resourcekey="ListItemResource1" ></asp:ListItem>
                                            <asp:ListItem Value="FirstName" Text="First Name" meta:resourcekey="ListItemResource2" ></asp:ListItem>
                                            <asp:ListItem Value="LastName" Text="Last Name" meta:resourcekey="ListItemResource3" ></asp:ListItem>
                                            <asp:ListItem Value="EmailID" Text="Email Id" meta:resourcekey="ListItemResource4" ></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="parsonal_textfild" style="padding: 0; float: right; width: 54%;">
                            <div class="date_cont_right">
                                <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" meta:resourcekey="txtSearchValResource1" ></asp:TextBox>
                            </div>
                            <div class="Search_button" style="margin-left: 0px;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="searchValidation" CssClass="btn" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1"></asp:Button>
                            </div>
                        </div>
                    </div>
                    <div class="entry_form">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grid_table">
                            <tbody>
                                <asp:ListView ID="lvPatient" runat="server" DataKeyNames="PatientId" DataSourceID="odsPatient"
                                    OnPreRender="lvPatient_PreRender" OnItemDataBound="lvPatient_ItemDataBound" EnableViewState="false">
                                    <LayoutTemplate>
                                        <tr>
                                            <td style="width: 200px">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkSortFirstName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                            CommandArgument="FirstName" Text="First Name" meta:resourcekey="lnkSortFirstNameResource1"  />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: 200px">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkSortLastName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                            CommandArgument="LastName" Text="Last Name" meta:resourcekey="lnkSortLastNameResource1" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: 200px">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkSortEmailID" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                            CommandArgument="EmailID" Text="Email Id" meta:resourcekey="lnkSortEmailIDResource1"  />
                                                    </span>
                                                </div>
                                            </td>
                                            <%--<td style="width: 50px">
                                                <div class="topadd_f flex" style="text-align: center; width: 100%;">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblEdit" Text="Edit" meta:resourcekey="lblEditResource1" ></asp:Label></span>
                                                </div>
                                            </td>--%>
                                        </tr>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                        <tr>
                                            <td align="right" colspan="5" class="datapager">
                                                <asp:DataPager ID="lvReviewDataPager" runat="server" PagedControlID="lvPatient">
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
                                                        <%# Eval("FirstName")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("LastName")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("EmailId")%>
                                                    </div>
                                                </div>
                                            </td>
                                            <%--<td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark drow8" : "grenchk light drow8" %>' id="flex">
                                                    <div class="winicons" style="text-align: center; width: 100%;">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="hypEdit" ToolTip="Edit" runat="server" ImageUrl="Content/images/icon_img10.png"
                                                                CommandName="PatientEdit" CommandArgument='<%# Eval("PatientId") + "&"+ Eval("UserID") %>' OnCommand="Custom_Command" meta:resourcekey="hypEditResource1" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblPatientFirstName" runat="server" Text="First Name" meta:resourcekey="lblPatientFirstNameResource1" ></asp:Label>
                                                    </span>
                                                </div>

                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblFirstName" Text="Last Name" meta:resourcekey="lblFirstNameResource1" ></asp:Label></span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label runat="server" ID="lblEmailID" Text="EmailID" meta:resourcekey="lblEmailIDResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle" class="equipbg" colspan="5">
                                                <div class="grenchk dark" id="flex">
                                                    <div class="whitetext" style="width: 100%; text-align: center;">
                                                        <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1" ></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                                <asp:ObjectDataSource ID="odsPatient" runat="server" SelectMethod="GetAllPatientDetails"
                                    SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                    TypeName="_4eOrtho.ListPatient" OnSelecting="odsReview_Selecting">
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
       <%-- function DeleteMessage(obj) {
            if (confirm("<%= this.GetLocalResourceObject("DeleteMessage") %>"))
                return true;
            else
                return false;
        }--%>
    </script>
</asp:Content>
