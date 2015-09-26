using System.Data.Entity;
using System.Linq;
using Portfolio.Dal.Model;
using Portfolio.Dal.RelatedEntites;

namespace Portfolio.Dal.Repositories
{
    public class FileRepository : Repository<File>, IFileRepository
    {
        public FileRepository(DbContext context)
            : base(context)
        {
        }

        public File GetFileById(int id, IRelatedEntities<File> relatedEntities = null)
        {
            return relatedEntities == null ? FindById(id) : Query(relatedEntities.Get()).Single(n => n.Id == id);
        }
    }
}