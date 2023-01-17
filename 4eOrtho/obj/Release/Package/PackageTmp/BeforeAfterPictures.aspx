<%@ Page Title="" Language="C#" MasterPageFile="~/Ortho.Master" AutoEventWireup="true" CodeBehind="BeforeAfterPictures.aspx.cs" Inherits="_4eOrtho.BeforeAfterPictures" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
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
    </style>
    <script src="Scripts/jquery-ui-1.8.18.custom.min.js"></script>
    <link rel="stylesheet" href="Styles/screen.css" type="text/css" media="screen" />
    <script type="text/javascript">
        $(document).ready(function ($) {
            $('a').smoothScroll({
                speed: 1000,
                easing: 'easeInOutCubic'
            });

            $.ajax({
                type: "POST",
                url: "BeforeAfterPictures.aspx/GetCategoryImagePaths",
                data: "{doctoremailid : '<%= hdnDoctorEmailId.Value %>'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var counter = 0;
                    $(msg.d).each(function (data, value) {                        
                        var i = 1;
                        var count = value.Path.length;
                        var dvElement = $("<div id=dv" + value.Condition + " style='float:left'><h4>" + value.Condition + "</h4></div>");
                        var dvHide = $("<div style='display:none'></div>")
                        var dvPopup = $("<div id=dvPoup" + counter + " ></div>");
                        var innerHtml, popupInnerHtml = "<div class='bxslider'><ul class='slides'>";
                        $.each(value.Path, function (key, val) {                            
                            if (i == 1) {
                                innerHtml = "<div class='single first'><a class='inline' href='#dvPoup" + counter + "'><div class='wrap'><img src='Photograph/thumbs/" + val + "' alt='Plants: image 1 0f 4 thumb'>";
                            }
                            else if (i == 2) {
                                innerHtml += "<img src='Photograph/thumbs/" + val + "' alt='Plants: image 1 0f 4 thumb'><h3 class='before'>Before</h3><h3 class='after'>After</h3></div></a></div>";
                            }
                            if (i % 2 != 0) {
                                popupInnerHtml += "<li><div class='wrap'><img src='Photograph/" + val + "' alt='Plants: image 1 0f 4 thumb' width='450px' height='230' >";
                            }
                            else if (i % 2 == 0) {
                                popupInnerHtml += "<img src='Photograph/" + val + "' alt='Plants: image 1 0f 4 thumb' width='450px' height='230'><h3 class='before'>Before</h3><h3 class='after'>After</h3></div></li>";
                            }
                            i++;
                        });
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
    <div class="left_title">
        <h2>
            <asp:Label ID="lblHeader" runat="server" Text="Before After Pictures" meta:resourcekey="lblHeaderResource1"></asp:Label></h2>
        <asp:HiddenField ID="hdnDoctorEmailId" runat="server" />
    </div>
    <div class="imageRow">
    </div>
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
            setTimeout(function () {
                startflexslider();
            }, 5000);
            function startflexslider() {                
                $('.bxslider').flexslider({
                    animation: "slide",
                    slideshow: false
                });
            }
        });
    </script>
</asp:Content>
