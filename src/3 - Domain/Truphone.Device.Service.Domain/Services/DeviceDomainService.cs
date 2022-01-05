using System;
using System.Linq;
using System.Threading;
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

        public async Task<Entities.Device> CreateAsync(Entities.Device device, CancellationToken cancellationToken)
        {
            if (!device.IsValid())
                throw new ArgumentException("Invalid device.");

            var deviceDB = await _deviceRepository.GetPagedAsync(device.Name, device.Brand, 0, 1, cancellationToken);

            if (deviceDB != null && deviceDB.Entries != null && deviceDB.Entries.Any())
                throw new ApplicationException("There is already a device with this name and brand.");

            return await _deviceRepository.CreateAsync(device, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _deviceRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<Entities.Device> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _deviceRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Page<Entities.Device>> GetPagedAsync(string name, string brand, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            return await _deviceRepository.GetPagedAsync(name, brand, pageIndex, pageSize, cancellationToken);
        }

        public async Task<Entities.Device> UpdateAsync(Entities.Device device, CancellationToken cancellationToken)
        {
            if (!device.IsValid())
                throw new ArgumentException("Invalid device.");

            var deviceDB = await this.GetByIdAsync(device.Id, cancellationToken);

            if (deviceDB == null)
                throw new ApplicationException("Device not found.");

            var devicesDB = await _deviceRepository.GetPagedAsync(device.Name, device.Brand, 0, 1, cancellationToken);

            if (devicesDB != null && devicesDB.Entries != null && devicesDB.Entries.Any())
                throw new ApplicationException("There is already a device with this name and brand.");

            return await _deviceRepository.UpdateAsync(device, cancellationToken);
        }
    }
}