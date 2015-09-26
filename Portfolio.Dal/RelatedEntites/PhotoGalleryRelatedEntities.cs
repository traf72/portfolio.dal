using Portfolio.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Portfolio.Dal.RelatedEntites
{
    public class PhotoGalleryRelatedEntities : IRelatedEntities<PhotoGallery>
    {
        /// <summary>
        /// Возвращать фотогалерею с главной картинкой
        /// </summary>
        public bool WithMainImage { get; set; }

        /// <summary>
        /// Возвращать фотогалерею со всеми картинками
        /// </summary>
        public bool WithAllImages { get; set; }

        public IEnumerable<Expression<Func<PhotoGallery, object>>> Get()
        {
            var includedEntities = new List<Expression<Func<PhotoGallery, object>>>();
            if (WithMainImage)
            {
                includedEntities.Add(pg => pg.MainImage);
            }
            if (WithAllImages)
            {
                includedEntities.Add(pg => pg.Images);
            }
            return includedEntities;
        }
    }
}