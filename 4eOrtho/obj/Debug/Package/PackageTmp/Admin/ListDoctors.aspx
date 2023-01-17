<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListDoctors.aspx.cs" Inherits="_4eOrtho.Admin.ListDoctors" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/Colorbox/jquery.colorbox-min.js"></script>
    <link href="../Scripts/Colorbox/colorbox.css" rel="stylesheet" />
    <script type="text/javascript">
        function ShowImgTemplate(pname, sourceType) {
            if (sourceType == "ORTHO")
                jQuery('.imgTemplate').attr("href", "../DoctorCertificate/" + pname);
            else
                jQuery('.imgTemplate').attr("href", "http://americanacademyofdentistry.com/DigitalCertificates/" + pname);
            jQuery('.imgTemplate')[0].click();

            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label ID="lblHeader" Text="Doctor List" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
                    <div id="divMsg" runat="server">
                        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                    </div>
                </div>
                <div class="widecolumn">
                    <div class="personal_box  alignleft">
                        <table class="alignleft">
                            <tr>
                                <td align="center" valign="middle">
                                    <span class="blue_btn_small">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add Doctor" ToolTip="Add Doctor" PostBackUrl="~/Admin/AddEditDoctor.aspx" meta:resourcekey="btnAddResource1" />
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" meta:resourcekey="pnlSearchResource1">
                            <table class="alignright">
                                <tr>
                                    <td class="radio-selection" style="margin-top: 5px;">
                                        <asp:RadioButton ID="rbtnNonCertified" runat="server" Text="Non Certified" AutoPostBack="True" Checked="True" GroupName="SourceType" OnCheckedChanged="rbtnNonCertified_CheckedChanged" meta:resourcekey="rbtnNonCertifiedResource1" />
                                    </td>
                                    <td class="radio-selection" style="margin-top: 5px;">
                                        <asp:RadioButton ID="rbtnCertified" runat="server" Text="Certified" AutoPostBack="True" GroupName="SourceType" OnCheckedChanged="rbtnCertified_CheckedChanged" meta:resourcekey="rbtnCertifiedResource1" />
                                    </td>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <div class="parsonal_selectSmallSearch">
                                                <asp:DropDownList ID="ddlSearchBy" name="State" runat="server" onchange="return OnChange();" meta:resourcekey="ddlSearchByResource1">
                                                    <asp:ListItem Value="0" Text="Select Search" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                    <asp:ListItem Value="CountryName" Text="Country" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                    <asp:ListItem Value="StateName" Text="State" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    <asp:ListItem Value="City" Text="City" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                                    <asp:ListItem Value="ZipCode" Text="Zip Code/ Postal" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqvddlSearchBy" ForeColor="Red" runat="server" SetFocusOnError="True"
                                                    ControlToValidate="ddlSearchBy" Display="None" InitialValue="0" ErrorMessage="Please select search."
                                                    CssClass="errormsg" ValidationGroup="serachValidation" meta:resourcekey="rqvddlSearchByResource1"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="cmverqvddlSearchBy" runat="server" CssClass="customCalloutStyle"
                                                    TargetControlID="rqvddlSearchBy" Enabled="True">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfildLarge">
                                            <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" Width="170px" meta:resourcekey="txtSearchValResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqvtxtSearchVal" ForeColor="Red" runat="server" SetFocusOnError="True"
                                                ControlToValidate="txtSearchVal" Display="None" ErrorMessage="Please enter search text."
                                                CssClass="errormsg" ValidationGroup="serachValidation" meta:resourcekey="rqvtxtSearchValResource1"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="cmverqvtxtSearchVal" runat="server" CssClass="customCalloutStyle"
                                                TargetControlID="rqvtxtSearchVal" Enabled="True">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <span class="dark_btn_small">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" ToolTip="Search" ValidationGroup="serachValidation" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1"></asp:Button>
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
                                        <asp:ListView ID="lvCertifiedDoctor" EnableViewState="False" DataSourceID="odsCertifiedDoctor" runat="server" OnPreRender="lvCertifiedDoctor_PreRender">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="top" class="equip">
                                                            <asp:LinkButton ID="lnkSortFirstName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Name" Text="Name" meta:resourcekey="lnkSortFirstName1Resource1" />
                                                        </td>
                                                        <td align="left" valign="top" class="equip">
                                                            <asp:LinkButton ID="lnkSortEmailId" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="EmailId" Text="Email Id" meta:resourcekey="lnkSortEmailId1Resource1" />
                                                        </td>
                                                        <td align="left" valign="top" class="equip">
                                                            <asp:LinkButton ID="lnkSortCountry" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Country" Text="Country" meta:resourcekey="lnkSortCountry1Resource1" />
                                                        </td>
                                                        <%--<td align="left" valign="top" class="equip">
                                                            <asp:LinkButton ID="lnkSortState" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="State" Text="State" meta:resourcekey="lnkSortState1Resource1" />
                                                        </td>--%>
                                                        <td align="left" valign="top" class="equip">
                                                            <asp:LinkButton ID="lnkSortCity" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="City" Text="City" meta:resourcekey="lnkSortCity1Resource1" />
                                                        </td>
                                                        <%--<td align="center" valign="top" class="equip" style="width: 32px;">
                                                            <asp:Label ID="lblSourceType1" runat="server" Text="Source Type" meta:resourcekey="lblSourceType1Resource1" />
                                                        </td>
                                                        <td align="center" valign="top" class="equip" style="width: 32px;">
                                                            <asp:Label ID="lblCaseCount" runat="server" Text="Case Count" meta:resourcekey="lblCaseCountResource2" />
                                                        </td>--%>
                                                        <td align="left" valign="top" class="equip" style="display: none;">
                                                            <asp:LinkButton ID="lnkSortCertificate" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="CertificateFileName" Text="Certificate" meta:resourcekey="lnkSortCertificateResource1" />
                                                        </td>
                                                        <%--<td align="center" valign="top" class="equip">
                                                            <asp:LinkButton ID="lnkIsPayment" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="IsPayment" Text="IsPayment" meta:resourcekey="lnkIsPaymentResource1" />
                                                        </td>--%>
                                                        <%--<td align="center" valign="top" class="equip" style="width: 62px;">
                                                            <asp:LinkButton ID="lnkIsAccActive" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="IsAccActive" Text="Account IsActive" meta:resourcekey="lnkIsAccActiveResource1" />
                                                        </td>--%>
                                                        <td align="center" valign="top" class="equip">
                                                            <%--<asp:Label ID="lblAutoLogin1" runat="server" Text="Auto Login" meta:resourcekey="lblAutoLogin1Resource2" />--%>
                                                            <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource2"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="12" align="right">
                                                            <asp:DataPager ID="dpCertifiedDoctor" runat="server" PagedControlID="lvCertifiedDoctor">
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
                                                        <%# Eval("FirstName") + " " + Eval("LastName") %> 
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("EmailId") %> 
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("CountryName") %> 
                                                    </td>
                                                    <%-- <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("StateName") %> 
                                                    </td>--%>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("City") %>
                                                    </td>
                                                    <%--<td align="left" valign="top" class="equipbg">AAAD
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <%# Convert.ToInt16(Eval("CaseCount")) > 0 ? Eval("CaseCount") : string.Empty %>
                                                    </td>--%>
                                                    <%--<td align="left" valign="top" class="equipbg">
                                                        <a onclick="javascript:ShowImgTemplate('<%# Eval("CertificateFileName") %>')" style="<%# !string.IsNullOrEmpty(Convert.ToString(Eval("CertificateFileName"))) ? "display:block;": "display:none;" %>">Show Certificate</a>
                                                    </td>--%>
                                                    <%--<td align="center" valign="top" class="equipbg">
                                                        <img src='<%# Convert.ToBoolean(Eval("IsPayment")) ? "../Content/Images/icon-active.gif" : "../Content/Images/icon-inactive.gif" %>' />
                                                    </td>--%>
                                                    <%--<td align="center" valign="top" class="equipbg">
                                                        <asp:ImageButton ID="imgbtnStatus" runat="server" CommandName="CUSTOMACTIVE" CommandArgument='<%# Eval("EmailId") + "," + Eval("LastName") %>' OnCommand="Custom_Command"
                                                            ImageUrl='<%# Convert.ToBoolean(Eval("IsAccountActivated")) ? "../Content/Images/icon-active.gif" : "../Content/Images/icon-inactive.gif" %>'
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsAccountActivated")) ? this.GetLocalResourceObject("Active"): this.GetLocalResourceObject("IsAccountActivated") %>' />
                                                    </td>--%>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <a onclick="javascript:ShowImgTemplate('<%# Eval("CertificateFileName") %>','AAD')" style="<%# !string.IsNullOrEmpty(Convert.ToString(Eval("CertificateFileName"))) ? "text-decoration:none;": "display:none;text-decoration:none;" %>">
                                                            <img src="../Content/images/certificate.png" title="<%= this.GetLocalResourceObject("ViewCertificate") %>" alt="<%= this.GetLocalResourceObject("ViewCertificate") %>" />
                                                        </a>
                                                        &nbsp;
                                                        <img src='<%# Convert.ToBoolean(Eval("IsPayment")) ? "../Content/Images/payment-recieved.png" : "../Content/Images/payment-not-recieved.png" %>' alt='<%# Convert.ToBoolean(Eval("IsPayment")) ? this.GetLocalResourceObject("PaymentReceived") : this.GetLocalResourceObject("PaymentNotReceived") %>'
                                                            title='<%# Convert.ToBoolean(Eval("IsPayment")) ? this.GetLocalResourceObject("PaymentReceived") : this.GetLocalResourceObject("PaymentNotReceived") %>' />
                                                        &nbsp;
                                                        <asp:ImageButton ID="imgbtnStatus" runat="server" CommandName="CUSTOMACTIVE" CommandArgument='<%# Eval("EmailId") + "," + Eval("LastName") %>' OnCommand="Custom_Command"
                                                            ImageUrl='<%# Convert.ToBoolean(Eval("IsAccountActivated")) ? "../Content/Images/active-account.png" : "../Content/Images/de-active-account.png" %>'
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsAccountActivated")) ? this.GetLocalResourceObject("ClickDeActive"): this.GetLocalResourceObject("ClickActive") %>'
                                                            ToolTip='<%# Convert.ToBoolean(Eval("IsAccountActivated")) ? this.GetLocalResourceObject("ClickDeActive"): this.GetLocalResourceObject("ClickActive") %>' />
                                                        <asp:ImageButton ID="imgbtnLogin1" runat="server" OnCommand="Custom_Command" CommandArgument='<%# Eval("EmailId") %>' CommandName="AUTOLOGIN" ImageUrl="../Content/Images/autologin.png" AlternateText="Auto Login" ToolTip="Auto Login"
                                                            Style="padding: 0px 5px; margin-left: 0px;" meta:resourcekey="imgbtnLogin1Resource1" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkSortFirstName1Resource1.Text") %>
                                                        </td>
                                                        <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkSortEmailId1Resource1.Text") %>
                                                        </td>
                                                        <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkSortCountry1Resource1.Text") %>
                                                        </td>
                                                        <%--<td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkSortState1Resource1.Text") %>
                                                        </td>--%>
                                                        <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkSortCity1Resource1.Text") %>
                                                        </td>
                                                        <%--<td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lblSourceType1Resource1.Text") %>
                                                        </td>
                                                        <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lblCaseCountResource2.Text") %>
                                                        </td>--%>
                                                        <%-- <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkSortCertificateResource1.Text") %>
                                                        </td>--%>
                                                        <%--<td align="center" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkIsPaymentResource1.Text") %>
                                                        </td>
                                                        <td align="center" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkIsAccActiveResource1.Text") %>
                                                        </td>--%>
                                                        <td align="center" valign="top" class="equip">
                                                            <%--<%= this.GetLocalResourceObject("lblAutoLogin1Resource2.Text") %>--%>
                                                            <%= this.GetLocalResourceObject("lblActionResource2.Text") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="middle" class="equipbg" colspan="12">
                                                            <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                        <asp:ObjectDataSource ID="odsCertifiedDoctor" runat="server" SelectMethod="GetCertifiedDoctorDetailsByFilterType"
                                            SelectCountMethod="GetTotalRowCertifiedCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.FindDoctor" OnSelecting="odsCertifiedDoctor_Selecting">
                                            <SelectParameters>
                                                <asp:Parameter Name="sortField" Type="String" />
                                                <asp:Parameter Name="sortDirection" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <asp:ListView ID="lvNonCertifiedDoctor" EnableViewState="False" DataSourceID="odsNonCertifiedDoctor" runat="server" OnItemDataBound="lvNonCertifiedDoctor_ItemDataBound" OnPreRender="lvNonCertifiedDoctor_PreRender">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="top" class="equip">
                                                            <asp:LinkButton ID="lnkSortFirstName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Name" Text="Name" meta:resourcekey="lnkSortFirstNameResource1" />
                                                        </td>
                                                        <td align="left" valign="top" class="equip">
                                                            <asp:LinkButton ID="lnkSortEmailId" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="EmailId" Text="Email Id" meta:resourcekey="lnkSortEmailIdResource1" />
                                                        </td>
                                                        <td align="left" valign="top" class="equip">
                                                            <asp:LinkButton ID="lnkSortCountry" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Country" Text="Country" meta:resourcekey="lnkSortCountryResource1" />
                                                        </td>
                                                        <%--<td align="left" valign="top" class="equip">
                                                            <asp:LinkButton ID="lnkSortState" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="State" Text="State" meta:resourcekey="lnkSortStateResource1" />
                                                        </td>--%>
                                                        <td align="left" valign="top" class="equip">
                                                            <asp:LinkButton ID="lnkSortCity" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="City" Text="City" meta:resourcekey="lnkSortCityResource1" />
                                                        </td>
                                                        <%--<td align="center" valign="top" class="equip" style="width: 5%;">
                                                            <asp:Label ID="lblSourceType" runat="server" Text="Source Type" meta:resourcekey="lblSourceTypeResource3" />
                                                        </td>
                                                        <td align="center" valign="top" class="equip" style="width: 5%">
                                                            <asp:Label ID="lblDomainName" runat="server" Text="Domain Name" meta:resourcekey="lblDomainURLResource1" />
                                                        </td>
                                                        <td align="center" valign="top" class="equip" style="width: 5%;">
                                                            <asp:Label ID="lblCaseCount" runat="server" Text="Case Count" meta:resourcekey="lblCaseCountResource4" />
                                                        </td>--%>
                                                        <td align="left" valign="top" class="equip" style="width: 32px;">
                                                            <asp:LinkButton ID="lnkSortCertificate" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="CertificateFileName" Text="Certificate" meta:resourcekey="lnkSortCertificateResource1" />
                                                        </td>
                                                        <%--<td align="center" valign="top" class="equip" style="width: 10%;">
                                                            <asp:LinkButton ID="lnkIsPayment" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="IsPayment" Text="IsPayment" meta:resourcekey="lnkIsPaymentResource1" />
                                                        </td>
                                                        <td align="center" valign="top" class="equip" style="width: 7%;">
                                                            <asp:LinkButton ID="lnkIsAccActive" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="IsAccActive" Text="Account IsActive" meta:resourcekey="lnkIsAccActiveResource1" />
                                                        </td>
                                                        <td align="center" valign="top" class="equip" style="width: 5%;">
                                                            <asp:Label ID="lblAutoLogin" runat="server" Text="Auto Login" meta:resourcekey="lblAutoLoginResource2" />
                                                        </td>--%>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource2"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="13" align="right">
                                                            <asp:DataPager ID="dpNonCertifiedDoctor" runat="server" PagedControlID="lvNonCertifiedDoctor">
                                                                <Fields>
                                                                    <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page" NumericButtonCssClass="button-page" meta:resourcekey="NumericPagerFieldResource2" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("FirstName") + " " + Eval("LastName") %> 
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("EmailId") %> 
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("CountryName") %> 
                                                    </td>
                                                    <%--<td align="left" valign="top" class="equipbg">
                                                        <%# Eval("StateName") %> 
                                                    </td>--%>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("City") %>
                                                    </td>
                                                    <%--<td align="center" valign="top" class="equipbg">
                                                        <%# Eval("SourceType") %> 
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <asp:HyperLink ID="hypDomainLink" runat="server" meta:resourcekey="hypDomainLinkResource1"></asp:HyperLink>
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <%# Convert.ToInt16(Eval("CaseCount")) > 0 ? Eval("CaseCount") : string.Empty %>
                                                    </td>--%>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <a onclick="javascript:ShowImgTemplate('<%# Eval("CertificateFileName") %>','<%# Eval("Is4eOrthoPass").ToString() == "1" ? "AAD" : "ORTHO" %>')" style="<%# !string.IsNullOrEmpty(Convert.ToString(Eval("CertificateFileName"))) ? "text-decoration:none;": (Eval("Is4eOrthoPass").ToString() == "1" ? "text-decoration:none;" : "display:none;text-decoration:none;" ) %>">
                                                            <img src="../Content/images/certificate.png" title="<%= this.GetLocalResourceObject("ViewCertificate") %>" alt="<%= this.GetLocalResourceObject("ViewCertificate") %>" />
                                                        </a>
                                                        <img src="../Content/images/disabled-certificate.png" alt="" title="" style="<%# !string.IsNullOrEmpty(Convert.ToString(Eval("CertificateFileName"))) && Eval("Is4eOrthoPass").ToString() == "0" ? "display:none": "opacity:0.5;margin-left:5px;" %>" />
                                                        <a href="#divComment<%# Container.DataItemIndex %>" class="commentbox" style="<%# !string.IsNullOrEmpty(Convert.ToString(Eval("CertificateFileName"))) && Eval("Is4eOrthoPass").ToString() == "0" ? "text-decoration:none;": "display:none;text-decoration:none;width:36px;" %>">
                                                            <img src="../Content/images/comment.png" title="<%= this.GetLocalResourceObject("ViewComment") %>" alt="<%= this.GetLocalResourceObject("ViewComment") %>" style="width: 16px;" />
                                                        </a>
                                                        <img src="../Content/images/comment.png" alt="" title="" style="<%# !string.IsNullOrEmpty(Convert.ToString(Eval("CertificateFileName"))) ? "display:none;width:16px;": "opacity:0.5;width:16px;margin-left:5px;" %>" />
                                                        <div style="display: none;">
                                                            <div id="divComment<%# Container.DataItemIndex %>" style="padding: 10px;">
                                                                <div class="parsonal_textfild">
                                                                    <label>
                                                                        <strong>
                                                                            <asp:Label ID="lblComment" runat="server" Text="Comment" meta:resourcekey="lblCommentResource1"></asp:Label>&nbsp;:</strong>
                                                                    </label>
                                                                </div>
                                                                <div class="clear"></div>
                                                                <div class="parsonal_textfild">
                                                                    <%# Eval("Comment") %>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <%--<td align="center" valign="top" class="equipbg">
                                                        <img src='<%# Convert.ToBoolean(Eval("IsPayment")) ? "../Content/Images/icon-active.gif" : "../Content/Images/icon-inactive.gif" %>' />
                                                    </td>--%>
                                                    <%--<td align="center" valign="top" class="equipbg">
                                                        <asp:ImageButton ID="imgbtnStatus" runat="server" CommandName="CUSTOMACTIVE" CommandArgument='<%# Eval("EmailId") + "," + Eval("LastName") %>' OnCommand="Custom_Command"
                                                            ImageUrl='<%# Convert.ToBoolean(Eval("IsAccountActivated")) ? "../Content/Images/icon-active.gif" : "../Content/Images/icon-inactive.gif" %>'
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsAccountActivated")) ? this.GetLocalResourceObject("Active"): this.GetLocalResourceObject("IsAccountActivated") %>' />
                                                    </td>--%>
                                                    <%--<td align="center" valign="top" class="equipbg">
                                                        <asp:ImageButton ID="imgbtnLoginNonCertified" runat="server" OnCommand="Custom_Command" CommandArgument='<%# Eval("EmailId") %>' CommandName="AUTOLOGIN" ImageUrl="~/Admin/Images/autologin.png" AlternateText="Auto Login" ToolTip="Auto Login"
                                                            Visible='<%# Convert.ToString(Eval("SourceType")).ToLower() == "aaad" ? false : true %>' Style="padding: 0px 5px; margin-left: 0px;" meta:resourcekey="imgbtnLoginNonCertifiedResource1" />
                                                    </td>--%>
                                                    <td align="center" valign="top" class="equipbg">
                                                        <img src='<%# Convert.ToBoolean(Eval("IsPayment")) ? "../Content/Images/payment-recieved.png" : "../Content/Images/payment-not-recieved.png" %>' alt='<%# Convert.ToBoolean(Eval("IsPayment")) ? this.GetLocalResourceObject("PaymentReceived") : this.GetLocalResourceObject("PaymentNotReceived") %>'
                                                            title='<%# Convert.ToBoolean(Eval("IsPayment")) ? this.GetLocalResourceObject("PaymentReceived") : this.GetLocalResourceObject("PaymentNotReceived") %>' />
                                                        &nbsp;                                                        
                                                        <asp:ImageButton ID="imgbtnStatus" runat="server" CommandName="CUSTOMACTIVE" CommandArgument='<%# Eval("EmailId") + "," + Eval("LastName") %>' OnCommand="Custom_Command"
                                                            ImageUrl='<%# Convert.ToBoolean(Eval("IsAccountActivated")) ? "../Content/Images/active-account.png" : "../Content/Images/de-active-account.png" %>'
                                                            AlternateText='<%# Convert.ToBoolean(Eval("IsAccountActivated")) ? this.GetLocalResourceObject("ClickDeActive"): this.GetLocalResourceObject("ClickActive") %>'
                                                            ToolTip='<%# Convert.ToBoolean(Eval("IsAccountActivated")) ? this.GetLocalResourceObject("ClickDeActive"): this.GetLocalResourceObject("ClickActive") %>' />
                                                        &nbsp;
                                                        <asp:ImageButton ID="imgbtnLoginNonCertified" runat="server" OnCommand="Custom_Command" CommandArgument='<%# Eval("EmailId") %>' CommandName="AUTOLOGIN" ImageUrl="../Content/Images/autologin.png" AlternateText="Auto Login" ToolTip="Auto Login"
                                                            Visible='<%# Convert.ToString(Eval("SourceType")).ToLower() == "aaad" ? false : true %>' Style="padding: 0px 5px; margin-left: 0px;" meta:resourcekey="imgbtnLoginNonCertifiedResource1" />
                                                        <asp:HyperLink ID="hypEdit" runat="server" ToolTip="Edit" ImageUrl='../content/images/edit.png' meta:resourcekey="hypEditResource1"></asp:HyperLink>
                                                        <%--</td>--%>
                                                        <%--<td style="width: 33%">
                                                                    <%--<asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="DeleteDiscount" ImageUrl="~/Admin/Images/bgi/delete.png" AlternateText="Delete" ToolTip="Delete"
                                                                        Style="padding: 0px 5px; margin-left: 0px;" meta:resourcekey="imgbtnDeleteResource1" />
                                                                </td>--%>
                                                        <%--</tr>
                                                        </table>--%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkSortFirstNameResource1.Text") %>
                                                        </td>
                                                        <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkSortEmailIdResource1.Text") %>
                                                        </td>
                                                        <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkSortCountryResource1.Text") %>
                                                        </td>
                                                        <%--<td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkSortStateResource1.Text") %>
                                                        </td>--%>
                                                        <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkSortCityResource1.Text") %>
                                                        </td>
                                                        <%--<td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lblSourceTypeResource3.Text") %>
                                                        </td>
                                                        <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lblDomainNameResource1.Text") %>
                                                        </td>
                                                        <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lblCaseCountResource4.Text") %>
                                                        </td>--%>
                                                        <td align="left" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkSortCertificateResource1.Text") %>
                                                        </td>
                                                        <%--<td align="center" valign="top" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkIsPaymentResource1.Text") %>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <%= this.GetLocalResourceObject("lnkIsAccActiveResource1.Text") %>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <%= this.GetLocalResourceObject("lblAutoLoginResource2.Text") %>
                                                        </td>--%>
                                                        <td align="left" valign="middle" class="equip">
                                                            <%= this.GetLocalResourceObject("lblActionResource2.Text") %>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                        <asp:ObjectDataSource ID="odsNonCertifiedDoctor" runat="server" SelectMethod="GetNonCertifiedDoctorDetailsByFilterType"
                                            SelectCountMethod="GetTotalRowNonCertifiedCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.FindDoctor" OnSelecting="odsNonCertifiedDoctor_Selecting">
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
            <a href="#" id="appReuest" class="imgTemplate" style="display: none;"></a>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="processUser" runat="server" AssociatedUpdatePanelID="up1"
        DisplayAfter="10">
        <ProgressTemplate>
            <div class="processbar1">
                <img src="../Content/images/loading.gif" alt="loading" style="top: 30%; left: 45%; position: absolute;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            jQuery(".imgTemplate").colorbox({
                iframe: true,
                width: "900px",
                height: "600px",
                overlayClose: false,
                escKey: true,
            });
            jQuery(".commentbox").colorbox({
                inline: true,
                width: "450px",
                height: "300px",
                overlayClose: false,
                escKey: true,
            });
        });
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {
            jQuery(".imgTemplate").colorbox({
                iframe: true,
                width: "900px",
                height: "600px",
                overlayClose: false,
                escKey: true,
            });
            jQuery(".commentbox").colorbox({
                inline: true,
                width: "450px",
                height: "300px",
                overlayClose: false,
                escKey: true,
            });
        });

        function confirmMessageA() {

        }
    </script>
</asp:Content>
