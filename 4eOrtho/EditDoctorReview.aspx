<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="EditDoctorReview.aspx.cs" Inherits="_4eOrtho.EditDoctorReview" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .parsonal_textfild label:last-child {
            width: 380px !important;            
            float: left;
            margin-right: 8px;
            font-size: 14px;
            color: #333333;
            line-height: 24px;
            direction: ltr;
        }
    </style>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="title main_right_cont" style="width: 100%;">
                <div class="supply-button2 back">
                    <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/ListReview.aspx" Width="100px" TabIndex="6" meta:resourcekey="btnBackResource1" />
                </div>
                <h2>
                    <asp:Label ID="lblHeader" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label>
                </h2>
            </div>
            <div class="main_right_cont minheigh">
                <div class="personal_box alignleft">
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblPatient" runat="server" Text="PatientName" meta:resourcekey="lblPatientResource1"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblPatientName" runat="server" meta:resourcekey="lblPatientNameResource1"></asp:Label>
                        </label>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblReview" runat="server" Text="Review" meta:resourcekey="lblReviewResource1"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblReviewContent" runat="server" meta:resourcekey="lblReviewContentResource1"></asp:Label>
                        </label>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblEmail" runat="server" Text="Patient Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="lblPatientEmail" runat="server" meta:resourcekey="lblPatientEmailResource1"></asp:Label>
                        </label>
                    </div>
                    <div class="clear"></div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label runat="server" ID="lblIsActive" Text="Published?" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                        </label>
                        <label>
                            <div class="radio-selection">
                                <asp:CheckBox ID="chkIsActive" runat="server" meta:resourcekey="chkIsActiveResource1" />
                            </div>
                        </label>
                    </div>
                    <div class="date2">
                        <div class="date_cont">
                            <div class="date_cont_right">
                                <div class="supply-button3">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="validation" OnClick="btnSubmit_Click" meta:resourcekey="btnSubmitResource1" />
                                </div>
                                <div class="supply-button3">
                                    <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="5" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
