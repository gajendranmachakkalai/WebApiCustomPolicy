using Microsoft.EntityFrameworkCore;

namespace MTAPP.DAL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        void Delete(T entity);
        DbSet<T> Get();
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
    }
}