<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="AddEditPatientGallery.aspx.cs" Inherits="_4eOrtho.AddEditPatientGallery" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-steps/jquery.steps.js"></script>
    <link rel="Stylesheet" type="text/css" href="Scripts/jquery-steps/jquery.steps.css" />
    <link href="Styles/common.css" rel="stylesheet" />
    <script src="Scripts/jquery.simpleFilePreview.js"></script>
    <link rel="stylesheet" href="Styles/simpleFilePreview.css" />
    <style type="text/css">
        .wizard > .steps a, .wizard > .steps a:hover, .wizard > .steps a:active {
            background: none repeat scroll 0 0 #eee;
            color: #aaa;
            cursor: default;
        }

        .imgUpload {
            border: 1px solid rgb(211, 211, 211);
            padding: 7px;
            width: 80px;
            float: left;
            margin: 2px;
        }

            .imgUpload img {
                margin-top: 0px;
            }

        .UploadImgBox {
            width: 175px;
            height: 175px;
            float: left;
            overflow: hidden;
            margin-top: 5px;
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

        .simpleFilePreview {
            margin-left: 0px;
        }

        .Remove {
            background: none;
            border: none;
        }

        .removediv {
            height: 140px;
            float: left;
            overflow: hidden;
            margin-top: 5px;
            padding-top: 7%;
            margin-left: -10px;
        }

        .divAdd {
            float: right;
            margin-top: -19%;
            margin-right: -25px;
        }

        .divAddAndRemove {
            margin-top: -14%;
            float: right;
            margin-right: -25px;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $('.FileUploadBefore').simpleFilePreview({
                'buttonContent': '<%= this.GetLocalResourceObject("DropSelect") + "<br/>" + this.GetLocalResourceObject("BeforePicture") %>'
            });
            $('.FileUploadAfter').simpleFilePreview({
                'buttonContent': '<%= this.GetLocalResourceObject("DropSelect") + "<br/>" + this.GetLocalResourceObject("AfterPicture") %>'
            });
            $('#' + '<%= ddlPatient.ClientID %>').focus();
            CreateWizard();
            BindBeforeImages();

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
        }

        var counter = 0;
        $("#<%=btnAdd.ClientID%>").live("click", function () {
            var div = document.createElement('DIV');
            div.className = "UploadImgBox";
            div.id = "divBeforeBox" + counter;
            div.innerHTML = '<input id="BeforeFile' + counter + '" class="FileUploadBefore" name = "file' + counter + '" type="file" onchange="validateImage(this)" />';
            document.getElementById('<%= divFileUpload.ClientID %>').appendChild(div);
            $('#BeforeFile' + counter).simpleFilePreview({
                'buttonContent': "<%= this.GetLocalResourceObject("DropSelect") + "<br/>" + this.GetLocalResourceObject("BeforePicture") %>"
            });

            var div = document.createElement('DIV');
            div.className = "UploadImgBox";
            div.id = "divAftereBox" + counter;
            div.innerHTML = '<input id="AfterFile' + counter + '" class="FileUploadAfter" name = "file' + counter + '" type="file" onchange="validateImage(this)" />';
            document.getElementById('<%= divFileUpload.ClientID %>').appendChild(div);
            $('#AfterFile' + counter).simpleFilePreview({
                'buttonContent': "<%= this.GetLocalResourceObject("DropSelect") + "<br/>" + this.GetLocalResourceObject("AfterPicture") %>"
            });

            var divAdd = document.getElementById('divAdd');
            divAdd.className = "divAddAndRemove";

            var div = document.createElement('DIV');
            div.className = "removediv";

            var btnRemove = document.createElement('button');
            btnRemove.id = counter;
            btnRemove.className = "Remove"

            var imgRemove = document.createElement('img');
            imgRemove.src = "admin/Images/Gallary_delete.png";
            btnRemove.appendChild(imgRemove);

            btnRemove.onclick = (function () {
                if (this.id > 0)
                    $(this).parent().parent().find('#divClear' + this.id - 1).remove();
                $(this).parent().remove();
                $("#divBeforeBox" + this.id).remove();
                $("#divAftereBox" + this.id).remove();
                $(this).remove();
                if ($('#ContentPlaceHolder1_divFileUpload').find('.removediv').length == 0)
                    divAdd.className = "divAdd";
            });

            div.appendChild(btnRemove);

            document.getElementById('<%= divFileUpload.ClientID %>').appendChild(div);

            var div = document.createElement('DIV');
            div.id = "divClear" + counter;
            div.className = "clear";

            document.getElementById('<%= divFileUpload.ClientID %>').appendChild(div);

            counter++;
            return false;
        });

        function RequiredFilesValidate() {
            var ddlPatientSelected = $('#' + '<%= ddlPatient.ClientID %>')[0].selectedIndex;
            if (ddlPatientSelected == '0') {
                alert("<%= this.GetLocalResourceObject("rfvPatientResource1.ErrorMessage") %>");
                return false;
            }

            var txtTreatment = $('#' + '<%= txtTreatment.ClientID %>')[0].value;
            if (txtTreatment == '') {
                alert("<%= this.GetLocalResourceObject("rqvTreatmentResource1.ErrorMessage") %>");
                return false;
            }

            var isTwoImageTemplate = $('#<%=rbtnIsTemplate.ClientID %> input:checked').val();
            if (isTwoImageTemplate == '2') {
                var beforeCount = 0;
                var afterCount = 0;
                if ($(".FileUploadBefore").length > 0) {
                    for (var i = 0 ; i < $(".FileUploadBefore").length ; i++) {
                        if ($(".FileUploadBefore")[i] != null && $(".FileUploadBefore")[i].nextSibling != null && $(".FileUploadBefore")[i].nextSibling.src != null)
                            beforeCount++;
                    }
                }
                if ($(".FileUploadAfter").length > 0) {
                    for (var i = 0 ; i < $(".FileUploadAfter").length ; i++) {
                        if ($(".FileUploadAfter")[i] != null && $(".FileUploadAfter")[i].nextSibling != null && $(".FileUploadAfter")[i].nextSibling.src != null)
                            afterCount++;
                    }
                }
                if (beforeCount != 0 && afterCount != 0) {
                    if (beforeCount != afterCount || $(".FileUploadAfter").length != afterCount || $(".FileUploadBefore").length != beforeCount) {
                        alert("<%= this.GetLocalResourceObject("selectboth") %>");
                        return false;
                    }
                }
                else {
                    alert("<%= this.GetLocalResourceObject("selectboth") %>");
                    return false;
                }
            }
            else {
                Page_ClientValidate("validation2");
                if (Page_IsValid) {
                    Page_ClientValidate("validation3");
                    if (Page_IsValid) {
                        return true;
                    }
                    else {
                        alert("<%=this.GetLocalResourceObject("AfterErrorMessage").ToString()%>");
                        return false;
                    }
                    return true;
                }
                else {
                    alert("<%=this.GetLocalResourceObject("BeforeErrorMessage").ToString()%>");
                    return false;
                }
            }
            loadingoverlayShow();
            return true;
        }
        function validateImage(objFile) {
            if (typeof (FileReader) != "undefined") {
                var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
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
                        return false;
                    }
                }
            } else {
                alert("This browser does not support HTML5 FileReader.");
            }
            return true;
        }
        function CreateWizard() {
            var form = $("#widget").show();
            form.steps({
                enablePagination: false,
                enableAllSteps: '<%= patientGalleryId > 0 ? true : false%>',
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
                }
            });
            $('.actions > ul > li:last-child').attr('style', 'display:none');
        }

        function txtEmailChange() {
            var patient = $('#<%=ddlPatient.ClientID %> option:selected').text();
            document.getElementById('<%= lblPname2.ClientID %>').innerHTML = patient;
            document.getElementById('<%= lblPName.ClientID %>').innerHTML = patient;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title main_right_cont" style="width: 100%;">
        <div class="supply-button2 back">
            <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/ListPictureTemplate.aspx" Width="100px" TabIndex="6" meta:resourcekey="btnBackResource1" />
        </div>
        <h2>
            <asp:Label ID="lblHeader" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label>
        </h2>
    </div>
    <div class="main_right_cont minheigh">
        <div id="divMsg" runat="server">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </div>
        <div class="personal_box alignleft">
            <div class="parsonal_textfild">
                <label>
                    <asp:Label ID="lblPatient" runat="server" Text="Select Patient" meta:resourcekey="lblPatientResource1"></asp:Label>
                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                </label>
                <div class="parsonal_select">
                    <asp:DropDownList ID="ddlPatient" runat="server" TabIndex="1" meta:resourcekey="ddlPatientResource1" onchange="txtEmailChange()"></asp:DropDownList>
                </div>
            </div>
            <div class="clear"></div>
            <div class="parsonal_textfild">
                <label>
                    <asp:Label runat="server" ID="lblSelectTemplate" Text="Select template" meta:resourcekey="lblSelectTemplateResource1"></asp:Label>
                </label>
                <div class="radio-selection">
                    <asp:RadioButtonList ID="rbtnIsTemplate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtnIsTemplate_SelectedIndexChanged" CausesValidation="false" Width="100%">
                        <asp:ListItem Text="Two Image Template" Selected="True" Value="2" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                        <asp:ListItem Text="Before - After Image Template" Value="8" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="clear"></div>
            <div class="parsonal_textfild">
                <label>
                    <asp:Label ID="lblTreatmentName" runat="server" Text="Treatment" meta:resourcekey="lblTreatmentNameResource1"></asp:Label>
                    <span class="alignright">:<span class="asteriskclass">*</span></span>
                </label>
                <asp:TextBox ID="txtTreatment" runat="server" MaxLength="50" TabIndex="2" meta:resourcekey="txtTreatmentResource1"></asp:TextBox>
            </div>
            <div class="clear"></div>            
            <div>
                <asp:PlaceHolder ID="phTwoImageTemplate" runat="server">
                    <div class="clear"></div>
                    <div class="parsonal_textfild">
                        <label>
                            <asp:Label ID="lblPictures" runat="server" Text="Select Picture:" meta:resourcekey="lblPicturesResource1"></asp:Label>
                            <span class="alignright">:<span class="asteriskclass">*</span></span>
                        </label>
                    </div>
                    <div class="parsonal_textfild" style="width: 565px;">
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuTwoImageFile1" CssClass="FileUploadBefore" onchange="validateImage(this)" runat="server" Style="height: 100% !important; width: 100% !important;" />
                        </div>
                        <div class="UploadImgBox">
                            <asp:FileUpload ID="fuTwoImageFile2" CssClass="FileUploadAfter" onchange="validateImage(this)" runat="server" Style="height: 100% !important; width: 100% !important;" />
                        </div>
                        <div id="divFileUpload" runat="server" style="width: 500px; float: left; margin-left: 208px;"></div>
                        <div id="divAdd" class="divAdd">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Admin/Images/Gallary_add.png" />
                        </div>
                    </div>
                    <div class="parsonal_textfild gallerylist" id="testuploaded">
                        <div id="fileList" runat="server">
                        </div>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phEightImageTemplate" runat="server" Visible="false">
                    <div class="widecolumn" id="widget">
                        <h3>
                            <asp:Label ID="lblBeforeTemplate" runat="server" Text="Before Template" meta:resourcekey="BeforeTemplateResource1"></asp:Label>
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
                                                <asp:Label ID="lblBeforeAfter" runat="server" Style="font-size: 15px; color: white;" meta:resourcekey="BeforeTreatmentResource1">Before Treatment</asp:Label>
                                                <div class="clear"></div>
                                                <img src="Content/images/logo.png" style="width: auto !important; height: 60px; margin: 7px 15px 5px 35px;" />
                                                <div class="clear"></div>
                                                <asp:Label ID="lblDoctorName" runat="server" Style="font-size: 15px; color: white;" meta:resourcekey="lblDoctorNameResource1"></asp:Label>
                                                <div class="clear"></div>
                                                <asp:Label ID="lblPatientName" runat="server" Style="font-size: 13px; color: white;" meta:resourcekey="PatientResource1"></asp:Label>
                                                <asp:Label ID="lblPName" runat="server" Style="font-size: 13px; color: white;"></asp:Label>
                                                <div class="clear"></div>
                                                <span style="font-size: 13px; color: white;">
                                                    <%= this.GetLocalResourceObject("CreatedDateResource1.Text") %>
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
                            </asp:Panel>
                        </fieldset>
                        <h3>
                            <asp:Label ID="lblAfterTemplate" runat="server" Text="After Template" meta:resourcekey="AfterTemplateResource1"></asp:Label>
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
                                                <asp:Label ID="Label5" runat="server" Style="font-size: 15px; color: white;" meta:resourcekey="AfterTreatmentResource1">After Treatment</asp:Label>
                                                <div class="clear"></div>
                                                <img src="Content/images/logo.png" style="width: auto !important; height: 60px; margin: 7px 15px 5px 35px;" />
                                                <div class="clear"></div>
                                                <asp:Label ID="lblDoctorName1" runat="server" Style="font-size: 15px; color: white;" meta:resourcekey="lblDoctorName1Resource1"></asp:Label>
                                                <div class="clear"></div>
                                                <asp:Label ID="lblPatientName1" runat="server" Style="font-size: 13px; color: white;" meta:resourcekey="PatientResource1"></asp:Label>
                                                <asp:Label ID="lblPname2" runat="server" Style="font-size: 13px; color: white;"></asp:Label>
                                                <div class="clear"></div>
                                                <span style="font-size: 13px; color: white;">
                                                    <%= this.GetLocalResourceObject("CreatedDateResource1.Text") %>
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
                            </asp:Panel>
                        </fieldset>
                    </div>
                </asp:PlaceHolder>
                <div class="clear"></div>
                <div class="date2">
                    <div class="date_cont" style="display: none;">
                        <div class="date_cont_right marginlf">
                            <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                            <span class="starcl">*</span>
                        </div>
                    </div>
                    <div class="date_cont" style="display: none;">
                        <div class="date_cont_right">
                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="3" meta:resourcekey="chkIsActiveResource1" />
                        </div>
                    </div>
                    <div class="date2">
                        <%--<asp:UpdatePanel ID="upAddEdit" runat="server">
                            <ContentTemplate>--%>
                        <div class="date_cont">
                            <div class="date_cont_right">
                                <div class="supply-button3">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="validation" OnClientClick="javascript:return RequiredFilesValidate();"
                                        OnClick="btnSubmit_Click" TabIndex="4" meta:resourcekey="btnSubmitResource1" />
                                </div>
                                <div class="supply-button3">
                                    <asp:Button runat="server" ID="btnReset" Text="Cancel" TabIndex="5" OnClientClick="window.open(window.location.href,'_self');return false;" meta:resourcekey="btnResetResource1" />
                                    <asp:HiddenField ID="hdnFileName" runat="server" />
                                    <asp:HiddenField ID="hdnClassName" runat="server" />
                                    <asp:HiddenField ID="hdnFileList" runat="server" />
                                </div>
                            </div>
                        </div>
                        <%--</ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hdnImages" runat="server" />
        <script type="text/javascript">
            function BindBeforeImages() {
                var galleryId = '<%= patientGalleryId %>';
                if (galleryId > 0) {
                    var hdnImages = document.getElementById('<%= hdnImages.ClientID %>').value;
                    hdnImages = hdnImages.toString().split(',');
                    for (var i = 0; i < hdnImages.length ; i++) {
                        if (i != 0 && i % 2 == 0)
                            $("#<%=btnAdd.ClientID%>").click();

                        var img = $('<img>');
                        img.attr('src', "PatientFiles/thumbs/" + hdnImages[i]);
                        img.attr('class', 'simpleFilePreview_preview');
                        img.appendTo("#simpleFilePreview_" + i);
                        document.getElementById("simpleFilePreview_" + i).getElementsByTagName("a")['0'].style.display = "none";
                    }
                }
            }
        </script>
    </div>
</asp:Content>
