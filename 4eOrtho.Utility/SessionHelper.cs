using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace _4eOrtho.Utility
{
    public static class SessionHelper
    {
        public static string CurrentCultureName
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("CurrentCultureName")))
                {
                    CommonLogic.SetSessionValue("CurrentCultureName", "");
                }
                return CommonLogic.GetSessionValue("CurrentCultureName");
            }
            set
            {
                CommonLogic.SetSessionValue("CurrentCultureName", value);
            }
        }
        public static int LanguageId
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("LanguageId")))
                {
                    CommonLogic.SetSessionObject("LanguageId", 1);
                }
                return Convert.ToInt32(CommonLogic.GetSessionObject("LanguageId"));
            }
            set
            {
                CommonLogic.SetSessionObject("LanguageId", value);
            }
        }
        public static string LoggedUserEmailAddress
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("LoggedUserEmailAddress")))
                {
                    CommonLogic.SetSessionValue("LoggedUserEmailAddress", "");
                }
                return CommonLogic.GetSessionValue("LoggedUserEmailAddress");
            }
            set
            {
                CommonLogic.SetSessionValue("LoggedUserEmailAddress", value);
            }
        }
        public static string LoggedUserFirstName
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("LoggedUserFirstName")))
                {
                    CommonLogic.SetSessionValue("LoggedUserFirstName", string.Empty);
                }
                return CommonLogic.GetSessionValue("LoggedUserFirstName");
            }
            set
            {
                CommonLogic.SetSessionValue("LoggedUserFirstName", value);
            }
        }
        public static string LoggedUserLastName
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("LoggedUserLastName")))
                {
                    CommonLogic.SetSessionValue("LoggedUserLastName", string.Empty);
                }
                return CommonLogic.GetSessionValue("LoggedUserLastName");
            }
            set
            {
                CommonLogic.SetSessionValue("LoggedUserLastName", value);
            }
        }
        public static string LoggedUserType
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("LoggedUserType")))
                {
                    CommonLogic.SetSessionValue("LoggedUserType", string.Empty);
                }
                return CommonLogic.GetSessionValue("LoggedUserType");
            }
            set
            {
                CommonLogic.SetSessionValue("LoggedUserType", value);
            }
        }
        public static long HOF
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("HOF")))
                {
                    CommonLogic.SetSessionValue("HOF", 0);
                }
                return Convert.ToInt64(CommonLogic.GetSessionValue("HOF"));
            }
            set
            {
                CommonLogic.SetSessionValue("HOF", value);
            }
        }
        public static bool IsAbleToNavigate
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("IsAbleToNavigate")))
                {
                    CommonLogic.SetSessionValue("IsAbleToNavigate", true);
                }
                return Convert.ToBoolean(CommonLogic.GetSessionValue("IsAbleToNavigate"));
            }
            set
            {
                CommonLogic.SetSessionValue("IsAbleToNavigate", value);
            }
        }
        public static bool IsAttemptFirstLogin
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("IsAttemptFirstLogin")))
                {
                    CommonLogic.SetSessionValue("IsAttemptFirstLogin", true);
                }
                return Convert.ToBoolean(CommonLogic.GetSessionValue("IsAttemptFirstLogin"));
            }
            set
            {
                CommonLogic.SetSessionValue("IsAttemptFirstLogin", value);
            }
        }
        public static bool IsEuropeanTeethNumbering
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("IsEuropeanTeethNumbering")))
                {
                    CommonLogic.SetSessionValue("IsEuropeanTeethNumbering", true);
                }
                return Convert.ToBoolean(CommonLogic.GetSessionValue("IsEuropeanTeethNumbering"));
            }
            set
            {
                CommonLogic.SetSessionValue("IsEuropeanTeethNumbering", value);
            }
        }
        public static long PatientId
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("PatientId")))
                {
                    CommonLogic.SetSessionValue("PatientId", 0);
                }
                return Convert.ToInt64(CommonLogic.GetSessionValue("PatientId"));
            }
            set
            {
                CommonLogic.SetSessionValue("PatientId", value);
            }
        }
        public static string LoggedRollTypes
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("RollTypes")))
                {
                    CommonLogic.SetSessionValue("RollTypes", string.Empty);
                }
                return CommonLogic.GetSessionValue("RollTypes");
            }
            set
            {
                CommonLogic.SetSessionValue("RollTypes", value);
            }
        }
        public static List<PageList> UserPageRights
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("UserPageRights")))
                {
                    CommonLogic.SetSessionValue("UserPageRights", string.Empty);
                }
                return CommonLogic.GetSessionValueArrayList("UserPageRights");
            }
            set
            {
                CommonLogic.SetSessionValue("UserPageRights", value);
            }
        }
        public static DateTime AppointmentSelectedDate
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("AppointmentSelectedDate")))
                {
                    CommonLogic.SetSessionValue("AppointmentSelectedDate", new DateTime());
                }
                return CommonLogic.GetSessionDateTimeValue("AppointmentSelectedDate");
            }
            set { CommonLogic.SetSessionValue("AppointmentSelectedDate", value); }
        }
        public static string IsRedirectedFrom
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("RedirectedFrom")))
                {
                    CommonLogic.SetSessionValue("RedirectedFrom", new DateTime());
                }
                return CommonLogic.GetSessionValue("RedirectedFrom");
            }
            set { CommonLogic.SetSessionValue("RedirectedFrom", value); }
        }
        public static string TreatmentPlanDesignPattern
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("TreatmentPlanDesignPattern")))
                {
                    CommonLogic.SetSessionValue("TreatmentPlanDesignPattern", string.Empty);
                }
                return CommonLogic.GetSessionValue("TreatmentPlanDesignPattern");
            }
            set { CommonLogic.SetSessionValue("TreatmentPlanDesignPattern", value); }
        }
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("ConnectionString")))
                {
                    CommonLogic.SetSessionValue("ConnectionString", string.Empty);
                }
                return CommonLogic.GetSessionValue("ConnectionString");
            }
            set { CommonLogic.SetSessionValue("ConnectionString", value); }
        }
        /// <summary>
        /// Description : Method for getting selected Language Id
        /// </summary>
        public static int LanguageID
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("LanguageID")))
                {
                    CommonLogic.SetSessionValue("LanguageID", 0);
                }
                return Convert.ToInt32(CommonLogic.GetSessionValue("LanguageID"));
            }
            set
            {
                CommonLogic.SetSessionValue("LanguageID", value);
            }
        }
        /// <summary>
        /// This property will Get/Set LoggedAdminUserID to Session
        /// </summary>
        public static long LoggedAdminUserID
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("LoggedAdminUserID")))
                {
                    CommonLogic.SetSessionObject("LoggedAdminUserID", 0);
                }
                return Convert.ToInt32(CommonLogic.GetSessionObject("LoggedAdminUserID"));
            }
            set
            {
                CommonLogic.SetSessionObject("LoggedAdminUserID", value);
            }
        }
        /// <summary>
        /// This property will Get/Set LoggedAdminUserName to Session
        /// </summary>
        public static string LoggedAdminEmailAddress
        {
            get
            {
                return CommonLogic.GetSessionValue("LoggedAdminEmailAddress");
            }
            set
            {
                CommonLogic.SetSessionValue("LoggedAdminEmailAddress", value);
            }
        }
        /// <summury>
        /// This property is use to check SuperAdmin.
        /// </summury>
        public static bool IsSuperAdmin
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("IsSuperAdmin")))
                {
                    CommonLogic.SetSessionObject("IsSuperAdmin", false);
                }
                return Convert.ToBoolean(CommonLogic.GetSessionObject("IsSuperAdmin"));
            }
            set
            {
                CommonLogic.SetSessionObject("IsSuperAdmin", value);
            }
        }
        /// <summary>
        /// This property will Get/Set UserAdminDisplayName to Session
        /// </summary>
        public static string UserDisplayName
        {
            get
            {
                return CommonLogic.GetSessionValue("UserAdminDisplayName");
            }
            set
            {
                CommonLogic.SetSessionValue("UserAdminDisplayName", value);
            }
        }
        public static string UserLoginSession
        {
            get
            {
                return CommonLogic.GetSessionValue("UserLoginSession");
            }
            set
            {
                CommonLogic.SetSessionValue("UserLoginSession", value);
            }
        }
        public static ArrayList UserDisplayPageRights
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("UserDisplayPageRights")))
                {
                    CommonLogic.SetSessionValue("UserDisplayPageRights", string.Empty);
                }
                return CommonLogic.GetSessionValueListByArray("UserDisplayPageRights");
            }
            set
            {
                CommonLogic.SetSessionValue("UserDisplayPageRights", value);
            }
        }
        /// <summary>
        /// This property will Get/Set PayPalServiceResponseErrors to Session
        /// </summary>
        public static object PayPalServiceResponseErrors
        {
            get
            {
                return CommonLogic.GetSessionObject("PayPalServiceResponseErrors");
            }
            set
            {
                CommonLogic.SetSessionObject("PayPalServiceResponseErrors", value);
            }
        }
        /// <summary>
        /// This property will Get/Set TransactionId to Session
        /// </summary>
        public static string TransactionId
        {
            get
            {
                return CommonLogic.GetSessionValue("TransactionId");
            }
            set
            {
                CommonLogic.SetSessionValue("TransactionId", value);
            }
        }
        /// <summary>
        /// This property will Get/Set TransactionTime to Session
        /// </summary>
        public static string TransactionTime
        {
            get
            {
                return CommonLogic.GetSessionValue("TransactionTime");
            }
            set
            {
                CommonLogic.SetSessionValue("TransactionTime", value);
            }
        }
        /// <summary>
        /// This property will Get/Set PaymentAmount to Session
        /// </summary>
        public static string PaymentAmount
        {
            get
            {
                return CommonLogic.GetSessionValue("PaymentAmount");
            }
            set
            {
                CommonLogic.SetSessionValue("PaymentAmount", value);
            }
        }
        public static string ReworkORRetainer
        {
            get
            {
                return CommonLogic.GetSessionValue("ReworkORRetainer");
            }
            set
            {
                CommonLogic.SetSessionValue("ReworkORRetainer", value);
            }
        }
        public static bool IsSendPatientMail
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("IsSendPatientMail")))
                {
                    CommonLogic.SetSessionValue("IsSendPatientMail", true);
                }
                return Convert.ToBoolean(CommonLogic.GetSessionValue("IsSendPatientMail"));
            }
            set
            {
                CommonLogic.SetSessionValue("IsSendPatientMail", value);
            }
        }
        public static bool IsPayment
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("IsPayment")))
                {
                    CommonLogic.SetSessionValue("IsPayment", true);
                }
                return Convert.ToBoolean(CommonLogic.GetSessionValue("IsPayment"));
            }
            set
            {
                CommonLogic.SetSessionValue("IsPayment", value);
            }
        }

        public static string CaseCharge
        {
            get
            {
                return CommonLogic.GetSessionValue("CaseCharge");
            }
            set
            {
                CommonLogic.SetSessionValue("CaseCharge", value);
            }
        }
        public static string CaseNo
        {
            get
            {
                return CommonLogic.GetSessionValue("CaseNo");
            }
            set
            {
                CommonLogic.SetSessionValue("CaseNo", value);
            }
        }
        public static string CaseType
        {
            get
            {
                return CommonLogic.GetSessionValue("CaseType");
            }
            set
            {
                CommonLogic.SetSessionValue("CaseType", value);
            }
        }
        public static string ShippingCharge
        {
            get
            {
                return CommonLogic.GetSessionValue("ShippingCharge");
            }
            set
            {
                CommonLogic.SetSessionValue("ShippingCharge", value);
            }
        }
        public static long PackageId
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("PackageId")))
                {
                    CommonLogic.SetSessionObject("PackageId", 0);
                }
                return Convert.ToInt32(CommonLogic.GetSessionObject("PackageId"));
            }
            set
            {
                CommonLogic.SetSessionObject("PackageId", value);
            }
        }

        public static long SupplyOrderId
        {
            get
            {
                if (string.IsNullOrEmpty(CommonLogic.GetSessionValue("SupplyOrderId")))
                {
                    CommonLogic.SetSessionObject("SupplyOrderId", 0);
                }
                return Convert.ToInt32(CommonLogic.GetSessionObject("SupplyOrderId"));
            }
            set
            {
                CommonLogic.SetSessionObject("SupplyOrderId", value);
            }
        }
    }
    public class PageList
    {
        public string PageName { get; set; }
        public string UserType { get; set; }
    }
    public class PageRight
    {
        public string RedirectPageName { get; set; }
        public bool IsLogIn { get; set; }
    }
}