using System.Linq;
using Portfolio.Dal.Model;

namespace Portfolio.Dal.Filters
{
    /// <summary>
    /// Фильтр для фотогалерей
    /// </summary>
    public class PhotoGalleryFilter : IFilter<PhotoGallery>
    {
        public PhotoGalleryFilter()
        {
            IsDeleted = false;
        }

        /// <summary>
        /// Фильтровать по наименованию
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фильтровать по флагу публикации
        /// </summary>
        public bool? IsPublished { get; set; }

        /// <summary>
        /// Возвратить только те галереи, которые прикреплены к новости и не существуют автономно
        /// </summary>
        public bool OnlyNews { get; set; }

        /// <summary>
        /// Фильтровать удалённые
        /// </summary>
        public bool? IsDeleted { get; set; }

        public IQueryable<PhotoGallery> Apply(IQueryable<PhotoGallery> photoGalleries)
        {
            if (IsPublished.HasValue)
            {
                photoGalleries = photoGalleries.Where(pg => pg.IsPublished == IsPublished);
            }
            if (IsDeleted.HasValue)
            {
                photoGalleries = photoGalleries.Where(pg => pg.IsDeleted == IsDeleted);
            }
            if (OnlyNews)
            {
                photoGalleries = photoGalleries.Where(pg => !pg.IsPublished && pg.News.Any() && pg.News.Any(n => !n.IsGalleryVisibleInGalleries));
            }
            else if (!IsPublished.HasValue)
            {
                photoGalleries = photoGalleries.Where(pg => pg.IsPublished || !pg.News.Any() || pg.News.Any(n => n.IsGalleryVisibleInGalleries));
            }
            else if (!IsPublished.Value)
            {
                photoGalleries = photoGalleries.Where(pg => !pg.News.Any() || pg.News.Any(n => n.IsGalleryVisibleInGalleries));
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                photoGalleries = photoGalleries.Where(n => n.Name.Contains(Name));
            }
            return photoGalleries;
        }
    }
}