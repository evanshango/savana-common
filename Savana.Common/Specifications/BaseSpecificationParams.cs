namespace Savana.Common.Specifications
{
    public class BaseSpecificationParams
    {
        private const int MaxPageSize = 50;
        public int Page { get; set; } = 1;
        private int _pageSize = 20;
        private string _search;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }

        public bool Active { get; set; } = true;
    }
}