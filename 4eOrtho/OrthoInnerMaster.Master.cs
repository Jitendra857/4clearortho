using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class OrthoInnerMaster : System.Web.UI.MasterPage
    {
        #region Declaration
        CurrentSession currentSession = null;
        BecomeProviderEntity becomeProviderEntity;
        private ILog logger = log4net.LogManager.GetLogger(typeof(OrthoInnerMaster));
        #endregion

        #region Events

        /// <summary>
        /// Page On Init event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            try
            {

                if ((CurrentSession)Session["UserLoginSession"] != null)
                {
                    MenuRightsWithRole();
                    if (!((CurrentSession)Session["UserLoginSession"]).IsPayment && ((CurrentSession)Session["UserLoginSession"]).UserType == "D")
                        Response.Redirect("Payment.aspx");
                }
                else
                    Response.Redirect("Home.aspx");                
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("Login process for WSB Admin", ex);
            }
        }

        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserLoginSession"] != null)
                {
                    currentSession = new CurrentSession();
                    currentSession = (CurrentSession)Session["UserLoginSession"];
                    if (currentSession.UserType == UserType.P.ToString())
                    {
                        lblUser.Text = currentSession.PatientFirstName + ' ' + currentSession.PatientLastName;
                    }
                    else if (currentSession.UserType == UserType.D.ToString() || currentSession.UserType == UserType.S.ToString())
                    {
                        lblUser.Text = currentSession.DoctorName;
                    }
                }
                else
                {
                    Response.Redirect("Home.aspx");
                    return;
                }
                if (!IsPostBack)
                {
                    if (currentSession.UserType == UserType.P.ToString())
                    {
                        BindPatientMenu();
                    }
                    else if (currentSession.UserType == UserType.D.ToString() || currentSession.UserType == UserType.S.ToString())
                    {
                        BindDoctorMenu();
                    }

                    PagesEntity pagesEntity = new PagesEntity();
                    PageDetail pageWithDetail = pagesEntity.GetPageDetailByMenuNameandLanguage("4ClearOrtho-Address", SessionHelper.LanguageId);
                    if (pageWithDetail != null)
                    {
                        ltrAddress.Text = pageWithDetail.PageContent;
                    }
                    if (SessionHelper.CurrentCultureName == "ar-SA" || SessionHelper.CurrentCultureName == "fa-IR")
                        lnk_style.Attributes.Add("href", "Styles/style.ar-SA.css?ver=1.1.3");
                    else
                        lnk_style.Attributes.Add("href", "Styles/style.css?ver=1.1.3");
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }

        }

        /// <summary>
        /// Event to log out user session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgLogOut_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["UserLoginSession"] = null;
                SessionHelper.IsAbleToNavigate = false;
                SessionHelper.LoggedUserEmailAddress = string.Empty;

                HttpCookie hcAccount = Request.Cookies["4eOrtho_Cookie"];
                if (hcAccount != null)
                {
                    hcAccount.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(hcAccount);
                }

                if (((CurrentSession)Session["UserLoginSession"]) == null)
                {
                    Response.Redirect("Home.aspx");
                    return;
                }
                else if (((CurrentSession)Session["UserLoginSession"]).UserType == UserType.P.ToString())
                {
                    ((CurrentSession)Session["UserLoginSession"]).UserType = string.Empty;
                    Response.Redirect("PatientLogin.aspx");
                    return;

                }
                else if (((CurrentSession)Session["UserLoginSession"]).UserType == UserType.D.ToString() || ((CurrentSession)Session["UserLoginSession"]).UserType == UserType.S.ToString())
                {
                    ((CurrentSession)Session["UserLoginSession"]).UserType = string.Empty;
                    Response.Redirect("DoctorLogin.aspx");
                    return;
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Event to registered user or become Certified.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                CurrentSession currentSession = new CurrentSession();
                if (hdSubscribe.Value == "Registered")
                {
                    currentSession = (CurrentSession)Session["UserLoginSession"];
                    if (currentSession != null)
                    {
                        becomeProviderEntity = new BecomeProviderEntity();
                        HttpCookie hcAccount = new HttpCookie("4eOrtho_Cookie");
                        StudentDetails studentDetails = becomeProviderEntity.GetStudentDetailsByEmailId(currentSession.EmailId);
                        hcAccount.Values["EmailId"] = currentSession.EmailId;
                        hcAccount.Values["Password"] = currentSession.Password;
                        hcAccount.Values["StudentId"] = Convert.ToString(studentDetails.StudentID);
                        hcAccount.Values["UserType"] = Convert.ToString(UserType.S);
                        hcAccount.Values["CourseCategoryId"] = Convert.ToString(studentDetails.CourseCategoryId);
                        hcAccount.Values["FirstName"] = Convert.ToString(studentDetails.FirstName);
                        hcAccount.Values["LastName"] = Convert.ToString(studentDetails.LastName);
                        hcAccount.Values["UseId"] = Convert.ToString(studentDetails.UserID);
                        hcAccount.Values["StudentTestId"] = Convert.ToString(studentDetails.StudentTestId);
                        hcAccount.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(hcAccount);
                        string studentId = Server.UrlEncode(Cryptography.EncryptStringAES(Convert.ToString(studentDetails.UserID), CommonLogic.GetConfigValue("sharedSecret")));
                        //Response.Redirect(CommonLogic.GetConfigValue("StudentCourseSubscribe") + "&SId=" + studentId);

                        // Added By Navik
                        LookupMasterEntity lookupMasterEntity = new LookupMasterEntity();
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Click", "window.open('" + lookupMasterEntity.GetStudentCourseSubscribeLinkFromLookUpMaster() + "');", true);


                        // Commented by navik
                        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Click", "window.open('" + CommonLogic.GetConfigValue("StudentCourseSubscribe") + "&SId=" + studentId + "');", true);
                    }
                }
                //else if (hdSubscribe.Value == "Subscribed")
                //{
                //    currentSession = (CurrentSession)Session["UserLoginSession"];
                //    if (currentSession != null)
                //    {
                //        becomeProviderEntity = new BecomeProviderEntity();
                //        HttpCookie hcAccount = new HttpCookie("4eOrtho_Cookie");
                //        StudentDetails studentDetails = becomeProviderEntity.GetStudentDetailsByEmailId(currentSession.EmailId);
                //        hcAccount.Values["EmailId"] = currentSession.EmailId;
                //        hcAccount.Values["Password"] = currentSession.Password;
                //        hcAccount.Values["StudentId"] = Convert.ToString(studentDetails.StudentID);
                //        hcAccount.Values["UserType"] = Convert.ToString(UserType.S);
                //        hcAccount.Values["CourseCategoryId"] = Convert.ToString(studentDetails.CourseCategoryId);
                //        hcAccount.Values["FirstName"] = Convert.ToString(studentDetails.FirstName);
                //        hcAccount.Values["LastName"] = Convert.ToString(studentDetails.LastName);
                //        hcAccount.Values["UseId"] = Convert.ToString(studentDetails.UserID);
                //        hcAccount.Values["StudentTestId"] = Convert.ToString(studentDetails.StudentTestId);
                //        hcAccount.Values["TestStatus"] = Convert.ToString(studentDetails.TestStatus);
                //        hcAccount.Expires = DateTime.Now.AddDays(1);
                //        Response.Cookies.Add(hcAccount);
                //        Response.Redirect(CommonLogic.GetConfigValue("StudentCertificate") + "/Examination.aspx?StudentTestID=" + CommonLogic.EncryptStringAES(Convert.ToString(studentDetails.StudentTestId)) + "&Status=" + CommonLogic.EncryptStringAES(studentDetails.TestStatus) + "");
                //    }
                //}
                else if (hdSubscribe.Value == "Certified")
                {
                    currentSession = (CurrentSession)Session["UserLoginSession"];
                    if (currentSession != null)
                    {
                        becomeProviderEntity = new BecomeProviderEntity();
                        BecomeProvider provider = new BecomeProvider();
                        provider = becomeProviderEntity.Create();
                        provider.CreatedDate = BaseEntity.GetServerDateTime;
                        provider.CreatedBy = 0;
                        provider.DoctorEmail = currentSession.EmailId;
                        provider.DoctorId = 0;
                        provider.IsSubscribe = false;
                        provider.IsRegistered = false;
                        provider.IsProvider = true;
                        provider.IsActive = true;
                        becomeProviderEntity.Save(provider);
                        becomeProviderEntity.UpdateStudenteDetails(currentSession.EmailId);
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }

        }

        /// <summary>
        /// Event to become certified.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnBecomeCertified_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    Int64 userId = new DoctorEntity().SubscribeAADStudent(currentSession.EmailId, currentSession.Password, currentSession.DoctorFirstName, currentSession.DoctorLastName, currentSession.MI, currentSession.Gender, Convert.ToDateTime(currentSession.DOB), string.Empty, currentSession.HomeContact, currentSession.DoctorMobile, currentSession.WorkContact, Convert.ToInt64(currentSession.CountryId), Convert.ToInt64(currentSession.StateId), currentSession.DoctorCity, currentSession.DoctorStreet, currentSession.DoctorZipcode);
                    if (userId > 0)
                    {
                        string emailTemplatePath = CommonLogic.GetConfigValue("StudentRegistration").ToString();
                        CommonLogic.WelcomeUserMail(currentSession.EmailId, currentSession.Password, currentSession.DoctorFirstName, currentSession.DoctorLastName, emailTemplatePath, currentSession.EmailId, "4eDental University – Welcome User");

                        // Added By Navik
                        LookupMasterEntity lookupMasterEntity = new LookupMasterEntity();
                        ScriptManager.RegisterStartupScript(Page, typeof(PageBase), "OpenWindow", "window.open('" + lookupMasterEntity.GetStudentCourseSubscribeLinkFromLookUpMaster() + "&SId=" + Server.UrlEncode(Cryptography.EncryptStringAES(Convert.ToString(userId), CommonLogic.GetConfigValue("sharedSecret"))) + "');", true);

                        // Commented by navik                        
                        //ScriptManager.RegisterStartupScript(Page, typeof(PageBase), "OpenWindow", "window.open('" + CommonLogic.GetConfigValue("StudentCourseSubscribe") + "&SId=" + Server.UrlEncode(Cryptography.EncryptStringAES(Convert.ToString(userId), CommonLogic.GetConfigValue("sharedSecret"))) + "');", true);
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }

        }
        #endregion

        #region Helpers

        /// <summary>
        /// Method to bind patient login menu.
        /// </summary>
        public void BindPatientMenu()
        {
            try
            {
                PageDetailsEntity pageDetailsEntity = new PageDetailsEntity();
                List<PageDetail> pageDetails = pageDetailsEntity.GetPageDetailsByRole(UserType.P.ToString(), SessionHelper.LanguageId);
                StringBuilder menuHTML = new StringBuilder();

                if (pageDetails != null && pageDetails.Count > 0)
                {
                    foreach (PageDetail pageDetail in pageDetails)
                        menuHTML.AppendLine(@"<li><a href='" + pageDetail.URLName + "'>" + pageDetail.MenuItem + "</a></li>");
                }
                menuHTML.AppendLine(@"<li><a href='AddRecommendedDentist.aspx'>" + this.GetLocalResourceObject("Recommendadentist") + "</a></li>");

                //menuHTML.AppendLine(@"<li><a href='FindDoctor.aspx'>" + this.GetLocalResourceObject("FindaDoctor") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='AddClientTestimonial.aspx'> " + this.GetLocalResourceObject("AddTestimonial") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='ClientTestimonials.aspx'>" + this.GetLocalResourceObject("Testimonial") + "</a></li>");
                //menuHTML.AppendLine(@"<li><a href='PatientBeforeAfterPictures.aspx'>" + this.GetLocalResourceObject("BeforeAfter") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='PatientBeforeAfterPictures.aspx'>" + this.GetLocalResourceObject("PatientReviewGallery") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='PatientStageDetails.aspx'>" + this.GetLocalResourceObject("Stage") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='ChangePassword.aspx'>" + this.GetLocalResourceObject("ChangePassword") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='PatientProfile.aspx'>" + this.GetLocalResourceObject("EditProfile") + "</a></li>");

                subcat.InnerHtml = menuHTML.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Method to bind doctor login menu.
        /// </summary>
        public void BindDoctorMenu()
        {
            try
            {
                PageDetailsEntity pageDetailsEntity = new PageDetailsEntity();
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                //List<PageDetail> pageDetails = pageDetailsEntity.GetPageDetailsByRole(UserType.D.ToString(), SessionHelper.LanguageId);
                StringBuilder menuHTML = new StringBuilder();

                if (!currentSession.IsAccActive)
                {
                    //menuHTML.AppendLine(@"<li><a href='AccountNotActivate'>" + this.GetLocalResourceObject("ManageGallery") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='AccountNotActivate'>" + this.GetLocalResourceObject("PatientGallery") + "</li>");
                    menuHTML.AppendLine(@"<li><a href='AccountNotActivate' style='cursor:pointer'>" + this.GetLocalResourceObject("Brochure") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='AccountNotActivate'>" + this.GetLocalResourceObject("ManagePictureTemplate") + "</li>");
                    menuHTML.AppendLine(@"<li><a href='AccountNotActivate'>" + this.GetLocalResourceObject("NewCase") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='AccountNotActivate'>" + this.GetLocalResourceObject("PatientList") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='AccountNotActivate'>" + this.GetLocalResourceObject("TrackCase") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='AccountNotActivate'>" + this.GetLocalResourceObject("OrderSupply") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='AccountNotActivate'>" + this.GetLocalResourceObject("Testimonial") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='AccountNotActivate'>" + this.GetLocalResourceObject("ReviewManagement") + "</a></li>");
                }
                else
                {
                    //menuHTML.AppendLine(@"<li><a href='ListPatientGallery.aspx'>" + this.GetLocalResourceObject("ManageGallery") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='BeforeAfterPictures.aspx'>" + this.GetLocalResourceObject("PatientGallery") + "</li>");
                    menuHTML.AppendLine(@"<li><a href='#' onclick='ShowAppointmentRequest();' style='cursor:pointer'>" + this.GetLocalResourceObject("Brochure") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='ListPictureTemplate.aspx'>" + this.GetLocalResourceObject("ManagePictureTemplate") + "</li>");
                    menuHTML.AppendLine(@"<li><a href='" + (currentSession.IsAccActive ? "ListNewCase.aspx" : "#") + "'>" + this.GetLocalResourceObject("NewCase") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='ListPatient.aspx'>" + this.GetLocalResourceObject("PatientList") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='ListTrackCase.aspx'>" + this.GetLocalResourceObject("TrackCase") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='ListSupplyOrder.aspx'>" + this.GetLocalResourceObject("OrderSupply") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='ClientTestimonials.aspx'>" + this.GetLocalResourceObject("Testimonial") + "</a></li>");
                    menuHTML.AppendLine(@"<li><a href='ListReview.aspx'>" + this.GetLocalResourceObject("ReviewManagement") + "</a></li>");
                }

                if (!currentSession.IsCertified && currentSession.SourceType != "AAD")
                {
                    menuHTML.AppendLine(@"<li><a href='UploadCertificate.aspx'>" + this.GetLocalResourceObject("UploadCertificate") + "</a></li>");
                }
                //Temp.. Hide becomeCertified 20121229
                if (currentSession != null && !currentSession.IsCertified)
                {
                    menuHTML.AppendLine(@"<li><a href='#' onclick='BecomeCertified();' style='cursor:pointer'>" + this.GetLocalResourceObject("BecomeCertified") + "</a></li>");
                }

                //menuHTML.AppendLine(@"<li><a href='AddClientTestimonial.aspx'>" + this.GetLocalResourceObject("AddTestimonial") + "</a></li>");

                //string status = string.Empty;
                //status = new BecomeProviderEntity().GetBecomeProviderStatus(currentSession.EmailId);
                //if (status == "NotRegistered")
                //    menuHTML.AppendLine(@"<li><a  href='" + CommonLogic.GetConfigValue("StudentRegistration") + "' target='_blank'>" + this.GetLocalResourceObject("BecomeProvider") + "</a></li>");
                //if (status == "Registered")
                //    menuHTML.AppendLine(@"<li><a  href='' id='aRegister' target='_blank'>" + this.GetLocalResourceObject("BecomeProvider") + "</a></li>");
                //else if (status == "Subscribed")
                //    menuHTML.AppendLine(@"<li><a  href='#' id='aSubsribe'>" + this.GetLocalResourceObject("BecomeProvider") + "</a></li>");
                //else if (status == "Certified")
                //    menuHTML.AppendLine(@"<li><a  href='#' id='aCertified'>" + this.GetLocalResourceObject("BecomeProvider") + "</a></li>");

                //if (status == "NotRegistered")
                //    menuHTML.AppendLine(@"<li><a target='_blank' href='" + CommonLogic.GetConfigValue("StudentRegistration") + "'>" + this.GetLocalResourceObject("TakeCourse") + "</a></li>");
                //else if (status == "Registered")
                //    menuHTML.AppendLine(@"<li><a href='#' id='aRegisterCourse'>" + this.GetLocalResourceObject("TakeCourse") + "</a></li>");


                subcat.InnerHtml = menuHTML.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Method to bind public menu.
        /// </summary>
        public void BindPublicMenu()
        {

            try
            {
                PageDetailsEntity pageDetailsEntity = new PageDetailsEntity();
                List<PageDetail> pageDetails = pageDetailsEntity.GetPageDetailsByRole(UserType.P.ToString(), UserType.D.ToString(), SessionHelper.LanguageId);
                StringBuilder menuHTML = new StringBuilder();
                if (pageDetails.Count > 0)
                {
                    foreach (PageDetail pageDetail in pageDetails)
                    {
                        menuHTML.AppendLine(@"<li><a href='" + pageDetail.URLName + "'>" + pageDetail.MenuItem + "</a></li>");
                    }

                }
                menuHTML.AppendLine(@"<li><a href='FindDoctor.aspx'>" + this.GetLocalResourceObject("FindaDoctor") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='BeforeAfterPictures.aspx'>" + this.GetLocalResourceObject("PatientGallery") + "</li>");
                menuHTML.AppendLine(@"<li><a href='ClientTestimonials.aspx'>" + this.GetLocalResourceObject("Testimonial") + "</a></li>");
                //menuHTML.AppendLine(@"<li><a href='#'>" + this.GetLocalResourceObject("NewCase") + "</a></li>");
                //menuHTML.AppendLine(@"<li><a href='#'>" + this.GetLocalResourceObject("TrackCase") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='ListSupplyOrder.aspx'>" + this.GetLocalResourceObject("OrderSupply") + "</a></li>");
                menuHTML.AppendLine(@"<li><a href='Brochure.aspx' target='_blank'>" + this.GetLocalResourceObject("Brochure") + "</a></li>");
                //menuHTML.AppendLine(@"<li><a href='PatientBeforeAfterPictures.aspx'>Before After Pictures</a></li>");
                subcat.InnerHtml = menuHTML.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }

        }

        private void MenuRightsWithRole()
        {
            #region Patient
            List<PageList> lstPage = new List<PageList>();
            PageList p = null;

            string[] lstPatientPages = { "FindDoctor", "AddAppointmentRequest", "DoctorReview", "AddRecommendedDentist", "AddClientTestimonial", "AppointmentRequest", "AddEditGallery", "ListGallery", "Welcome", 
                                         "PatientIntroductoryVideo", "ClientTestimonials", "PatientBeforeAfterPictures", "ChangePassword", "PatientProfile" , "DoctorProfile" ,"Contact_Us" };

            foreach (string pageName in lstPatientPages)
            {
                p = new PageList();
                p.PageName = pageName;
                p.UserType = UserType.P.ToString();
                lstPage.Add(p);
            }

            #endregion

            #region Doctor

            string[] lstDoctorPages = { "Welcome", "AddAppointmentRequest", "DoctorReview","Contact_Us", "ListPatientGallery", "AddEditPatientGallery", "BeforeAfterPictures", "ListPictureTemplate",
                                        "AddEditPictureTemplate", "ListNewCase", "AddNewCase", "ListTrackCase", "ListSupplyOrder", "AddEditSupplyOrder", "ClientTestimonials", 
                                        "AddClientTestimonial", "ListReview", "EditDoctorReview", "Payment", "PaymentFailure", "PaymentSuccess", "UploadCertificate",
                                        "UpdateTrackDetail", "ReviewAndConfirm", "PatientBrochure","PatientBrochureEstimate", "DoctorProfile" , "ListPatient" , "EditPatient" };

            foreach (string pageName in lstDoctorPages)
            {
                p = new PageList();
                p.PageName = pageName;
                p.UserType = UserType.D.ToString();
                lstPage.Add(p);
            }

            #endregion

            SessionHelper.UserPageRights = lstPage;
        }
        #endregion
    }
}