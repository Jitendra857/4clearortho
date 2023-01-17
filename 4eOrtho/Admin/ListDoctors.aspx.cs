using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using _4eOrtho.Utility;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using System.IO;
using System.Net.Mail;

namespace _4eOrtho.Admin
{
    public partial class ListDoctors : PageBase
    {
        #region Declaration        
        private ILog logger = log4net.LogManager.GetLogger(typeof(ListDoctors));
        #endregion Declaration

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["SortBy"] = "CreatedDate";
                    ViewState["AscDesc"] = "DESC";
                }
                lvCertifiedDoctor.Visible = rbtnCertified.Checked;
                lvNonCertifiedDoctor.Visible = !rbtnCertified.Checked;
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
        /// Certified doctor object data resource selecting events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsCertifiedDoctor_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                e.InputParameters["SearchField"] = ddlSearchBy.SelectedValue;
                e.InputParameters["SearchValue"] = txtSearchVal.Text.Trim();
                e.InputParameters["patientEmail"] = "";
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Get Non Certified doctor object resource events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsNonCertifiedDoctor_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["sortField"] = ViewState["SortBy"].ToString();
                e.InputParameters["sortDirection"] = ViewState["AscDesc"].ToString();
                e.InputParameters["SearchField"] = ddlSearchBy.SelectedValue;
                e.InputParameters["SearchValue"] = txtSearchVal.Text.Trim();
                e.InputParameters["patientEmail"] = "";
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void rbtnCertified_CheckedChanged(object sender, EventArgs e)
        {
            lvNonCertifiedDoctor.Visible = false;
            lvCertifiedDoctor.Visible = true;
        }

        protected void rbtnNonCertified_CheckedChanged(object sender, EventArgs e)
        {
            lvCertifiedDoctor.Visible = false;
            lvNonCertifiedDoctor.Visible = true;
        }

