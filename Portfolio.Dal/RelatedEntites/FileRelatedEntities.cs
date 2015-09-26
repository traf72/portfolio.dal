using Portfolio.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Portfolio.Dal.RelatedEntites
{
    public class FileRelatedEntities : IRelatedEntities<File>
    {
        /// <summary>
        /// Возвращать файл с превьюхой
        /// </summary>
        public bool WithThumbnails { get; set; }

        /// <summary>
        /// Возвращать файл с родителем
        /// </summary>
        public bool WithParent { get; set; }

        public IEnumerable<Expression<Func<File, object>>> Get()
        {
            var includedEntities = new List<Expression<Func<File, object>>>();
            if (WithThumbnails)
            {
                includedEntities.Add(f => f.Thumbnails);
            }
            if (WithParent)
            {
                includedEntities.Add(f => f.ParentFile);
            }
            return includedEntities;
        }
    }
}