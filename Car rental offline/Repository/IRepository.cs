using System.Linq.Expressions;

namespace Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddNew(T entity);
        Task<bool> Update(T entity, Expression<Func<T, bool>> expression);
        Task<bool> Delete(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllByTransactionAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> expression, params string[] includes);
        Task<T> Find(Expression<Func<T, bool>> expression);
        Task<int> CountElement(Expression<Func<T, bool>> expression);
        Task<bool> IsExist(Expression<Func<T, bool>> expression);
    }

}
