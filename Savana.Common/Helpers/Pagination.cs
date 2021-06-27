using System.Collections.Generic;

namespace Savana.Common.Helpers
{
    public class Pagination<T> where T : class
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pageIndex, int pageSize, int total, IReadOnlyList<T> data)
        {
            Page = pageIndex;
            PageSize = pageSize;
            Total = total;
            Data = data;
        }
    }
}