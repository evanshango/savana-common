using System.Collections.Generic;
using Savana.Common.Entities;

namespace Savana.Common.Dtos
{
    public class ItemsToReturnDto<T> where T: BaseEntity
    {   
        public int Total { get; init; }
        public IReadOnlyList<T> Items { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }
}