<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditGallery.aspx.cs" Inherits="_4eOrtho.Admin.AddEditGallery" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/common.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery.simpleFilePreview.js"></script>
    <link rel="stylesheet" href="../Styles/simpleFilePreview.css" />
    <style type="text/css">
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
        }

        .divAdd {
            float: right;
            margin-top: -19%;
        }

        .divAddAndRemove {
            margin-top: -14%;
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        var counter = 0;
        $("#<%=btnAdd.ClientID%>").live("click", function () {
            var div = document.createElement('DIV');
            div.className = "UploadImgBox";
            div.id = "divBeforeBox" + counter;
            div.innerHTML = '<input id="BeforeFile' + counter + '" class="FileUploadBefore" name = "file' + counter + '" type="file" onchange="validateImage(this)" />';
            document.getElementById('<%= divFileUpload.ClientID %>').appendChild(div);
            $('#BeforeFile' + counter).simpleFilePreview({
                'buttonContent': 'Drop/Select Before Picture'
            });

            var div = document.createElement('DIV');
            div.className = "UploadImgBox";
            div.id = "divAftereBox" + counter;
            div.innerHTML = '<input id="AfterFile' + counter + '" class="FileUploadAfter" name = "file' + counter + '" type="file" onchange="validateImage(this)" />';
            document.getElementById('<%= divFileUpload.ClientID %>').appendChild(div);
            $('#AfterFile' + counter).simpleFilePreview({
                'buttonContent': 'Drop/Select After Picture'
            });

            var divAdd = document.getElementById('divAdd');
            divAdd.className = "divAddAndRemove";

            var div = document.createElement('DIV');
            div.className = "removediv";

            var btnRemove = document.createElement('button');
            btnRemove.id = counter;
            btnRemove.className = "Remove"

            var imgRemove = document.createElement('img');
            imgRemove.src = "Images/Gallary_delete.png";
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
            if (beforeCount != afterCount || $(".FileUploadAfter").length != afterCount || $(".FileUploadBefore").length != beforeCount) {
                alert("Please select both before and after images.");
                return false;
            }
            return true;
        }

        function pageLoad() {
            $('.FileUploadBefore').simpleFilePreview({
                'buttonContent': 'Drop/Select Before Picture'
            });
            $('.FileUploadAfter').simpleFilePreview({
                'buttonContent': 'Drop/Select After Picture'
            });

            $(".FileUpload").change(function () {
                if (typeof (FileReader) != "undefined") {
                    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                    $($(this)[0].files).each(function () {
                        var file = $(this);
                        if (file.length > 0) {
                            var size = parseFloat(file[0].size / 1024).toFixed(2);
                            if (regex.test(file[0].name.toLowerCase())) {
                                if (size > 2024) {
                                    alert("File size can't be more than 2MB !");
                                    return false;
                                }
                            } else {
                                alert(file[0].name + " is not a valid image file.");
                                return false;
                            }
                        }
                    });
                } else {
                    alert("This browser does not support HTML5 FileReader.");
                }
            });
            BindBeforeImages();
        }
        function validateImage(objFile) {
            if (typeof (FileReader) != "undefined") {
                var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                var file = $(objFile)[0];
                if (file.files.length > 0) {
                    var size = parseFloat(file.files[0].size / 1024).toFixed(2);
                    if (regex.test(file.files[0].name.toLowerCase())) {
                        if (size > 2024) {
                            alert("File size can't be more than 2MB !");
                            objFile.value = "";
                            return false;
                        }

                        var reader = new FileReader();
                        reader.onload = function (e) {
                        }
                        reader.readAsDataURL(file.files[0]);
                    } else {
                        alert(file.files[0].name + " is not a valid image file.");
                        return false;
                    }
                }
            } else {
                alert("This browser does not support HTML5 FileReader.");
            }
            return true;
        }
        $(document).ready(function () {
            $('#' + '<%= txtCondition.ClientID %>').focus();
        });
            function AjaxFileUpload_change_text() {
                Sys.Extended.UI.Resources.AjaxFileUpload_SelectFile = "<%= this.GetLocalResourceObject("SelectFile").ToString() %>"; // Select Files text change
                Sys.Extended.UI.Resources.AjaxFileUpload_DropFiles = "<%= this.GetLocalResourceObject("DropFilesHere").ToString() %>";
            }
            function onClientUploadComplete(sender, e) {
                onImageValidated("TRUE", e);
                $('.ajax__fileupload_queueContainer').css('display', 'none');
            }
            function onImageValidated(arg, context) {

                var test = document.getElementById("testuploaded");
                test.style.display = 'block';

                var fileList = document.getElementById("ContentPlaceHolder1_fileList");
                var item = document.createElement('div');
                item.style.padding = '4px';
                item.style.border = '1px solid #d3d3d3';
                if (arg == "TRUE") {
                    var url = context.get_postedUrl();
                    url = url.replace('&amp;', '&');
                    item.appendChild(createThumbnail(context, url));
                } else {
                    item.appendChild(createFileInfo(context));
                }

                fileList.appendChild(item);
            }

            function createFileInfo(e) {
                var holder = document.createElement('div');
                holder.appendChild(document.createTextNode(e.get_fileName()));
                return holder;
            }

            function createThumbnail(e, url) {
                var holder = document.createElement('div');
                var img = document.createElement("img");
                img.style.width = '80px';
                img.style.height = '80px';
                img.style.marginTop = '10px';
                img.setAttribute("src", url);
                holder.appendChild(createFileInfo(e));
                holder.appendChild(img);
                return holder;
            }
    </script>

    <div id="container" class="cf">
        <asp:HiddenField ID="hdnCounter" runat="server" Value="4" />
        <div class="page_title">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        <h2 class="padd">
                            <asp:Label ID="lblHeader" runat="server" Text="Add Patient Gallery" meta:resourcekey="lblHeaderResource1"></asp:Label>
                        </h2>
                    </td>
                    <td style="width: 50%;">
                        <span class="dark_btn_small">
                            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" Width="100px" TabIndex="9" meta:resourcekey="btnBackResource1" />
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div id="divMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
            </div>
        </div>
        <div class="widecolumn">
            <div class="personal_box alignleft">
                <div class="parsonal_textfild">
                    <label style="width: 170px;">
                        <asp:Label ID="lblPatient" runat="server" Text="Select Patient:" meta:resourcekey="lblPatientResource1"></asp:Label><span
                            class="asteriskclass">*</span><span class="alignright">:</span></label>
                    <div class="parsonal_textfild">
                        <asp:TextBox ID="txtCondition" runat="server" MaxLength="150"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPatient" ForeColor="Red" runat="server" ControlToValidate="txtCondition"
                            Display="None" ErrorMessage="Please enter Patient Name" CssClass="errormsg"
                            SetFocusOnError="True" ValidationGroup="validation" meta:resourcekey="rfvPatientResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="vceDoctor" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rfvPatient" Enabled="True" />
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild" style="width: 565px;">
                    <label style="width: 170px;">
                        <asp:Label ID="lblPictures" runat="server" Text="Select Picture:" meta:resourcekey="lblPicturesResource1"></asp:Label><span
                            class="asteriskclass">*</span><span class="alignright">:</span></label>
                    <div class="UploadImgBox">
                        <asp:FileUpload ID="fuFile1" CssClass="FileUploadBefore" onchange="validateImage(this)" runat="server" Style="height: 100% !important; width: 100% !important;" />
                    </div>
                    <div class="UploadImgBox">
                        <asp:FileUpload ID="fuFile2" CssClass="FileUploadAfter" onchange="validateImage(this)" runat="server" Style="height: 100% !important; width: 100% !important;" />
                    </div>
                    <div class="clear"></div>
                    <div id="divFileUpload" runat="server" style="width: 500px; float: left; margin-left: 178px;"></div>
                    <div id="divAdd" class="divAdd">
                        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Admin/Images/Gallary_add.png" />
                    </div>
                </div>
                <div class="parsonal_textfild gallerylist" id="testuploaded">
                    <div id="fileList" runat="server">
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild">
                    <label style="width: 170px;">
                        <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div style="padding-top: 7px;">
                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="5" meta:resourcekey="chkIsActiveResource1" />
                    </div>
                </div>
                <div class="clear"></div>
                <div class="parsonal_textfild">
                    <label style="width: 170px;">
                        <asp:Label runat="server" ID="Label1" Text="Is Homepage" meta:resourcekey="Label1Resource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div style="padding-top: 7px;">
                        <asp:CheckBox ID="chkHomePageDisplay" runat="server" Checked="True" TabIndex="6" meta:resourcekey="chkHomePageDisplayResource1" />
                    </div>
                </div>
            </div>
        </div>
        <div class="bottom_btn tpadd alignright" style="width: 268px;">
            <span class="blue_btn">
                <asp:Button ID="btnSubmit" runat="server" Text="Save" ValidationGroup="validation"
                    OnClick="btnSubmit_Click" TabIndex="7" OnClientClick="javascript: return RequiredFilesValidate();" meta:resourcekey="btnSubmitResource1" />
            </span><span class="dark_btn">
                <asp:Button runat="server" ID="btnReset" Text="Reset" TabIndex="8" OnClick="btnReset_Click" meta:resourcekey="btnResetResource1" />
            </span>
        </div>
    </div>
    <asp:HiddenField ID="hdnImages" runat="server" />
    <script type="text/javascript">
        function BindBeforeImages() {
            var galleryId = '<%= galleryId %>';
            if (galleryId > 0) {                
                var hdnImages = document.getElementById('<%= hdnImages.ClientID %>').value;
                hdnImages = hdnImages.toString().split(',');
                for (var i = 0; i < hdnImages.length ; i++) {
                    if (i != 0 && i % 2 == 0)
                        $("#<%=btnAdd.ClientID%>").click();

                    var img = $('<img>');
                    img.attr('src', "/Photograph/" + hdnImages[i]);
                    img.attr('class', 'simpleFilePreview_preview');
                    img.appendTo("#simpleFilePreview_" + i);
                    document.getElementById("simpleFilePreview_" + i).getElementsByTagName("a")['0'].style.display = "none";
                }
            }
        }
    </script>
</asp:Content>
