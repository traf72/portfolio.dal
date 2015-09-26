namespace Portfolio.Dal.Utils.ImagePreparers
{
    /// <summary>
    /// Обработчик изображений для анонса новости
    /// </summary>
    public class NewsAnouncementImagePreparer : DefaultImagePreparer
    {
        public NewsAnouncementImagePreparer(RawImage rawImage)
            : base(rawImage)
        {
            thumbnailMaxHeight = 240;
        }
    }
}