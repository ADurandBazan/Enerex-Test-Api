using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Utilities
{
    /// <summary>
    /// Unit of work interface for managing database transactions
    /// </summary>
    /// <typeparam name="TContext">Type of the DbContext to be used by the unit of work</typeparam>
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        /// <summary>
        /// The DbContext instance used by the unit of work
        /// </summary>
        TContext Context { get; }

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        /// <returns>The number of state entries written to the database</returns>
        int SaveChanges();

        /// <summary>
        /// Saves changes to the database asynchronously
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the operation (optional)</param>
        /// <returns>A task that represents the asynchronous operation, containing the number of state entries written to the database</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Unit of work implementation for managing database transactions
    /// </summary>
    /// <typeparam name="TContext">Type of the DbContext to be used by the unit of work</typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        /// <summary>
        /// The DbContext instance used by the unit of work
        /// </summary>
        public TContext Context { get { return _context; } }

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class
        /// </summary>
        /// <param name="context">DbContext instance to be used by the unit of work</param>
        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        /// <returns>The number of state entries written to the database</returns>
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Saves changes to the database asynchronously
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the operation (optional)</param>
        /// <returns>A task that represents the asynchronous operation, containing the number of state entries written to the database</returns>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Disposes the unit of work and the underlying DbContext
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
