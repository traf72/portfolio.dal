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
    public class PhotoGalleryRepository : Repository<PhotoGallery>, IPhotoGalleryRepository
    {
        private readonly IFileStorage fileStorage;

        public PhotoGalleryRepository(DbContext context, IFileStorage fileStorage)
            : base(context)
        {
            this.fileStorage = fileStorage;
        }

        public IQueryable<PhotoGallery> GetPhotoGalleries(IFilter<PhotoGallery> filter = null, Paging paging = null,
            IEnumerable<ISorter<PhotoGallery>> sorter = null)
        {
            var photoGalleries = Query();
            if (filter != null)
            {
                photoGalleries = filter.Apply(photoGalleries);
            }
            if (sorter != null)
            {
                photoGalleries = photoGalleries.OrderBy(sorter);
            }
            if (paging != null)
            {
                photoGalleries = paging.Apply(photoGalleries);
            }
            return photoGalleries;
        }

        public int GetPhotoGalleriesCount(IFilter<PhotoGallery> filter = null)
        {
            return GetPhotoGalleries(filter).Count();
        }

        public PhotoGallery GetPhotoGalleryById(int id, IRelatedEntities<PhotoGallery> relatedEntities = null)
        {
            return relatedEntities == null ? FindById(id) : Query(relatedEntities.Get()).Single(n => n.Id == id);
        }

        public File SavePhotoGalleryImage(InputEntityFile<int> file)
        {
            // TODO Пока не грузим не картинки
            if (!file.IsImage)
            {
                return null;
            }

            var photoGallery = GetPhotoGalleryById(file.EntityId);
            var containerId = photoGallery.ContainerId;
            var preparedImage = GetPreparedImage(file);
            var imageNameInStorage = fileStorage.SaveFile(containerId, preparedImage.Name, preparedImage.Image);
            var thumbnailNameInStorage = fileStorage.SaveFile(containerId, string.Format("thumbnail_{0}", preparedImage.Name), preparedImage.Thumbnail);

            var galleryImage = new File
            {
                ContainerId = containerId,
                MimeType = preparedImage.MimeType,
                OriginalName = preparedImage.Name,
                NameInStorage = imageNameInStorage,
                Size = preparedImage.Image.Length,
            };

            var galleryThumbmail = new File
            {
                ContainerId = containerId,
                MimeType = preparedImage.MimeType,
                OriginalName = preparedImage.Name,
                NameInStorage = thumbnailNameInStorage,
                Size = preparedImage.Thumbnail.Length,
                ParentFile = galleryImage,
            };

            photoGallery.Images.Add(new PhotoGalleriesFile
                {
                    Image = galleryImage,
                    Thumbnail = galleryThumbmail,
                });

            if (!photoGallery.MainImageId.HasValue)
            {
                photoGallery.MainImage = galleryThumbmail;
            }

            SavePhotoGallery(photoGallery);
            return galleryThumbmail;
        }

        public void SavePhotoGallery(PhotoGallery photoGallery)
        {
            if (photoGallery.Id == 0)
            {
                photoGallery.ContainerId = fileStorage.CreateNewContainer();
                Insert(photoGallery);
            }
            else
            {
                photoGallery.UpdateDate = DateTime.UtcNow;
                Update(photoGallery);
            }
        }

        public PhotoGallery UnpublishPhotoGallery(int id)
        {
            var photoGallery = FindById(id);
            photoGallery.IsPublished = false;
            return photoGallery;
        }

        public void MarkAsDeleted(PhotoGallery photoGallery)
        {
            photoGallery.IsDeleted = true;
        }

        private PreparedImage GetPreparedImage(InputEntityFile<int> file)
        {
            var rawImage = new RawImage
            {
                Image = file.Data,
                Name = file.FileName,
                MimeType = file.MimeType,
            };

            var imagePreparer = ImagePreparerFactory.Create(ImagePreparerType.GalleryImage, rawImage);
            return imagePreparer.Prepare();
        }
    }
}