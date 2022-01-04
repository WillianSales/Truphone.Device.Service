using System.Collections.Generic;

namespace Truphone.Device.Service.Application.DTO
{
    public class PageDto<T>
    {
        public PageDto()
        {
            this.Entries = new List<T>();
        }

        public IList<T> Entries { get; set; }

        public long PageIndex { get; set; }

        public long TotalItems { get; set; }

        public long TotalPages { get; set; }
    }
}