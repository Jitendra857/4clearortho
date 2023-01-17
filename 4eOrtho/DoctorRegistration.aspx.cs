using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho
{
    public partial class DoctorRegistration : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(DoctorRegistration));
        public long doctorUserId = 0;
        #endregion

        #region Events

        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindCountry();
                    BindSpeciality();
                    //BindDegree();
                    Page.Title = Convert.ToString(this.GetLocalResourceObject("AddTitle"));
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
        /// Event to get state list by country.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindStateByCountry(ddlState, Convert.ToInt64(ddlCountry.SelectedValue));
                txtDoctorPassword.Attributes.Add("value", txtDoctorPassword.Text);
                txtConfirmPassword.Attributes.Add("value", txtConfirmPassword.Text);
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Event to validate emailid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtEmailid_TextChanged(object sender, EventArgs e)
        {
            User user = new UserEntity().GetUserByEmailAddress(txtEmailid.Text.Trim());
            if (user != null)
            {
                CommonHelper.ShowMessage(MessageType.Error, Convert.ToString(this.GetLocalResourceObject("AlreadySignedUp")), divMsg, lblMsg);
                btnSubmit.Enabled = false;
            }
            else
            {
                AllDoctorDetailsByEmailId doctor = new DoctorEntity().GetDoctorDetailsByEmailId(txtEmailid.Text);
                if (doctor != null)
                {
                    CommonHelper.ShowMessage(MessageType.Success, Convert.ToString(this.GetLocalResourceObject("ExistUserMessage")), divMsg, lblMsg);

                    txtFirstName.Text = doctor.FirstName;
                    txtLastName.Text = doctor.LastName;
                    txtMi.Text = doctor.MI;
                    txtTitle.Text = doctor.Title;
                    txtDoctorNo.Text = doctor.DoctorNo;
                    txtDOB.Text = (doctor.DOB != null) ? Convert.ToString(doctor.DOB.Value.ToShortDateString().Replace('-', '/')) : string.Empty;
                    txtSSNNo.Text = doctor.SSNNo;
                    txtStateIDNo.Text = doctor.StateIDNo;
                    txtTINNo.Text = doctor.TinNo;
                    txtMedicalNo.Text = doctor.MedicalNo;
                    txtDrugNo.Text = doctor.DrugIdNo;
                    txtNPINo.Text = doctor.NPINo;
                    rdbblueCross.Checked = (Convert.ToBoolean(doctor.IsCross) == true);
                    rdbblueSchield.Checked = (Convert.ToBoolean(doctor.IsCross) == true);
                    txtCrossOrSchieldValue.Text = doctor.CrossShieldValue;
                    txtProv.Text = doctor.ProvNo;
                    txtOffice.Text = doctor.OfficeNo;
                    txtMedicare.Text = doctor.MedicareNo;
                    txtOtherID.Text = doctor.OtherIdNo;
                    ddlSpecialty.SelectedIndex = ddlSpecialty.Items.IndexOf(ddlSpecialty.Items.FindByText(doctor.Speciality));
                    rbtnMale.Checked = (doctor.Gender == "M");
                    rbtnFemale.Checked = !rbtnMale.Checked;
                    txtCity.Text = doctor.City;
                    txtStreet.Text = doctor.street;
                    txtZip.Text = doctor.zipcode;
                    txtmobile.Text = doctor.Mobile;
                    txtEmailid.Text = doctor.EmailID;
                    //txtConfirmPassword.Text = Cryptography.DecryptStringAES(doctor.Password, CommonLogic.GetConfigValue("sharedSecret"));
                    //txtDoctorPassword.Text = txtConfirmPassword.Text;
                    ddlCountry.SelectedIndex = !string.IsNullOrEmpty(doctor.CountryName) ? ddlCountry.Items.IndexOf(ddlCountry.Items.FindByText(doctor.CountryName)) : 0;
                    if (ddlCountry.SelectedIndex > 0)
                    {
                        BindStateByCountry(ddlState, Convert.ToInt64(ddlCountry.SelectedItem.Value));
                        ddlState.SelectedIndex = !string.IsNullOrEmpty(doctor.StateName) ? ddlState.Items.IndexOf(ddlState.Items.FindByText(doctor.StateName)) : 0;
                    }
                }
                else
                {
                    divMsg.Visible = false;
                    btnSubmit.Enabled = true;
                }
                //divMsg.Visible = false;
                //btnSubmit.Enabled = true;
            }
            txtFirstName.Focus();
        }

        /// <summary>
        /// Event to validated emailid.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void custxtEmailid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                //ContactMasterEntity contactMasterEntity = new ContactMasterEntity();
                DoctorEntity doctorEntity = new DoctorEntity();
                Doctor doctor = doctorEntity.GetDoctorByUserId(doctorUserId);

                //if (doctor != null)
                //    args.IsValid = !(contactMasterEntity.IsEditEmailIDExist(args.Value, (long)doctor.ContactId));
                //else
                //    args.IsValid = !(contactMasterEntity.IsAddEmailIDExist(args.Value));
                UserEntity user = new UserEntity();
                if (doctor != null)
                {
                    User usertable = user.GetUserByEmailAddress(args.Value);
                    if (user.GetExistUserIdByEmailAddressAndID(args.Value, (long)usertable.ID, "D") > 0)
                    {
                        args.IsValid = false;
                    }
                    else
                    {
                        args.IsValid = true;
                    }
                }
                else
                {
                    if (user.GetExistUserIdByEmailAddressAndID(args.Value, 0, "D") > 0)
                    {
                        args.IsValid = false;
                    }
                    else
                    {
                        args.IsValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Event to submit doctor details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SaveDoctor();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                try
                {
                    logger.Info("An error occured.", ex);
                }
                catch (Exception)
                {                    
                    throw;
                }               
            }
        }

        /// <summary>
        /// Event to redirect subscribe 4clearortho cource.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnClickhere_Click(object sender, EventArgs e)
        {
            try
            {
                UserDomainMaster user = null;
                if (Session["user"] != null)
                {
                    user = (UserDomainMaster)Session["user"];

                    ContactMaster contact = new ContactMaster();
                    if (Session["contact"] != null)
                        contact = (ContactMaster)Session["contact"];

                    AddressMaster address = new AddressMaster();
                    if (Session["address"] != null)
                        address = (AddressMaster)Session["address"];

                    Doctor doctor = new Doctor();
                    if (Session["doctor"] != null)
                        doctor = (Doctor)Session["doctor"];

                    Int64 userId = new DoctorEntity().SubscribeAADStudent(user.EMailId, user.Password, doctor.FirstName, doctor.LastName, doctor.MI, doctor.Gender, Convert.ToDateTime(doctor.DOB), string.Empty, contact.HomeContact, contact.Mobile, contact.WorkContact, Convert.ToInt64(address.CountryId), Convert.ToInt64(address.StateId), address.City, address.Street, address.ZipCode);
                    if (userId > 0)
                    {
                        string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("StudentRegistrationTemplate")).ToString();
                        CommonLogic.WelcomeUserMail(user.EMailId, Cryptography.DecryptStringAES(user.Password, CommonLogic.GetConfigValue("sharedSecret")), doctor.FirstName, doctor.LastName, emailTemplatePath, user.EMailId, "4eDental University – Welcome User");

                        Session["doctor"] = null;
                        Session["address"] = null;
                        Session["contact"] = null;
                        Session["user"] = null;

                        // Added By Navik
                        LookupMasterEntity lookupMasterEntity = new LookupMasterEntity();
                        Response.Redirect(lookupMasterEntity.GetStudentCourseSubscribeLinkFromLookUpMaster(), true);

                        // Commented by Navik
                        //Response.Redirect(CommonLogic.GetConfigValue("StudentCourseSubscribe") + "&SId=" + Server.UrlEncode(Cryptography.EncryptStringAES(Convert.ToString(userId), CommonLogic.GetConfigValue("sharedSecret"))), true);
                        
                        
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

        //protected void chkshowpassowrdtextchange(Object sender, EventArgs args)
        //{
        //    if (!string.IsNullOrEmpty(hdPassword.Value))
        //    {
        //        txtDoctorPassword.Text = txtConfirmPassword.Text = hdPassword.Value;
        //        //txtDoctorPassword.Text = Cryptography.DecryptStringAES(userdomainmaster.Password, CommonLogic.GetConfigValue("sharedSecret")).ToString();
        //        //txtConfirmPassword.Text = Cryptography.DecryptStringAES(userdomainmaster.Password, CommonLogic.GetConfigValue("sharedSecret")).ToString();
        //        //hdPassword.Value = Cryptography.DecryptStringAES(userdomainmaster.Password, CommonLogic.GetConfigValue("sharedSecret")).ToString();
        //        chkshowPassword.Visible = true;
        //        if (chkshowPassword.Checked)
        //        {
        //            txtDoctorPassword.TextMode = TextBoxMode.SingleLine;
        //            txtConfirmPassword.TextMode = TextBoxMode.SingleLine;
        //        }
        //        else
        //        {
        //            txtDoctorPassword.TextMode = TextBoxMode.Password;
        //            txtConfirmPassword.TextMode = TextBoxMode.Password;
        //        }
        //    }
        //    else
        //    {
        //        chkshowPassword.Visible = true;
        //        if (chkshowPassword.Checked)
        //        {
        //            txtDoctorPassword.TextMode = TextBoxMode.SingleLine;
        //            txtConfirmPassword.TextMode = TextBoxMode.SingleLine;
        //        }
        //        else
        //        {
        //            txtDoctorPassword.TextMode = TextBoxMode.Password;
        //            txtConfirmPassword.TextMode = TextBoxMode.Password;
        //        }
        //    }
        //}
        #endregion

        #region Helper

        /// <summary>
        /// Method to bind state by country.
        /// </summary>
        /// <param name="ddlState"></param>
        /// <param name="countryId"></param>
        private void BindStateByCountry(DropDownList ddlState, long countryId)
        {
            ddlState.Items.Clear();
            StateEntity stateEntity = new StateEntity();

            List<WSB_State> stateList;
            stateList = stateEntity.GetStateByCountryId(countryId).ToList();

            if (stateList != null && stateList.Count > 0)
            {
                ddlState.Enabled = true;
                ddlState.DataSource = stateList;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateId";
                ddlState.DataBind();
            }
            else
            {
                ddlState.Enabled = false;
            }
            ddlState.Items.Insert(0, new ListItem("Select State", "0"));
        }

        /// <summary>
        /// Method to bind country
        /// </summary>
        private void BindCountry()
        {
            CountryEntity countryEntity = new CountryEntity();
            List<WSB_Country> countryList = countryEntity.GetAllCountry();

            if (doctorUserId == 0)
                countryList = countryList.Where(x => x.IsActive == true).ToList();

            if (countryList != null && countryList.Count > 0)
            {
                ddlCountry.DataSource = countryList;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
            }
            ddlCountry.Items.Insert(0, new ListItem("Select Country", "0"));
        }

        /// <summary>
        /// Method to bind speciality.
        /// </summary>
        private void BindSpeciality()
        {
            LookupMasterEntity lookUpMasterEntity = new LookupMasterEntity();
            List<LookUpDetailsByLookupType> lookUpList = lookUpMasterEntity.GetLookUpDetails("Doctor Specialties");
            if (lookUpList != null && lookUpList.Count > 0)
            {
                ddlSpecialty.DataSource = lookUpList;
                ddlSpecialty.DataTextField = "LookupName";
                ddlSpecialty.DataValueField = "LookupId";
                ddlSpecialty.DataBind();
            }
        }

        /// <summary>
        /// Method to save doctor details.
        /// </summary>
        protected void SaveDoctor()
        {
            /* Account Information */
            chkshowPassword.Visible = false;
            TransactionScope scope = new TransactionScope();
            string oldemailid = txtEmailid.Text.Trim();

            using (scope)
            {
                User orthoUser = new DAL.User();
                UserDomainMaster userDomainMaster = new UserDomainMaster();

                bool shouldSendEmail = false;

                if (doctorUserId > 0)
                {
                    orthoUser = new UserEntity().GetUserByEmailAddress(txtEmailid.Text.Trim());

                    //WSB_UserDomainMaster doctorUserDomain = new DoctorEntity().GetUserDomain("D", "Ortho", txtEmailid.Text);

                    if (txtDoctorPassword.Text.Trim().ToString().Length > 0)
                    {
                        if (!(Cryptography.EncryptStringAES(txtDoctorPassword.Text.Trim().ToString(), CommonLogic.GetConfigValue("sharedSecret")).Equals(orthoUser.Password.ToString())))
                            shouldSendEmail = true;
                    }
                    else if (!(txtEmailid.Text.Trim().ToString().Equals(orthoUser.EmailAddress.ToString())))
                        shouldSendEmail = true;
                    else
                        shouldSendEmail = false;
                    userDomainMaster.IsUpdate = true;
                }
                else
                {
                    userDomainMaster.RoleType = "D";
                    userDomainMaster.EMailId = txtEmailid.Text.Trim();
                    userDomainMaster.IsUpdate = false;
                }
                userDomainMaster.Password = (!string.IsNullOrEmpty(txtDoctorPassword.Text)) ? Cryptography.EncryptStringAES(txtDoctorPassword.Text.Trim(), CommonLogic.GetConfigValue("sharedSecret")) : Cryptography.EncryptStringAES(hdPassword.Value, CommonLogic.GetConfigValue("sharedSecret"));

                DoctorEntity doctorEntity = new DoctorEntity();
                Doctor doctor;
                if (doctorUserId > 0)
                {
                    doctor = doctorEntity.GetDoctorByUserId(doctorUserId);
                    doctor.LastUpdatedBy = Authentication.GetLoggedUserID();
                }
                else
                {
                    doctor = doctorEntity.Create();
                    doctor.CreatedBy = Authentication.GetLoggedUserID();
                    doctor.PhotographName = (rbtnMale.Checked) ? ContentPlaceHolder1_hdMalePhotoName.Value : ContentPlaceHolder1_hdFemalePhotoName.Value;
                    shouldSendEmail = true;
                }

                doctor.FirstName = txtFirstName.Text.Trim();
                doctor.LastName = txtLastName.Text.Trim();
                doctor.MI = txtMi.Text.Trim();
                doctor.Title = txtTitle.Text.Trim();
                doctor.DoctorNo = txtDoctorNo.Text.Trim();

                if (txtDOB.Text != "")
                    doctor.DOB = DateTime.ParseExact(txtDOB.Text.ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                else
                    doctor.DOB = null;

                doctor.SSNNo = txtSSNNo.Text.Trim();
                doctor.StateIDNo = txtStateIDNo.Text.Trim();
                doctor.TinNo = txtTINNo.Text.Trim();
                doctor.MedicalNo = txtMedicalNo.Text.Trim();
                doctor.DrugIdNo = txtDrugNo.Text.Trim();
                doctor.NPINo = txtNPINo.Text.Trim();
                doctor.IsCross = rdbblueCross.Checked;
                doctor.CrossShieldValue = txtCrossOrSchieldValue.Text.Trim();
                doctor.ProvNo = txtProv.Text.Trim();
                doctor.OfficeNo = txtOffice.Text.Trim();
                doctor.MedicareNo = txtMedicare.Text.Trim();
                doctor.OtherIdNo = txtOtherID.Text.Trim();
                doctor.IsActive = true;
                doctor.Gender = rbtnMale.Checked ? "M" : "F";
                doctor.SpecialityId = Convert.ToInt64(ddlSpecialty.SelectedValue);
                //doctor.DegreeId = Convert.ToInt64(ddlDegree.SelectedValue);

                ContactMasterEntity contactMasterEntity = new ContactMasterEntity();
                ContactMaster contact;
                if (doctor.ContactId > 0)
                {
                    contact = contactMasterEntity.GetContactById(doctor.ContactId);
                    contact.LastUpdatedBy = Authentication.GetLoggedUserID();
                }
                else
                {
                    contact = contactMasterEntity.Create();
                    contact.CreatedBy = Authentication.GetLoggedUserID();
                }

                contact.Mobile = txtmobile.Text.Trim();
                contact.HomeContact = txthome.Text.Trim();
                contact.WorkContact = txtwork.Text.Trim();
                contact.EmailID = txtEmailid.Text.Trim();
                contact.CreatedBy = Authentication.GetLoggedUserID();
                contact.IsActive = true;
                long contactId = contactMasterEntity.Save(contact);
                doctor.ContactId = contactId;
                Session["contact"] = contact;

                AddressMasterEntity addressMasterEntity = new AddressMasterEntity();
                AddressMaster address;
                if (doctor.AddressId > 0)
                {
                    address = addressMasterEntity.GetAddressbyId(doctor.AddressId);
                    address.LastUpdatedBy = Authentication.GetLoggedUserID();
                }
                else
                {
                    address = addressMasterEntity.Create();
                    address.CreatedBy = Authentication.GetLoggedUserID();
                }

                if (ddlCountry.SelectedIndex > 0)
                    address.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);

                if (ddlState.SelectedIndex > 0)
                    address.StateId = Convert.ToInt64(ddlState.SelectedValue);

                address.City = txtCity.Text.Trim();
                address.Street = txtStreet.Text.Trim();
                address.ZipCode = txtZip.Text.Trim();
                address.CreatedBy = Authentication.GetLoggedUserID();
                address.IsActive = true;
                long addressId = addressMasterEntity.Save(address);
                Session["address"] = address;

                doctor.AddressId = addressId;
                long doctorid = doctorEntity.Save(doctor);
                Session["doctor"] = doctor;

                userDomainMaster.DomainURL = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/'));
                userDomainMaster.UserID = userDomainMaster.SourceId = doctorid;
                userDomainMaster.SourceType = "Ortho";
                userDomainMaster.DatabaseName = "4ClearOrtho";
                //new PatientEntity().InsertToUserDomainMaster(userDomainMaster);
                Session["user"] = userDomainMaster;

                orthoUser = new UserEntity().Create();
                orthoUser.CreatedDate = DateTime.Now;
                orthoUser.EmailAddress = userDomainMaster.EMailId;
                orthoUser.FirstName = doctor.FirstName;
                orthoUser.IsActive = true;
                orthoUser.IsSuperAdmin = false;
                orthoUser.LastName = doctor.LastName;
                orthoUser.Password = userDomainMaster.Password;
                orthoUser.UpdatedDate = DateTime.Now;
                orthoUser.UserType = "D";

                new UserEntity().Save(orthoUser);

                if (doctor.DoctorId.Equals(Authentication.GetLoggedUserID()))
                    CommonLogic.UpdateSessionValues(doctor.FirstName, doctor.LastName);

                if (shouldSendEmail)
                {
                    if (doctorUserId > 0)
                        SendWelcomeUserEmail(txtEmailid.Text.Trim(), Cryptography.DecryptStringAES(userDomainMaster.Password, CommonLogic.GetConfigValue("sharedSecret")), txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmailid.Text.Trim(), "ResetPassword");
                    else
                        SendWelcomeUserEmail(txtEmailid.Text.Trim(), Cryptography.DecryptStringAES(userDomainMaster.Password, CommonLogic.GetConfigValue("sharedSecret")), txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmailid.Text.Trim(), "UserRegistration");

                    CurrentSession currentSession = new CurrentSession();
                    currentSession.EmailId = txtEmailid.Text.Trim();
                    currentSession.Password = userDomainMaster.Password;
                    currentSession.UserType = "D";
                    currentSession.DomainURL = "http://4clearortho.com/";
                    currentSession.DatabaseName = "4ClearOrtho";
                    currentSession.DoctorName = txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim();
                    currentSession.DoctorFirstName = txtFirstName.Text.Trim();
                    currentSession.DoctorLastName = txtLastName.Text.Trim();
                    currentSession.DoctorMobile = txtmobile.Text.Trim();
                    currentSession.DoctorState = ddlState.SelectedItem.Text;
                    currentSession.DoctorStreet = txtStreet.Text.Trim();
                    currentSession.DoctorCountry = ddlCountry.SelectedItem.Text;
                    currentSession.DoctorCity = txtCity.Text.Trim();
                    currentSession.DoctorZipcode = txtZip.Text.Trim();
                    currentSession.IsCertified = false;
                    currentSession.Gender = doctor.Gender;
                    currentSession.IsCertified = false;
                    currentSession.SourceType = "Ortho";
                    currentSession.Gender = rbtnMale.Checked ? "M" : "F";
                    currentSession.DOB = txtDOB.Text.Trim();
                    currentSession.HomeContact = txthome.Text.Trim();
                    currentSession.MI = txtMi.Text.Trim();
                    currentSession.StateId = Convert.ToInt64(ddlState.SelectedItem.Value);
                    currentSession.WorkContact = txtwork.Text.Trim();
                    currentSession.CountryId = Convert.ToInt64(ddlCountry.SelectedItem.Value);
                    currentSession.IsPayment = false;
                    currentSession.IsAccActive = false;
                    Session["UserLoginSession"] = currentSession;
                    SessionHelper.LoggedUserType = "D";
                    //CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("successmessage").ToString(), divMsg, lblMsg);
                    //lbtnClickhere.Visible = true;
                    //lblBecomecerty.Visible = true;
                    //ClearControl();
                }
                else
                    CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("saveError").ToString(), divMsg, lblMsg);

                scope.Complete();
                Response.Redirect("Payment.aspx");
            }
            //Response.Redirect("Doctorlogin.aspx");
        }

        /// <summary>
        /// Method to clear control.
        /// </summary>
        private void ClearControl()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtMi.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtDoctorNo.Text = string.Empty;
            txtDOB.Text = string.Empty;
            txtSSNNo.Text = string.Empty;
            txtStateIDNo.Text = string.Empty;
            txtTINNo.Text = string.Empty;
            txtMedicalNo.Text = string.Empty;
            txtDrugNo.Text = string.Empty;
            txtNPINo.Text = string.Empty;
            txtCrossOrSchieldValue.Text = string.Empty;
            txtProv.Text = string.Empty;
            txtOffice.Text = string.Empty;
            txtMedicare.Text = string.Empty;
            txtOtherID.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtStreet.Text = string.Empty;
            txtZip.Text = string.Empty;
            txtmobile.Text = string.Empty;
            txthome.Text = string.Empty;
            txtwork.Text = string.Empty;
            txtEmailid.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtDoctorPassword.Text = string.Empty;
            ddlCountry.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
        }

        /// <summary>
        /// Method to send welcome mail to doctor.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailId"></param>
        /// <param name="TemplateType"></param>
        private void SendWelcomeUserEmail(string userName, string password, string firstName, string lastName, string emailId, string TemplateType)
        {
            try
            {
                string emailTemplatePath = String.Empty;
                if (TemplateType.Equals("ResetPassword"))
                {
                    emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("ResetPassword")).ToString();
                    CommonLogic.RegisterUserEmail(userName, password, firstName, lastName, emailTemplatePath, emailId, "Doctor", "EditUser");
                    CommonHelper.SaveRegisterUserEmailLog(userName, password, firstName, lastName, "Doctor", CommonLogic.GetConfigValue("FromNoReplyEmail"), emailId, null, null, emailTemplatePath, "4Clear Ortho – Login Credentials Changed");
                }
                else if (TemplateType.Equals("UserRegistration"))
                {
                    emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("UserRegistraion")).ToString();
                    CommonLogic.RegisterUserEmail(userName, password, firstName, lastName, emailTemplatePath, emailId, "Doctor", "Registration");
                    CommonHelper.SaveRegisterUserEmailLog(userName, password, firstName, lastName, "Doctor", CommonLogic.GetConfigValue("FromNoReplyEmail"), emailId, null, null, emailTemplatePath, "4Clear Ortho – Welcome User");
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion
    }
}