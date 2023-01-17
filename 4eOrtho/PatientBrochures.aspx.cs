using _4eOrtho.BAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using iTextSharp.text;
using iTextSharp.text.pdf;
using log4net;
using System;
using System.IO;
using System.Web;

namespace _4eOrtho
{
    public partial class PatientBrochures : PageBase
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(PatientBrochures));
        #endregion Declaration

        #region Event

        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserLoginSession"] != null)
                {
                    BindPatientBrochureDetails();
                    //fillPDFForm();
                    DeleteAllFilesFromFolder();
                }
                else
                {
                    PageRedirect("Home.aspx");
                }
            }
            //FillPDFDoctorInformation();
        }

        /// <summary>
        /// Event to download pdf.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            FillPDFDoctorInformation();
        }

        #endregion

        #region Helper

        /// <summary>
        /// Bind Patient Brochure Details
        /// </summary>
        public void BindPatientBrochureDetails()
        {
            try
            {

                PatientBrochureDetails patientBrochureDetails = new PatientBrochureDetails();
                if (CommonLogic.GetSessionValue("PatientBrochure") != null)
                    patientBrochureDetails = (PatientBrochureDetails)CommonLogic.GetSessionObject("PatientBrochure");

                if (Session["UserLoginSession"] != null)
                {
                    CurrentSession currentSession = new CurrentSession();
                    currentSession = (CurrentSession)Session["UserLoginSession"];
                    if (currentSession.UserType == UserType.D.ToString() || currentSession.UserType == UserType.S.ToString())
                    {
                        dvName.InnerHtml = currentSession.DoctorName;
                        dvContact.InnerHtml = currentSession.EmailId;
                        dvMobilepage1.InnerHtml = currentSession.DoctorMobile + "&nbsp;";
                        dvNameDoctor.InnerHtml = currentSession.DoctorName;
                        dvContactdoctor.InnerHtml = currentSession.EmailId;
                        dvNamePage2.InnerHtml = currentSession.DoctorName;
                        dvdoctorEmpailpage2.InnerHtml = currentSession.EmailId;
                        dvmobile.InnerHtml = currentSession.DoctorMobile + "&nbsp;";
                        dvmobile2.InnerHtml = currentSession.DoctorMobile + "&nbsp;";
                        string address = currentSession.DoctorStreet + "&nbsp;";
                        string addressLine2 = currentSession.DoctorCity + " " + currentSession.DoctorState + "&nbsp;";
                        string addressLine3 = currentSession.DoctorCountry + " " + currentSession.DoctorZipcode + "&nbsp;";
                        divaddress.InnerHtml = address + "&nbsp;";
                        divaddress2.InnerHtml = addressLine2 + "&nbsp;";
                        divaddress3.InnerHtml = addressLine3 + "&nbsp;";
                        dvadressmid.InnerHtml = address + "&nbsp;";
                        dvaddressmid2.InnerHtml = addressLine2 + "&nbsp;";
                        dvaddressmid3.InnerHtml = addressLine3 + "&nbsp;";
                    }
                }

                if (patientBrochureDetails != null)
                {
                    lblAmount.Text = string.Format("{0:0.00}", patientBrochureDetails.Amount);
                    lblEmailAddress.Text = patientBrochureDetails.EmailAddress;
                    lblPatientName.Text = patientBrochureDetails.PatientName;
                    lblBrochureDate.Text = patientBrochureDetails.BrochureDate.ToString("MM/dd/yyyy");
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        /// <summary>
        /// Fill PDF form
        /// </summary>
        private void fillPDFForm()
        {
            PatientBrochureDetails patientBrochureDetails = new PatientBrochureDetails();
            if (CommonLogic.GetSessionValue("PatientBrochure") != null)
                patientBrochureDetails = (PatientBrochureDetails)CommonLogic.GetSessionObject("PatientBrochure");

            if (patientBrochureDetails != null)
            {
                lblAmount.Text = string.Format("{0:0.00}", patientBrochureDetails.Amount);
                lblEmailAddress.Text = patientBrochureDetails.EmailAddress;
                lblPatientName.Text = patientBrochureDetails.PatientName;
                lblBrochureDate.Text = patientBrochureDetails.BrochureDate.ToString("MM/dd/yyyy");

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
                    txtPatientNameData.Text = patientBrochureDetails.PatientName;
                    txtPatientNameData.FontSize = 10;
                    pdfStamper.AddAnnotation(txtPatientNameData.GetTextField(), 1);

                    TextField txtAmount = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(250, 60, 80, 1050), "Amount");
                    txtAmount.Text = "Estimated Amount :";
                    txtAmount.FontSize = 10;
                    pdfStamper.AddAnnotation(txtAmount.GetTextField(), 1);

                    TextField txtAmountData = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(300, 60, 180, 1050), "AmounData");
                    txtAmountData.Text = string.Format("{0:0.00}", patientBrochureDetails.Amount);
                    txtAmountData.FontSize = 10;
                    pdfStamper.AddAnnotation(txtAmountData.GetTextField(), 1);

                    TextField txtPatientEmail = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(350, 60, 200, 1100), "PatientMail");
                    txtPatientEmail.Text = "Patient Email :";
                    txtPatientEmail.FontSize = 10;
                    pdfStamper.AddAnnotation(txtPatientEmail.GetTextField(), 1);

                    TextField txtDoctorEmailData = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(430, 60, 370, 1100), "DoctorEmailData");
                    txtDoctorEmailData.Text = patientBrochureDetails.EmailAddress;
                    txtDoctorEmailData.FontSize = 10;
                    pdfStamper.AddAnnotation(txtDoctorEmailData.GetTextField(), 1);

                    TextField txtBrochureDate = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(350, 60, 290, 1050), "BrochureDate");
                    txtBrochureDate.Text = "Brochure Date :";
                    txtBrochureDate.FontSize = 10;
                    pdfStamper.AddAnnotation(txtBrochureDate.GetTextField(), 1);

                    TextField txtBrochureDateData = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(430, 60, 370, 1050), "BrochureDateData");
                    txtBrochureDateData.Text = patientBrochureDetails.BrochureDate.ToString("MM/dd/yyyy");
                    txtBrochureDateData.FontSize = 10;
                    pdfStamper.AddAnnotation(txtBrochureDateData.GetTextField(), 1);

                    pdfStamper.Close();
                    DownloadFile(Response, newFile);
                }
            }
        }

        /// <summary>
        /// Fill PDF Doctor Information
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
                string address = currentSession.DoctorStreet;
                string addressLine2 = currentSession.DoctorCity + " " + currentSession.DoctorState;
                string addressLine3 = currentSession.DoctorCountry + " " + currentSession.DoctorZipcode;
                if (currentSession.UserType == UserType.D.ToString() || currentSession.UserType == UserType.S.ToString())
                {
                    dvName.InnerHtml = currentSession.DoctorName;
                    dvContact.InnerHtml = currentSession.EmailId;
                    dvNameDoctor.InnerHtml = currentSession.DoctorName;
                    dvContactdoctor.InnerHtml = currentSession.EmailId;
                    dvNamePage2.InnerHtml = currentSession.DoctorName;
                    dvdoctorEmpailpage2.InnerHtml = currentSession.EmailId;
                    dvmobile.InnerHtml = currentSession.DoctorMobile;
                    dvmobile2.InnerHtml = currentSession.DoctorMobile;

                    divaddress.InnerHtml = address;
                    divaddress2.InnerHtml = addressLine2;
                    divaddress3.InnerHtml = addressLine3;
                    dvadressmid.InnerHtml = address;
                    dvaddressmid2.InnerHtml = addressLine2;
                    dvaddressmid3.InnerHtml = addressLine3;
                }
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

                //string address = currentSession.DoctorStreet + "," + currentSession.DoctorCity + "," + currentSession.DoctorState;
                //string addressLine2 = currentSession.DoctorCountry + "," + currentSession.DoctorZipcode;

                //add content to the page using ColumnText
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Patient Name :"), 85, 585, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(patientBrochureDetails.PatientName), 205, 585, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Estimated Amount :"), 85, 570, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(string.Format("{0:0.00}", patientBrochureDetails.Amount)), 205, 570, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Patient Email :"), 345, 585, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(patientBrochureDetails.EmailAddress), 485, 585, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Print Date(mm/dd/yyyy) :"), 345, 570, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(patientBrochureDetails.BrochureDate.ToString("MM/dd/yyyy")), 485, 570, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorName, new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.WHITE)), 100, 545, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.EmailId, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, BaseColor.WHITE)), 100, 530, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorMobile, new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)), 100, 515, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(address, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, BaseColor.WHITE)), 100, 500, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(addressLine2, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, BaseColor.WHITE)), 100, 485, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(addressLine3, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, BaseColor.WHITE)), 100, 470, 0);
                //ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("4clearortho.com", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, new BaseColor(88, 87, 71))), 620, 525, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorName, new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, new BaseColor(88, 87, 71))), 330, 160, 0); // doctor name
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.EmailId, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 330, 145, 0);

                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorMobile, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 330, 130, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(address, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 330, 115, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(addressLine2, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 330, 100, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(addressLine3, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 330, 85, 0);

                //ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("481 Garrisonville Rd Ste 101", new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 350, 120, 0);
                //ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("Stafford, VA 22554 USA", new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 360, 110, 0);
                //ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorMobile, new Font(Font.FontFamily.HELVETICA, 11, Font.NORMAL, new BaseColor(88, 87, 71))), 340, 70, 0);

                // PdfContentByte from stamper to add content to the pages under the original content
                PdfContentByte pbunder = stamper.GetUnderContent(1);

                reader = new PdfReader(pathin);
                //select two pages from the original document
                reader.SelectPages("2-2");
                // PdfContentByte from stamper to add content to the pages over the original content
                pbover = stamper.GetOverContent(2);
                //add content to the page using ColumnText
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorName, new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.WHITE)), 90, 555, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.EmailId, new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)), 90, 540, 0);
                ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(currentSession.DoctorMobile, new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.WHITE)), 90, 525, 0);
                //ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase("4clearortho.com", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, new BaseColor(88, 87, 71))), 620, 490, 0);
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
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/TempFiles/"));
            foreach (FileInfo files in directory.GetFiles())
            {
                DateTime modifiedDate = System.IO.File.GetLastWriteTime(directory + files.Name);
                if (modifiedDate < DateTime.Now.AddDays(-1))
                {
                    File.Delete(directory + files.Name);
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
            //  response.WriteFile(filePath);
            response.TransmitFile(filePath);
            response.End();
        }

        #endregion
    }
}