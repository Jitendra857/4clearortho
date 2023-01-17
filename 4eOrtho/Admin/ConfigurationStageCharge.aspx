<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/AdminMaster.Master" CodeBehind="ConfigurationStageCharge.aspx.cs" Inherits="_4eOrtho.ConfigurationStageCharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../Styles/Jquery-UI/jquery-ui-1.8.23.custom.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        var ShouldAllowPrefUpdate = true;
        window.onload = function () {
            document.getElementById('<%=txtFees.ClientID%>').focus();
        };
        function pageLoad() {
            var currentDate = new Date('<%= _4eOrtho.BAL.BaseEntity.GetServerDateTime %>');
            currentDate.setFullYear(currentDate.getFullYear() - 18, 11, currentDate.getDate());
            $('.From-Date').datepicker({
                showOn: "button",
                buttonText: "Select Date",
                buttonImage: "images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                minDate: currentDate
            });
        }
    </script>
    <style>
        .parsonal_textfild label {
            width:250px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upFees" runat="server">
        <ContentTemplate>
            <div id="container" class="cf">
                <div class="page_title">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <h2 class="padd">
                                    <asp:Label ID="lblHeaderEdit" runat="server" Text="Stage fees Configuration"></asp:Label></h2>
                            </td>
                            <td style="width: 50%;">
                                <span class="dark_btn_small">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/PatientStageList.aspx" meta:resourcekey="btnBackResource1" />
                                </span>
                            </td>
                        </tr>

                      
                    </table>
                </div>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="widecolumn">
                    <div class="personal_box alignleft">
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblPerStageFees" runat="server" Text="Per Stage Fees ($)"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <asp:TextBox ID="txtFees" runat="server" meta:resourcekey="txtFeesResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvtxtFees" runat="server" ValidationGroup="validation" ErrorMessage="Please enter fees." ControlToValidate="txtFees" Display="None" meta:resourcekey="rqvtxtFeesResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqvetxtFees" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvtxtFees" Enabled="True" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteFees" runat="server" Enabled="True"
                                TargetControlID="txtFees" ValidChars="0123456789." />
                        </div>
                        <br />
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="Stage Status"></asp:Label>
                                <span class="asteriskclass">*</span><span class="alignright">:</span>
                            </label>
                            <div class="parsonal_textfild" style="padding: 0 0 0 0;">
                                            <div class="parsonal_selectSmallSearch">
                                                <asp:DropDownList ID="ddlstagestatus" CommandName="CustomSort" OnCommand="Custom_Command" runat="server">
                                                    <%--<asp:ListItem Value="" Text="-- Select Status Type --" meta:resourcekey="ListItemResource1"></asp:ListItem>--%>
                                                    <asp:ListItem Value="0" Text="Submitted" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="In Process" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Completed" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                        </div>
                    </div>                                                  
                </div>
                <div class="bottom_btn tpadd alignright" style="width: 268px;">
                    <span class="blue_btn">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="validation" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
                    </span><span class="dark_btn">
                        <input type="reset" tabindex="7" title="Reset" value="Reset" />
                    </span>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
