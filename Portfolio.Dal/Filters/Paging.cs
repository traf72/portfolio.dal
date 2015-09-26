using System.Linq;

namespace Portfolio.Dal.Filters
{
    /// <summary>
    /// Пагинатор
    /// </summary>
    public class Paging
    {
        /// <summary>
        /// Номер страницы
        /// </summary>
        public int PageNum { get; set; }

        /// <summary>
        /// Размер страницы
        /// </summary>
        public int PageSize { get; set; }

        public Paging()
        {
        }

        public Paging(int pageNum, int pageSize)
        {
            PageNum = pageNum;
            PageSize = pageSize;
        }
        
        /// <summary>
        /// Применяет пагинатор к последовательности
        /// </summary>
        public IQueryable<T> Apply<T>(IQueryable<T> entities)
        {
            return entities.Skip((PageNum - 1) * PageSize).Take(PageSize);
        }
    }
}