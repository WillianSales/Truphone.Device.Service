using System;
using System.Threading.Tasks;
using Truphone.Device.Service.Application.DTO;
using Truphone.Device.Service.Application.Services.Interfaces;
using Truphone.Device.Service.CC.Common.Mapper;
using Truphone.Device.Service.Domain.Services.Interfaces;

namespace Truphone.Device.Service.Application.Services
{
    public class DeviceApplicationService : IDeviceApplicationService
    {
        private readonly IDeviceDomainService _deviceDomainService;

        public DeviceApplicationService(IDeviceDomainService deviceDomainService)
        {
            _deviceDomainService = deviceDomainService;
        }

        public async Task<DeviceDto> CreateAsync(DeviceDto deviceDto)
        {
            var device = await _deviceDomainService.CreateAsync(deviceDto.MapTo<Domain.Entities.Device>());

            return device.MapTo<DeviceDto>();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _deviceDomainService.DeleteAsync(id);
        }

        public async Task<DeviceDto> GetByIdAsync(Guid id)
        {
            var device = await _deviceDomainService.GetByIdAsync(id);

            return device.MapTo<DeviceDto>();
        }

        public async Task<PageDto<DeviceDto>> GetPagedAsync(string brand, int pageIndex = 0, int pageSize = 30)
        {
            var pagedDevice = await _deviceDomainService.GetPagedAsync(brand, pageIndex, pageSize);

            return pagedDevice.MapTo<PageDto<DeviceDto>>();
        }

        public async Task<DeviceDto> UpdateAsync(DeviceDto deviceDto)
        {
            var device = await _deviceDomainService.UpdateAsync(deviceDto.MapTo<Domain.Entities.Device>());

            return device.MapTo<DeviceDto>();
        }
    }
}