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

namespace Portfolio.Dal.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        private readonly IFileStorage fileStorage;

        public DocumentRepository(DbContext context, IFileStorage fileStorage)
            : base(context)
        {
            this.fileStorage = fileStorage;
        }

        public IQueryable<Document> GetDocuments(IRelatedEntities<Document> relatedEntities = null, IFilter<Document> filter = null, Paging paging = null,
            IEnumerable<ISorter<Document>> sorter = null)
        {
            var documents = relatedEntities == null ? Query() : Query(relatedEntities.Get());
            if (filter != null)
            {
                documents = filter.Apply(documents);
            }
            if (sorter != null)
            {
                documents = documents.OrderBy(sorter);
            }
            if (paging != null)
            {
                documents = paging.Apply(documents);
            }
            return documents;
        }

        public int GetDocumentsCount(IFilter<Document> filter = null)
        {
            return GetDocuments(filter: filter).Count();
        }

        public Document GetDocumentById(int id, IRelatedEntities<Document> relatedEntities = null)
        {
            return relatedEntities == null ? FindById(id) : Query(relatedEntities.Get()).Single(n => n.Id == id);
        }

        public File SaveDocumentFile(InputEntityFile<int> file)
        {
            var document = GetDocumentById(file.EntityId);
            if (file.IsImage)
            {
                // TODO нужно обрезать документ, если он очень большой
                // TODO также возможно в будущем генерировать превьюху
            }

            var containerId = fileStorage.CreateNewContainer();
            var fileNameInStorage = fileStorage.SaveFile(containerId, file.FileName, file.Data);

            var documentFile = new File
            {
                ContainerId = containerId,
                MimeType = file.MimeType,
                OriginalName = file.FileName,
                NameInStorage = fileNameInStorage,
                Size = file.Data.Length,
            };

            document.File = documentFile;
            SaveDocument(document);
            return document.File;
        }

        public void SaveDocument(Document document)
        {
            if (document.Id == 0)
            {
                Insert(document);
            }
            else
            {
                document.UpdateDate = DateTime.UtcNow;
                Update(document);
            }
        }

        public IEnumerable<int> GetAllDocumentYears()
        {
            return
                (from d in Query()
                 where d.DocumentDate != null
                 select d.DocumentDate.Value.Year).Distinct();
        }

        public void MarkAsDeleted(Document document)
        {
            document.IsDeleted = true;
        }

        public Document UnpublishDocument(int id)
        {
            var document = FindById(id);
            document.IsPublished = false;
            return document;
        }
    }
}