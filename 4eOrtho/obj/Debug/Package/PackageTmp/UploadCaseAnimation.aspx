<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/OrthoInnerMaster.Master" CodeBehind="UploadCaseAnimation.aspx.cs" Inherits="_4eOrtho.UploadCaseAnimation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Styles/screen.css" type="text/css" media="screen" />
    <style type="text/css">
        .auto-style1 {
            width: 72px;
        }
        .bothsection .right_section {
    float: right;
    width: 239px;
}   
        .bothsection .left_section {
    float: left;
    width: 661px;
}
    </style>

    <link href="../Styles/lightbox.min.css" rel="stylesheet" />
    <script type="text/javascript"  src="../Scripts/lightbox-plus-jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            
            AjaxFileUpload_change_text();
        });

        function AjaxFileUpload_change_text() {
            // you can change text here
          <%--  Sys.Extended.UI.Resources.AjaxFileUpload_SelectFile = "<%= this.GetLocalResourceObject("SelectFile").ToString() %>"; // Select Files text change
            Sys.Extended.UI.Resources.AjaxFileUpload_DropFiles = "<%= this.GetLocalResourceObject("DropFilesHere").ToString() %>";--%>
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
            //item.style.border = '1px solid #d3d3d3';
            if (arg == "TRUE") {
                var url = context.get_postedUrl();
                url = url.replace('&amp;', '&');
                item.appendChild(createThumbnail(context, url));
            } else {
                //item.appendChild(createFileInfo(context));
            }

            fileList.appendChild(item);
        }

        function createFileInfo(e) {
            var holder = document.createElement('div');
            holder.appendChild(document.createTextNode(e.get_fileName()));
            return holder;
        }

        function createDeleteInfo(e) {
            var div = document.createElement('div');
            var span = document.createElement('span');
            var anchor = document.createElement('a');
            var img = document.createElement("img");
            div.style.textAlign = "center";
            //span.setAttribute("style", "margin-left: 30px;");
            img.setAttribute("src", "Content/Images/deletenew.png");
            img.setAttribute("onclick", "DeletePicture('" + e.get_fileName() + "')");
            anchor.appendChild(img);
            span.appendChild(anchor);
            div.appendChild(span);
            return div;
        }

        function createThumbnail(e, url) {
            var holder = document.createElement('div');
            var anchor = document.createElement('a');
            var img = document.createElement("img");
            anchor.setAttribute("href", url);
            anchor.setAttribute("class", "example-image-link");
            anchor.setAttribute("data-lightbox", "example-1");
           
            img.style.width = '85px';
            img.style.height = '85px';
            //img.style.marginTop = '10px';
            img.setAttribute("src", url);
            img.setAttribute("class", "example-image");
            anchor.appendChild(img);
            //holder.appendChild(createFileInfo(e));
            holder.appendChild(anchor);
            holder.appendChild(createDeleteInfo(e));

            return holder;
        }

        function DeletePicture(filename) {
            if (confirm("Are you sure you wan't to delete?")) {
                $('#' + '<%= hdnPictureName.ClientID %>').val(filename);
                $('#' + '<%= btnRemovePicture.ClientID %>').click();
            }
            else {
                $('#' + '<%= hdnPictureName.ClientID %>').val("");
            }
        }
    </script>
    <style type="text/css">
        #ContentPlaceHolder1_fileList > div img:last-child {
            margin-top: 5px;
        }

        .example-image-link > img {
            margin:0 !important;
        }

        #ContentPlaceHolder1_fileList > div a:first-child {
            margin: 0px;
        }

        #ContentPlaceHolder1_fileList > div {
            background-color: #fff;
            margin-bottom: 5px;
            padding: 3px !important;
            padding-bottom: 5px !important;
            margin-right: 2px;
            border:1px solid #d8d8d8;
            display: inline-block;
            /*background-color: #fff;
            margin-bottom: 8px;
            padding: 0 !important;
            padding-bottom: 6px !important;
            margin-left: 10px;
            display: inline-block;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upNewCase" runat="server">
        <ContentTemplate>
            <div class="rightbar">
                <div class="main_right_cont" style="width: 100%;">
   
         <div class="title">
                        <div class="supply-button2 back">
                          <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/ListNewCase.aspx" TabIndex="7" meta:resourcekey="btnBackResource1" />
                        </div>
                        <h2>
                            <asp:Label runat="server" ID="lblProductList" Text="Upload / Case Animation" meta:resourcekey="lblProductListResource1"></asp:Label>
                        </h2>
                    </div>
        <div>
            <div id="divMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
            </div>
        </div>
        <div class="widecolumn">
            <div class="personal_box alignleft">
               

                <div class="parsonal_textfild">
                    <label>
                        <asp:Label ID="lblPictures" runat="server" Text="Select Picture" meta:resourcekey="lblPicturesResource1"></asp:Label><span
                            class="asteriskclass">*</span><span class="alignright">:</span></label>

                    <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server" ThrobberID="PhotoPreview" AllowedFileTypes="jpg,jpeg,png,gif"
                        MaximumNumberOfFiles="10" OnUploadComplete="File_Upload" OnClientUploadComplete="onClientUploadComplete"
                        Width="500px" />

                </div>
                <div class="parsonal_textfild packagelist" id="testuploaded">
                    <div id="fileList" runat="server">
                        <%--<div style="padding: 4px; border: 1px solid rgb(211, 211, 211);">
                            <div>
                                <div>463893502_610.jpg with size 34176 bytes</div>
                                <img style="width: 80px; height: 80px;" src="../Photograph/916467b6-722e-48a1-9c40-3569a827bf31.jpg">
                            </div>
                        </div>--%>
                    </div>
                </div>
             
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="bottom_btn tpadd alignright" style="width: 268px;">
            <span class="blue_btn">
                <asp:Button ID="btnSubmit" runat="server" Text="Save" ToolTip="Save" ValidationGroup="validation"
                    OnClick="btnSubmit_Click" TabIndex="5" meta:resourcekey="btnSubmitResource1" />
            </span><span class="dark_btn">
                <asp:Button runat="server" ID="btnReset" Text="Reset" TabIndex="6" OnClick="btnReset_Click" meta:resourcekey="btnResetResource1" />
            </span>
            <asp:HiddenField runat="server" ID="hdnPictureName" />
            <asp:Button runat="server" ID="btnRemovePicture" OnClick="btnRemovePicture_Click" Style="display: none;" />
        </div>

    </div>
     </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
