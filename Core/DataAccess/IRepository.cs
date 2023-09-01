using Core.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace Core.DataAccess.Repositories
{
    public interface IRepository<T> : IQuery<T> where T : BaseEntity
    {
        T Get(Expression<Func<T, bool>>? predicate);

        IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    int index = 0, int size = 10,
    bool enableTracking = true);

        IList<T> GetAsList(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy =
                null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include =
                null,
            bool enableTracking = true);

        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
        void DeleteRange(List<T> entities);
        void AddRange(List<T> entities);
    }
}
