<%@ Page Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true"  CodeBehind="PatientStageDetails.aspx.cs" Inherits="_4eOrtho.PatientStageDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
#ContentPlaceHolder1_GridView1 td{
  /*color:white !important*/
  text-align:center !important
}
.GridHeader
{
    text-align:center !important;    
}

/*#ContentPlaceHolder1_GridView1 tr:last-child {
   background-color:white;
}

#ContentPlaceHolder1_GridView1 tr:last-child td {
  color:black;
  font-weight:bold
}*/
#ContentPlaceHolder1_GridView1{
   
    color: #333333;
    width: 600px !important;
    border-collapse: collapse;

}
#ContentPlaceHolder1_btnAddNewCase{
    margin-right:10px !important
}
.main_right_cont {
    width: 600px;
    float: left;
    margin: 0 0 0 -48px !important;
    /* width: 100%; */
}
span.one {
    color: #fff;
    font-size: 13px;
    float: left;
    margin: 25px 0px 0px 6px;
    font-weight: bold;
    width: 100%;
}
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
<asp:UpdatePanel ID="upNewCase" runat="server">
        <ContentTemplate>
            <div class="rightbar">
                <div class="main_right_cont" style="width: 111%;">
                    <div class="title">
                        <div class="supply-button2 back">

                            <%
if(this.Session["IsLoggedIn"].ToString()=="D")
{%>


                            <asp:Button ID="btnAddNewCase" runat="server" Text="Patient/New Stage" PostBackUrl="AddUpdateStage.aspx" ToolTip="Add Patient/New Case"  meta:resourcekey="btnAddNewCaseResource1" />
                   
                             <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/ListNewCase.aspx" TabIndex="7" meta:resourcekey="btnBackResource1" />
                       

<% } 
else 
{
%>

<% } %>
                             </div>
                     
                        <h2>
                            <asp:Label runat="server" ID="lblProductList" Text="Patient / Stage List" meta:resourcekey="lblProductListResource1"></asp:Label>
                        </h2>
                    </div>
                    <%--<asp:Panel ID="pnlSearch" DefaultButton="btnSearch" runat="server" meta:resourcekey="pnlSearchResource1">
                        <div class="date2">
                          
                            <div class="parsonal_textfild" style="padding: 0; float: right; width: 54%;">
                                <div class="date_cont_right">
                                    <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" meta:resourcekey="txtSearchValResource1"></asp:TextBox>
                                </div>
                                <div class="Search_button" style="margin-left: 0px;">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ToolTip="Search" ValidationGroup="searchValidation" CssClass="btn"  meta:resourcekey="btnSearchResource1"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>--%>
                    <div class="entry_form">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grid_table">
                            <tbody>
                              <asp:ListView ID="lvCustomers" runat="server" GroupPlaceholderID="groupPlaceHolder1"
ItemPlaceholderID="itemPlaceHolder1" OnPagePropertiesChanging="OnPagePropertiesChanging">
                                    <LayoutTemplate>
                                        <tr>
                                            <%--<td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblCaseNo" runat="server" Text="Stage Id." meta:resourcekey="lblCaseNoResource2"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>--%>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkCreatedDate" runat="server" 
                                                            CommandArgument="CreatedDate" Text="Stage Name" meta:resourcekey="lblCreatedDateResource2" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkFirstName" runat="server" 
                                                            CommandArgument="FirstName" Text="Stage Price" meta:resourcekey="lnkFirstNameResource2" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="lnkLastName" runat="server" 
                                                            CommandArgument="LastName" Text="Is Dispetched?" meta:resourcekey="lnkLastNameResource2" />
                                                    </span>
                                                </div>
                                            </td>
                                             <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="LinkButton2" runat="server" 
                                                            CommandArgument="Status" Text="Is Received?" meta:resourcekey="lnkLastNameResource2" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:LinkButton ID="LinkButton4" runat="server" 
                                                            CommandArgument="Status" Text="Patient Payment" meta:resourcekey="lnkLastNameResource2" />
                                                    </span>
                                                </div>
                                            </td>
                                            <td style="width: auto" >
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="Label1" runat="server"
                                                            Text="Status" meta:resourcekey="lnkLastNameResource2" />
                                                    </span>
                                                </div>
                                            </td>
                                             <td style="width: auto" colspan="3">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="LinkButton1" runat="server"
                                                            Text="Action" meta:resourcekey="lnkLastNameResource2" />
                                                    </span>
                                                </div>
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
                                    </LayoutTemplate>
                                  <GroupTemplate>
    <tr>
        <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
    </tr>
