<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListContactUs.aspx.cs" Inherits="_4eOrtho.Admin.ListContactUs" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>--%>
    <script src="../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
    <link href="../Scripts/Colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var iscolorboxCloseButton;
        function pageLoad() {
            $(document).ready(function () {
                $(".inline").colorbox({
                    inline: true,
                    width: "50%",
                    height: "80%",
                    onOpen: function () {
                        iscolorboxCloseButton = true;
                    },
                    onClosed: function () {
                        if (!iscolorboxCloseButton) {
                            var id = $(this)[0].id;
                            $('#Form1').find('.detail' + id).click();
                        }
                    }
                });
            }).appendTo('form');
        }
        function DetailsClick() {
            iscolorboxCloseButton = false;
            $.colorbox.close();
        }

    </script>
    <style type="text/css">
        .response td {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="UpcontactUs" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>
    <div id="container" class="cf">
        <div class="page_title">
            <h2 class="padd">
                <asp:Label ID="lblHeader" Text="Contact Us List" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
            <div id="divMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
            </div>
        </div>
        <div class="widecolumn">
            <div class="personal_box alignleft">
                <table class="alignleft">
                    <tr>
                        <td align="center" valign="middle">
                            <span class="blue_btn_small">
                                <%-- <asp:Button ID="btnAddNew" runat="server" Text="Add New" PostBackUrl="~/Admin/AddEditClientTestimonial.aspx"></asp:Button>--%>
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
                                        <asp:DropDownList ID="ddlContactUs" runat="server">
                                            <asp:ListItem Value="0" Text="-- Select Search Type --" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                            <asp:ListItem Value="Name" Text="Name" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                            <asp:ListItem Value="Email" Text="Email" meta:resourcekey="ListItemResource3"></asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                            <td align="center" valign="middle">
                                <div class="parsonal_textfildLarge">
                                    <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" Width="170px"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rqvSearchVal" runat="server" Display="None" ValidationGroup="searchValidation" ControlToValidate="txtSearchVal" ErrorMessage="Please enter atleast one Search Value." 
                                        meta:resourcekey="rqvSearchValResource1"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveSearchVal" runat="server" CssClass="customCalloutStyle"
                                        TargetControlID="rqvSearchVal" Enabled="True" />--%>
                                </div>
                            </td>
                            <td align="center" valign="middle">
                                <span class="dark_btn_small">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="searchValidation" OnClick="btnSearch_Click"
                                        meta:resourcekey="btnSearchResource1"></asp:Button>
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
                                <asp:ListView ID="lvContactUs" DataSourceID="odsContactUs" runat="server" OnPreRender="lvContactUs_PreRender" OnItemCommand="lvContactUs_ItemCommand">
                                    <LayoutTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <%--<td align="left" valign="middle" class="equip" width="12%">
                                                    <asp:LinkButton ID="lnkContactId" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="ContactId" Text="ContactId" meta:resourcekey="lnkContactIdResource1"></asp:LinkButton>
                                                </td>--%>
                                                <td align="left" valign="middle" class="equip" width="30%">
                                                    <asp:LinkButton ID="lnkName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="Name" Text="Name" meta:resourcekey="lnkNameResource1"></asp:LinkButton>
                                                </td>
                                                <td align="left" valign="middle" class="equip" width="30%">
                                                    <asp:LinkButton ID="lnkEmail" runat="server" CommandArgument="Email" OnCommand="Custom_Command"
                                                        CommandName="CustomSort" Text="Email" meta:resourcekey="lnkEmailResource1"></asp:LinkButton>
                                                </td>
                                                <td align="left" valign="middle" class="equip" width="60%">
                                                    <asp:Label ID="lnkSubject" runat="server" Text="Subject" meta:resourcekey="lnkSubjectResource1"></asp:Label>
                                                </td>
                                                <td align="left" valign="middle" class="equip" width="60%">
                                                    <asp:Label ID="lblDetails" runat="server" Text="Details" meta:resourcekey="lnkDetailsResource1"></asp:Label>
                                                </td>
                                                <td align="left" valign="middle" class="equip" width="60%">
                                                    <asp:Label ID="lblIsResponded" runat="server" Text="Is Responded" meta:resourcekey="lnkIsResponsibleResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                            <tr class="equip-paging">
                                                <td colspan="5" align="right">
                                                    <asp:DataPager ID="dpContactUsPaging" runat="server" PagedControlID="lvContactUs">
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
                                            <%-- <td align="left" valign="top" class="equipbg">
                                                <%# Eval("ContactId")%>
                                            </td>--%>
                                            <td align="left" valign="top" class="equipbg">
                                                <%# Eval("Name")%>
                                            </td>
                                            <td align="left" valign="top" class="equipbg">
                                                <%# Eval("Email") %>
                                            </td>
                                            <td align="left" valign="top" class="equipbg">
                                                <%# Eval("Subject") %>
                                            </td>
                                            <td align="center" valign="middle" class="equipbg">
                                                <a class='inline' href='<%# "#div" + Eval("ContactId") %>' id='<%# Eval("ContactId") %>'>
                                                    <asp:Image ID="imgDetails" runat="server" ImageUrl="images/bgi/view.png" AlternateText="Details"
                                                        ToolTip="Details" />
                                                </a>
                                                <asp:ImageButton ID="hypRespond" runat="server" CommandName="responded" CommandArgument='<%# Eval("ContactId") %>'
                                                    CssClass='<%# "detail" + Eval("ContactId") %>' Style="display: none;" />
                                                <div style="display: none;">
                                                    <div id='<%# "div" + Eval("ContactId") %>' style="padding: 10px;">
                                                        <div class="parsonal_textfild">
                                                            <label>
                                                                <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                                                <span class="alignright">:</span></label>
                                                            <label>
                                                                <%#Eval("Name") %>
                                                            </label>
                                                        </div>
                                                        <div class="clear"></div>
                                                        <div class="parsonal_textfild">
                                                            <label>
                                                                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                                                                <span class="alignright">:</span></label>
                                                            <label>
                                                                <%#Eval("Email") %>
                                                            </label>
                                                        </div>
                                                        <div class="clear"></div>
                                                        <div class="parsonal_textfild">
                                                            <label>
                                                                <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label>
                                                                <span class="alignright">:</span></label>
                                                            <label>
                                                                <%#Eval("Mobile") %>
                                                            </label>
                                                        </div>
                                                        <div class="clear"></div>
                                                        <div class="parsonal_textfild">
                                                            <label>
                                                                <asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label>
                                                                <span class="alignright">:</span></label>
                                                            <label>
                                                                <%#Eval("CountryName") %>
                                                            </label>
                                                        </div>
                                                        <div class="clear"></div>
                                                        <div class="parsonal_textfild">
                                                            <label>
                                                                <asp:Label ID="lblState" runat="server" Text="State"></asp:Label>
                                                                <span class="alignright">:</span></label>
                                                            <label>
                                                                <%#Eval("StateName") %>
                                                            </label>
                                                        </div>
                                                        <div class="clear"></div>
                                                        <div class="parsonal_textfild">
                                                            <label>
                                                                <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                                                                <span class="alignright">:</span></label>
                                                            <label>
                                                                <%#Eval("City") %>
                                                            </label>
                                                        </div>
                                                        <div class="clear"></div>
                                                        <div class="parsonal_textfild">
                                                            <label>
                                                                <asp:Label ID="lblSubject" runat="server" Text="Subject"></asp:Label>
                                                                <span class="alignright">:</span></label>
                                                            <label>
                                                                <%#Eval("Subject") %>
                                                            </label>
                                                        </div>
                                                        <div class="clear"></div>
                                                        <div class="parsonal_textfild">
                                                            <label>
                                                                <asp:Label ID="lblComment" runat="server" Text="Comment"></asp:Label>
                                                                <span class="alignright">:</span></label>
                                                            <label style="width: 380px; text-align: justify">
                                                                <%#Eval("Comment") %>
                                                            </label>
                                                        </div>
                                                        <div class="clear"></div>
                                                        <div class="parsonal_textfild">
                                                            <label>
                                                                <asp:Label ID="lblResponse" runat="server" Text="Mark as Responded"></asp:Label>
                                                                <span class="alignright">:</span></label>
                                                            <label>
                                                                <asp:CheckBox ID="chkIsResponded" runat="server" />
                                                                <%--Checked='<%# Eval("IsResponded")==null?true:Eval("IsResponded") %>'--%>
                                                            </label>
                                                        </div>
                                                        <div class="clear"></div>
                                                        <div class="alignright" style="padding-bottom: 10px;">
                                                            <span class="blue_btn_small">
                                                                <asp:Button runat="server" ID="btnResponded" Text="Save"
                                                                    OnClientClick='<%# "DetailsClick();" %>' />
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td align="center" valign="top" class="equipbg">
                                                <asp:Image ID="imgResponded" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsResponded"))?"Images/icon-active.gif":"Images/icon-inactive.gif" %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <%--<td align="left" valign="middle" class="equip" width="10%">
                                                    <asp:Label ID="lblContactId" runat="server" Text="ContactId" meta:resourcekey="lblContactIdResource1"></asp:Label>
                                                </td>--%>
                                                <td align="left" valign="middle" class="equip" width="10%">
                                                    <asp:Label ID="lblFullName" runat="server" Text="Name" meta:resourcekey="lblFullNameResource1"></asp:Label>
                                                </td>

                                                <td align="left" valign="middle" class="equip" width="10%">
                                                    <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                </td>
                                                <td align="left" valign="middle" class="equip" width="10%">
                                                    <asp:Label ID="lblSubject" runat="server" Text="Subject" meta:resourcekey="lblSubjectResource1"></asp:Label>
                                                </td>
                                                <td align="left" valign="middle" class="equip" width="60%">
                                                    <asp:Label ID="lblDetails" runat="server" Text="Details"></asp:Label>
                                                </td>
                                                <td align="left" valign="middle" class="equip" width="60%">
                                                    <asp:Label ID="lblIsResponded" runat="server" Text="Is Responded"></asp:Label>
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

                                <asp:ObjectDataSource ID="odsContactUs" runat="server" SelectMethod="GetContactUsListBySearch"
                                    SelectCountMethod="GetContactUsDataCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                    TypeName="_4eOrtho.Admin.ListContactUs" OnSelecting="odsContactUs_Selecting"></asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
        </div>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
