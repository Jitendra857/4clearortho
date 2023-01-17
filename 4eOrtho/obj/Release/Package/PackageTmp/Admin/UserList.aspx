<%@ Page Title="Admin - User List" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="_4eOrtho.Admin.UserList" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        function pageLoad() {
            HideShowStatus();
        }
        function HideShowStatus() {
            var ddlSearchField = document.getElementById('<%=ddlSearchField.ClientID %>');
            var selectedFilterType = ddlSearchField.options[ddlSearchField.selectedIndex].value;
            if (selectedFilterType == "Status") {
                document.getElementById('<%=txtSearchVal.ClientID %>').style.display = "none";
                document.getElementById('<%=statusDiv.ClientID %>').style.display = "";
            }
            else {
                document.getElementById('<%=txtSearchVal.ClientID %>').style.display = "";
                document.getElementById('<%=statusDiv.ClientID %>').style.display = "none";
            }
        }
        function SearchValidation(objddlSearchField, objtxtSearchVal) {
            var ddlSearchField = jQuery("#" + objddlSearchField);
            var txtSearchVal = jQuery("#" + objtxtSearchVal);
            if (jQuery(ddlSearchField).val() == '0') {
                alert("<%= this.GetLocalResourceObject("Pleaseselectsearchfield") %>");11
                jQuery(ddlSearchField).focus();
                return false;
            }
            if (jQuery(txtSearchVal).css("display") != 'none' && jQuery.trim(jQuery(txtSearchVal).val()) == '') {
                alert("<%= this.GetLocalResourceObject("Pleaseentersearchtext") %>");
                jQuery(txtSearchVal).focus();
                return false;
            }
            return true;
        }

    </script>
    <asp:UpdatePanel ID="upUserList" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label ID="lblHeader" Text="User List" runat="server"
                            meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
                    <div id="divMsg" runat="server">
                        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                    </div>
                </div>
                <div class="widecolumn">
                    <div class="personal_box wdt alignleft">
                        <table class="alignleft">
                            <tr>
                                <td align="center" valign="middle">
                                    <span class="blue_btn_small">
                                        <asp:Button ID="btnAddNew" runat="server" Text="Add New"
                                            PostBackUrl="~/Admin/AddUser.aspx" meta:resourcekey="btnAddNewResource1"></asp:Button>
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
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch"
                            meta:resourcekey="pnlSearchResource1">
                            <table class="alignright">
                                <tr>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <div class="parsonal_selectSmall">
                                                <asp:DropDownList ID="ddlSearchField" runat="server"
                                                    onchange="javascript:HideShowStatus();"
                                                    meta:resourcekey="ddlSearchFieldResource1">
                                                    <asp:ListItem Value="0" Text="-- Select Search Type --"
                                                        meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                    <asp:ListItem Value="FirstName" Text="First Name"
                                                        meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                    <asp:ListItem Value="LastName" Text="Last Name"
                                                        meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    <asp:ListItem Value="EmailAddress" Text="Email Address"
                                                        meta:resourcekey="ltrEmailAddressResource2"></asp:ListItem>
                                                    <asp:ListItem Value="Status" Text="Status" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                     <td align="center" valign="middle">
                                        <div class="parsonal_textfildLarge">
                                            <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" Width="170px"
                                                meta:resourcekey="txtSearchValResource1"></asp:TextBox>
                                            <div id="statusDiv" runat="server" class="parsonal_selectSmall">
                                                <asp:DropDownList ID="ddlSearchVal" runat="server"
                                                    meta:resourcekey="ddlSearchValResource1">
                                                    <asp:ListItem Value="1" Text="Active" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="In Active" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <span class="dark_btn_small">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search"
                                                OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1"></asp:Button>
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
                                        <asp:ListView ID="lvUser" DataSourceID="odsUserList" OnItemCommand="lvUser_ItemCommand"
                                            runat="server" OnPreRender="lvUser_PreRender">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip" width="12%">
                                                            <asp:LinkButton ID="lnkFirstName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="FirstName" Text="First Name"
                                                                meta:resourcekey="lnkFirstNameResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="12%">
                                                            <asp:LinkButton ID="lnkLastName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="LastName" Text="Last Name"
                                                                meta:resourcekey="lnkLastNameResource1"></asp:LinkButton>
                                                        </td>
                                                        <%--<td align="left" valign="middle" class="equip" width="12%">
                                                            <asp:LinkButton ID="lnkUserName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="UserName" Text="User Name"
                                                                meta:resourcekey="lnkUserNameResource1"></asp:LinkButton>
                                                        </td>--%>
                                                        <td align="left" valign="middle" class="equip" width="12%">
                                                           <%-- <asp:Literal ID="ltrEmailAddress" runat="server" Text="Email Address"
                                                                meta:resourcekey="ltrEmailAddressResource2"></asp:Literal>--%>
                                                            <asp:LinkButton ID="lbtnEmailAddress" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="EmailAddress" Text="Email Address"
                                                                meta:resourcekey="ltrEmailAddressResource2"></asp:LinkButton>
                                                        </td>
                                                         <td align="left" valign="middle" class="equip" width="12%">
                                                          <%--  <asp:Literal ID="ltrRegisteredDate" runat="server" Text="Registered Date"
                                                                ></asp:Literal>--%>
                                                              <asp:LinkButton ID="lblRegisteredDate" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="RegisteredDate" Text="Registered Date" meta:resourcekey="lbtRegistrationDateResource2" ></asp:LinkButton>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="4%">
                                                            <asp:Literal ID="ltrStatus" runat="server" Text="Status"
                                                                meta:resourcekey="ltrStatusResource2"></asp:Literal>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="4%">
                                                            <asp:Literal ID="ltrAction" runat="server" Text="Action"
                                                                meta:resourcekey="ltrActionResource2"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("FirstName")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("LastName")%>
                                                    </td>
                                                  <%--  <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("UserName")%>
                                                    </td>--%>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("EmailAddress")%>
                                                    </td>
                                                     <td align="left" valign="top" class="equipbg">
                                                        <%# Convert.ToDateTime(Eval("CreatedDate")).ToString("MM/dd/yyyy")%>
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:Image ID="imgStatus" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>'
                                                            ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) ? this.GetLocalResourceObject("ListItemResource6.Text").ToString() : this.GetLocalResourceObject("ListItemResource7.Text").ToString() %>'
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? this.GetLocalResourceObject("ListItemResource6.Text").ToString() : this.GetLocalResourceObject("ListItemResource7.Text").ToString() %>'
                                                            meta:resourcekey="imgStatusResource1" />
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:HyperLink ID="hypEdit" NavigateUrl='<%# "~/Admin/AddUser.aspx?id=" + Server.UrlEncode(Eval("ID").ToString()) %>'
                                                            runat="server" ToolTip="Edit" ImageUrl="images/bgi/edit.png"
                                                            meta:resourcekey="hypEditResource1"></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip" width="12%">
                                                            <asp:Literal ID="ltrFirstName" runat="server" Text="First Name"
                                                                meta:resourcekey="ltrFirstNameResource1"></asp:Literal>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="12%">
                                                            <asp:Literal ID="ltrLastName" runat="server" Text="Last Name"
                                                                meta:resourcekey="ltrLastNameResource1"></asp:Literal>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="12%">
                                                            <asp:Literal ID="ltrUserName" runat="server" Text="User Name"
                                                                meta:resourcekey="ltrUserNameResource1"></asp:Literal>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="12%">
                                                            <asp:Literal ID="ltrEmailAddress" runat="server" Text="Email Address"
                                                                meta:resourcekey="ltrEmailAddressResource1"></asp:Literal>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="12%">
                                                            <asp:Literal ID="ltrRegisteredDate" runat="server" Text="Registered Date"
                                                                ></asp:Literal>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="4%">
                                                            <asp:Literal ID="ltrStatus" runat="server" Text="Status"
                                                                meta:resourcekey="ltrStatusResource1"></asp:Literal>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="4%">
                                                            <asp:Literal ID="ltrAction" runat="server" Text="Action"
                                                                meta:resourcekey="ltrActionResource1"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top" class="equipbg" colspan="7">
                                                            <asp:Literal ID="ltrNoDataFound" runat="server" Text="No Data Found"
                                                                meta:resourcekey="ltrNoDataFoundResource1"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr class="equip-paging">
                                                <td colspan="5" align="right" style="direction: ltr;">
                                                    <asp:DataPager ID="dpCountryPaging" runat="server" PagedControlID="lvUser">
                                                        <Fields>
                                                            <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page"
                                                                NumericButtonCssClass="button-page"
                                                                meta:resourcekey="NumericPagerFieldResource1" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:ObjectDataSource ID="odsUserList" runat="server" SelectMethod="GetUserListBySearch"
                                            SelectCountMethod="GetUserDataCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.Admin.UserList" OnSelecting="odsUserList_Selecting"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="parsonal_textfild alignright" style="display: none;">
                            <label class="wide">
                                <asp:Literal ID="ltrSelectRecordLimit" runat="server"
                                    Text="Select Record Limit" meta:resourcekey="ltrSelectRecordLimitResource1"></asp:Literal>
                                <span class="alignright" style="width: auto;">:</span></label>
                            <div class="parsonal_selectAuto">
                                <asp:DropDownList ID="ddlPageSize" runat="server" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                                    AutoPostBack="True" meta:resourcekey="ddlPageSizeResource1">
                                    <asp:ListItem Value="10" Text="10" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20" meta:resourcekey="ListItemResource9"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="30" meta:resourcekey="ListItemResource10"></asp:ListItem>
                                    <asp:ListItem Value="40" Text="40" meta:resourcekey="ListItemResource11"></asp:ListItem>
                                    <asp:ListItem Value="50" Text="50" meta:resourcekey="ListItemResource12"></asp:ListItem>
                                    <asp:ListItem Value="60" Text="60" meta:resourcekey="ListItemResource13"></asp:ListItem>
                                    <asp:ListItem Value="70" Text="70" meta:resourcekey="ListItemResource14"></asp:ListItem>
                                    <asp:ListItem Value="80" Text="80" meta:resourcekey="ListItemResource15"></asp:ListItem>
                                    <asp:ListItem Value="90" Text="90" meta:resourcekey="ListItemResource16"></asp:ListItem>
                                    <asp:ListItem Value="100" Text="100" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lvUser" />
        </Triggers>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="processUserList" runat="server" AssociatedUpdatePanelID="upUserList"
        DisplayAfter="10">
        <ProgressTemplate>
            <div class="processbar1">
                <img src="../Images/waiting.gif" alt="loading" width="150" height="150" style="top: 30%; left: 45%; position: absolute;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            document.getElementById('<%=btnAddNew.ClientID%>').focus();
        };
        function ConfirmForDelete(MsgType) {
            return confirm(MsgType);
        }
    </script>
</asp:Content>
