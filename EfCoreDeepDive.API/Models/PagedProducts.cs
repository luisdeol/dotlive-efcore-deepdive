using EfCoreDeepDive.API.Entities;

namespace EfCoreDeepDive.API.Models
{
    public class PagedProducts
    {
        public PagedProducts(List<Product> products, int pageNumber, int pageSize, int totalRecords)
        {
            Products = products;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }

        public List<Product> Products { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public int TotalPages => (int) Math.Ceiling((double)TotalRecords / PageSize);
        public bool HasPreviousPage => PageNumber > 0;
        public bool HasNextPage => PageNumber < TotalPages - 1;
        // Pagina = 1
        // 
    }

    public class PagedResult<T>
    {
        public PagedResult(List<T> items, int pageNumber, int pageSize, int totalRecords)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }

        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
        public bool HasPreviousPage => PageNumber > 0;
        public bool HasNextPage => PageNumber < TotalPages - 1;
        // Pagina = 1
        // 
    }
}
