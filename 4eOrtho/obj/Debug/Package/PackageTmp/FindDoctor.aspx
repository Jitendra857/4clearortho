<%@ Page Title="" Language="C#" MasterPageFile="~/OrthoInnerMaster.Master" AutoEventWireup="true" CodeBehind="FindDoctor.aspx.cs" Inherits="_4eOrtho.FindDoctor" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- http://www.radioactivethinking.com/rateit/example/example.htm -->
    <link href="Styles/rateit.css" rel="stylesheet" />
    <script src="Scripts/jquery.rateit.js" type="text/javascript"></script>
    <link href="Styles/selectric.css" rel="stylesheet" />
    <script src="Scripts/jquery.selectric.js" type="text/javascript"></script>
    <style type="text/css">
        .certiRating {
            display: none;
        }

        .noncertiRating {
            display: none;
        }
    </style>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="left-top">
                <div class="leftdmenu">
                    <asp:DropDownList ID="ddlSearchBy" name="State" runat="server" meta:resourcekey="ddlSearchByResource1" onchange="return OnChange();">
                        <asp:ListItem Value="0" Text="Select All" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="CountryName" Text="Country" meta:resourcekey="ListItemResource4"></asp:ListItem>
                        <asp:ListItem Value="StateName" Text="State" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Value="ZipCode" Text="Zip Code/ Postal" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="search_con">
                    <asp:TextBox ID="txtSearch" Enabled="false" placeholder="Zip Code / Postal / State / Country" runat="server" onkeypress="return isNumber(event)" meta:resourcekey="txtSearchResource1"></asp:TextBox>
                </div>
                <div class="searchbutton">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return searchValidation();" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1" />
                </div>
            </div>
            <table style="display: none">
                <tr>
                    <td>
                        <a href="AppointmentRequest.aspx" id="appReuest" class="appointment_request"
                            style="display: none;"></a>
                        <a id="btnAdd" onclick='ShowAppointmentRequest("CMS_EMR");'><%= this.GetLocalResourceObject("AddAppointment") %></a>
                        <div class="parsonal_select" style="margin-top: 10px">
                        </div>
                    </td>
                    <td>
                        <div class="parsonal_textfild">
                            <label class="wide">Enter Zip/Pin code</label>
                        </div>
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" meta:resourcekey="btnRefreshResource1" />
                    </td>
                </tr>
            </table>
            <div class="search_results">
                <ul id="search_results">
                    <asp:ListView ID="lvCertifiedDoctor" runat="server" GroupItemCount="2" GroupPlaceholderID="groupPlaceHolder1" DataSourceID="odsCertifiedDoctor"
                        ItemPlaceholderID="itemPlaceHolder1" OnItemDataBound="lvCertifiedDoctor_ItemDataBound">
                        <LayoutTemplate>
                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <GroupTemplate>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                        </GroupTemplate>
                        <ItemTemplate>
                            <li>
                                <div class="incont">
                                    <div class="incont_left">
                                        <a id="aProfile" target="_blank" href='<%# "DoctorProfile.aspx?EmailId=" + _4eOrtho.Utility.CommonLogic.EncryptStringAES(Convert.ToString(Eval("EmailId")))%>'>
                                            <asp:Image ID="imgDoctorProfile" CssClass="imgNonCertiDoctorProfile" runat="server" Width="103" Height="130" AlternateText="doctorimage"></asp:Image></a>
                                    </div>
                                    <div class="incont_right">
                                        <div class="star_cont">

                                            <div style="float: left;" class="rateit doctorRating" id="dvCertiDoctorProfile" runat="server">
                                                <asp:Label ID="hdCertiRatingValue" CssClass="certiRating" Text='<%# Eval("Rating") %>' runat="server"></asp:Label>
                                            </div>
                                            <div class="star">
                                                <img src="Content/images/star.png" alt="certificate" />
                                            </div>
                                        </div>
                                        <div class="incont_right">
                                            <div class="doc_content">
                                                <p class="one">
                                                    <a id="aNameProfile" target="_blank" href='<%# "DoctorProfile.aspx?EmailId=" + _4eOrtho.Utility.CommonLogic.EncryptStringAES(Convert.ToString(Eval("EmailId")))%>'>
                                                        <%# Eval("DoctorName") %>
                                                    </a>
                                                </p>
                                                <p class="two">DDS (Ortho)</p>
                                            </div>
                                            <div class="doc_content topmargin">
                                                <p class="two">
                                                    <%# Eval("City") %>
                                                    <%# Eval("StateName") %><br />
                                                    <%# Eval("CountryName")%>
                                                    <%# Eval("ZipCode") %><br />
                                                </p>
                                                <p class="two">
                                                    <%# Eval("Mobile")%>
                                                </p>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- <div class="incont" style="height: 30px">
                            <p class="add_appointment" style='<%# Convert.ToString(Eval("IsPatientRating")) == "1" ? "": "display:none" %>'>
                                <a id="aRateId" target="_blank" href='<%# "Rating.aspx?EmailId=" + _4eOrtho.Utility.CommonLogic.EncryptStringAES(Convert.ToString(Eval("EmailId"))) + "&Rating=" + _4eOrtho.Utility.CommonLogic.EncryptStringAES("GiveRating")  %>'><%= this.GetLocalResourceObject("RateIt") %></a>

                            </p>
                            <p class="add_appointment">
                                <a id="aReview" target="_blank" href='<%# "DoctorReview.aspx?EmailId=" + _4eOrtho.Utility.CommonLogic.EncryptStringAES(Convert.ToString(Eval("EmailId")))%>'><%= this.GetLocalResourceObject("Review") %></a>
                            </p>
                        </div>
                        <div class="incont" style="height: 30px">
                            <p class="add_appointment">
                                <a id="btnAdd" title="<%= this.GetLocalResourceObject("AddAppointment") %>" onclick='<%# string.Format("return ShowAppointmentRequest(\"{0}\", \"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\");",Eval("DataBaseName"),Eval("EmailId"),Eval("FirstName"),Eval("LastName"),Eval("DoctorId"),(Session["UserLoginSession"] != null ? ((_4eOrtho.BAL.CurrentSession)Session["UserLoginSession"]).EmailId: "")) %>'><%= this.GetLocalResourceObject("AddAppointment") %></a>
                            </p>
                        </div>--%>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                    <asp:ObjectDataSource ID="odsCertifiedDoctor" runat="server" SelectMethod="GetCertifiedDoctorDetailsByFilterType"
                        SelectCountMethod="GetTotalRowCertifiedCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                        TypeName="_4eOrtho.FindDoctor" OnSelecting="odsCertifiedDoctor_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="sortField" Type="String" />
                            <asp:Parameter Name="sortDirection" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </ul>
            </div>
            <div class="pagination clearfix datapager">
                <asp:DataPager ID="lvDoctorPager" runat="server" PagedControlID="lvCertifiedDoctor">
                    <Fields>
                        <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page" NumericButtonCssClass="button-page" meta:resourcekey="NumericPagerFieldResource1" />
                    </Fields>
                </asp:DataPager>
            </div>
            <div class="search_results">
                <ul id="search_results">
                    <asp:ListView ID="lvNonCertifiedDoctor" runat="server" GroupItemCount="2" GroupPlaceholderID="groupPlaceHolder1" DataSourceID="odsNonCertifiedDoctor"
                        ItemPlaceholderID="itemPlaceHolder1" OnItemDataBound="lvNonCertifiedDoctor_ItemDataBound">
                        <LayoutTemplate>
                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <GroupTemplate>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                        </GroupTemplate>
                        <ItemTemplate>
                            <li>
                                <div class="incont">
                                    <div class="incont_left">
                                        <a id="aProfile" target="_blank" href='<%# "DoctorProfile.aspx?EmailId=" + _4eOrtho.Utility.CommonLogic.EncryptStringAES(Convert.ToString(Eval("EmailId")))%>'>
                                            <asp:Image ID="imgDoctorProfile" CssClass="imgCertiDoctorProfile" runat="server" Width="103" Height="130" AlternateText="doctorimage" />
                                        </a>
                                    </div>
                                    <div class="incont_right">
                                        <div class="star_cont">
                                            <div style="float: left;" class="rateit doctorRating" id="dvNonCertiDoctorProfile" runat="server">
                                                <asp:Label ID="hdNonRatingValue" CssClass="noncertiRating" Text='<%# Eval("Rating") %>' runat="server"></asp:Label>
                                            </div>
                                            <div class="star">
                                            </div>
                                        </div>
                                        <div class="incont_right">
                                            <div class="doc_content">
                                                <p class="one">
                                                    <a id="aNonCerProfile" target="_blank" href='<%# "DoctorProfile.aspx?EmailId=" + _4eOrtho.Utility.CommonLogic.EncryptStringAES(Convert.ToString(Eval("EmailId")))%>'>
                                                        <%# Eval("DoctorName") %>
                                                    </a>
                                                </p>
                                                <p class="two">DDS (Ortho)</p>
                                            </div>
                                            <div class="doc_content topmargin">
                                                <p class="two">
                                                    <%# Eval("City") %>
                                                    <%# Eval("StateName") %><br />
                                                    <%# Eval("CountryName")%>
                                                    <%# Eval("ZipCode") %><br />
                                                </p>
                                                <p class="two">
                                                    <%# Eval("Mobile")%>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- <div class="incont" style="height: 30px">
                            <p class="add_appointment" style='<%# Convert.ToString(Eval("IsPatientRating")) == "1" ? "": "display:none" %>'>
                                <a id="aRateId" target="_blank" href='<%# "Rating.aspx?EmailId=" + _4eOrtho.Utility.CommonLogic.EncryptStringAES(Convert.ToString(Eval("EmailId"))) + "&Rating=" + _4eOrtho.Utility.CommonLogic.EncryptStringAES("GiveRating")  %>'><%= this.GetLocalResourceObject("RateIt") %></a>

                            </p>
                            <p class="add_appointment">
                                <a id="aReview" target="_blank" href='<%# "DoctorReview.aspx?EmailId=" + _4eOrtho.Utility.CommonLogic.EncryptStringAES(Convert.ToString(Eval("EmailId")))%>'><%= this.GetLocalResourceObject("Review") %></a>
                            </p>
                        </div>
                        <div class="incont" style="height: 30px">
                            <p class="add_appointment">
                                <a id="btnAdd" title="<%= this.GetLocalResourceObject("AddAppointment") %>" onclick='<%# string.Format("return ShowAppointmentRequest(\"{0}\", \"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\");",Eval("DataBaseName"),Eval("EmailId"),Eval("FirstName"),Eval("LastName"),Eval("DoctorId"),(Session["UserLoginSession"] != null ? ((_4eOrtho.BAL.CurrentSession)Session["UserLoginSession"]).EmailId: "")) %>'><%= this.GetLocalResourceObject("AddAppointment") %></a>
                            </p>
                        </div>--%>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                    <asp:ObjectDataSource ID="odsNonCertifiedDoctor" runat="server" SelectMethod="GetNonCertifiedDoctorDetailsByFilterType"
                        SelectCountMethod="GetTotalRowNonCertifiedCount" EnablePaging="True" MaximumRowsParameterName="pageSize"
                        TypeName="_4eOrtho.FindDoctor" OnSelecting="odsNonCertifiedDoctor_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="sortField" Type="String" />
                            <asp:Parameter Name="sortDirection" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </ul>
            </div>
            <div class="pagination clearfix datapager">
                <asp:DataPager ID="lvNonCertifiedDoctorPager" runat="server" PagedControlID="lvNonCertifiedDoctor">
                    <Fields>
                        <asp:NumericPagerField CurrentPageLabelCssClass="selected-button-page" NumericButtonCssClass="button-page" meta:resourcekey="NumericPagerFieldResource2" />
                    </Fields>
                </asp:DataPager>
            </div>

            <div class="search_results" id="divNoDataFound" style="display: none;">
                <div class="incont">No searched data found.</div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(document).ready(function () {
            if (typeof (Sys) != "undefined") {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler);
            }
            dropdownStyle();
            CheckRecordFound();
        });

        function CheckRecordFound() {
            if ($('.search_results').find('li').length > 0)
                $('#divNoDataFound').hide();
            else
                $('#divNoDataFound').show();
        }
        function endRequestHandler() {
            dropdownStyle();
            CheckRecordFound();
        }

        function dropdownStyle() {
            $('select, .select').selectric();
            $('.customOptions').selectric({
                optionsItemBuilder: function (itemData, element, index) {
                    return element.val().length ? '<span class="ico ico-' + element.val() + '"></span>' + itemData.text : itemData.text;
                }
            });

            jQuery(".appointment_request").colorbox({
                iframe: true,
                width: "800px",
                height: "450px",
                overlayClose: false,
                escKey: true,
                onClosed: function () {
                    window.location.reload();
                }
            });
            //for display rating
            var i = 0;
            $('.certiRating').each(function () {
                var hdCertiRatingValue = $(this).children();
                var dvCertiDoctorProfile = $(this).parent();
                $(dvCertiDoctorProfile).rateit('value', hdCertiRatingValue.context.innerHTML);
                $('.doctorRating').rateit('readonly', true);
                i++;
            });
            var i = 0;
            $('.noncertiRating').each(function () {
                var hdNonCertiRatingValue = $(this).children();
                var dvNonCertiDoctorProfile = $(this).parent();
                $(dvNonCertiDoctorProfile).rateit('value', hdNonCertiRatingValue.context.innerHTML);
                $('.doctorRating').rateit('readonly', true);
                i++;
            });
        }
        function ShowAppointmentRequest(databaseName, doctorEmail, doctorFirstName, doctorLastName, doctorId, patientEmail) {
            if (patientEmail == "") {
                window.location.href = "PatientLogin.aspx";
                return false;
            }
            jQuery('.appointment_request').attr("href", "AddAppointmentRequest.aspx?databaseName=" + databaseName + "&doctorFirstName=" + doctorFirstName + "&doctorLastName=" + doctorLastName + "&doctorEmail=" + doctorEmail + "&doctorId=" + doctorId);
            jQuery('.appointment_request').click();
            return false;
        }
        function OnChange() {
            if ($('#<%=ddlSearchBy.ClientID%>').val() != '0') {
                $('.leftdmenu').css('border', '1px solid #d3d3d3');
                $('[id$=txtSearch]').removeAttr("disabled");
            }
            else {
                $('[id$=txtSearch]').attr("disabled", "disabled");
            }
            $('[id$=txtSearch]').val("");
        }
        function isNumber(evt) {
            if ($('[id$=ddlSearchBy]').val() == "ZipCode") {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            if ($('[id$=txtSearch]').val()) {
                $('[id$=btnSearch]').removeAttr("disabled");
            }
            else {
                $('[id$=btnSearch]').attr("disabled", "disabled");
            }
        }
        function searchValidation() {
            if (!$('[id$=txtSearch]').val() && !$('[id$=txtSearch]').attr('disabled')) {
                $('.search_con').css('border', '1px solid #E66464');
                return false;
            } else
                $('.search_con').css('border', '1px solid #d3d3d3');
            return true;
        }
    </script>
</asp:Content>
