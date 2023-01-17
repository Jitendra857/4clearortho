<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="DoctorProfile.aspx.cs" Inherits="_4eOrtho.DoctorProfile" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- http://www.radioactivethinking.com/rateit/example/example.htm -->
    <link href="Styles/rateit.css" rel="stylesheet" />
    <style type="text/css">
        div.bigstars div.rateit-range
        {
            background: url(Content/images/star-white32.png);
            height: 32px;
        }

        div.bigstars div.rateit-hover
        {
            background: url(Content/images/star-gold32.png);
        }

        div.bigstars div.rateit-selected
        {
            background: url(Content/images/star-gold32.png);
        }

        div.bigstars div.rateit-reset
        {
            background: url(Content/images/star-black32.png);
            width: 32px;
            height: 32px;
        }

            div.bigstars div.rateit-reset:hover
            {
                background: url(Content/images/star-white32.png);
            }
    </style>
    <script src="Scripts/jquery.rateit.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            $('.rateit').rateit();
            $('.rateit').rateit('readonly', true);
            $('.satisfaction').rateit('value', $('#' + '<%=hdPatientSatisfaction.ClientID%>').val());
            $('#dvAppointment').rateit('value', $('#' + '<%=hdAppointment.ClientID%>').val());
            $('#dvOffice').rateit('value', $('#' + '<%=hdOffice.ClientID%>').val());
            $('#dvStaff').rateit('value', $('#' + '<%=hdStaff.ClientID%>').val());
            $('#dvWaitTime').rateit('value', $('#' + '<%=hdWaitTime.ClientID%>').val());
            $('#dvDecisions').rateit('value', $('#' + '<%=hdDecisions.ClientID%>').val());
            $('#dvCondition').rateit('value', $('#' + '<%=hdCondition.ClientID%>').val());
            $('#dvAnswers').rateit('value', $('#' + '<%=hdAnswers.ClientID%>').val());
            $('#dvSpendsRating').rateit('value', $('#' + '<%=hdSpendsRating.ClientID%>').val());

        });
    </script>
    <div class="left_title">
        <h2><%= this.GetLocalResourceObject("DoctorProfile").ToString() %></h2>
    </div>
    <div class="left_img">
        <div class="left_pro_pic">
            <asp:Image ID="imgProfilePic" runat="server" Height="289px" Width="229px" meta:resourcekey="imgProfilePicResource1" />
        </div>
        <div class="left_pro_txt">
            <div class="incont_right">
                <div class="star_cont" runat="server" id="starimg" visible="false">
                    <div class="star">
                        <img src="Content/images/starbig.png">
                    </div>
                </div>
                <div class="incont_right">
                    <div class="doc_content">
                        <p class="one">
                            <asp:Label ID="lblDoctorName" runat="server" meta:resourcekey="lblDoctorNameResource1"></asp:Label>
                        </p>
                        <p class="two">DDS (Ortho)</p>

                    </div>

                    <div class="rating">
                        <h3 class="two" style="border-bottom: none">Patient Satisfaction</h3>
                        <div class="rateit bigstars satisfaction" data-rateit-starwidth="32" data-rateit-starheight="32"></div>
                        <asp:HiddenField ID="hdPatientSatisfaction" runat="server" />
                        <%--<div id="hover5" style="float: left; margin-left: 10px; margin-top: 5px;">
                        </div>--%>
                    </div>
                    <div class="doc_content topmargin">
                        <p class="two">
                            <asp:Label ID="lblCity" runat="server" meta:resourcekey="lblCityResource1"></asp:Label>
                        </p>
                        <p class="two">
                            <asp:Label ID="lblStateName" runat="server" meta:resourcekey="lblStateNameResource1"></asp:Label>
                        </p>
                        <p class="two">
                            <asp:Label ID="lblCountryName" runat="server" meta:resourcekey="lblCountryNameResource1"></asp:Label>
                        </p>
                        <p class="two">
                            <asp:Label ID="lblZipCode" runat="server" meta:resourcekey="lblZipCodeResource1"></asp:Label>
                        </p>
                        <p class="two">
                            <asp:Label ID="lblMobileNo" runat="server" meta:resourcekey="lblMobileNoResource1"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="left_content">
        <p>Dr. Michel Clark Duncan is committed to being a leader in his profession. </p>
        <p>Dr. Michel Clark Duncan is practicing exclusively orthodontic and dentofacial orthopedics and over the years successfully treated over ten thousand orthodontic patients. </p>
        <p>Dr. Michel Clark Duncan completed his Doctorate of Dental Surgery at New York University College of Dentistry. He completed two additional years in the Post Graduate Orthodontic program at New York University where he received his certificate as an Orthodontic Specialist.</p>
    </div>
    <div class="edu">
        <h3><%= this.GetLocalResourceObject("Education").ToString() %></h3>
        <div class="containertab">
            <div class="table-row">
                <div class="col wid">Education</div>
                <div class="col">NYU Dental</div>
            </div>
            <div class="table-row">
                <div class="col wid">Associations</div>
                <div class="col">
                    <p>American Association of Orthodontists (AAO)</p>
                    <p>American Dental Association (ADA)</p>
                    <p>RADA Rassian American Dental Association</p>
                    <p>MASO</p>
                </div>
            </div>
            <div class="table-row">
                <div class="col wid">Specialties</div>
                <div class="col">Orthodontist</div>
            </div>
        </div>
    </div>
    <%--<div class="left_content">
        <p>Dr. Michel Clark Duncan is committed to being a leader in his profession. </p>
        <p>Dr. Michel Clark Duncan is practicing exclusively orthodontic and dentofacial orthopedics and over the years successfully treated over ten thousand orthodontic patients. </p>
        <p>Dr. Michel Clark Duncan completed his Doctorate of Dental Surgery at New York University College of Dentistry. He completed two additional years in the Post Graduate Orthodontic program at New York University where he received his certificate as an Orthodontic Specialist.</p>
    </div>
    <div class="edu">
        <h3>EDUCATION AND PRACTICE SPECIALTIES</h3>
        <div class="containertab">
            <div class="table-row">
                <div class="col wid">Education</div>
                <div class="col">NYU Dental</div>
            </div>
            <div class="table-row">
                <div class="col wid">Associations</div>
                <div class="col">
                    <p>American Association of Orthodontists (AAO)</p>
                    <p>American Dental Association (ADA)</p>
                    <p>RADA Rassian American Dental Association</p>
                    <p>MASO</p>
                </div>
            </div>
            <div class="table-row">
                <div class="col wid">Specialties</div>
                <div class="col">Orthodontist</div>
            </div>
        </div>
    </div>--%>
     <div class="review">
        <h3><asp:Label ID="lblDoctorNameTitle" runat="server"></asp:Label>'s <%= this.GetLocalResourceObject("Office").ToString() %> </h3>

        <table class="surveytable">
            <tr>
                <td><%= this.GetLocalResourceObject("Ease").ToString() %>
                </td>
                <td>
                    <div class="rateit bigstars options" id="dvAppointment" data-rateit-starwidth="32" data-rateit-starheight="32"></div>
                    <asp:HiddenField ID="hdAppointment" runat="server" />
                </td>
            </tr>
            <tr>
                <td><%= this.GetLocalResourceObject("OfficeEnvironment").ToString() %>
                </td>
                <td>
                    <div class="rateit bigstars options" id="dvOffice" data-rateit-starwidth="32" data-rateit-starheight="32"></div>
                    <asp:HiddenField ID="hdOffice" runat="server" />
                </td>
            </tr>
            <tr>
                <td><%= this.GetLocalResourceObject("Staff").ToString() %>
                </td>
                <td>
                    <div class="rateit bigstars options" id="dvStaff" data-rateit-starwidth="32" data-rateit-starheight="32"></div>
                    <asp:HiddenField ID="hdStaff" runat="server" />
                </td>
            </tr>
            <tr>
                <td><%= this.GetLocalResourceObject("TotalWait").ToString() %>
                </td>
                <td>
                    <div class="rateit bigstars options" id="dvWaitTime" data-rateit-starwidth="32" data-rateit-starheight="32"></div>
                    <asp:HiddenField ID="hdWaitTime" runat="server" />
                </td>
            </tr>
        </table>
        <h3><%= this.GetLocalResourceObject("Experience").ToString() %> <asp:Label ID="lblDoctorNameTitle2" runat="server"></asp:Label></h3>
        <table class="surveytable">
            <tr>
                <td><%= this.GetLocalResourceObject("LevelTrust").ToString() %>
                </td>
                <td style="width: 215px;">
                    <div class="rateit bigstars options" id="dvDecisions" data-rateit-starwidth="32" data-rateit-starheight="32"></div>
                    <asp:HiddenField ID="hdDecisions" runat="server" />
                </td>
            </tr>
            <tr>
                <td><%= this.GetLocalResourceObject("Conditions").ToString() %>
                </td>
                <td>
                    <div class="rateit bigstars options" id="dvCondition" data-rateit-starwidth="32" data-rateit-starheight="32"></div>
                    <asp:HiddenField ID="hdCondition" runat="server" />
                </td>
            </tr>
            <tr>
                <td><%= this.GetLocalResourceObject("AnswerQuestion").ToString() %>
                </td>
                <td>
                    <div class="rateit bigstars options" id="dvAnswers" data-rateit-starwidth="32" data-rateit-starheight="32"></div>
                    <asp:HiddenField ID="hdAnswers" runat="server" />
                </td>
            </tr>
            <tr>
                <td><%= this.GetLocalResourceObject("TimeWithPatients").ToString() %>
                </td>
                <td>
                    <div class="rateit bigstars options" id="dvSpendsRating" data-rateit-starwidth="32" data-rateit-starheight="32"></div>
                    <asp:HiddenField ID="hdSpendsRating" runat="server" />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
