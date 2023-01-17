using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace _4eOrtho.Utility
{
    #region Enums

    /// <summary>
    ///  - Type of logging to perform.
    /// </summary>
    public enum LogType
    {
        // Debug only logging.
        Debug = 0,

        // Warning logging.
        Warning = 1,

        // Error message logging.
        Error = 2,

        // Application critical logging.
        Critical = 3
    }

    #endregion Enums

    /// <summary>
    ///  - Handles all log writing.
    /// </summary>
    public static class CommonLog
    {
        #region Settings Proerties

        public static string LogPath
        {
            get { return System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["LogPath"]); }
        }

        public static string ApplicationName
        {
            get { return ConfigurationManager.AppSettings["ApplicationName"]; }
        }

        #endregion Settings Proerties

        #region Constants

        // Extension of the log file.
        private const string LOG_EXT = "log";

        #endregion Constants

        #region Methods

        /// <summary>
        ///  - Writes a message to the error log.
        /// </summary>
        /// <param name="message">Message to write.</param>
        public static void Write(string message)
        {
            Write(message + "\r\n", LogType.Error);
        }

        /// <summary>
        ///  - Writes a message to the specified log.
        /// </summary>
        /// <param name="message">Message to write.</param>
        /// <param name="type">Type of log to write to.</param>
        public static void Write(string message, LogType type)
        {
            // Initialize log file.
            string logFileName = Initialize(type);
            // Write out message to the log.
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(logFileName, true);
                writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - " + message);
            }
            catch (Exception)
            {
                // Do nothing.
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        ///  - Writes a message and an exception to the error log.
        /// </summary>
        /// <param name="message">Message to write.</param>
        /// <param name="ex">Exception to write.</param>
        public static void Write(string message, Exception ex)
        {
            Write(message + "\r\n", ex, LogType.Error);
        }

        /// <summary>
        ///  - Writes a message and an exception to the specified log.
        /// </summary>
        /// <param name="message">Message to write.</param>
        public static void Write(string message, Exception ex, LogType type)
        {
            Write(message + "\r\n" + ex, type);
        }

        /// <summary>
        ///  - Initializes the log file being written to.
        /// </summary>
        /// <param name="type">Type of log file to initialize.</param>
        /// <returns>Name of the log file to write to.</returns>
        private static string Initialize(LogType type)
        {
            string logFileName = string.Format("{0}{1}.{2}.{3}.{4}.{5}", LogPath,
                DateTime.Now.ToString("yyyyMMdd"), ApplicationName, type, Environment.MachineName, LOG_EXT);
            // Verify file exists.
            if (!File.Exists(logFileName))
            {
                // Create directory if it doesn't exist.
                if (!Directory.Exists(LogPath))
                    Directory.CreateDirectory(LogPath);
                // Create the log file for the day.
                StreamWriter writer = null;
                try
                {
                    writer = File.CreateText(logFileName);
                    // Output file header.
                    writer.WriteLine(string.Format(
@"-------------------------- {0} {1} Log - {2} - {3} --------------------------
", ApplicationName, type, DateTime.Now.ToShortDateString(), Environment.MachineName));
                }
                catch (Exception)
                {
                    // Do nothing.
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }

            return logFileName;
        }

        #endregion Methods

        #region Common function

        /// <summary>
        /// This function will generate ASP.net Lable control
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="text"></param>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        public static Label CreateLabel(string fieldName, string text, string cssClass)
        {
            Label lblGenericLable = new Label();
            lblGenericLable.ID = fieldName;
            lblGenericLable.Text = text;
            if (cssClass != string.Empty)
                lblGenericLable.CssClass = cssClass;
            return lblGenericLable;
        }

        /// <summary>
        /// This function will generate ASP.net checkbox control
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="cssClass"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static CheckBox CreateCheckeBox(string fieldName, string cssClass, string defaultvalue, bool isControlEnable)
        {
            CheckBox checekbox = new CheckBox();
            checekbox.Enabled = isControlEnable;
            checekbox.ID = fieldName;
            checekbox.Visible = true;

            if (!string.IsNullOrEmpty(defaultvalue))
            {
                checekbox.Checked = Convert.ToBoolean(defaultvalue);
            }

            if (cssClass != string.Empty)
                checekbox.CssClass = cssClass;
            return checekbox;
        }

        /// <summary>
        /// This function will generate Asp.net checkbox control
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="cssclass"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static RadioButton createRadiobutton(string fieldName, string cssclass, string group, string defaultvalue, bool isChecked, bool isControlEnable)
        {
            RadioButton radiobutton = new RadioButton();
            radiobutton.Enabled = isControlEnable;
            radiobutton.ID = fieldName;
            radiobutton.Checked = isChecked;
            radiobutton.GroupName = group;
            radiobutton.Text = defaultvalue;
            radiobutton.Visible = true;
            if (cssclass != string.Empty)
                radiobutton.CssClass = cssclass;
            return radiobutton;
        }

        /// <summary>
        /// This function will generate ASP.net TextBox control
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="text"></param>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        public static TextBox CreateTextBox(string fieldName, string text, string cssClass, TextBoxMode txtMode, bool isControlEnable, int maxLength = 0)
        {
            TextBox txtGenericTextBox = new TextBox();
            txtGenericTextBox.Enabled = isControlEnable;
            txtGenericTextBox.ID = fieldName;
            txtGenericTextBox.TextMode = txtMode;
            txtGenericTextBox.Visible = true;
            if (maxLength != 0)
                txtGenericTextBox.MaxLength = maxLength;
            if (cssClass.Contains("date"))
            {
                if (text != string.Empty)
                    txtGenericTextBox.Text = Convert.ToDateTime(text).ToLocalTime().ToString("dd/MM/yyyy");
            }
            else
                txtGenericTextBox.Text = text;

            if (cssClass.Contains("money"))
            {
                txtGenericTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
                txtGenericTextBox.MaxLength = 15;
            }
            if (cssClass != string.Empty)
                txtGenericTextBox.CssClass = cssClass;
            return txtGenericTextBox;
        }

        #endregion Common function



        #region Common login function
        public static bool HasLoginUser()
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}