        /// <summary>
        /// for sorting and deletion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Custom_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToUpper() == "CUSTOMSORT")
                {
                    if (ViewState["AscDesc"] == null || ViewState["AscDesc"].ToString() == "")
                        ViewState["AscDesc"] = "DESC";
                    else
                    {
                        if (ViewState["AscDesc"].ToString() == "ASC")
                            ViewState["AscDesc"] = "DESC";
                        else
                            ViewState["AscDesc"] = "ASC";
                    }
                    ViewState["SortBy"] = e.CommandArgument;
                }
                else if (e.CommandName.ToUpper() == "AUTOLOGIN")
                {
                    string LoginUrl = Server.UrlDecode(Request.Url.ToString()).Replace(Server.UrlDecode(Request.Url.PathAndQuery), "") + "/doctorlogin.aspx";
                    SessionHelper.LoggedUserEmailAddress = Convert.ToString(e.CommandArgument);
                    SessionHelper.IsAbleToNavigate = true;
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "OpenWindow", "window.open('" + LoginUrl + "');", true);
                }
                else if (e.CommandName.ToUpper() == "CUSTOMACTIVE")
                {
                    string emailId = Convert.ToString(e.CommandArgument).Split(',')[0];
                    string lastName = Convert.ToString(e.CommandArgument).Split(',')[1];

                    UserConfigEntity userConfigEntity = new UserConfigEntity();
                    UserConfig userConfig = userConfigEntity.GetUserByEmailAddress(emailId);
                    if (userConfig != null)
                    {
                        userConfig.IsAccountActivated = !userConfig.IsAccountActivated;
                        userConfig.UpdatedDate = DateTime.Now;
                        userConfig.UpdatedBy = SessionHelper.LoggedAdminUserID;
                    }
                    else
                    {
                        userConfig = new UserConfig();
                        userConfig.IsAccountActivated = true;
                        userConfig.CreatedBy = userConfig.UpdatedBy = SessionHelper.LoggedAdminUserID;
                        userConfig.CreatedDate = userConfig.UpdatedDate = DateTime.Now;
                        userConfig.EmailId = Convert.ToString(emailId);
                    }
                    userConfigEntity.Save(userConfig);
                    SendAccountActiveMail(userConfig.IsAccountActivated, emailId, lastName);
                }
                if (rbtnCertified.Checked)
                    lvCertifiedDoctor.DataBind();
                else
                    lvNonCertifiedDoctor.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// update image on basis of ascending/descending sorting option
        /// </summary>
        private void SetSortImage()
        {
            try
            {
                ListView lvDoctors = new ListView();
                lvDoctors = rbtnCertified.Checked ? lvCertifiedDoctor : lvNonCertifiedDoctor;

                (lvDoctors.FindControl("lnkSortFirstName") as LinkButton).Attributes.Add("class", "");
                (lvDoctors.FindControl("lnkSortEmailId") as LinkButton).Attributes.Add("class", "");
                (lvDoctors.FindControl("lnkSortCountry") as LinkButton).Attributes.Add("class", "");
                (lvDoctors.FindControl("lnkSortCity") as LinkButton).Attributes.Add("class", "");
                //(lvDoctors.FindControl("lnkIsAccActive") as LinkButton).Attributes.Add("class", "");
                //(lvDoctors.FindControl("lnkIsPayment") as LinkButton).Attributes.Add("class", "");
                (lvDoctors.FindControl("lnkSortCertificate") as LinkButton).Attributes.Add("class", "");

                LinkButton lnkSortedColumn = null;
                if (ViewState["SortBy"] != null)
                {
                    switch (ViewState["SortBy"].ToString().ToLower())
                    {
                        case "name":
                            lnkSortedColumn = lvDoctors.FindControl("lnkSortFirstName") as LinkButton;
                            break;
                        case "CertificateFileName":
                            lnkSortedColumn = lvDoctors.FindControl("lnkSortCertificate") as LinkButton;
                            break;
                        case "emailid":
                            lnkSortedColumn = lvDoctors.FindControl("lnkSortEmailId") as LinkButton;
                            break;
                        case "country":
                            lnkSortedColumn = lvDoctors.FindControl("lnkSortCountry") as LinkButton;
                            break;
                        case "state":
                            lnkSortedColumn = lvDoctors.FindControl("lnkSortState") as LinkButton;
                            break;
                        case "city":
                            lnkSortedColumn = lvDoctors.FindControl("lnkSortCity") as LinkButton;
                            break;
                        case "ispayment":
                            lnkSortedColumn = lvDoctors.FindControl("lnkIsPayment") as LinkButton;
                            break;
                        case "isaccactive":
                            lnkSortedColumn = lvDoctors.FindControl("lnkIsAccActive") as LinkButton;
                            break;
                    }
                }
                if (lnkSortedColumn != null)
                {
                    if (ViewState["AscDesc"].ToString().ToLower() == "asc")
                    {
                        lnkSortedColumn.Attributes.Add("class", "ascending");
                    }
                    else
                    {
                        lnkSortedColumn.Attributes.Add("class", "descending");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void lvCertifiedDoctor_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvCertifiedDoctor.Items.Count > 0)
                    SetSortImage();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void lvNonCertifiedDoctor_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (lvNonCertifiedDoctor.Items.Count > 0)
                    SetSortImage();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbtnCertified.Checked)
                    lvCertifiedDoctor.DataBind();
                else
                    lvNonCertifiedDoctor.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        protected void lvNonCertifiedDoctor_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                NonCertifiedDoctorDetailsByFilterType doctor = (NonCertifiedDoctorDetailsByFilterType)e.Item.DataItem;
                HyperLink hypEdit = (HyperLink)e.Item.FindControl("hypEdit");
                //ImageButton imgbtnDelete = (ImageButton)e.Item.FindControl("imgbtnDelete");
                ImageButton imgbtnStatus = (ImageButton)e.Item.FindControl("imgbtnStatus");
                HyperLink hypDomainLink = (HyperLink)e.Item.FindControl("hypDomainLink");


                if (doctor.SourceType.Trim() == "Ortho")
                {
                    hypEdit.NavigateUrl = "~/Admin/AddEditDoctor.aspx?id=" + Server.UrlEncode(Cryptography.EncryptStringAES(doctor.DoctorId.ToString(), CommonLogic.GetConfigValue("SharedSecret")));
                    //imgbtnDelete.CommandArgument = Cryptography.EncryptStringAES(doctor.DoctorId.ToString(), CommonLogic.GetConfigValue("SharedSecret"));
                    //imgbtnDelete.OnClientClick = "javascript:return confirm('" + this.GetLocalResourceObject("DeleteMessage") + "')";
                    //imgbtnDelete.OnClientClick = "javascript:return confirm('Are you sure you want delete this record?')";                    
                }
                else if (doctor.SourceType.Trim().ToUpper() == "EMR")
                {
                    if (hypDomainLink != null)
                    {
                        if (doctor.DataBaseName != null && doctor.DataBaseName.Split('_').Length > 2)
                            hypDomainLink.Text = doctor.DataBaseName.Split('_')[2];
                        else
                            hypDomainLink.Text = "WWW";
                        hypDomainLink.NavigateUrl = doctor.DomainURL;
                    }
                }
                else
                {
                    hypEdit.Visible = false;//imgbtnDelete.Visible = 
                }

                if (doctor.IsAccountActivated)
                    imgbtnStatus.OnClientClick = "javascript:return confirm('" + this.GetLocalResourceObject("deactivatemessage") + "')";
                else
                    imgbtnStatus.OnClientClick = "javascript:return confirm('" + this.GetLocalResourceObject("activatemessage") + "')";
            }
        }

        protected void SendAccountActiveMail(bool IsAccountActivated, string toEmailAddress, string lastName)
        {
            string emailContent = string.Empty;
            if (IsAccountActivated)
                emailContent = "Congratulations!! Your account has been activated now.<br />It’s our pleasure and honor to welcome you on behalf of <a href='http://4clearortho.com/'>4ClearOrtho.com</a>, please go through the features of it.";
            else
                emailContent = "Your account has been deactivated now. Please contact to administrator.";

            string emailTemplatePath = Server.MapPath("..//" + CommonLogic.GetConfigValue("AccountActivated")).ToString();

            if (File.Exists(emailTemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##LastName##", lastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##Message##", emailContent);
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", DateTime.Now.Year.ToString());
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));

                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                MailAddress toMailAddress = new MailAddress(toEmailAddress);
                CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho – Account Status Updated");
            }

        }
    }
}