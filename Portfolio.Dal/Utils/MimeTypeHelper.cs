using System.Collections.Generic;

namespace Portfolio.Dal.Utils
{
    public static class MimeTypeHelper
    {
        private static readonly IList<string> customImageMimes = new List<string> { "application/jpg", "application/x-jpg" };
        private static readonly IList<string> customNotImageMimes = new List<string> { "image/vnd.djvu" };
        
        /// <summary>
        /// Проверяет, является ли файл изображением по МimeType
        /// </summary>
        public static bool IsImage(string mimeType)
        {
            if (string.IsNullOrWhiteSpace(mimeType))
            {
                return false;
            }

            return (mimeType.StartsWith("image/") && !customNotImageMimes.Contains(mimeType)) || customImageMimes.Contains(mimeType);
        }
    }
}