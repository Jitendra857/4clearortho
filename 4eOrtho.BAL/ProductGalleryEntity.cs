using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _4eOrtho.Utility;
using _4eOrtho.DAL;
using System.Data.Objects;
using System.IO;
using System.Net.Mail;

namespace _4eOrtho.BAL
{
    public class ProductGalleryEntity : BaseEntity
    {
        /// <summary>
        /// create object method
        /// </summary>
        /// <returns></returns>
        public ProductGallery Create()
        {
            return orthoEntities.ProductGalleries.CreateObject();
        }
        /// <summary>
        /// save record in condition gallery table
        /// </summary>
        /// <param name="state"></param>
        public void Save(ProductGallery gallery)
        {
            if (gallery.EntityState == System.Data.EntityState.Detached)
            {
                gallery.CreatedDate = BaseEntity.GetServerDateTime;
                gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
                orthoEntities.AddToProductGalleries(gallery);               
            }
            else
            {
                gallery.LastUpdatedDate = BaseEntity.GetServerDateTime;
            }
            orthoEntities.SaveChanges();
        }
        /// <summary>
        /// remove all gallery files from conditional gallery table
        /// </summary>
        /// <param name="galleryId"></param>
        public void RemoveGalleryIdFiles(long productId)
        {
            List<ProductGallery> lstGallery = orthoEntities.ProductGalleries.Where(x => x.ProductId == productId).ToList();
            foreach (ProductGallery gallery in lstGallery)
            {
                orthoEntities.DeleteObject(gallery);
                orthoEntities.SaveChanges();
            }
        }
        /// <summary>
        /// get product gallery by product id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductGallery> GetProductGalleriesByProductId(long productId)
        {
            return orthoEntities.ProductGalleries.Where(x => x.ProductId == productId).ToList();
        }
          
    }

    [Serializable]
    public class ProductGalleryFiles
    {
        public string fileName { get; set; }
        public byte[] fileContent { get; set; }
        public bool IsNewUploaded { get; set; }
        public string PreviewUrl { get; set; }
    }

}
