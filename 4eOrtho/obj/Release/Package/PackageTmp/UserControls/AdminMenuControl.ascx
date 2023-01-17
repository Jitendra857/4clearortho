<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminMenuControl.ascx.cs" Inherits="_4eOrtho.UserControls.AdminMenuControl" %>

<div class="alignleft" id="parentmainNavigation" runat="server" style="width: 100% !important;">
    <ul id="mainNavigation" class="mainNavigation" runat="server">
        <li class="none">
            <asp:HyperLink ID="hyplnkManagement" runat="server" Text="Admin" meta:resourcekey="hyplnkManagementResource1"></asp:HyperLink>
            <ul>
                <li>
                    <asp:LinkButton ID="lnkuserManagement" runat="server" Text="User Management" PostBackUrl="../Admin/UserList.aspx"
                        meta:resourcekey="lnkuserManagementResource1"></asp:LinkButton></li>
                <li>
                <li>
                    <asp:HyperLink ID="hyplnkLocalContactUser" runat="server" Text="Local Contact User" NavigateUrl="~/Admin/ListLocalContact.aspx"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="hyplnkContentManagement" runat="server" Text="Page Content"
                        NavigateUrl="../Admin/ListContentPage.aspx" meta:resourcekey="hyplnkContentManagementResource1"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="hyplnkClientTestimonial" runat="server" Text="Testimonial"
                        NavigateUrl="~/Admin/ListClientTestimonial.aspx" meta:resourcekey="hyplnkClientTestimonialResource1"></asp:HyperLink>
                </li>
                <%--<li>
                    <asp:HyperLink ID="hyplnkCountry" runat="server" Text="Country Management"
                        NavigateUrl="../Admin/Listcountry.aspx" meta:resourcekey="hyplnkCountryResource1"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="hyplnkState" runat="server" Text="State Management"
                        NavigateUrl="~/Admin/ListState.aspx" meta:resourcekey="hyplnkStateResource1"></asp:HyperLink>
                </li>--%>
                <li>
                    <asp:HyperLink ID="hypReviewManagment" runat="server" Text="Review Management"
                        NavigateUrl="~/Admin/ListReviewManagement.aspx" meta:resourcekey="hypReviewManagmentResource1"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="hypNewCaseDetails" runat="server" Text="Patient / Case List"
                        NavigateUrl="~/Admin/ListNewCaseDetails.aspx" meta:resourcekey="hypNewCaseDetailsResource1"></asp:HyperLink>
                </li>
                <%--<li>
                    <asp:HyperLink ID="hypDiscountedCaseList" runat="server" Text="Discounted Case List"
                        NavigateUrl="~/Admin/ListDiscountedCase.aspx" meta:resourcekey="hypDiscountedCaseListResource1"></asp:HyperLink>
                </li>--%>
                <li>
                    <asp:HyperLink ID="hypListDoctors" runat="server" Text="Doctor List"
                        NavigateUrl="~/Admin/ListDoctors.aspx" meta:resourcekey="hypListDoctors"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Post Payment Cash List"
                        NavigateUrl="~/Admin/ListPatientCaseCashReport.aspx"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li id="liSetup" runat="server">
            <asp:HyperLink ID="hyplnkSetUp" runat="server" Text="Setup" meta:resourcekey="hyplnkSetUpResource1"></asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="hyplnkContactUsManagement" runat="server" Text="Contact Us" NavigateUrl="~/Admin/ListContactUs.aspx" meta:resourcekey="hyplnkContactUsManagementResource1"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="hyplnkRecommendedDentistManagement" runat="server" Text="Recommended Dentist" NavigateUrl="~/Admin/ListRecommendedDentist.aspx" meta:resourcekey="hyplnkRecommendedDentistManagementResource1"></asp:HyperLink>
                </li>
                <%-- <li>
                    <asp:HyperLink ID="hplnkBecomeProviderList" runat="server" Text="Become Provider" NavigateUrl="~/Admin/ListBecomeProvider.aspx" meta:resourcekey="hplnkBecomeProviderListResource1"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="hplnkTakeCourseList" runat="server" Text="Take Course" NavigateUrl="~/Admin/ListTakeCourse.aspx" meta:resourcekey="hplnkTakeCourseListResource1"></asp:HyperLink>
                </li>--%>
                <li>
                    <asp:HyperLink ID="hplnkFees" runat="server" Text="Registration Configuration" NavigateUrl="~/Admin/EditRegistrationFees.aspx" meta:resourcekey="hplnkFeesResource1"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="hplnkCaseCharges" runat="server" Text="Case Charges" NavigateUrl="~/Admin/ListCaseCharges.aspx" meta:resourcekey="hplnkCaseChargesResource1"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="hplnkShipmentCharges" runat="server" Text="Shipment Charges" NavigateUrl="~/Admin/ListOrthoCharges.aspx" meta:resourcekey="hplnkShipmentChargesResource1"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="hplnkAADCourse" runat="server" Text="AAD Course Link" NavigateUrl="~/Admin/AddCource.aspx" meta:resourcekey="hplnkAADCourseResource1"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="hplnkCaseType" runat="server" Text="Case Types" NavigateUrl="~/Admin/AddEditCaseTypes.aspx"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="hplnkStageFees" runat="server" Text="Stage Charges" NavigateUrl="~/Admin/EditStageFees.aspx"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li id="liProductManagement" runat="server">
            <asp:HyperLink ID="hyplnkPackage" runat="server" Text="Product" meta:resourcekey="hyplnkPackageResource1"></asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="hyplnkPackageList" runat="server" Text="Product" NavigateUrl="~/Admin/ListProductMaster.aspx" meta:resourcekey="hyplnkPackageListResource1"></asp:HyperLink></li>
                <li>
                    <asp:HyperLink ID="hyplnkModuleManagement" runat="server" Text="Product Package"
                        NavigateUrl="~/Admin/ListProductPackageMaster.aspx" meta:resourcekey="hyplnkModuleManagementResource1"></asp:HyperLink></li>

                <li>
                    <asp:HyperLink ID="hyplnkOrderManagement" runat="server" Text="Order Supply" NavigateUrl="~/Admin/ListSupplyOrder.aspx" meta:resourcekey="hyplnkOrderManagementResource1"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li id="ChangePassword" runat="server">
            <asp:HyperLink ID="hyplnkChangePassword" runat="server" Text="Change Password" NavigateUrl="~/Admin/ChangePassword.aspx" meta:resourcekey="hyplnkChangePasswordResource1"></asp:HyperLink></li>
        <li id="liGallery" runat="server">
            <asp:HyperLink ID="hyplnkGallery" runat="server" Text="Gallery" NavigateUrl="~/Admin/ListGallery.aspx" meta:resourcekey="hyplnkGalleryResource1"></asp:HyperLink>
        </li>

    </ul>
</div>
