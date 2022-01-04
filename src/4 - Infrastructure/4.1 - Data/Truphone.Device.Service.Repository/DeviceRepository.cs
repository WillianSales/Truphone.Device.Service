using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Domain.Entities.Device> CreateAsync(Domain.Entities.Device device)
        {
            _deviceDb.Add(Guid.NewGuid(), device);

            return device;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return _deviceDb.Remove(id);
        }

        public async Task<Domain.Entities.Device> GetByIdAsync(Guid id)
        {
            return _deviceDb.Values?.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Page<Domain.Entities.Device>> GetPagedAsync(string brand, int pageIndex = 0, int pageSize = 30)
        {
            var queryDevices = _deviceDb.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                queryDevices = queryDevices.Where(x => x.Value.Brand.ToLower().Equals(brand.ToLower()));
            }

            var count = queryDevices.LongCount();
            var totalPagesCount = ((count - 1) / pageSize) + 1;

            var devices = queryDevices.Skip(pageSize * pageIndex)
                .Take(pageSize)
                .Select(x => x.Value)
                .ToList();

            return new Page<Domain.Entities.Device>(devices, pageIndex, count, totalPagesCount);
        }

        public async Task<Domain.Entities.Device> UpdateAsync(Domain.Entities.Device device)
        {
            return _deviceDb[device.Id] = device;
        }
    }
}