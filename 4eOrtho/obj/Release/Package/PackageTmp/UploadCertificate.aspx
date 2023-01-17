<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="UploadCertificate.aspx.cs" Inherits="_4eOrtho.UploadCertificate" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validateImage(objFile) {
            if (typeof (FileReader) != "undefined") {
                var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp|.pdf)$/;
                var file = $(objFile)[0];
                if (file.files.length > 0) {
                    var size = parseFloat(file.files[0].size / 1024).toFixed(2);
                    if (regex.test(file.files[0].name.toLowerCase())) {
                        if (size > 2024) {
                            alert("<%= this.GetLocalResourceObject("filesize") %>");
                            objFile.value = "";
                            return false;
                        }

                        var reader = new FileReader();
                        reader.onload = function (e) {
                        }
                        reader.readAsDataURL(file.files[0]);
                    } else {
                        alert(file.files[0].name + "<%= this.GetLocalResourceObject("validimage") %>");
                        objFile.value = "";
                        return false;
                    }
                }
            } else {
                alert("This browser does not support HTML5 FileReader.");
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main_right_cont minheigh">
        <div class="title">
            <h2>
                <asp:Label ID="lblHeader" runat="server" Text="Upload Certificate" meta:resourcekey="lblHeaderResource1"></asp:Label>
            </h2>
        </div>
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </div>
        <div class="personal_box alignleft">
            <div class="parsonal_textfild alignleft">
                <label>
                    <asp:Label ID="lblSelectFile" runat="server" Text="Select File" meta:resourcekey="lblSelectFileResource1"></asp:Label>
                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                </label>
                <asp:FileUpload ID="fuUploadCertificate" onchange="validateImage(this)" runat="server" Style="border: 1px solid gray; border-radius: 3px;" meta:resourcekey="fuUploadCertificateResource1" />
                <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" ControlToValidate="fuUploadCertificate" ValidationGroup="validation" Display="None"
                    SetFocusOnError="true" ErrorMessage="Please select file."></asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="rgveCallfuUpload" runat="server"
                    CssClass="customCalloutStyle" TargetControlID="rfvFileUpload" Enabled="True">
                </ajaxToolkit:ValidatorCalloutExtender>
            </div>
            <div class="parsonal_textfild alignleft">
                <label>
                    <asp:Label ID="lblComment" runat="server" Text="Comment"></asp:Label>
                    <span class="alignright">:<span class="asteriskclass">&nbsp;</span></span>
                </label>
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="date2">
                <div class="date_cont">
                    <div class="date_cont_right">
                        <div class="supply-button3">
                            <asp:Button ID="btnSubmit" runat="server" Text="Upload" ValidationGroup="validation"
                                OnClick="btnSubmit_Click" TabIndex="4" meta:resourcekey="btnSubmitResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="date2">
            <iframe id="myIframe" width="100%" src="<%= hdnFileName.Value %>" height="500px" style="<%= !string.IsNullOrEmpty(hdnFileName.Value) ? "display:block;": "display:none;" %>"></iframe>
            <asp:HiddenField ID="hdnFileName" runat="server" />
            <%--<asp:Image ID="imgCertificate" runat="server" Style="width: 100%;" meta:resourcekey="imgCertificateResource1" />--%>
        </div>
    </div>
</asp:Content>
