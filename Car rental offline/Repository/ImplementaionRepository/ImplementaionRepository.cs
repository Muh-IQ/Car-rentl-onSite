using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ImplementaionRepository
{
    public class ImplementaionRepository<T>(DbContext context) : IRepository<T> where T : class
    {
        public async Task<T> AddNew(T entity)
        {
            await context.Set<T>().AddAsync(entity);

            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountElement(Expression<Func<T, bool>> expression)
        {
            var query = context.Set<T>().AsQueryable();

            if (expression != null)
                query = query.Where(expression);


            return  query.ToList().Count();
        }

        public async Task<T> Find(Expression<Func<T?, bool>> expression)
        {
            return await context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByTransactionAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = context.Set<T>().AsQueryable();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            foreach (var include in includes)
                query = query.Include(include);

            return await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)

                .ToListAsync();
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> expression)
        {
            return await context.Set<T>().AnyAsync(expression);
        }

        public async Task<bool> Update(T entity, Expression<Func<T, bool>> expression)
        {
            var res = await context.Set<T>().FirstOrDefaultAsync(expression);
            if (res == null)
                return false;
            context.Entry(res).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Expression<Func<T, bool>> expression)
        {
            var entity = await context.Set<T>().FirstOrDefaultAsync(expression);

            if (entity == null)
                return false; 

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return true;
        }

    }
}
