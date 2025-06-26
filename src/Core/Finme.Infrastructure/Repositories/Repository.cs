using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using Finme.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Finme.Infrastructure.Repositories
{
    public class Repository<T>(AppDbContext context) : IRepository<T> where T : class
    {
        private readonly AppDbContext _context = context;

        public IQueryable<T> GetQueryable() => _context.Set<T>().AsQueryable();
        public async Task<T[]> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken) => await query.ToArrayAsync(cancellationToken);
        public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate) => await _context.Set<T>().Where(predicate).ToListAsync();
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate) => await _context.Set<T>().FirstOrDefaultAsync(predicate);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        // Novo método para consultar usuário com dependências
        public async Task<User?> GetUserWithDependenciesAsync(Expression<Func<User, bool>> predicate)
        {
            if (typeof(T) != typeof(User))
                throw new InvalidOperationException("This method is only valid for User entity.");

            return await _context.Set<User>()
                .Include(u => u.UserBanks)
                .Include(u => u.UserAddreses)
                .Include(u => u.Bookmarks)
                .Include(u => u.UserDocuments!.Where(x => x.DocumentType != "profile"))
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<Operation?> GetOperationWithDependenciesAsync(Expression<Func<Operation, bool>> predicate)
        {
            if (typeof(T) != typeof(Operation))
                throw new InvalidOperationException("This method is only valid for User entity.");

            return await _context.Set<Operation>()
                .Include(u => u.Files)
                .Include(u => u.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Bookmark>?> GetBookmarksWithDependenciesAsync(Expression<Func<Bookmark, bool>> predicate)
        {
            if (typeof(T) != typeof(Bookmark))
                throw new InvalidOperationException("This method is only valid for User entity.");

            return await _context.Set<Bookmark>()
                .Include(u => u.Operation)
                .Where(predicate).ToListAsync();
        }

        public async Task<Investment?> GetInvestmentsWithDependenciesAsync(Expression<Func<Investment, bool>> predicate)
        {
            if (typeof(T) != typeof(Investment))
                throw new InvalidOperationException("This method is only valid for User entity.");

            return await _context.Set<Investment>()
               .Include(u => u.InvestmentStatuses)
               .FirstOrDefaultAsync(predicate);
        }
    }
}
