using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4eOrtho.ViewModels
{
    public class PatientStageViewModel
    {
        public int Id { get; set; }
        public int StageId { get; set; }
        public  string StageName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Status { get; set; }
        public double StageAmount { get; set; }
        public bool IsPaymentByPatient { get; set; }
    }
}