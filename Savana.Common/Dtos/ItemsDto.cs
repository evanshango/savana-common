using System.Collections.Generic;

namespace Savana.Common.Dtos
{
    public class ItemsDto<T> where T : class
    {
        public int Total { get; init; }
        public IReadOnlyList<T> Items { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }
}