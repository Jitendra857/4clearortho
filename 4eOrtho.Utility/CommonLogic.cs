using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Caching;
using log4net;

namespace _4eOrtho.Utility
{
    public class CommonLogic
    {
        public static bool IsLocalSite = true;

        #region Conversion Related Function

        public static string GetStringValue(object value)
        {
            return Convert.ToString(value);
        }

        public static int GetIntValue(object value)
        {
            return Convert.ToInt32(value);
        }

        public static bool GetBoolValue(object value)
        {
            return Convert.ToBoolean(value);
        }

        public static decimal GetDecimalValue(object value)
        {
            return Math.Round(Convert.ToDecimal(value), 2);
        }

        public static string GetDecimalValueInStringFormat(decimal value)
        {
            return Math.Round(value, 2).ToString();
        }

        public static string GetDecimalValueInCurrencyFormat(decimal value)
        {
            return string.Format("{0:c2}", value);
        }

        public static DateTime GetDateTimeValue(object value)
        {
            if (Convert.IsDBNull(value))
                return System.DateTime.MinValue;
            else
                return Convert.ToDateTime(value);
        }

        public static DateTime? GetDateTimeNullValue(object value)
        {
            if (Convert.IsDBNull(value))
                return null;
            else
                return Convert.ToDateTime(value);
        }

        #endregion Conversion Related Function

        #region QUERYSTRING SUPPORTED FUNCTIONS

        public static String QueryString(String paramName)
        {
            return QueryStringCanBeDangerousContent(paramName);
        }

        public static String QueryStringCanBeDangerousContent(String paramName)
        {
            String tmpS = String.Empty;
            if (HttpContext.Current.Request.QueryString[paramName] != null)
            {
                try
                {
                    tmpS = HttpContext.Current.Request.QueryString[paramName].ToString();
                }
                catch
                {
                    tmpS = String.Empty;
                }
            }
            return tmpS;
        }

        #endregion QUERYSTRING SUPPORTED FUNCTIONS

        #region SESSION SUPPORTED FUNCTIONS

