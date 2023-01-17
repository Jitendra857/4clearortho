<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListNewCaseDetails.aspx.cs" Inherits="_4eOrtho.Admin.ListNewCaseDetails" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../Styles/Jquery-UI/jquery-ui-1.8.23.custom.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $('.From-Date').datepicker({
                showOn: "button",
                buttonText: "Select Date",
                buttonImage: "../Content/images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                yearRange: "-100:+20",
                maxDate: new Date()
            });
            $('.not-edit').attr("readonly", "readonly");

            HideShowSearchControl();
        }
        $(document).ready(function () {
            $('#<%=ddlFilter.ClientID%>').change(function () {
                HideShowSearchControl();
            });
        });
        function DeleteMessage(obj) {
            if (confirm("<%=this.GetLocalResourceObject("DeleteMessage").ToString()%>"))
                return true;
            else
                return false;
        }
        function HideShowSearchControl() {
            if ($('#<%=ddlFilter.ClientID%>').val() == 'Doctor') {
                $('#tdTextBox').css('display', 'none');
                $('#tdDoctor').css('display', '');
                $('#tdDate').css('display', 'none');
                $('#tdTrackStatus').css('display', 'none');

                ValidatorEnable($('[id$=rfvDoctor]')[0], true);
                ValidatorEnable($('[id$=rfvTrackStatus]')[0], false);
                ValidatorEnable($('[id$=rfvDaTe]')[0], false);

            }
            else if ($('#<%=ddlFilter.ClientID%>').val() == 'CreatedDate') {
                $('#tdTextBox').css('display', 'none');
                $('#tdDoctor').css('display', 'none');
                $('#tdDate').css('display', '');
                $('#tdTrackStatus').css('display', 'none');

                ValidatorEnable($('[id$=rfvDoctor]')[0], false);
                ValidatorEnable($('[id$=rfvTrackStatus]')[0], false);
                ValidatorEnable($('[id$=rfvDaTe]')[0], true);
            }
            else if ($('#<%=ddlFilter.ClientID%>').val() == 'TrackStatus') {
                $('#tdTextBox').css('display', 'none');
                $('#tdDoctor').css('display', 'none');
                $('#tdDate').css('display', 'none');
                $('#tdTrackStatus').css('display', '');

                ValidatorEnable($('[id$=rfvDoctor]')[0], false);
                ValidatorEnable($('[id$=rfvTrackStatus]')[0], true);
                ValidatorEnable($('[id$=rfvDaTe]')[0], false);
            }
            else {
                $('#tdTextBox').css('display', '');
                $('#tdDoctor').css('display', 'none');
                $('#tdDate').css('display', 'none');
                $('#tdTrackStatus').css('display', 'none');

                ValidatorEnable($('[id$=rfvDoctor]')[0], false);
                ValidatorEnable($('[id$=rfvTrackStatus]')[0], false);
                ValidatorEnable($('[id$=rfvDaTe]')[0], false);
            }
        }
    </script>
    <asp:UpdatePanel ID="upNewCase" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label runat="server" ID="lblNewCaseList" Text="Patient / Case List" meta:resourcekey="lblNewCaseListResource1"></asp:Label></h2>
                    <div id="divMsg" runat="server">
                        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                    </div>
                </div>
                <div class="widecolumn">
                    <div class="personal_box alignleft">
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch" meta:resourcekey="pnlSearchResource1">
                            <table class="alignright" width="100%">
                                <tr>
                                    <td align="center" valign="middle">
                                        <span class="dark_btn_small">
                                            <asp:Button ID="btnShowAll" runat="server" Text="Show All" OnClick="btnShowAll_Click" meta:resourcekey="btnShowAllResource1" />
                                        </span>
                                    </td>
                                    <td align="center" valign="middle" width="35%">
                                        <div class="radio-selection" style="width: 100%">
                                            <asp:CheckBox ID="chkCompleted" runat="server" Text="Completed Cases" OnCheckedChanged="chkCompleted_CheckedChanged" AutoPostBack="true" meta:resourcekey="chkCompletedResource1" />
                                            <asp:CheckBox ID="chkDiscounted" runat="server" Text="Discounted Cases" OnCheckedChanged="chkCompleted_CheckedChanged" AutoPostBack="true" meta:resourcekey="chkDiscountedResource1" />
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <div class="parsonal_selectSmallSearch">
                                                <asp:DropDownList ID="ddlFilter" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="Select Search Type"></asp:ListItem>
                                                    <asp:ListItem Value="PatientName" Text="Patient Name"></asp:ListItem>
                                                    <asp:ListItem Value="Doctor" Text="Doctor"></asp:ListItem>
                                                    <asp:ListItem Value="CreatedDate" Text="Case Date"></asp:ListItem>
                                                    <asp:ListItem Value="TrackStatus" Text="TrackStatus"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td id="tdTextBox" align="center" valign="middle">
                                        <div class="parsonal_textfildLarge">
                                            <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" Width="170px" meta:resourcekey="txtSearchValResource1"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td id="tdDoctor" align="center" valign="middle">
                                        <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <div class="parsonal_selectSmallSearch" style="width: 170px">
                                                <asp:DropDownList ID="ddlDoctor" runat="server" Style="width: 170px" meta:resourcekey="ddlDoctorResource1"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDoctor" ForeColor="Red" runat="server" SetFocusOnError="True"
                                                    ControlToValidate="ddlDoctor" Display="None" InitialValue="0" ErrorMessage="Please select doctor."
                                                    CssClass="errormsg" ValidationGroup="searchValidation" meta:resourcekey="rfvDoctorResource1"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="customCalloutStyle"
                                                    TargetControlID="rfvDoctor" Enabled="True" PopupPosition="BottomLeft">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                    </td>
                                    <td id="tdTrackStatus" align="center" valign="middle">
                                        <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <div class="parsonal_selectSmallSearch" style="width: 170px">
                                                <asp:DropDownList ID="ddlTrackStatus" runat="server" Style="width: 170px">
                                                    <asp:ListItem Text="- Select -" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Submitted" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Acknowledged" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="InProcess" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Shipped" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvTrackStatus" ForeColor="Red" runat="server" SetFocusOnError="True"
                                                    ControlToValidate="ddlTrackStatus" Display="None" InitialValue="0" ErrorMessage="Please select tracking status."
                                                    CssClass="errormsg" ValidationGroup="searchValidation" meta:resourcekey="rfvTrackStatusResource1"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="cmverfvTrackStatus" runat="server" CssClass="customCalloutStyle"
                                                    TargetControlID="rfvTrackStatus" Enabled="True" PopupPosition="BottomLeft">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                    </td>
                                    <td id="tdDate" align="center" valign="middle">
                                        <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <asp:TextBox ID="txtDateSelect" CssClass="From-Date not-edit textfild search-datepicker" runat="server" MaxLength="50" Width="170px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDaTe" ForeColor="Red" runat="server" SetFocusOnError="True"
                                                ControlToValidate="txtDateSelect" Display="None" InitialValue="0" ErrorMessage="Please select date."
                                                CssClass="errormsg" ValidationGroup="searchValidation" meta:resourcekey="rfvDaTeResource1"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="customCalloutStyle"
                                                TargetControlID="rfvDaTe" Enabled="True" PopupPosition="BottomLeft">
                                            </ajaxToolkit:ValidatorCalloutExtender>
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
                        <div class="clear"></div>
                        <div class="list-data">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left" valign="top" class="rgt" width="100%">
                                        <asp:ListView ID="lvNewCase" runat="server" DataKeyNames="CaseId" DataSourceID="odsNewCase"
                                            OnPreRender="lvNewCase_PreRender" OnItemDataBound="lvNewCase_ItemDataBound">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label ID="lblCaseNo" runat="server" Text="Case No" meta:resourcekey="lblCaseNoResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:LinkButton ID="lnkPatientName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="PatientName" Text="Patient Name" meta:resourcekey="lnkPatientNameResource2" />
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:LinkButton ID="lnkRegistrationDate" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="CaseDate" Text="Case Date" meta:resourcekey="lnkRegistrationDateResource2" />
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:LinkButton ID="lnkDoctorName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="DoctorName" Text="Doctor Name" meta:resourcekey="lblDoctorResource1" />
                                                        </td>
                                                        <td align="right" valign="middle" class="equip" id="thTotalAmount" runat="server" visible="false">
                                                            <asp:LinkButton ID="lnkTotalAmount" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="TotalAmount" Text="Total Amount ($)" meta:resourcekey="lnkTotalAmountResource1" />
                                                        </td>
                                                        <td align="right" valign="middle" class="equip" id="thDiscountAmount" runat="server" visible="false">
                                                            <asp:LinkButton ID="lnkDiscountAmt" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="DiscountAmount" Text="Discount ($)" meta:resourcekey="lnkDiscountAmtResource1" />
                                                        </td>
                                                        <td align="right" valign="middle" class="equip">
                                                            <asp:LinkButton ID="lnkPaymentAmount" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="PaymentAmount" Text="Payment Amount" meta:resourcekey="lnkPaymentAmountResource2" />
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:LinkButton ID="lnkPaymentDate" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="PaymentDate" Text="Payment Date" meta:resourcekey="lnkPaymentDateResource2" />
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label ID="lblTrackStatus" runat="server" Text="Track Status" meta:resourcekey="lblTrackStatusResource3"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label runat="server" ID="lblAction" meta:resourcekey="Action"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="16" align="right">
                                                            <asp:DataPager ID="lvPackageDataPager" runat="server" PagedControlID="lvNewCase">
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
                                                    <td align="left" valign="middle" class="equipbg">
                                                        <%#Eval("CaseNo") %>
                                                    </td>
                                                    <td align="left" valign="middle" class="equipbg">
                                                        <%# Eval("PatientName") %>
                                                    </td>
                                                    <td align="center" valign="middle" class="equipbg">
                                                        <%# Convert.ToDateTime(Eval("CaseDate")).ToString("MM/dd/yyyy") %>
                                                    </td>
                                                    <td align="left" valign="middle" class="equipbg">
                                                        <%#Eval("DoctorName") %>                                                        
                                                    </td>
                                                    <td align="right" valign="middle" class="equipbg" id="tdTotalAmount" runat="server" visible="false">
                                                        <%# Eval("TotalAmount") != null ? Convert.ToDecimal(Eval("TotalAmount")).ToString("0.00") : "" %>
                                                    </td>
                                                    <td align="right" valign="middle" class="equipbg" id="tdDiscountAmount" runat="server" visible="false">
                                                        <%# Eval("DiscountAmount") != null ? Convert.ToDecimal(Eval("DiscountAmount")).ToString("0.00") : "" %>
                                                    </td>
                                                    <td align="right" valign="middle" class="equipbg">
                                                        <%# Eval("Ammount") != null ? Convert.ToDecimal(Eval("Ammount")).ToString("0.00") : "" %>
                                                    </td>
                                                    <td align="center" valign="middle" class="equipbg">
                                                        <%# Eval("PaymentDate") != null ? Convert.ToDateTime(Eval("PaymentDate")).ToString("MM/dd/yyyy") : "" %>
                                                    </td>
                                                    <td align="center" valign="middle" class="equipbg">
                                                        <asp:Label ID="lblTrackStatus" runat="server" meta:resourcekey="lblTrackStatusResource2"></asp:Label>
                                                        <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("Status") %>' />
                                                    </td>
                                                    <td align="center" valign="middle" class="equipbg">
                                                        <table>
                                                            <tr>
                                                                <td style="width: 30%;">
                                                                    <img src='<%# Convert.ToInt64(Eval("PaymentId")) > 0 ? "../Content/Images/payment-recieved.png" : "../Content/Images/payment-not-recieved.png" %>' alt='<%# Convert.ToInt64(Eval("PaymentId")) > 0 ? this.GetLocalResourceObject("PaymentReceived") : this.GetLocalResourceObject("PaymentNotReceived") %>'
                                                                        title='<%# Convert.ToInt64(Eval("PaymentId")) > 0 ? this.GetLocalResourceObject("PaymentReceived") : this.GetLocalResourceObject("PaymentNotReceived") %>' /></td>
                                                                <td style="width: 30%;">
                                                                    <asp:LinkButton ID="lnkUpdateStatus" runat="server" CommandName="UpdateStatus" CommandArgument='<%# Eval("TrackId") %>'
                                                                        Text="Track" OnCommand="Custom_Command" Style="color: blue; text-decoration: underline;" meta:resourcekey="lnkUpdateStatusResource1"></asp:LinkButton></td>
                                                                <td style="width: 33%; text-align: center; margin: 0 auto;">
                                                                    <asp:ImageButton ID="btnShareCase" runat="server" ImageUrl="../Content/Images/share16.png" Style='<%# Convert.ToBoolean(Eval("IsShared")) ? "display: inline": "display: none" %>'
                                                                        ToolTip="Case Shared" CommandName="share" Visible='<%# Eval("CaseId") != null && Convert.ToInt64(Eval("CaseId")) > 0 %>'
                                                                        CommandArgument='<%# Eval("CaseId") %>' OnCommand="Custom_Command" />
                                                                </td>

                                                                 <td style="width: 33%; text-align: center; margin: 0 auto;">
                                                                    <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../Content/Images/add.png" Style='<%# Convert.ToBoolean(Eval("IsShared")) ? "display: inline": "display: none" %>'
                                                                        ToolTip="Case Shared" CommandName="share" Visible='<%# Eval("CaseId") != null && Convert.ToInt64(Eval("CaseId")) > 0 %>'
                                                                        CommandArgument='<%# Eval("CaseId") %>' OnCommand="Custom_Command" />--%>

                                                                       <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../Content/images/add.png" 
                                                                ToolTip="Track Stage" CommandName="Stage" Visible='<%# Eval("CaseId") != null && Convert.ToInt64(Eval("CaseId")) > 0 %>'
                                                                CommandArgument='<%# Eval("CaseId") %>'  
                                                               OnCommand="Custom_Command" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip" style="width: 15%">
                                                            <asp:Label ID="lblCaseId" runat="server" Text="Case No" meta:resourcekey="lblCaseIdResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" style="width: 200px">Patient Name
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" style="width: 200px">
                                                            <asp:Label ID="lblPatientRegDate" runat="server" Text="Case Registered Date" meta:resourcekey="lnkLastNameResource1" />
                                                        </td>
                                                        <td align="right" valign="middle" class="equip">
                                                            <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount ($)" meta:resourcekey="lnkTotalAmountResource1" />
                                                        </td>
                                                        <td align="right" valign="middle" class="equip">
                                                            <asp:Label ID="lblDiscountAmt" runat="server" Text="Discount ($)" meta:resourcekey="lnkDiscountAmtResource1" />
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label ID="lblAmount" runat="server" Text="Amount" meta:resourcekey="lblAmountResource1"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip">
                                                            <asp:Label ID="lblPaymentDate" runat="server" Text="Payment Date" meta:resourcekey="lblPaymentDateResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <asp:Label ID="lblTrackStatus" runat="server" Text="Track Status" meta:resourcekey="lblTrackStatusResource1"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip">
                                                            <%= this.GetLocalResourceObject("Action.Text") %>
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
                                        <asp:ObjectDataSource ID="odsNewCase" runat="server" SelectMethod="GetAllNewCase"
                                            SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.Admin.ListNewCaseDetails" OnSelecting="odsNewCase_Selecting">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
