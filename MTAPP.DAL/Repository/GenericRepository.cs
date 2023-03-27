using Microsoft.EntityFrameworkCore;

namespace MTAPP.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MTAppContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public GenericRepository(MTAppContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsNoTracking().AsEnumerable();
        }
        public DbSet<T> Get()
        {
            return entities;
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
