<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="_4eOrtho.error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.7.2.min.js"></script>
    <link href="Admin/Styles/AdminStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function SelectAll(chkAll) {
            if ($($(chkAll)[0]).prop('checked') == true) {
                var chkDelete = $('[id*=chkDelete]');
                $(chkDelete).each(function () {
                    $(this).prop('checked', true);
                });
            }
            else {
                var chkDelete = $('[id*=chkDelete]');
                $(chkDelete).each(function () {
                    $(this).prop('checked', false);
                });
            }
        }
        function CheckAll() {
            var chkDelete = $('[id*=chkDelete]');
            var checkedCount = 0;
            $(chkDelete).each(function () {
                if ($(this).prop('checked') == true)
                    checkedCount = checkedCount + 1;
            });
            if (checkedCount == chkDelete.length) {
                $('[id$=chkAll]').prop('checked', true);
            }
            else {
                $('[id$=chkAll]').prop('checked', false);
            }
        }
        function DeleteMessage() {
            var chkDelete = $('[id*=lvError_chkDelete]');
            var flag = false;
            $(chkDelete).each(function () {
                if ($(this).prop('checked') == true) {
                    flag = true;
                }
            });
            if (flag) {
                if (confirm("Are you sure you want to delete this record(s)."))
                    return true;
                else
                    return false;
            }
            else {
                alert("Please select record.");
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upList" runat="server">
            <ContentTemplate>
                <div id="container" class="cf">
                    <div class="widecolumn">
                        <div class="personal_box  alignleft">
                            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                                <table class="alignright">
                                    <tr>
                                        <td align="center" valign="middle">
                                            <div class="parsonal_textfildLarge">
                                                <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" Width="170px"></asp:TextBox>
                                            </div>
                                        </td>
                                        <td align="center" valign="middle">
                                            <span class="dark_btn_small">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="serachValidation"></asp:Button>
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
                                            <asp:ListView ID="lvError" DataSourceID="odsErrorList" runat="server" DataKeyNames="Id">
                                                <LayoutTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td class="equip" width="5%" style="text-align: center; vertical-align: middle;">
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAll(this);" />
                                                            </td>
                                                            <td class="equip" width="10%" style="text-align: center; vertical-align: middle;">DateTime</td>
                                                            <td class="equip" width="20%" style="text-align: left; vertical-align: middle;">Logger</td>
                                                            <td class="equip" width="20%" style="text-align: left; vertical-align: middle;">Message</td>
                                                            <td class="equip" width="35%" style="text-align: left; vertical-align: middle;">Exception</td>
                                                        </tr>
                                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                        <tr class="equip-paging">
                                                            <td>
                                                                <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="content/images/icon_img09.png" OnClick="imgbtnDelete_Click" OnClientClick="return DeleteMessage();" />
                                                            </td>
                                                            <td colspan="5" align="right">
                                                                <asp:DataPager ID="dpErrorPaging" runat="server" PagedControlID="lvError">
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
                                                        <td class="equipbg" style="text-align: center; vertical-align: middle;">
                                                            <asp:CheckBox ID="chkDelete" runat="server" onclick="CheckAll();" />
                                                        </td>
                                                        <td class="equipbg" style="text-align: center; vertical-align: middle;">
                                                            <%# Convert.ToDateTime(Eval("Date")) %>
                                                        </td>
                                                        <td class="equipbg" style="text-align: left; vertical-align: middle;">
                                                            <%# Eval("Logger") %>
                                                        </td>
                                                        <td class="equipbg" style="text-align: left; vertical-align: middle;">
                                                            <span title="<%# Convert.ToString(Eval("Message")) %>">
                                                                <%# Server.HtmlEncode(Convert.ToString(Eval("Message"))).Length > 50 ? Server.HtmlEncode(Convert.ToString(Eval("Message"))).Substring(0,50) :  Server.HtmlEncode(Convert.ToString(Eval("Message"))) %>
                                                            </span>
                                                        </td>
                                                        <td class="equipbg" style="text-align: left; vertical-align: middle;">
                                                            <%# Convert.ToString(Eval("Exception")) %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td class="equip" width="5%" style="text-align: center; vertical-align: middle;">
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAll(this);" />
                                                            </td>
                                                            <td class="equip" width="10%" style="text-align: center; vertical-align: middle;">DateTime</td>
                                                            <td class="equip" width="20%" style="text-align: left; vertical-align: middle;">Logger</td>
                                                            <td class="equip" width="20%" style="text-align: left; vertical-align: middle;">Message</td>
                                                            <td class="equip" width="35%" style="text-align: left; vertical-align: middle;">Exception</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="middle" class="equipbg" colspan="10">No Data Found</td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                            <asp:ObjectDataSource ID="odsErrorList" runat="server" SelectMethod="GetErrorListBySearch"
                                                SelectCountMethod="GetErrorDataCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                                TypeName="_4eOrtho.error" OnSelecting="odsErrorList_Selecting"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--<asp:UpdateProgress ID="processCountryList" runat="server" AssociatedUpdatePanelID="upList"
            DisplayAfter="10">
            <ProgressTemplate>
                <div class="processbar1">
                    <img src="Content/images/loading.gif" alt="loading" style="top: 50%; left: 50%; position: absolute;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
    </form>
</body>
</html>
