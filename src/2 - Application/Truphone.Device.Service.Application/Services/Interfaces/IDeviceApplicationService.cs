using System;
using System.Threading.Tasks;
using Truphone.Device.Service.Application.DTO;

namespace Truphone.Device.Service.Application.Services.Interfaces
{
    public interface IDeviceApplicationService
    {
        Task<PageDto<DeviceDto>> GetPagedAsync(string brand, int pageIndex = 0, int pageSize = 30);

        Task<DeviceDto> GetByIdAsync(Guid id);

        Task<DeviceDto> CreateAsync(DeviceDto deviceDto);

        Task<DeviceDto> UpdateAsync(DeviceDto deviceDto);

        Task DeleteAsync(Guid id);
    }
}