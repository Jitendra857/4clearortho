using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Utility;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Web;

namespace _4eOrtho
{
    public partial class Brochure : System.Web.UI.Page
    {
        #region Events

        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLoginSession"] != null)
            {
                CurrentSession currentSession = new CurrentSession();
                currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession.UserType == UserType.D.ToString() || currentSession.UserType == UserType.S.ToString())
                {
                  //  dvName.InnerHtml = currentSession.DoctorName;
                  //  dvContact.InnerHtml = currentSession.EmailId;
                }
            }
            PagesEntity pagesEntity = new PagesEntity();
            PageDetail pageWithDetail = pagesEntity.GetPageDetailByMenuNameandLanguage("4ClearOrtho-Address", SessionHelper.LanguageId);
            if (pageWithDetail != null)
            {
                ltrAddress.Text = pageWithDetail.PageContent;
            }
            //fillPDFForm();
            //FillPDFDoctorInformation();
        }

        #endregion

        #region Helper

        /// <summary>
        /// fill pdf file with merging text
        /// </summary>
        private void fillPDFForm()
        {
            string pdfFileName = "PatientBrochure" + Guid.NewGuid().ToString() + ".pdf";
            string newFile = Server.MapPath("~/TempFiles/" + pdfFileName);
            string pdfTemplate = Server.MapPath("~/PDF/PatientBrochure.pdf");
            File.Copy(pdfTemplate, newFile);

            using (PdfStamper pdfStamper = new PdfStamper(new PdfReader(pdfTemplate), new FileStream(newFile, FileMode.Open, FileAccess.Write), new char(), true))
            {
                TextField txtPatientName = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(200, 60, 80, 1100), "PatientName");
                txtPatientName.Text = "Patient Name :";
                txtPatientName.FontSize = 10;
                pdfStamper.AddAnnotation(txtPatientName.GetTextField(), 1);

                TextField txtPatientNameData = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(300, 60, 180, 1100), "PatientNameData");
                txtPatientNameData.Text = "John Carter Medinson";
                txtPatientNameData.FontSize = 10;
                pdfStamper.AddAnnotation(txtPatientNameData.GetTextField(), 1);

                TextField txtAmount = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(250, 60, 80, 1050), "Amount");
                txtAmount.Text = "Estimated Amount :";
                txtAmount.FontSize = 10;
                pdfStamper.AddAnnotation(txtAmount.GetTextField(), 1);

                TextField txtAmountData = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(300, 60, 180, 1050), "AmounData");
                txtAmountData.Text = string.Format("{0:0.00}", 600.00);
                txtAmountData.FontSize = 10;
                pdfStamper.AddAnnotation(txtAmountData.GetTextField(), 1);

                TextField txtPatientEmail = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(400, 60, 290, 1100), "PatientMail");
                txtPatientEmail.Text = "Patient Email :";
                txtPatientEmail.FontSize = 10;
                pdfStamper.AddAnnotation(txtPatientEmail.GetTextField(), 1);

                TextField txtDoctorEmailData = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(500, 60, 370, 1100), "DoctorEmailData");
                txtDoctorEmailData.Text = "bhargav3126@yahoo.com";
                txtDoctorEmailData.FontSize = 10;
                pdfStamper.AddAnnotation(txtDoctorEmailData.GetTextField(), 1);

                TextField txtBrochureDate = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(400, 60, 290, 1050), "BrochureDate");
                txtBrochureDate.Text = "Brochure Date :";
                txtBrochureDate.FontSize = 10;
                pdfStamper.AddAnnotation(txtBrochureDate.GetTextField(), 1);

                TextField txtBrochureDateData = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(550, 60, 370, 1050), "BrochureDateData");
                txtBrochureDateData.Text = "10/11/2014";
                txtBrochureDateData.FontSize = 10;
                pdfStamper.AddAnnotation(txtBrochureDateData.GetTextField(), 1);

                pdfStamper.Close();
                DownloadFile(Response, newFile);
            }
            DownloadFile(Response, newFile);
        }

        /// <summary>
        /// Method to fill doctor information to pdf.
        /// </summary>
        public void FillPDFDoctorInformation()
        {

            String pathin = Server.MapPath("~/PDF/PatientBrochure.pdf");
            string pdfFileName = "PatientBrochure" + Guid.NewGuid().ToString() + ".pdf";
            string newFile = Server.MapPath("~/TempFiles/" + pdfFileName);
            string pdfTemplate = Server.MapPath("~/PDF/PatientBrochure.pdf");
            String pathout = newFile;
            //create a document object
            //var doc = new Document(PageSize.A4);
            //create PdfReader object to read from the existing document
            PdfReader reader = new PdfReader(pathin);
            //select two pages from the original document
            reader.SelectPages("1-2");
            //create PdfStamper object to write to get the pages from reader
            PdfStamper stamper = new PdfStamper(reader, new FileStream(pathout, FileMode.Create));
            // PdfContentByte from stamper to add content to the pages over the original content
            PdfContentByte pbover = stamper.GetOverContent(1);
            //add content to the page using ColumnText
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Patient Name :"), 80, 565, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("John Carter Madinson"), 200, 565, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Estimated Amount :"), 80, 545, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("6000.00"), 200, 545, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Patient Email :"),340, 565, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("patient@vervsys.local"), 480, 565, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Print Date(mm/dd/yyyy) :"), 340, 545, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("12/15/2014"), 480, 545, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("doctor user", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.WHITE)), 85, 510, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("(540) 657-4733", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)), 85, 492, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("www.4clearortho.com", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)), 85, 480, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("4CLEAR ORTHO", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, new BaseColor(88, 87, 71))), 370, 140, 0); // doctor name
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("4clearOrtho.com", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, new BaseColor(88, 87, 71))), 378, 130, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("481 Garrisonville Rd Ste 101", new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 350, 120, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Stafford, VA 22554 USA", new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 360, 110, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("540 645-4333", new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 380, 70, 0);
            // PdfContentByte from stamper to add content to the pages under the original content
            PdfContentByte pbunder = stamper.GetUnderContent(1);
            
            reader = new PdfReader(pathin);
            //select two pages from the original document
            reader.SelectPages("2-2");
            // PdfContentByte from stamper to add content to the pages over the original content
            pbover = stamper.GetOverContent(2);
            //add content to the page using ColumnText
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("doctor user", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.WHITE)), 85, 540, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("(540) 657-4733", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)), 85, 510, 0);
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("www.4clearortho.com", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)), 85, 500, 0);
            // PdfContentByte from stamper to add content to the pages under the original content
            pbunder = stamper.GetUnderContent(2);

            stamper.Close();
            DownloadFile(Response, pathout);
        }

        /// <summary>
        /// Download PDF File
        /// </summary>
        /// <param name="response">Response object</param>
        /// <param name="fileRelativePath">File physical path on server</param>
        public static void DownloadFile(HttpResponse response, string filePath)
        {
            string contentType = "";
            string fileExt = Path.GetExtension(filePath).Split('.')[1].ToLower();
            if (fileExt == "pdf")
                contentType = "Application/pdf";
            response.ContentType = contentType;
            response.AppendHeader("content-disposition", "attachment; filename=" + (new FileInfo(filePath)).Name);
            response.WriteFile(filePath);
            response.End();
        }

        #endregion
    }
}