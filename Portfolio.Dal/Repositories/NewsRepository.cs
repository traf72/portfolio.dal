using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Portfolio.Dal.Dto;
using Portfolio.Dal.Extensions;
using Portfolio.Dal.FileStorage;
using Portfolio.Dal.Filters;
using Portfolio.Dal.Model;
using Portfolio.Dal.RelatedEntites;
using Portfolio.Dal.Utils.ImagePreparers;

namespace Portfolio.Dal.Repositories
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        private readonly IFileStorage fileStorage;

        public NewsRepository(DbContext context, IFileStorage fileStorage)
            : base(context)
        {
            this.fileStorage = fileStorage;
        }

        public IQueryable<News> GetNews(IFilter<News> filter = null, Paging paging = null, IEnumerable<ISorter<News>> sorter = null)
        {
            var news = Query();
            if (filter != null)
            {
                news = filter.Apply(news);
            }
            if (sorter != null)
            {
                news = news.OrderBy(sorter);
            }
            if (paging != null)
            {
                news = paging.Apply(news);
            }
            return news;
        }

        public int GetNewsCount(IFilter<News> filter = null)
        {
            return GetNews(filter).Count();
        }

        public News GetNewsById(int id, IRelatedEntities<News> relatedEntities = null)
        {
            return relatedEntities == null ? FindById(id) : Query(relatedEntities.Get()).SingleOrDefault(n => n.Id == id);
        }

        public File SaveNewsAnnouncementImage(InputEntityFile<int> file)
        {
            // TODO Пока не грузим не картинки
            if (!file.IsImage)
            {
                return null;
            }

            var news = GetNewsById(file.EntityId);
            if (string.IsNullOrWhiteSpace(news.ContainerId))
            {
                news.ContainerId = fileStorage.CreateNewContainer();
            }
            var rawImage = new RawImage
            {
                Image = file.Data,
                Name = file.FileName,
                MimeType = file.MimeType,
            };

            var preparedAnnouncementImage = GetPreparedAnnouncementImage(rawImage);
            var announcementImageNameInStorage = fileStorage.SaveFile(news.ContainerId, string.Format("news_announcement_{0}", preparedAnnouncementImage.Name), preparedAnnouncementImage.Image);
            var announcementThumbnailNameInStorage = fileStorage.SaveFile(news.ContainerId, string.Format("thumbnail_news_announcement{0}", preparedAnnouncementImage.Name), preparedAnnouncementImage.Thumbnail);
            var announcementImage = new File
            {
                ContainerId = news.ContainerId,
                MimeType = preparedAnnouncementImage.MimeType,
                OriginalName = preparedAnnouncementImage.Name,
                NameInStorage = announcementImageNameInStorage,
                Size = preparedAnnouncementImage.Image.Length,
            };
            var announcementThumbmail = new File
            {
                ContainerId = news.ContainerId,
                MimeType = preparedAnnouncementImage.MimeType,
                OriginalName = preparedAnnouncementImage.Name,
                NameInStorage = announcementThumbnailNameInStorage,
                Size = preparedAnnouncementImage.Thumbnail.Length,
                ParentFile = announcementImage,
            };

            if (!news.ImageId.HasValue)
            {
                rawImage.Image.Position = 0;
                news.Image = GetNewsTextImage(news.ContainerId, rawImage);
            }

            news.Thumbnail = announcementThumbmail;
            SaveNews(news);
            return news.Thumbnail;
        }

        public File SaveNewsTextImage(InputEntityFile<int> file)
        {
            // TODO Пока не грузим не картинки
            if (!file.IsImage)
            {
                return null;
            }

            var news = GetNewsById(file.EntityId);
            if (string.IsNullOrWhiteSpace(news.ContainerId))
            {
                news.ContainerId = fileStorage.CreateNewContainer();
            }
            var rawImage = new RawImage
            {
                Image = file.Data,
                Name = file.FileName,
                MimeType = file.MimeType,
            };

            news.Image = GetNewsTextImage(news.ContainerId, rawImage);
            SaveNews(news);
            return news.Image;
        }

        private File GetNewsTextImage(string containerId, RawImage rawImage)
        {
            var preparedTextImage = GetPreparedTextImage(rawImage);
            var textImageNameInStorage = fileStorage.SaveFile(containerId, string.Format("news_text_{0}", preparedTextImage.Name), preparedTextImage.Image);
            var textThumbnailNameInStorage = fileStorage.SaveFile(containerId, string.Format("thumbnail_news_text_{0}", preparedTextImage.Name), preparedTextImage.Thumbnail);
            var textImage = new File
            {
                ContainerId = containerId,
                MimeType = preparedTextImage.MimeType,
                OriginalName = preparedTextImage.Name,
                NameInStorage = textImageNameInStorage,
                Size = preparedTextImage.Image.Length,
            };
            var textThumbmail = new File
            {
                ContainerId = containerId,
                MimeType = preparedTextImage.MimeType,
                OriginalName = preparedTextImage.Name,
                NameInStorage = textThumbnailNameInStorage,
                Size = preparedTextImage.Thumbnail.Length,
                ParentFile = textImage,
            };

            return textThumbmail;
        }

        private PreparedImage GetPreparedAnnouncementImage(RawImage rawImage)
        {
            var imagePreparer = ImagePreparerFactory.Create(ImagePreparerType.NewsAnouncementImage, rawImage);
            return imagePreparer.Prepare();
        }

        private PreparedImage GetPreparedTextImage(RawImage rawImage)
        {
            var imagePreparer = ImagePreparerFactory.Create(ImagePreparerType.NewsTextImage, rawImage);
            return imagePreparer.Prepare();
        }

        public void SaveNews(News news)
        {
            if (news.Id == 0)
            {
                news.ContainerId = fileStorage.CreateNewContainer();
                Insert(news);
            }
            else
            {
                news.UpdateDate = DateTime.UtcNow;
                Update(news);
            }
        }

        public News PublishNews(int id)
        {
            var news = FindById(id);
            news.IsPublished = true;
            news.PublishDate = DateTime.UtcNow;
            return news;
        }

        public News UnpublishNews(int id)
        {
            var news = FindById(id);
            news.IsPublished = false;
            return news;
        }

        public void MarkAsDeleted(News news)
        {
            news.IsDeleted = true;
        }
    }
}