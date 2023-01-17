using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Utility;
using _4eOrtho.Helper;
using log4net;
using System.Transactions;
using System.IO;
using System.Threading;

namespace _4eOrtho.Admin
{
    public partial class AddEditDoctor : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditDoctor));

        public long doctorUserId
        {
            get
            {
                try
                {
                    if (!String.IsNullOrEmpty(CommonLogic.QueryString("id")))
                    {
                        return Convert.ToInt32(Cryptography.DecryptStringAES(CommonLogic.QueryString("id"), CommonLogic.GetConfigValue("sharedSecret")));
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch
                {
                    CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("URLHampered").ToString(), divMsg, lblMsg);
                    return 0;
                }
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// Page Load event
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

                    if (doctorUserId > 0)
                    {
                        spanPassword.Visible = false;
                        spanConfirmPassword.Visible = false;
                        lblHeaderEdit.Visible = true;
                        lblHeader.Visible = false;
                        txtDoctorNo.Enabled = false;
                        rqvtxtPassword.Enabled = false;
                        rqvtxtConfirmPassword.Enabled = false;
                        FillDoctorInfo(doctorUserId);
                        Page.Title = Convert.ToString(this.GetLocalResourceObject("EditTitle"));
                    }
                    else
                    {
                        spanPassword.Visible = true;
                        spanConfirmPassword.Visible = true;
                        cvConfirmPassword.Enabled = false;
                        txtDoctorNo.Enabled = true;
                        ddlState.Enabled = false;
                        btnBack.Visible = false;
                        Page.Title = Convert.ToString(this.GetLocalResourceObject("AddTitle"));
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
        /// Bind State list when selected Country changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindStateByCountry(ddlState, Convert.ToInt64(ddlCountry.SelectedValue));
                txtPassword.Attributes.Add("value", txtPassword.Text);
                txtConfirmPassword.Attributes.Add("value", txtConfirmPassword.Text);
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Show Password when checkbox checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void chkshowpassowrdtextchange(Object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(hdPassword.Value))
            {
                txtPassword.Text = txtConfirmPassword.Text = hdPassword.Value;
                //txtPassword.Text = Cryptography.DecryptStringAES(userdomainmaster.Password, CommonLogic.GetConfigValue("sharedSecret")).ToString();
                //txtConfirmPassword.Text = Cryptography.DecryptStringAES(userdomainmaster.Password, CommonLogic.GetConfigValue("sharedSecret")).ToString();
                //hdPassword.Value = Cryptography.DecryptStringAES(userdomainmaster.Password, CommonLogic.GetConfigValue("sharedSecret")).ToString();
                chkshowPassword.Visible = true;
                if (chkshowPassword.Checked)
                {
                    txtPassword.TextMode = TextBoxMode.SingleLine;
                    txtConfirmPassword.TextMode = TextBoxMode.SingleLine;
                }
                else
                {
                    txtPassword.TextMode = TextBoxMode.Password;
                    txtConfirmPassword.TextMode = TextBoxMode.Password;
                }
            }
            else
            {
                chkshowPassword.Visible = true;
                if (chkshowPassword.Checked)
                {
                    txtPassword.TextMode = TextBoxMode.SingleLine;
                    txtConfirmPassword.TextMode = TextBoxMode.SingleLine;
                }
                else
                {
                    txtPassword.TextMode = TextBoxMode.Password;
                    txtConfirmPassword.TextMode = TextBoxMode.Password;
                }
            }
        }

        /// <summary>
        /// Check email id is exist or not
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
                    CommonHelper.ShowMessage(MessageType.Error, Convert.ToString(this.GetLocalResourceObject("ExistUserMessage")), divMsg, lblMsg);

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
                        ddlState.SelectedIndex = !string.IsNullOrEmpty(doctor.CountryName) ? ddlCountry.Items.IndexOf(ddlCountry.Items.FindByText(doctor.CountryName)) : 0;
                    }
                }
                else
                {
                    divMsg.Visible = false;
                    btnSubmit.Enabled = true;
                }
            }
            txtFirstName.Focus();
        }

        /// <summary>
        /// Check email id is exist or not
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
                //{
                //    if (contactMasterEntity.IsEditEmailIDExist(args.Value, (long)doctor.ContactId))
                //    {
                //        args.IsValid = false;
                //    }
                //    else
                //    {
                //        args.IsValid = true;
                //    }
                //}
                //else
                //{
                //    if (contactMasterEntity.IsAddEmailIDExist(args.Value))
                //    {
                //        args.IsValid = false;
                //    }
                //    else
                //    {
                //        args.IsValid = true;
                //    }
                //}

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
        /// Save doctor information
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
                logger.Error("An error occured.", ex);
            }
        }

        #endregion

        #region Helper

        /// <summary>
        /// Fill Doctor Information
        /// </summary>
        /// <param name="doctorId"></param>
        private void FillDoctorInfo(long doctorId)
        {
            DoctorEntity doctorEntity = new DoctorEntity();
            Doctor doctor = doctorEntity.GetDoctorByUserId(doctorId);

            if (doctor != null)
            {
                hdDoctorId.Value = Convert.ToString(doctor.DoctorId);
                imgPatientPhoto.ImageUrl = !string.IsNullOrEmpty(doctor.PhotographName) ? "Photograph/" + doctor.PhotographName : "Photograph/no-photo.jpg";
                if (doctor.Gender == "M")
                {
                    imgPatientPhoto.ImageUrl = !string.IsNullOrEmpty(doctor.PhotographName) ? "Photograph/" + doctor.PhotographName : "Photograph/male_sm.jpg";
                    hdMalePhotoName.Value = !string.IsNullOrEmpty(doctor.PhotographName) ? doctor.PhotographName : "male_sm.jpg";
                }
                else
                {
                    imgPatientPhoto.ImageUrl = !string.IsNullOrEmpty(doctor.PhotographName) ? "Photograph/" + doctor.PhotographName : "Photograph/female_sm.jpg";
                    hdFemalePhotoName.Value = !string.IsNullOrEmpty(doctor.PhotographName) ? doctor.PhotographName : "female_sm.jpg";
                }

                //hdPhotoName.Value = !string.IsNullOrEmpty(doctor.PhotographName) ? doctor.PhotographName : "no-photo.jpg";
                txtFirstName.Text = doctor.FirstName;
                txtLastName.Text = doctor.LastName;
                txtMi.Text = doctor.MI;
                txtTitle.Text = doctor.Title;
                txtDoctorNo.Text = doctor.DoctorNo;

                if (doctor.DOB != null)
                {
                    txtDOB.Text = Convert.ToString(doctor.DOB.Value.ToShortDateString().Replace('-', '/'));
                }
                else
                {
                    txtDOB.Text = "";
                }

                txtSSNNo.Text = doctor.SSNNo;
                txtStateIDNo.Text = doctor.StateIDNo;
                txtTINNo.Text = doctor.TinNo;
                txtMedicalNo.Text = doctor.MedicalNo;
                txtDrugNo.Text = doctor.DrugIdNo;
                txtNPINo.Text = doctor.NPINo;

                if (doctor.IsCross == true)
                {
                    rdbblueCross.Checked = true;
                }
                else
                {
                    rdbblueSchield.Checked = true;
                }

                txtCrossOrSchieldValue.Text = doctor.CrossShieldValue;
                txtProv.Text = doctor.ProvNo;
                txtOffice.Text = doctor.OfficeNo;
                txtMedicare.Text = doctor.MedicareNo;
                txtOtherID.Text = doctor.OtherIdNo;
                chkIsActive.Checked = doctor.IsActive;

                //added
                //ddlDegree.SelectedValue = doctor.DegreeId.ToString();
                if (doctor.Gender == "M")
                    rbtnMale.Checked = true;
                else if (doctor.Gender == "F")
                    rbtnFemale.Checked = true;

                AddressMasterEntity addressMasterEntity = new AddressMasterEntity();
                AddressMaster address = addressMasterEntity.GetAddressbyId(doctor.AddressId);

                if (address.CountryId > 0)
                {
                    ddlCountry.SelectedValue = Convert.ToString(address.CountryId);
                    BindStateByCountry(ddlState, (long)address.CountryId);
                    ddlState.SelectedValue = Convert.ToString(address.StateId);
                }
                else
                {
                    ddlState.Enabled = false;
                }

                txtCity.Text = address.City;
                txtStreet.Text = address.Street;
                txtZip.Text = address.ZipCode;

                ContactMasterEntity contactMasterEntity = new ContactMasterEntity();
                ContactMaster contact = contactMasterEntity.GetContactById(doctor.ContactId);
                txtmobile.Text = contact.Mobile;
                txthome.Text = contact.HomeContact;
                txtwork.Text = contact.WorkContact;
                txtEmailid.Text = contact.EmailID;

                User user = new UserEntity().GetUserByEmailAddress(contact.EmailID);
                if (user != null)
                {
                    hdPassword.Value = Cryptography.DecryptStringAES(user.Password, CommonLogic.GetConfigValue("sharedSecret"));
                }

                //WSB_UserDomainMaster doctorUserDomain = new DoctorEntity().GetUserDomain("D", "Ortho", txtEmailid.Text);
                //if (doctorUserDomain != null)
                //{
                //    hdPassword.Value = Cryptography.DecryptStringAES(doctorUserDomain.Password, CommonLogic.GetConfigValue("sharedSecret"));
                //}
            }
            else
            {
                CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("URLhampered").ToString(), divMsg, lblMsg);
                btnSubmit.Enabled = false;
            }
        }

        /// <summary>
        /// Fill Country
        /// </summary>
        private void BindCountry()
        {
            CountryEntity countryEntity = new CountryEntity();
            List<WSB_Country> countryList = countryEntity.GetAllCountry();
            if (doctorUserId == 0)
            {
                countryList = countryList.Where(x => x.IsActive == true).ToList();
            }
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
        /// Fill Speciality
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
        /// Fill State according to country
        /// </summary>
        /// <param name="ddlState"></param>
        /// <param name="countryId"></param>
        private void BindStateByCountry(DropDownList ddlState, long countryId)
        {
            ddlState.Items.Clear();
            StateEntity stateEntity = new StateEntity();

            List<WSB_State> stateList;
            if (doctorUserId == 0)
            {
                stateList = stateEntity.GetStateByCountryId(countryId).ToList();
            }
            else
            {
                stateList = stateEntity.GetStateByCountryId(countryId).ToList();
            }
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
        /// Save the doctor record
        /// </summary>
        protected void SaveDoctor()
        {
            try
            {
                /* Account Information */
                chkshowPassword.Visible = false;

                TransactionOptions transactionOptions = new TransactionOptions();
                transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
                transactionOptions.Timeout = TimeSpan.MaxValue;
                TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions);

                string oldemailid = txtEmailid.Text.Trim();

                using (scope)
                {
                    //UserDomainMaster user = new UserDomainMaster();
                    User orthoUser;
                    //EMR_User user;                

                    bool shouldSendEmail = false;

                    if (doctorUserId > 0)
                    {
                        orthoUser = new UserEntity().GetUserByEmailAddress(txtEmailid.Text.Trim());

                        //WSB_UserDomainMaster doctorUserDomain = new DoctorEntity().GetUserDomain("D", "Ortho", txtEmailid.Text);

                        if (txtPassword.Text.Trim().ToString().Length > 0)
                        {
                            if (!(Cryptography.EncryptStringAES(txtPassword.Text.Trim().ToString(), CommonLogic.GetConfigValue("sharedSecret")).Equals(orthoUser.Password.ToString())))
                            {
                                shouldSendEmail = true;
                            }
                        }
                        else if (!(txtEmailid.Text.Trim().ToString().Equals(orthoUser.EmailAddress.ToString())))
                            shouldSendEmail = true;
                        else
                            shouldSendEmail = false;
                        //user.IsUpdate = true;
                    }
                    else
                    {
                        orthoUser = new UserEntity().Create();
                        orthoUser.CreatedDate = DateTime.Now;
                        orthoUser.EmailAddress = txtEmailid.Text.Trim();
                        orthoUser.UserType = "D";
                        //user.RoleType = "D";
                        //user.EMailId = txtEmailid.Text.Trim();
                        //user.IsUpdate = false;
                        //user = userEntity.Create();
                        //user.CreatedBy = Authentication.GetLoggedUserID();
                    }

                    if (!string.IsNullOrEmpty(txtPassword.Text))
                    {
                        orthoUser.Password = Cryptography.EncryptStringAES(txtPassword.Text.Trim(), CommonLogic.GetConfigValue("sharedSecret"));
                    }
                    else
                    {
                        orthoUser.Password = Cryptography.EncryptStringAES(hdPassword.Value, CommonLogic.GetConfigValue("sharedSecret"));
                    }

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
                        if (rbtnMale.Checked)
                            doctor.PhotographName = hdMalePhotoName.Value;
                        else
                            doctor.PhotographName = hdFemalePhotoName.Value;
                        shouldSendEmail = true;
                    }

                    doctor.FirstName = txtFirstName.Text.Trim();
                    doctor.LastName = txtLastName.Text.Trim();
                    doctor.MI = txtMi.Text.Trim();
                    doctor.Title = txtTitle.Text.Trim();
                    doctor.DoctorNo = txtDoctorNo.Text.Trim();

                    if (txtDOB.Text != "")
                    {
                        doctor.DOB = DateTime.ParseExact(txtDOB.Text.ToString(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        doctor.DOB = null;
                    }

                    doctor.SSNNo = txtSSNNo.Text.Trim();
                    doctor.StateIDNo = txtStateIDNo.Text.Trim();
                    doctor.TinNo = txtTINNo.Text.Trim();
                    doctor.MedicalNo = txtMedicalNo.Text.Trim();
                    doctor.DrugIdNo = txtDrugNo.Text.Trim();
                    doctor.NPINo = txtNPINo.Text.Trim();

                    if (rdbblueCross.Checked)
                    {
                        doctor.IsCross = true;
                    }
                    else
                    {
                        doctor.IsCross = false;
                    }

                    doctor.CrossShieldValue = txtCrossOrSchieldValue.Text.Trim();
                    doctor.ProvNo = txtProv.Text.Trim();
                    doctor.OfficeNo = txtOffice.Text.Trim();
                    doctor.MedicareNo = txtMedicare.Text.Trim();
                    doctor.OtherIdNo = txtOtherID.Text.Trim();
                    doctor.IsActive = chkIsActive.Checked;

                    if (rbtnMale.Checked)
                        doctor.Gender = "M";
                    else
                        doctor.Gender = "F";
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
                    contact.IsActive = chkIsActive.Checked;
                    long contactId = contactMasterEntity.Save(contact);
                    doctor.ContactId = contactId;

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
                    {
                        address.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                    }

                    if (ddlState.SelectedIndex > 0)
                    {
                        address.StateId = Convert.ToInt64(ddlState.SelectedValue);
                    }
                    address.City = txtCity.Text.Trim();
                    address.Street = txtStreet.Text.Trim();
                    address.ZipCode = txtZip.Text.Trim();
                    address.CreatedBy = Authentication.GetLoggedUserID();
                    address.IsActive = chkIsActive.Checked;
                    long addressId = addressMasterEntity.Save(address);
                    doctor.AddressId = addressId;
                    long doctorid = doctorEntity.Save(doctor);

                    orthoUser.FirstName = txtFirstName.Text.Trim();
                    orthoUser.IsActive = true;
                    orthoUser.IsSuperAdmin = false;
                    orthoUser.LastName = txtLastName.Text.Trim();
                    orthoUser.UpdatedDate = DateTime.Now;
                    new UserEntity().Save(orthoUser);
                    //user.DomainURL = Request.Url.ToString().Replace(Request.RawUrl, string.Empty);                
                    //user.UserID = user.SourceId = doctorid;
                    //user.SourceType = "Ortho";
                    //user.DatabaseName = "4ClearOrtho";
                    //new PatientEntity().InsertToUserDomainMaster(user);

                    if (doctor.DoctorId.Equals(Authentication.GetLoggedUserID()))
                    {
                        CommonLogic.UpdateSessionValues(doctor.FirstName, doctor.LastName);
                    }

                    logger.Info("Before Send Mail");

                    if (shouldSendEmail)
                    {
                        if (doctorUserId > 0)
                        {
                            SendWelcomeUserEmail(txtEmailid.Text.Trim(), Cryptography.DecryptStringAES(orthoUser.Password, CommonLogic.GetConfigValue("sharedSecret")), txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmailid.Text.Trim(), "ResetPassword");
                        }
                        else
                        {
                            SendWelcomeUserEmail(txtEmailid.Text.Trim(), Cryptography.DecryptStringAES(orthoUser.Password, CommonLogic.GetConfigValue("sharedSecret")), txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmailid.Text.Trim(), "UserRegistration");
                        }
                    }
                    logger.Info("after Send Mail");
                    scope.Complete();
                    logger.Info("after scope complete");
                }
                Response.Redirect("ListDoctors.aspx");
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// send mail if either username or password is changed
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