using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.Utility;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using log4net;
using System.Collections;
using System.Web.Security;

namespace _4eOrtho.UserControls
{
    /// <summary>
    /// File Name       :   MenuControl.cs
    /// File Description:	UserControl for bind menu from database..
    /// ----------------------------------------------------------------------------------------------------------
    /// Date Created    :	19-07-2014
    /// Author		    :	Piyush Makvana. Verve Systems Pvt. Ltd..
    /// ----------------------------------------------------------------------------------------------------------
    /// Change History
    /// Date Changed    Changed By          Description
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// </summary>
    public partial class MenuControl : System.Web.UI.UserControl
    {
        #region Declaration
        List<MenuPageWithLevel> list = new List<MenuPageWithLevel>();
        BecomeProviderEntity becomeProviderEntity;
        StringBuilder menuHTML = new StringBuilder();
        bool isSetCurrentLink = false;
        long curruntrootID;
        long pageId;
        private ILog Logger = log4net.LogManager.GetLogger(typeof(MenuControl));
        #endregion

        #region PageLoad
        /// <summary>
        /// Page Load Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("inside page load");
                if (!Page.IsPostBack)
                {
                    if (!String.IsNullOrEmpty(CommonLogic.QueryString("id")))
                    {
                        pageId = Convert.ToInt32(CommonLogic.QueryString("id"));
                    }

                    if (!String.IsNullOrEmpty(CommonLogic.QueryString("rid")))
                    {
                        curruntrootID = Convert.ToInt32(CommonLogic.QueryString("rid"));
                    }
                    BindMenuItem();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("error Occred in page load" + ex.ToString());
            }
        }
        #endregion

        private void BindMenuItem()
        {
            try
            {
                Logger.Info("inside bind menu item");
                PagesEntity pagesEntity = new PagesEntity();
                string roleType = "";
                CurrentSession currentSession = new CurrentSession();
                currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession == null)
                    roleType = "";
                else if (currentSession.UserType == UserType.P.ToString())
                    roleType = "P";
                else if (currentSession.UserType == UserType.D.ToString() || currentSession.UserType == UserType.S.ToString())
                    roleType = "D";
                List<MenuPageWithLevel> pageWithLevelList = pagesEntity.GetMenuPage(1, roleType);
                pageWithLevelList = pageWithLevelList.Where(x => x.LanguageID != null).ToList();
                SetProperPageTreeData(pageWithLevelList, null);

                if (pageId == 0 && curruntrootID == 0 && CommonLogic.GetThisPageName(false).ToUpper() == "Default.aspx")
                {
                    MenuPageWithLevel firstPage = list.FirstOrDefault();
                    if (firstPage != null)
                    {
                        //--Original code of CMS
                        //--Response.Redirect(string.Format("index.aspx?rid={0}&id={0}", firstPage.PageID));
                        //--Code changes done by shivani to test URL Routing
                        //--Response.Redirect(string.Format("~/{0}", firstPage.PageTitle));
                        Response.Redirect(string.Format("~/{0}/{1}", firstPage.MenuItem));
                        return;
                    }
                }

                if (list.Where(x => x.PageID == pageId).FirstOrDefault() == null && CommonLogic.GetThisPageName(false).ToUpper() == "Default.aspx")
                {
                    if (list.Count > 0)
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                string contactUSMenuItem = string.Empty;
                string status = string.Empty;

                if (roleType == "D")
                {
                    status = new BecomeProviderEntity().GetBecomeProviderStatus(currentSession.EmailId);
                    if (status == "NotRegistered")
                        menuHTML.AppendLine(@"<li><a target='_blank' href='" + CommonLogic.GetConfigValue("StudentRegistration") + "'>Become Provider</a></li>");
                    if (status == "Registered")
                        menuHTML.AppendLine(@"<li><a target='_blank' href='#' id='aRegister' >Become Provider</a></li>");
                    else if (status == "Subscribed")
                        menuHTML.AppendLine(@"<li><a  href='#' id='aSubsribe'>Become Provider</a></li>");
                    else if (status == "Certified")
                        menuHTML.AppendLine(@"<li><a  href='#' id='aCertified'>Become Provider</a></li>");

                    if (status == "NotRegistered")
                        menuHTML.AppendLine(@"<li><a target='_blank' href='" + CommonLogic.GetConfigValue("StudentRegistration") + "'>Take Course</a></li>");
                    else if (status == "Registered")
                        menuHTML.AppendLine(@"<li><a target='_blank' href='#' id='aRegisterCourse'>Take Course</a></li>");

                    menuHTML.AppendLine(@"<li><a href='AddEditSupplyOrder.aspx'>Supply Order</a></li>");
                    menuHTML.AppendLine(@"<li><a href='Contact-us.aspx'>Contact Us</a></li>");
                    menuHTML.AppendLine(@"<li><a href='ListSupplyOrder.aspx'>Order Supply</a></li>");
                }
                else if (roleType == "P")
                {
                    menuHTML.AppendLine(@"<li><a href='AddRecommendedDentist.aspx'>Recommend Dentist</a></li>");
                    menuHTML.AppendLine(@"<li><a href='AddClientTestimonial.aspx'>Testimonial</a></li>");
                    menuHTML.AppendLine(@"<li><a href='FindDoctor.aspx'>Find Doctor</a></li>");
                    menuHTML.AppendLine(@"<li><a href='Contact-us.aspx'>Contact Us</a></li>");
                }
                else if (roleType == "")
                {
                    //menuHTML.AppendLine(@"<li><a href='Contact-us.aspx'>Contact Us</a></li>");
                    menuHTML.AppendLine(@"<li><a href='PatientLogin.aspx'>Patient Login</a></li>");
                    menuHTML.AppendLine(@"<li><a href='DoctorLogin.aspx'>Doctor Login</a></li>");
                    menuHTML.AppendLine(@"<li><a href='AddEditSupplyOrder.aspx'>Supply Order</a></li>");
                }



                GenerateMenuHtml(list, null, 0);

                menuHTML.AppendLine(contactUSMenuItem);
                lblMenuitem.Text = menuHTML.ToString();
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                Logger.Error("error Occred in Bind Menuitem" + ex.ToString());
            }
        }

