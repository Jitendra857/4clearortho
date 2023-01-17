<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="ListPictureTemplate.aspx.cs" Inherits="_4eOrtho.ListPictureTemplate" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
            /*height: 130px;
            width: 187px;*/
            float: left;
            /*margin-right: 15px;*/
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
            /*height: 130px;*/
            /*width: 187px;*/
            float: left;
            /*margin-right: 15px;*/
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
            /*border: 1px solid #d4d4d4;*/
            /*padding: 5px;*/
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

        .grid-image {
            padding-left: 15px !important;
        }

        .tabHed-active {
            text-decoration: none;
            padding: 10px;
            font-weight: bold;
            float: left;
            color: white;
            background-color: rgb(76, 168, 222);
            border: 1px solid rgb(204, 204, 204);
        }

        .tabHed-deactive {
            text-decoration: none;
            padding: 10px;
            font-weight: bold;
            float: left;
            background-color: white;
            color: rgb(76, 168, 222);
            border: 1px solid rgb(204, 204, 204);
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            beginRequest();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
        }

        function beginRequest() {
            InitMethod();
        }
        function endRequest() {
            InitMethod();
        }
        function InitMethod() {
            $(".imgTemplate").colorbox({
                iframe: true,
                width: "900px",
                height: "600px",
                overlayClose: false,
                escKey: true,
            });
            var ddlSearchType = $('[id$=ddlSearchType]');
            if (ddlSearchType.length > 0) {
                ddlSearchType.selectbox();
                ddlSearchType[0].style.display = "block";
                ddlSearchType[0].style.position = "absolute";
                ddlSearchType[0].style.zIndex = "-1";
                ddlSearchType[0].style.width = "initial";
            }
        }
        function ShowImgTemplate(id, doctorname, pname, treatment) {
            $('.imgTemplate').attr("href", "PatientBeforeAfterImgPopup.aspx?galleryId=" + id + "&dname=" + doctorname + "&pname=" + pname + "&treatment=" + treatment);
            $('.imgTemplate')[0].click();
            return false;
        }
        function DeleteMessage(obj) {
            if (confirm("<%=this.GetLocalResourceObject("DeleteMessage").ToString()%>"))
                return true;
            else
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
                            <asp:Button ID="btnAddGallery" runat="server" Text="Add Gallery" meta:resourcekey="btnaddgalleryResource1" PostBackUrl="~/AddEditPatientGallery.aspx" />
                        </div>
                        <h2>
                            <asp:Label runat="server" ID="lblHeader" Text="Gallery List" meta:resourcekey="lblHeaderResource1"></asp:Label>
                        </h2>
                    </div>
                    <div class="date2">
                        <%--<div class="date_cont" style="width: 46%;">
                            <div class="date_cont_right">
                                <div class="Sort_by">
                                    <div class="product_dropdown">
                                        <asp:DropDownList ID="ddlSearchType" runat="server" CssClass="low_high_search" meta:resourcekey="ddlSearchTypeResource1">
                                            <asp:ListItem Value="0" Text="Select Search Type" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                            <asp:ListItem Value="PatientName" Text="Patient Name" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rqvddlsearchtype" runat="server" Display="None" ValidationGroup="searchValidation"
                                            ControlToValidate="ddlSearchType" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please select search type." meta:resourcekey="rqvddlsearchtypeResource1"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="customCalloutStyle"
                                            TargetControlID="rqvddlsearchtype" Enabled="True" />
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <div class="parsonal_textfild" style="padding: 0; float: right;">
                            <div class="date_cont_right">
                                <asp:TextBox ID="txtSearchVal" runat="server" MaxLength="50" meta:resourcekey="txtSearchValResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqvSearchVal" runat="server" Display="None" ValidationGroup="searchValidation"
                                    ControlToValidate="txtSearchVal" ErrorMessage="Please enter atleast one Search Value." meta:resourcekey="rqvSearchValResource1"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="rqveSearchVal" runat="server" CssClass="customCalloutStyle"
                                    TargetControlID="rqvSearchVal" Enabled="True" PopupPosition="Left" />
                            </div>
                            <div class="Search_button" style="margin-left: 0px;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="searchValidation" CssClass="btn" meta:resourcekey="btnSearchResource1"></asp:Button>
                            </div>
                            <div style="float: right; margin-left: 20px; margin-top: -2px">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/Content/images/clear.png" OnClick="imgbtnClear_Click" />
                            </div>
                        </div>
                    </div>
                    <div style="margin: 30px 0 0; float: left;">
                        <asp:LinkButton ID="lbtnEightImage" CssClass="tabHed-active" Enabled="false" runat="server" Text="Eight Image" OnClick="lbtnEightImage_Click" meta:resourcekey="lbtnEightImageResource1"></asp:LinkButton>
                        <asp:LinkButton ID="lbtntwoImage" CssClass="tabHed-deactive" runat="server" Text="Two Image" OnClick="lbtntwoImage_Click" meta:resourcekey="lbtntwoImageResource1"></asp:LinkButton>
                    </div>
                    <div class="entry_form" style="margin: 0;">
                        <div id="tabs-1">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grid_table">
                                <tbody>
                                    <asp:ListView ID="lvBeforeGallery" runat="server" DataSourceID="odsBeforeGallery" OnPreRender="lvBeforeGallery_PreRender" OnItemDataBound="lvBeforeGallery_ItemDataBound">
                                        <LayoutTemplate>
                                            <tr>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:LinkButton ID="lnkSortPatient" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="PatientName" Text="Patient Name" meta:resourcekey="lnkSortPatientResource1" />
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:LinkButton ID="lnkCreatedDate" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="CreatedDate" Text="Create Date" meta:resourcekey="lnkCreatedDateResource1" />
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
                                                <td  style="text-align: center;">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label ID="lblStatus" runat="server" Text="Status" meta:resourcekey="lblStatusResource1"></asp:Label>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td style="text-align: center;">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%--<asp:Label ID="lblEdit" runat="server" Text="Edit" meta:resourcekey="lblEditResource1"></asp:Label>--%>
                                                            <asp:Literal ID="ltrAction" runat="server" meta:resourcekey="ltrActionResource1"></asp:Literal>
                                                        </span>
                                                    </div>
                                                </td>
                                                <%--<td style="width: 50px">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblDelte" runat="server" Text="Delete" meta:resourcekey="lblDelteResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>--%>
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
                                                            <%# Eval("PName")%>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                        <div class="whitetext">
                                                            <%# Convert.ToDateTime(Eval("CreatedDate")).ToString("MM/dd/yyyy") %>
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
                                                <td class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="imgbtnStatus" runat="server" OnClientClick="return DeactivateMessage(this);" CommandName="CUSTOMDELETE" CommandArgument='<%# Eval("PatientGalleryId") + "," + Eval("AfterId") %>' OnCommand="Custom_Command"
                                                                ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Content/Images/icon-active.gif" : "Content/Images/icon-inactive.gif" %>'
                                                                AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? this.GetLocalResourceObject("Active"): this.GetLocalResourceObject("InActive") %>' />
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                        <div style="padding-right: 15px;">
                                                            <div class="editicon grid-image">
                                                                <asp:ImageButton ID="hypEdit" ToolTip="Edit" runat="server" ImageUrl="Content/images/view.png" meta:resourcekey="hypEditResource1" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <%--<td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="Content/images/icon_img09.png"
                                                                OnClientClick="return DeleteMessage(this);" ToolTip="Delete"
                                                                CommandName="CustomDelete" CommandArgument='<%# Eval("PatientGalleryId") + "," + Eval("AfterId") %>' OnCommand="Custom_Command" meta:resourcekey="imgbtnDeleteResource1" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lnkSortPatientResource1.Text") %>                                                        
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lnkCreatedDateResource1.Text") %>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td style="text-align: center;">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lblBeforeResource1.Text") %>                                                        
                                                        </span>
                                                    </div>
                                                </td>
                                                <td style="text-align: center;">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lblAfterResource1.Text") %>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td style="text-align: center;">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lblStatusResource1.Text") %>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td style="text-align: center;">
                                                    <div class="topadd_f flex">
                                                        <span class="one">&nbsp;
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="11">
                                                    <div class="grenchk dark" id="flex">
                                                        <div class="whitetext" style="width: 100%; text-align: center;">
                                                            <%= this.GetLocalResourceObject("NoDataFound") %>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                    <asp:ObjectDataSource ID="odsBeforeGallery" runat="server" SelectMethod="GetBeforeGalleryDetails"
                                        SelectCountMethod="GetBeforeTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                        TypeName="_4eOrtho.ListPictureTemplate" OnSelecting="odsBeforeGallery_Selecting">
                                        <SelectParameters>
                                            <asp:Parameter Name="sortField" Type="String" />
                                            <asp:Parameter Name="sortDirection" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </tbody>
                            </table>
                        </div>
                        <div id="tabs-2">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grid_table">
                                <tbody>
                                    <asp:ListView ID="lvTwoImageGallery" Visible="false" runat="server" DataSourceID="odsTwoImageGallery" OnItemDataBound="lvTwoImageGallery_ItemDataBound">
                                        <LayoutTemplate>
                                            <tr>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:LinkButton ID="lnkSortPatient" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="PatientName" Text="Patient Name" meta:resourcekey="lnkSortPatientResource1" />
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:LinkButton ID="lnkTreatment" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="Treatment" Text="Treatment" />
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:LinkButton ID="lnkCreatedDate" runat="server" CommandName="CustomSort" OnCommand="Custom_Command"
                                                                CommandArgument="CreatedDate" Text="Create Date" meta:resourcekey="lnkCreatedDateResource1" />
                                                        </span>
                                                    </div>
                                                </td>
                                                <td style="text-align: center;">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <asp:Label ID="lblImageTemplate" runat="server" Text="Image Template"></asp:Label>
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
                                                <%--<td>
                                                <div class="topadd_f flex">
                                                    <span class="one">                                                        
                                                        <asp:Literal ID="ltrAction" runat="server" meta:resourcekey="ltrActionResource1"></asp:Literal>
                                                    </span>
                                                </div>
                                            </td>--%>
                                                <%--<td style="width: 50px">
                                                <div class="topadd_f flex">
                                                    <span class="one">
                                                        <asp:Label ID="lblDelte" runat="server" Text="Delete" meta:resourcekey="lblDelteResource1"></asp:Label>
                                                    </span>
                                                </div>
                                            </td>--%>
                                            </tr>
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                            <tr>
                                                <td align="right" colspan="10" class="datapager">
                                                    <asp:DataPager ID="lvBeforeGalleryDataPager" runat="server" PagedControlID="lvTwoImageGallery">
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
                                                            <%# Eval("PatientName")%>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="whitetext">
                                                        <%# Eval("Treatment")%>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                        <div class="whitetext">
                                                            <%# Convert.ToDateTime(Eval("CreatedDate")).ToString("MM/dd/yyyy") %>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                        <div class="whitetext" style="text-align: center; width: 100%;">
                                                            <div>
                                                                <a href="#dvPoup<%# Container.DataItemIndex %>" class="inline" style="text-decoration: none;">
                                                                    <img src='content/images/before.png' />
                                                                    <img src='content/images/after.png' />
                                                                </a>
                                                            </div>
                                                        </div>
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
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="imgbtnStatus" runat="server" OnClientClick="return DeactivateMessage(this);" CommandName="CUSTOMDELETE" CommandArgument='<%# Eval("PatientGalleryId") + "," + Eval("AfterId") %>' OnCommand="Custom_Command"
                                                                ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) ? "Content/Images/icon-active.gif" : "Content/Images/icon-inactive.gif" %>'
                                                                AlternateText='<%# Convert.ToBoolean(Eval("IsActive")) ? this.GetLocalResourceObject("Active"): this.GetLocalResourceObject("InActive") %>' />
                                                        </div>
                                                    </div>
                                                </td>
                                                <%-- <td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div style="padding-right: 15px;">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="hypEdit" ToolTip="Edit" runat="server" ImageUrl="Content/images/view.png" meta:resourcekey="hypEditResource1" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>--%>
                                                <%--<td>
                                                <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                                    <div class="winicons">
                                                        <div class="editicon grid-image">
                                                            <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="Content/images/icon_img09.png"
                                                                OnClientClick="return DeleteMessage(this);" ToolTip="Delete"
                                                                CommandName="CustomDelete" CommandArgument='<%# Eval("PatientGalleryId") + "," + Eval("AfterId") %>' OnCommand="Custom_Command" meta:resourcekey="imgbtnDeleteResource1" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lnkSortPatientResource1.Text") %>                                                        
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lnkSortPatientResource1.Text") %>                                                        
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lnkCreatedDateResource1.Text") %>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td style="text-align: center;">
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lblAfterResource1.Text") %>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="topadd_f flex">
                                                        <span class="one">
                                                            <%= this.GetLocalResourceObject("lblStatusResource1.Text") %>
                                                        </span>
                                                    </div>
                                                </td>
                                                <%--<td>
                                                <div class="topadd_f flex">
                                                    <span class="one">&nbsp;
                                                    </span>
                                                </div>
                                            </td>--%>
                                            </tr>
                                            <tr>
                                                <td colspan="11">
                                                    <div class="grenchk dark" id="flex">
                                                        <div class="whitetext" style="width: 100%; text-align: center;">
                                                            <%= this.GetLocalResourceObject("NoDataFound") %>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                    <asp:ObjectDataSource ID="odsTwoImageGallery" runat="server" SelectMethod="GetTwoImageGallery"
                                        SelectCountMethod="GetTwoImageGalleryTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                                        TypeName="_4eOrtho.ListPictureTemplate" OnSelecting="odsTwoImageGallery_Selecting">
                                        <SelectParameters>
                                            <asp:Parameter Name="sortField" Type="String" />
                                            <asp:Parameter Name="sortDirection" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="entry_form title">
                        <h2>
                            <asp:Label runat="server" ID="lblPublicGalleryList" Text="Public Gallery List" meta:resourcekey="lblPublicGalleryListResource1"></asp:Label>
                        </h2>
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
                                                            <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblStatusResource1"></asp:Label>
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
                                                        <div class="whitetext" style="padding-left: 15px;">
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
                                                            <asp:Label runat="server" ID="lblIsActive" Text="Is Active" meta:resourcekey="lblStatusResource1"></asp:Label>
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
                                    TypeName="_4eOrtho.ListPictureTemplate" OnSelecting="odsPublicGallery_Selecting">
                                    <SelectParameters>
                                        <asp:Parameter Name="sortField" Type="String" />
                                        <asp:Parameter Name="sortDirection" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </tbody>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <a href="#" id="appReuest" class="imgTemplate" style="display: none;"></a>
    </div>
    <script type="text/javascript">
        $(".imgTemplate").colorbox({
            iframe: true,
            width: "900px",
            height: "600px",
            overlayClose: false,
            escKey: true,
        });
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

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {
            jQuery(".imgTemplate").colorbox({
                iframe: true,
                width: "900px",
                height: "600px",
                overlayClose: false,
                escKey: true,
            });
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
        });
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
    </script>
</asp:Content>
