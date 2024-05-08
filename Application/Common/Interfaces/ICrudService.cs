using Application.Common.Utilities;

namespace Application.Common.Interfaces
{
    public interface ICrudService<TModel> where TModel : BaseEntityDto
    {
        Task<TModel> CreateAsync(TModel element, CancellationToken cancellationToken = default);
        Task<TModel?> UpdateAsync<T>(T id, TModel element, CancellationToken cancellationToken = default);
        Task DeleteAsync(int elementId, CancellationToken cancellationToken = default);
        Task<TModel> GetByIdAsync(int elementId, CancellationToken cancellationToken = default);
    }
}
