using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);
        T Update(T entity);
        Task<T> Get(int id);
        Task<IEnumerable<T>> All();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetPagedReponse(int page, int size);
    }
}
