<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopUp.Master" AutoEventWireup="true"
    CodeBehind="PhotoUpload.aspx.cs" Inherits="_4eOrtho.PhotoUpload" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/AjaxToolkit.css" rel="Stylesheet" type="text/css" />
    <%--<link href="Styles/Jquery-UI/jquery-ui-1.8.23.custom.css" rel="Stylesheet" type="text/css" />
    <link href="Styles/jquery.ui.timepicker.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.8.0.min.js" language="javascript" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.23.custom.min.js" language="javascript" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.timepicker.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/modernizr.custom.82896.js"></script>
    <link rel="Stylesheet" type="text/css" href="Scripts/Colorbox/colorbox.css" />
    <script type="text/javascript" src="Scripts/Colorbox/jquery.colorbox.js"></script>
    <script type="text/javascript" src="Scripts/jquery.autotab-1.1b.js"></script>
    <link href="Styles/Jquery-UI/jquery-ui-timepicker-addon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-sliderAccess.js"></script>--%>
    <link rel="Stylesheet" href="Styles/style.css" type="text/css" />
    <script type="text/javascript">
        function closePhotoUpload(_id) {
            parent.jQuery.colorbox.close();
            parent.window.location.href = "PatientProfile.aspx?pid=" + _id + "&IsImageUpload=true";
            //parent.RefreshPhoto();
        }

        function closeDoctorPhotoUpload(NewPhotoName) {            
            if (window.parent.jQuery('[id$=rbtnMale]')[0].checked) {
                window.parent.jQuery('#ContentPlaceHolder1_hdMalePhotoName')[0].value = NewPhotoName;
                window.parent.jQuery('#ContentPlaceHolder1_imgPatientPhoto')[0].src = "../Photograph/" + NewPhotoName;
            }
            else if (window.parent.jQuery('[id$=rbtnFemale]')[0].checked) {
                window.parent.jQuery('#ContentPlaceHolder1_hdFemalePhotoName')[0].value = NewPhotoName;
                window.parent.jQuery('#ContentPlaceHolder1_imgPatientPhoto')[0].src = "../Photograph/" + NewPhotoName;
            }
            //window.parent.jQuery('#ContentPlaceHolder1_hdPhotoName')[0].value = NewPhotoName;
            //window.parent.jQuery('#ContentPlaceHolder1_imgPatientPhoto')[0].src = "Photograph/" + NewPhotoName;            
            parent.jQuery.colorbox.close();            
        }

        function ResetForm() {
            parent.jQuery.colorbox.close();
        }

        function CheckFile() {
            if (document.getElementById('ContentPlaceHolder1_fuploadPhoto').value == "") {
                alert("<%= this.GetLocalResourceObject("SelectPhoto")%>");
                return false;
            }
        }

        window.onload = function () {
            document.getElementById('ContentPlaceHolder1_fuploadPhoto').focus();
        }
    </script>
    <div id="container" class="cf" style="padding-bottom: 0px; padding-right: 29px">
        <div class="personal_box alignleft" style="width: 466px; height: 165px; padding-bottom: 0px;" id="divPhotoUpload">
            <h3 style="padding-bottom: 0px;">
                <asp:Label ID="lblPhotoUpload" runat="server" Text="Photo Upload"
                    meta:resourcekey="lblPhotoUploadResource1"></asp:Label></h3>
            <div style="height: 10px;" class="parsonal_textfild alignleft">
                <asp:Label ID="lblMessage" ForeColor="Red" runat="server"
                    meta:resourcekey="lblMessageResource1" />
            </div>
            <div class="clear">
            </div>
            <div class="parsonal_textfild alignleft">
                <label style="width: 120px">
                    <asp:Label ID="lblSelectPhoto" runat="server" Text="Select Photo "
                        meta:resourcekey="lblSelectPhotoResource1"></asp:Label><span class="asteriskclass">*</span><span class="alignright">:</span></label>
                <asp:FileUpload ID="fuploadPhoto" runat="server"
                    meta:resourcekey="fuploadPhotoResource1" />
                <%--<asp:RequiredFieldValidator ID="rqvfuploadPhoto" ForeColor="Red" runat="server" SetFocusOnError="true"
                    ControlToValidate="fuploadPhoto" Display="None" ErrorMessage="Please Select Photo."
                    CssClass="errormsg" ValidationGroup="validation" />
                <ajaxToolkit:ValidatorCalloutExtender ID="rqvefuploadPhoto" runat="server" CssClass="customCalloutStyle"
                    TargetControlID="rqvfuploadPhoto" Enabled="True" Width="140px">
                </ajaxToolkit:ValidatorCalloutExtender>--%>
            </div>
            <div class="clear">
            </div>
            <div class="bottom_btn  alignright" style="padding-top: 10px; ">
                <span class="blue_btn_small">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return CheckFile();"
                        OnClick="btnSubmit_Click" meta:resourcekey="btnSubmitResource1" /></span> <span class="dark_btn_small" style="margin: 0">
                            <input type="button" value="<%= this.GetLocalResourceObject("Cancel")%>"  title="<%= this.GetLocalResourceObject("Cancel")%>" onclick="ResetForm();" />
                            <asp:HiddenField ID="hdPatientId" runat="server" />
                            <asp:HiddenField ID="hdDoctorId" runat="server" />
                        </span>
            </div>
            <div class="clear">
            </div>
            <div style="height: 10px; padding-top: 5px;">
                <asp:Label Text="(It allows only .png, .bmp, .jpg, .gif, .jpeg format file with size of maximum 2MB.)"
                    runat="server" meta:resourcekey="LabelResource1" />
            </div>
        </div>
    </div>
</asp:Content>
