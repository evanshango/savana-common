using Savana.Common.Entities;

namespace Savana.Common.Dtos
{
    public class ItemResponse<T> where T: BaseEntity
    {
        public string Message { get; set; }
        public T Item { get; set; }
        public int StatusCode { get; set; }
    }
}   