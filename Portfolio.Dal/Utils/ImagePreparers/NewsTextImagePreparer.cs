namespace Portfolio.Dal.Utils.ImagePreparers
{
    /// <summary>
    /// Обработчик для основного изображения новости
    /// </summary>
    public class NewsTextImagePreparer : DefaultImagePreparer
    {
        public NewsTextImagePreparer(RawImage rawImage)
            : base(rawImage)
        {
            thumbnailMaxWidth = 240;
            thumbnailMaxHeight = 500;
        }
    }
}