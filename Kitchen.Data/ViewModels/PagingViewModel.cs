using System.Collections.Generic;

namespace Kitchen.Data.ViewModels
{
    public class PagedParams
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 20;
    }

    public class QueryParams : PagedParams
    {
        public string SearchKeyword { get; set; } = string.Empty;
    }

    public class Pager<TEntity> where TEntity : class
    {
        public int Count { get; set; }

        public int Page { get; set; }

        public int Size { get; set; }
        public bool HasNextPage { get; set; }
        public IEnumerable<TEntity> Items { get; set; }
    }
}
