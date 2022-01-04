using System;

namespace Truphone.Device.Service.Application.DTO
{
    public class DeviceDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public DateTime CreationTime { get; set; }
    }
}