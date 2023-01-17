<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListTakeCourse.aspx.cs" Inherits="_4eOrtho.Admin.ListTakeCourse" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="cf">
        <div class="page_title">
            <h2 class="padd">
                <asp:Label ID="lblHeader" Text="Take Course List" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
            <div id="divMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
            </div>
        </div>
        <div class="widecolumn">
            <div class="personal_box alignleft">
                <table class="alignleft">
                    <tr>
                        <td align="center" valign="middle">
                            <span class="blue_btn_small"></span>
                        </td>
                    </tr>
                </table>
                <div class="clear">
                </div>
                <div class="list-data">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" valign="top" class="rgt">
                                <asp:ListView ID="lvTakeCourse" DataSourceID="odsTakeCourse" runat="server" OnPreRender="lvTakeCourse_PreRender">
                                    <LayoutTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="left" valign="middle" class="equip" width="20%">
                                                    <asp:LinkButton ID="lnkDocotorName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="DoctorName" Text="Doctor Name" meta:resourcekey="lnkDocotorNameResource1"></asp:LinkButton>
                                                </td>
                                                <td align="left" valign="middle" class="equip" width="22%">
                                                    <asp:Label ID="lnkBecomeProvider" runat="server" Text="Provider Email" meta:resourcekey="lnkBecomeProviderResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                            <tr class="equip-paging">
                                                <td colspan="5" align="right">
                                                    <asp:DataPager ID="dpTakeCourse" runat="server" PagedControlID="lvTakeCourse">
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
                                                <%# Eval("DoctorName")%>
                                            </td>
                                            <td align="left" valign="top" class="equipbg">
                                                <%# Eval("EmailId")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="left" valign="middle" class="equip" width="10%">
                                                    <asp:Label ID="lblFullName" runat="server" Text="Name" meta:resourcekey="lblFullNameResource1"></asp:Label>
                                                </td>
                                                <td align="left" valign="middle" class="equip" width="10%">
                                                    <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="middle" class="equipbg" colspan="10">
                                                    <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:ListView>

                                <asp:ObjectDataSource ID="odsTakeCourse" runat="server" SelectMethod="GetTakeCourseDetails"
                                    SelectCountMethod="GetTakeCourseDetailsCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                    TypeName="_4eOrtho.Admin.ListTakeCourse" OnSelecting="odsTakeCourse_Selecting">
                                    <SelectParameters>
                                        <asp:Parameter Name="sortField" Type="String" />
                                        <asp:Parameter Name="sortDirection" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
