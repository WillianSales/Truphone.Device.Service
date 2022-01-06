using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Truphone.Device.Service.Domain.Entities;
using Truphone.Device.Service.Domain.Repositories;

namespace Truphone.Device.Service.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly Dictionary<Guid, Domain.Entities.Device> _deviceDb;

        public DeviceRepository()
        {
            _deviceDb = new Dictionary<Guid, Domain.Entities.Device>();
        }

        public async Task<Domain.Entities.Device> CreateAsync(Domain.Entities.Device device, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var id = Guid.NewGuid();

            device.Id = id;
            device.CreationTime = DateTime.UtcNow;

            _deviceDb.Add(id, device);

            return device;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return _deviceDb.Remove(id);
        }

        public async Task<Domain.Entities.Device> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return _deviceDb.Values?.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Page<Domain.Entities.Device>> GetPagedAsync(string name, string brand, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var queryDevices = _deviceDb.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                queryDevices = queryDevices.Where(x => x.Value.Name.ToLower().Equals(name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(brand))
            {
                queryDevices = queryDevices.Where(x => x.Value.Brand.ToLower().Equals(brand.ToLower()));
            }

            if (pageIndex < 0)
                pageIndex = 0;

            if (pageSize <= 0)
                pageSize = 30;

            var count = queryDevices.LongCount();
            var totalPagesCount = ((count - 1) / pageSize) + 1;

            var devices = queryDevices.Skip(pageSize * pageIndex)
                .Take(pageSize)
                .Select(x => x.Value)
                .ToList();

            return new Page<Domain.Entities.Device>(devices, pageIndex, count, totalPagesCount);
        }

        public async Task<Domain.Entities.Device> UpdateAsync(Domain.Entities.Device device, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var deviceDB = await this.GetByIdAsync(device.Id, cancellationToken);

            device.CreationTime = deviceDB.CreationTime;

            return _deviceDb[device.Id] = device;
        }
    }
}