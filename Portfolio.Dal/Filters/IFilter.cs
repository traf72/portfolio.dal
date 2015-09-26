using System.Linq;

namespace Portfolio.Dal.Filters
{
    /// <summary>
    /// Интерфейс фильтра
    /// </summary>
    /// <typeparam name="T">Тип сущности, к которой применяется фильтр</typeparam>
    public interface IFilter<T>
    {
        /// <summary>
        /// Применяет фильтр к последовательности
        /// </summary>
        IQueryable<T> Apply(IQueryable<T> entities);
    }
}