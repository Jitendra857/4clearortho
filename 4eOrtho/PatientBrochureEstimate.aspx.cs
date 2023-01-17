using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using iTextSharp.text;
using iTextSharp.text.pdf;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class PatientBrochureEstimate : PageBase
    {
        #region Declaration

        private ILog logger = log4net.LogManager.GetLogger(typeof(PatientBrochureEstimate));

        #endregion Declaration

        #region Events

        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DeleteAllFilesFromFolder();
            }
        }

        /// <summary>
        /// Event to save patient brochure estimate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SavePatientBrochureEstimate();
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Method to save patient brochure estimate.
        /// </summary>
        private void SavePatientBrochureEstimate()
        {
            try
            {
                CurrentSession currentSession = (CurrentSession)Session["UserLoginSession"];
                if (currentSession != null)
                {
                    logger.Info("Request to save data in session");
                    PatientBrochureEntity patientBrochure = new PatientBrochureEntity();
                    PatientBrochure brochure = new PatientBrochure();
                    brochure.PatientName = txtPatientName.Text;
                    brochure.PatientEmail = txtEmailAddress.Text;
                    brochure.Amount = Convert.ToDecimal(txtAmount.Text);
                    patientBrochure.Save(brochure);
                    PatientBrochureDetails patientBrochureDetails = new PatientBrochureDetails();
                    patientBrochureDetails.PatientName = txtPatientName.Text;
                    patientBrochureDetails.EmailAddress = txtEmailAddress.Text;
                    patientBrochureDetails.Amount = Convert.ToDecimal(txtAmount.Text);
                    patientBrochureDetails.BrochureDate = BaseEntity.GetServerDateTime;

                    CommonLogic.SetSessionValue("PatientBrochure", patientBrochureDetails);
                    logger.Info("Session Saved SuccessFully");
                    ClearPatientBrochure();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "ResetForm();", true);
                    //btnSubmit.Attributes.Add("OnClientClick","RestForm()");

                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Method to clear patient brochure controls.
        /// </summary>
        public void ClearPatientBrochure()
        {
            txtAmount.Text = "";
            txtEmailAddress.Text = "";
            txtPatientName.Text = "";
        }

        /// <summary>
        /// Method to fill pdf doctor information.
        /// </summary>
        public void FillPDFDoctorInformation()
        {
            PatientBrochureDetails patientBrochureDetails = new PatientBrochureDetails();
            if (CommonLogic.GetSessionValue("PatientBrochure") != null)
                patientBrochureDetails = (PatientBrochureDetails)CommonLogic.GetSessionObject("PatientBrochure");

            if (Session["UserLoginSession"] != null)
            {
                CurrentSession currentSession = new CurrentSession();
                currentSession = (CurrentSession)Session["UserLoginSession"];
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
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(patientBrochureDetails.PatientName), 200, 565, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Estimated Amount :"), 80, 545, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(string.Format("{0:0.00}", patientBrochureDetails.Amount)), 200, 545, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Patient Email :"), 340, 565, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(patientBrochureDetails.EmailAddress), 480, 565, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Print Date(mm/dd/yyyy) :"), 340, 545, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(patientBrochureDetails.BrochureDate.ToString("MM/dd/yyyy")), 480, 545, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorName, new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.WHITE)), 85, 510, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorMobile, new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)), 85, 492, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("4clearortho.com", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)), 85, 480, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorName, new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, new BaseColor(88, 87, 71))), 340, 140, 0); // doctor name
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.EmailId, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 340, 125, 0);
                //ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("481 Garrisonville Rd Ste 101", new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 350, 120, 0);
                //ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Stafford, VA 22554 USA", new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 360, 110, 0);
                //ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorMobile, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 340, 70, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorMobile, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 340, 110, 0);
                // PdfContentByte from stamper to add content to the pages under the original content
                PdfContentByte pbunder = stamper.GetUnderContent(1);


                reader = new PdfReader(pathin);
                //select two pages from the original document
                reader.SelectPages("2-2");
                // PdfContentByte from stamper to add content to the pages over the original content
                pbover = stamper.GetOverContent(2);
                //add content to the page using ColumnText
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorName, new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.WHITE)), 85, 525, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.EmailId, new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)), 85, 500, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("4clearortho.com", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)), 85, 510, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("4clearortho.com", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, new BaseColor(88, 87, 71))), 680, 510, 0);
                // PdfContentByte from stamper to add content to the pages under the original content
                pbunder = stamper.GetUnderContent(2);

                stamper.Close();
                DownloadFile(Response, pathout);
            }
        }

        /// <summary>
        /// Delete All Files which are 1 day older 
        /// </summary>
        public void DeleteAllFilesFromFolder()
        {
            if (Directory.Exists(Server.MapPath("~/TempFiles/")))
            {
                DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/TempFiles/"));
                List<FileInfo> lstFileInfo = directory.GetFiles().ToList();
                if (lstFileInfo != null && lstFileInfo.Count > 0)
                {
                    foreach (FileInfo files in directory.GetFiles())
                    {
                        DateTime modifiedDate = System.IO.File.GetLastWriteTime(directory + files.Name);
                        if (modifiedDate < DateTime.Now.AddDays(-1))
                        {
                            File.Delete(directory + files.Name);
                        }
                    }
                }
            }
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