using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Utilities
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        public TContext Context { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        public TContext Context { get { return _context; } }
        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
