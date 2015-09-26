using System.Collections.Generic;
using System.Linq;
using Portfolio.Dal.Dto;
using Portfolio.Dal.Filters;
using Portfolio.Dal.Model;
using Portfolio.Dal.RelatedEntites;

namespace Portfolio.Dal.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для новостей
    /// </summary>
    public interface INewsRepository : IRepository<News>
    {
        /// <summary>
        /// Получает новости в соответствии с определёнными условиями
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="paging">Пагинация</param>
        /// <param name="sorter">Сортировка</param>
        IQueryable<News> GetNews(IFilter<News> filter = null, Paging paging = null, IEnumerable<ISorter<News>> sorter = null);

        /// <summary>
        /// Получает кол-во новостей с учётом фильтра
        /// </summary>
        int GetNewsCount(IFilter<News> filter = null);

        /// <summary>
        /// Получает новость по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор новости</param>
        /// <param name="relatedEntities">Связанные сущности</param>
        News GetNewsById(int id, IRelatedEntities<News> relatedEntities = null);

        /// <summary>
        /// Сохраняет изображение для анонса новости
        /// </summary>
        File SaveNewsAnnouncementImage(InputEntityFile<int> file);

        /// <summary>
        /// Сохраняет изображение для основного текста новости
        /// </summary>
        File SaveNewsTextImage(InputEntityFile<int> file);

        /// <summary>
        /// Сохраняет новость
        /// </summary>
        void SaveNews(News news);

        /// <summary>
        /// Публикует новость
        /// </summary>
        /// <param name="id">Идентификатор новости</param>
        News PublishNews(int id);

        /// <summary>
        /// Снимает новость с публикации
        /// </summary>
        /// <param name="id">Идентификатор новости</param>
        News UnpublishNews(int id);

        /// <summary>
        /// Помечает новость как удалённую
        /// </summary>
        void MarkAsDeleted(News news);
    }
}