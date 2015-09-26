using Portfolio.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Portfolio.Dal.RelatedEntites
{
    public class NewsRelatedEntities : IRelatedEntities<News>
    {
        /// <summary>
        /// Возвращать новость со связанной фотогалереей
        /// </summary>
        public bool WithPhotoGallery { get; set; }

        /// <summary>
        /// Возвращать новость с изображениями из связанной фотогалереи
        /// </summary>
        public bool WithPhotoGalleryImages { get; set; }

        /// <summary>
        /// Возвращать новость с полным изображением
        /// </summary>
        public bool WithImage { get; set; }

        /// <summary>
        /// Возвращать новость с превьюхой
        /// </summary>
        public bool WithThumbnail { get; set; }

        public IEnumerable<Expression<Func<News, object>>> Get()
        {
            var includedEntities = new List<Expression<Func<News, object>>>();
            if (WithPhotoGallery)
            {
                includedEntities.Add(n => n.PhotoGallery);
            }
            if (WithPhotoGalleryImages)
            {
                includedEntities.Add(n => n.PhotoGallery.Images);
            }
            if (WithImage)
            {
                includedEntities.Add(n => n.Image);
            }
            if (WithThumbnail)
            {
                includedEntities.Add(n => n.Thumbnail);
            }
            return includedEntities;
        }
    }
}