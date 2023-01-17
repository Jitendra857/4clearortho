<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopUp.Master" AutoEventWireup="true" CodeBehind="AddAppointmentRequest.aspx.cs" Inherits="_4eOrtho.AddAppointmentRequest" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .asteriskclass
        {
            color: Red;
            vertical-align: middle;
        }

        .emptyTable
        {
            height: 25px;
            color: Red;
            border-top: 1px solid #DDDDDD;
            border-left: 1px solid #DDDDDD;
            margin-top: 5px;
        }
    </style>
    <script type="text/javascript">
        function ResetForm() {
            parent.jQuery.colorbox.close();
        }
    </script>
    <script type="text/javascript">
        function pageLoad() {
            var currentDateTime = new Date();
            $('.From-Date').datepicker({
                showOn: "button",
                buttonText: "<%= this.GetLocalResourceObject("SelectDate") %>",
                buttonImage: "Content/images/bgi/calendar.png",
                buttonImageOnly: true,
                disabled: false,
                changeMonth: true,
                changeYear: true,
                yearRange: "-10:+20",
                minDate: new Date(),
                onSelect: function (value, date) {
                    $('#' + '<%= txtPreferedTime.ClientID %>').timepicker({
                        onHourShow: function (hour) {
                            var now = new Date();
                            if ($('.From-Date').val() == $.datepicker.formatDate('mm/dd/yy', now)) {
                                if (hour <= now.getHours()) {
                                    return false;
                                }
                            }
                            return true;
                        }
                    });
                }
            });
                $('.not-edit').attr("readonly", "readonly");
                $(".ui-datepicker-trigger").css('margin-bottom', '-9px');

                $("#slider").slider({
                    range: "max",
                    min: 10,
                    max: 240,
                    value: 10,
                    step: 10,
                    slide: function (event, ui) {
                        $("#spnAppointmentLength").text(ui.value + ' min');
                        $('#' + '<%= hdAppLength.ClientID %>').val(ui.value);
                    }
                });
                $('#' + '<%= txtAppointmentDate.ClientID %>').focus();
            if (document.getElementById('<%=hdEditLength.ClientID%>').value != null && document.getElementById('<%=hdEditLength.ClientID%>').value != "") {
                $("#slider").slider("value", document.getElementById('<%=hdEditLength.ClientID%>').value);
                $("#spnAppointmentLength").text(document.getElementById('<%=hdEditLength.ClientID%>').value + "min");
                document.getElementById('<%= hdAppLength.ClientID%>').value = document.getElementById('<%=hdEditLength.ClientID%>').value;
            }
        }

        function Reset() {
            var hdEditLength = document.getElementById('<%=hdEditLength.ClientID%>');
            $("#slider").slider("value", hdEditLength.value);
            if (hdEditLength.value == "") {
                $("#spnAppointmentLength").text("10 min");
            }
            else {
                $("#spnAppointmentLength").text(hdEditLength.value + "min");
            }
        }
    </script>


    <div class="alignleft app_popup" id="intailcolorboxContent">

        <asp:UpdatePanel ID="upTreatment" runat="server">
            <ContentTemplate>
                <div class="page_title">
                    <h2 class="padd">
                        <asp:Label ID="lblHeader" Text="Add Appointment Request" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>

                </div>
                <div id="divMsg" runat="server">
                    <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                </div>
                <fieldset class="fieldsetclass app_book_bordercolor">
                    <legend class="legendclass">
                        <asp:Label ID="lblSelectInitialReasons" runat="server"
                            Text="Add Appointment Request" meta:resourcekey="lblSelectInitialReasonsResource1"></asp:Label></legend>

                    <div class="popupparsonal_textfild alignleft marginrightclaim">
                        <%--<label class="wide">
                    <asp:Label ID="lblPreferredDoctor" runat="server" Text="Preferred Doctor "></asp:Label><span
                        class="asteriskclass">*</span><span class="alignright">:</span></label>
                <div class="parsonal_select">
                    <asp:DropDownList ID="ddlDoctor" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDoctor" ForeColor="Red" runat="server" ControlToValidate="ddlDoctor"
                        Display="None" ErrorMessage="Please select preferred Doctor Name" CssClass="errormsg"
                        SetFocusOnError="True" ValidationGroup="validation" InitialValue="0" />
                    <ajaxToolkit:ValidatorCalloutExtender ID="vceDoctor" runat="server" CssClass="customCalloutStyle"
                        TargetControlID="rfvDoctor" Enabled="True" />
                </div>--%>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="popupparsonal_textfild alignleft marginrightclaim sliderlength">
                        <label class="wide">
                            <asp:Label ID="lblAppointmentDuration" runat="server" Text="Appointment Duration " meta:resourcekey="lblAppointmentDurationResource1"></asp:Label><span class="alignright">:</span></label>
                        <div id="slider" style="width: 215px; margin-left: 186px;">
                            <div style="margin-left: 203px; width: 100px;">
                                <asp:HiddenField ID="hdAppLength" Value="10" runat="server" />
                                <asp:HiddenField ID="hdEditLength" runat="server" />
                            </div>
                        </div>

                    </div>
                    <span id="spnAppointmentLength" class="spanslider" style="margin-top: 10px; margin-left: 10px; float: left">10 min</span>
                    <div class="clear">
                    </div>
                    <div class="popupparsonal_textfild alignleft marginrightclaim">
                        <label class="wide">
                            <asp:Label ID="lblAppointmentDate" runat="server" Text="Appointment Date " meta:resourcekey="lblAppointmentDateResource1"></asp:Label><span
                                class="asteriskclass">*</span><span class="alignright">:</span></label>
                        <asp:TextBox ID="txtAppointmentDate" runat="server" CssClass="From-Date not-edit textfild search-datepicker"
                            Width="186px" meta:resourcekey="txtAppointmentDateResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvAppointmentDate" ForeColor="Red" runat="server"
                            ControlToValidate="txtAppointmentDate" SetFocusOnError="True" Display="None"
                            ErrorMessage="Enter Appointment Date" CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvAppointmentDateResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveAppointmentDate" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvAppointmentDate" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="clear">
                    </div>
                    <div class="popupparsonal_textfild alignleft marginrightclaim">
                        <label class="wide">
                            <asp:Label ID="lblPreferredTime" runat="server" Text="Preferred Time " meta:resourcekey="lblPreferredTimeResource1"></asp:Label><span
                                class="asteriskclass">*</span><span class="alignright">:</span></label>
                        <asp:TextBox ID="txtPreferedTime" runat="server" CssClass="not-edit" meta:resourcekey="txtPreferedTimeResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvTime" ForeColor="Red" runat="server" ControlToValidate="txtPreferedTime"
                            SetFocusOnError="True" Display="None" ErrorMessage="Enter Time" CssClass="errormsg"
                            ValidationGroup="validation" meta:resourcekey="rqvTimeResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveTime" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvTime" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </div>
                    <div class="clear">
                    </div>
                    <div id="divIsBooked" runat="server" class="parsonal_textfild" style="display: none;">
                        <label class="wide">
                            <asp:Label ID="lblIsBooked" runat="server" Text="Is Booked " meta:resourcekey="lblIsBookedResource1"></asp:Label><span
                                class="alignright">:</span>
                        </label>
                        <div style="padding-top: 7px;">
                            <asp:CheckBox ID="chkIsBooked" runat="server" meta:resourcekey="chkIsBookedResource1" />
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="popupparsonal_textfild alignleft marginrightclaim" style="display: none;">
                        <label class="wide">
                            <asp:Label ID="lblIsActive" runat="server" Text="Is Active " meta:resourcekey="lblIsActiveResource1"></asp:Label><span
                                class="alignright">:</span>
                        </label>
                        <div style="padding-top: 7px;">
                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" meta:resourcekey="chkIsActiveResource1" />
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="popupparsonal_textfild alignleft marginrightclaim">
                        <label class="wide">
                            <asp:Label ID="lblDescription" runat="server" Text="Description " meta:resourcekey="lblDescriptionResource1"></asp:Label><span
                                class="alignright">:</span></label>
                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" Width="220px"
                            meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                    </div>
                    <div class="clear">
                    </div>
                </fieldset>
                <div class="bottom_btn tpadd alignright" style="width: 310px; margin-top: 5px">
                    <span class="blue_btn_small">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="validation"
                            OnClick="btnSubmit_Click" meta:resourcekey="btnSubmitResource1" />
                    </span>
                    <span class="dark_btn_small">
                        <input id="btnReset" type="reset" value='<%= this.GetLocalResourceObject("Cancel") %>'
                            title='<%= this.GetLocalResourceObject("Cancel") %>'
                            onclick="Reset();" />
                    </span>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
