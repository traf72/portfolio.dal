using System;
using System.Linq;
using System.Linq.Expressions;

namespace Portfolio.Dal.Repositories
{
    /// <summary>
    /// Интерфейс сортировщика
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISorter<TEntity>
    {
        /// <summary>
        /// Применяет к последовательности метод OrderBy
        /// </summary>
        IOrderedQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> source);

        /// <summary>
        /// Применяет к последовательности метод ThenBy
        /// </summary>
        IOrderedQueryable<TEntity> ApplyThenBy(IOrderedQueryable<TEntity> source);
    }

    public abstract class Sorter<TEntity> : ISorter<TEntity>
    {
        /// <summary>
        /// Создаёт сортировщик по определённому полю сущности
        /// </summary>
        /// <param name="keySelector">Expression, определяющий поле для сортировки</param>
        /// <param name="descending">Флаг сортировки по убыванию</param>
        public static Sorter<TEntity, TKeyType> Create<TKeyType>(Expression<Func<TEntity, TKeyType>> keySelector, bool descending = false)
        {
            return new Sorter<TEntity, TKeyType>(keySelector, descending);
        }

        public abstract IOrderedQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> source);

        public abstract IOrderedQueryable<TEntity> ApplyThenBy(IOrderedQueryable<TEntity> source);
    }

    public class Sorter<TEntity, TKeyType> : Sorter<TEntity>
    {
        /// <summary>
        /// Expression, определяющий поле для сортировки
        /// </summary>
        public Expression<Func<TEntity, TKeyType>> KeySelector { get; set; }

        /// <summary>
        /// Флаг сортировки по убыванию
        /// </summary>
        public bool Descending { get; set; }

        public Sorter(Expression<Func<TEntity, TKeyType>> keySelector, bool descending = false)
        {
            KeySelector = keySelector;
            Descending = descending;
        }

        public override IOrderedQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> source)
        {
            return Descending
                       ? source.OrderByDescending(KeySelector)
                       : source.OrderBy(KeySelector);
        }

        public override IOrderedQueryable<TEntity> ApplyThenBy(IOrderedQueryable<TEntity> source)
        {
            return Descending
                       ? source.ThenByDescending(KeySelector)
                       : source.ThenBy(KeySelector);
        }
    }
}