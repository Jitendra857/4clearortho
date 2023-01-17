<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UpdateTrackStatus.aspx.cs" Inherits="_4eOrtho.Admin.UpdateTrackStatus" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .parsonal_textfild label {
            width: 236px;
        }
    </style>
    <div id="container" class="cf">

        <div class="page_title">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        <h2 class="padd">
                            <asp:Label ID="lblHeader" runat="server" Text="Track Status" meta:resourcekey="lblHeaderResource1"></asp:Label>
                    </td>
                    <td style="width: 50%;">
                        <span class="dark_btn_small">
                            <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/ListNewCaseDetails.aspx"
                                TabIndex="5" meta:resourcekey="btnBackResource1"></asp:Button>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </div>
        <div class="widecolumn">
            <div class="personal_box alignleft">
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblTrackId" runat="server" Text="Track No" meta:resourcekey="lblTrackIdResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:Label ID="lblTrackNo" runat="server" meta:resourcekey="lblTrackNoResource1"></asp:Label>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblCaseNo1" runat="server" Text="Case No" meta:resourcekey="lblCaseNo1Resource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:Label ID="lblCaseNo" runat="server" meta:resourcekey="lblCaseNoResource1"></asp:Label>
                    </div>
                </div>
                <%--<div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblCurrentStatus1" runat="server" Text="Current Status" meta:resourcekey="lblCurrentStatus1Resource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:Label ID="lblCurrentStatus" runat="server" meta:resourcekey="lblCurrentStatusResource1"></asp:Label>
                    </div>
                </div>--%>
                <div class="clear"></div>
                <div class="parsonal_textfild">
                    <label>
                        <asp:Label ID="lblTrackHistory" runat="server" Text="Track History" meta:resourcekey="lblTrackHistoryResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div style="width: 70%; float: left;">
                        <div class="list-data">
                            <asp:ListView ID="lvTrackHistory" runat="server">
                                <LayoutTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="left" valign="top" class="equip">
                                                <asp:Label ID="lblDate" runat="server" meta:resourcekey="lblDateResource1"></asp:Label>
                                            </td>
                                            <td align="left" valign="top" class="equip">
                                                <asp:Label ID="lblStatus" runat="server" meta:resourcekey="lblStatusResource1"></asp:Label>
                                            </td>
                                            <td align="left" valign="top" class="equip">
                                                <asp:Label ID="lblDescription" runat="server" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                                            </td>
                                            <td align="left" valign="top" class="equip">
                                                <asp:Label ID="lblUpdatedBy" runat="server" meta:resourcekey="lblUpdatedByResource1"></asp:Label>
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" valign="top" class="equipbg">
                                            <%# Eval("CreatedDate") %>
                                        </td>
                                        <td align="left" valign="top" class="equipbg">
                                            <%# Eval("TrackStatus") %>
                                        </td>
                                        <td align="left" valign="top" class="equipbg">
                                            <a href="#" title="<%# Eval("Description") %>">
                                                <%# Convert.ToString(Eval("Description")).Length > 50 ? Convert.ToString(Eval("Description")).Substring(0,50) + "..." : Convert.ToString(Eval("Description"))  %>
                                            </a>
                                        </td>
                                        <td align="left" valign="top" class="equipbg">
                                            <%# Eval("UpdatedBy") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="left" valign="top" class="equip">DateTime
                                            </td>
                                            <td align="left" valign="top" class="equip">Status
                                            </td>
                                            <td align="left" valign="top" class="equip">Description
                                            </td>
                                            <td align="left" valign="top" class="equip">Updated By
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" class="equip" colspan="3">No data found.
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left" valign="top" class="rgt"></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblChangeStatus" runat="server" Text="Update Status" meta:resourcekey="lblChangeStatusResource1"></asp:Label><span class="required">*</span>
                        <span class="alignright">:</span></label>
                    <div class="parsonal_select">
                        <asp:DropDownList ID="ddlUpdateStatus" runat="server" class="low_high2" Style="color: black; margin-top: 0px;" meta:resourcekey="ddlUpdateStatusResource1">
                            <asp:ListItem Text="Select Status" Value="0" meta:resourcekey="ListItemResource1"></asp:ListItem>
                            <asp:ListItem Text="Acknowledged" Value="Acknowledged" meta:resourcekey="ListItemResource2"></asp:ListItem>
                            <asp:ListItem Text="InProcess" Value="InProcess" meta:resourcekey="ListItemResource3"></asp:ListItem>
                            <asp:ListItem Text="Shipped" Value="Shipped" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlUpdateStatus" ForeColor="Red" runat="server" ControlToValidate="ddlUpdateStatus"
                            Display="None" ErrorMessage="Please Select Status" CssClass="errormsg"
                            SetFocusOnError="True" ValidationGroup="validation" InitialValue="0" meta:resourcekey="rfvddlUpdateStatusResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddlUpdateStatus" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rfvddlUpdateStatus" Enabled="True" />
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblDescription" runat="server" Text="Description" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Style="width: 400px; height: 100px" TabIndex="12" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="clear"></div>
                <%--<div class="parsonal_textfild">
                    <label>
                        <asp:Label ID="lblIsactive" runat="server" Text="IsActive?" meta:resourcekey="lblIsactiveResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" meta:resourcekey="chkIsActiveResource1" />
                </div>--%>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="bottom_btn tpadd alignright" style="width: 268px;">
            <span class="blue_btn">
                <asp:Button ID="btnSubmit" runat="server" Text="Save" ValidationGroup="validation" OnClick="btnSubmit_Click" meta:resourcekey="btnSubmitResource1" />
            </span>
            <span class="dark_btn">                
                <input type="reset" tabindex="7" title="<%= this.GetLocalResourceObject("Reset") %>" value="<%= this.GetLocalResourceObject("Reset") %>" />
            </span>
        </div>
    </div>
</asp:Content>
