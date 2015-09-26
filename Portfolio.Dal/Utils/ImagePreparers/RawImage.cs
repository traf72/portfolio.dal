using System.IO;

namespace Portfolio.Dal.Utils.ImagePreparers
{
    /// <summary>
    /// Изображение в сыром виде
    /// </summary>
    public class RawImage
    {
        public Stream Image { get; set; }

        public string Name { get; set; }

        public string MimeType { get; set; }
    }
}