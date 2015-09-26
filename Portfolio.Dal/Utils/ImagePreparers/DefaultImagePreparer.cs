namespace Portfolio.Dal.Utils.ImagePreparers
{
    /// <summary>
    /// Обработчик изображений по умолчанию
    /// </summary>
    public class DefaultImagePreparer : ImagePreparer
    {
        public DefaultImagePreparer(RawImage rawImage)
            : base(rawImage)
        {
            imageMaxWidth = 800;
            imageMaxHeight = 600;
            thumbnailMaxWidth = 180;
            thumbnailMaxHeight = 160;
        }
    }
}