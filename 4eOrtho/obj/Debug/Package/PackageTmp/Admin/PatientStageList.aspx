<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="PatientStageList.aspx.cs" Inherits="_4eOrtho.Admin.PatientStageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

.imgspace
{
    margin-left:15px 
}
</style>
  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
      <%--  $(document).ready(function () {
            $('#' + '<%= ddlRecommendeddentist.ClientID %>').focus();
        });--%>
    </script>

    <asp:UpdatePanel ID="upListRecommendedDentist" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        <h2 class="padd">
                            <asp:Label ID="lblHeader" runat="server" Text="Patient Stage List" meta:resourcekey="lblHeaderResource1"></asp:Label>
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

              <%--  <div class="page_title">
                    <h2 class="padd">
                        <asp:Label ID="lblHeader" Text="Patient Stage List" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
                    <div id="divMsg" runat="server">
                        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                    </div>
                </div>--%>
                <div class="widecolumn">
                    <div class="personal_box  alignleft">
                        <table class="alignleft">
                            <tr>
                                <td align="center" valign="middle">
                                    <span class="blue_btn_small">
                                        <%--<asp:Button ID="btnAddNew" runat="server" Text="Add New" PostBackUrl="~/Admin/AddEditRecommendedDentist.aspx"></asp:Button>--%>
                                    </span>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                            <table class="alignright">
                                <tr>
                                    <td align="center" valign="middle">
                                    
                                    <td align="center" valign="middle">
                                        <div class="parsonal_textfildLarge">
                                            <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" Width="170px"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rqvSearchVal" runat="server" Display="None" ValidationGroup="searchValidation" ControlToValidate="txtSearchVal" 
                                        meta:resourcekey="rqvSearchValResource1" ErrorMessage="Please enter atleast one Search Value."></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveSearchVal" runat="server" CssClass="customCalloutStyle"
                                        TargetControlID="rqvSearchVal" Enabled="True" />--%>
                                        </div>
                                    </td>
                                    <td align="center" valign="middle">
                                        <span class="dark_btn_small">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="searchValidation" 
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
                                        <asp:ListView ID="lvCustomers" runat="server" GroupPlaceholderID="groupPlaceHolder1" OnPreRender="lvCustomers_PreRender"
ItemPlaceholderID="itemPlaceHolder1" OnPagePropertiesChanging="OnPagePropertiesChanging">
                                          <LayoutTemplate>
                                              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                         <tr>
                                                       
                                                        <td align="left" valign="middle" class="equip" width="10">
                                                            <asp:LinkButton ID="lnkName" runat="server" 
                                                                CommandArgument="Name" Text="Stage Name" meta:resourcekey="lnkNameResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:LinkButton ID="lnkamunt" runat="server" 
                                                                CommandName="CustomSort" Text="Stage Price" meta:resourcekey="lnkEmailResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:LinkButton ID="lnkCountry" runat="server" 
                                                                CommandName="CustomSort" Text="Is Payment?" meta:resourcekey="lnkCountryResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:LinkButton ID="lnkState" runat="server"
                                                                CommandName="CustomSort" Text="Is Received?" meta:resourcekey="lnkStateResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle" class="equip" width="10%">
                                                            <asp:LinkButton ID="lnkstatus" runat="server" 
                                                                CommandName="CustomSort" Text="Status" meta:resourcekey="lnkCityResource1"></asp:LinkButton>
                                                        </td>
                                                        <td align="left" valign="middle"  class="equip" width="40%">
                                                    <asp:Literal ID="ltrAction" runat="server" Text="Action" meta:resourcekey="ltrActionResource1"></asp:Literal>
                                                </td>
                                                    </tr>
                                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                        <tr>
                                            <td align="right" colspan="11" class="datapager">
                                                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvCustomers" PageSize="6">
                                                    <Fields>
                                                        <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page" NumericButtonCssClass="button-page" meta:resourcekey="NumericPagerFieldResource1" />
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                    
                                        </tr>
                                                   </table>
                                    </LayoutTemplate>

                                              <GroupTemplate>
    <tr>
        <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
    </tr>
