using System;
using System.Threading;
using System.Threading.Tasks;
using Truphone.Device.Service.Application.DTO;

namespace Truphone.Device.Service.Application.Services.Interfaces
{
    public interface IDeviceApplicationService
    {
        Task<PageDto<DeviceDto>> GetPagedAsync(string name, string brand, int pageIndex, int pageSize, CancellationToken cancellationToken);

        Task<DeviceDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<DeviceDto> CreateAsync(DeviceDto deviceDto, CancellationToken cancellationToken);

        Task<DeviceDto> UpdateAsync(DeviceDto deviceDto, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}