<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="PaymentFailure.aspx.cs" Inherits="_4eOrtho.PaymentFailure" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="left_title">
        <h2>
            <asp:Label ID="lblTitle" runat="server" Text="Payment Failed" meta:resourcekey="lblTitleResource1"></asp:Label>
        </h2>
    </div>
    <div id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
    </div>
    <div class="date2">
        <h4 class="section-ttl">
            <asp:Label ID="lblErrorMsg" runat="server" Text="Error Message" meta:resourcekey="lblErrorMsgResource1"></asp:Label>
        </h4>
    </div>
    <div class="date2">
        <asp:Literal ID="ltrErrorMsg" runat="server" meta:resourcekey="ltrErrorMsgResource1"></asp:Literal>
    </div>
    <div class="date2">
        <p>
            <asp:Label ID="lblDifferentCard" runat="server"
                Text="Try again with the same or a different card" meta:resourcekey="lblDifferentCardResource1"></asp:Label>
        </p>
    </div>
    <div class="date2">
        <div class="Search_button" style="margin-left:0px;">
            <asp:Button ID="btnBack" runat="server" Text="Done" OnClick="btnBack_Click" meta:resourcekey="btnBackResource1" />
        </div>
    </div>
</asp:Content>
