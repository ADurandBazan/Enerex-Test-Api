using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task SaveAsync(TEntity element, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity element, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity element, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync<TId>(TId elementId, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllByIdAsync(IEnumerable<int> elementIds, CancellationToken cancellationToken = default);

        void Save(TEntity element);
        void Update(TEntity element);
        void Delete(TEntity element);
        TEntity GetById<TId>(TId elementId);
    }
}
