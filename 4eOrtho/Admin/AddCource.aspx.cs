using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;

namespace _4eOrtho.Admin
{
    public partial class AddCource : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(ChangePassword));        
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((CurrentSession)Session["UserLoginSession"] != null)
                {
                    PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                    if (pageRight != null)
                    {
                        PageRedirect(pageRight.RedirectPageName);
                    }
                }
                BindDropDown();
                CheckAADCouceExistsorNot();
            }
        }

        private void CheckAADCouceExistsorNot()
        {
            try
            {
                CurrentSession user = (CurrentSession)Session["UserLoginSession"];
                LookupTypeMasterEntity lookupTypeMasterEntity = new LookupTypeMasterEntity();
                List<LookupTypeMaster> data = lookupTypeMasterEntity.GetLookupMasterDetails("StudentCourseSubscribeLink");
                if (data.Count > 0)
                {
                    LookupMasterEntity lookupMasterEntity = new LookupMasterEntity();
                    LookupMaster infomaster = lookupMasterEntity.GetLookupMasterByLookUpTypeId(data[0].LookupTypeId);
                    if (infomaster != null)
                    {
                        txtAADCourcelink.Text = infomaster.LookupName;
                    }
                }
            }
            catch (Exception exp)
            {
                logger.Error("An error occured.", exp);
            }
        }

        protected void ddlcource_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CourseEntity courseEntity = new CourseEntity();
                List<GetAllSubCourceForAdmin> info = courseEntity.GetAllSubCourceForAdmin(Convert.ToInt64(ddlcource.SelectedValue));
                DDl_SubCource.Items.Clear();
                if (info != null)
                {
                    DDl_SubCource.DataTextField = "Title";
                    DDl_SubCource.DataValueField = "CourseCategoryId";
                    DDl_SubCource.DataSource = info;
                    DDl_SubCource.DataBind();
                }
                DDl_SubCource.Items.Insert(0, new ListItem(this.GetLocalResourceObject("DDLCourseText").ToString(), "0"));
            }
            catch (Exception exp)
            {
                logger.Error("An error occured.", exp);
            }
        }

        protected void DDl_SubCource_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string encrypid = CommonLogic.EncryptStringAES(DDl_SubCource.SelectedValue);
                string url = "http://americanacademyofdentistry.com/SubscriptionPayment.aspx?SelectedCourseID=" + encrypid;
                txtAADCourcelink.Text = url;
            }
            catch (Exception exp)
            {
                logger.Error("An error occured.", exp);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SaveData();
                }
            }
            catch (Exception exp)
            {
                logger.Error("An error occured.", exp);
                CommonHelper.ShowMessage(MessageType.Error, "Error occured while inserting record", divMsg, lblMsg);
            }
        }
        #endregion

        #region Helper
        private void BindDropDown()
        {
            try
            {
                CourseEntity courseEntity = new CourseEntity();
                List<GetAllCourceForAdmin> info = courseEntity.GetAllCourceForAdmin();
                if (info != null)
                {
                    ddlcource.DataTextField = "LookupName";
                    ddlcource.DataValueField = "LookupID";
                    ddlcource.DataSource = info;
                    ddlcource.DataBind();
                }
                ddlcource.Items.Insert(0, new ListItem(this.GetLocalResourceObject("DDLCategoryText").ToString(), "0"));
                DDl_SubCource.Items.Insert(0, new ListItem(this.GetLocalResourceObject("DDLCourseText").ToString(), "0"));
            }
            catch (Exception exp)
            {
                logger.Error("An error occured.", exp);
            }
        }

        private void SaveData()
        {
            CurrentSession user = (CurrentSession)Session["UserLoginSession"];
            LookupTypeMasterEntity lookupTypeMasterEntity = new LookupTypeMasterEntity();
            List<LookupTypeMaster> data = lookupTypeMasterEntity.GetLookupMasterDetails("StudentCourseSubscribeLink");
            if (data.Count > 0)
            {
                LookupMasterEntity lookupMasterEntity = new LookupMasterEntity();
                LookupMaster infomaster = lookupMasterEntity.GetLookupMasterByLookUpTypeId(data[0].LookupTypeId);
                if (infomaster != null)
                {
                    infomaster.LookupName = txtAADCourcelink.Text;
                    infomaster.LastUpdatedBy = SessionHelper.LoggedAdminUserID;
                    infomaster.LookupDescription = data[0].LookupTypeDescription;
                    infomaster.LastUpdatedDate = DateTime.Now;
                    lookupMasterEntity.UpdateLookup(infomaster);
                    ddlcource.SelectedValue = "0";
                    DDl_SubCource.SelectedValue = "0";
                    CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("Updatemessage").ToString(), divMsg, lblMsg);
                }
                else
                {

                    lookupMasterEntity.Create();
                    LookupMaster master = new LookupMaster();
                    master.IsActive = true;
                    master.IsDelete = false;
                    master.LookupName = txtAADCourcelink.Text;
                    master.LookupTypeId = data[0].LookupTypeId;
                    master.LookupDescription = data[0].LookupTypeDescription;
                    master.CreatedBy = SessionHelper.LoggedAdminUserID;
                    lookupMasterEntity.Save(master);
                    CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("InsertedMessage").ToString(), divMsg, lblMsg);
                    ddlcource.SelectedValue = "0";
                    DDl_SubCource.SelectedValue = "0";
                }
            }
            else
            {
                CommonHelper.ShowMessage(MessageType.Error, "Lookup Type Does not Found.", divMsg, lblMsg);
            }
        }
        #endregion
    }
}