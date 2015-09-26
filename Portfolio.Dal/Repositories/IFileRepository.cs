using Portfolio.Dal.Model;
using Portfolio.Dal.RelatedEntites;

namespace Portfolio.Dal.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для файлов
    /// </summary>
    public interface IFileRepository : IRepository<File>
    {
        /// <summary>
        /// Получает файл по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор файла</param>
        /// <param name="relatedEntities">Связанные сущности</param>
        File GetFileById(int id, IRelatedEntities<File> relatedEntities = null);
    }
}