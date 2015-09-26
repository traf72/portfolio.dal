using System;

namespace Portfolio.Dal.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Возвращает базовый репозиторий для указанной сущности
        /// </summary>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Возвращает репозиторий для новостей
        /// </summary>
        INewsRepository GetNewsRepository();

        /// <summary>
        /// Возвращает репозиторий для документов
        /// </summary>
        IDocumentRepository GetDocumentRepository();

        /// <summary>
        /// Возвращает репозиторий для фотогалерей
        /// </summary>
        IPhotoGalleryRepository GetPhotoGalleryRepository();

        /// <summary>
        /// Возвращает репозиторий для файлов
        /// </summary>
        IFileRepository GetFileRepository();

        /// <summary>
        /// Обновляет контекст
        /// </summary>
        void RefreshContext();

        /// <summary>
        /// Сохраняет все изменения в базу данных
        /// </summary>
        void Save();
    }
}