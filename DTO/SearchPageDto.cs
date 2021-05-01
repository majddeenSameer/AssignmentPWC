namespace DTO
{
    public class SearchPageDto<T>
    {
        public int Page { get; set; }
        public int PageIndex { get { return Page - 1; } }

        public int PageSize { get; set; }

        public string SortBy { get; set; }

        public OrderDirectionEnum OrderDirection { get; set; }

        public T Criteria { get; set; }
    }
}
