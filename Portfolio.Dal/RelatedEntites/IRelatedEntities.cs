using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Portfolio.Dal.RelatedEntites
{
    /// <summary>
    /// Интерфейс для связанных сущностей
    /// </summary>
    /// <typeparam name="TEntity">Доменная сущность</typeparam>
    public interface IRelatedEntities<TEntity>
    {
        /// <summary>
        /// Возвращает список связанных сущностей
        /// </summary>
        IEnumerable<Expression<Func<TEntity, object>>> Get();
    }
}