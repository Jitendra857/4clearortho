using _4eOrtho.BAL;
using _4eOrtho.DAL;
using _4eOrtho.Helper;
using _4eOrtho.Utility;
using _4eOrtho.ViewModels;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.Services;

namespace _4eOrtho
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.     
    public class mobile : System.Web.Services.WebService
    {
        private ILog logger = log4net.LogManager.GetLogger(typeof(mobile));

        public mobile()
        {

        }

        [WebMethod(true)]
        public void RegisterDoctor()
        {
            try
            {
                string email = this.Context.Request.Form["email"];
                string first_name = this.Context.Request.Form["first_name"];
                string last_name = this.Context.Request.Form["last_name"];
                string gender = this.Context.Request.Form["gender"];
                string date_of_birth = this.Context.Request.Form["date_of_birth"];
                string country = this.Context.Request.Form["country"];
                string state = this.Context.Request.Form["state"];
                string mobile = this.Context.Request.Form["mobile"];
                string doctor_registration_no = this.Context.Request.Form["license_no"];
                string password = this.Context.Request.Form["password"];

                if (string.IsNullOrEmpty(email))
                {
                    jsonResponse["message"] = "Email is required";
                    jsonResponse["error"] = 1;
                }
                else if (string.IsNullOrEmpty(mobile))
                {
                    jsonResponse["message"] = "Mobile is required";
                    jsonResponse["error"] = 1;
                }
                else if (string.IsNullOrEmpty(date_of_birth))
                {
                    jsonResponse["message"] = "Date of birth is required";
                    jsonResponse["error"] = 1;
                }
                else if (!ValidateEmail(email))
                {
                    jsonResponse["message"] = "Please send valid email address";
                    jsonResponse["error"] = 1;
                }
                else if (new UserEntity().GetUserByEmailAddress(email) != null)
                {
                    jsonResponse["message"] = "There is already a user with this email address.";
                    jsonResponse["error"] = 1;
                }
                else if (string.IsNullOrEmpty(first_name))
                {
                    jsonResponse["message"] = "First name is required";
                    jsonResponse["error"] = 1;
                }
                else if (string.IsNullOrEmpty(last_name))
                {
                    jsonResponse["message"] = "Last name is required";
                    jsonResponse["error"] = 1;
                }
                else if (string.IsNullOrEmpty(gender))
                {
                    jsonResponse["message"] = "Gender is required";
                    jsonResponse["error"] = 1;
                }
                else if (string.IsNullOrEmpty(doctor_registration_no))
                {
                    jsonResponse["message"] = "Doctor registration number is required";
                    jsonResponse["error"] = 1;
                }
                else
                {
                    /* Account Information */
                    TransactionScope scope = new TransactionScope();

                    using (scope)
                    {
                        User orthoUser = new DAL.User();
                        UserDomainMaster userDomainMaster = new UserDomainMaster();

                        userDomainMaster.RoleType = "D";
                        userDomainMaster.EMailId = email.Trim();
                        userDomainMaster.IsUpdate = false;

                        string auto_generated_password = !string.IsNullOrEmpty(password) ? password : CreateRandomPassword();
                        userDomainMaster.Password = Cryptography.EncryptStringAES(auto_generated_password, CommonLogic.GetConfigValue("sharedSecret"));

                        DoctorEntity doctorEntity = new DoctorEntity();
                        Doctor doctor;

                        long adminUserId = new UserEntity().GetAdminUserId();

                        doctor = doctorEntity.Create();
                        doctor.CreatedBy = adminUserId;
                        doctor.DoctorNo = doctor_registration_no;
                        doctor.FirstName = first_name.Trim();
                        doctor.LastName = last_name.Trim();
                        doctor.DoctorSource = 1;
                      

                        if (!string.IsNullOrEmpty(date_of_birth))
                            doctor.DOB = DateTime.ParseExact(date_of_birth, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        else
                            doctor.DOB = null;

                        doctor.IsActive = true;
                        doctor.Gender = gender;

                        ContactMasterEntity contactMasterEntity = new ContactMasterEntity();
                        ContactMaster contact;

                        contact = contactMasterEntity.Create();
                        contact.CreatedBy = adminUserId;

                        if (!string.IsNullOrEmpty(mobile))
                            contact.Mobile = mobile.Trim();
                        contact.EmailID = email.Trim();
                        contact.CreatedBy = adminUserId;
                        contact.IsActive = true;
                        long contactId = contactMasterEntity.Save(contact);
                        doctor.ContactId = contactId;

                        AddressMasterEntity addressMasterEntity = new AddressMasterEntity();
                        AddressMaster address = addressMasterEntity.Create();
                        address.CreatedBy = adminUserId;

                        if (!string.IsNullOrEmpty(country))
                            address.CountryId = new CountryEntity().GetCountryIdByName(0, country);

                        if (!string.IsNullOrEmpty(state))
                            address.StateId = new StateEntity().GetStateIdByNameAndCountry(0, state, Convert.ToInt32(address.CountryId));

                        address.CreatedBy = adminUserId;
                        address.IsActive = true;
                        long addressId = addressMasterEntity.Save(address);

                        doctor.AddressId = addressId;
                        doctor.PhotographName = getProfileFilePath(this.Context);

                        long doctorid = doctorEntity.Save(doctor);

                        userDomainMaster.DomainURL = this.Context.Request.Url.ToString().Substring(0, this.Context.Request.Url.ToString().LastIndexOf('/'));
                        userDomainMaster.UserID = userDomainMaster.SourceId = doctorid;
                        userDomainMaster.SourceType = "Ortho";
                        userDomainMaster.DatabaseName = "4ClearOrtho";

                        orthoUser = new UserEntity().Create();
                        orthoUser.CreatedDate = DateTime.Now;
                        orthoUser.EmailAddress = userDomainMaster.EMailId;
                        orthoUser.FirstName = doctor.FirstName;
                        orthoUser.IsActive = true;
                        orthoUser.IsSuperAdmin = false;
                        orthoUser.LastName = doctor.LastName;
                        orthoUser.Password = userDomainMaster.Password;
                        orthoUser.UpdatedDate = DateTime.Now;
                        orthoUser.UserType = "D";
                        orthoUser.is_connect_user = true;

                        new UserEntity().Save(orthoUser);

                        UserConfigEntity userConfigEntity = new UserConfigEntity();
                        UserConfig userConfig = new UserConfig();
                        userConfig.IsPayment = true;
                        userConfig.CreatedBy = userConfig.UpdatedBy = adminUserId;
                        userConfig.CreatedDate = userConfig.UpdatedDate = DateTime.Now;
                        userConfig.IsAccountActivated = true;
                        userConfig.EmailId = email.Trim();
                        userConfig.CertificateFileName = getCertificateFilePath(this.Context);
                        userConfigEntity.Save(userConfig);

                        SendWelcomeUserEmail(email.Trim(), Cryptography.DecryptStringAES(userDomainMaster.Password, CommonLogic.GetConfigValue("sharedSecret")), first_name.Trim(), last_name.Trim(), email.Trim(), "UserRegistration");
                        scope.Complete();
                    }

                    jsonResponse["error"] = 0;
                    jsonResponse["message"] = "Doctor registered in 4ClearOrtho.com successfully.";
                }
            }
            catch (Exception ex)
            {
                jsonResponse["message"] = ex.Message.ToString();
                jsonResponse["error"] = 1;
                logger.Error("An error occured.", ex);
            }
            string _json_string = JsonConvert.SerializeObject(jsonResponse);
            Context.Response.Write(_json_string);
            EndResponse();
        }

        [WebMethod(true)]
        public void RegisterPatient()
        {
            try
            {
                string email = this.Context.Request.Form["email"];
                string first_name = this.Context.Request.Form["first_name"];
                string last_name = this.Context.Request.Form["last_name"];
                string gender = this.Context.Request.Form["gender"];
                string date_of_birth = this.Context.Request.Form["date_of_birth"];
                string password = this.Context.Request.Form["password"];
                string appointmentid = this.Context.Request.Form["appointment_id"];

                if (string.IsNullOrEmpty(email))
                {
                    jsonResponse["message"] = "Email is required";
                    jsonResponse["error"] = 1;
                }
                else if (!ValidateEmail(email))
                {
                    jsonResponse["message"] = "Please send valid email address";
                    jsonResponse["error"] = 1;
                }
                else if (new UserEntity().GetPatientUserByEmailAddress(email) != null)
                {
                    jsonResponse["message"] = "There is already a user with this email address.";
                    jsonResponse["error"] = 1;
                }
                else if (string.IsNullOrEmpty(first_name))
                {
                    jsonResponse["message"] = "First name is required";
                    jsonResponse["error"] = 1;
                }
                else if (string.IsNullOrEmpty(last_name))
                {
                    jsonResponse["message"] = "Last name is required";
                    jsonResponse["error"] = 1;
                }
                else if (string.IsNullOrEmpty(gender))
                {
                    jsonResponse["message"] = "Gender is required";
                    jsonResponse["error"] = 1;
                }
                else if (string.IsNullOrEmpty(date_of_birth))
                {
                    jsonResponse["message"] = "Date of birth is required";
                    jsonResponse["error"] = 1;
                }
                //else if (DateTime.ParseExact(date_of_birth, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture) == null)
                //{
                //    jsonResponse["message"] = "Please pass valid date of birth";
                //    jsonResponse["error"] = 1;
                //}
                else
                {

                    PatientEntity patientEntity = new PatientEntity();
                    Patient patient = patientEntity.Create();
                    patient.EmailId = email.Trim();
                    patient.FirstName = first_name.Trim();
                    patient.LastName = last_name.Trim();

                    //if (!string.IsNullOrEmpty(date_of_birth))
                    //    patient.BirthDate = DateTime.ParseExact(date_of_birth, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    patient.Gender = gender;
                    patient.BirthDate = DateTime.Now;
                    patient.IsActive = true;
                    patient.IsDelete = false;
                    patient.PatientSource = 1;
                    patient.AppointmentId = Convert.ToInt32(appointmentid);
                    long patientId = patientEntity.Save(patient);

                    //Add to UserDomainMaster
                    string auto_generated_password = !string.IsNullOrEmpty(password) ? password : CreateRandomPassword();
                    User user = new UserEntity().Create();
                    user.CreatedDate = DateTime.Now;
                    user.EmailAddress = email.Trim();
                    user.FirstName = first_name.Trim();
                    user.IsActive = true;
                    user.IsSuperAdmin = false;
                    user.LastName = last_name.Trim();
                    user.Password = Cryptography.EncryptStringAES(auto_generated_password, CommonLogic.GetConfigValue("SharedSecret"));
                    user.UpdatedDate = DateTime.Now;
                    user.UserType = "P";
                    user.is_connect_user = true;
                    new UserEntity().Save(user);

                    SendMailOnRegistrationComplete(patientId);

                    jsonResponse["error"] = 0;
                    jsonResponse["message"] = "Patient registered in 4ClearOrtho.com successfully.";
                }
            }
            catch (Exception ex)
            {
                jsonResponse["message"] = ex;// ex.Message.ToString();
                jsonResponse["error"] = 1;
                logger.Error("An error occured.", ex);
            }
            string _json_string = JsonConvert.SerializeObject(jsonResponse);
            Context.Response.Write(_json_string);
            EndResponse();
        }

        [WebMethod(true)]
        public void PushPatientPayment()
        {
            try
            {
                string email = this.Context.Request.Form["email"];
                string transaction_id = this.Context.Request.Form["transaction_id"];
                //string time_stamp = this.Context.Request.Form["time_stamp"];
                string amount = this.Context.Request.Form["amount"];
                string stageid = this.Context.Request.Form["stage_id"];
                string patientid = this.Context.Request.Form["patient_id"];
                string appointmentid = this.Context.Request.Form["appointment_id"];
                string description = this.Context.Request.Form["description"];
              

               

                if (string.IsNullOrEmpty(email))
                {
                    jsonResponse["message"] = "Email is required";
                    jsonResponse["error"] = 1;
                }
                else if (!ValidateEmail(email))
                {
                    jsonResponse["message"] = "Please send valid email address";
                    jsonResponse["error"] = 1;
                }
                else if (new UserEntity().GetPatientUserByEmailAddress(email) == null)
                {
                    jsonResponse["message"] = "There is no any patient user with this email address.";
                    jsonResponse["error"] = 1;
                }
                //else if (string.IsNullOrEmpty(time_stamp))
                //{
                //    jsonResponse["message"] = "Payment date & time is required";
                //    jsonResponse["error"] = 1;
                //}
                //else if (DateTime.ParseExact(time_stamp, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture) == null)
                //{
                //    jsonResponse["message"] = "Please pass valid date of payment time stamp";
                //    jsonResponse["error"] = 1;
                //}
                else if (string.IsNullOrEmpty(stageid))
                {
                    jsonResponse["message"] = "Stage id is required";
                    jsonResponse["error"] = 1;
                }
                else if (string.IsNullOrEmpty(patientid))
                {
                    jsonResponse["message"] = "Patient id is required";
                    jsonResponse["error"] = 1;
                }
                else if (string.IsNullOrEmpty(appointmentid))
                {
                    jsonResponse["message"] = "Appointment id is required";
                    jsonResponse["error"] = 1;
                }
                else
                {
                    PaymentSuccessEntity entity = new PaymentSuccessEntity();
                    _4eOrtho.DAL.PaymentSuccess payment = entity.Create();
                    payment.PatientEmailId = email;
                    payment.Ammount = Convert.ToDecimal(amount);
                    payment.StageId = Convert.ToInt16(stageid);
                    payment.PatientId = Convert.ToInt16(patientid);
                    payment.AppointmentId = Convert.ToInt16(appointmentid);
                    payment.TransactionId = transaction_id;
                    payment.TimeStamp = DateTime.Now;
                    payment.Status = "SUCCESS";
                    payment.CreatedDate = DateTime.Now;
                    payment.Description = description;

                    entity.Save(payment);

                    jsonResponse["error"] = 0;
                    jsonResponse["message"] = "Patient payment data saved successfully.";
                }
            }
            catch (Exception ex)
            {
                jsonResponse["message"] = ex.Message.ToString();
                jsonResponse["error"] = 1;
                logger.Error("An error occured.", ex);
            }
            string _json_string = JsonConvert.SerializeObject(jsonResponse);
            Context.Response.Write(_json_string);
            EndResponse();
        }

        /// <summary>
        /// API's response object 
        /// </summary>
        private Dictionary<string, object> jsonResponse = new Dictionary<string, object>()
        {
            {"message", ""},
            {"error", 0}
        };

        private void EndResponse()
        {
            Context.Response.ContentType = "application/json";
            Context.Response.ContentEncoding = Encoding.UTF8;
            Context.Response.Flush(); // Sends all currently buffered output to the client.
            Context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            Context.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
        }

        private void SendWelcomeUserEmail(string userName, string password, string firstName, string lastName, string emailId, string TemplateType)
        {
            try
            {
                string emailTemplatePath = String.Empty;
                if (TemplateType.Equals("ResetPassword"))
                {
                    emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("ResetPassword")).ToString();
                    CommonLogic.RegisterUserEmail(userName, password, firstName, lastName, emailTemplatePath, emailId, "Doctor", "EditUser");
                    CommonHelper.SaveRegisterUserEmailLog(userName, password, firstName, lastName, "Doctor", CommonLogic.GetConfigValue("FromNoReplyEmail"), emailId, null, null, emailTemplatePath, "4Clear Ortho – Login Credentials Changed");
                }
                else if (TemplateType.Equals("UserRegistration"))
                {
                    emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("UserRegistraion")).ToString();
                    CommonLogic.RegisterUserEmail(userName, password, firstName, lastName, emailTemplatePath, emailId, "Doctor", "Registration");
                    CommonHelper.SaveRegisterUserEmailLog(userName, password, firstName, lastName, "Doctor", CommonLogic.GetConfigValue("FromNoReplyEmail"), emailId, null, null, emailTemplatePath, "4Clear Ortho – Welcome User");
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }

        // Default size of random password is 15
        private static string CreateRandomPassword(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        private bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }

        private void SendMailOnRegistrationComplete(long lPatientId)
        {
            try
            {
                PatientRegistrationDetailForMail patientDetail = new PatientEntity().GetPatientRegistrationDetailForEmail(lPatientId);
                if (patientDetail != null)
                {
                    string emailTemplatePath = Server.MapPath(CommonLogic.GetConfigValue("PatientRegistrationMail")).ToString();
                    string sPassword = Cryptography.DecryptStringAES(patientDetail.Password, CommonLogic.GetConfigValue("SharedSecret"));
                    string userType = string.Empty;
                    if (patientDetail.UserType == "P")
                        userType = "Patient";
                    PatientEntity.PatientRegistrationMail(patientDetail.FirstName, patientDetail.LastName, sPassword, emailTemplatePath, patientDetail.EmailId, userType, "Registration", "", "", "");
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured.", ex);
            }
        }


        /// <summary>
        /// Upload the file on server of the HttpContext's requested file objects 
        /// If requested file object's contains the multiple files then it will return the file with the separator "||"
        /// </summary>
        /// <param name="context">HTTP Current Context</param>
        /// <param name="uploadFolderName">Destination file folder</param>
        /// <returns></returns>
        public string getCertificateFilePath(HttpContext context)
        {
            string file_path = string.Empty,
                saved_path = context.Request.PhysicalApplicationPath + "DoctorCertificate\\";

            if (!System.IO.Directory.Exists(saved_path))
            {
                System.IO.Directory.CreateDirectory(saved_path);
            }
            try
            {
                if (context.Request.Files["license_certificate"] != null)
                {
                    HttpPostedFile file = context.Request.Files["license_certificate"];
                    string dirPath = saved_path, upload_path = saved_path;
                    upload_path += file.FileName.ToString();
                    upload_path = dirPath + DateTime.Now.ToString("yyyyMMddHHmmssffff") + System.IO.Path.GetExtension(upload_path);
                    file.SaveAs(upload_path);
                    file_path += upload_path.Replace(Context.Request.PhysicalApplicationPath, string.Empty).Replace("\\", "//");
                    file_path = (new System.IO.FileInfo(file_path)).Name;
                }
            }
            catch (Exception upload_ex)
            {
                file_path = upload_ex.Message.ToString();
            }
            return file_path;
        }

        /// <summary>
        /// Upload the file on server of the HttpContext's requested file objects 
        /// If requested file object's contains the multiple files then it will return the file with the separator "||"
        /// </summary>
        /// <param name="context">HTTP Current Context</param>
        /// <param name="uploadFolderName">Destination file folder</param>
        /// <returns></returns>
        public string getProfileFilePath(HttpContext context)
        {
            string file_path = string.Empty,
                saved_path = context.Request.PhysicalApplicationPath + "Photograph\\";

            if (!System.IO.Directory.Exists(saved_path))
            {
                System.IO.Directory.CreateDirectory(saved_path);
            }
            try
            {
                if (context.Request.Files["image"] != null)
                {
                    HttpPostedFile file = context.Request.Files["image"];
                    string dirPath = saved_path, upload_path = saved_path;
                    upload_path += file.FileName.ToString();
                    upload_path = dirPath + DateTime.Now.ToString("yyyyMMddHHmmssffff") + System.IO.Path.GetExtension(upload_path);
                    file.SaveAs(upload_path);
                    file_path += upload_path.Replace(Context.Request.PhysicalApplicationPath, string.Empty).Replace("\\", "//");
                    file_path = (new System.IO.FileInfo(file_path)).Name;
                }
            }
            catch (Exception upload_ex)
            {
                file_path = upload_ex.Message.ToString();
            }
            return file_path;
        }

        //By jitendra
       [WebMethod(true)]
       public  void GetPatientStageDetails(int patientid=0)
        {
            string constr = SqlConnectionHelper.Connection; //@"data source=DESKTOP-NGNOP1Q\EXPRESS1;initial catalog=4ClearOrtho;Integrated Security=true;multipleactiveresultsets=True;user id=sa;password=joshi@123;";
            List<PatientStageViewModel> patientStage = new List<PatientStageViewModel>();

            // return this.View(viewModel);

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                //using (SqlCommand cmd = new SqlCommand("select ps.Id,ps.PatientId,p.FirstName+' '+p.LastName as PatientName,ps.StageId,s.StageName,ps.Status  from PatientStageDetails ps inner join Patient p  on p.PatientId=ps.PatientId inner join StageMaster s on s.StageId=ps.StageId", con))
                using (SqlCommand cmd = new SqlCommand("select ps.StageId,ps.PatientId,p.FirstName+' '+p.LastName as PatientName,ps.StageId,ps.StageName,ps.Status,ps.StageAmount,ps.isPaymentByPatient  from Stage ps inner join Patient p  on p.PatientId=ps.PatientId  where ps.PatientId=" + patientid+ " and ps.IsDelete!=1 ", con))
                {
                    // SqlDataReader rd = cmd.ExecuteReader();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PatientStageViewModel _objpstage = new PatientStageViewModel();
                            _objpstage.Id =Convert.ToInt32(reader[0]);
                           
                          
                            _objpstage.PatientId = Convert.ToInt32(reader[1]);
                            _objpstage.PatientName =reader[2].ToString();
                            _objpstage.StageId = Convert.ToInt32(reader[3]);
                            _objpstage.StageName = reader[4].ToString();
                            _objpstage.Status = GetStatus(Convert.ToInt32(reader[5]));
                            _objpstage.StageAmount = Convert.ToDouble(reader[6]);
                            _objpstage.IsPaymentByPatient = Convert.ToBoolean(reader[7]);

                            patientStage.Add(_objpstage);
                        }

                        jsonResponse["error"] = 0;
                        jsonResponse["message"] = "Patient stage list";
                        jsonResponse["result"] = patientStage;

                        string _json_string = JsonConvert.SerializeObject(jsonResponse);
                        Context.Response.Write(_json_string);
                      //  return patientStage;
                    }

                    // using (SqlDataAdapter sda = new SqlDataAdapter())
                    //{
                    //    cmd.Connection = con;
                    //    sda.SelectCommand = cmd;
                    //    using (DataTable dt = new DataTable())
                    //    {
                    //        sda.Fill(dt);
                    //        ViewState["Paging"] = dt;
                    //        GridView1.DataSource = dt;
                    //        GridView1.DataBind();
                    //    }
                    //}
                }
            }
        }

       
        public string GetStatus(int status)
        {
            switch (status)
            {
                case 0:
                    return "Pending";
                    
                case 1:
                    return "Completed";
                default:
                    return "Progress";


            }
        }

        public class PatientPaymentModel
        {
            public int StageId { get; set; }
            public int PatientId { get; set; }
            public int AppointmentId { get; set; }
            public double Payment { get; set; }
            public string Description { get; set; }
            public string PatientEmail { get; set; }
            public string TransactionId { get; set; }
        }
    }
}
