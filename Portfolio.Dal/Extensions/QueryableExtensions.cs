using System;
using System.Collections.Generic;
using System.Linq;
using Portfolio.Dal.Repositories;

namespace Portfolio.Dal.Extensions
{
    /// <summary>
    /// Класс расширений для IQueryable
    /// </summary>
    internal static class QueryableExtensions
    {
        /// <summary>
        /// Применяет сортировку к последовательности
        /// </summary>
        /// <param name="sorters">Список сортировщиков</param>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<ISorter<T>> sorters)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (sorters == null)
            {
                return source;
            }
            using (var enumerator = sorters.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return source;
                }
                var orderedSource = enumerator.Current.ApplyOrderBy(source);
                while (enumerator.MoveNext())
                {
                    orderedSource = enumerator.Current.ApplyThenBy(orderedSource);
                }
                return orderedSource;
            }
        }
    }
}