namespace Portfolio.Dal.Utils.ImagePreparers
{
    /// <summary>
    /// Фабрика для ImagePrepare'ов
    /// </summary>
    public static class ImagePreparerFactory
    {
        /// <summary>
        /// Создаёт ImagePreparer
        /// </summary>
        /// <param name="type">Тип ImagePreparer'а</param>
        /// <param name="rawImage">Сырое изображение</param>
        public static ImagePreparer Create(ImagePreparerType type, RawImage rawImage)
        {
            switch (type)
            {
                case ImagePreparerType.NewsAnouncementImage:
                    return new NewsAnouncementImagePreparer(rawImage);

                case ImagePreparerType.NewsTextImage:
                    return new NewsTextImagePreparer(rawImage);

                case ImagePreparerType.GalleryImage:
                    return new GalleryImagePreparer(rawImage);
            }

            return new ImagePreparer(rawImage);
        }
    }
}