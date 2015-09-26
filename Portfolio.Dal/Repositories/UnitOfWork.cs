using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using Portfolio.Dal.FileStorage;

namespace Portfolio.Dal.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext, new()
    {
        protected readonly DbContext context;
        private readonly Dictionary<Type, object> repositories;
        private INewsRepository newsRepository;
        private IDocumentRepository documentRepository;
        private IPhotoGalleryRepository photoGalleryRepository;
        private IFileRepository fileRepository;
        private bool disposed;

        public UnitOfWork()
        {
            context = new TContext();
            repositories = new Dictionary<Type, object>();
            disposed = false;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)))
            {
                return repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repository = new Repository<TEntity>(context);
            repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public INewsRepository GetNewsRepository()
        {
            return newsRepository ?? (newsRepository = new NewsRepository(context, new FileSystemStorage()));
        }

        public IDocumentRepository GetDocumentRepository()
        {
            return documentRepository ?? (documentRepository = new DocumentRepository(context, new FileSystemStorage()));
        }

        public IPhotoGalleryRepository GetPhotoGalleryRepository()
        {
            return photoGalleryRepository ?? (photoGalleryRepository = new PhotoGalleryRepository(context, new FileSystemStorage()));
        }

        public IFileRepository GetFileRepository()
        {
            return fileRepository ?? (fileRepository = new FileRepository(context));
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var sb = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    sb.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sb.AppendFormat("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void RefreshContext()
        {
            var refreshContext = ((IObjectContextAdapter)context).ObjectContext;
            var refreshableObjects = context.ChangeTracker.Entries().Select(c => c.Entity);
            refreshContext.Refresh(RefreshMode.StoreWins, refreshableObjects);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;
        }
    }
}