</GroupTemplate>
                                    <ItemTemplate>
                                        <tr>
                                           <%-- <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%#Eval("StageId") %>
                                                    </div>
                                                </div>
                                            </td>--%>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%#Eval("StageName") %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                       $<%#Convert.ToInt16(Eval("StageAmount")) %>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        <%--<%#Eval("IsDispetched") %>--%>
                                                        <span class="label <%#Convert.ToBoolean(Eval("IsDispetched"))==true?"success":"warning"%>"><%#Convert.ToBoolean(Eval("IsDispetched"))==true?"Yes":"No"%></span>

                                                    </div>
                                                </div>
                                            </td>
                                           
                                             <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                      <%--  <%# Eval("IsReceived") %>--%>
                                                        <span class="label <%#Convert.ToBoolean(Eval("IsReceived"))==true?"info":"warning"%>"><%#Convert.ToBoolean(Eval("IsReceived"))==true?"Yes":"No"%></span>

                                                        
                                                    </div>
                                                </div>
                                            </td>
                                             <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                       <%-- <%# Eval("isPaymentByPatient") %>--%>

                                                        <span class="label <%#Convert.ToBoolean(Eval("isPaymentByPatient"))==true?"info":"warning"%>"><%#Convert.ToBoolean(Eval("isPaymentByPatient"))==true?"Paid":"Pending"%></span>

                                                        
                                                    </div>
                                                </div>
                                            </td>
                                             <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex">
                                                    <div class="whitetext">
                                                        
                                                       <%-- <span class="label success"><%# Eval("Status").ToString()=="0"?"Submitted"?InProcess":"Completed"%></span>--%>
                                                        <span class="label <%#Convert.ToInt16(Eval("Status"))==0?"info": Convert.ToInt16(Eval("Status"))==1?"warning":"success"%>"><%#Convert.ToInt16(Eval("Status"))==0?"Submitted": Convert.ToInt16(Eval("Status"))==1?"In Process":"Completed"%></span>
                                                        
                                                    </div>
                                                </div>
                                            </td>
                                            <%
if(this.Session["IsLoggedIn"].ToString()=="P")
{%>

<td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex" style="text-align: right;">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="ImageButton2" ToolTip="Pay " runat="server" ImageUrl="Content/images/pay-button.png" Width="70"
                                                               Visible='<%# ((bool)Eval("IsReceived"))==true? false : true %>'   CommandName="Pay" CommandArgument='<%# Eval("StageId") %>' OnCommand="Custom_Command" />

                                                             <asp:ImageButton ID="ImageButton3" ToolTip="View Receipt " runat="server" CssClass="imgspace" ImageUrl="Content/images/viewinvoice.png" Width="30"
                                                               Visible='<%# ((bool)Eval("IsReceived"))==true? true : false %>'   CommandName="ViewReceipt" CommandArgument='<%# Eval("StageId") %>' OnCommand="Custom_Command" />

                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>

<% } 
else 
{
%>
 <td>
     
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex" style="text-align: right;">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="imgbtnReceived" ToolTip="Edit Stage" runat="server" ImageUrl="Content/images/updatenew.png" Width="25"
                                                                CommandName="EDIT" CommandArgument='<%# Eval("StageId") %>' OnCommand="Custom_Command"
                                                                Visible='<%# ((bool)Eval("IsReceived"))==true? false : true %>' />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>

                                              <td>
                                                <div class="<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light"  %>" id="flex" style="text-align: right;">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="ImageButton1" ToolTip="Remove Stage" runat="server" ImageUrl="Content/images/deletenew.png" Width="25"
                                                              Visible='<%# ((bool)Eval("IsReceived"))==true? false : true %>'   CommandName="REMOVE" CommandArgument='<%# Eval("StageId") %>' OnCommand="Custom_Command" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
<% } %>

                                         
                                             
                                          
                                            
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
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
                                    </EmptyDataTemplate>
                                </asp:ListView>
                                
                            </tbody>
                        </table>
                    </div>
                   
                   
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

