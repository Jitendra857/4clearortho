<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListClientTestimonial.aspx.cs" Inherits="_4eOrtho.Admin.ListClientTestimonial" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#' + '<%= ddlClientTestimonial.ClientID %>').focus();
        });
        function searchTypeChange(obj) {
            if (obj.options[obj.selectedIndex].value == "UserType") {
                $('[id$=txtSearchVal]').hide();
                $('#statusDiv').show();
                $('[id$=txtSearchVal]').val('D');
                $('[id$=ddlSearchVal]')[0].selectedIndex = 0;
            }
            else {
                $('[id$=txtSearchVal]').show();
                $('#statusDiv').hide();
                $('[id$=txtSearchVal]').val('');
            }
        }
        function ddlSearchVal(obj) {
            $('[id$=txtSearchVal]').val(obj.options[obj.selectedIndex].value);
        }
        function ActiveMessage(obj) {
            if (confirm("<%= this.GetLocalResourceObject("ActiveMessage") %>"))
                return true;
            else
                return false;
        }
        function DeleteMessage(obj) {
            if (confirm("<%= this.GetLocalResourceObject("DeleteMessage") %>"))
                return true;
            else
                return false;
        }

    </script>

    <asp:UpdatePanel ID="upListClientTestinomial" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label ID="lblHeader" Text="Testimonial List" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
                    <div id="divMsg" runat="server">
                        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                    </div>
                </div>
                <div class="widecolumn">
                    <div class="personal_box alignleft">
                        <table class="alignleft">
                            <tr>
                                <td align="center" valign="middle" style="display: none;">
                                    <span class="blue_btn_small">
                                        <asp:Button ID="btnAddNew" runat="server" Text="Add New" PostBackUrl="~/Admin/AddEditClientTestimonial.aspx" meta:resourcekey="btnAddNewResource1"></asp:Button>
                                    </span>
                                </td>
                                <td align="center" valign="middle">
                                    <span class="blue_btn_small">
                                        <asp:Button ID="btnShowAll" runat="server" Text="Show All"
                                            OnClick="btnShowAll_Click" meta:resourcekey="btnShowAllResource1"></asp:Button>
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                            <table class="alignright">
                                <tr>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <div class="parsonal_selectSmallSearch">
                                                <asp:DropDownList ID="ddlClientTestimonial" runat="server" onchange="searchTypeChange(this)">
                                                    <asp:ListItem Value="0" Text="-- Select Search Type --" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                    <asp:ListItem Value="Name" Text="Name" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                    <asp:ListItem Value="Email" Text="Email" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    <asp:ListItem Value="UserType" Text="User Type" meta:resourcekey="lblUserTypeResource1"></asp:ListItem>
                                                    <asp:ListItem Value="PageContent" Text="Page Content" meta:resourcekey="lblPageContentResource1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfildLarge">
                                            <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" Width="170px"></asp:TextBox>
                                            <div id="statusDiv" class="parsonal_selectSmall" style="display: none;">
                                                <asp:DropDownList ID="ddlSearchVal" runat="server" onchange="ddlSearchVal(obj)">
                                                    <asp:ListItem Value="D" Text="Doctor"></asp:ListItem>
                                                    <asp:ListItem Value="P" Text="Patient"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <%-- <asp:RequiredFieldValidator ID="rqvSearchVal" runat="server" Display="None" ValidationGroup="searchValidation" ControlToValidate="txtSearchVal" ErrorMessage="Please enter atleast one Search Value." meta:resourcekey="rqvSearchValResource1"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveSearchVal" runat="server" CssClass="customCalloutStyle"
                                        TargetControlID="rqvSearchVal" Enabled="True" />--%>
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
                                        <asp:ListView ID="lvClientTestimonial" DataSourceID="odsClientTestimonialList" runat="server" OnItemDataBound="lvClientTestimonial_ItemDataBound" OnPreRender="lvClientTestimonial_PreRender" OnItemCommand="lvClientTestimonial_ItemCommand">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <%-- <td align="left" valign="middle" class="equip" width="15%">
                                                    <asp:LinkButton ID="lnkClientTestimonialId" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="ClientTestimonialId" Text="ClientTestimonialId" meta:resourcekey="lnkClientTestimonialIdResource1"></asp:LinkButton>
                                                </td>--%>
                                                        <td align="left" valign="middle" class="equip" width="20%">
                                                            <asp:LinkButton ID="lnkName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Name" Text="Name" meta:resourcekey="lnkNameResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="20%">
                                                            <asp:LinkButton ID="lnkEmail" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Email" Text="Email" meta:resourcekey="lnkEmailResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="30%">
                                                            <asp:Label ID="lblContent" runat="server" Text="Page Content" meta:resourcekey="lblPageContentResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblUserType" runat="server" Text="User Type" meta:resourcekey="lblUserTypeResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lnkStatus" runat="server" Text="Status" meta:resourcekey="lnkStatusResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Literal ID="ltrAction" runat="server" Text="Action" meta:resourcekey="ltrActionResource1"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="6" align="right">
                                                            <asp:DataPager ID="dpClientTestimonialPaging" runat="server" PagedControlID="lvClientTestimonial">
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
                                                    <%--<td align="left" valign="top" class="equipbg">
                                                <%# Eval("ClientTestimonialId")%>
                                            </td>--%>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("Name")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("Email")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# new string(Eval("PageContent").ToString().Take(48).ToArray()) + ".." %>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("UserType") %>
                                                    </td>

                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:ImageButton ID="imginactive" runat="server" ImageUrl='<%# Eval("IsActive").ToString().Equals("True") ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>'
                                                            CommandName="Active" CommandArgument='<%# Eval("ClientTestimonialId") %>'
                                                            meta:resourcekey="imginactiveResource1" OnClientClick="return ActiveMessage(this);" />
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:HyperLink ID="hypEdit" NavigateUrl='<%# "~/Admin/AddEditClientTestimonial.aspx?id=" + Server.UrlEncode(Eval("ClientTestimonialId").ToString()) %>'
                                                            runat="server" ToolTip="Edit" ImageUrl="Images/edit.png" meta:resourcekey="hypEditResource1"></asp:HyperLink>
                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("ClientTestimonialId") %>'
                                                            ImageUrl="~/Admin/Images/delete.png" ToolTip="Delete" Style="padding-right: 10px;" OnCommand="Custom_Command"
                                                            OnClientClick="return DeleteMessage(this);" meta:resourcekey="imgbtnDeleteResource1" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <%--
                                                <td align="left" valign="middle" class="equip" width="10%">
                                                    <asp:Label ID="lblClientTestimonialId" runat="server" Text="ClientTestimonialId" meta:resourcekey="lblClientTestimonialIdResource1"></asp:Label>
                                                </td>--%>
                                                        <td align="left" valign="middle" class="equip" width="20%">
                                                            <asp:Label ID="lblFullName" runat="server" Text="Name" meta:resourcekey="lblFullNameResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="20%">
                                                            <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                        </td>

                                                        <td align="left" valign="middle" class="equip" width="30%">
                                                            <asp:Label ID="lblUserType" runat="server" Text="User Type" meta:resourcekey="lblUserTypeResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblPageContent" runat="server" Text="Page Content" meta:resourcekey="lblPageContentResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblStatus" runat="server" Text="Status" meta:resourcekey="lblStatusResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource1"></asp:Label>
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

                                        <asp:ObjectDataSource ID="odsClientTestimonialList" runat="server" SelectMethod="GetClientTestimonialListBySearch"
                                            SelectCountMethod="GetClientTestimonialDataCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.Admin.ListClientTestimonial" OnSelecting="odsClientTestimonialList_Selecting"></asp:ObjectDataSource>
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
