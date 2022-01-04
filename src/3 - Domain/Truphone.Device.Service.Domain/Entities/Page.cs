using System.Collections.Generic;

namespace Truphone.Device.Service.Domain.Entities
{
    public class Page<T>
    {
        public Page(IList<T> entries, long pageIndex, long totalItems, long totalPages)
        {
            this.Entries = entries;
            this.PageIndex = pageIndex;
            this.TotalItems = totalItems;
            this.TotalPages = totalPages;
        }

        public IList<T> Entries { get; set; }

        public long PageIndex { get; set; }

        public long TotalItems { get; set; }

        public long TotalPages { get; set; }

        public int PageNumber { get; set; }

    }
}