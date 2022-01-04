using System;
using System.Threading.Tasks;
using Truphone.Device.Service.Domain.Entities;

namespace Truphone.Device.Service.Domain.Services.Interfaces
{
    public interface IDeviceDomainService
    {
        Task<Page<Entities.Device>> GetPagedAsync(string brand, int pageIndex = 0, int pageSize = 30);

        Task<Entities.Device> GetByIdAsync(Guid id);

        Task<Entities.Device> CreateAsync(Entities.Device device);

        Task<Entities.Device> UpdateAsync(Entities.Device device);

        Task DeleteAsync(Guid id);
    }
}