using System;
using System.Threading;
using System.Threading.Tasks;
using Truphone.Device.Service.Domain.Entities;

namespace Truphone.Device.Service.Domain.Services.Interfaces
{
    public interface IDeviceDomainService
    {
        Task<Page<Entities.Device>> GetPagedAsync(string name, string brand, int pageIndex, int pageSize, CancellationToken cancellationToken);

        Task<Entities.Device> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Entities.Device> CreateAsync(Entities.Device device, CancellationToken cancellationToken);

        Task<Entities.Device> UpdateAsync(Entities.Device device, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}