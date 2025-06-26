using Finme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetQueryable();
        Task<T[]> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(); // Adicionado para persistência
        Task<User?> GetUserWithDependenciesAsync(Expression<Func<User, bool>> predicate);
        Task<Operation?> GetOperationWithDependenciesAsync(Expression<Func<Operation, bool>> predicate);
        Task<List<Bookmark>?> GetBookmarksWithDependenciesAsync(Expression<Func<Bookmark, bool>> predicate);
        Task<Investment?> GetInvestmentsWithDependenciesAsync(Expression<Func<Investment, bool>> predicate);
    }
}
