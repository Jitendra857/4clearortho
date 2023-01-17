<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditProductMaster.aspx.cs" Inherits="_4eOrtho.Admin.AddEditProductMaster" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Styles/lightbox.min.css" rel="stylesheet" />
    <script type="text/javascript"  src="../Scripts/lightbox-plus-jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#' + '<%= txtProductName.ClientID %>').focus();
            AjaxFileUpload_change_text();
        });

        function AjaxFileUpload_change_text() {
            // you can change text here
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
            img.setAttribute("src", "Images/delete.png");
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
            anchor.setAttribute("title", "<%= this.GetLocalResourceObject("ViewFullImage").ToString() %>");
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
   <%-- <a class="example-image-link" href="http://lokeshdhakar.com/projects/lightbox2/images/image-1.jpg" data-lightbox="example-1"><img class="example-image"  src="http://lokeshdhakar.com/projects/lightbox2/images/thumb-1.jpg" /></a>--%>
    <div id="container" class="cf">
        <div class="page_title">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        <h2 class="padd">
                            <asp:Label ID="lblHeader" runat="server" Text="Add Product Master" meta:resourcekey="lblHeaderResource1"></asp:Label>
                            <%--<asp:Label ID="lblHeaderEdit" runat="server" Text="Edit Product Master" Visible="false"></asp:Label>--%>
                        </h2>
                    </td>
                    <td style="width: 50%;">
                        <span class="dark_btn_small">
                            <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" PostBackUrl="~/Admin/ListProductMaster.aspx" TabIndex="7" meta:resourcekey="btnBackResource1" />
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
                <div id="divproductname" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label runat="server" ID="lblProductName" Text="Product Name" meta:resourcekey="lblProductNameResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span></label>

                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtProductName" runat="server" MaxLength="50" TabIndex="1" meta:resourcekey="txtProductNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvProductName" ForeColor="Red" runat="server" ControlToValidate="txtProductName"
                            SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Product Name"
                            CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvProductNameResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveProductName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvProductName" Enabled="True" />
                        <asp:CustomValidator runat="server" ID="custxtProductName" ControlToValidate="txtProductName"
                            SetFocusOnError="True" Display="None" OnServerValidate="custxtProductName_ServerValidate"
                            ValidationGroup="validation" CssClass="error" ErrorMessage="This Product Name already exist, please try another one" meta:resourcekey="custxtProductNameResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveUniqueProductName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="custxtProductName" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </div>
                </div>

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
                <div id="amount" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label ID="lblAmount" runat="server" Text="Amount ($)" meta:resourcekey="lblAmountResource1"></asp:Label><span
                            class="asteriskclass">*</span><span class="alignright">:</span></label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtAmount" runat="server" MaxLength="15" Style="text-align: right;" TabIndex="2" meta:resourcekey="txtAmountResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvAmount" ForeColor="Red" runat="server" SetFocusOnError="True"
                            ControlToValidate="txtAmount" Display="None" ErrorMessage="Please enter amount."
                            CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvAmountResource1" />
                        <asp:RegularExpressionValidator ID="rgvAmount" runat="server" ControlToValidate="txtAmount"
                            SetFocusOnError="True" ValidationExpression="\d+(\.\d{1,2})?" ValidationGroup="validation" Display="None"
                            CssClass="errormsg" ErrorMessage="Only Numeric Values with two precesion values is allowed"
                            meta:resourcekey="rgvAmountResource1"></asp:RegularExpressionValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteMobile" runat="server" Enabled="True"
                            TargetControlID="txtAmount" ValidChars="0123456789." />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqvefuploadPhoto" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvAmount" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rgveAmount" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rgvAmount" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </div>
                </div>
                <div id="emailid" class="parsonal_textfild" runat="server">
                    <label>
                        <asp:Label runat="server" ID="lblDescription" Text="Description" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                        <span class="alignright">:</span></label>

                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"
                        TabIndex="3" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>

                </div>


                <div class="parsonal_textfild">
                    <label>
                        <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="4" meta:resourcekey="chkIsActiveResource1" />
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
   
</asp:Content>
