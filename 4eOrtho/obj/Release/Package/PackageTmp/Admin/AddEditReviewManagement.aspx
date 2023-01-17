<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditReviewManagement.aspx.cs" Inherits="_4eOrtho.Admin.AddEditReviewManagement" Culture="auto" UICulture="auto" meta:resourcekey="PageResource2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#' + '<%= chkActive.ClientID %>').focus();
          });
    </script>
    <div id="container" class="cf">

        <div class="page_title">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        <h2 class="padd">
                            <asp:Label ID="lblHeader" runat="server" Text="Add Review Managment" meta:resourcekey="lblHeaderResource2" ></asp:Label>
                            <asp:Label ID="lblHeaderEdit" runat="server" Text="Edit Review Managment" Visible="False" meta:resourcekey="lblHeaderEditResource2"></asp:Label></h2>
                    </td>
                    <td style="width: 50%;">
                        <span class="dark_btn_small">
                            <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/ListReviewManagement.aspx" meta:resourcekey="btnBackResource2"  />
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource2" ></asp:Label>
        </div>
        <div class="widecolumn">
            <div class="personal_box alignleft">
                <div class="parsonal_textfild">
                    <label style="width: 128px;">
                        <asp:Label ID="lblReviewContent" runat="server" Text="Review Content" meta:resourcekey="lblReviewContentResource1" ></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <label>
                        <asp:Label ID="lblReviewContentData" runat="server" meta:resourcekey="lblReviewContentDataResource2" ></asp:Label></label>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild">
                    <label style="width: 128px;">
                        <asp:Label ID="lblPatientName" runat="server" Text="Patient Name" meta:resourcekey="lblPatientNameResource1" ></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <label>
                        <asp:Label ID="lblPatientNameData" runat="server" meta:resourcekey="lblPatientNameDataResource2"></asp:Label></label>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild">
                    <label style="width: 128px;">
                        <asp:Label ID="lblPatientEmail" runat="server" Text="Patient Email" meta:resourcekey="lblPatientEmailResource1" ></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <label>
                        <asp:Label ID="lblPatientEmailData" runat="server" meta:resourcekey="lblPatientEmailDataResource2" ></asp:Label></label>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild">
                    <label style="width: 128px;">
                        <asp:Label ID="lblDoctorEmail" runat="server" Text="Doctor Email" meta:resourcekey="lblDoctorEmailResource1" ></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <label>
                        <asp:Label ID="lblDoctorEmailData" runat="server" meta:resourcekey="lblDoctorEmailDataResource2" ></asp:Label>
                    </label>
                </div>

                <div class="clear"></div>
                <div class="parsonal_textfild">
                    <label style="width: 128px;">
                        <asp:Label ID="lblActive" runat="server" Text="IsActive" meta:resourcekey="lblActiveResource2" ></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <asp:CheckBox ID="chkActive" runat="server" Checked="True" meta:resourcekey="chkActiveResource2"  />
                </div>
            </div>
        </div>
        <div class="bottom_btn tpadd alignright" style="width: 268px;">
            <span class="blue_btn">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="validation" OnClick="btnSubmit_Click" meta:resourcekey="btnSaveResource2"  />
            </span><span class="dark_btn">
                <input type="reset" tabindex="7" title='<%= this.GetLocalResourceObject("Reset") %>'  value='<%= this.GetLocalResourceObject("Reset") %>' />
            </span>
        </div>
    </div>
</asp:Content>
