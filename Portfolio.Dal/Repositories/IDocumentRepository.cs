using System.Collections.Generic;
using System.Linq;
using Portfolio.Dal.Dto;
using Portfolio.Dal.Filters;
using Portfolio.Dal.Model;
using Portfolio.Dal.RelatedEntites;

namespace Portfolio.Dal.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для документов
    /// </summary>
    public interface IDocumentRepository : IRepository<Document>
    {
        /// <summary>
        /// Получает документы в соответствии с определёнными условиями
        /// </summary>
        /// <param name="relatedEntities">Связанные сущности</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="paging">Пагинация</param>
        /// <param name="sorter">Сортировка</param>
        IQueryable<Document> GetDocuments(IRelatedEntities<Document> relatedEntities = null, IFilter<Document> filter = null, Paging paging = null,
            IEnumerable<ISorter<Document>> sorter = null);

        /// <summary>
        /// Получает кол-во документов с учётом фильтра
        /// </summary>
        int GetDocumentsCount(IFilter<Document> filter = null);

        /// <summary>
        /// Получает документ по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор документа</param>
        /// <param name="relatedEntities">Связанные сущности</param>
        Document GetDocumentById(int id, IRelatedEntities<Document> relatedEntities = null);

        /// <summary>
        /// Сохраняет файл документа
        /// </summary>
        File SaveDocumentFile(InputEntityFile<int> file);

        /// <summary>
        /// Сохраняет документ
        /// </summary>
        void SaveDocument(Document document);

        /// <summary>
        /// Снимает документ с публикации
        /// </summary>
        /// <param name="id">Идентификатор документа</param>
        Document UnpublishDocument(int id);

        /// <summary>
        /// Получает все года, за которые когда-либо публиковались документы
        /// </summary>
        IEnumerable<int> GetAllDocumentYears();

        /// <summary>
        /// Помечает документ как удалённый
        /// </summary>
        void MarkAsDeleted(Document document);
    }
}