        public static string GetSessionValue(string paramName)
        {
            String tmpS = String.Empty;
            try
            {
                tmpS = Convert.ToString(HttpContext.Current.Session[paramName]);
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }

        public static StringBuilder GetSessionValueStringBuilder(string paramName)
        {
            StringBuilder tmp5 = new StringBuilder();
            try
            {
                tmp5.Append(HttpContext.Current.Session["paramName"]);
            }
            catch
            {
                tmp5 = new StringBuilder();
            }
            return tmp5;
        }

        public static List<PageList> GetSessionValueArrayList(string paramName)
        {
            List<PageList> tmpS = new List<PageList>();
            try
            {
                tmpS = HttpContext.Current.Session[paramName] as List<PageList>;
            }
            catch
            {
                tmpS = new List<PageList>();
            }
            return tmpS;
        }


        public static ArrayList GetSessionValueListByArray(string paramName)
        {
            ArrayList tmpS = new ArrayList();
            try
            {
                tmpS = HttpContext.Current.Session[paramName] as ArrayList;
            }
            catch
            {
                tmpS = new ArrayList();
            }
            return tmpS;
        }


        public static DateTime GetSessionDateTimeValue(string paramName)
        {
            DateTime tmpS = new DateTime();
            try
            {
                tmpS = (DateTime)HttpContext.Current.Session[paramName];
            }
            catch
            {
                tmpS = new DateTime();
            }
            return tmpS;
        }

        public static object GetSessionObject(string paramName)
        {
            object tmpO = null;
            try
            {
                tmpO = HttpContext.Current.Session[paramName];
            }
            catch
            {
                tmpO = null;
            }
            return tmpO;
        }

        public static void SetSessionValue(string paramName, object paramValue)
        {
            HttpContext.Current.Session[paramName] = paramValue;
        }

        public static object GetCacheObject(string paramName)
        {
            object tmpO = null;
            try
            {
                tmpO = HttpContext.Current.Cache[paramName];
            }
            catch
            {
                tmpO = null;
            }
            return tmpO;
        }

        public static void SetCacheValue(string paramName, object paramValue)
        {
            HttpContext.Current.Cache[paramName] = paramValue;
        }

        public static void SetCacheValueWithExpiretime(string paramName, object paramValue)
        {
            HttpContext.Current.Cache.Add(paramName, paramValue, null, DateTime.MaxValue, new TimeSpan(1, 0, 0), CacheItemPriority.Default, null);
        }

        public static void RemoveCacheValue(string paramName)
        {
            HttpContext.Current.Cache.Remove(paramName);
        }

        public static void RemoveSession(string paramName)
        {
            HttpContext.Current.Session.Remove(paramName);
        }

        #endregion SESSION SUPPORTED FUNCTIONS

        #region COOKIE SUPPORTED FUNCTIONS

        public static object CheckCookieObject(string paramName)
        {
            object tmpO = null;
            try
            {
                tmpO = HttpContext.Current.Request.Cookies[paramName];
            }
            catch
            {
                tmpO = null;
            }
            return tmpO;
        }

        public static void RemoveCookieObject(string paramName)
        {
            try
            {
                HttpContext.Current.Request.Cookies.Remove(paramName);
            }
            catch
            {
            }
        }

        public static void AddCookieObject(HttpCookie paramName)
        {
            try
            {
                HttpContext.Current.Response.Cookies.Add(paramName);
            }
            catch
            {
            }
        }

        public static string GetCookieValue(string paramName)
        {
            String tmpS = String.Empty;
            try
            {
                tmpS = HttpContext.Current.Request.Cookies[paramName].Value;
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }

        #endregion COOKIE SUPPORTED FUNCTIONS

        #region WEB CONFIG SUPPORTED FUNCTION

        public static string GetConfigValue(string paramName)
        {
            String tmpS = String.Empty;
            try
            {
                tmpS = ConfigurationManager.AppSettings[paramName].ToString();
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }

        #endregion WEB CONFIG SUPPORTED FUNCTION

        #region PARSING RELATED FUNCTION

        static public int ParseNativeInt(String s)
        {
            int ni;
            System.Int32.TryParse(s, NumberStyles.Integer, Thread.CurrentThread.CurrentCulture, out ni); // use default locale setting
            return ni;
        }

        static public decimal ParseNativeDecimal(String s)
        {
            Decimal nd;
            System.Decimal.TryParse(s, NumberStyles.Number, Thread.CurrentThread.CurrentUICulture, out nd);
            return nd;
        }

        public static string ParseNativeStringValue(object value, string defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToString(value);
        }

        static public DateTime ParseNativeDateTime(String s)
        {
            try
            {
                return System.DateTime.Parse(s); // use default locale setting
            }
            catch
            {
                return Convert.ToDateTime("1/1/1900");
            }
        }

        static public bool ParseFrontStatus(String status)
        {
            switch (status.ToLower().Trim())
            {
                case "true":
                case "1":
                    return true;
                case "false":
                case "0":
                    return false;
                default:
                    return false;

            }
        }

        #endregion PARSING RELATED FUNCTION

        #region SUPPORTED FUNCTIONS

        static public void CheckForScriptTag(String s)
        {
            if (s.Replace(" ", "").IndexOf("<script", StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                throw new ArgumentException("SECURITY EXCEPTION");
            }
        }

        /// <summary>
        /// Composes the returnUrl query string into 1 chunk of string.
        /// </summary>
        /// <param name="returnUrl">The return url string to be used on the query string</param>
        /// <returns></returns>
        public static string ReturnURLEncode(string returnUrl)
        {
            return returnUrl.Replace('&', '|').Replace('=', '$');
        }

        /// <summary>
        /// Decodes the returnUrl string that was originally encoded by calling  ReturnUrlEncode
        /// </summary>
        /// <param name="returnUrl">The returnUrl querystring that was originally ReturnUrlEncoded</param>
        /// <returns></returns>
        public static string ReturnURLDecode(string returnUrl)
        {
            return returnUrl.Replace('|', '&').Replace('$', '=');
        }

        /// <summary>
        /// Gets the current page name together with additional query strings if any
        /// </summary>
        /// <returns></returns>
        public static string GetThisPageUrlWithQueryString()
        {
            return GetThisPageUrlWithQueryString(string.Empty);
        }

        /// <summary>
        /// Gets the current page name together with additional query strings if any
        /// </summary>
        /// <param name="additionalQueryStringsInNameValuePair">The additional query strings in name=value&name=value format</param>
        /// <returns></returns>
        public static string GetThisPageUrlWithQueryString(string additionalQueryStringsInNameValuePair)
        {
            return GetThisPageUrlWithQueryString(additionalQueryStringsInNameValuePair, true);
        }

        /// <summary>
        /// Gets the current page name together with additional query strings if any
        /// </summary>
        /// <param name="additionalQueryStringsInNameValuePair">The additional query strings in name=value&name=value format</param>
        /// <param name="encodeValues">The flag whether to url encode the values, should always default this to true</param>
        /// <returns></returns>
        public static string GetThisPageUrlWithQueryString(string additionalQueryStringsInNameValuePair, bool encodeValues)
        {
            // NOTE:
            //  If the current url is url-rewrited, we must honor the current format
            StringBuilder url = new StringBuilder();

            string pageName = IIF(
                                HttpContext.Current.Items["RequestedPage"] != null,
                                HttpContext.Current.Items["RequestedPage"].ToString(),
                                GetThisPageName(false));
            url.Append(pageName);

            // our data structure to hold temporarily the query name value pairs
            Dictionary<string, string> allQueryStrings = new Dictionary<string, string>();

            string originalQueryStringsInNameValuePair = CommonLogic.IIF(
                                            HttpContext.Current.Items["RequestedQuerystring"] != null,
                                            HttpContext.Current.Items["RequestedQuerystring"].ToString(),
                                            HttpContext.Current.Request.Url.Query);

            if (originalQueryStringsInNameValuePair.StartsWith("?"))
            {
                originalQueryStringsInNameValuePair = originalQueryStringsInNameValuePair.Remove(0, 1);
            }

            string[] originalQueryStrings = originalQueryStringsInNameValuePair.Split('&');

            // first add the original query strings if any
            if (originalQueryStrings.Length > 0)
            {
                foreach (string queryStringNameValuePair in originalQueryStrings)
                {
                    string[] queryStringValues = queryStringNameValuePair.Split('=');
                    if (queryStringValues.Length == 2)
                    {
                        string queryStringName = queryStringValues[0];
                        string queryStringValue = queryStringValues[1];

                        // let's make sure we have no duplicates in the query string
                        // if we have any, we'll use the first one
                        if (!allQueryStrings.ContainsKey(queryStringName))
                        {
                            allQueryStrings.Add(queryStringName, queryStringValue);
                        }
                    }
                }
            }

            // now let's add the additional query strings if we have any
            string[] additionalQueryStrings = additionalQueryStringsInNameValuePair.Split('&');
            if (additionalQueryStrings.Length > 0)
            {
                foreach (string queryStringNameValuePair in additionalQueryStrings)
                {
                    string[] queryStringValues = queryStringNameValuePair.Split('=');
                    if (queryStringValues.Length == 2)
                    {
                        string queryStringName = queryStringValues[0];
                        string queryStringValue = queryStringValues[1];

                        // let's make sure we have no duplicates in the query string
                        // if we have any, we'll use the first one
                        if (!allQueryStrings.ContainsKey(queryStringName))
                        {
                            allQueryStrings.Add(queryStringName, queryStringValue);
                        }
                    }
                }
            }

            // check if we have query strings
            if (allQueryStrings.Count > 0)
            {
                url.Append("?");

                int ctr = 0;
                foreach (KeyValuePair<string, string> queryString in allQueryStrings)
                {
                    if (ctr != 0)
                    {
                        url.Append("&");
                    }

                    url.AppendFormat(
                        "{0}={1}",
                        HttpUtility.UrlEncode(queryString.Key),
                        CommonLogic.IIF(encodeValues, HttpUtility.UrlEncode(queryString.Value), queryString.Value));

                    ctr++;
                }
            }

            return url.ToString();
        }

        static public String IIF(bool condition, String a, String b)
        {
            String x = String.Empty;
            if (condition)
            {
                x = a;
            }
            else
            {
                x = b;
            }
            return x;
        }

        public static String ServerVariables(String paramName)
        {
            String tmpS = String.Empty;
            if (HttpContext.Current.Request.ServerVariables[paramName] != null)
            {
                try
                {
                    tmpS = HttpContext.Current.Request.ServerVariables[paramName].ToString();
                }
                catch
                {
                    tmpS = String.Empty;
                }
            }
            return tmpS;
        }

        static public String GetThisPageName(bool includePath)
        {
            String s = CommonLogic.ServerVariables("SCRIPT_NAME");
            if (!includePath)
            {
                int ix = s.LastIndexOf("/");
                if (ix != -1)
                {
                    s = s.Substring(ix + 1);
                }
            }
            return s;
        }

        static public String GetThisPageNameWithQueryString()
        {
            String pageName = GetThisPageName(false);

            String queryParam = ServerVariables("QUERY_STRING");
            if (!String.IsNullOrEmpty(queryParam))
                pageName = pageName + "?" + queryParam;

            return pageName;
        }

        public static string GetSiteURL()
        {
            string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            if (url.ToLower().Contains(".aspx"))
            {
                url = url.Substring(0, url.LastIndexOf("/") + 1);
            }
            return url;
        }

        #endregion SUPPORTED FUNCTIONS

        #region FORMAT FUNCTIONS

        public static string DateTimeFormat(object value, string format)
        {
            if (Convert.IsDBNull(value))
                return "";
            else
                return Convert.ToDateTime(value).ToString(format);
        }

        public static string DateTimeFormat(object value)
        {
            if (Convert.IsDBNull(value))
                return "";
            else
                return Convert.ToDateTime(value).ToString("MM/dd/yyyy");
        }

        public static DateTime GetESTTimefromLocalTime(DateTime value)
        {
            return TimeZoneInfo.ConvertTime(value, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
        }

        #endregion FORMAT FUNCTIONS

        #region MISC FUNCTIONS

        static public bool HasRowsInDatatable(DataTable dt)
        {
            if (dt == null) return false;
            if (dt.Rows.Count <= 0) return false;
            return true;
        }

        static public bool HasRowsInDataset(DataSet ds)
        {
            if (ds == null) return false;
            if (ds.Tables.Count <= 0) return false;
            if (ds.Tables[0].Rows.Count <= 0) return false;
            return true;
        }

        static public bool HasRowsInIDataReader(IDataReader dr)
        {
            if (dr == null) return false;
            if (dr.IsClosed.Equals(true)) return false;
            int intCounter = 0;
            while (dr.Read())
            {
                intCounter++;
            }
            if (!intCounter.Equals(0))
                return true;
            else
                return false;
        }

        static public bool IsFieldMorethenOneInIDataReader(IDataReader dr)
        {
            if (dr == null) return false;
            if (dr.IsClosed.Equals(true)) return false;
            if (dr.FieldCount > 1)
                return true;
            else
                return false;
        }

        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@$?";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }

            return new string(chars);
        }

        #endregion MISC FUNCTIONS

        #region Mail

        /// <summary>
        /// function for send email
        /// </summary>
        /// <param name="ToMail">Comma seperated email addresses to whom wants to send email</param>
        /// <param name="FromMail">From whom wants to send email</param>
        /// <param name="Cc">Add Cc using comma seperated</param>
        /// <param name="Bcc">Add Cc using comma seperated</param>
        /// <param name="body">Body part of email address</param>
        /// <param name="subject">Subject of  an email</param>
        /// <returns></returns>
        public static bool SendMail(MailAddress fromMailAddress, MailAddress toMailAddress, MailAddress ccMailAddress, MailAddress bccMailAddress, string body, string subject)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            ILog logger = log4net.LogManager.GetLogger(typeof(CommonLogic));

            using (SmtpClient smtp = new SmtpClient(smtpSection.Network.ClientDomain))
            {                
                NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;                
                smtp.Credentials = networkCred;
                smtp.Port = smtpSection.Network.Port;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = smtpSection.Network.EnableSsl;
                smtp.Host = smtpSection.Network.Host;

                MailMessage mailmsg = new MailMessage();
                mailmsg.From = fromMailAddress;
                mailmsg.To.Add(toMailAddress);

                if (ccMailAddress != null)
                    mailmsg.CC.Add(ccMailAddress);

                if (bccMailAddress != null)
                    mailmsg.Bcc.Add(bccMailAddress);
                else
                {
                    string bccAddress = CommonLogic.GetConfigValue("TestEmailID");
                    if (!String.IsNullOrEmpty(bccAddress))
                        mailmsg.Bcc.Add(bccAddress);
                }
                mailmsg.Body = body;
                mailmsg.Subject = subject;
                mailmsg.IsBodyHtml = true;

                try
                {
                    smtp.Send(mailmsg);
                }
                catch (Exception ex)
                {
                    logger.Error("An error occured .", ex);
                    return false;
                }
                finally
                {
                    mailmsg.Dispose();
                }
            }
            return true;
        }

        public static bool SendMail(string to, string subject, string msg, string cc = "", string bcc = "")
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Credentials = (System.Net.NetworkCredential)client.Credentials;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(((System.Net.NetworkCredential)client.Credentials).UserName.ToString());
                if (!string.IsNullOrEmpty(to))
                {
                    mail.To.Add(to);
                }
                if (!string.IsNullOrEmpty(bcc))
                {
                    mail.Bcc.Add(bcc);
                }
                if (!string.IsNullOrEmpty(cc))
                {
                    mail.CC.Add(cc);
                }

                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Body = msg;
                client.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void RegisterUserEmail(string userName, string password, string firstName, string lastName, string emailTemplatePath, string emailId, String userType, string templateName)
        {
            if (File.Exists(emailTemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserFirstName##", firstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserLastName##", lastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserEmailAddress##", emailId);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserType##", userType);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##SupportEmail##", CommonLogic.GetConfigValue("SupportEmailKey"));
                emailtemplateHTML = emailtemplateHTML.Replace("##UserPassword##", password.Replace("&", "&amp;"));
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());

                if (userType.ToLower() == "doctor")
                    emailtemplateHTML = emailtemplateHTML.Replace("##SiteURL##", CommonLogic.GetSiteURL() + @"DoctorLogin.aspx");
                else if (userType.ToLower() == "admin")
                    emailtemplateHTML = emailtemplateHTML.Replace("##SiteURL##", CommonLogic.GetSiteURL() + @"Login.aspx");

                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("FromNoReplyEmail"), CommonLogic.GetConfigValue("FromNoReplyName"));
                MailAddress toMailAddress = new MailAddress(emailId, userName);
                if (templateName.Equals("Registration"))
                {
                    CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho – Welcome User");
                }
                else if (templateName.Equals("EditUser"))
                {
                    CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho – Login Credentials Changed");
                }
            }
        }

        public static void PatientNotificationMail(string userName, string patientName, string firstName, string lastName, string emailTemplatePath, string emailId, string message)
        {
            if (File.Exists(emailTemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserFullName##", patientName);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserFirstName##", firstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserLastName##", lastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##Message##", message);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());

                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("FromNoReplyEmail"), CommonLogic.GetConfigValue("FromNoReplyName"));
                MailAddress toMailAddress = new MailAddress(emailId, userName);
                CommonLogic.SendMail(fromMailAddress, toMailAddress, null, null, emailtemplateHTML, "4ClearOrtho - Appointment Notification");
            }
        }

        public static void AppointmentBookingAndRescheduleMail(string patientUserName, string doctorUserName, string patientFullName, string patientFirstName, string patientLastName, string doctorName, string operatory, string description, string duration, string appointmentDate, string fromTime, string toTime, string emailTemplatePath, string emailId, string doctorMailId, string appointmentSubject)
        {
            if (File.Exists(emailTemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientLastName##", patientLastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##PatientFirstName##", patientFirstName);
                //--emailtemplateHTML = emailtemplateHTML.Replace("##PatientFullName##", patientFullName);
                emailtemplateHTML = emailtemplateHTML.Replace("##DoctorName##", doctorName);
                emailtemplateHTML = emailtemplateHTML.Replace("##Operatory##", operatory);
                emailtemplateHTML = emailtemplateHTML.Replace("##Description##", description);
                emailtemplateHTML = emailtemplateHTML.Replace("##Duration##", duration);
                emailtemplateHTML = emailtemplateHTML.Replace("##AppointmentDate##", appointmentDate);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##FromTime##", fromTime);
                emailtemplateHTML = emailtemplateHTML.Replace("##ToTime##", toTime);
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());

                MailAddress fromMailAddress = new MailAddress(CommonLogic.GetConfigValue("FromNoReplyEmail"), CommonLogic.GetConfigValue("FromNoReplyName"));
                MailAddress toMailAddress = new MailAddress(emailId, patientUserName);
                MailAddress ccMailAddress = new MailAddress(doctorMailId, doctorUserName);
                CommonLogic.SendMail(fromMailAddress, toMailAddress, ccMailAddress, null, emailtemplateHTML, appointmentSubject);
            }
        }

        /// <summary>s
        /// function for send email
        /// </summary>
        /// <param name="ToMail">Comma seperated email addresses to whom wants to send email</param>
        /// <param name="FromMail">From whom wants to send email</param>
        /// <param name="Cc">Add Cc using comma seperated</param>
        /// <param name="Bcc">Add Cc using comma seperated</param>
        /// <param name="body">Body part of email address</param>
        /// <param name="subject">Subject of  an email</param>
        /// <param name="FilePath">Filepath of attachment</param>
        /// <returns></returns>
        public static void SendMailWithAttachment(MailAddress fromMailAddress, MailAddress toMailAddress, MailAddress ccMailAddress, MailAddress bccMailAddress, string body, string subject, string FilePaths, string FileName)
        {
            ILog logger = log4net.LogManager.GetLogger(typeof(CommonLogic));
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = true;
            //Check SMTP_UserName and SMTP_Password does not blank, it's black then Use Default Credentials...
            if (!String.IsNullOrEmpty(CommonLogic.GetConfigValue("SMTP_UserName")) && !String.IsNullOrEmpty(CommonLogic.GetConfigValue("SMTP_Password")))
            {
                NetworkCredential basicAuthenticationInfo = new NetworkCredential(CommonLogic.GetConfigValue("SMTP_UserName"), CommonLogic.GetConfigValue("SMTP_Password"));
                smtp.Credentials = basicAuthenticationInfo;
            }
            //smtp.EnableSsl = true;
            smtp.Host = CommonLogic.GetConfigValue("SMTP_Host").ToString();
            if (!String.IsNullOrEmpty(CommonLogic.GetConfigValue("SMTP_Port")))
            {
                smtp.Port = CommonLogic.GetIntValue(CommonLogic.GetConfigValue("SMTP_Port"));
            }

            MailMessage mailmsg = new MailMessage();
            mailmsg.From = fromMailAddress;
            mailmsg.To.Add(toMailAddress);

            if (ccMailAddress != null)
            {
                mailmsg.CC.Add(ccMailAddress);
            }
            if (bccMailAddress != null)
            {
                mailmsg.Bcc.Add(bccMailAddress);
            }
            else
            {
                string bccAddress = CommonLogic.GetConfigValue("TestEmailID");
                if (!String.IsNullOrEmpty(bccAddress))
                {
                    mailmsg.Bcc.Add(bccAddress);
                }
            }
            mailmsg.Body = body;
            mailmsg.Subject = subject;
            mailmsg.IsBodyHtml = true;

            foreach (string FilePath in FilePaths.Split(','))
            {
                if (File.Exists(FilePath))
                {
                    FileInfo objFileInfo = new FileInfo(FilePath);
                    Attachment objAttachment = new Attachment(FilePath);
                    //string strFileName = subject.Replace(" ", "_");
                    objAttachment.Name = objFileInfo.Name;
                    mailmsg.Attachments.Add(objAttachment);
                }
            }
            try
            {
                smtp.Send(mailmsg);
                mailmsg.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }
            //return true;

            //SmtpClient smtp = new SmtpClient();
            //MailMessage mailmsg = new MailMessage();

            //mailmsg.From = fromMailAddress;
            //mailmsg.To.Add(toMailAddress);
            //if (ccMailAddress != null)
            //{
            //    mailmsg.CC.Add(ccMailAddress);
            //}
            //if (bccMailAddress != null)
            //{
            //    mailmsg.Bcc.Add(bccMailAddress);
            //}
            //else
            //{
            //    string bccAddress = CommonLogic.GetConfigValue("TestEmailID");
            //    if (!String.IsNullOrEmpty(bccAddress))
            //    {
            //        mailmsg.Bcc.Add(bccAddress);
            //    }
            //}
            //mailmsg.Body = body;
            //mailmsg.Subject = subject;
            //mailmsg.IsBodyHtml = true;

            ////if (FileID.Length > 0)
            ////{
            //    if (File.Exists(FilePath))
            //    {
            //        FileInfo objFileInfo = new FileInfo(FilePath);
            //        Attachment objAttachment = new Attachment(FilePath);
            //        string strFileName = subject.Replace(" ", "_");
            //        objAttachment.Name = strFileName + objFileInfo.Extension;
            //        mailmsg.Attachments.Add(objAttachment);
            //    }
            ////}

            ////Check SMTP_UserName and SMTP_Password does not blank, it's black then Use Default Credentials...
            //if (CommonLogic.GetApplicationValue("SMTP_UserName").ToString() != String.Empty && CommonLogic.GetApplicationValue("SMTP_Password").ToString() != String.Empty)
            //{
            //    NetworkCredential basicAuthenticationInfo = new NetworkCredential(CommonLogic.GetApplicationValue("SMTP_UserName").ToString(), CommonLogic.GetApplicationValue("SMTP_Password").ToString());
            //    smtp.UseDefaultCredentials = false;
            //    smtp.Credentials = basicAuthenticationInfo;
            //}

            //smtp.Host = CommonLogic.GetApplicationValue("SMTP_Host").ToString();
            //smtp.Port = CommonLogic.GetIntValue(CommonLogic.GetApplicationValue("SMTP_Port"));

            //try
            //{
            //    smtp.Send(mailmsg);
            //    mailmsg.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        public static void WelcomeUserMail(string userName, string password, string firstName, string lastName, string emailTemplatePath, string emailId, string subject)
        {
            if (File.Exists(emailTemplatePath))
            {
                string emailtemplateHTML = File.ReadAllText(emailTemplatePath);
                emailtemplateHTML = emailtemplateHTML.Replace("##CMSEmailImagePath##", CommonLogic.GetConfigValue("CMSEmailImagePath"));
                emailtemplateHTML = emailtemplateHTML.Replace("##UserFirstName##", firstName);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserLastName##", lastName);
                emailtemplateHTML = emailtemplateHTML.Replace("##EmailId##", emailId);
                emailtemplateHTML = emailtemplateHTML.Replace("##UserPassword##", password.Replace("&", "&amp;"));
                //string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
                //url = url.Replace("AddUser", "Login");
                //emailtemplateHTML = emailtemplateHTML.Replace(CommonLogic.GetConfigValue("EmailSiteURL"), url);
                emailtemplateHTML = emailtemplateHTML.Replace(CommonLogic.GetConfigValue("SupportEmailKey"), CommonLogic.GetConfigValue("SupportEmailValue"));
                emailtemplateHTML = emailtemplateHTML.Replace("##Currentyear##", System.DateTime.Now.Year.ToString());
                //emailtemplateHTML = emailtemplateHTML.Replace("##RoleType##", roleType);
                //emailtemplateHTML = emailtemplateHTML.Replace("##ExtraLine##", extraLine);

                MailAddress fromMail = new MailAddress(CommonLogic.GetConfigValue("FromNoReplyEmail"), CommonLogic.GetConfigValue("FromNoReplyName"));
                MailAddress toMail = new MailAddress(emailId, firstName + " " + lastName);
                SendMail(fromMail, toMail, null, null, emailtemplateHTML, subject);
            }
        }
        #endregion Mail

        #region GET SESSION VARIABLE

        public static int GetStoreID()
        {
            return ParseNativeInt(GetSessionValue("Store_Id"));
        }

        public static int GetCompanyRegionalID()
        {
            return ParseNativeInt(GetSessionValue("Company_Regional_Id"));
        }

        public static int GetCompnayID()
        {
            return ParseNativeInt(GetSessionValue("Company_Id"));
        }

        #endregion GET SESSION VARIABLE

        #region Resize Image

        //public static void ResizeImage(string SourcePath, string DestPath, int Width, int Height)
        //{
        //    Bitmap bmp = CreateThumbnail(SourcePath, Width, Height);

        //    if (bmp != null)
        //    {
        //        try
        //        {
        //            bmp.Save(DestPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        //            if (System.IO.File.Exists(SourcePath))
        //            {
        //                System.IO.File.Delete(SourcePath);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            bmp.Dispose();
        //        }
        //    }
        //}

        //public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
        //{
        //    Bitmap bmpOut = null;
        //    try
        //    {
        //        bool IsThumbWithRatio = true;
        //        Bitmap loBMP = new Bitmap(lcFilename);
        //        ImageFormat loFormat = loBMP.RawFormat;

        //        decimal lnRatio = default(decimal);
        //        int lnNewWidth = 0;
        //        int lnNewHeight = 0;

        //        //*** If the image is smaller than a thumbnail just return it
        //        //If loBMP.Width < lnWidth AndAlso loBMP.Height < lnHeight Then
        //        // lnNewWidth = loBMP.Width
        //        // lnNewHeight = loBMP.Height
        //        // bmpOut = New Bitmap(lnNewWidth, lnNewHeight)
        //        // Dim g As Graphics = Graphics.FromImage(bmpOut)
        //        // g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        //        // g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight)
        //        // g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight)

        //        // loBMP.Dispose()

        //        // Return bmpOut
        //        //End If

        //        if (IsThumbWithRatio == true)
        //        {
        //            if (loBMP.Width > loBMP.Height)
        //            {
        //                lnRatio = (decimal)lnWidth / loBMP.Width;
        //                lnNewWidth = lnWidth;
        //                decimal lnTemp = loBMP.Height * lnRatio;
        //                lnNewHeight = (int)lnTemp;
        //            }
        //            else
        //            {
        //                lnRatio = (decimal)lnHeight / loBMP.Height;
        //                lnNewHeight = lnHeight;
        //                decimal lnTemp = loBMP.Width * lnRatio;
        //                lnNewWidth = (int)lnTemp;
        //            }
        //        }
        //        else
        //        {
        //            lnNewWidth = lnWidth;
        //            lnNewHeight = lnHeight;
        //        }

        //        bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
        //        Graphics gps = Graphics.FromImage(bmpOut);
        //        gps.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //        gps.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
        //        gps.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
        //        loBMP.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    return bmpOut;
        //}

        #endregion Resize Image

        #region Applcation Logic

        public static string GetValidationMsg(string strResourceKey)
        {
            return strResourceKey.ToString().Replace("'", @"\'");
        }

        public static string GetUserStringFromType(string strUserType)
        {
            /*
        P-Patient
        D-Doctor
        C-Clinical Assistant
        F-Front Desk
        A-Collection Agent

        */
            switch (strUserType)
            {
                case "P":
                    return "Patient";

                case "D":
                    return "Doctor";

                case "C":
                    return "Clinical Assistant";

                case "F":
                    return "Front Desk";

                case "A":
                    return "Collection Agent";

                default:
                    return "Other Type";
            }
        }

        /// <summary>
        /// Check the seesion, if expire the redirect to given page.
        /// </summary>
        /// <param name="PageName">Page Name on which want to redirect.</param>
        public static void CheckSession(string PageName)
        {
            if (System.Web.HttpContext.Current.Session["User_Id"] == null)
                System.Web.HttpContext.Current.Response.Redirect(PageName);
        }

        /// <summary>
        /// Check the seesion, expired or not.
        /// </summary>
        /// <param name="PageName">Page Name on which want to redirect.</param>
        public static bool CheckSessionExist(string SessionKey)
        {
            bool status = false;
            if (System.Web.HttpContext.Current.Session[SessionKey] == null)
                status = false;
            else
                status = true;

            return status;
        }

        /// <summary>
        /// Check the session, if expire then redirect to login page.
        /// </summary>
        public static void CheckSession()
        {
            if (System.Web.HttpContext.Current.Session["User_Id"] == null)
            {
                string strReturnUrl = System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.ToString());
                System.Web.HttpContext.Current.Response.Redirect("AdminLogin.aspx?returnurl=" + strReturnUrl);
            }
        }

        /// <summary>
        /// Check the session, if expire then redirect to login page.
        /// </summary>
        public static void CheckStudentSession()
        {
            if (System.Web.HttpContext.Current.Session["Student_Id"] == null)
            {
                string strReturnUrl = System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.ToString());
                System.Web.HttpContext.Current.Response.Redirect("login.aspx?returnurl=" + strReturnUrl);
            }
        }

        /// <summary>
        /// Check logged user is student or not
        /// </summary>
        public static bool IsStudent()
        {
            if (System.Web.HttpContext.Current.Session["Student_Id"] != null)
            {
                if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Session["Student_Id"].ToString()))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check Logged user is super admin or not.
        /// </summary>
        /// <returns></returns>
        public static bool IsSuperAdmin()
        {
            if (System.Web.HttpContext.Current.Session["User_Id"] != null && System.Web.HttpContext.Current.Session["User_Type"] != null)
            {
                if (Convert.ToInt32(System.Web.HttpContext.Current.Session["User_Type"]) == 1)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check Logged user is admin or not.
        /// </summary>
        /// <returns></returns>
        public static bool IsAdmin()
        {
            if (System.Web.HttpContext.Current.Session["User_Id"] != null && System.Web.HttpContext.Current.Session["User_Type"] != null)
            {
                if (Convert.ToInt32(System.Web.HttpContext.Current.Session["User_Type"]) == 2)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check Logged user is rebecca or not.
        /// </summary>
        /// <returns></returns>
        public static bool IsRebecca()
        {
            if (System.Web.HttpContext.Current.Session["User_Id"] != null && System.Web.HttpContext.Current.Session["User_Type"] != null)
            {
                if (Convert.ToInt32(System.Web.HttpContext.Current.Session["User_Type"]) == 3)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check Logged user is mentor or not.
        /// </summary>
        /// <returns></returns>
        public static bool IsMentor()
        {
            if (System.Web.HttpContext.Current.Session["User_Id"] != null && System.Web.HttpContext.Current.Session["User_Type"] != null)
            {
                if (Convert.ToInt32(System.Web.HttpContext.Current.Session["User_Type"]) == 4)
                    return true;
            }
            return false;
        }

        #endregion Applcation Logic

        #region Application Supported Function

        /// <summary>
        /// Get the Value of application variable which pass in the parameter ApplicationItem
        /// </summary>
        /// <param name="ApplicationItem"></param>
        /// <returns></returns>
        public static string GetApplicationValue(string ApplicationItem)
        {
            string tmpString = String.Empty;
            try
            {
                tmpString = System.Web.HttpContext.Current.Application[ApplicationItem].ToString();
            }
            catch
            {
                tmpString = String.Empty;
            }
            return tmpString;
        }

        public static void SetApplicationValue(string paramName, object paramValue)
        {
            HttpContext.Current.Application[paramName] = paramValue;
        }

        #endregion Application Supported Function

        #region Get File Related

        /// <summary>
        /// Get the Value of File Icon based on strFileExtension for display into front
        /// </summary>
        /// <param name="strFileExtension"></param>
        /// <returns>String</returns>
        public static string GetFileIcon(string strFileExtension)
        {
            switch (strFileExtension.ToUpper())
            {
                case ".DOC":
                case ".DOCX":
                case ".ODT":
                    return "page_white_word.png,Word Document";

                case ".XLS":
                case ".XLSX":
                case ".ODS":
                case ".EXLS":
                    return "page_white_excel.png,Excel Document";

                case ".JPG":
                case ".GIF":
                case ".JPEG":
                case ".PNG":
                case ".IMG":
                case ".PSPIMAGE":
                case ".PSD":
                case ".BMP":
                case ".THM":
                case ".TIF":
                case ".UUV":
                    return "page_white_picture.png,Image";

                case ".PDF":
                    return "page_white_acrobat.png,PDF Document";

                case ".PPT":
                case ".PP":
                case ".PPTX":
                    return "page_white_powerpoint.png,Powerpoint";

                default:
                    return "world_link.png,Link";
            }

        }

        #endregion Get File Related

        #region Error Log Related

        public static void WriteErrorLog(Exception exception)
        {
            try
            {
                string logFilePath = HttpContext.Current.Server.MapPath(CommonLogic.GetConfigValue("ErrorLogPath"));
                if (File.Exists(logFilePath))
                {
                    string strError = "Error Caught in Application_Error event On Date : " + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + Environment.NewLine +
                            "Error in: " + HttpContext.Current.Request.Url.ToString() + Environment.NewLine +
                            "Error Message:" + exception.Message.ToString() + Environment.NewLine +
                            "Stack Trace:" + exception.StackTrace.ToString() + Environment.NewLine + Environment.NewLine +
                            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@"
                            + Environment.NewLine + Environment.NewLine;

                    StreamWriter swErrorLog = new StreamWriter(logFilePath, true);
                    swErrorLog.Write(strError);
                    swErrorLog.Close();
                    swErrorLog.Dispose();
                }
            }
            catch { }
        }

        #endregion Error Log Related

        /// <summary>
        /// used to update session first name and last name, if logged in user's data is modified
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public static void UpdateSessionValues(string firstName, string lastName)
        {
            SessionHelper.LoggedUserFirstName = firstName;
            SessionHelper.LoggedUserLastName = lastName;
        }

        /// <summary>
        /// This method will set store object to Session
        /// </summary>
        /// <param name="paramName">string</param>
        /// <param name="paramValue">object</param>
        public static void SetSessionObject(string paramName, object paramValue)
        {
            HttpContext.Current.Session[paramName] = paramValue;
        }

        #region URL Encrypt & Decrypt

        /// <summary>
        /// Encrypt the given string using AES.  The string can be decrypted using 
        /// DecryptStringAES().  The sharedSecret parameters must match.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for encryption.</param>
        public static string EncryptStringAES(string plainText)
        {
            //Set up the encryption objects
            using (AesCryptoServiceProvider acsp = GetProvider(Encoding.Default.GetBytes(CommonLogic.GetConfigValue("SharedSecret"))))
            {
                byte[] sourceBytes = Encoding.ASCII.GetBytes(plainText);
                ICryptoTransform ictE = acsp.CreateEncryptor();

                //Set up stream to contain the encryption
                MemoryStream msS = new MemoryStream();

                //Perform the encrpytion, storing output into the stream
                CryptoStream csS = new CryptoStream(msS, ictE, CryptoStreamMode.Write);
                csS.Write(sourceBytes, 0, sourceBytes.Length);
                csS.FlushFinalBlock();

                //sourceBytes are now encrypted as an array of secure bytes
                byte[] encryptedBytes = msS.ToArray(); //.ToArray() is important, don't mess with the buffer

                //return the encrypted bytes as a BASE64 encoded string
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical sharedSecret.
        /// </summary>
        /// <param name="cipherText">The text to decrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for decryption.</param>
        public static string DecryptStringAES(string cipherText)
        {
            using (AesCryptoServiceProvider acsp = GetProvider(Encoding.Default.GetBytes(CommonLogic.GetConfigValue("SharedSecret"))))
            {
                byte[] RawBytes = Convert.FromBase64String(cipherText.Replace(" ", "+"));
                ICryptoTransform ictD = acsp.CreateDecryptor();

                //RawBytes now contains original byte array, still in Encrypted state

                //Decrypt into stream
                MemoryStream msD = new MemoryStream(RawBytes, 0, RawBytes.Length);
                CryptoStream csD = new CryptoStream(msD, ictD, CryptoStreamMode.Read);
                //csD now contains original byte array, fully decrypted

                //return the content of msD as a regular string
                // csD.FlushFinalBlock();
                return (new StreamReader(csD)).ReadToEnd();
            }

        }

        private static AesCryptoServiceProvider GetProvider(byte[] key)
        {
            AesCryptoServiceProvider result = new AesCryptoServiceProvider();
            result.BlockSize = 128;
            result.KeySize = 128;
            result.Mode = CipherMode.CBC;
            result.Padding = PaddingMode.PKCS7;

            result.GenerateIV();
            result.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            byte[] RealKey = GetKey(key, result);
            result.Key = RealKey;
            // result.IV = RealKey;
            return result;
        }

        private static byte[] GetKey(byte[] suggestedKey, SymmetricAlgorithm p)
        {
            byte[] kRaw = suggestedKey;
            List<byte> kList = new List<byte>();

            for (int i = 0; i < p.LegalKeySizes[0].MinSize; i += 8)
            {
                kList.Add(kRaw[(i / 8) % kRaw.Length]);
            }
            byte[] k = kList.ToArray();
            return k;
        }
        #endregion

        public static object SetSessionValue(string p)
        {
            throw new NotImplementedException();
        }
    }
}