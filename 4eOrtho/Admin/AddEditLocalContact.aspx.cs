using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho.Admin
{
    public partial class AddEditLocalContact : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(AddEditLocalContact));

        public long Id
        {
            get
            {
                try
                {
                    return (!String.IsNullOrEmpty(CommonLogic.QueryString("id"))) ? Convert.ToInt32(Cryptography.DecryptStringAES(CommonLogic.QueryString("id"), CommonLogic.GetConfigValue("sharedSecret"))) : 0;
                }
                catch
                {
                    CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("URLHampered").ToString(), divMsg, lblMsg);
                    return 0;
                }
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindCountry();

                    if (Id > 0)
                    {
                        lblHeaderEdit.Visible = true;
                        lblHeader.Visible = false;
                        FillLocalContactDetail();
                        //Page.Title = Convert.ToString(this.GetLocalResourceObject("EditTitle"));
                    }
                    else
                    {
                        ddlState.Enabled = false;
                        //Page.Title = Convert.ToString(this.GetLocalResourceObject("AddTitle"));
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

        private void FillLocalContactDetail()
        {
            LocalContact localContact = new LocalContactEntity().GetLocalContactById(Id);

            if (localContact != null)
            {
                AddressMaster addressMaster = new AddressMasterEntity().GetAddressbyId(localContact.AddressId);
                ContactMaster contactMaster = new ContactMasterEntity().GetContactById(localContact.ContactId);

                txtEmailAddress.Text = contactMaster.EmailID;
                txtOrganisationName.Text = localContact.OrganizationName;
                txtFirstName.Text = localContact.FirstName;
                txtLastName.Text = localContact.LastName;
                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(Convert.ToString(addressMaster.CountryId)));
                BindStateByCountry(ddlState, Convert.ToInt64(ddlCountry.SelectedValue));
                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(Convert.ToString(addressMaster.StateId)));
                txtCity.Text = addressMaster.City;
                txtStreet.Text = addressMaster.Street;
                txtZip.Text = addressMaster.ZipCode;
                txtmobile.Text = contactMaster.Mobile;
                txthome.Text = contactMaster.HomeContact;
                txtwork.Text = contactMaster.WorkContact;
            }
        }

        protected void cstmtxtEmailAddress_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Id > 0)
                args.IsValid = (new LocalContactEntity().IsEditEmailValid(txtEmailAddress.Text.Trim(), Id));
            else
                args.IsValid = (new LocalContactEntity().IsAddEmailValid(txtEmailAddress.Text.Trim()));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    TransactionScope scope = new TransactionScope();
                    using (scope)
                    {
                        bool shouldSendEmail = false;
                        LocalContact localContactUser = null;
                        AddressMaster addressMaster = null;
                        ContactMaster contactMaster = null;
                        User user = null;
                        string userAutoPassword = string.Empty;

                        if (Id > 0)
                        {
                            localContactUser = new LocalContactEntity().GetLocalContactById(Id);

                            if (localContactUser != null)
                            {
                                addressMaster = localContactUser.AddressMaster;
                                contactMaster = localContactUser.ContactMaster;
                                user = localContactUser.User; 
                                localContactUser.LastUpdatedBy = localContactUser.ContactMaster.LastUpdatedBy = localContactUser.AddressMaster.LastUpdatedBy = Authentication.GetLoggedUserID();
                            }
                        }
                        else
                        {
                            localContactUser = new LocalContactEntity().Create();
                            localContactUser.AddressMaster = new AddressMasterEntity().Create();
                            localContactUser.ContactMaster = new ContactMasterEntity().Create();
                            localContactUser.User = new UserEntity().Create();

                            userAutoPassword = Guid.NewGuid().ToString().Substring(0, 6);
                            localContactUser.User.Password = Cryptography.EncryptStringAES(userAutoPassword, CommonLogic.GetConfigValue("SharedSecret"));
                            localContactUser.User.IsActive = true;
                            localContactUser.CreatedBy = localContactUser.ContactMaster.CreatedBy = localContactUser.AddressMaster.CreatedBy = SessionHelper.LoggedAdminUserID;
                            shouldSendEmail = true;
                            localContactUser.IsActive = true;
                        }

                        localContactUser.AddressMaster.City = txtCity.Text.Trim();
                        localContactUser.AddressMaster.CountryId = Convert.ToInt64(ddlCountry.SelectedItem.Value);
                        localContactUser.AddressMaster.IsActive = true;
                        localContactUser.AddressMaster.IsDelete = false;
                        localContactUser.AddressMaster.StateId = Convert.ToInt64(ddlState.SelectedItem.Value);
                        localContactUser.AddressMaster.Street = txtStreet.Text.Trim();
                        localContactUser.AddressMaster.ZipCode = txtZip.Text.Trim();                        

                        localContactUser.ContactMaster.EmailID = txtEmailAddress.Text.Trim();
                        localContactUser.ContactMaster.HomeContact = txthome.Text.Trim();
                        localContactUser.ContactMaster.IsActive = true;
                        localContactUser.ContactMaster.IsDelete = false;
                        localContactUser.ContactMaster.Mobile = txtmobile.Text.Trim();
                        localContactUser.ContactMaster.WorkContact = txtwork.Text.Trim();                        

                        localContactUser.User.EmailAddress = txtEmailAddress.Text.Trim();
                        localContactUser.User.FirstName = txtFirstName.Text.Trim();
                        localContactUser.User.LastName = txtLastName.Text.Trim();
                        localContactUser.User.UserType = UserType.LC.ToString().Trim();
                        
                        localContactUser.FirstName = txtFirstName.Text.Trim();
                        localContactUser.LastName = txtLastName.Text.Trim();
                        localContactUser.OrganizationName = txtOrganisationName.Text.Trim();
                        new LocalContactEntity().Save(localContactUser);

                        scope.Complete();

                        if (shouldSendEmail)
                        {
                            string welcomeEmailTemplatePath = Server.MapPath("\\" + CommonLogic.GetConfigValue("UserRegistraion"));
                            CommonLogic.RegisterUserEmail(localContactUser.User.UserName, userAutoPassword, localContactUser.User.FirstName, localContactUser.User.LastName, welcomeEmailTemplatePath, localContactUser.User.EmailAddress, "Local Contact", "Registration");
                        }
                    }
                    Response.Redirect("ListLocalContact.aspx", false);
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindStateByCountry(ddlState, Convert.ToInt64(ddlCountry.SelectedValue));
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Fill Country
        /// </summary>
        private void BindCountry()
        {
            CountryEntity countryEntity = new CountryEntity();
            List<WSB_Country> countryList = countryEntity.GetAllCountry();

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
        /// Fill State according to country
        /// </summary>
        /// <param name="ddlState"></param>
        /// <param name="countryId"></param>
        private void BindStateByCountry(DropDownList ddlState, long countryId)
        {
            ddlState.Items.Clear();
            StateEntity stateEntity = new StateEntity();
            List<WSB_State> stateList = stateEntity.GetStateByCountryId(countryId).ToList();

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
            ddlState.Focus();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtEmailAddress.Text = string.Empty;
            txtOrganisationName.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            ddlCountry.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
            txtCity.Text = string.Empty;
            txtStreet.Text = string.Empty;
            txtZip.Text = string.Empty;
            txtmobile.Text = string.Empty;
            txthome.Text = string.Empty;
            txtwork.Text = string.Empty;
        }
    }
}