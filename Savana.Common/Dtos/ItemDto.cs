namespace Savana.Common.Dtos
{
    public class ItemResponse<T> where T : class
    {
        public string Message { get; set; }
        public T Item { get; set; }
        public int StatusCode { get; set; }
    }
}