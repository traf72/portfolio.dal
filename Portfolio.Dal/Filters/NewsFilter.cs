using Portfolio.Dal.Model;
using System;
using System.Linq;

namespace Portfolio.Dal.Filters
{
    /// <summary>
    /// Фильтр для новостей
    /// </summary>
    public class NewsFilter : IFilter<News>
    {
        public NewsFilter()
        {
            IsDeleted = false;
        }

        /// <summary>
        /// Фильтровать по флагу публикации
        /// </summary>
        public bool? IsPublished { get; set; }

        /// <summary>
        /// Фильтровать удалённые
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Фильтровать по категории
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Фильтровать по флагу афиша/новость
        /// </summary>
        public bool? IsPoster { get; set; }

        /// <summary>
        /// Фильтровать по важности
        /// </summary>
        public bool? IsImportant { get; set; }

        /// <summary>
        /// Фильтровать по году публикации
        /// </summary>
        public int? PublishYear { get; set; }

        public IQueryable<News> Apply(IQueryable<News> news)
        {
            if (IsPublished.HasValue)
            {
                news = news.Where(n => n.IsPublished == IsPublished);
            }
            if (IsDeleted.HasValue)
            {
                news = news.Where(n => n.IsDeleted == IsDeleted);
            }
            if (CategoryId.HasValue)
            {
                news = news.Where(n => n.CategoryId == CategoryId.Value);
            }
            if (IsPoster.HasValue)
            {
                news = news.Where(n => n.IsPoster == IsPoster.Value);
            }
            if (IsImportant.HasValue)
            {
                news = news.Where(n => n.IsImportant == IsImportant.Value);
            }
            if (PublishYear.HasValue)
            {
                news = news.Where(n => n.PublishDate.HasValue && n.PublishDate.Value.Year == PublishYear);
            }
            return news;
        }
    }
}