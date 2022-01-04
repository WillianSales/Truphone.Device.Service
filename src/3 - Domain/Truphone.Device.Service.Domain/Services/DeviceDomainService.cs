using System;
using System.Threading.Tasks;
using Truphone.Device.Service.Domain.Entities;
using Truphone.Device.Service.Domain.Repositories;
using Truphone.Device.Service.Domain.Services.Interfaces;

namespace Truphone.Device.Service.Domain.Services
{
    public class DeviceDomainService : IDeviceDomainService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceDomainService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<Entities.Device> CreateAsync(Entities.Device device)
        {
            return await _deviceRepository.CreateAsync(device);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _deviceRepository.DeleteAsync(id);
        }

        public async Task<Entities.Device> GetByIdAsync(Guid id)
        {
            return await _deviceRepository.GetByIdAsync(id);
        }

        public async Task<Page<Entities.Device>> GetPagedAsync(string brand, int pageIndex = 0, int pageSize = 30)
        {
            return await _deviceRepository.GetPagedAsync(brand, pageIndex, pageSize);
        }

        public async Task<Entities.Device> UpdateAsync(Entities.Device device)
        {
            return await _deviceRepository.UpdateAsync(device);
        }
    }
}