using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _4eOrtho.Helper;
using System.Security.Cryptography;
using System.Text;

namespace _4eOrtho.Admin
{
    public partial class PatientCaseDetails : PageBase
    {
        #region Declaration
        long caseId = 0;
        private ILog logger = log4net.LogManager.GetLogger(typeof(UpdateTrackStatus));
        PatientCaseDetailEntity patientCaseEntity = new PatientCaseDetailEntity();
        PatientCaseDetail patientCaseDetails = null;
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["CaseId"] != null)
                    caseId = Convert.ToInt64(Session["CaseId"]);
                if (!Page.IsPostBack)
                {
                    if (caseId > 0)
                    {
                        GetCaseDetailById();
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (!string.IsNullOrEmpty(combobox.SelectedItem.Value))
                    {
                        if (patientCaseDetails == null)
                            patientCaseDetails = patientCaseEntity.GetPatientCaseById(caseId);

                        if (patientCaseDetails != null)
                        {
                            PatientCaseDetail sharedPatientCase = patientCaseEntity.Create();
                            sharedPatientCase.PatientId = patientCaseDetails.PatientId;
                            sharedPatientCase.OrthoSystem = patientCaseDetails.OrthoSystem;
                            sharedPatientCase.OrthoCondition = patientCaseDetails.OrthoCondition;
                            sharedPatientCase.Notes = patientCaseDetails.Notes;
                            sharedPatientCase.CreatedDate = DateTime.Now;
                            sharedPatientCase.LastUpdatedDate = DateTime.Now;
                            sharedPatientCase.IsActive = true;
                            sharedPatientCase.IsDelete = false;
                            sharedPatientCase.CaseNo = GetUniqueCaseNo(8, patientCaseDetails.Patient.FirstName, patientCaseDetails.Patient.LastName);
                            sharedPatientCase.DoctorEmailId = combobox.SelectedItem.Value;
                            sharedPatientCase.IsRework = patientCaseDetails.IsRework;
                            sharedPatientCase.IsRetainer = patientCaseDetails.IsRetainer;
                            sharedPatientCase.CaseCharge = patientCaseDetails.CaseCharge;
                            sharedPatientCase.CaseTypeId = patientCaseDetails.CaseTypeId;                            
                            sharedPatientCase.IsShared = false;
                            sharedPatientCase.SharedDoctorEmailId = patientCaseDetails.DoctorEmailId;

                            if (patientCaseDetails.SupplyOrderId > 0)
                            {
                                SupplyOrder supplyOrder = new SupplyOrderEntity().GetSupplyOrderById(patientCaseDetails.SupplyOrderId);
                                if (supplyOrder != null)
                                {
                                    SupplyOrderEntity supplyOrderEntity = new SupplyOrderEntity();
                                    SupplyOrder sharedSupplyOrder = supplyOrderEntity.Create();
                                    sharedSupplyOrder.Amount = supplyOrder.Amount;
                                    sharedSupplyOrder.CreatedBy = supplyOrder.CreatedBy;
                                    sharedSupplyOrder.CreatedDate = DateTime.Now;
                                    sharedSupplyOrder.EmailId = sharedPatientCase.DoctorEmailId;
                                    sharedSupplyOrder.FirstName = combobox.SelectedItem.Text.Split(' ')[0];
                                    sharedSupplyOrder.LastName = combobox.SelectedItem.Text.Split(' ')[1];
                                    sharedSupplyOrder.PackageId = supplyOrder.PackageId;
                                    sharedSupplyOrder.ProductId = supplyOrder.ProductId;
                                    sharedSupplyOrder.Quantity = supplyOrder.Quantity;
                                    sharedSupplyOrder.TotalAmount = supplyOrder.TotalAmount;
                                    sharedSupplyOrder.LastUpdatedDate = DateTime.Now;
                                    sharedSupplyOrder.IsDispatch = false;
                                    sharedSupplyOrder.IsRecieved = false;
                                    supplyOrderEntity.Save(sharedSupplyOrder);
                                    sharedPatientCase.SupplyOrderId = sharedSupplyOrder.SupplyOrderId;
                                }
                            }

                            long newCaseId = patientCaseEntity.Save(sharedPatientCase);

                            List<PatientGalleryMaster> lstPatientGalleryMaster = new PatientGalleryMasterEntity().GetPatientGalleryByCaseId(patientCaseDetails.CaseId);
                            if (lstPatientGalleryMaster != null && lstPatientGalleryMaster.Count > 0)
                            {
                                foreach (PatientGalleryMaster gallerymaster in lstPatientGalleryMaster)
                                {
                                    PatientGalleryMasterEntity entity = new PatientGalleryMasterEntity();
                                    PatientGalleryMaster master = new PatientGalleryMaster();
                                    master.Treatment = gallerymaster.Treatment;
                                    master.PatientId = gallerymaster.PatientId;
                                    master.BeforeGalleryId = gallerymaster.BeforeGalleryId;
                                    master.IsActive = gallerymaster.IsActive;
                                    master.DoctorEmail = sharedPatientCase.DoctorEmailId;
                                    master.CreatedDate = DateTime.Now;
                                    master.LastUpdatedDate = DateTime.Now;
                                    master.PatientEmail = gallerymaster.PatientEmail;
                                    master.isTemplate = gallerymaster.isTemplate;
                                    master.CaseId = newCaseId;
                                    master.IsDelete = false;
                                    long masterId = entity.Save(master);

                                    PatientGalleryEntity galleryEntity = new PatientGalleryEntity();
                                    List<PatientGallery> lstPatientGallery = galleryEntity.GetPatientGalleriesByGalleryId(gallerymaster.PatientGalleryId);

                                    foreach(PatientGallery gallery in lstPatientGallery)
                                    {
                                        PatientGallery gallery1 = galleryEntity.Create();
                                        gallery1.GalleryId = masterId;
                                        gallery1.FileName = gallery.FileName;
                                        gallery1.IsActive = true;
                                        gallery1.CreatedDate = DateTime.Now;
                                        gallery1.LastUpdatedDate = DateTime.Now;
                                        galleryEntity.Save(gallery1);
                                    }
                                }
                            }

                            patientCaseDetails.SharedDoctorEmailId = sharedPatientCase.DoctorEmailId;
                            patientCaseDetails.IsShared = false;
                            patientCaseEntity.Save(patientCaseDetails);

                            if (newCaseId > 0)
                                CommonHelper.ShowMessage(MessageType.Success, "Record saved successfully", divMsg, lblMsg);
                            else
                                CommonHelper.ShowMessage(MessageType.Error, "Failed to save record", divMsg, lblMsg);
                        }
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }
        }

        private string GetUniqueCaseNo(int size, string sFirstName, string sLastName)
        {
            char[] chars = new char[50];
            string a = "1234567890";
            byte[] data = new byte[size];
            StringBuilder result = new StringBuilder(size);
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            if (!string.IsNullOrEmpty(sFirstName))
            {
                chars = sFirstName.ToCharArray();
                result.Append(chars[0].ToString().ToUpper());
                chars = sLastName.ToCharArray();
                result.Append(chars[0].ToString().ToUpper());
                chars = a.ToCharArray();
                crypto.GetNonZeroBytes(data);
                foreach (byte b in data)
                { result.Append(chars[b % (chars.Length)]); }
            }
            return result.ToString();
        }
        #endregion

        #region Helper
        public void GetCaseDetailById()
        {
            try
            {
                patientCaseDetails = patientCaseEntity.GetPatientCaseById(caseId);
                if (patientCaseDetails != null)
                {
                    List<GetDoctorListFromOrthoAndAAAD_Result> lstDoctors = new DoctorEntity().GetAllGetDoctorListFromOrthoAndAAAD();

                    if (lstDoctors != null && lstDoctors.Count > 0)
                    {
                        lstDoctors.Remove(lstDoctors.Find(x => x.EmailId == patientCaseDetails.DoctorEmailId));

                        combobox.DataSource = lstDoctors;
                        combobox.DataTextField = "DoctorName";
                        combobox.DataValueField = "EmailId";
                        combobox.DataBind();
                        combobox.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Doctor", "0"));
                    }


                    if (!string.IsNullOrEmpty(patientCaseDetails.SharedDoctorEmailId))
                        hdnSharedDoctorEmail.Value = patientCaseDetails.SharedDoctorEmailId;

                    txtPatientName.Text = patientCaseDetails.Patient.FirstName + " " + patientCaseDetails.Patient.LastName;
                    txtDOB.Text = patientCaseDetails.Patient.BirthDate != null ? patientCaseDetails.Patient.BirthDate.ToString("dd-MMM-YYYY") : string.Empty;
                    txtGender.Text = patientCaseDetails.Patient.Gender == "M" ? "Male" : "Female";

                    txtCaseType.Text = new LookupMasterEntity().GetLookupMasterById(Convert.ToInt64(patientCaseDetails.CaseTypeId)).LookupName;
                    string[] orthoConditions = patientCaseDetails.OrthoCondition.Split(',');
                    for (int i = 0; i < orthoConditions.Length; i++)
                    {
                        switch (orthoConditions[i])
                        {
                            case "1":
                                txtOrthoCondition.Text = txtOrthoCondition.Text + ", " + OrthoCondition.CROWDING;
                                break;
                            case "2":
                                txtOrthoCondition.Text = txtOrthoCondition.Text + ", " + OrthoCondition.SPACING;
                                break;
                            case "3":
                                txtOrthoCondition.Text = txtOrthoCondition.Text + ", " + OrthoCondition.CROSSBITE;
                                break;
                            case "4":
                                txtOrthoCondition.Text = txtOrthoCondition.Text + ", " + OrthoCondition.ANTERIOR;
                                break;
                            case "5":
                                txtOrthoCondition.Text = txtOrthoCondition.Text + ", " + OrthoCondition.POSTERIOR;
                                break;
                            case "6":
                                txtOrthoCondition.Text = txtOrthoCondition.Text + ", " + OrthoCondition.POSTERIOR;
                                break;
                            case "7":
                                txtOrthoCondition.Text = txtOrthoCondition.Text + ", " + OrthoCondition.DEEPBITE;
                                break;
                            case "8":
                                txtOrthoCondition.Text = txtOrthoCondition.Text + ", " + OrthoCondition.NARROWARCH;
                                break;
                        }
                    }
                    txtOrthoCondition.Text = txtOrthoCondition.Text.Substring(1);
                    txtCaseDetails.Text = patientCaseDetails.Notes;

                    if (patientCaseDetails.SupplyOrderId > 0)
                    {
                        SupplyOrder supplyOrder = new SupplyOrderEntity().GetSupplyOrderById(patientCaseDetails.SupplyOrderId);
                        txtPackageAmount.Text = supplyOrder.Amount.ToString("0.00");
                        txtTotalPackageAmount.Text = Convert.ToDecimal(supplyOrder.TotalAmount).ToString("0.00");
                        txtQuantity.Text = Convert.ToString(supplyOrder.Quantity);

                        PackageMaster packageDetails = new PackageMasterEntity().GetPackageByPackageId(supplyOrder.PackageId);
                        if (packageDetails != null)
                        {
                            txtPackage.Text = packageDetails.PackageName;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured .", ex);
            }
        }

        #endregion

    }
}