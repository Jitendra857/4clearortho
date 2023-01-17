<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="PatientBeforeAfterPictures.aspx.cs" Inherits="_4eOrtho.PatientBeforeAfterPictures" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="Scripts/jquery.smooth-scroll.min.js"></script>
    <script type="text/javascript" src="Scripts/lightbox.js"></script>
    <link rel="stylesheet" href="Styles/screen.css" type="text/css" media="screen" />
    <link href="Styles/lightbox.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript">
        $(document).ready(function ($) {
            $('a').smoothScroll({
                speed: 1000,
                easing: 'easeInOutCubic'
            });

            $.ajax({
                type: "POST",
                url: "PatientBeforeAfterPictures.aspx/GetPatientImagePaths",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var counter = 0;
                    $(msg.d).each(function (data, value) {
                        var i = 1;
                        var count = value.Path.length;
                        var Counter = 0;
                        var dvElement = $("<div id=dv" + value.Treatment + " style='float:left'><h4>" + value.Treatment + "</h4></div>");
                        var dvHide = $("<div style='display:none'></div>")
                        var dvPopup = $("<div id=dvPoup" + counter + " ></div>");
                        var innerHtml, popupInnerHtml = "<div class='bxslider'><ul class='slides'>";

                        if (count > 0)
                            $('#lblNodatafound').remove();

                        for (Counter = 0; Counter < value.Path.length; Counter++) {
                            if (i == 1) {
                                innerHtml = "<div class='single first'><a class='inline' href='#dvPoup" + counter + "'><div class='wrap'><img src='PatientFiles/thumbs/" + value.Path[Counter] + "' alt='Plants: image 1 0f 4 thumb'>";
                            }
                            else if (i == 2) {
                                innerHtml += "<img src='PatientFiles/thumbs/" + value.Path[Counter] + "' alt='Plants: image 1 0f 4 thumb'><h3 class='before'>Before</h3><h3 class='after'>After</h3></div></a></div>";
                            }
                            if (i % 2 != 0) {
                                popupInnerHtml += "<li><div class='wrap'><img src='PatientFiles/slides/" + value.Path[Counter] + "' alt='Plants: image 1 0f 4 thumb' width='450px' height='230' >";
                            }
                            else if (i % 2 == 0) {
                                popupInnerHtml += "<img src='PatientFiles/slides/" + value.Path[Counter] + "' alt='Plants: image 1 0f 4 thumb' width='450px' height='230'><h3 class='before'>Before</h3><h3 class='after'>After</h3></div></li>";
                            }
                            i++;
                        }
                        popupInnerHtml += "</ul></div>";
                        dvElement.append(innerHtml);
                        dvPopup.append(popupInnerHtml);
                        dvHide.append(dvPopup);
                        dvElement.append(dvHide);
                        $(".imageRow").append(dvElement);
                        counter++;
                    });
                    $(".inline").colorbox({
                        inline: true,
                        width: 450,
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
            });

            $('.showOlderChanges').on('click', function (e) {
                $('.changelog .old').slideDown('slow');
                $(this).fadeOut();
                e.preventDefault();
            })
        });

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-2196019-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
    <script type="text/javascript">
        function pageLoad() {
            jQuery(".imgTemplate").colorbox({
                iframe: true,
                width: "900px",
                height: "600px",
                overlayClose: false,
                escKey: true,
            });
        }

        function ShowImgTemplate(id, doctorname, treatment) {
            jQuery('.imgTemplate').attr("href", "PatientBeforeAfterImgPopup.aspx?galleryId=" + id + "&dname=" + doctorname + "&treatment=" + treatment);
            jQuery('.imgTemplate').click();
            return false;
        }
    </script>
    <style type="text/css">
        .textcenter {
            text-align: center;
        }

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
            width: 202px;
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
            top: 88%;
            left: 0;
            z-index: 2;
            text-align: center;
        }

        .two_img_template {
            width: 50%;
            float: left;
        }

            .two_img_template span {
                float: left;
                width: 100%;
                color: #000;
                font-size: smaller;
            }

        .two_tem_mian {
            border: 1px solid #ddd;
            border-radius: 6px;
            margin: 10px 10px 0 0;
        }

        .ui-state-active a, .ui-state-active a:link, .ui-state-active a:visited {
            color: #4CA8DE !important;
        }
    </style>
    <asp:UpdatePanel ID="upGallery" runat="server">
        <ContentTemplate>
            <div class="left_title">
                <h2>
                    <asp:Label ID="lblHeader" runat="server" Text="Before After Pictures" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
            </div>
            <div class="entry_form">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grid_table">
                    <tbody>
                        <asp:ListView ID="lvBeforeGallery" runat="server" DataSourceID="odsBeforeGallery" OnPreRender="lvBeforeGallery_PreRender" OnItemDataBound="lvImageTemplate_ItemDataBound">
                            <LayoutTemplate>
                                <tr>
                                    <td style="width: 15%">
                                        <div class="topadd_f flex">
                                            <span class="one">
                                                <asp:Label ID="lblHDoctor" runat="server" Text="Doctor Name" meta:resourcekey="lblHDoctorResource2"></asp:Label>
                                            </span>
                                        </div>
                                    </td>
                                    <td style="width: 10%">
                                        <div class="topadd_f flex">
                                            <span class="one">
                                                <asp:Label ID="lblHCreatedDate" runat="server" Text="Created Date" meta:resourcekey="lblHCreatedDateResource2"></asp:Label>
                                            </span>
                                        </div>
                                    </td>
                                    <td style="width: 30%" colspan="2">
                                        <div class="topadd_f flex">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <%--<tr>
                                                    <td align="center" colspan="2">
                                                        <span class="one" style="width: 100%; margin: 10px 10px 0 5px; text-align: center;">
                                                            <asp:Label ID="lblImageTemplate" runat="server" Text="Image Template" meta:resourcekey="lblImageTemplateResource2"></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <span class="one">
                                                            <asp:Label ID="lblBeforeTreatment" runat="server" Text="Before Treatment" meta:resourcekey="lblBeforeTreatmentResource2"></asp:Label>
                                                        </span>
                                                    </td>
                                                    <td>
                                                        <span class="one">
                                                            <asp:Label ID="lblAfterTreatment" runat="server" Text="After Treatment" meta:resourcekey="lblAfterTreatmentResource2"></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                    <td style="width: 10%" class="textcenter">
                                        <div class="topadd_f flex">
                                            <span class="one">
                                                <asp:Label ID="lbl_DoctorReview" runat="server" Text="Review" meta:resourcekey="lblHDoctorReviewResource2"></asp:Label>
                                            </span>
                                        </div>
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                <tr>
                                    <td align="right" colspan="5" class="datapager">
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
                                            <div class="whitetext" style="width: 100%;">
                                                <%# Eval("DName") %>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="width: 15%">
                                        <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                            <div class="whitetext" style="width: 100%">
                                                <%# Convert.ToDateTime(Eval("CreatedDate")).ToString("MM/dd/yyyy") %>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                            <div class="whitetext" style="width: 100%; text-align: center;">
                                                <div>
                                                    <asp:HyperLink ID="hlnkBefore" NavigateUrl="#" ToolTip="View Before Template" meta:resourcekey="hlnkBeforeResource1" ImageUrl='content/images/before.png' runat="server"></asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                            <div class="whitetext" style="width: 100%; text-align: center;">
                                                <div>
                                                    <asp:HyperLink ID="hlnkAfter" NavigateUrl="#" ToolTip="View After Template" meta:resourcekey="hlnkAfterResource1" ImageUrl='content/images/after.png' runat="server"></asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="width: 10%" class="textcenter">
                                        <div class='<%# Container.DataItemIndex % 2 == 0 ? "grenchk dark" : "grenchk light" %>' id="flex">
                                            <div class="whitetext" style="width: 100%">
                                                <asp:HyperLink runat="server" ID="Hpyl_review" Target="_blank" NavigateUrl='<%# "DoctorReview.aspx?EmailId=" + _4eOrtho.Utility.CommonLogic.EncryptStringAES(Convert.ToString(Eval("DoctorEmail")))%>' ImageUrl='content/images/review.png' meta:resourcekey="hlnkReviewResource1"></asp:HyperLink>
                                                <asp:HyperLink runat="server" ID="Hpyl_Rating" Target="_blank" NavigateUrl='<%# "Rating.aspx?EmailId=" + _4eOrtho.Utility.CommonLogic.EncryptStringAES(Convert.ToString(Eval("DoctorEmail")))%>' ImageUrl='content/images/Rating.png' meta:resourcekey="hlnkRatingwResource1" Style="padding-left: 7px;"></asp:HyperLink>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyItemTemplate>
                                <tr>
                                    <td style="width: 15%">
                                        <div class="topadd_f flex">
                                            <span class="one">
                                                <asp:Label ID="lblHDoctor" runat="server" Text="Doctor Name" meta:resourcekey="lblHDoctorResource2"></asp:Label>
                                            </span>
                                        </div>
                                    </td>
                                    <td style="width: 10%">
                                        <div class="topadd_f flex">
                                            <span class="one">
                                                <asp:Label ID="lblHCreatedDate" runat="server" Text="Created Date" meta:resourcekey="lblHCreatedDateResource2"></asp:Label>
                                            </span>
                                        </div>
                                    </td>
                                    <td style="width: 30%" colspan="2">
                                        <div class="topadd_f flex">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <span class="one">
                                                            <asp:Label ID="lblBeforeTreatment" runat="server" Text="Before Treatment" meta:resourcekey="lblBeforeTreatmentResource2"></asp:Label>
                                                        </span>
                                                    </td>
                                                    <td>
                                                        <span class="one">
                                                            <asp:Label ID="lblAfterTreatment" runat="server" Text="After Treatment" meta:resourcekey="lblAfterTreatmentResource2"></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                    <td style="width: 10%" class="textcenter">
                                        <div class="topadd_f flex">
                                            <span class="one">
                                                <asp:Label ID="lbl_DoctorReview" runat="server" Text="Review" meta:resourcekey="lblHDoctorReviewResource2"></asp:Label>
                                            </span>
                                        </div>
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                <tr>
                                    <td colspan="5">
                                        <div style="float: left; text-align: center;">
                                            <span id="lblNodatafound"><%= this.GetLocalResourceObject("NoDataFound") %> </span>
                                        </div>                                        
                                    </td>
                                </tr>
                            </EmptyItemTemplate>
                        </asp:ListView>
                        <asp:ObjectDataSource ID="odsBeforeGallery" runat="server" SelectMethod="GetBeforeGalleryDetails"
                            SelectCountMethod="GetBeforeTotalRowCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                            TypeName="_4eOrtho.PatientBeforeAfterPictures" OnSelecting="odsBeforeGallery_Selecting">
                            <SelectParameters>
                                <asp:Parameter Name="sortField" Type="String" />
                                <asp:Parameter Name="sortDirection" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="left_title">
        <h2>
            <%--<asp:Label ID="lblPatientGallery" runat="server" Text="Patient Gallery" meta:resourcekey="lblPatientGallery"></asp:Label>--%>
            <%--<asp:Label ID="lblTwoImage" runat="server" Text="Two Image Template" meta:resourcekey="lblTwoImageResource1"></asp:Label>--%>
            <asp:Label ID="lblTwoImage" runat="server" Text="Two Image Gallery" meta:resourcekey="lblTwoImageGalleryResource1"></asp:Label>
        </h2>
    </div>
    <%--<div class="date2">
        <div class="radio-selection">
            <asp:RadioButton ID="rbtn2Image" runat="server" CssClass="TwoImgView" Checked="true" Text="Two Image Template" GroupName="listby" />
            <asp:RadioButton ID="rbtn8Image" runat="server" CssClass="EightImgView" Text="Eight Image Template" GroupName="listby" />
        </div>
    </div>--%>

    <div class="date2">
        <div id="tabs">
            <%--<ul style="background: #4CA8DE !important;">
                <li><a href="#tabs-1">
                    <asp:Label ID="lblTwoImage" runat="server" Text="Two Image Template" meta:resourcekey="lblTwoImageResource1"></asp:Label>
                </a></li>
                <li><a href="#tabs-2">
                    <asp:Label ID="lblEightImage" runat="server" Text="Eight Image Template" meta:resourcekey="lblEightImageResource1"></asp:Label></a></li>
            </ul>--%>
            <div id="tabs-1">
                <div class="imageRow" id="dv2image" runat="server">
                    <div style="float: left; text-align: center;">
                        <span id="lblNodatafound"><%= this.GetLocalResourceObject("NoDataFound") %> </span>
                    </div>
                </div>
            </div>
            <%--<div id="tabs-2" style="overflow: hidden;">
                <div id="dv8Image" runat="server">
                    <asp:Repeater ID="rep8Image" runat="server" OnItemDataBound="rep8Image_ItemDataBound">
                        <ItemTemplate>
                            <div style="float: left; text-align: center;" id="dvTreat" class="two_tem_mian">
                                <h4>
                                    <asp:Label ID="lblTitle" runat="server"></asp:Label></h4>
                                <div class="single first">
                                    <div class="wrap">
                                        <div class="two_img_template">
                                            <asp:HyperLink ID="hlnkBefore" NavigateUrl="#" ToolTip="View Before Template" meta:resourcekey="hlnkBeforeResource1" ImageUrl='content/images/before.png' runat="server"></asp:HyperLink>
                                            <asp:Label ID="lblBefore" runat="server" Text="Before" meta:resourcekey="lblBeforeResource1"></asp:Label>
                                        </div>
                                        <div class="two_img_template">
                                            <asp:HyperLink ID="hlnkAfter" NavigateUrl="#" ToolTip="View After Template" meta:resourcekey="hlnkAfterResource1" ImageUrl='content/images/after.png' runat="server"></asp:HyperLink>
                                            <asp:Label ID="lblAfter" runat="server" Text="After" meta:resourcekey="lblAfterResource1"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div style="float: left; text-align: center;" id="dvRepEmpty" runat="server">
                        <span id="Span2"><%= this.GetLocalResourceObject("NoDataFound") %> </span>
                    </div>
                </div>
            </div>--%>
        </div>
    </div>
    <a href="AppointmentRequest.aspx" id="appReuest" class="imgTemplate"
        style="display: none;"></a>

    <script type="text/javascript" src="Scripts/jquery.smooth-scroll.min.js"></script>
    <script src="Scripts/jquery.flexslider.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $('.bxslider').flexslider({
                animation: "slide",
                slideshow: false
            });
        });
        $(document).ready(function () {
            $('#tabs').tabs();
            setTimeout(function () {
                startflexslider();
            }, 5000);
            function startflexslider() {
                $('.bxslider').flexslider({
                    animation: "slide",
                    slideshow: false
                });
            }
            $('.TwoImgView').click(function () {
                $('[id$=dv8Image]').hide();
                $('[id$=dv2image]').show();
            });
            $('.EightImgView').click(function () {
                $('[id$=dv2image]').hide();
                $('[id$=dv8Image]').show();
            });
        });
    </script>
</asp:Content>
