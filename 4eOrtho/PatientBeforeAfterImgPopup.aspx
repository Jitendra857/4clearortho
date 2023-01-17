<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopUp.Master" AutoEventWireup="true" CodeBehind="PatientBeforeAfterImgPopup.aspx.cs" Inherits="_4eOrtho.PatientBeforeAfterImgPopup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

    <div id="printablediv">
        <style type="text/css">
            .leftSection {
                float: left;
                width: 820px;
                height: 100%;
            }

            .rightSection {
                margin-top: 20px;
                float: right;
                width: 50px;
                height: 100%;
            }

                .rightSection input {
                    margin-bottom: 10px;
                }

            .imgbox {
                /*border: 1px solid black;*/
                float: left;
                height: 173px;
                list-style-type: none;
                margin: 5px;
                width: 260px;
            }

                .imgbox img {
                    height: 173px;
                    width: 260px;
                }
        </style>
        <asp:Panel ID="pnlPDF" runat="server">
            <div class="leftSection">
                <div class="imgbox">
                    <asp:Image ID="Image1" runat="server" AlternateText="" />
                </div>
                <div class="imgbox">
                    <asp:Image ID="Image2" runat="server" AlternateText="" />
                </div>
                <div class="imgbox">
                    <asp:Image ID="Image3" runat="server" AlternateText="" />
                </div>
                <div class="imgbox">
                    <asp:Image ID="Image4" runat="server" AlternateText="" />
                </div>
                <div class="imgbox" style="text-align: center; background-color: #164D8E;">
                    <div style="padding-top: 10px;">
                        <asp:Label ID="lblBeforeAfter" runat="server" Style="font-size: 20px; font-weight: bold; color: white;">Treatment</asp:Label><br />
                        <img src="Content/images/logo.png" style="padding-top: 5px; width: 30%; height: 30%;" /><br />
                        <br />
                        <asp:Label ID="lblDoctorName" runat="server" Style="font-size: 20px; font-weight: bold; color: white;">Dr.</asp:Label><br />
                        <asp:Label ID="lblPatientName" runat="server" Style="font-size: 17px; color: white; font-weight: bold;">Patient :</asp:Label>
                        <br />
                        <asp:Label ID="lblCreatedDate" runat="server" Style="font-size: 17px; color: white; font-weight: bold;">Created Dt.:</asp:Label>
                    </div>
                </div>
                <div class="imgbox">
                    <asp:Image ID="Image5" runat="server" AlternateText="" />
                </div>
                <div class="imgbox">
                    <asp:Image ID="Image6" runat="server" AlternateText="" />
                </div>
                <div class="imgbox">
                    <asp:Image ID="Image7" runat="server" AlternateText="" />
                </div>
                <div class="imgbox">
                    <asp:Image ID="Image8" runat="server" AlternateText="" />
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="rightSection">
        <input type="image" class="btn" onclick="javascript: printDiv('printablediv')" title="Print" id="Submit1" value="Print" src="Content/images/print.png" />
        <asp:ImageButton ID="btnDownload" class="btn" runat="server" Text="Download" ToolTip="Download PDF" OnClick="btnDownload_Click" ImageUrl="~/Content/images/download.png" />
    </div>
</asp:Content>
