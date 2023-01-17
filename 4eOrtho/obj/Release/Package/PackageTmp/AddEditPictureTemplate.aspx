<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="AddEditPictureTemplate.aspx.cs" Inherits="_4eOrtho.AddEditPictureTemplate" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-steps/jquery.steps.js"></script>
    <link rel="Stylesheet" type="text/css" href="Scripts/jquery-steps/jquery.steps.css" />
    <link href="Styles/common.css" rel="stylesheet" />
    <script src="Scripts/jquery.simpleFilePreview.js"></script>
    <link rel="stylesheet" href="Styles/simpleFilePreview.css" type="text/css" />

    <style type="text/css">
        .wizard > .steps a, .wizard > .steps a:hover, .wizard > .steps a:active {
            background: none repeat scroll 0 0 #eee;
            color: #aaa;
            cursor: default;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            CreateWizard();
            //$('.FileUpload').simpleFilePreview();
            $('#' + '<%= fuFile1.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("FullFace") %>" });
            $('#' + '<%= fuFile2.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Smile") %>" });
            $('#' + '<%= fuFile3.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Profile") %>" });
            $('#' + '<%= fuFile4.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("RightLateral") %>" });
            $('#' + '<%= fuFile5.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("LeftLateral") %>" });
            $('#' + '<%= fuFile6.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Maxillary") %>" });
            $('#' + '<%= fuFile7.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Anterior") %>" });
            $('#' + '<%= fuFile8.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Mandibulary") %>" });

            $('#' + '<%= fuFile9.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("FullFace") %>" });
            $('#' + '<%= fuFile10.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Smile") %>" });
            $('#' + '<%= fuFile11.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Profile") %>" });
            $('#' + '<%= fuFile12.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("RightLateral") %>" });
            $('#' + '<%= fuFile13.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("LeftLateral") %>" });
            $('#' + '<%= fuFile14.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Maxillary") %>" });
            $('#' + '<%= fuFile15.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Anterior") %>" });
            $('#' + '<%= fuFile16.ClientID %>').simpleFilePreview({ buttonContent: "<%= this.GetLocalResourceObject("buttonContent") + "<br/>" + this.GetLocalResourceObject("Mandibulary") %>" });


            $('.not-edit').attr("readonly", "readonly");

            $(".FileUpload").change(function () {
                if (typeof (FileReader) != "undefined") {
                    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                    $($(this)[0].files).each(function () {
                        var file = $(this);
                        if (file.length > 0) {
                            var size = parseFloat(file[0].size / 1024).toFixed(2);
                            if (regex.test(file[0].name.toLowerCase())) {
                                if (size > 2024) {
                                    alert("FileSize");
                                    return false;
                                }
                            } else {
                                alert(file[0].name + "<%=this.GetLocalResourceObject("Isnotavalidimage").ToString()%>");
                                return false;
                            }
                        }
                    });
                } else {
                    alert("This browser does not support HTML5 FileReader.");
                }
            });
            BindBeforeImages();
            BindAfterImages();
        }
        function CreateWizard() {
            var form = $("#widget").show();
            form.steps({
                labels: {
                    finish: "<%=this.GetLocalResourceObject("Finish").ToString()%>",
                    next: "<%=this.GetLocalResourceObject("Next").ToString()%>",
                    previous: "<%=this.GetLocalResourceObject("Previous").ToString()%>"
                },
                startIndex: eval('<%= tabIndex %>'),
                enablePagination: '<%= caseId > 0 ? false : true %>',
                enableAllSteps: '<%= caseId > 0 ? true : false%>',
                headerTag: "h3",
                bodyTag: "fieldset",
                transitionEffect: "none",
                titleTemplate: "#title#",
                onStepChanged: function (event, currentIndex, newIndex) {
                    if (currentIndex == 2) {
                    }
                },
                onStepChanging: function (event, currentIndex, newIndex) {
                    $('.actions > ul > li:last-child').attr('style', 'display:none');

                    if (currentIndex > newIndex) {
                        Page_ClientValidate("validation1");
                        if (Page_IsValid) {
                            return true;
                        }
                        else {
                            alert("<%=this.GetLocalResourceObject("Pleaseuploadallimages").ToString()%>");
                            return false;
                        }
                        return true;
                    }
                    if (currentIndex === 0) {
                        Page_ClientValidate("validation2");
                        if (Page_IsValid) {
                            return true;
                        }
                        else {
                            alert("<%=this.GetLocalResourceObject("Pleaseuploadallimages").ToString()%>");
                            return false;
                        }
                    }
                },
                onFinishing: function (event, currentIndex) {
                    return true;
                },
                onFinished: function (event, currentIndex) {
                    Page_ClientValidate("validation3");
                    if (Page_IsValid) {
                        $("#<%=btnSubmit.ClientID%>")[0].click();
                    }
                    else {
                        alert("<%=this.GetLocalResourceObject("Pleaseuploadallimages").ToString()%>");
                        return false;
                    }
                },
                onCanceled: function (event) {
                }
            });
            if ($("#<%=select_tab.ClientID%>")[0].value != "") {                
                currentIndex = $("#<%=select_tab.ClientID%>")[0].value;                
                form.steps("setStep", currentIndex);
            }
            else
                currentIndex = 0;
            //$('.actions > ul > li:last-child').attr('style', 'display:none');
        }

        function validateImage(objFile) {
            if (typeof (FileReader) != "undefined") {
                var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                var file = $(objFile)[0];
                if (file.files.length > 0) {
                    var size = parseFloat(file.files[0].size / 1024).toFixed(2);
                    if (regex.test(file.files[0].name.toLowerCase())) {
                        if (size > 2024) {
                            alert("<%=this.GetLocalResourceObject("FileSize").ToString()%>");
                            objFile.value = "";
                            return false;
                        }

                        var reader = new FileReader();
                        reader.onload = function (e) {
                        }
                        reader.readAsDataURL(file.files[0]);
                    } else {
                        alert(file.files[0].name + "<%=this.GetLocalResourceObject("Isnotavalidimage").ToString()%>");
                        return false;
                    }
                }
            } else {
                alert("This browser does not support HTML5 FileReader.");
            }
            return true;
        }

        function resizeJquerySteps() {
            $('.wizard .content').animate({ height: $('.body.current').outerHeight() }, "slow");
        }
    </script>
    <style type="text/css">
        .UploadImgBox {
            width: 175px;
            height: 175px;
            float: left;
            overflow: hidden;
            margin: 5px;
        }

            .UploadImgBox img {
                width: 100%;
                height: 100%;
                float: left;
                overflow: hidden;
            }

        .imgbox {
            height: 173px;
            list-style-type: none;
            margin: 5px;
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main_right_cont minheigh">
        <div class="title">
            <div class="supply-button2 back">
                <asp:Button ID="btnBack" runat="server" Text="Back" ToolTip="Back" CausesValidation="False" OnClientClick="javascript:window.location='ListPictureTemplate.aspx';" PostBackUrl="~/ListPictureTemplate.aspx" Width="100px" TabIndex="16" meta:resourcekey="btnBackResource1" />
            </div>
            <h2>
                <asp:Label ID="lblHeader" runat="server" Text="Add/Edit Picture Template" meta:resourcekey="lblHeaderResource1"></asp:Label>
            </h2>
        </div>
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </div>
        <div class="widecolumn" id="widget">
            <h3>
                <asp:Label ID="lblBeforeTemplate" runat="server" Text="Before Template" meta:resourcekey="lblBeforeTemplateResource1"></asp:Label>
            </h3>
            <fieldset>
                <asp:Panel ID="pnlImage" runat="server" meta:resourcekey="pnlImageResource1">
                    <div class="wdt alignleft">
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile1" CssClass="FileUpload" onchange="validateImage(this)" runat="server" Style="height: 100% !important; width: 100% !important;" meta:resourcekey="fuFile1Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile1" runat="server" ControlToValidate="fuFile1" Display="Dynamic" ValidationGroup="validation2" meta:resourcekey="rfvFile1Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile2" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile2Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile2" runat="server" ControlToValidate="fuFile2" Display="Dynamic" ValidationGroup="validation2" meta:resourcekey="rfvFile2Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile3" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile3Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile3" runat="server" ControlToValidate="fuFile3" Display="Dynamic" ValidationGroup="validation2" meta:resourcekey="rfvFile3Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile4" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile4Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile4" runat="server" ControlToValidate="fuFile4" Display="Dynamic" ValidationGroup="validation2" meta:resourcekey="rfvFile4Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <div class="imgbox" style="text-align: center; background-color: #164D8E;">
                                <div style="padding-top: 10px;">
                                    <asp:Label ID="lblBeforeAfter" runat="server" Style="font-size: 15px; color: white;" meta:resourcekey="lblBeforeAfterResource1">Before Treatment</asp:Label>
                                    <div class="clear"></div>
                                    <img src="Content/images/logo.png" style="width: auto !important; height: 60px; margin: 7px 15px 5px 35px;" />
                                    <div class="clear"></div>
                                    <asp:Label ID="lblDoctorName" runat="server" Style="font-size: 15px; color: white;" meta:resourcekey="lblDoctorNameResource1"></asp:Label>
                                    <div class="clear"></div>
                                    <asp:Label ID="lblPatientName" runat="server" Style="font-size: 13px; color: white;" meta:resourcekey="lblPatientNameResource1"></asp:Label>
                                    <div class="clear"></div>
                                    <span style="font-size: 13px; color: white;">
                                        <%= this.GetLocalResourceObject("createddate") %>
                                        <asp:Label ID="lbldate" runat="server"></asp:Label></span>
                                </div>
                            </div>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile5" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile5Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile5" runat="server" ControlToValidate="fuFile5" Display="Dynamic" ValidationGroup="validation2" meta:resourcekey="rfvFile5Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile6" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile6Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile6" runat="server" ControlToValidate="fuFile6" Display="Dynamic" ValidationGroup="validation2" meta:resourcekey="rfvFile6Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile7" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile7Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile7" runat="server" ControlToValidate="fuFile7" Display="Dynamic" ValidationGroup="validation2" meta:resourcekey="rfvFile7Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile8" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile8Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile8" runat="server" ControlToValidate="fuFile8" Display="Dynamic" ValidationGroup="validation2" meta:resourcekey="rfvFile8Resource1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <%--<div class="date2">
                        <div class="date_cont">
                            <div class="date_cont_right marginlf">
                                <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                                <span class="starcl">*</span>
                            </div>
                        </div>
                        <div class="date_cont">
                            <div class="date_cont_right">
                                <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="3" meta:resourcekey="chkIsActiveResource1" />
                            </div>
                        </div>
                    </div>--%>
                </asp:Panel>
            </fieldset>
            <h3>
                <asp:Label ID="lblAfterTemplate" runat="server" Text="After Template" meta:resourcekey="lblAfterTemplateResource1"></asp:Label>
            </h3>
            <fieldset>
                <asp:Panel ID="pnlImage1" runat="server" meta:resourcekey="pnlImage1Resource1">
                    <div class="wdt alignleft">
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile9" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile9Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile9" runat="server" ControlToValidate="fuFile9" Display="Dynamic" ValidationGroup="validation3" meta:resourcekey="rfvFile9Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile10" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile10Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile10" runat="server" ControlToValidate="fuFile10" Display="Dynamic" ValidationGroup="validation3" meta:resourcekey="rfvFile10Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile11" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile11Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile11" runat="server" ControlToValidate="fuFile11" Display="Dynamic" ValidationGroup="validation3" meta:resourcekey="rfvFile11Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile12" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile12Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile12" runat="server" ControlToValidate="fuFile12" Display="Dynamic" ValidationGroup="validation3" meta:resourcekey="rfvFile12Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <div class="imgbox" style="text-align: center; background-color: #164D8E;">
                                <div style="padding-top: 10px;">
                                    <asp:Label ID="Label5" runat="server" Style="font-size: 15px; color: white;" meta:resourcekey="Label5Resource1">After Treatment</asp:Label>
                                    <div class="clear"></div>
                                    <img src="Content/images/logo.png" style="width: auto !important; height: 60px; margin: 7px 15px 5px 35px;" />
                                    <div class="clear"></div>
                                    <asp:Label ID="lblDoctorName1" runat="server" Style="font-size: 15px; color: white;" meta:resourcekey="lblDoctorName1Resource1"></asp:Label>
                                    <div class="clear"></div>
                                    <asp:Label ID="lblPatientName1" runat="server" Style="font-size: 13px; color: white;" meta:resourcekey="lblPatientName1Resource1"></asp:Label>
                                    <div class="clear"></div>
                                    <span style="font-size: 13px; color: white;">
                                        <%= this.GetLocalResourceObject("createddate") %>
                                        <asp:Label ID="lbldate2" runat="server"></asp:Label>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile13" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile13Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile13" runat="server" ControlToValidate="fuFile13" Display="Dynamic" ValidationGroup="validation3" meta:resourcekey="rfvFile13Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile14" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile14Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile14" runat="server" ControlToValidate="fuFile14" Display="Dynamic" ValidationGroup="validation3" meta:resourcekey="rfvFile14Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile15" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile15Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile15" runat="server" ControlToValidate="fuFile15" Display="Dynamic" ValidationGroup="validation3" meta:resourcekey="rfvFile15Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuFile16" CssClass="FileUpload" onchange="validateImage(this)" runat="server" meta:resourcekey="fuFile16Resource1" />
                            <asp:RequiredFieldValidator ID="rfvFile16" runat="server" ControlToValidate="fuFile16" Display="Dynamic" ValidationGroup="validation3" meta:resourcekey="rfvFile16Resource1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <%--<div class="date2">
                        <div class="date_cont">
                            <div class="date_cont_right marginlf">
                                <asp:Label runat="server" ID="lblAfterIsActive" Text="Is Active" meta:resourcekey="lblAfterIsActiveResource1"></asp:Label>
                                <span class="starcl">*</span>
                            </div>
                        </div>
                        <div class="date_cont">
                            <div class="date_cont_right">
                                <asp:CheckBox ID="chkAfterIsActive" runat="server" Checked="True" TabIndex="3" meta:resourcekey="chkAfterIsActiveResource1" />
                            </div>
                        </div>
                    </div>--%>
                </asp:Panel>
            </fieldset>
        </div>
    </div>
    <div style="display: none">
        <%--<asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>--%>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" OnClientClick="loadingoverlayShow()"
            OnClick="btnSubmit_Click" TabIndex="14" meta:resourcekey="btnSubmitResource1" />
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <asp:HiddenField ID="hdnImages" runat="server" />
    <asp:HiddenField ID="select_tab" runat="server" />
    <asp:HiddenField ID="selected_tab" runat="server" />
    <asp:HiddenField ID="hdnPassword" runat="server" />
    <asp:HiddenField ID="hdnPatientId" runat="server" />
    <asp:HiddenField ID="hdnBeforeId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnAfterId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnSkip" runat="server" Value="false" />
    <script type="text/javascript">
        function BindBeforeImages() {
            var beforeId = '<%= beforeId %>';
            if (beforeId > 0) {
                var hdnImages = document.getElementById('<%= hdnImages.ClientID %>').value;                
                if (hdnImages != "") {
                    hdnImages = hdnImages.toString().split(',');
                    for (var i = 0; i < 8 ; i++) {
                        var img = $('<img>');
                        img.attr('src', "/PatientFiles/slides/" + hdnImages[i]);
                        img.attr('class', 'simpleFilePreview_preview');
                        img.appendTo("#simpleFilePreview_" + i);
                        document.getElementById("simpleFilePreview_" + i).getElementsByTagName("a")['0'].style.display = "none";
                    }
                }
            }
        }
        function BindAfterImages() {
            var afterId = '<%= afterId %>';
            if (afterId > 0) {
                var hdnImages = document.getElementById('<%= hdnImages.ClientID %>').value;
                hdnImages = hdnImages.toString().split(',');
                if (hdnImages.length > 8) {
                    for (var i = 8; i < 16 ; i++) {
                        var img = $('<img>');
                        img.attr('src', "/PatientFiles/slides/" + hdnImages[i]);
                        img.attr('class', 'simpleFilePreview_preview');
                        img.appendTo("#simpleFilePreview_" + i);
                        document.getElementById("simpleFilePreview_" + i).getElementsByTagName("a")['0'].style.display = "none";
                    }
                }
            }
        }
    </script>
</asp:Content>
