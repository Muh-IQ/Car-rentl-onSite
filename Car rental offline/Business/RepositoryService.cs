using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class RepositoryService<T>(IRepository<T> repository) : IRepository<T> where T : class
    {
       
        public async Task<IEnumerable<T>> GetAllByTransactionAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> expression, params string[] includes)
        {
            return await repository.GetAllByTransactionAsync(pageIndex, pageSize, expression, includes); 
        }
        public async Task<int> CountElement(Expression<Func<T, bool>> expression)
        {
            return await repository.CountElement(expression);
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> expression)
        {
            return await repository.IsExist(expression);
        }
        public async Task<T?> Find(Expression<Func<T, bool>> expression)
        {
            return await repository.Find(expression);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<T> AddNew(T entity)
        {
            return await repository.AddNew(entity);
        }

        public async Task<bool> Update(T entity, Expression<Func<T, bool>> expression)
        {
            return await repository.Update(entity, expression);
        }

        public async Task<bool> Delete(Expression<Func<T, bool>> expression)
        {
            return await repository.Delete(expression);
        }
    }
}
