using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Utility;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using log4net;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace _4eOrtho
{
    public partial class PatientBeforeAfterImgPopup : System.Web.UI.Page
    {
        #region Declaration
        private ILog logger = log4net.LogManager.GetLogger(typeof(AppointmentRequest));
        private long galleryId;
        private string treatment = string.Empty;
        private string dname, pname = string.Empty;
        #endregion

        #region Event

        /// <summary>
        /// Page Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (CommonLogic.QueryString("galleryId") != null)
                        galleryId = Convert.ToInt64(CommonLogic.QueryString("galleryId"));
                    if (CommonLogic.QueryString("dname") != null)
                        dname = Convert.ToString(CommonLogic.QueryString("dname"));
                    if (CommonLogic.QueryString("pname") != null)
                        pname = Convert.ToString(CommonLogic.QueryString("pname"));
                    if (CommonLogic.QueryString("treatment") != null)
                        treatment = Convert.ToString(CommonLogic.QueryString("treatment"));

                    if (galleryId > 0)
                    {
                        lblDoctorName.Text = "Dr. " + dname;
                        if (string.IsNullOrEmpty(pname))
                            lblPatientName.Text = "Patient : " + ((CurrentSession)Session["UserLoginSession"]).PatientFirstName + " " + ((CurrentSession)Session["UserLoginSession"]).PatientLastName;
                        else
                            lblPatientName.Text = "Patient : " + pname;

                        lblBeforeAfter.Text = treatment + " Treatment";
                        DateTime date = DateTime.Now;

                        List<PatientGallery> lstPatientGallery = new PatientGalleryEntity().GetPatientGalleriesByGalleryId(galleryId);
                        if (lstPatientGallery != null && lstPatientGallery.Count > 0)
                        {
                            if (lstPatientGallery[0] != null)
                                Image1.ImageUrl = Server.UrlDecode(Request.Url.ToString()).Replace(Server.UrlDecode(Request.Url.PathAndQuery), "") + "/PatientFiles/slides/" + lstPatientGallery[0].FileName;
                            if (lstPatientGallery[1] != null)
                                Image2.ImageUrl = Server.UrlDecode(Request.Url.ToString()).Replace(Server.UrlDecode(Request.Url.PathAndQuery), "") + "/PatientFiles/slides/" + lstPatientGallery[1].FileName;
                            if (lstPatientGallery[2] != null)
                                Image3.ImageUrl = Server.UrlDecode(Request.Url.ToString()).Replace(Server.UrlDecode(Request.Url.PathAndQuery), "") + "/PatientFiles/slides/" + lstPatientGallery[2].FileName;
                            if (lstPatientGallery[3] != null)
                                Image4.ImageUrl = Server.UrlDecode(Request.Url.ToString()).Replace(Server.UrlDecode(Request.Url.PathAndQuery), "") + "/PatientFiles/slides/" + lstPatientGallery[3].FileName;
                            if (lstPatientGallery[4] != null)
                                Image5.ImageUrl = Server.UrlDecode(Request.Url.ToString()).Replace(Server.UrlDecode(Request.Url.PathAndQuery), "") + "/PatientFiles/slides/" + lstPatientGallery[4].FileName;
                            if (lstPatientGallery[5] != null)
                                Image6.ImageUrl = Server.UrlDecode(Request.Url.ToString()).Replace(Server.UrlDecode(Request.Url.PathAndQuery), "") + "/PatientFiles/slides/" + lstPatientGallery[5].FileName;
                            if (lstPatientGallery[6] != null)
                                Image7.ImageUrl = Server.UrlDecode(Request.Url.ToString()).Replace(Server.UrlDecode(Request.Url.PathAndQuery), "") + "/PatientFiles/slides/" + lstPatientGallery[6].FileName;
                            if (lstPatientGallery[7] != null)
                                Image8.ImageUrl = Server.UrlDecode(Request.Url.ToString()).Replace(Server.UrlDecode(Request.Url.PathAndQuery), "") + "/PatientFiles/slides/" + lstPatientGallery[7].FileName;

                            Session["lstPatientGallery"] = lstPatientGallery;
                            date = lstPatientGallery[0].CreatedDate;
                        }
                        lblCreatedDate.Text = lblCreatedDate.Text + " " + date.ToString("MM/dd/yyyy");
                    }
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
        /// Event to download pdf.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownload_Click(object sender, ImageClickEventArgs e)
        {
            GeneratePDF();
        }

        #endregion

        #region Helper

        /// <summary>
        /// Methodt to generate pdf.
        /// </summary>
        private void GeneratePDF()
        {
            Document document = new Document(PageSize.A4.Rotate(), 88f, 88f, 10f, 10f);
            iTextSharp.text.Font NormalFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                List<PatientGallery> lstPatientGallery = null;
                if (Session["lstPatientGallery"] != null)
                    lstPatientGallery = (List<PatientGallery>)Session["lstPatientGallery"];


                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                //iTextSharp.text.BaseColor color = null;

                document.Open();

                //Header Table
                table = new PdfPTable(3);
                table.TotalWidth = (float)(980 * 72 / 96);
                table.LockedWidth = true;
                table.DefaultCell.Border = PdfPCell.NO_BORDER;
                table.SetWidths(new float[] { 0.3f, 0.3f, 0.3f });

                int xheight = 225;
                int xweight = 305;

                cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstPatientGallery[0].FileName), xheight, xweight, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);
                cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstPatientGallery[1].FileName), xheight, xweight, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);
                cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstPatientGallery[2].FileName), xheight, xweight, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);
                cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstPatientGallery[3].FileName), xheight, xweight, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);


                PdfPTable innerTable = new PdfPTable(1);
                BaseColor myColor = WebColors.GetRGBColor("#164D8E");
                innerTable.SetTotalWidth(new float[] { 1f });
                innerTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                phrase = new Phrase();
                phrase.Add(new Chunk(lblBeforeAfter.Text, FontFactory.GetFont("Arial", 20, Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                cell.PaddingBottom = 20f;
                cell.BackgroundColor = myColor;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.BorderColor = myColor;
                innerTable.AddCell(cell);

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Content/images/logo.png"));
                image.ScaleAbsoluteHeight((float)(70 * 72 / 96));
                image.ScaleAbsoluteWidth((float)(115 * 72 / 96));

                PdfPCell innerCell = new PdfPCell(image);
                innerCell.BackgroundColor = myColor;
                innerCell.BorderColor = myColor;
                innerCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                innerCell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                innerCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                innerTable.AddCell(innerCell);

                phrase = new Phrase();
                phrase.Add(new Chunk(lblDoctorName.Text, FontFactory.GetFont("Arial", 20, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cell.BackgroundColor = myColor;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.BorderColor = myColor;
                innerTable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(lblPatientName.Text, FontFactory.GetFont("Arial", 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cell.BackgroundColor = myColor;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.BorderColor = myColor;
                innerTable.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(lblCreatedDate.Text, FontFactory.GetFont("Arial", 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cell.BackgroundColor = myColor;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.BorderColor = myColor;
                innerTable.AddCell(cell);

                cell.AddElement(innerTable);
                cell.PaddingLeft = 3f;
                cell.PaddingRight = 3f;
                cell.PaddingBottom = 10f;
                cell.PaddingTop = 10f;
                table.AddCell(cell);


                //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Content/images/logo.png"));
                //phrase = new Phrase();
                //phrase.Add(new Chunk(lblBeforeAfter.Text + "\n",FontFactory.GetFont("Arial", 20, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                //phrase.Add(new Chunk(image, 20f, 20f));
                //phrase.Add(new Chunk("\n" + lblDoctorName.Text + "\n", FontFactory.GetFont("Arial", 20, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                //phrase.Add(new Chunk(lblPatientName.Text, FontFactory.GetFont("Arial", 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));                                
                //cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                //cell.BorderColor = iTextSharp.text.BaseColor.WHITE;
                //cell.BorderWidth = 15f;
                //cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                //BaseColor myColor = WebColors.GetRGBColor("#164D8E");
                //cell.BackgroundColor = myColor;                
                //table.AddCell(cell);

                cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstPatientGallery[4].FileName), xheight, xweight, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);
                cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstPatientGallery[5].FileName), xheight, xweight, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);
                cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstPatientGallery[6].FileName), xheight, xweight, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);
                cell = ImageCell(Server.MapPath("~/PatientFiles/slides/" + lstPatientGallery[7].FileName), xheight, xweight, PdfPCell.ALIGN_CENTER);
                table.AddCell(cell);


                document.Add(table);
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lblBeforeAfter.Text.Replace(" ", "") + ".pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }

        /// <summary>
        /// Method to draw line in pdf.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, iTextSharp.text.BaseColor color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }

        /// <summary>
        /// Method to create pdf cell.
        /// </summary>
        /// <param name="phrase"></param>
        /// <param name="align"></param>
        /// <returns></returns>
        private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            //cell.BorderColor = iTextSharp.text.BaseColor.WHITE;                        
            cell.VerticalAlignment = align;
            cell.HorizontalAlignment = align;
            //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            //cell.HorizontalAlignment = align;
            return cell;
        }

        /// <summary>
        /// Method to create pdf image cell.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="align"></param>
        /// <returns></returns>
        private static PdfPCell ImageCell(string path, int height, int width, int align)
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
            image.ScaleAbsoluteHeight((float)(height * 72 / 96));
            image.ScaleAbsoluteWidth((float)(width * 72 / 96));
            //image.ScaleToFit((float)(width * 72 / 96), (float)(height * 72 / 96));
            PdfPCell cell = new PdfPCell(image);
            cell.BorderColor = iTextSharp.text.BaseColor.WHITE;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //cell.HorizontalAlignment = align;
            cell.PaddingLeft = 3f;
            cell.PaddingRight = 3f;
            cell.PaddingBottom = 10f;
            cell.PaddingTop = 10f;
            return cell;
        }

        #endregion
    }
}