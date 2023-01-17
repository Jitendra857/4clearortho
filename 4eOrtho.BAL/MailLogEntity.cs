using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.BAL;
using _4eOrtho.DAL;

namespace _4eOrtho.BAL
{
    public class MailLogEntity : BaseEntity
    {
        /// <summary>
        ///  Method to save MailLog in add condition
        /// </summary>       
        public void Save(MailLog maillog)
        {
            orthoEntities.AddToMailLogs(maillog);
            orthoEntities.SaveChanges();            
        }

        /// <summary>
        ///  Method to create instance of MailLog
        /// </summary>
        /// <returns></returns>
        public MailLog Create()
        {
            return orthoEntities.MailLogs.CreateObject();
        }
    }
}
