<%@ Page Title="" Language="C#" MasterPageFile="~/Ortho.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_4eOrtho.Default" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="ltrCMSContent" runat="server" EnableViewState="False"></asp:Literal>
    <script src="//f.vimeocdn.com/js/froogaloop2.min.js"></script>
   <%-- <script type="text/javascript">
        $(document).ready(function () {
            var iframe = $f('#player1')[0];
            var player = $(iframe);
            
            player.addEvent('ready', function () {
                player.addEvent('finish', onFinish);
            });
            function onFinish(id) {
                alert("In");
            }
        });
    </script>--%>
</asp:Content>
