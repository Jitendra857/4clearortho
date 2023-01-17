using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _4eOrtho.UserControls
{
    public partial class HTMLEditor : System.Web.UI.UserControl
    {
        public string Mode { get; set; }
        protected string modeHtmlCode { get; set; }

        public bool _limit = false;
        public bool Limit
        {
            get { return this._limit; }
            set { this._limit = value; }
        }
        public string Text
        {
            get { return editor.Text; }
            set { editor.Text = value; }
        }
        private long _maxLength = long.MaxValue;
        public long MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }

        private int _rows = 15;
        public int Rows
        {
            get { return this._rows; }
            set { this._rows = value; }
        }
        private int _cols = 60;
        public int Cols
        {
            get { return this._cols; }
            set { this._cols = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.editor.Rows = this._rows;
            this.editor.Columns = this._cols;

            HtmlEditInit();
        }

        public string StylePath
        {
            get
            {
                return ResolveUrl("~/App_Themes/test/stylesheet.css");
            }
        }

        public string JSPath
        {
            get
            {
                return ResolveUrl("~/Scripts/tinymce/");
            }
        }

        protected string LanguageName
        {
            get
            {
                string name = "en";
                // string languageNum = System.Web.Configuration.WebConfigurationManager.AppSettings["EnumLanguage"].ToString();
                string languageNum = "en";
                switch (languageNum)
                {
                    case "0":
                        break;              //English
                    case "1":
                        name = "zh";//SimplifiedChinese
                        break;
                    case "2":
                        name = "zhtw";//TraditionalChinese
                        break;
                    case "3":
                        name = "es";   //Spanish
                        break;
                    case "4":
                        name = "jp";   //Japaness
                        break;
                    case "5":
                        name = "fr";   //French
                        break;
                    case "6":
                        name = "de";   //German
                        break;
                    case "7":
                        name = "ar";   //Arabic
                        break;
                    case "8":
                        name = "pg";   //Portuguese
                        break;
                    case "9":
                        name = "ko";   //Korean
                        break;
                    case "10":
                        name = "it";       //Italian
                        break;
                    case "11":
                        name = "ru";       //Russian
                        break;
                    case "12":
                        name = "sw";       //Swedish
                        break;
                    case "13":
                        name = "du";    //Dutch
                        break;
                    default:
                        break;
                }
                return name;
            }
        }

        public bool _ifAllowInsertImage = true;
        public bool _ifAllowInsertLink = true;
        public bool IfAllowInsertImage
        {
            get { return _ifAllowInsertImage; }
            set { _ifAllowInsertImage = value; }
        }
        public bool IfAllowInsertLink
        {
            get { return _ifAllowInsertLink; }
            set { _ifAllowInsertLink = value; }
        }
        protected string ImageAndLink
        {
            get { return (IfAllowInsertLink ? ",link,unlink,|" : "") + (IfAllowInsertImage ? ",image" : ""); }
        }
        /*2.0*/
        private void HtmlEditInit()
        {
            HtmlEditModeInit();
        }
        private void HtmlEditModeInit()
        {
            /*default*/
            if (string.IsNullOrEmpty(Mode))
            {
                modeHtmlCode = "mode: \"textareas\",";
            }
            /*band one control by id porperty*/
            else if (Mode.ToLower() == "bandbyidorname")
            {
                modeHtmlCode = string.Format("mode: \"exact\","
                                + "elements: \"{0}\",", this.editor.ClientID);
            }
            /*band one control by type*/
            else if (Mode.ToLower() == "bandbytype")
            {
                modeHtmlCode = "mode: \"textareas\",";
            }
            else if (Mode.ToLower() == "text")
            {
                modeHtmlCode = "";
            }
        }   
      
    }
}