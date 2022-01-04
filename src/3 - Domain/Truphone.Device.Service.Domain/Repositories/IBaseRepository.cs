using System.Collections.Generic;
using System.Threading.Tasks;
using Truphone.Device.Service.Domain.Entities;

namespace Truphone.Device.Service.Domain.Repositories
{
    public interface IBaseRepository<TEntity, TIdentifier>
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TIdentifier id);

        Task<TEntity> GetByIdAsync(TIdentifier id);

        Task<Page<TEntity>> GetPagedAsync(string brand, int pageIndex = 0, int pageSize = 30);
    }
}