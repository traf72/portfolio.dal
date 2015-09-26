using System.Collections.Generic;
using System.Linq;
using Portfolio.Dal.Dto;
using Portfolio.Dal.Filters;
using Portfolio.Dal.Model;
using Portfolio.Dal.RelatedEntites;

namespace Portfolio.Dal.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для фотогалерей
    /// </summary>
    public interface IPhotoGalleryRepository : IRepository<PhotoGallery>
    {
        /// <summary>
        /// Получает фотогалереи в соответствии с определёнными условиями
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="paging">Пагинация</param>
        /// <param name="sorter">Сортировка</param>
        IQueryable<PhotoGallery> GetPhotoGalleries(IFilter<PhotoGallery> filter = null, Paging paging = null,
            IEnumerable<ISorter<PhotoGallery>> sorter = null);

        /// <summary>
        /// Получает кол-во фотогалерей с учётом фильтра
        /// </summary>
        int GetPhotoGalleriesCount(IFilter<PhotoGallery> filter = null);

        /// <summary>
        /// Получает фотогалерею по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор фотогалереи</param>
        /// <param name="relatedEntities">Связанные сущности</param>
        PhotoGallery GetPhotoGalleryById(int id, IRelatedEntities<PhotoGallery> relatedEntities = null);

        /// <summary>
        /// Сохраняет изображение для фотогалереи
        /// </summary>
        File SavePhotoGalleryImage(InputEntityFile<int> file);

        /// <summary>
        /// Сохраняет фотогалерею
        /// </summary>
        void SavePhotoGallery(PhotoGallery photoGallery);

        /// <summary>
        /// Снимает фотогалерею с публикации
        /// </summary>
        /// <param name="id">Идентификатор фотогалереи</param>
        PhotoGallery UnpublishPhotoGallery(int id);

        /// <summary>
        /// Помечает фотогалерею как удалённую
        /// </summary>
        void MarkAsDeleted(PhotoGallery photoGallery);
    }
}