using System;
using System.Threading;
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

        public async Task<DeviceDto> CreateAsync(DeviceDto deviceDto, CancellationToken cancellationToken)
        {
            var device = await _deviceDomainService.CreateAsync(deviceDto.MapTo<Domain.Entities.Device>(), cancellationToken);

            return device.MapTo<DeviceDto>();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _deviceDomainService.DeleteAsync(id, cancellationToken);
        }

        public async Task<DeviceDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var device = await _deviceDomainService.GetByIdAsync(id, cancellationToken);

            return device.MapTo<DeviceDto>();
        }

        public async Task<PageDto<DeviceDto>> GetPagedAsync(string name, string brand, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var pagedDevice = await _deviceDomainService.GetPagedAsync(name, brand, pageIndex, pageSize, cancellationToken);

            return pagedDevice.MapTo<PageDto<DeviceDto>>();
        }

        public async Task<DeviceDto> UpdateAsync(DeviceDto deviceDto, CancellationToken cancellationToken)
        {
            var device = await _deviceDomainService.UpdateAsync(deviceDto.MapTo<Domain.Entities.Device>(), cancellationToken);
            
            return device.MapTo<DeviceDto>();
        }
    }
}