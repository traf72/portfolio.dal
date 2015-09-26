using Portfolio.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Portfolio.Dal.RelatedEntites
{
    public class DocumentRelatedEntities : IRelatedEntities<Document>
    {
        /// <summary>
        /// Возвращать документ вместе с файлом
        /// </summary>
        public bool WithFile { get; set; }

        /// <summary>
        /// Возвращать документ вместе с категориями
        /// </summary>
        public bool WithCategories { get; set; }

        public IEnumerable<Expression<Func<Document, object>>> Get()
        {
            var includedEntities = new List<Expression<Func<Document, object>>>();
            if (WithCategories)
            {
                includedEntities.Add(d => d.DocumentCategories);
            }
            if (WithFile)
            {
                includedEntities.Add(d => d.File);
            }
            return includedEntities;
        }
    }
}