</GroupTemplate>
                                       <ItemTemplate>
                                                <tr>
                                                    <%--<td align="left" valign="top" class="equipbg">
                                                <%# Eval("RecommendId")%>
                                            </td>--%>
                                                    <td align="left" valign="top" class="equipbg" width="25%">
                                                        <%# Eval("StageName")%>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg" width="15%">
                                                       $<%#Convert.ToInt16(Eval("StageAmount")) %>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg" width="15%">
                                                       <%-- <%# Eval("isPaymentByPatient") %>--%>
                                                        <span class="label <%#Convert.ToBoolean(Eval("isPaymentByPatient"))==true?"info":"warning"%>"><%#Convert.ToBoolean(Eval("isPaymentByPatient"))==true?"Paid":"Pending"%></span>
                                                    </td>
                                                    <td align="left" valign="top" class="equipbg" width="20%">
                                                     <%--   <%# Eval("IsReceived") %>'--%>
                                                         <span class="label <%#Convert.ToBoolean(Eval("IsReceived"))==true?"info":"warning"%>"><%#Convert.ToBoolean(Eval("IsReceived"))==true?"Yes":"No"%></span>
                                                    </td>
                                                   <%-- <td align="left" valign="top" class="equipbg">
                                                        <%# Eval("Status") %>
                                                    </td>--%>
                                                    <td align="left" valign="top" class="equipbg" width="15%">
                                               
                                                                                                        <span class="label <%#Convert.ToInt16(Eval("Status"))==0?"info": Convert.ToInt16(Eval("Status"))==1?"warning":"success"%>"><%#Convert.ToInt16(Eval("Status"))==0?"Submitted": Convert.ToInt16(Eval("Status"))==1?"In Process":"Completed"%></span>


                                                         <%--<asp:LinkButton ID="imgbtnDelete" runat="server" ForeColor= "#0084b5" CommandName='<%# ((int)Eval("Status"))==0? "close" : "open" %>' OnCommand="Custom_Command"
                                                            CommandArgument='<%# Eval("StageId") %>' Text='<%# ((int)Eval("Status"))==0? "Open" : "Close" %>' OnClientClick="return confirm('Are you sure you want to change status of this stage?');" meta:resourcekey="lblCreatedDateResource2" />--%>
                                            </td>
                                                    <td align="left" valign="top" class="equipbg" width="40%">
                                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit"   CommandArgument='<%# Eval("StageId") %>'
                                                   Visible='<%# ((bool)Eval("IsReceived"))==true? false : true %>'   ImageUrl= 'images/edit.png' ToolTip="Edit" Style="padding-right: 10px;" OnCommand="Custom_Command"
                                                   meta:resourcekey="imgbtnDeleteResource1" />

                                                        

                                                   
                                                         <asp:ImageButton ID="ImageButton3" ToolTip="View Receipt " runat="server" Height="24px" CssClass="" ImageUrl="images/viewinvoice.png" 
                                                               Visible='<%# ((bool)Eval("IsReceived"))==true? true : false %>'   CommandName="ViewReceipt" CommandArgument='<%# Eval("StageId") %>' OnCommand="Custom_Command" />

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblCaseNo" runat="server" Text="Stage Id." meta:resourcekey="lblCaseNoResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblCreatedDate" runat="server" Text="Stage Name" meta:resourcekey="lblCreatedDateResource2"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lnkFirstName" runat="server" Text="Stage Price" meta:resourcekey="lnkFirstNameResource1" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lnkLastName" runat="server" Text="Is Active" meta:resourcekey="lnkLastNameResource1" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle" class="equipbg" colspan="10">
                                                <div class="grenchk dark" id="flex">
                                                    <div class="whitetext" style="width: 100%; text-align: center;">
                                                        <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                                    </table>
                                    </EmptyDataTemplate>
                                        </asp:ListView>

                                       
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
