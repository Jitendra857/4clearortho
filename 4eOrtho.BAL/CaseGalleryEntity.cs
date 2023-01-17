using _4eOrtho.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4eOrtho.BAL
{
    public class CaseGalleryEntity: BaseEntity
    {
        public CaseGallery Create()
        {
            return orthoEntities.CaseGalleries.CreateObject();
        }

        public void Save(CaseGallery gallery)
        {
            if (gallery.EntityState == System.Data.EntityState.Detached)
            {
                gallery.CreatedDate = BaseEntity.GetServerDateTime;
                gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToCaseGalleries(gallery);
            }
            else
            {
                gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }

        public void RemoveGalleryIdFiles(long productId)
        {
            List<CaseGallery> lstGallery = orthoEntities.CaseGalleries.Where(x => x.CaseId == productId).ToList();
            foreach (CaseGallery gallery in lstGallery)
            {
                orthoEntities.DeleteObject(gallery);
                orthoEntities.SaveChanges();
            }
        }
        public List<CaseGallery> GetProductGalleriesByProductId(long productId)
        {
            return orthoEntities.CaseGalleries.Where(x => x.CaseId == productId).ToList();
        }
    }
}
