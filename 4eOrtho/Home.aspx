<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="_4eOrtho.Home" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>4ClearOrtho - Home</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!--styles -->
    <link runat="server" id="lnk_style" rel="stylesheet" />
    <link href="Styles/flexslider.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="Styles/jquery.selectbox2.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="Content/Images/logo.ico" />
</head>

<body>
    <!-- Header -->
    <form runat="server" id="form1">
        <div class="header home">
            <div class="wrapper">
                <div class="logo">
                    <a href="Home.aspx">
                        <img src="Content/images/logo.png" alt="logo"></a>
                </div>
                <div class="Banner_top_login_section">
                    <div class="Banner_login_P_content"><a href="PatientLogin.aspx"><%= this.GetLocalResourceObject("PatientLogin") %></a></div>
                    <div class="Banner_login_D_content"><a href="DoctorLogin.aspx"><%= this.GetLocalResourceObject("DoctorLogin") %></a></div>
                    <div class="Blog_laguage">
                        <asp:DropDownList ID="ddlLanguage" name="Language_Dropdown" Font-Names="Language" runat="server" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddlLanguageResource1">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <!-- Header -->
        <!-- FlexSlider -->
        <div class="flexslider">
            <span class="bottomshadow"></span>
            <ul class="slides">
                <li>
                    <div class="Banner one">
                        <div class="wrapper">
                            <div class="Banner_left">
                                <h1>HIGH QUALITY PATIENT CARE</h1>
                                <p>Modern Approach to Straightening teeth</p>
                            </div>
                            <div class="Banner_right">
                                <img src="Content/images/banner1.png" alt="image">
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="Banner two">
                        <div class="wrapper">
                            <div class="Banner_left">
                                <h1>HIGH QUALITY PATIENT CARE</h1>
                                <p>Modern Approach to Straightening teeth</p>
                            </div>
                            <div class="Banner_right">
                                <img src="Content/images/banner2.png" alt="image">
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="Banner one">
                        <div class="wrapper">
                            <div class="Banner_left">
                                <h1>HIGH QUALITY PATIENT CARE</h1>
                                <p>Modern Approach to Straightening teeth</p>
                            </div>
                            <div class="Banner_right">
                                <img src="Content/images/banner4.png" alt="image">
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="Banner two">
                        <div class="wrapper">
                            <div class="Banner_left">
                                <h1>HIGH QUALITY PATIENT CARE</h1>
                                <p>Modern Approach to Straightening teeth</p>
                            </div>

                            <div class="Banner_right">
                                <img src="Content/images/banner3.png" alt="image">
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
        <!-- FlexSlider -->
        <!-- Mid Portion -->
        <div class="midportion">
            <div class="wrapper">
                <div class="mid_content">
                    <h1><%= this.GetLocalResourceObject("Whatis4ClearOrtho") %>
                    </h1>
                    <p>
                        <%= this.GetLocalResourceObject("4clearOrthoInfo") %>
                        <br>
                        <%= this.GetLocalResourceObject("4clearOrthoInfo1") %>
                    </p>
                </div>
                <div class="boxes">
                    <ul id="boxes">
                        <li>
                            <div class="left">
                                <img src="Content/images/HomePage_29.png">
                            </div>
                            <div class="right">
                                <h2><%= this.GetLocalResourceObject("4ClearOrtho") %>
                                </h2>
                                <ul class="subbox" id="ulOrtho" runat="server">
                                </ul>
                            </div>
                        </li>
                        <li>
                            <div class="left">
                                <img src="Content/images/HomePage_26.png">
                            </div>
                            <div class="right">
                                <h2><%= this.GetLocalResourceObject("ForPatient") %>
                                </h2>
                                <ul class="subbox" id="ulPatient" runat="server">
                                </ul>
                                <span class="login"><a href="PatientLogin.aspx"><%= this.GetLocalResourceObject("Login") %>
                                </a></span>
                            </div>
                        </li>
                        <li>
                            <div class="left">
                                <img src="Content/images/HomePage_23.png">
                            </div>
                            <div class="right">
                                <h2><%= this.GetLocalResourceObject("ForDoctors") %>
                                </h2>
                                <ul class="subbox" id="ulDoctors" runat="server">
                                </ul>
                                <span class="login"><a href="DoctorLogin.aspx"><%= this.GetLocalResourceObject("Login") %></a></span>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="contact_footer">
                    <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                    <div class="inner_contact">
                        <a class="mail_footer" href="Contact-Us.aspx"><%= this.GetLocalResourceObject("ContactUs") %></a>
                    </div>
                </div>
                <div class="footer">
                    <div class="footer_left">
                        © <%=DateTime.Now.Year.ToString() %> 4clearortho.com <%= this.GetLocalResourceObject("Rights") %>.
                    </div>
                    <div class="footer_right"><%= this.GetLocalResourceObject("PoweredBy") %> 4edental.com</div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
        <!-- Mid Portion -->
    </form>
    <!-- jQuery -->
    <script type="text/javascript" src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="Scripts/jquery.selectbox-0.2.js" type="text/javascript"></script>
    <script src="Scripts/jquery.flexslider.js" type="text/javascript"></script>
    <!-- FlexSlider -->
    <script type="text/javascript">
        $(function () {
            $("#Language_Dropdown, #ddlLanguage").selectbox();
        });
        $(window).load(function () {
            $('.flexslider').flexslider({
                animation: "slide",
                start: function (slider) {
                    $('body').removeClass('loading');
                }
            });
        });
    </script>
</body>
</html>
