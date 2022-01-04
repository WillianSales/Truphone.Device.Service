using System;

namespace Truphone.Device.Service.Domain.Repositories
{
    public interface IDeviceRepository : IBaseRepository<Entities.Device, Guid>
    {
    }
}