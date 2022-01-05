using System.Threading;
using System.Threading.Tasks;
using Truphone.Device.Service.Domain.Entities;

namespace Truphone.Device.Service.Domain.Repositories
{
    public interface IBaseRepository<TEntity, TIdentifier>
    {
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(TIdentifier id, CancellationToken cancellationToken);

        Task<TEntity> GetByIdAsync(TIdentifier id, CancellationToken cancellationToken);

        Task<Page<TEntity>> GetPagedAsync(string name, string brand, int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}