        /// <summary>
        /// Description : Method for setting tree data
        /// </summary>
        /// <param name="pageWithLevelList"></param>
        /// <param name="parentId"></param>
        private void SetProperPageTreeData(List<MenuPageWithLevel> pageWithLevelList, long? parentId)
        {
            var nodes = pageWithLevelList.Where(x => x.ParentID == parentId && x.Status == true).OrderBy(x => x.DisplayOrder).ThenBy(x => x.PageID).ToList();

            if (CommonLogic.GetConfigValue("EnableUserAuthModule") == "1")
            {
                if (!CommonLog.HasLoginUser())
                {
                    nodes = nodes.Where(x => x.RequiredAuthentication == false).ToList();
                }
            }

            foreach (var node in nodes)
            {
                list.Add(node);
                SetProperPageTreeData(pageWithLevelList, node.PageID);
            }
        }

        /// <summary>
        /// Description : For generating MenuHTML
        /// </summary>
        /// <param name="pageWithLevelList"></param>
        /// <param name="parentId"></param>
        /// <param name="rootID"></param>
        private void GenerateMenuHtml(List<MenuPageWithLevel> pageWithLevelList, long? parentId, long rootID)
        {
            try
            {
                Logger.Info("inside generate menu html");
                MenuPageWithLevel currentNode;
                var nodes = pageWithLevelList.Where(x => x.ParentID == parentId && x.Status == true).OrderBy(x => x.DisplayOrder).ThenBy(x => x.PageID).ToList();
                if (parentId.HasValue)
                {
                    currentNode = list.Where(x => x.PageID == parentId.Value).FirstOrDefault();
                    string strMenuSelectedClass = string.Empty;
                    if (currentNode.ParentID == null)
                    {
                        if (pageId == 0)
                        {
                            pageId = parentId.Value;
                        }
                        if (!isSetCurrentLink)
                        {
                            if (curruntrootID == 0)
                            {
                                strMenuSelectedClass = "class=''";
                                isSetCurrentLink = true;
                            }
                            else if (curruntrootID == currentNode.PageID)
                            {
                                strMenuSelectedClass = "class=''";
                                isSetCurrentLink = true;
                            }
                        }
                    }

                    if (HasAnyChildren(parentId.Value))
                    {
                        //--Original code of CMS
                        //--menuHTML.AppendLine(@"<li><a href='index.aspx?rid=" + rootID.ToString() + "&id=" + parentId.Value + "' >" + currentNode.MenuItem + "</a><ul>");
                        //--Code changes done by shivani to test URL Routing
                        //menuHTML.AppendLine(@"<li><a href='" + currentNode.MenuItem + "'>" + currentNode.MenuItem + "</a><ul>");
                        menuHTML.AppendLine(@"<li><a href='" + GetPageURL(currentNode.URLName.ToString(), "StoreRoute") + "'>" + currentNode.MenuItem + "</a><ul>");

                    }
                    else
                    {
                        //--Original code of CMS
                        //--menuHTML.AppendLine(@"<li><a href='index.aspx?rid=" + rootID.ToString() + "&id=" + parentId.Value + "'>" + currentNode.MenuItem + "</a></li>");
                        //--Code changes done by shivani to test URL Routing
                        //menuHTML.AppendLine(@"<li><a href='" + currentNode.MenuItem + "'>" + currentNode.MenuItem + "</a></li>");
                        menuHTML.AppendLine(@"<li><a href='" + GetPageURL(currentNode.URLName.ToString(), "StoreRoute") + "'>" + currentNode.MenuItem + "</a></li>");

                    }
                }
                foreach (var node in nodes)
                {
                    if (node.Level == 1)
                    {
                        rootID = node.PageID;
                    }
                    GenerateMenuHtml(pageWithLevelList, node.PageID, rootID);
                }
                if (parentId != null)
                {
                    if (HasAnyChildren(parentId.Value))
                    {
                        menuHTML.AppendLine(@"</ul></li>");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("error Occred in Generate Menu :: " + ex.StackTrace.ToString());
            }
        }

        public static string GetPageURL(string pageName, string routeName)
        {
            string pageURL = string.Empty;
            RouteValueDictionary urlParameters = new RouteValueDictionary{
                        {"Name", pageName}};
            VirtualPathData urlPathData = RouteTable.Routes.GetVirtualPath(null, routeName, urlParameters);
            pageURL = urlPathData.VirtualPath;
            return pageURL;
        }
        /// <summary>
        /// Description : This method is used to check if menu has any child menu
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        private bool HasAnyChildren(long pageID)
        {
            return list.Where(x => x.ParentID == pageID).Count() > 0;
        }

        protected override void OnInit(EventArgs e)
        {
            //MenuPageRightsWithRole();
            MenuRightsWithRole();

        }

        private void MenuRightsWithRole()
        {
            #region Patient
            List<PageList> lstPage = new List<PageList>();
            PageList p = new PageList();

            string[] lstPatientPages = { "FindDoctor", "DoctorReview", "AddRecommendedDentist", "AddClientTestimonial", "AppointmentRequest", "AddEditGallery", "ListGallery", "Welcome", 
                                         "PatientIntroductoryVideo", "ClientTestimonials", "PatientBeforeAfterPictures", "ChangePassword", "PatientProfile" , "Contact-Us" };

            foreach (string pageName in lstPatientPages)
            {
                p.PageName = pageName;
                p.UserType = UserType.P.ToString();
                lstPage.Add(p);
            }

            #endregion

            #region Doctor

            string[] lstDoctorPages = { "Welcome", "Contact-Us", "ListPatientGallery", "AddEditPatientGallery", "BeforeAfterPictures", "ListPictureTemplate",
                                        "AddEditPictureTemplate", "ListNewCase", "AddNewCase", "ListTrackCase", "ListSupplyOrder", "AddEditSupplyOrder", "ClientTestimonials", 
                                        "AddClientTestimonial", "ListReview", "EditDoctorReview", "Payment", "PaymentFailure", "PaymentSuccess", "UploadCertificate",
                                        "UpdateTrackDetail", "ReviewAndConfirm", "PatientBrochure","PatientBrochureEstimate", "DoctorProfile" , "ListPatient" };

            foreach (string pageName in lstPatientPages)
            {
                p.PageName = pageName;
                p.UserType = UserType.D.ToString();
                lstPage.Add(p);
            }

            #endregion

            SessionHelper.UserPageRights = lstPage;
        }

        private void MenuPageRightsWithRole()
        {
            if ((((CurrentSession)Session["UserLoginSession"]) != null))
            {
                ArrayList arr_list = new ArrayList();
                if (((CurrentSession)Session["UserLoginSession"]).UserType == UserType.P.ToString())
                {
                    arr_list.Add("FindDoctor");
                    arr_list.Add("ForgotPassword");
                    arr_list.Add("PatientLogin");
                    arr_list.Add("AddRecommendedDentist");
                    arr_list.Add("AddClientTestimonial");
                }
                else if (((CurrentSession)Session["UserLoginSession"]).UserType == UserType.D.ToString() || ((CurrentSession)Session["UserLoginSession"]).UserType == UserType.S.ToString())
                {
                    arr_list.Add("AddEditSupplyOrder");
                    arr_list.Add("DoctorLogin");
                    arr_list.Add("ForgotPassword");

                }
                SessionHelper.UserDisplayPageRights = arr_list;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
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

                    // Added By Navik
                    LookupMasterEntity lookupMasterEntity = new LookupMasterEntity();
                    Response.Redirect(lookupMasterEntity.GetStudentCourseSubscribeLinkFromLookUpMaster());

                    // Commented By Navik
                    //Response.Redirect(CommonLogic.GetConfigValue("StudentCourseSubscribe"));
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


    }
}