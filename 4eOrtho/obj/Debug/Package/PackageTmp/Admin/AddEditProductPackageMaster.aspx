<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditProductPackageMaster.aspx.cs" Inherits="_4eOrtho.Admin.AddEditProductPackageMaster" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Styles/lightbox.min.css" rel="stylesheet" />
    <script type="text/javascript"  src="../Scripts/lightbox-plus-jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#' + '<%= txtPackageName.ClientID %>').focus();
            AjaxFileUpload_change_text();
        });
        function onClientUploadComplete(sender, e) {
            onImageValidated("TRUE", e);
            $('.ajax__fileupload_queueContainer').css('display', 'none');
        }
        function AjaxFileUpload_change_text() {
            // you can change text here
            Sys.Extended.UI.Resources.AjaxFileUpload_SelectFile = "<%= this.GetLocalResourceObject("SelectFile").ToString() %>"; // Select Files text change
            Sys.Extended.UI.Resources.AjaxFileUpload_DropFiles = "<%= this.GetLocalResourceObject("DropFilesHere").ToString() %>";
        }
        function onImageValidated(arg, context) {

            var test = document.getElementById("testuploaded");
            test.style.display = 'block';

            var fileList = document.getElementById("ContentPlaceHolder1_fileList");
            var item = document.createElement('div');
            item.style.padding = '4px';
            item.style.display = 'inline-block';
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
        function DeleteMessage(obj) {
            if (confirm("<%= this.GetLocalResourceObject("DeleteMessage") %>"))
                return true;
            else
                return false;
        }

        function DeletePicture(filename) {
            if (confirm("<%= this.GetLocalResourceObject("DeleteMessage") %>")) {
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
            /*background-color: #fff;
            margin-bottom: 4px;
            padding: 6px !important;
            margin-left: 1px;
            border: 1px solid #cccccc;*/
            /*background-color: #fff;
            margin-bottom: 8px;
            padding: 0 !important;
            padding-bottom: 6px !important;
            margin-left: 10px;*/
        }
    </style>
    <div id="container" class="cf">
        <div class="page_title">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        <h2 class="padd">
                            <asp:Label ID="lblHeader" runat="server" meta:resourcekey="lblHeaderResource1"></asp:Label>

                        </h2>
                    </td>
                    <td style="width: 50%;">
                        <span class="dark_btn_small">
                            <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" TabIndex="10" OnClick="btnBack_Click" meta:resourcekey="btnBackResource1" />
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
                    <label>
                        <asp:Label runat="server" ID="lblPackageName" Text="Package Name" meta:resourcekey="lblPackageNameResource1"></asp:Label>
                        <span class="asteriskclass">*</span><span class="alignright">:</span></label>
                    <div class="ClientTextalign">
                        <asp:TextBox ID="txtPackageName" runat="server" MaxLength="50" TabIndex="1" meta:resourcekey="txtPackageNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvPackageName" ForeColor="Red" runat="server" ControlToValidate="txtPackageName"
                            SetFocusOnError="True" Display="None" ErrorMessage="Please Enter Package Name"
                            CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvPackageNameResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqvePackageName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvPackageName" Enabled="True" />
                        <asp:CustomValidator runat="server" ID="custxtPackageName" ControlToValidate="txtPackageName"
                            SetFocusOnError="True" Display="None" OnServerValidate="custxtPackageName_ServerValidate"
                            ValidationGroup="validation" CssClass="error" ErrorMessage="This Package Name already exist, please try another one" meta:resourcekey="custxtPackageNameResource1" />
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveUniquePackageName" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="custxtPackageName" Enabled="True">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="clear">
                </div>
                <asp:UpdatePanel ID="upProductDetails" runat="server">
                    <ContentTemplate>
                        <fieldset class="fieldsetclass">

                            <legend class="legendclass"><%= this.GetLocalResourceObject("ProductDetails") %></legend>
                            <div class="parsonal_textfild" style="margin-bottom: 32px;">
                                <label class="parsonal_textfildlbl">
                                    <asp:Label runat="server" ID="lblSelectProduct" Text="Select Product" meta:resourcekey="lblSelectProductResource1"></asp:Label>
                                    <span class="asteriskclass">*</span><span class="alignright">:</span></label>
                                <div class="parsonal_select">
                                    <asp:DropDownList ID="ddlProductName" runat="server" DataTextField="ProductName"
                                        DataValueField="ProductId" TabIndex="2" meta:resourcekey="ddlProductNameResource1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rqvProductName" ForeColor="Red" runat="server" ControlToValidate="ddlProductName"
                                        SetFocusOnError="True" Display="None"
                                        ErrorMessage="Please select Product Name" CssClass="errormsg"
                                        ValidationGroup="validationProduct" InitialValue="0" meta:resourcekey="rqvProductNameResource1" />
                                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveProductName" runat="server" CssClass="customCalloutStyle"
                                        TargetControlID="rqvProductName" Enabled="True" />
                                    <asp:CustomValidator runat="server" ID="custxtProductName" ControlToValidate="ddlProductName"
                                        SetFocusOnError="True" Display="None" OnServerValidate="custxtProductName_ServerValidate"
                                        ValidationGroup="validationProduct" CssClass="error" ErrorMessage="This Product Name already exist, please try another one" meta:resourcekey="custxtProductNameResource1" />
                                    <ajaxToolkit:ValidatorCalloutExtender ID="rqveUniqueProductName" runat="server" CssClass="customCalloutStyle"
                                        TargetControlID="custxtProductName" Enabled="True">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="parsonal_textfild">
                                <label class="parsonal_textfildlbl">
                                    <asp:Label ID="lblProductAmount" runat="server" Text="Quantity " meta:resourcekey="lblProductAmountResource1"></asp:Label><span
                                        class="asteriskclass">*</span><span class="alignright">:</span></label>
                                <asp:TextBox ID="txtQuantity" runat="server" MaxLength="15" Style="text-align: right;" TabIndex="3" meta:resourcekey="txtQuantityResource1"></asp:TextBox>


                                <%--<asp:Button ID="btnAdd" runat="server"  Text="Add Product" OnClick="btnAdd_Click" ValidationGroup="validationProduct" meta:resourcekey="btnAddResource1" TabIndex="4" />--%>
                                <asp:ImageButton runat="server" ImageUrl="~/Admin/Images/add.png" ID="btnAdd" TabIndex="4" ValidationGroup="validationProduct" OnClick="btnAdd_Click" />


                                <asp:RequiredFieldValidator ID="rqvQuantity" ForeColor="Red" runat="server" SetFocusOnError="True"
                                    ControlToValidate="txtQuantity" Display="None" ErrorMessage="Please enter Qunatity."
                                    CssClass="errormsg" ValidationGroup="validationProduct" meta:resourcekey="rqvQuantityResource1" />
                                <asp:RegularExpressionValidator ID="rgvQuantity" runat="server" ControlToValidate="txtQuantity"
                                    SetFocusOnError="True" ValidationExpression="^(0|[1-9][0-9]*)$" ValidationGroup="validationProduct"
                                    CssClass="errormsg" ErrorMessage="Only Numeric Values are allowed" meta:resourcekey="rgvQuantityResource1"></asp:RegularExpressionValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fteQuantity" runat="server" Enabled="True"
                                    TargetControlID="txtQuantity" ValidChars="0123456789" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="rqveQuantity" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rqvQuantity" Enabled="True" Width="140px">
                                </ajaxToolkit:ValidatorCalloutExtender>

                            </div>

                            <div class="parsonal_textfild legendtextfield">

                                <asp:Repeater ID="rptrProductDetails" runat="server" OnItemDataBound="rptrProductDetails_ItemDataBound" OnItemCommand="rptrProductDetails_ItemCommand">
                                    <HeaderTemplate>
                                        <table border="1" cellpadding="2" cellspacing="2" class="rgt">
                                            <tr>
                                                <td class="equip">
                                                    <asp:Label ID="lblProduct" runat="server" Text="Product Name" meta:resourcekey="lblProductResource1"></asp:Label>

                                                </td>
                                                <td class=" equip">
                                                    <asp:Label ID="lblQuant" runat="server" Text="Quantity" meta:resourcekey="lblQuantResource1"></asp:Label>


                                                </td>
                                                <td class=" equip">
                                                    <asp:Label ID="lblAmount" runat="server" Text="Amount" meta:resourcekey="lblAmountResource1"></asp:Label>


                                                </td>
                                                <td class="equip" align="center"><%= this.GetLocalResourceObject("Delete") %></td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="equip">
                                                <%#DataBinder.Eval(Container.DataItem, "ProductName")%>
                                            </td>
                                            <td class="equip">
                                                <%#DataBinder.Eval(Container.DataItem, "Quantity")%>   
                                            </td>
                                            <td class="equip">
                                                <%#DataBinder.Eval(Container.DataItem, "TotalAmount")%>   
                                            </td>
                                            <td align="center" class="equipImg">
                                                <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/Admin/Images/delete.png" CommandName="delete" OnClientClick='return DeleteMessage(this)'
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RowId") %>' meta:resourcekey="lnkDeleteResource1" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr id="trNoData" runat="server" visible="False">
                                            <td colspan="4" runat="server" class="equip" align="center">
                                                <asp:Label ID="lblEmptyData"
                                                    Text="No Data.." runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        </table>           
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>

                        </fieldset>

                        <div class="clear">
                        </div>
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblPictures" runat="server" Text="Select Picture:" meta:resourcekey="lblPicturesResource1"></asp:Label><span
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
                        <div class="clear">
                        </div>
                        <div class="parsonal_textfild">
                            <label>
                                <asp:Label ID="lblAmount" runat="server" Text="Amount ($)" meta:resourcekey="lblAmountResource1"></asp:Label><span
                                    class="asteriskclass">*</span><span class="alignright">:</span></label>

                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="15" Style="text-align: right;" TabIndex="5" meta:resourcekey="txtAmountResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqvAmount" ForeColor="Red" runat="server" SetFocusOnError="True"
                                ControlToValidate="txtAmount" Display="None" ErrorMessage="Please enter amount."
                                CssClass="errormsg" ValidationGroup="validation" meta:resourcekey="rqvAmountResource1" />
                            <asp:RegularExpressionValidator ID="rgvAmount" runat="server" ControlToValidate="txtAmount"
                                SetFocusOnError="True" ValidationExpression="\d+(\.\d{1,2})?" ValidationGroup="validation"
                                CssClass="errormsg" ErrorMessage="Only Numeric Values with two precesion values is allowed" Display="None"
                                meta:resourcekey="rgvAmountResource1"></asp:RegularExpressionValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteMobile" runat="server" Enabled="True"
                                TargetControlID="txtAmount" ValidChars="0123456789." />
                            <ajaxToolkit:ValidatorCalloutExtender ID="rqvefuploadPhoto" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rqvAmount" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <ajaxToolkit:ValidatorCalloutExtender ID="rgvcamount" runat="server" CssClass="customCalloutStyle"
                                TargetControlID="rgvAmount" Enabled="True">
                            </ajaxToolkit:ValidatorCalloutExtender>

                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="clear">
                </div>
                <div class="parsonal_textfild">
                    <label>
                        <asp:Label runat="server" ID="lblDescription" Text="Description" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                        <span class="alignright">:</span></label>

                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"
                        TabIndex="6" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>

                </div>
                <div class="clear">
                </div>
                <div class="parsonal_textfild phonetab">
                    <label class="commondoc">
                        <asp:Label runat="server" ID="lblIsCase" Text="" meta:resourcekey="lblIsCaseResource1"></asp:Label>
                        <span class="alignright"></span>
                    </label>
                    <div class="radio-selection" style="width: auto; margin-left: 205px;">
                        <asp:RadioButton ID="rbtnForCase" runat="server" GroupName="CASE" Text="For Case" Checked="true" meta:resourcekey="rbtnForCaseResource1" />
                        <asp:RadioButton ID="rbtnForAll" runat="server" GroupName="CASE" Text="For All" Checked="true" meta:resourcekey="rbtnForAllResource1" />
                    </div>
                </div>
                <div class="clear">
                </div>
                <div class="parsonal_textfild">
                    <label>
                        <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblIsActiveResource1"></asp:Label>
                        <span class="alignright">:</span>
                    </label>
                    <div style="padding-top: 7px;">
                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" TabIndex="7" meta:resourcekey="chkIsActiveResource1" />
                    </div>
                </div>
            </div>
        </div>
        <div class="bottom_btn tpadd alignright" style="width: 268px;">
            <span class="blue_btn">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="validation"
                    OnClick="btnSubmit_Click" TabIndex="8" meta:resourcekey="btnSubmitResource1" />
            </span><span class="dark_btn">
                <asp:Button runat="server" ID="btnReset" Text="Reset" TabIndex="9" OnClick="btnReset_Click" meta:resourcekey="btnResetResource1" />
            </span>
            <asp:HiddenField runat="server" ID="hdnPictureName" />
            <asp:Button runat="server" ID="btnRemovePicture" OnClick="btnRemovePicture_Click" Style="display: none;" />
        </div>
    </div>    
</asp:Content>
