namespace Services.Viewmodels
{
    public class PagedResult<T>
    {
        public List<T> Results { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
