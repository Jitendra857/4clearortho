<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="PatientStagePaymentReport.aspx.cs" Inherits="_4eOrtho.Admin.PatientStagePaymentReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../Styles/Jquery-UI/jquery-ui-1.8.23.custom.css" rel="Stylesheet" type="text/css" />


    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {
            load();
        });

        function Confirm() {
            if (confirm("Are you sure?")) {
                return true;
            }
            else {
                return false;
            }
        }

        function pageLoad() {
            load();
        }

        function load() {
            $('#txtstartdate').datepicker({
                showOn: "button",
                buttonText: 'Select Date',
                buttonImage: "/admin/images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true
            });
            $('#txtendate').datepicker({
                showOn: "button",
                buttonText: 'Select Date',
                buttonImage: "/admin/images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true
            });
        }

    </script>

    <style type="text/css">
        .label {
    color: white;
    padding: 4px;
    font-size: 12px;
    font-family: Arial;
}
.success {
    background-color: #04AA6D;
}
.success {background-color: #04AA6D;} /* Green */
.info {background-color: #2196F3;} /* Blue */
.warning {background-color: #ff9800;} /* Orange */
.danger {background-color: #f44336;} /* Red */
.other {background-color: #e7e7e7; color: black;} /* Gray */

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upListPatientCaseCashReport" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label ID="lblHeader" Text="Patient Stage Payment Report" runat="server"></asp:Label></h2>
                    <div id="divMsg" runat="server">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="widecolumn">
                    <div class="personal_box alignleft">
                        <table>
                            <tr>
                                <td align="left" valign="middle">
                                    <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                        <asp:Label runat="server" ID="lblStartDate" Text="Start Date :"></asp:Label>
                                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtstartdate" Enabled="false" CssClass="To-Date not-edit textfild search-datepicker" Width="70px"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="Requiredstartdate" Display="None" ControlToValidate="txtstartdate"
                                            SetFocusOnError="True" CssClass="error" ValidationGroup="validation" ClientIDMode="Static"
                                            ErrorMessage="Please select start date."></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="customCalloutStyle"
                                            TargetControlID="Requiredstartdate" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </div>
                                </td>
                                <td>
                                    <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                        <asp:Label runat="server" ID="lblEndDate" Text="End Date :"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtendate" ClientIDMode="Static" Enabled="false" CssClass="To-Date not-edit textfild search-datepicker" Width="70px"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="Requiredenddate" Display="None" ControlToValidate="txtendate"
                                            SetFocusOnError="True" CssClass="error" ValidationGroup="validation" ClientIDMode="Static"
                                            ErrorMessage="Please select end date."></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="customCalloutStyle"
                                            TargetControlID="Requiredenddate" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                        <asp:CompareValidator ID="cmpvaldate" runat="server" SetFocusOnError="True" CssClass="error" ValidationGroup="validation" ClientIDMode="Static"
                                            ControlToValidate="txtstartdate" ControlToCompare="txtendate" Operator="LessThan" Type="Date" Display="None"
                                            ErrorMessage="Start date must be less than End date."></asp:CompareValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="customCalloutStyle"
                                            TargetControlID="cmpvaldate" Enabled="True">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </div>
                                </td>
                                <td align="center" valign="middle">
                                    <span class="dark_btn_small">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="validation" OnClick="btnSearch_Click"></asp:Button>
                                    </span>
                                </td>                                
                                <td align="center" valign="middle">
                                    <div class="parsonal_textfild" style="padding: 0 0 0 0;margin-left:20px;">
                                        <div class="parsonal_selectSmallSearch">
                                            <asp:DropDownList ID="ddlPaymentStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentStatus_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="-- Select Payment Status --"></asp:ListItem>
                                                <asp:ListItem Value="RECEIVED" Text="Received"></asp:ListItem>
                                                <asp:ListItem Value="PENDING" Text="Pending"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </td>
                                <td align="center" valign="middle">
                                    <span class="dark_btn_small">
                                        <asp:Button ID="btnshowall" runat="server" Text="Show All" OnClick="btnshowall_Click" />
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <div class="clear">
                        </div>
                        <div class="list-data">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left" valign="top" class="rgt">
                                        <asp:ListView ID="lvPatientCaseCashReport" DataSourceID="odsPatientCaseCashReport" runat="server" OnPreRender="lvPatientCaseCashReport_PreRender">
                                            <LayoutTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:LinkButton ID="lnkName" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Name" Text="Patient Name"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:LinkButton ID="lnkEmail" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Email" Text="Stage Name"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="15%">
                                                            <asp:Label ID="lblCaseNo" runat="server" Text="Status"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblCashAmount" runat="server" Text="Stage Amount"></asp:Label>
                                                        </td>
                                                         <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="Label3" runat="server" Text="Payment Date"></asp:Label>
                                                        </td>
                                                       <%-- <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lnkStatus" runat="server" Text="Payment Status"></asp:Label>
                                                        </td>
                                                        <td align="center" valign="middle" class="equip" width="10%">
                                                            <asp:Literal ID="ltrAction" runat="server" Text="Action"></asp:Literal>
                                                        </td>--%>
                                                    </tr>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                    <tr class="equip-paging">
                                                        <td colspan="6" align="right">
                                                            <asp:DataPager ID="dpPatientCaseCashReport" runat="server" PagedControlID="lvPatientCaseCashReport">
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
                                                        <%# Eval("Name")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("StageName")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg">
                                                     <%--   <%# Eval("Status") %>--%>

                                                        <span class="label <%#Convert.ToInt16(Eval("Status"))==0?"info": Convert.ToInt16(Eval("Status"))==1?"warning":"success"%>"><%#Convert.ToInt16(Eval("Status"))==0?"Submitted": Convert.ToInt16(Eval("Status"))==1?"In Process":"Completed"%></span>
                                                    </td>
                                                    <td align="center" valign="top" class="equipbg">
                                                       $<%# Convert.ToInt32(Eval("Ammount")) %>
                                                    </td>
                                                     <td align="center" valign="top" class="equipbg">
                                                       <%#Eval("CreatedDate") %>
                                                    </td>
                                                   <%-- <td align="center" valign="top" class="equipbg">
                                                        <asp:Image ID="imginactive" runat="server" ToolTip='<%# Eval("Status").ToString().Equals("RECEIVED") ? "Payment Received" : "Payment Pending"  %>' ImageUrl='<%# Eval("Status").ToString().Equals("RECEIVED") ? "../Content/Images/payment-recieved.png" : "../Content/Images/payment-not-recieved.png" %>' />
                                                    </td>--%>
                                                  <%--  <td align="center" valign="top" class="equipbg">
                                                        <asp:LinkButton runat="server" ID="lbtnReceive" Text="Click To Receive" OnCommand="Custom_Command"
                                                            CommandName="Receive" CommandArgument='<%# Eval("PaymentId") %>' OnClientClick="return Confirm();" Visible='<%# !Eval("Status").ToString().Equals("RECEIVED") %>'></asp:LinkButton>
                                                    </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="middle" class="equip" width="20%">
                                                            <asp:Label ID="lblFullName" runat="server" Text="Patient Name"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="20%">
                                                            <asp:Label ID="lblEmail" runat="server" Text="Stage Name"></asp:Label>
                                                        </td>

                                                        <td align="left" valign="middle" class="equip" width="30%">
                                                            <asp:Label ID="Label1" runat="server" Text="Status"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="Label2" runat="server" Text="Amount"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblStatus" runat="server" Text="Payment Date"></asp:Label>
                                                        </td>
                                                        <%--<td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                                        </td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="middle" class="equipbg" colspan="10">
                                                            <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                        <asp:ObjectDataSource ID="odsPatientCaseCashReport" runat="server" SelectMethod="GetPatientCaseCashReport"
                                            SelectCountMethod="GetPatientCaseCashReportCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                            TypeName="_4eOrtho.Admin.PatientStagePaymentReport" OnSelecting="odsPatientCaseCashReport_Selecting"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--            <asp:AsyncPostBackTrigger ControlID="lvPatientCaseCashReport" />               
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>