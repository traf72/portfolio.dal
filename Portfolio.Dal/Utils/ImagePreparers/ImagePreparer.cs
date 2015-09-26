using System.IO;
using TsSoft.Commons.Graphics;

namespace Portfolio.Dal.Utils.ImagePreparers
{
    /// <summary>
    /// Класс, обрабатывающий сырое изображение
    /// </summary>
    public class ImagePreparer
    {
        protected RawImage rawImage;
        protected ImageProcessor imageProcessor;
        protected PreparedImage preparedImage;
        protected int imageMaxWidth;
        protected int imageMaxHeight;
        protected int thumbnailMaxWidth;
        protected int thumbnailMaxHeight;

        public ImagePreparer(RawImage rawImage)
        {
            InitImageProcessor(rawImage.Image);
            this.rawImage = rawImage;
            preparedImage = new PreparedImage();
        }

        /// <summary>
        /// Инициализирует ImageProcessor из TsSoft.Commons.Graphics
        /// </summary>
        private void InitImageProcessor(Stream image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                imageProcessor = new ImageProcessor { Image = memoryStream.ToArray() };
                // TODO перенести в TsSoft.Commons.Graphics.ImageProcessor
                var bitmap = new System.Drawing.Bitmap(memoryStream);
                imageProcessor.Width = bitmap.Width;
                imageProcessor.Height = bitmap.Height;
            }
        }

        /// <summary>
        /// Подготовить изображение
        /// </summary>
        public virtual PreparedImage Prepare()
        {
            PrepareImage();
            PrepareThumbnail();
            SetExtraInfo();
            return preparedImage;
        }

        /// <summary>
        /// Установить для изображения дополнительную информацию
        /// </summary>
        private void SetExtraInfo()
        {
            // TODO TsSoft.Commons всегда конвертирует в jpg (что не очень хорошо, так как может потеряться прозрачность для png или анимация для gif)
            preparedImage.Name = string.Format("{0}.jpg", Path.GetFileNameWithoutExtension(rawImage.Name));
            preparedImage.MimeType = "image/jpeg";
        }

        /// <summary>
        /// Подготовить основное изображение
        /// </summary>
        protected virtual void PrepareImage()
        {
            preparedImage.Image = Resize(imageMaxWidth, imageMaxHeight, false);
        }

        /// <summary>
        /// Подготовить превьюху
        /// </summary>
        protected virtual void PrepareThumbnail()
        {
            preparedImage.Thumbnail = Resize(thumbnailMaxWidth, thumbnailMaxHeight, true);
        }

        /// <summary>
        /// Изменяет размер изображения, сохраняя пропорции
        /// </summary>
        /// <param name="maxWidth">Максимаьная ширина</param>
        /// <param name="maxHeight">Максимальная высота</param>
        /// <param name="force">Изменить изображение в любом случае, даже если размер увеличится (при этом может потеряться качество)</param>
        protected Stream Resize(int maxWidth, int maxHeight, bool force)
        {
            if (force
                || imageProcessor.Width > imageMaxWidth
                || imageProcessor.Height > imageMaxHeight)
            {
                imageProcessor.Resize(maxWidth, maxHeight);
            }
            return new MemoryStream(imageProcessor.Image);
        }
    }
}