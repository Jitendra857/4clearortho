using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class PatientProfile : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(PatientProfile));
        long lPatientId = 0;
        PatientEntity patientEntity;
        #endregion

        #region Event

        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    PageRight pageRight = CheckRights(this.Page.GetType().BaseType.Name);
                    if (pageRight != null)
                    {
                        PageRedirect(pageRight.RedirectPageName);
                    }

                    lPatientId = Convert.ToInt64(currentSession.PatientId);
                }
                if (!Page.IsPostBack)
                {
                    GetPatientById();
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
        /// Event to submit patient details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    SavePatient();
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        #endregion

        #region Helper

        /// <summary>
        /// Method to save patient.
        /// </summary>
        private void SavePatient()
        {
            try
            {
                patientEntity = new PatientEntity();
                if (lPatientId > 0)
                {
                    Patient patient = patientEntity.GetPatientById(lPatientId);
                    if (patient != null)
                    {
                        patient.EmailId = txtEmail.Text;
                        patient.BirthDate = Convert.ToDateTime(txtDateofBirth.Text);
                        patient.CreatedBy = 0;
                        patient.FirstName = txtFirstName.Text;
                        patient.LastName = txtLastName.Text;
                        patient.LastUpdatedBy = 0;
                        if (rbtnFemale.Checked)
                            patient.Gender = "F";
                        else
                            patient.Gender = "M";

                        long patientId = patientEntity.Save(patient);
                        CommonHelper.ShowMessage(MessageType.Success, this.GetLocalResourceObject("ProfileUpdated").ToString(), divMsg, lblMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Method to get patient detail by patient id.
        /// </summary>
        private void GetPatientById()
        {
            try
            {
                patientEntity = new PatientEntity();
                if (lPatientId > 0)
                {
                    Patient patient = patientEntity.GetPatientById(lPatientId);
                    if (patient != null)
                        txtEmail.Text = patient.EmailId;
                    txtDateofBirth.Text = patient.BirthDate.ToString("MM/dd/yyyy");
                    txtFirstName.Text = patient.FirstName;
                    txtLastName.Text = patient.LastName;
                    if (patient.Gender == "M")
                        rbtnMale.Checked = true;
                    else
                        rbtnFemale.Checked = true;
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