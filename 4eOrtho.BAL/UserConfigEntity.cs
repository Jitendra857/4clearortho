using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using _4eOrtho.DAL;
using _4eOrtho.Utility;
using System.Data.Objects;

namespace _4eOrtho.BAL
{
    public class UserConfigEntity : BaseEntity
    {
        /// <summary>
        /// Create instance of UserConfig object
        /// </summary>
        /// <returns></returns>
        public UserConfig Create()
        {
            return orthoEntities.UserConfigs.CreateObject();
        }

        /// <summary>
        /// Save UserConfig
        /// </summary>
        /// <param name="userConfig"></param>
        public void Save(UserConfig userConfig)
        {
            if (userConfig.EntityState == System.Data.EntityState.Detached)
            {
                userConfig.CreatedDate = BaseEntity.GetServerDateTime;
                userConfig.UpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToUserConfigs(userConfig);
            }
            else
            {
                userConfig.UpdatedDate = DateTime.Now;
            }
            orthoEntities.SaveChanges();
        }

        /// <summary>
        /// Get UserConfig by emai address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public UserConfig GetUserByEmailAddress(string emailAddress)
        {
            return orthoEntities.UserConfigs.Where(x => x.EmailId == emailAddress).FirstOrDefault();
        }
    }
}
