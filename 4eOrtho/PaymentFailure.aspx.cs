using _4eOrtho.BAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using log4net;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class PaymentFailure : System.Web.UI.Page
    {
        #region GLOBAL DECLARATION        
        private ILog logger;
        #endregion

        #region EVENTS

        /// <summary>
        /// Page Load Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            logger = log4net.LogManager.GetLogger(typeof(PaymentFailure));
            if ((CurrentSession)Session["UserLoginSession"] != null)
            {
                if (!Page.IsPostBack)
                {
                    GetAndSetInitialValues();
                    SendPaymentSuccessEmail();
                }
            }
            else
            {
                Response.Redirect("Home.aspx", false);
                return;
            }
        }

        /// <summary>
        /// Event to redirect previous page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Convert.ToString(Session["redirect"]), false);
        }

        #endregion

        #region FUNCTIONS

        /// <summary>
        /// This function will get values from session.
        /// And set session values to controls
        /// </summary>
        private void GetAndSetInitialValues()
        {
            try
            {
                //userInfo = (UserSessionData)SessionHelper.PersonalInformation;
                ltrErrorMsg.Text = string.Empty;

                List<ErrorType> errorList = (List<ErrorType>)SessionHelper.PayPalServiceResponseErrors;
                if (errorList != null && errorList.Count > 0)
                {
                    int i = 1;
                    foreach (ErrorType item in errorList)
                    {
                        ltrErrorMsg.Text += GetErrorMessage(item.LongMessage, i);
                        i++;
                    }
                }
                CommonHelper.ShowMessage(MessageType.Error, this.GetLocalResourceObject("PaymentFailed").ToString(), divMsg, lblMsg);
                //CommonLogic.SessionAbandon();
            }
            catch (Exception exp)
            {
                logger.Error("Payment Failure page loading process", exp);
            }
        }

        /// <summary>
        /// This method will format label with error message.
        /// </summary>
        /// <param name="longMsg"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private string GetErrorMessage(string longMsg, int step)
        {
            return string.Format("<label style=\"margin-bottom: 15px; width:100%;\"> (" + step.ToString() + ") " + longMsg + "</label><br>");
        }


        /// <summary>
        /// This function will send mail to user.
        /// </summary>
        private void SendPaymentSuccessEmail()
        {
            try
            {
                string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("PaymentFailure")).ToString();

                if (File.Exists(emailTemplatePath))
                {
                    CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];

                    string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                    emailtemplateHTML = emailtemplateHTML.Replace("##DoctorName##", currentSession.DoctorName);
                    emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", DateTime.Now.Year.ToString());
                    emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));

                    emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONSTATUS##", "Failed");
                    emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONDATE##", SessionHelper.TransactionTime);
                    emailtemplateHTML = emailtemplateHTML.Replace("##TRANSACTIONAMOUNT##", SessionHelper.PaymentAmount);
                    emailtemplateHTML = emailtemplateHTML.Replace("##ERRORMESSAGE##", ltrErrorMsg.Text);

                    MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("ToMail"));
                    MailAddress toMailAddress = new MailAddress(currentSession.EmailId);
                    CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho – Payment Failed");
                }
                else
                    logger.Info(emailTemplatePath + " emailtemplate is not exist.");
            }
            catch (Exception ex)
            {
                logger.Error("Payment email sending process", ex);
            }
        }
        #endregion
    }
}