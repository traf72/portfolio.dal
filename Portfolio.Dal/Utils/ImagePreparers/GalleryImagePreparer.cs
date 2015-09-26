namespace Portfolio.Dal.Utils.ImagePreparers
{
    /// <summary>
    /// Обработчик изображений для фотогалерей
    /// </summary>
    public class GalleryImagePreparer : DefaultImagePreparer
    {
        public GalleryImagePreparer(RawImage rawImage)
            : base(rawImage)
        {
            thumbnailMaxHeight = int.MaxValue;
            thumbnailMaxWidth = 260;
        }
    }
}