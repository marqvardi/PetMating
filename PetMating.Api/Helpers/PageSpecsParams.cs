namespace PetMating.Api.Helpers
{
    public class PageSpecsParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        public string Sort { get; set; }
        private int _pageSize = 10;
        private string _search;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value) > MaxPageSize ? MaxPageSize : value;
        }

        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}