<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListCaseCharges.aspx.cs" Inherits="_4eOrtho.Admin.ListCaseCharges" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="cf">
        <div class="page_title">
            <h2 class="padd">
                <asp:Label runat="server" ID="lblHeader" Text="Case Charges List" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
        </div>
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </div>
        <div class="widecolumn">
            <div class="personal_box alignleft">
                <table class="alignleft">
                    <tr>
                        <td align="center" valign="middle">
                            <span class="blue_btn_small">
                                <asp:Button ID="btnAddNew" runat="server" Text="Add New" PostBackUrl="~/Admin/AddEditCaseCharges.aspx" meta:resourcekey="btnAddNewResource1"></asp:Button>
                            </span>
                        </td>
                    </tr>
                </table>
                <div class="clear">
                </div>
                <div class="list-data">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" valign="top" class="rgt" width="100%">
                                <asp:ListView ID="lvCaseCharges" runat="server" DataSourceID="odsCaseCharge" OnPreRender="lvCaseCharges_PreRender">
                                    <LayoutTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="left" valign="middle" class="equip">
                                                    <asp:LinkButton ID="lnkLookupName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="LookupName" Text="Case Type" meta:resourcekey="lnkLookupNameResource1" />
                                                </td>
                                                <td align="right" valign="middle" class="equip">
                                                    <asp:LinkButton ID="lnkAmount" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="Amount" Text="Amount ($)" meta:resourcekey="lnkAmountResource1" />
                                                </td>
                                                <td align="center" valign="middle" class="equip">
                                                    <asp:LinkButton ID="lnkCouponCode" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="CouponCode" Text="Coupon Code" meta:resourcekey="lnkCouponCodeResource1" />
                                                </td>
                                                <td align="right" valign="middle" class="equip">
                                                    <asp:LinkButton ID="lnkDiscountValue" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                        CommandArgument="DiscountValue" Text="Discount Amount" meta:resourcekey="lnkDiscountValueResource1" />
                                                </td>
                                                <td align="center" valign="middle" class="equip">
                                                    <asp:Label ID="lblCaseChargeIsActive" runat="server" Text="Case IsActive?" meta:resourcekey="lblCaseChargeIsActiveResource2"></asp:Label>
                                                </td>
                                                <td align="center" valign="middle" class="equip">
                                                    <asp:Label ID="lblDiscountIsActive" runat="server" Text="Discount IsActive?" meta:resourcekey="lblDiscountIsActiveResource2"></asp:Label>
                                                </td>
                                                  <td align="center" valign="middle" class="equip">
                                                    <asp:Label ID="lblDoctorNameDisplay" runat="server" Text="Doctor Name"></asp:Label>
                                                </td>
                                                <td align="center" valign="middle" class="equip">
                                                    <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource2"></asp:Label>
                                                </td>
                                            </tr>
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                            <tr class="equip-paging">
                                                <td colspan="16" align="right">
                                                    <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvCaseCharges">
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
                                                <%# Eval("LookupName") %>
                                            </td>
                                            <td align="right" valign="top" class="equipbg">
                                                <%# Convert.ToDecimal(Eval("Amount")).ToString("0.00") %>
                                            </td>
                                            <td align="center" valign="top" class="equipbg">
                                                <%# Eval("CouponCode") %>
                                            </td>
                                            <td align="right" valign="top" class="equipbg">
                                                <%# !string.IsNullOrEmpty(Convert.ToString(Eval("DiscountValue"))) ? Convert.ToInt64(Eval("DiscountValue")) + (!Convert.ToBoolean(Eval("IsFlat")) ? "%" : string.Empty ) : string.Empty %>
                                            </td>
                                            <td align="center" valign="top" class="equipbg">
                                                <asp:ImageButton ID="imgbtnStatus" runat="server" OnClientClick="return DeactivateMessage(this);" CommandName="CUSTOMISACTIVE" CommandArgument='<%# Eval("CaseChargeId") %>' OnCommand="Custom_Command"
                                                    ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>' meta:resourcekey="imgbtnStatusResource1"
                                                    AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? this.GetLocalResourceObject("ClickDeActive"): this.GetLocalResourceObject("ClickActive") %>'
                                                    ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) ? this.GetLocalResourceObject("ClickDeActive"): this.GetLocalResourceObject("ClickActive") %>' />
                                            </td>
                                            <td align="center" valign="top" class="equipbg">
                                                <asp:ImageButton ID="imgbtnDiscountStatus" runat="server" OnClientClick="return DeactivateMessage(this);" CommandName="CUSTOMDISCOUNTISACTIVE" CommandArgument='<%# Eval("DiscountId") %>' OnCommand="Custom_Command"
                                                    ImageUrl='<%# Convert.ToBoolean(Eval("DiscountIsActive")) ? "Images/icon-active.gif" : "Images/icon-inactive.gif" %>' meta:resourcekey="imgbtnDiscountStatusResource1"
                                                    AlternateText='<%# Convert.ToBoolean(Eval("DiscountIsActive")) ? this.GetLocalResourceObject("ClickDeActive"): this.GetLocalResourceObject("ClickActive") %>'
                                                    ToolTip='<%# Convert.ToBoolean(Eval("DiscountIsActive")) ? this.GetLocalResourceObject("ClickDeActive"): this.GetLocalResourceObject("ClickActive") %>'
                                                    Style='<%# string.IsNullOrEmpty(Convert.ToString(Eval("DiscountValue"))) ? "display:none;": string.Empty %>' />
                                            </td>
                                            <td align="center" valign="top" class="equipbg">
                                                <%# Eval("DoctorName") %>
                                            </td>
                                            <td align="center" valign="top" class="equipbg">
                                                <asp:ImageButton ID="hypEdit" runat="server" ImageUrl="images/edit.png" PostBackUrl='<%# "~/Admin/AddEditCaseCharges.aspx?id=" + _4eOrtho.Utility.Cryptography.EncryptStringAES( Eval("CaseChargeId").ToString(), _4eOrtho.Utility.CommonLogic.GetConfigValue("SharedSecret")) %>' meta:resourcekey="hypEditResource1" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="left" valign="middle" class="equip">
                                                    <asp:Label ID="lblLookupName" runat="server" Text="Case Type" meta:resourcekey="lblLookupNameResource1"></asp:Label>
                                                </td>
                                                <td align="right" valign="middle" class="equip">
                                                    <asp:Label ID="lblAmount" runat="server" Text="Amount ($)" meta:resourcekey="lblAmountResource1"></asp:Label>
                                                </td>
                                                <td align="center" valign="middle" class="equip">
                                                    <asp:Label ID="lblCouponCode" runat="server" Text="Coupon Code" meta:resourcekey="lblCouponCodeResource1"></asp:Label>
                                                </td>
                                                <td align="right" valign="middle" class="equip">
                                                    <asp:Label ID="lblDiscountValue" runat="server" Text="Discount Amount" meta:resourcekey="lblDiscountValueResource1"></asp:Label>
                                                </td>
                                                <td align="center" valign="middle" class="equip">
                                                    <asp:Label ID="lblCaseChargeIsActive" runat="server" Text="Case IsActive?" meta:resourcekey="lblCaseChargeIsActiveResource1"></asp:Label>
                                                </td>
                                                <td align="center" valign="middle" class="equip">
                                                    <asp:Label ID="lblDiscountIsActive" runat="server" Text="Discount IsActive?" meta:resourcekey="lblDiscountIsActiveResource1"></asp:Label>
                                                </td>
                                                <td align="center" valign="middle" class="equip">
                                                    <asp:Label ID="lblDoctorName" runat="server" Text="Doctor Name"></asp:Label>
                                                </td>
                                                <td align="center" valign="middle" class="equip">
                                                    <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="middle" class="equipbg" colspan="15">
                                                    <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                                <asp:ObjectDataSource ID="odsCaseCharge" runat="server" SelectMethod="GetAllCaseCharges"
                                    SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                    TypeName="_4eOrtho.Admin.ListCaseCharges" OnSelecting="odsCaseCharge_Selecting">
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
