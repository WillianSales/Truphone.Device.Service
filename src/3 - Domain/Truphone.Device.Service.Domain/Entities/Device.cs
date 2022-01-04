using System;

namespace Truphone.Device.Service.Domain.Entities
{
    public class Device
    {
        public Device()
        {
            Id = Guid.NewGuid();
            Name =  string.Empty;
            Brand = string.Empty;
            CreationTime = DateTime.UtcNow;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public DateTime CreationTime { get; set; }
    }
}