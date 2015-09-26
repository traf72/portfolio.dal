using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Portfolio.Dal.Repositories
{
    /// <summary>
    /// Интерфейс базового репозитория
    /// </summary>
    /// <typeparam name="TEntity">Доменная сущность</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Получает сущность по идентификатору
        /// </summary>
        TEntity FindById<TId>(TId id);

        /// <summary>
        /// Проверяет - существует ли уже сущность в базе данных
        /// </summary>
        bool Exists<TId>(TId id);

        /// <summary>
        /// Получает все сущности из базы данных
        /// </summary>
        /// <param name="includeProperties">Список связанных сущностей</param>
        IQueryable<TEntity> Query(IEnumerable<Expression<Func<TEntity, object>>> includeProperties);

        /// <summary>
        /// Вставляет сущность в базу данных
        /// </summary>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Обновляет сущность в базе данных
        /// </summary>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Удаляет сущность по идентификатору
        /// </summary>
        void DeleteById<TId>(TId id);

        /// <summary>
        /// Удаляет сущность
        /// </summary>
        void Delete(TEntity entity);
    }
}