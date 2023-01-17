<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="ListPatientGallery.aspx.cs" Inherits="_4eOrtho.ListPatientGallery" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery.smooth-scroll.min.js"></script>
    <script src="Scripts/jquery.flexslider.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/jquery.smooth-scroll.min.js"></script>
    <script type="text/javascript" src="Scripts/lightbox.js"></script>
    <link rel="stylesheet" href="Styles/screen.css" type="text/css" media="screen" />
    <link href="Styles/lightbox.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
        .beforeImgBox {
            background-image: url('content/images/before.png');
            background-size: 50% 50%;
            background-repeat: no-repeat;
            border: 5px solid White;
            box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.5);
            border-radius: 4px;
            float: left;
            opacity: 0.8;
            text-decoration: none;
            font-size: medium;
            font-weight: bold;
            color: #000;
            text-align: center;
            vertical-align: middle;
        }

        .afterImgBox {
            background-image: url('content/images/after.png');
            background-size: 50% 50%;
            background-repeat: no-repeat;
            box-shadow: 0 1px 4px 0 rgba(0, 0, 0, 0.5);
            border-radius: 2px;
            border: 5px solid White;
            float: left;
            opacity: 0.8;
            text-decoration: none;
            font-size: medium;
            font-weight: bold;
            color: #000;
            text-align: center;
            vertical-align: middle;
        }

        .beforeImgBox a {
            opacity: 1;
            color: black;
        }

        .linkStyle {
            color: #79797a;
            padding-top: 36px;
            display: table-cell;
            text-shadow: 0 0 15px rgba(255, 255, 255, 0.5), 0 0 10px rgba(255, 255, 255, 0.5),0 0 15px rgba(255, 255, 255, 0.5), 0 0 10px rgba(255, 255, 255, 0.5),0 0 15px rgba(255, 255, 255, 0.5), 0 0 10px rgba(255, 255, 255, 0.5),0 0 15px rgba(255, 255, 255, 0.5), 0 0 10px rgba(255, 255, 255, 0.5);
            font-size: 15px;
            text-decoration: none;
        }

            .linkStyle:hover {
                color: #016dae;
            }

        .whitetext img {
            height: 20px;
            width: 20px;
        }

        .wrap {
            position: relative;
            float: left;
            clear: none;
            overflow: hidden;
            color: white;
        }

        .single .wrap {
            /*width: 202px;*/
        }

        .wrap img {
            position: relative;
            z-index: 1;
            margin: 0px;
            padding: 0px;
        }

        .wrap .before {
            display: block;
            position: absolute;
            width: 100%;
            top: -2%;
            left: 0;
            z-index: 2;
            text-align: center;
        }

        .wrap .after {
            display: block;
            position: absolute;
            width: 100%;
            top: 84%;
            left: 0;
            z-index: 2;
            text-align: center;
        }

        .grenchk {
            float: none;
        }
    </style>
    <script type="text/javascript">
        function DeleteMessage(obj) {
            if (confirm("<%= this.GetLocalResourceObject("DeleteMessage") %>"))
                return true;
            else
                return false;
        }
        function DeactivateMessage(obj) {
            if (obj.title == "<%= this.GetLocalResourceObject("Active") %>") {
                if (confirm("<%= this.GetLocalResourceObject("DeactivateMessage") %>"))
                    return true;
                else
                    return false;
            }
            else {
                if (confirm("<%= this.GetLocalResourceObject("ActivateMessage") %>"))
                    return true;
                else
                    return false;
            }
        }
        function ShowImgTemplate(id, doctorname, pname, treatment) {
            jQuery('.imgTemplate').attr("href", "PatientBeforeAfterImgPopup.aspx?galleryId=" + id + "&dname=" + doctorname + "&pname=" + pname + "&treatment=" + treatment);
            jQuery('.imgTemplate')[0].click();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="rightbar">
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div class="main_right_cont" style="width: 100%;">
                    <div class="title">
                        <div class="supply-button2 back">
                            <asp:Button ID="btnAddGallery" runat="server" Text="Add Patient Gallery" OnClick="btnAddGallery_Click" meta:resourcekey="btnAddGalleryResource1" />
                        </div>
                        <h2>
                            <asp:Label runat="server" ID="lblProductList" Text="Gallery List" meta:resourcekey="lblProductListResource1"></asp:Label>
                        </h2>
                    </div>
                    <div class="date2">
                        <div class="date_cont" style="width: 46%;">
                            <div class="date_cont_right">
                                <div class="Sort_by">
                                    <div class="product_dropdown">
                                        <asp:DropDownList ID="ddlGallery" runat="server" CssClass="low_high_search" meta:resourcekey="ddlGalleryResource1">
                                            <asp:ListItem Value="0" Text="Select Search Type" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                            <asp:ListItem Value="Treatment" Text="Condition" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="parsonal_textfild" style="padding: 0; float: right; width: 54%;">
                            <div class="date_cont_right">
                                <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" meta:resourcekey="txtSearchValResource1"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rqvSearchVal" runat="server" Display="None" ValidationGroup="searchValidation" ControlToValidate="txtSearchVal" ErrorMessage="Please enter atleast one Search Value."></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="rqveSearchVal" runat="server" CssClass="customCalloutStyle"
                            TargetControlID="rqvSearchVal" Enabled="True" />--%>
                            </div>
                            <div class="Search_button" style="margin-left: 0px;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="searchValidation" CssClass="btn" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1"></asp:Button>
                            </div>
                        </div>
                    </div>
                    <div class="date2">
                        <div class="radio-selection">
                            <asp:RadioButton ID="rbtn2Image" runat="server" AutoPostBack="true" Checked="true" Text="Two Image Template" GroupName="listby" OnCheckedChanged="rbtn2Image_CheckedChanged" meta:resourcekey="rbtn2ImageResource1" />
                            <asp:RadioButton ID="rbtn8Image" runat="server" AutoPostBack="true" Text="Eight Image Template" GroupName="listby" OnCheckedChanged="rbtn2Image_CheckedChanged" meta:resourcekey="rbtn8ImageResource1" />
                        </div>
                    </div>
                    <div class="entry_form">
                        <asp:PlaceHolder ID="phPatientGallery" runat="server">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grid_table">
                                <tbody>
                                    <asp:ListView ID="lvGallery" runat="server" DataKeyNames="PatientGalleryId" DataSourceID="odsGallery" OnItemDataBound="lvGallery_ItemDataBound">
                                        <LayoutTemplate>
                                            <tr>
                                                <%-- <td style="width: 200px">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:LinkButton ID="lnkSortPatient" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="PatientName" Text="Patient Name" meta:resourcekey="lnkSortPatientResource1" />
                                                        </span>
                                                    </div>
                                                </td>--%>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label runat="server" ID="lblTreatment" Text="Treatment" meta:resourcekey="lnkConditionResource1"></asp:Label></span>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">&nbsp;
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label runat="server" ID="lblStatus" Text="Status" meta:resourcekey="lblStatusResource2"></asp:Label></span>
                                                    </div>
                                                </td>
                                                <%--<td style="width: 50px">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label runat="server" ID="lblEdit" Text="Edit" meta:resourcekey="lblEditResource2"></asp:Label></span>
                                                    </div>
                                                </td>      --%>
                                                <%--<td style="width: 50px">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label runat="server" ID="lblDelete" Text="Delete" meta:resourcekey="lblDeleteResource2"></asp:Label></span>
                                                    </div>
                                                </td>--%>
                                            </tr>
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                            <tr>
                                                <td align="right" colspan="5" class="datapager">
                                                    <asp:DataPager ID="lvGalleryDataPager" runat="server" PagedControlID="lvGallery">
                                                        <Fields>
                                                            <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page" NumericButtonCssClass="button-page" meta:resourcekey="NumericPagerFieldResource1" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </td>
                                            </tr>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <%--<td>
                                                    <div class="grenchk dark" id="flex">
                                                        <div class="whitetext">
                                                            <%# Eval("PatientName")%>
                                                        </div>
                                                    </div>

                                                </td>--%>
                                                <td class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("Treatment")%>
                                                    </div>
                                                </td>
                                                <td class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex" style="padding: 5px 50px 5px 5px;">
                                                    <div>
                                                        <div style="float: right">
                                                            <div class="single first">
                                                                <a href="#dvPoup<%# Container.DataItemIndex %>" class="inline">
                                                                    <div class="wrap">
                                                                        <asp:Image ID="imgBefore" runat="server" Width="50" Height="50" />
                                                                        <asp:Image ID="imgAfter" runat="server" Width="50" Height="50" Style="float: left;" />
                                                                        <span class="before" style="width: 0; font-size: small; font-weight: bold; text-align: center; padding-left: 5px; padding-top: 5px;">Before</span>
                                                                        <span class="after" style="width: 0; font-size: small; font-weight: bold; text-align: center; padding-left: 7px; padding-bottom: 5px;">After</span>
                                                                    </div>
                                                                </a>
                                                            </div>
                                                            <div style="display: none">
                                                                <div id="dvPoup<%# Container.DataItemIndex %>">
                                                                    <div class="bxslider">
                                                                        <ul class="slides">
                                                                            <asp:Repeater ID="rptSlider" runat="server">
                                                                                <ItemTemplate>
                                                                                    <li>
                                                                                        <div class="wrap">
                                                                                            <asp:Image ID="imgBefore" runat="server" Width="450" Height="230" ImageUrl='<%# Eval("BeforeImagePath") %>' />
                                                                                            <asp:Image ID="imgAfter" runat="server" Width="450" Height="230" ImageUrl='<%# Eval("AfterImagePath") %>' />
                                                                                            <h3 class="before">Before</h3>
                                                                                            <h3 class="after">After</h3>
                                                                                        </div>
                                                                                    </li>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                            <%--<li>
                                                                                <div class="wrap">
                                                                                    <img width="450" height="230" alt="Plants: image 1 0f 4 thumb" src="PatientFiles/slides/ef3a0fac-8071-473e-b97f-c716457f1895.jpg">
                                                                                    <img width="450" height="230" alt="Plants: image 1 0f 4 thumb" src="PatientFiles/slides/eef39bb4-a9fb-414a-be8d-59ae04b049a3.jpg">
                                                                                    <h3 class="before">Before</h3>
                                                                                    <h3 class="after">After</h3>
                                                                                </div>
                                                                            </li>--%>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex" style="padding: 5px 50px 5px 5px;">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="imgbtnStatus" runat="server" OnClientClick="return DeactivateMessage(this);" CommandName="CUSTOMDELETE" CommandArgument='<%# Eval("PatientGalleryId") %>' OnCommand="Custom_Command"
                                                                ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Content/Images/icon-active.gif" : "Content/Images/icon-inactive.gif" %>'
                                                                AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? this.GetLocalResourceObject("Active"): this.GetLocalResourceObject("InActive") %>' />
                                                        </div>
                                                    </div>
                                                </td>
                                                <%-- <td class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="hypEdit" ToolTip="Edit" runat="server" ImageUrl="Content/images/icon_img10.png"
                                                                meta:resourcekey="hypEditResource1"
                                                                CommandName="CustomEdit" CommandArgument='<%# Eval("PatientGalleryId") %>' OnCommand="Custom_Command" />
                                                        </div>
                                                    </div>
                                                </td>--%>
                                                <%--  <td>
                                                    <div class="grenchk dark drow8" id="flex">
                                                        <div class="winicons">
                                                            <div class="editicon grid-image">
                                                                <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="Content/images/icon_img09.png"
                                                                    OnClientClick="return DeleteMessage(this);"
                                                                    CommandName="CustomDelete" CommandArgument='<%# Eval("PatientGalleryId") %>' OnCommand="Custom_Command" meta:resourcekey="imgbtnDeleteResource2" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <%--<td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label ID="lblProductName" runat="server" Text="Patient Name" meta:resourcekey="lblProductNameResource1"></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>--%>
                                                <td style="width: 250px">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label runat="server" ID="lblTreatment" Text="Treatment" meta:resourcekey="lnkConditionResource1"></asp:Label></span>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label runat="server" ID="lblStatus" Text="Status" meta:resourcekey="lblStatusResource1"></asp:Label></span>
                                                    </div>
                                                </td>
                                                <%--  <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label runat="server" ID="lblEdit" Text="Edit" meta:resourcekey="lblEditResource1"></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>--%>
                                                <%--<td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label runat="server" ID="lblDelete" Text="Delete" meta:resourcekey="lblDeleteResource1"></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="middle" class="equipbg" colspan="5">
                                                    <div class="grenchk dark" id="flex">
                                                        <div class="whitetext" style="width: 100%; text-align: center;">
                                                            <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                    <asp:ListView ID="lvBeforeGallery" runat="server" DataSourceID="odsGallery" OnItemDataBound="lvBeforeGallery_ItemDataBound" EnableViewState="false">
                                        <LayoutTemplate>
                                            <tr>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label runat="server" ID="lblTreatment" Text="Treatment" meta:resourcekey="lnkConditionResource1"></asp:Label></span>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td style="text-align: center;">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label ID="lblBefore" runat="server" Text="Before Template" meta:resourcekey="lblBeforeResource1"></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td style="text-align: center;">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label ID="lblAfter" runat="server" Text="After Template" meta:resourcekey="lblAfterResource1"></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label ID="lblStatus" runat="server" Text="Status" meta:resourcekey="lblStatusResource1"></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                            <tr>
                                                <td align="right" colspan="10" class="datapager">
                                                    <asp:DataPager ID="lvBeforeGalleryDataPager" runat="server" PagedControlID="lvBeforeGallery">
                                                        <Fields>
                                                            <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page" NumericButtonCssClass="button-page" meta:resourcekey="NumericPagerFieldResource1" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </td>
                                            </tr>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                        <div class="whitetext">
                                                            <%# Eval("Treatment")%>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                        <div class="whitetext" style="text-align: center; width: 100%;">
                                                            <div>
                                                                <asp:HyperLink ID="hlnkBefore" NavigateUrl="#" ToolTip="View Before Template" ImageUrl='content/images/before.png' runat="server" meta:resourcekey="hlnkBeforeResource1"></asp:HyperLink>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                        <div class="whitetext" style="text-align: center; width: 100%;">
                                                            <div>
                                                                <asp:HyperLink ID="hlnkAfter" NavigateUrl="#" ToolTip="View Before Template" ImageUrl='content/images/before.png' runat="server" meta:resourcekey="hlnkAfterResource1"></asp:HyperLink>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                        <div class="winicons" style="text-align: center; width: 80%;">
                                                            <div class="editicon grid-image">
                                                                <asp:ImageButton ID="imgbtnStatus" runat="server" OnClientClick="return DeactivateMessage(this);" CommandName="CUSTOMDELETE" CommandArgument='<%# Eval("PatientGalleryId") + "," + Eval("AfterId") %>' OnCommand="Custom_Command"
                                                                    ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Content/Images/icon-active.gif" : "Content/Images/icon-inactive.gif" %>'
                                                                    AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? this.GetLocalResourceObject("Active"): this.GetLocalResourceObject("InActive") %>' />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lnkConditionResource1.Text") %>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td style="width: 25%; text-align: center;">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lblBeforeResource1.Text") %>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td style="width: 25%; text-align: center;">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lblAfterResource1.Text") %>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td style="width: 10%">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lblStatusResource1.Text") %>
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="11">
                                                    <div class="grenchk dark" id="flex">
                                                        <div class="whitetext" style="width: 100%; text-align: center;">
                                                            <%= this.GetLocalResourceObject("lblNoDataFoundResource1.Text") %>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                    <asp:ObjectDataSource ID="odsGallery" runat="server" SelectMethod="GetPatientGalleryDetails"
                                        SelectCountMethod="GetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                        TypeName="_4eOrtho.ListPatientGallery" OnSelecting="odsGallery_Selecting">
                                        <SelectParameters>
                                            <asp:Parameter Name="sortField" Type="String" />
                                            <asp:Parameter Name="sortDirection" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </tbody>
                            </table>
                        </asp:PlaceHolder>
                    </div>
                    <div class="title">
                        <h2>
                            <asp:Label runat="server" ID="lblPublicGalleryList" Text="Public Gallery List" meta:resourcekey="lblPublicGalleryListResource1"></asp:Label>
                        </h2>
                    </div>
                    <div class="entry_form">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grid_table">
                            <tbody>
                                <asp:ListView ID="lvPublicGallery" runat="server" DataKeyNames="GalleryId" DataSourceID="odsPublicGallery" OnItemDataBound="lvPublicGallery_ItemDataBound">
                                    <LayoutTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="left" valign="middle" class="equip">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:LinkButton ID="lnkCondition" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Condition" Text="Condition" meta:resourcekey="lnkConditionResource1" />
                                                        </span>
                                                    </div>
                                                </td>
                                                <td align="left" valign="middle" class="equip">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblStatusResource2"></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                            <tr class="equip-paging">
                                                <td colspan="5" align="right" class="datapager">
                                                    <asp:DataPager ID="lvPublicGalleryDataPager" runat="server" PagedControlID="lvPublicGallery">
                                                        <Fields>
                                                            <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page" NumericButtonCssClass="button-page" meta:resourcekey="NumericPagerFieldResource1" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </td>
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td align="left" valign="middle" class="equipbg">
                                                <div>
                                                    <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                        <div class="whitetext">
                                                            <%# Eval("Condition")%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <%-- <td align="left" valign="middle" class="equipbg">
                                                <div>
                                                    <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                        <div class="whitetext">
                                                            <%# Convert.ToBoolean(Eval("IsHomeDisplay")) ? "Yes" : "No" %>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>--%>
                                            <td align="left" valign="middle" class="equipbg">
                                                <div>
                                                    <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                        <div class="whitetext">
                                                            <asp:ImageButton ID="imgbtnStatus" runat="server" OnClientClick="return DeactivateMessage(this);" CommandName="CustomPublicGalleryActive" CommandArgument='<%# Eval("GalleryId") %>' OnCommand="Custom_Command"
                                                                ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Content/Images/icon-active.gif" : "Content/Images/icon-inactive.gif" %>'
                                                                AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? this.GetLocalResourceObject("Active"): this.GetLocalResourceObject("InActive") %>' />
                                                            <%--<asp:Image ID="imgStatus" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Content/Images/icon-active.gif" : "Content/Images/icon-inactive.gif" %>'
                                                        AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "In-Active" %>' meta:resourcekey="imgStatusResource1" />--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="left" valign="middle" class="equip">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:LinkButton ID="lnkCondition" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Condition" Text="Condition" meta:resourcekey="lnkConditionResource1" />
                                                        </span>
                                                    </div>
                                                </td>
                                                <td align="left" valign="middle" class="equip">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblStatusResource2"></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="middle" class="equipbg" colspan="5">
                                                    <asp:Label ID="lblNoDataFound" runat="server" Text="No Data Found" meta:resourcekey="lblNoDataFoundResource1"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                                <asp:ObjectDataSource ID="odsPublicGallery" runat="server" SelectMethod="GetGalleryDetails"
                                    SelectCountMethod="PublicGalleryGetTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                    TypeName="_4eOrtho.ListPatientGallery" OnSelecting="odsPublicGallery_Selecting">
                                    <SelectParameters>
                                        <asp:Parameter Name="sortField" Type="String" />
                                        <asp:Parameter Name="sortDirection" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </tbody>
                        </table>
                    </div>
                </div>
                <a href="#" id="appReuest" class="imgTemplate" style="display: none;"></a>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1"
            DisplayAfter="10">
            <ProgressTemplate>
                <div class="processbar1">
                    <img src="../Content/images/loading.gif" alt="loading" style="top: 50%; left: 50%; position: absolute;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
    </div>
    <script type="text/javascript">
        function pageLoad() {
            $("#" + "<%= ddlGallery.ClientID %>").selectbox();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(beforeafter);
        }
        jQuery(document).ready(function ($) {
            jQuery(".imgTemplate").colorbox({
                iframe: true,
                width: "900px",
                height: "600px",
                overlayClose: false,
                escKey: true,
            });
            beforeafter();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {
            jQuery(".imgTemplate").colorbox({
                iframe: true,
                width: "900px",
                height: "600px",
                overlayClose: false,
                escKey: true,
            });
        });
        function beforeafter() {
            $(".inline").colorbox({
                inline: true,
                width: 460,
                onComplete: function () {
                    $(window).trigger('resize');
                    $('.bxslider').flexslider({
                        animation: "slide",
                        slideshow: false
                    });
                }
            });
            $('.bxslider').flexslider({
                animation: "slide",
                slideshow: false
            });
        }
    </script>
</asp:Content>
