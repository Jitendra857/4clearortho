<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopUp.Master" AutoEventWireup="true" CodeBehind="PatientBrochureEstimate.aspx.cs" Inherits="_4eOrtho.PatientBrochureEstimate" culture="auto"  uiculture="auto" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .asteriskclass
        {
            color: Red;
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
       

        function ResetForm() {
            
            //window.location.reload();
            //parent.jQuery.colorbox.close();
            parent.jQuery.colorbox.close();
           // parent.window.location.href = "PatientBrochures.aspx";
            window.open('/PatientBrochures.aspx');
           //  window.open('', '_new').location.replace(url)               
            //  parent.jQuery.colorbox.close();
        //    window.open(url);
        }
      
    </script>
    <div class="alignleft app_popup" id="intailcolorboxContent">
  <%--      <asp:UpdatePanel ID="upTreatment" runat="server">
            <ContentTemplate>--%>
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label ID="lblHeader" Text="Estimate Patient Treatment" runat="server" meta:resourcekey="lblHeaderResource1" ></asp:Label></h2>
                </div>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1" ></asp:Label>
                </div>
                <fieldset class="fieldsetclass app_book_bordercolor">
                    <legend class="legendclass">
                        <asp:Label ID="lblSelectInitialReasons" runat="server"
                            Text="Estimate Patient Treatment" meta:resourcekey="lblSelectInitialReasonsResource1" ></asp:Label></legend>
                    <div class="popupparsonal_textfild alignleft marginrightclaim">
                        <label class="wide">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name " meta:resourcekey="lblPatientNameResource1"></asp:Label><span
                                class="asteriskclass">*</span><span class="alignright">:</span></label>
                        <asp:TextBox ID="txtPatientName" runat="server"
                            Width="186px" meta:resourcekey="txtPatientNameResource1" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvPatientName" ForeColor="Red" runat="server"
                            ControlToValidate="txtPatientName" SetFocusOnError="True" Display="None"
                            ErrorMessage="Please enter patient name" CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvPatientNameResource1"  />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqvePatientName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvPatientName" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="popupparsonal_textfild alignleft marginrightclaim">
                        <label class="wide">
                            <asp:Label ID="lblPatientEmail" runat="server" Text="Email" meta:resourcekey="lblPatientEmailResource1"></asp:Label><span
                                class="asteriskclass">*</span><span class="alignright">:</span></label>
                        <asp:TextBox ID="txtEmailAddress" runat="server"
                            Width="186px" meta:resourcekey="txtEmailAddressResource1" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvPatientEmail" ForeColor="Red" runat="server"
                            ControlToValidate="txtEmailAddress" SetFocusOnError="True" Display="None"
                            ErrorMessage="Please enter patient email" CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvPatientEmailResource1"  />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqvePatientEmail" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvPatientEmail" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <asp:RegularExpressionValidator ID="rgevprEmail" runat="server" ValidationGroup="validation" ControlToValidate="txtEmailAddress" ForeColor="Red" ErrorMessage="Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None" meta:resourcekey="rgevprEmailResource1" ></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rgevepremail" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rgevprEmail" Enabled="True" />
                    </div>
                    <div class="clear">
                    </div>
                    <div class="popupparsonal_textfild alignleft marginrightclaim">
                        <label class="wide">
                            <asp:Label ID="lblAmount" runat="server" Text="Amount" meta:resourcekey="lblAmountResource1"></asp:Label><span
                                class="asteriskclass">*</span><span class="alignright">:</span></label>
                        <asp:TextBox ID="txtAmount"  runat="server" Width="186px" meta:resourcekey="txtAmountResource1"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="rqvAmount" ForeColor="Red" runat="server" SetFocusOnError="True"
                            ControlToValidate="txtAmount" Display="None" ErrorMessage="Please enter amount."
                            CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvAmountResource1"  />
                         <ajaxToolkit:ValidatorCalloutExtender ID="rqveAmount" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvAmount" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteMobile" runat="server" Enabled="True"
                            TargetControlID="txtAmount" ValidChars="0123456789." />
                      <%--  <asp:RegularExpressionValidator ID="rgvAmount" runat="server" ControlToValidate="txtAmount" Display="None"
                            SetFocusOnError="True" ValidationExpression="\d+(\.\d{1,2})?" ValidationGroup="validation"
                            CssClass="errormsg" ErrorMessage="Only Numeric Values with two precesion values is allowed"
                            ></asp:RegularExpressionValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteMobile" runat="server" Enabled="True"
                            TargetControlID="txtAmount" ValidChars="0123456789." />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveuploadPhoto" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvAmount" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                    </div>
                    <div class="clear">
                    </div>

                </fieldset>
                <div class="bottom_btn tpadd alignright" style="width: 310px; margin-top: 5px">
                    <span class="blue_btn_small">
                        <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click"  runat="server" Text="Submit" ValidationGroup="validation" meta:resourcekey="btnSubmitResource1"/>                       
                       
                     </span>
                    <span class="dark_btn_small">
                        <input id="btnReset" type="reset" value='<%= this.GetLocalResourceObject("Reset") %>'
                            title='<%= this.GetLocalResourceObject("Reset") %>' />
                        <%--<input id="btnReset" type="reset" value='<%= this.GetLocalResourceObject("Cancel") %>'
                            title='<%= this.GetLocalResourceObject("Cancel") %>'
                            onclick="Reset();" />--%>
                    </span>
                </div>

            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
