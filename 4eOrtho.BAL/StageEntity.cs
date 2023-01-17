using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using System.Data.Objects;

namespace _4eOrtho.BAL
{
    public class StageEntity : BaseEntity
    {   
        public Stage Create()
        {
            return orthoEntities.Stages.CreateObject();
        }
                
        public void Save(Stage stage)
        {
            if (stage.EntityState == System.Data.EntityState.Detached)
            {
                stage.CreatedDate = stage.LastUpdatedDate = BaseEntity.GetServerDateTime;                
                orthoEntities.AddToStages(stage);
            }
            else
            {
                stage.LastUpdatedDate = BaseEntity.GetServerDateTime;                
            }
            orthoEntities.SaveChanges();
        }

        public List<Stage> GetAllStage(string patientEmail)
        {
            return orthoEntities.Stages.Where(x => x.PatientEmail == patientEmail).ToList();
        }

        public List<Stage> GetStage(long stageId)
        {
            return orthoEntities.Stages.Where(x => x.StageId  == stageId).ToList();
        }

        public Stage GetStageById(long stageId)
        {
            return orthoEntities.Stages.Where(x => x.StageId == stageId).FirstOrDefault();
        }
        public void save()
        {
                orthoEntities.SaveChanges();
        }
    }
}
