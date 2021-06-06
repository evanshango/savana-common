using System.Collections.Generic;

namespace Savana.Common.Helpers
{
    public class Pagination<T> where T: class
    {
        private int Page { get; }
        private int PageSize { get; }
        private int TotalItems { get; }
        private IReadOnlyList<T> Data { get; }

        public Pagination(int pageIndex, int pageSize, int totalItems, IReadOnlyList<T> data)
        {
            Page = pageIndex;
            PageSize = pageSize;
            TotalItems = totalItems;
            Data = data;
        }
    }
}