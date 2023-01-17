<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="UpdateTrackDetail.aspx.cs" Inherits="_4eOrtho.UpdateTrackDetail" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main_right_cont minheigh">
        <div class="title">
            <div class="supply-button2 back">
                <asp:Button ID="btnBack" runat="server" Text="Back" ToolTip="Back" PostBackUrl="~/ListTrackCase.aspx" Width="100px" TabIndex="16" meta:resourcekey="btnBackResource1" />
            </div>

            <h2>
                <asp:Label ID="lblHeader" runat="server" Text="Track Case" meta:resourcekey="lblHeaderResource1"></asp:Label>
            </h2>
        </div>
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </div>
        <%--<asp:UpdatePanel ID="upNewCase" runat="server">
            <ContentTemplate>--%>
        <div class="date2">
            <div class="date_cont">
                <div class="date_cont_right marginlf">
                    <asp:Label ID="lblTrackId" runat="server" Text="Track No" meta:resourcekey="lblTrackIdResource1"></asp:Label>
                </div>
            </div>
            <div class="date_cont">
                <div class="date_cont_right">
                    <asp:Label ID="lblTrackNo" runat="server" meta:resourcekey="lblTrackNoResource1"></asp:Label>
                </div>
            </div>
        </div>
        <div class="date2">
            <div class="date_cont">
                <div class="date_cont_right marginlf">
                    <asp:Label ID="lblCaseNo1" runat="server" Text="Case No" meta:resourcekey="lblCaseNo1Resource1"></asp:Label>
                </div>
            </div>
            <div class="date_cont">
                <div class="date_cont_right">
                    <asp:Label ID="lblCaseNo" runat="server" meta:resourcekey="lblCaseNoResource1"></asp:Label>
                </div>
            </div>
        </div>
        <div class="date2">
            <div class="date_cont">
                <div class="date_cont_right marginlf">
                    <asp:Label ID="lblCurrentStatus1" runat="server" Text="Current Status" meta:resourcekey="lblCurrentStatus1Resource1"></asp:Label>
                </div>
            </div>
            <div class="date_cont">
                <div class="date_cont_right ">
                    <asp:Label ID="lblCurrentStatus" runat="server" meta:resourcekey="lblCurrentStatusResource1"></asp:Label>
                </div>
            </div>
        </div>
        <div id="dvSelectStatus" runat="server" class="date2">
            <div class="date_cont">
                <div class="date_cont_right marginlf">
                    <asp:Label ID="lblChangeStatus" runat="server" Text="Update Status" meta:resourcekey="lblChangeStatusResource1"></asp:Label>
                    <span class="starcl">*</span>
                </div>
            </div>
            <div class="date_cont">
                <div class="date_cont_right product_dropdown">
                    <asp:DropDownList ID="ddlUpdateStatus" runat="server" class="low_high2" Style="color: black; font-size: 14px; margin-top: 0px;" meta:resourcekey="ddlUpdateStatusResource1">
                        <asp:ListItem Text="Select Status" Value="0" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Received" Value="Received" meta:resourcekey="ListItemResource2"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlUpdateStatus" ForeColor="Red" runat="server" ControlToValidate="ddlUpdateStatus"
                        Display="None" ErrorMessage="Please Select Status" CssClass="errormsg"
                        SetFocusOnError="True" ValidationGroup="validation" InitialValue="0" meta:resourcekey="rfvddlUpdateStatusResource1" />
                    <ajaxToolkit:ValidatorCalloutExtender ID="vceddlUpdateStatus" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rfvddlUpdateStatus" Enabled="True" />
                </div>
            </div>
        </div>
        <div class="date2">
            <div class="date_cont">
                <div class="date_cont_right marginlf">
                    <asp:Label ID="lblDescription" runat="server" Text="Description" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                </div>
            </div>
            <div class="date_cont" style="width: 312px; height: 110px">
                <div class="date_cont_right">
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Style="width: 400px; height: 100px" TabIndex="12" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="date2">
            <div class="date_cont">
                <div class="date_cont_right marginlf">
                    <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                    <span class="starcl">*</span>
                </div>
            </div>
            <div class="date_cont">
                <div class="date_cont_right">
                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="13" meta:resourcekey="chkIsActiveResource1" />
                </div>
            </div>
        </div>
        <div class="date2">
            <div class="date_cont">
                <div class="date_cont_right">
                    <div class="supply-button3">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="validation"
                            OnClick="btnSubmit_Click" TabIndex="14" meta:resourcekey="btnSubmitResource1" />
                    </div>
                    <div class="supply-button3">
                        <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="15" ToolTip="Cancel" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
