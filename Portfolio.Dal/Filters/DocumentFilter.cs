using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Portfolio.Dal.Model;

namespace Portfolio.Dal.Filters
{
    /// <summary>
    /// Фильтр для документов
    /// </summary>
    public class DocumentFilter : IFilter<Document>
    {
        public DocumentFilter()
        {
            IsDeleted = false;
        }

        /// <summary>
        /// Фильтровать по категории
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Фильтровать по нескольким категориям
        /// </summary>
        public IEnumerable<int> Categories { get; set; }

        /// <summary>
        /// Фильтровать по типу
        /// </summary>
        public int? TypeId { get; set; }

        /// <summary>
        /// Фильтровать по дате документа (дата документа больше указанной)
        /// </summary>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// Фильтровать по дате документа (дата документа меньше указанной)
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Фильтровать по флагу публикации
        /// </summary>
        public bool? IsPublished { get; set; }

        /// <summary>
        /// Фильтровать удалённые
        /// </summary>
        public bool? IsDeleted { get; set; }

        public IQueryable<Document> Apply(IQueryable<Document> documents)
        {
            if (CategoryId.HasValue)
            {
                documents = documents.Where(d => d.DocumentCategories.Select(c => c.Id).Contains(CategoryId.Value));
            }
            if (Categories != null)
            {
                documents = documents.Where(d => Categories.All(c => d.DocumentCategories.Select(x => x.Id).Contains(c)));
            }
            if (TypeId.HasValue)
            {
                documents = documents.Where(d => d.TypeId == TypeId);
            }
            if (BeginDate.HasValue)
            {
                documents = documents.Where(d => d.DocumentDate != null && d.DocumentDate.Value >= BeginDate);
            }
            if (EndDate.HasValue)
            {
                documents = documents.Where(d => d.DocumentDate != null && d.DocumentDate.Value <= EndDate);
            }
            if (IsPublished.HasValue)
            {
                documents = documents.Where(d => d.IsPublished == IsPublished);
            }
            if (IsDeleted.HasValue)
            {
                documents = documents.Where(n => n.IsDeleted == IsDeleted);
            }
            return documents;
        }
    }
}