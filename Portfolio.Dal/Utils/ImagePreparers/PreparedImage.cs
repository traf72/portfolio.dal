using System.IO;

namespace Portfolio.Dal.Utils.ImagePreparers
{
    /// <summary>
    /// Обработанное изображение
    /// </summary>
    public class PreparedImage : RawImage
    {
        /// <summary>
        /// Превьюха
        /// </summary>
        public Stream Thumbnail { get; set; }
    }
}