using Domain.Common;
using Infrastructure.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Utilities
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected IUnitOfWork<ApplicationDbContext> _context;
        protected readonly DbSet<TEntity> Entity;

        public Repository(IUnitOfWork<ApplicationDbContext> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _context = context;
            Entity = context.Context.Set<TEntity>();

        }
        public void Delete(TEntity element)
        {
            Entity.Remove(element);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(TEntity element, CancellationToken cancellationToken = default)
        {
            Entity.Remove(element);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public void Save(TEntity element)
        {
            Entity.Add(element);
            _context.SaveChanges();
        }

        public async Task SaveAsync(TEntity element, CancellationToken cancellationToken)
        {
            Entity.Add(element);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(TEntity element)
        {
            Entity.Update(element);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(TEntity element, CancellationToken cancellationToken = default)
        {
            Entity.Update(element);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync<TId>(TId elementId, CancellationToken cancellationToken = default)
        {
            return await Entity.FindAsync(elementId, cancellationToken);
        }

        public TEntity GetById<TId>(TId elementId)
        {
            return Entity.Find(elementId);
        }

        public async Task<IEnumerable<TEntity>> GetAllByIdAsync(IEnumerable<int> elementIds, CancellationToken cancellationToken = default)
        {
            return await Entity.AsQueryable().Where(e => elementIds.Contains(e.Id)).ToListAsync(cancellationToken);
        }
    }
}
