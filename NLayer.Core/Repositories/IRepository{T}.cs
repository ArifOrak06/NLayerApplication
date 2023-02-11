using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IRepository<T> where T: class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T>  Where(Expression<Func<T,bool>> expression);
        IQueryable<T>  GetAll();
        Task<bool>  AnyAsnyc(Expression<Func<T,bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);    
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);




    }
}
