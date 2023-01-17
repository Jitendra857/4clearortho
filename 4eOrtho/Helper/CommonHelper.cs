using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using _4eOrtho.Utility;
using _4eOrtho.BAL;
using _4eOrtho.DAL;
using System.IO;

namespace _4eOrtho.Helper
{
    public class CommonHelper
    {
        public static void ShowMessage(MessageType messageType, string message, HtmlGenericControl divContainer, Label lblMessage)
        {
            divContainer.Visible = true;
            lblMessage.Text = message;
            switch (messageType)
            {
                case MessageType.Help:
                    divContainer.Attributes.Add("class", "errormsgbox");
                    break;
                case MessageType.Error:
                    divContainer.Attributes.Add("class", "errormsgbox");
                    break;
                case MessageType.ForgotError:
                    divContainer.Attributes.Add("class", "forgoterrormsgbox");
                    break;
                case MessageType.Warning:
                    divContainer.Attributes.Add("class", "errormsgbox");
                    break;
                case MessageType.Success:
                    divContainer.Attributes.Add("class", "successbox");
                    break;
                case MessageType.ForgotSuccess:
                    divContainer.Attributes.Add("class", "forgotsuccessbox");
                    break;
            }
        }

        public static void SaveRegisterUserEmailLog(string userName, string password, string firstName, string lastName, String UserType, string fromMailAddress, string toMailAddress, string ccMailAddress, string bccMailAddress, string emailTemplatePath, string subject)
        {
            try
            {
                MailLogEntity mailLogEntity = new MailLogEntity();
                MailLog emrMailLog = mailLogEntity.Create();
                emrMailLog.FromMailId = fromMailAddress;
                emrMailLog.ToMailId = toMailAddress;

                string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserFirstName##", firstName + " " + lastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserEmailAddress##", toMailAddress);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserType##", UserType);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##UserPassword##", password.Replace("&", "&amp;"));
                emailtemplateHTML = emailtemplateHTML.Replace("##SiteURL##", CommonLogic.GetSiteURL() + @"Login.aspx");

                if (ccMailAddress != null)
                {
                    emrMailLog.CCMailId = ccMailAddress;
                }

                if (bccMailAddress != null)
                {
                    emrMailLog.BccMailId = bccMailAddress;
                }

                emrMailLog.Time = BaseEntity.GetServerDateTime;
                emrMailLog.Body = emailtemplateHTML;
                emrMailLog.Subject = subject;
                mailLogEntity.Save(emrMailLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string MaskCreditCardNumber(string creditCardNo)
        {
            return string.Format("XXXX{0}", creditCardNo.Substring(creditCardNo.Length - 4));
        }
    }

    public static class SqlConnectionHelper
    {
        //Live connection string
        //<add name="OrthoEntities" connectionString="metadata=res://*/4eOrtho.csdl|res://*/4eOrtho.ssdl|res://*/4eOrtho.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=S192-169-188-49;initial catalog=4ClearOrtho_Test;user id=sa;password=login12*;multipleactiveresultsets=True;application name=EntityFramework&quot;" providerName="System.Data.EntityClient" />
        // public const string Connection = @"data source=S192-169-188-49;initial catalog=4ClearOrtho;Integrated Security=true;multipleactiveresultsets=True;user id=sa;password=login12*;";

        //Local
        public const string Connection = @"data source=DESKTOP-NGNOP1Q\EXPRESS1;initial catalog=4ClearOrtho_New;Integrated Security=true;multipleactiveresultsets=True;user id=sa;password=joshi@123;";
    }

    
    
}