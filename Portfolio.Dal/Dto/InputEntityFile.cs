using System.IO;
using Portfolio.Dal.Utils;

namespace Portfolio.Dal.Dto
{
    /// <summary>
    /// Файл, который нужно прикрепить к сущности
    /// </summary>
    /// <typeparam name="T">Тип идентификатора сущности, к которой прикрепляется файл</typeparam>
    public class InputEntityFile<T>
    {
        /// <summary>
        /// Идентификатор сущности, к которой прикрепляется файл
        /// </summary>
        public T EntityId { get; set; }

        public string FileName { get; set; }

        public string MimeType { get; set; }

        public Stream Data { get; set; }

        /// <summary>
        /// Является ли файл изображением
        /// </summary>
        public bool IsImage
        {
            get { return MimeTypeHelper.IsImage(MimeType); }
        }
    }
}