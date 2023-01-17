<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="StagePaymentReceipt.aspx.cs" Inherits="_4eOrtho.Admin.StagePaymentReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css"/>
<script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
<script src="//code.jquery.com/jquery-1.11.1.min.js"></script>

    <style type="text/css">
        .well {
    min-height: 25px;
    padding: 27px;
    margin-bottom: 0px;
    /* background-color: #f5f5f5; */
    border: 1px solid #e3e3e3;
    border-radius: 4px;
    -webkit-box-shadow: inset 0 1px 1px rgb(0 0 0 / 5%);
    box-shadow: inset 0 1px 1px rgb(0 0 0 / 5%);
    margin-top: 0px;
}
        .btn-lg {
    padding: 1px 8px;
    font-size: 17px;
    line-height: 1.33;
    border-radius: 6px;
}
        .page_title {
    border-bottom: 1px solid #DDDDDD;
    clear: both;
    direction: ltr;
    height: 50px;
    margin-bottom: 10px;
    overflow: hidden;
    padding-bottom: 5px;
    width: 100%;
    float:left;
}
        #container h2 {
    color: #333333;
    direction: ltr;
    font-size: 22px;
    font-weight: normal;
    line-height: 18px;
    width:40%;
    float:left
}
      

button, input[type="button"], input[type="reset"], input[type="submit"] {
    direction: ltr;
    cursor: pointer;
    -webkit-appearance: button;
    *overflow: visible;
}
.btnback
{
    background-color: black;
    color: white;
    border: 10px solid white;
    margin-top: 0px;
    padding: 7px 23px 8px 19px;
    float:right
}

        </style>
 

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
   
    <asp:UpdatePanel ID="upListRecommendedDentist" runat="server">
        <ContentTemplate>


<div id="container" class="cf" >
     <div class="page_title">
                    <h2 >
                        <asp:Label ID="Label1" Text="Patient Payment Receipt" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label>


                    </h2>
                    
    <asp:Button ID="btnBack" CssClass="btnback" runat="server" Text="Back" PostBackUrl="PatientStageList.aspx"  TabIndex="7" meta:resourcekey="btnBackResource1" />
                </div>
    
        <div class="well col-xs-10 col-sm-10 col-md-12 col-xs-offset-1 col-sm-offset-1 col-md-offset-0" id="printablediv">
            <div class="row">
                <div class="col-xs-6 col-sm-6 col-md-6">
                    <address>
                        <strong><asp:Label id="lblpatientname" 
                    runat="server" /></strong>
                        <br>
                      <asp:Label id="lblemail" 
                    runat="server" />
                       
                        <br>
                       Transaction Id:  <asp:Label id="lbltransactionid" 
                    runat="server" />
                    </address>
                </div>
                <div class="col-xs-6 col-sm-6 col-md-6 text-right">
                    <p>
                        <em>Date:  <asp:Label id="lblpaymentdate" 
                    runat="server" /></em>
                    </p>
                    <p>
                        <em>Acknowledgement :  <asp:Label id="lblacknowledgement" 
                    runat="server" /></em>
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="text-center">
                    <h1>Receipt</h1>
                </div>
                </span>
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th>Product</th>
                            <th>#</th>
                            <th class="text-center">Price</th>
                            <th class="text-center">Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="col-md-9"><em> <asp:Label id="lblstagename" 
                    runat="server" /></em></h4></td>
                            <td class="col-md-1" > 1 </td>
                            <td class="col-md-1 text-center"><asp:Label id="lblprice" 
                    runat="server" /></td>
                            <td class="col-md-1 text-center"> <asp:Label id="lbltotalprice" 
                    runat="server" /></td>
                        </tr>
                       
                       <%-- <tr>
                            <td>   </td>
                            <td>   </td>
                            <td class="text-right">
                            <p>
                                <strong>Subtotal: </strong>
                            </p>
                            <p>
                                <strong>Tax: </strong>
                            </p></td>
                            <td class="text-center">
                            <p>
                                <strong>$6.94</strong>
                            </p>
                            <p>
                                <strong>$6.94</strong>
                            </p></td>
                        </tr>--%>
                        <tr>
                            <td>   </td>
                            <td>   </td>
                            <td class="text-right"><h4><strong>Total: </strong></h4></td>
                            <td class="text-center text-danger"><h4><strong> <asp:Label id="lblgranttotal" 
                    runat="server" /></strong></h4></td>
                        </tr>
                    </tbody>
                </table>
               
            </div>
        </div>
        <button type="button" onclick="javascript: printDiv('printablediv')" class="btn btn-primary btn-lg btn-block">
                    Print  <img src="images/printwhite.png" />   <%--<span class="glyphicon glyphicon-chevron-right"></span>--%>
                </button>
   
  </div>
     </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function printDiv(printablediv) {
            //Get the HTML of div
            var divElements = document.getElementById(printablediv).innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;
            //Reset the page's HTML with div's HTML only
            document.body.innerHTML =
              "<html><head><title></title></head><body>" +
              divElements + "</body>";
            //Print Page
            window.print();
            //Restore orignal HTML
            document.body.innerHTML = oldPage;
        }
    </script>
</asp:Content>