using Domain.Common;
using Infrastructure.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Common.Utilities
{
    /// <summary>
    /// Base repository class for CRUD operations
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity to be managed by the repository</typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields
        /// <summary>
        /// Unit of work for the repository
        /// </summary>
        protected IUnitOfWork<ApplicationDbContext> _context;

        /// <summary>
        /// DbSet for the entity type
        /// </summary>
        protected readonly DbSet<TEntity> Entity;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Repository class
        /// </summary>
        /// <param name="context">Unit of work for the repository</param>
        public Repository(IUnitOfWork<ApplicationDbContext> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _context = context;
            Entity = context.Context.Set<TEntity>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="element">Entity to be deleted</param>
        public void Delete(TEntity element)
        {
            Entity.Remove(element);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes an entity asynchronously
        /// </summary>
        /// <param name="element">Entity to be deleted</param>
        /// <param name="cancellationToken">Cancellation token for the operation (optional)</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task DeleteAsync(TEntity element, CancellationToken cancellationToken = default)
        {
            Entity.Remove(element);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Saves an entity
        /// </summary>
        /// <param name="element">Entity to be saved</param>
        public void Save(TEntity element)
        {
            Entity.Add(element);
            _context.SaveChanges();
        }

        /// <summary>
        /// Saves an entity asynchronously
        /// </summary>
        /// <param name="element">Entity to be saved</param>
        /// <param name="cancellationToken">Cancellation token for the operation (optional)</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task SaveAsync(TEntity element, CancellationToken cancellationToken)
        {
            Entity.Add(element);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="element">Entity to be updated</param>
        public void Update(TEntity element)
        {
            Entity.Update(element);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates an entity asynchronously
        /// </summary>
        /// <param name="element">Entity to be updated</param>
        /// <param name="cancellationToken">Cancellation token for the operation (optional)</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task UpdateAsync(TEntity element, CancellationToken cancellationToken = default)
        {
            Entity.Update(element);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves an entity by ID asynchronously
        /// </summary>
        /// <typeparam name="TId">Type of the entity ID</typeparam>
        /// <param name="elementId">ID of the entity to be retrieved</param>
        /// <param name="cancellationToken">Cancellation token for the operation (optional)</param>
        /// <returns>A task that represents the asynchronous operation, containing the retrieved entity</returns>
        public async Task<TEntity> GetByIdAsync<TId>(TId elementId, CancellationToken cancellationToken = default)
        {
            return await Entity.FindAsync(elementId, cancellationToken);
        }

        /// <summary>
        /// Retrieves an entity by ID
        /// </summary>
        /// <typeparam name="TId">Type of the entity ID</typeparam>
        /// <param name="elementId">ID of the entity to be retrieved</param>
        /// <returns>The retrieved entity</returns>
        public TEntity GetById<TId>(TId elementId)
        {
            return Entity.Find(elementId);
        }

        /// <summary>
        /// Retrieves a list of entities by ID asynchronously
        /// </summary>
        /// <typeparam name="TId">Type of the entity ID</typeparam>
        /// <param name="elementIds">List of IDs of the entities to be retrieved</param>
        /// <param name="cancellationToken">Cancellation token for the operation (optional)</param>
        /// <returns>A task that represents the asynchronous operation, containing the retrieved list of entities</returns>
        public async Task<IEnumerable<TEntity>> GetAllByIdAsync(IEnumerable<int> elementIds, CancellationToken cancellationToken = default)
        {
            return await Entity.AsQueryable().Where(e => elementIds.Contains(e.Id)).ToListAsync(cancellationToken);
        } 
        #endregion
    }
}
