using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.DAL;
namespace _4eOrtho.BAL
{
    public class PatientBrochureEntity : BaseEntity
    {
        /// <summary>
        /// save record in patient brochure
        /// </summary>
        /// <param name="state"></param>
        public void Save(PatientBrochure brochure)
        {
            if (brochure.EntityState == System.Data.EntityState.Detached)
            {

                brochure.CreatedDate = BaseEntity.GetServerDateTime;
                brochure.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToPatientBrochures(brochure);
            }
            else
            {
                brochure.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }
    }
    public class PatientBrochureDetails
    {
        public string PatientName { get; set; }
        public string EmailAddress { get; set; }
        public decimal Amount { get; set; }
        public DateTime BrochureDate { get; set; }
    }
}
