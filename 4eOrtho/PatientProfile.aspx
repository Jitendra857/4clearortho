<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="PatientProfile.aspx.cs" Inherits="_4eOrtho.PatientProfile" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $('.From-Date').datepicker({
                showOn: "button",
                buttonText: "<%=this.GetLocalResourceObject("SelectDate").ToString()%>",
                buttonImage: "Content/images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                yearRange: "-100:+20",
                maxDate: new Date()
            });
            $('.not-edit').attr("readonly", "readonly");
        }
        $(document).ready(function () {
            $('#txtEmail').focus()
        });
    </script>
    <asp:UpdatePanel ID="upNewCase" runat="server">
        <ContentTemplate>
            <div class="main_right_cont minheigh">
                <div class="title">
                    <h2>
                        <asp:Label ID="lblHeader" runat="server" Text="Edit Profile" meta:resourcekey="lblHeaderResource1"></asp:Label>
                    </h2>
                </div>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <div class="personal_box alignleft" style="width: 100%">
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtEmail" runat="server" TabIndex="1" Enabled="False" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name" meta:resourcekey="lblFirstNameResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtFirstName" runat="server" TabIndex="1" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirstName" ForeColor="Red" runat="server" ControlToValidate="txtFirstName"
                            Display="None" ErrorMessage="Please enter first name" CssClass="errormsg"
                            SetFocusOnError="True" ValidationGroup="validation" meta:resourcekey="rfvFirstNameResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="vceFirstName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rfvFirstName" Enabled="True" />
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name" meta:resourcekey="lblLastNameResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtLastName" runat="server" TabIndex="2" meta:resourcekey="txtLastNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtLastName" ForeColor="Red" runat="server" ControlToValidate="txtLastName"
                            Display="None" ErrorMessage="Please enter last name" CssClass="errormsg"
                            SetFocusOnError="True" ValidationGroup="validation" meta:resourcekey="rfvtxtLastNameResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="vceLastName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rfvtxtLastName" Enabled="True" />
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblGender" runat="server" Text="Gender" meta:resourcekey="lblGenderResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <div class="radio-selection">
                            <asp:RadioButton ID="rbtnMale" Text="Male" runat="server" GroupName="Gender" Checked="True" TabIndex="4" meta:resourcekey="rbtnMaleResource1" />
                            <asp:RadioButton ID="rbtnFemale" Text="Female" runat="server" GroupName="Gender" TabIndex="5" meta:resourcekey="rbtnFemaleResource1" />
                        </div>
                    </div>
                    <div class="parsonal_textfild alignleft">
                        <label>
                            <asp:Label ID="lblDateofBirth" runat="server" Text="Date Of Birth" meta:resourcekey="lblDateofBirthResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                        <asp:TextBox ID="txtDateofBirth" runat="server" CssClass="From-Date not-edit textfild search-datepicker" TabIndex="3" Style="margin-right: 10px" meta:resourcekey="txtDateofBirthResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtDateofBirth" ForeColor="Red" runat="server" ControlToValidate="txtDateofBirth"
                            Display="None" ErrorMessage="Please select date of birth." CssClass="errormsg"
                            SetFocusOnError="True" ValidationGroup="validation" meta:resourcekey="rfvtxtDateofBirthResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="vceDateofBirth" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rfvtxtDateofBirth" Enabled="True" />
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
            </div>
            <asp:HiddenField ID="hdnPassword" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
