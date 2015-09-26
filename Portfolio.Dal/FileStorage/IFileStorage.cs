using System.IO;

namespace Portfolio.Dal.FileStorage
{
    /// <summary>
    /// Интерфейс для файлового хранилища
    /// </summary>
    public interface IFileStorage
    {
        /// <summary>
        /// Сохраняет файл
        /// </summary>
        /// <param name="containerId">Идентификатор контейнера, в который необходимо поместить файл</param>
        string SaveFile(string containerId, string fileName, Stream data);

        /// <summary>
        /// Получает файл
        /// </summary>
        Stream GetFile(FileInStorage file);
        
        /// <summary>
        /// Создаёт новый контейнер
        /// </summary>
        /// <returns>Идентификатор нового контейнера</returns>
        string CreateNewContainer();

        /// <summary>
        /// Проверяет - существует ли уже контейнер с таким идентификатором
        /// </summary>
        bool IsContainerExist(string containerId);

        /// <summary>
        /// Удаляет файл
        void DeleteFile(FileInStorage file);
    }
}